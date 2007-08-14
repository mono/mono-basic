' ''
' Visual Basic.Net Compiler
' Copyright (C) 2004 - 2007 Rolf Bjarne Kvinge, RKvinge@novell.com
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
    Implements IPropertyMember
    'Implements IDefinableMember, INonTypeMember

    Private m_Descriptor As New PropertyDescriptor(Me)

    Private m_Signature As FunctionSignature
    Private m_Get As MethodDeclaration
    Private m_Set As MethodDeclaration
    Private m_MemberImplementsClause As MemberImplementsClause

    Private m_Builder As PropertyBuilder
    Private m_Handlers As Generic.List(Of AddOrRemoveHandlerStatement)
    Private m_HandlesField As VariableDeclaration

    Sub New(ByVal Parent As TypeDeclaration)
        MyBase.new(Parent)
    End Sub

    Property HandlesField() As VariableDeclaration
        Get
            Return m_HandlesField
        End Get
        Set(ByVal value As VariableDeclaration)
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

    Overloads Sub Init(ByVal Attributes As Attributes, ByVal Modifiers As Modifiers, ByVal Name As String, ByVal ReturnType As TypeName)
        MyBase.Init(Attributes, Modifiers, Name)

        m_Signature = New FunctionSignature(Me)
        m_Signature.Init(Name, Nothing, New ParameterList(m_Signature), Nothing, ReturnType, Nothing)

        Dim getMethod As PropertyGetDeclaration
        Dim setMethod As PropertySetDeclaration


        getMethod = New PropertyGetDeclaration(Me)
        getMethod.Init(Attributes, Modifiers, m_Signature, Nothing, Nothing)

        setMethod = New PropertySetDeclaration(Me)
        setMethod.Init(Attributes, Modifiers, m_Signature, Nothing, Nothing, Nothing)

        m_Get = getMethod
        m_Set = setMethod
    End Sub

    Overloads Sub Init(ByVal Attributes As Attributes, ByVal Modifiers As Modifiers, ByVal Name As String, ByVal ReturnType As Type)
        MyBase.Init(Attributes, Modifiers, Name)

        m_Signature = New FunctionSignature(Me)
        m_Signature.Init(Name, Nothing, New ParameterList(m_Signature), Nothing, ReturnType, Nothing)

        Dim getMethod As PropertyGetDeclaration
        Dim setMethod As PropertySetDeclaration


        getMethod = New PropertyGetDeclaration(Me)
        getMethod.Init(Attributes, Modifiers, m_Signature, Nothing, Nothing)

        setMethod = New PropertySetDeclaration(Me)
        setMethod.Init(Attributes, Modifiers, m_Signature, Nothing, Nothing, Nothing)

        m_Get = getMethod
        m_Set = setMethod
    End Sub

    Overloads Sub Init(ByVal Attributes As Attributes, ByVal Modifiers As Modifiers, ByVal Signature As FunctionSignature, ByVal GetMethod As MethodDeclaration, ByVal SetMethod As MethodDeclaration, ByVal MemberImplementsClause As MemberImplementsClause)
        MyBase.Init(Attributes, Modifiers, Signature.Name)
        m_Signature = Signature
        m_Get = GetMethod
        m_Set = SetMethod
        m_MemberImplementsClause = MemberImplementsClause

        Helper.Assert(m_Signature IsNot Nothing)
    End Sub

    Overloads Sub Init(ByVal Attributes As Attributes, ByVal Modifiers As Modifiers, ByVal PropertySignature As FunctionSignature, ByVal MemberImplementsClause As MemberImplementsClause)
        Dim GetMethod As PropertyGetDeclaration
        Dim SetMethod As PropertySetDeclaration
        If Modifiers.Is(ModifierMasks.ReadOnly) = False Then
            SetMethod = New PropertySetDeclaration(Me)
            SetMethod.Init(Attributes, Modifiers, PropertySignature, Nothing, Nothing, Nothing)
        Else
            SetMethod = Nothing
        End If
        If Modifiers.Is(ModifierMasks.WriteOnly) = False Then
            GetMethod = New PropertyGetDeclaration(Me)
            GetMethod.Init(Attributes, Modifiers, PropertySignature, Nothing, Nothing)
        Else
            GetMethod = Nothing
        End If
        Init(Attributes, Modifiers, PropertySignature, GetMethod, SetMethod, MemberImplementsClause)
    End Sub

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

    Public Overrides ReadOnly Property MemberDescriptor() As System.Reflection.MemberInfo
        Get
            Return m_Descriptor
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

    Public ReadOnly Property GetMethod() As System.Reflection.MethodInfo 'Implements IPropertyMember.GetMethod
        Get
            If m_Get IsNot Nothing Then
                Return m_Get.Descriptor
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public ReadOnly Property PropertyBuilder() As System.Reflection.Emit.PropertyBuilder 'Implements IPropertyMember.PropertyBuilder
        Get
            Return m_Builder
        End Get
    End Property

    Public ReadOnly Property SetMethod() As System.Reflection.MethodInfo 'Implements IPropertyMember.SetMethod
        Get
            If m_Set IsNot Nothing Then
                Return m_Set.Descriptor
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public ReadOnly Property Signature() As SubSignature 'Implements IPropertyMember.Signature
        Get
            Return m_Signature
        End Get
    End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveTypeReferences AndAlso result
        If m_Signature IsNot Nothing Then result = m_Signature.ResolveTypeReferences AndAlso result
        If m_Get IsNot Nothing Then result = m_Get.ResolveTypeReferences AndAlso result
        If m_Set IsNot Nothing Then result = m_Set.ResolveTypeReferences AndAlso result

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

        Return result
    End Function

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveCode(INFO) AndAlso result
        If m_Get IsNot Nothing Then result = m_Get.ResolveCode(Info) AndAlso result
        If m_Set IsNot Nothing Then result = m_Set.ResolveCode(INFO) AndAlso result

        Return result
    End Function

    Public Function DefineMember() As Boolean Implements IDefinableMember.DefineMember
        Dim result As Boolean = True

        If m_Get IsNot Nothing Then
            result = m_Get.DefineMember() AndAlso result
        End If

        If m_Set IsNot Nothing Then
            result = m_Set.DefineMember AndAlso result
        End If

        Dim name As String
        Dim attributes As PropertyAttributes
        Dim returnType As Type
        Dim parameterTypes() As Type

        name = Me.Name
        attributes = PropertyAttributes.None
        returnType = Me.Signature.ReturnType
        parameterTypes = Me.Signature.Parameters.ToTypeArray

        Helper.SetTypeOrTypeBuilder(parameterTypes)
        returnType = Helper.GetTypeOrTypeBuilder(returnType)

        m_Builder = DeclaringType.TypeBuilder.DefineProperty(name, attributes, returnType, parameterTypes)
        Compiler.TypeManager.RegisterReflectionMember(m_Builder, Me.MemberDescriptor)

        If m_Set IsNot Nothing Then m_Builder.SetSetMethod(m_Set.MethodBuilder)
        If m_Get IsNot Nothing Then m_Builder.SetGetMethod(m_Get.MethodBuilder)

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
