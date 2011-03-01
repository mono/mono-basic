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

Public Class EventDeclaration
    Inherits MemberDeclaration
    Implements INonTypeMember, IHasImplicitMembers

    'Set during parse phase
    Private m_Identifier As Identifier
    Private m_ImplementsClause As MemberImplementsClause

    'Set during parse phase (in CreateCompilerGeneratedElements) 
    'or by the Custom Event declaration.
    ''' <summary>The add method.</summary>
    Private m_AddMethod As EventHandlerDeclaration
    ''' <summary>The remove method.</summary>
    Private m_RemoveMethod As EventHandlerDeclaration
    ''' <summary>The raise method. Is only something if it is a custom event.</summary>
    Private m_RaiseMethod As EventHandlerDeclaration

    Private m_CecilBuilder As Mono.Cecil.EventDefinition

    ReadOnly Property CecilBuilder() As Mono.Cecil.EventDefinition
        Get
            Return m_CecilBuilder
        End Get
    End Property

    Sub New(ByVal Parent As TypeDeclaration)
        MyBase.new(Parent)
    End Sub

    Shadows Sub Init(ByVal Modifiers As Modifiers, ByVal Identifier As Identifier, ByVal ImplementsClause As MemberImplementsClause)
        MyBase.Init(Modifiers, Identifier.Name)

        m_Identifier = Identifier
        m_ImplementsClause = ImplementsClause
    End Sub

    Public ReadOnly Property EventDescriptor() As Mono.Cecil.EventDefinition
        Get
            Return m_CecilBuilder
        End Get
    End Property

    Public Property EventType() As Mono.Cecil.TypeReference
        Get
            Return m_CecilBuilder.EventType
        End Get
        Set(ByVal value As Mono.Cecil.TypeReference)
            CecilBuilder.EventType = Helper.GetTypeOrTypeReference(Compiler, value)
        End Set
    End Property

    ReadOnly Property AddDefinition() As Mono.Cecil.MethodDefinition
        Get
            If m_AddMethod Is Nothing Then
                Return Nothing
            Else
                Return m_AddMethod.CecilBuilder
            End If
        End Get
    End Property

    ReadOnly Property RemoveDefinition() As Mono.Cecil.MethodDefinition
        Get
            If m_RemoveMethod Is Nothing Then
                Return Nothing
            Else
                Return m_RemoveMethod.CecilBuilder
            End If
        End Get
    End Property

    ReadOnly Property RaiseDefinition() As Mono.Cecil.MethodDefinition
        Get
            If m_RaiseMethod Is Nothing Then
                Return Nothing
            Else
                Return m_RaiseMethod.CecilBuilder
            End If
        End Get
    End Property

    Property AddMethod() As EventHandlerDeclaration
        Get
            Return m_AddMethod
        End Get
        Set(ByVal value As EventHandlerDeclaration)
            Helper.Assert(m_AddMethod Is Nothing)
            m_AddMethod = value
            If m_CecilBuilder IsNot Nothing Then m_CecilBuilder.AddMethod = value.CecilBuilder
        End Set
    End Property

    Property RemoveMethod() As EventHandlerDeclaration
        Get
            Return m_RemoveMethod
        End Get
        Set(ByVal value As EventHandlerDeclaration)
            Helper.Assert(m_RemoveMethod Is Nothing)
            m_RemoveMethod = value
            If m_CecilBuilder IsNot Nothing Then m_CecilBuilder.RemoveMethod = value.CecilBuilder
        End Set
    End Property

    Property RaiseMethod() As EventHandlerDeclaration
        Get
            Return m_RaiseMethod
        End Get
        Set(ByVal value As EventHandlerDeclaration)
            Helper.Assert(m_RaiseMethod Is Nothing)
            m_RaiseMethod = value
            If m_CecilBuilder IsNot Nothing Then m_CecilBuilder.InvokeMethod = value.CecilBuilder
        End Set
    End Property

    Public Overrides ReadOnly Property MemberDescriptor() As Mono.Cecil.MemberReference
        Get
            Return m_CecilBuilder
        End Get
    End Property

    ReadOnly Property Identifier() As Identifier
        Get
            Return m_Identifier
        End Get
    End Property

    ReadOnly Property ImplementsClause() As MemberImplementsClause
        Get
            Return m_ImplementsClause
        End Get
    End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        If m_ImplementsClause IsNot Nothing Then result = m_ImplementsClause.ResolveTypeReferences AndAlso result

        result = MyBase.ResolveTypeReferences AndAlso result
        If m_AddMethod IsNot Nothing Then result = m_AddMethod.ResolveTypeReferences AndAlso result
        If m_RemoveMethod IsNot Nothing Then result = m_RemoveMethod.ResolveTypeReferences AndAlso result
        If m_RaiseMethod IsNot Nothing Then result = m_RaiseMethod.ResolveTypeReferences AndAlso result

        Helper.Assert(EventType IsNot Nothing)

        Return result
    End Function

    Private Function CreateImplicitMembers() As Boolean Implements IHasImplicitMembers.CreateImplicitMembers
        Dim result As Boolean = True

        result = ResolveTypeReferences() AndAlso result

        Return result
    End Function

    Public Function ResolveMember(ByVal Info As ResolveInfo) As Boolean Implements INonTypeMember.ResolveMember
        Dim result As Boolean = True

        If m_ImplementsClause IsNot Nothing Then result = m_ImplementsClause.ResolveCode(Info) AndAlso result

        Helper.Assert(EventType IsNot Nothing)

        Return result
    End Function

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveCode(Info) AndAlso result

        Return result
    End Function

    Function DefineOverrides() As Boolean
        Dim result As Boolean = True

        If m_ImplementsClause IsNot Nothing Then
            result = m_ImplementsClause.DefineImplements(Me)
        End If

        Return result
    End Function


    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Helper.Assert(m_AddMethod.CecilBuilder IsNot Nothing)
        Helper.Assert(m_RemoveMethod.CecilBuilder IsNot Nothing)

        'm_Builder.SetAddOnMethod(m_AddMethod.MethodBuilder)
        'm_Builder.SetRemoveOnMethod(m_RemoveMethod.MethodBuilder)
        'If m_RaiseMethod IsNot Nothing Then m_Builder.SetRaiseMethod(m_RaiseMethod.MethodBuilder)

        result = DefineOverrides() AndAlso result
        result = MyBase.GenerateCode(Info) AndAlso result

        m_CecilBuilder.AddMethod = m_AddMethod.CecilBuilder
        m_CecilBuilder.RemoveMethod = m_RemoveMethod.CecilBuilder
        If m_RaiseMethod IsNot Nothing Then m_CecilBuilder.InvokeMethod = m_RaiseMethod.CecilBuilder

        Return result
    End Function

    Public Overrides Function CreateDefinition() As Boolean
        Dim result As Boolean = True

        Helper.Assert(m_CecilBuilder Is Nothing)
        result = MyBase.CreateDefinition AndAlso result

        m_CecilBuilder = New Mono.Cecil.EventDefinition(Name, 0, Nothing)
        m_CecilBuilder.Annotations.Add(Compiler, Me)
        FindFirstParent(Of TypeDeclaration).CecilType.Events.Add(m_CecilBuilder)
        m_CecilBuilder.Name = Name

        If m_AddMethod IsNot Nothing Then
            result = m_AddMethod.CreateDefinition AndAlso result
            DeclaringType.Members.Add(m_AddMethod)
            m_CecilBuilder.AddMethod = m_AddMethod.CecilBuilder
        End If

        If m_RemoveMethod IsNot Nothing Then
            result = m_RemoveMethod.CreateDefinition AndAlso result
            DeclaringType.Members.Add(m_RemoveMethod)
            m_CecilBuilder.RemoveMethod = m_RemoveMethod.CecilBuilder
        End If

        If m_RaiseMethod IsNot Nothing Then
            result = m_RaiseMethod.CreateDefinition AndAlso result
            DeclaringType.Members.Add(m_RaiseMethod)
            m_CecilBuilder.InvokeMethod = m_RaiseMethod.CecilBuilder
        End If

        Return result
    End Function
End Class
