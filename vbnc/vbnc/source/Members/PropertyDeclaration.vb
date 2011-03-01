' ''
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

Public Class PropertyDeclaration
    Inherits MemberDeclaration
    Implements INonTypeMember

    Private m_Signature As FunctionSignature
    Private m_Get As MethodDeclaration
    Private m_Set As MethodDeclaration
    Private m_MemberImplementsClause As MemberImplementsClause

    Private m_Handlers As Generic.List(Of AddOrRemoveHandlerStatement)
    Private m_HandlesField As TypeVariableDeclaration

    Private m_CecilBuilder As Mono.Cecil.PropertyDefinition

    Sub New(ByVal Parent As TypeDeclaration)
        MyBase.new(Parent)
    End Sub

    Overloads Sub Init(ByVal Modifiers As Modifiers, ByVal Name As String, ByVal ReturnType As TypeName)
        Me.Init(Modifiers, New FunctionSignature(Me, Name, Nothing, ReturnType, Me.Location), Nothing)
    End Sub

    Overloads Sub Init(ByVal Modifiers As Modifiers, ByVal Name As String, ByVal ReturnType As Mono.Cecil.TypeReference)
        Me.Init(Modifiers, New FunctionSignature(Me, Name, Nothing, ReturnType, Me.Location), Nothing)
    End Sub

    Overloads Sub Init(ByVal Modifiers As Modifiers, ByVal Signature As FunctionSignature, Optional ByVal GetMethod As PropertyGetDeclaration = Nothing, Optional ByVal SetMethod As PropertySetDeclaration = Nothing, Optional ByVal MemberImplementsClause As MemberImplementsClause = Nothing)
        MyBase.Init(Modifiers, Signature.Name)

        m_Signature = Signature

        If Modifiers.Is(ModifierMasks.ReadOnly) = False AndAlso SetMethod Is Nothing Then
            SetMethod = New PropertySetDeclaration(Me)
            SetMethod.Init(Modifiers, Nothing, Nothing, Nothing)
        End If
        If Modifiers.Is(ModifierMasks.WriteOnly) = False AndAlso GetMethod Is Nothing Then
            GetMethod = New PropertyGetDeclaration(Me)
            GetMethod.Init(Modifiers, Nothing, Nothing)
        End If

        m_Get = GetMethod
        m_Set = SetMethod
        m_MemberImplementsClause = MemberImplementsClause

        Helper.Assert(m_Signature IsNot Nothing)
    End Sub

    ReadOnly Property CecilBuilder() As Mono.Cecil.PropertyDefinition
        Get
            Return m_CecilBuilder
        End Get
    End Property

    Property HandlesField() As TypeVariableDeclaration
        Get
            Return m_HandlesField
        End Get
        Set(ByVal value As TypeVariableDeclaration)
            m_HandlesField = value
        End Set
    End Property

    ReadOnly Property Handlers() As Generic.List(Of AddOrRemoveHandlerStatement)
        Get
            If m_Handlers Is Nothing Then
                m_Handlers = New Generic.List(Of AddOrRemoveHandlerStatement)
            End If
            Return m_Handlers
        End Get
    End Property

    ReadOnly Property ImplementsClause() As MemberImplementsClause
        Get
            Return m_MemberImplementsClause
        End Get
    End Property

    ReadOnly Property CanRead() As Boolean
        Get
            Return Modifiers.Is(ModifierMasks.WriteOnly) = False
        End Get
    End Property

    ReadOnly Property CanWrite() As Boolean
        Get
            Return Modifiers.Is(ModifierMasks.ReadOnly) = False
        End Get
    End Property

    Public Overrides ReadOnly Property MemberDescriptor() As Mono.Cecil.MemberReference
        Get
            Return m_CecilBuilder
        End Get
    End Property

    ReadOnly Property GetDeclaration() As MethodDeclaration
        Get
            Return m_Get
        End Get
    End Property

    ReadOnly Property SetDeclaration() As MethodDeclaration
        Get
            Return m_Set
        End Get
    End Property

    Public ReadOnly Property GetMethod() As Mono.Cecil.MethodDefinition
        Get
            If m_Get IsNot Nothing Then
                Return m_Get.CecilBuilder
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public ReadOnly Property PropertyBuilder() As Mono.Cecil.PropertyDefinition
        Get
            Return m_CecilBuilder
        End Get
    End Property

    Public ReadOnly Property SetMethod() As Mono.Cecil.MethodDefinition
        Get
            If m_Set IsNot Nothing Then
                Return m_Set.CecilBuilder
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public Property Signature() As FunctionSignature
        Get
            Return m_Signature
        End Get
        Set(ByVal value As FunctionSignature)
            m_Signature = value
        End Set
    End Property

    Public Overrides Function CreateDefinition() As Boolean
        Dim result As Boolean = True

        result = MyBase.CreateDefinition AndAlso result

        Helper.Assert(m_CecilBuilder Is Nothing)
        m_CecilBuilder = New Mono.Cecil.PropertyDefinition(Name, 0, Helper.GetTypeOrTypeReference(Compiler, Me.Signature.ReturnType))
        m_CecilBuilder.Annotations.Add(Compiler, Me)
        DeclaringType.CecilType.Properties.Add(m_CecilBuilder)
        m_CecilBuilder.Name = Me.Name

        If m_Signature IsNot Nothing Then result = m_Signature.CreateDefinition AndAlso result

        If m_Get IsNot Nothing Then
            result = m_Get.CreateDefinition AndAlso result
            m_CecilBuilder.GetMethod = m_Get.CecilBuilder
        End If

        If m_Set IsNot Nothing Then
            result = m_Set.CreateDefinition AndAlso result
            m_CecilBuilder.SetMethod = m_Set.CecilBuilder
        End If

        Return result
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveTypeReferences AndAlso result
        If m_Signature IsNot Nothing Then result = m_Signature.ResolveTypeReferences AndAlso result
        If m_Get IsNot Nothing Then result = m_Get.ResolveTypeReferences AndAlso result
        If m_Set IsNot Nothing Then result = m_Set.ResolveTypeReferences AndAlso result

        m_CecilBuilder.PropertyType = Helper.GetTypeOrTypeReference(Compiler, Me.Signature.ReturnType)

        If m_MemberImplementsClause IsNot Nothing Then result = m_MemberImplementsClause.ResolveTypeReferences AndAlso result

        Return result
    End Function

    Public Function ResolveMember(ByVal Info As ResolveInfo) As Boolean Implements INonTypeMember.ResolveMember
        Dim result As Boolean = True

        result = m_Signature.ResolveCode(Info) AndAlso result

        If m_Get IsNot Nothing Then result = m_Get.ResolveMember(ResolveInfo.Default(Info.Compiler)) AndAlso result
        If m_Set IsNot Nothing Then result = m_Set.ResolveMember(ResolveInfo.Default(Info.Compiler)) AndAlso result

        If Modifiers.Is(ModifierMasks.Default) Then
            Dim tp As TypeDeclaration = Me.FindFirstParent(Of TypeDeclaration)()
            result = tp.SetDefaultAttribute(Me.Name) AndAlso result
        End If

        result = Signature.VerifyParameterNamesDoesntMatchFunctionName() AndAlso result

        Return result
    End Function

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveCode(Info) AndAlso result
        If m_Get IsNot Nothing Then result = m_Get.ResolveCode(Info) AndAlso result
        If m_Set IsNot Nothing Then result = m_Set.ResolveCode(Info) AndAlso result

        Return result
    End Function

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        If m_Get IsNot Nothing Then result = m_Get.GenerateCode(Info) AndAlso result
        If m_Set IsNot Nothing Then result = m_Set.GenerateCode(Info) AndAlso result

        result = MyBase.GenerateCode(Info) AndAlso result

        Return result
    End Function
End Class
