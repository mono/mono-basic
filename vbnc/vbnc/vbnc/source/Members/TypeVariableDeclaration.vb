' 
' Visual Basic.Net Compiler
' Copyright (C) 2004 - 2010 Rolf Bjarne Kvinge, RKvinge@novell.com
' 
' This library is free software; you can redistribute it and/or
' modify it under the terms of the GNU Lesser General Public
' License as published by the Free Software Foundation; either
' version 2.1 of the License, or (at your option) any later version.
' 
' This library is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
' Lesser General Public License for more details.
' 
' You should have received a copy of the GNU Lesser General Public
' License along with this library; if not, write to the Free Software
' Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
' 

''' <summary>
''' Represents one single variable of a VariableDeclarator.
''' 
''' VariableDeclarator  ::=
'''  	VariableIdentifiers  [  As  [  New  ]  TypeName  [  (  ArgumentList  )  ]  ]  |
'''     VariableIdentifier   [  As  TypeName  ]  [  =  VariableInitializer  ]
''' </summary>
''' <remarks></remarks>
Public Class TypeVariableDeclaration
    Inherits VariableDeclaration
    Implements IFieldMember, IHasImplicitMembers

    Private m_FieldBuilderCecil As Mono.Cecil.FieldDefinition

    Private m_HandledEvents As New Generic.List(Of Mono.Cecil.EventReference)

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
        UpdateDefinition()
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Modifiers As Modifiers, ByVal VariableIdentifier As VariableIdentifier, _
    ByVal IsNew As Boolean, ByVal TypeName As TypeName, ByVal VariableInitializer As VariableInitializer, ByVal ArgumentList As ArgumentList)
        MyBase.New(Parent, Modifiers, VariableIdentifier, IsNew, TypeName, VariableInitializer, ArgumentList)
        UpdateDefinition()
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Modifiers As Modifiers, ByVal VariableIdentifier As Identifier, _
    ByVal IsNew As Boolean, ByVal TypeName As TypeName, ByVal VariableInitializer As VariableInitializer, ByVal ArgumentList As ArgumentList)
        MyBase.New(Parent, Modifiers, VariableIdentifier, IsNew, TypeName, VariableInitializer, ArgumentList)
        UpdateDefinition()
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Identifier As Identifier, _
    ByVal IsNew As Boolean, ByVal TypeName As NonArrayTypeName, ByVal VariableInitializer As VariableInitializer, ByVal ArgumentList As ArgumentList)
        MyBase.New(Parent, Identifier, IsNew, TypeName, VariableInitializer, ArgumentList)
        UpdateDefinition()
    End Sub

    Shadows Sub Init(ByVal Modifiers As Modifiers, ByVal Name As String, ByVal VariableType As Mono.Cecil.TypeReference)
        MyBase.Init(Modifiers, Name, VariableType)
        UpdateDefinition()

        Helper.Assert(FieldType IsNot Nothing)
    End Sub

    Shadows Sub Init(ByVal Modifiers As Modifiers, ByVal Name As String, ByVal VariableType As TypeName)
        MyBase.Init(Modifiers, Name, VariableType)
        UpdateDefinition()

    End Sub

    ReadOnly Property FieldBuilder() As Mono.Cecil.FieldDefinition Implements IFieldMember.FieldBuilder
        Get
            Return m_FieldBuilderCecil
        End Get
    End Property

    ReadOnly Property IsFieldVariable() As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides ReadOnly Property MemberDescriptor() As Mono.Cecil.MemberReference
        Get
            Return m_FieldBuilderCecil
        End Get
    End Property

    ReadOnly Property FieldType() As Mono.Cecil.TypeReference
        Get
            Return VariableType
        End Get
    End Property

    Private ReadOnly Property FieldType2() As Mono.Cecil.TypeReference Implements IFieldMember.FieldType
        Get
            Return m_FieldBuilderCecil.FieldType
        End Get
    End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveTypeReferences AndAlso result

        If result = False Then Return result

        Helper.Assert(VariableType IsNot Nothing)

        UpdateDefinition()

        Return result
    End Function

    Function ResolveMember(ByVal Info As ResolveInfo) As Boolean Implements INonTypeMember.ResolveMember
        Dim result As Boolean = True

        Return result
    End Function

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveCode(Info) AndAlso result

        Return result
    End Function

    Overrides Sub UpdateDefinition()
        If Compiler Is Nothing Then Return
        MyBase.UpdateDefinition()
        Dim result As Boolean = True

        If m_FieldBuilderCecil Is Nothing Then
            m_FieldBuilderCecil = New Mono.Cecil.FieldDefinition(Name, Helper.GetAttributes(Compiler, Me), Helper.GetTypeOrTypeReference(Compiler, FieldType))
            DeclaringType.CecilType.Fields.Add(m_FieldBuilderCecil)
        End If
        m_FieldBuilderCecil.Attributes = Helper.GetAttributes(Compiler, Me)
        If VariableType IsNot Nothing AndAlso m_FieldBuilderCecil.FieldType Is Nothing Then
            m_FieldBuilderCecil.FieldType = Helper.GetTypeOrTypeReference(Compiler, VariableType)
        End If
        m_FieldBuilderCecil.Name = Name
        m_FieldBuilderCecil.IsStatic = Me.IsShared
    End Sub

    Public Function DefineMember() As Boolean Implements IDefinableMember.DefineMember
        Dim result As Boolean = True

        Return result
    End Function

    Friend Function DefineStaticMember() As Boolean
        Dim result As Boolean = True

        If FieldBuilder Is Nothing Then

            Dim staticName As String
            staticName = "$STATIC$" & Me.FindFirstParent(Of INameable).Name & "$" & Me.ObjectID.ToString & "$" & Me.Name
            m_FieldBuilderCecil = New Mono.Cecil.FieldDefinition(staticName, Helper.GetAttributes(Compiler, Me), Helper.GetTypeOrTypeReference(Compiler, FieldType))
            DeclaringType.CecilType.Fields.Add(m_FieldBuilderCecil)
        End If

        Return result
    End Function

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True
        Helper.Assert(VariableType IsNot Nothing)

        If Me.GeneratedCode = False Then
            result = MyBase.GenerateCode(Info) AndAlso result
        End If

        Return result
    End Function

    Protected Overrides Sub EmitStore(ByVal Info As EmitInfo)
        If FieldBuilder IsNot Nothing Then
            Emitter.EmitStoreField(Info, FieldBuilder)
        Else
            Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
        End If
    End Sub

    Protected Overrides Sub EmitThisIfNecessary(ByVal Info As EmitInfo)
        If FieldBuilder IsNot Nothing AndAlso FieldBuilder.IsStatic = False Then
            Emitter.EmitLoadMe(Info, FieldBuilder.DeclaringType)
        End If
    End Sub

    Public Function CreateImplicitMembers() As Boolean Implements IHasImplicitMembers.CreateImplicitMembers
        Dim result As Boolean = True

        If Me.Modifiers.Is(ModifierMasks.WithEvents) = False Then Return result

        Dim parentType As TypeDeclaration = Me.FindFirstParent(Of TypeDeclaration)()
        Dim propertyAccessor As New PropertyDeclaration(parentType)
        Dim modifiers As Modifiers

        If Me.IsShared Then
            modifiers.AddModifiers(ModifierMasks.Shared)
        Else
            modifiers.AddModifier(KS.Overridable)
        End If
        If (Me.Modifiers.Mask And ModifierMasks.AccessModifiers) = 0 Then
            modifiers.AddModifier(KS.Private)
        Else
            modifiers.AddModifiers(Me.Modifiers.Mask And ModifierMasks.AccessModifiers)
        End If

        propertyAccessor.Init(modifiers, Name, Me.TypeName)
        result = propertyAccessor.ResolveTypeReferences() AndAlso result
        propertyAccessor.HandlesField = Me
        propertyAccessor.SetDeclaration.MethodImplAttributes = Mono.Cecil.MethodImplAttributes.Synchronized

        Me.CustomAttributes.Add(New Attribute(Me, Compiler.TypeCache.System_Runtime_CompilerServices_AccessedThroughPropertyAttribute, Name))
        Rename("_" & Name)

        parentType.Members.Add(propertyAccessor)

        Return result
    End Function

    Public Function ResolveAndGetConstantValue(ByRef value As Object) As Boolean Implements IFieldMember.ResolveAndGetConstantValue
        Return False
    End Function
End Class
