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
#If DEBUG Then
#Const EXTENDEDDEBUG = 0
#End If
''' <summary>
''' TypeDeclaration Hierarchy:
''' 
''' TypeDeclaration
'''   + DelegateDeclaration
'''   + EnumDeclaration
'''   + ModuleDeclaration
'''   - GenericTypeDeclaration
'''       + InterfaceDeclaration
'''       - PartialTypeDeclaration
'''           + ClassDeclaration
'''           + StructureDeclaration
'''         
''' </summary>
''' <remarks></remarks>
Public MustInherit Class TypeDeclaration
    Inherits MemberDeclaration
    Implements IType

    'Private m_TypeDescriptor As TypeDescriptor

    'Information collected during parse phase.
    Private m_Members As New MemberDeclarations(Me)
    Private m_Namespace As String
    Private m_Name As Identifier

    Private m_DefaultInstanceConstructor As ConstructorDeclaration
    Private m_DefaultSharedConstructor As ConstructorDeclaration
    Private m_StaticVariables As Generic.List(Of LocalVariableDeclaration)
    Private m_Serializable As Boolean
    Private m_AddedCompareTextAttribute As Boolean

    'Information collected during define phase.
    Private m_CecilType As Mono.Cecil.TypeDefinition

    Private m_FullName As String

    Private m_AddHandlers As New Generic.List(Of AddOrRemoveHandlerStatement)
    Private m_MyGroupField As TypeVariableDeclaration

    Property MyGroupField() As TypeVariableDeclaration
        Get
            Return m_MyGroupField
        End Get
        Set(ByVal value As TypeVariableDeclaration)
            m_MyGroupField = value
        End Set
    End Property

    Property Serializable() As Boolean
        Get
            Return m_Serializable
        End Get
        Set(ByVal value As Boolean)
            If m_CecilType IsNot Nothing Then m_CecilType.IsSerializable = value
            m_Serializable = value
        End Set
    End Property

    ReadOnly Property DescriptiveType() As String
        Get
            If TypeOf Me Is ClassDeclaration Then
                Return "class"
            ElseIf TypeOf Me Is ModuleDeclaration Then
                Return "module"
            ElseIf TypeOf Me Is EnumDeclaration Then
                Return "enum"
            ElseIf TypeOf Me Is StructureDeclaration Then
                Return "structure"
            ElseIf TypeOf Me Is DelegateDeclaration Then
                Return "delegate"
            ElseIf TypeOf Me Is InterfaceDeclaration Then
                Return "interface"
            Else
                Return "type"
            End If
        End Get
    End Property

    ReadOnly Property AddHandlers() As Generic.List(Of AddOrRemoveHandlerStatement)
        Get
            Return m_AddHandlers
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject, ByVal [Namespace] As String, ByVal Name As Identifier)
        MyBase.New(Parent)

        m_Namespace = [Namespace]
        m_Name = Name
        MyBase.Name = Name.Name

        Helper.Assert(m_Namespace IsNot Nothing)
        Helper.Assert(m_Name IsNot Nothing)
    End Sub

    Public Overrides Function CreateDefinition() As Boolean
        Dim result As Boolean = True

        result = MyBase.CreateDefinition AndAlso result

        If m_CecilType IsNot Nothing Then
            'This may happen with partial types
            Return result
        End If

        If Me.IsNestedType Then
            m_CecilType = New Mono.Cecil.TypeDefinition(Nothing, Me.Name, 0)
        Else
            m_CecilType = New Mono.Cecil.TypeDefinition(Me.Namespace, Me.Name, 0)
        End If
        m_CecilType.Annotations.Add(Compiler, Me)
        m_CecilType.Name = Name
        m_CecilType.Attributes = Helper.getTypeAttributeScopeFromScope(Modifiers, IsNestedType)

        If IsNestedType Then
            DeclaringType.CecilType.NestedTypes.Add(m_CecilType)
            m_CecilType.DeclaringType = DeclaringType.CecilType
        Else
            Compiler.ModuleBuilderCecil.Types.Add(m_CecilType)
        End If

        'create definitions for all members
        For i As Integer = 0 To Members.Count - 1
            result = Members(i).CreateDefinition() AndAlso result
        Next

        Return result
    End Function

    Public Property BeforeFieldInit() As Boolean
        Get
            Return m_CecilType.IsBeforeFieldInit
        End Get
        Set(ByVal value As Boolean)
            m_CecilType.IsBeforeFieldInit = value
        End Set
    End Property

    Protected Friend Property DefaultInstanceConstructor() As ConstructorDeclaration
        Get
            Return m_DefaultInstanceConstructor
        End Get
        Set(ByVal value As ConstructorDeclaration)
            m_DefaultInstanceConstructor = value
        End Set
    End Property

    Protected Property DefaultSharedConstructor() As ConstructorDeclaration
        Get
            Return m_DefaultSharedConstructor
        End Get
        Set(ByVal value As ConstructorDeclaration)
            m_DefaultSharedConstructor = value
        End Set
    End Property

    Protected Sub FindDefaultConstructors()
        For i As Integer = 0 To Me.Members.Count - 1
            Dim member As IMember = Me.Members(i)
            Dim ctor As ConstructorDeclaration = TryCast(member, ConstructorDeclaration)

            If ctor Is Nothing Then Continue For

            Dim isdefault As Boolean
            isdefault = False
            If ctor.GetParameters.Length = 0 Then
                isdefault = True
            Else
                isdefault = ctor.GetParameters()(0).IsOptional
            End If
            If isdefault Then
                If ctor.IsShared Then
                    If m_DefaultSharedConstructor Is Nothing Then m_DefaultSharedConstructor = ctor
                Else
                    If m_DefaultInstanceConstructor Is Nothing Then m_DefaultInstanceConstructor = ctor
                End If
            End If
        Next
    End Sub

    ReadOnly Property [Namespace]() As String Implements IType.Namespace
        Get
            Helper.Assert(m_Namespace IsNot Nothing)
            Return m_Namespace
        End Get
    End Property

    Sub AddInterface(ByVal Type As Mono.Cecil.TypeReference)
        m_CecilType.Interfaces.Add(Helper.GetTypeOrTypeReference(Compiler, Type))
    End Sub

    ReadOnly Property Identifier() As Identifier
        Get
            Return m_Name
        End Get
    End Property

    Public Overrides ReadOnly Property IsShared() As Boolean
        Get
            Return TypeOf Me Is ModuleDeclaration
        End Get
    End Property

    Public Overrides ReadOnly Property MemberDescriptor() As Mono.Cecil.MemberReference
        Get
            Return m_CecilType
        End Get
    End Property

    Public Overrides ReadOnly Property FullName() As String
        Get
            If m_FullName Is Nothing Then
                If Me.IsNestedType Then
                    m_FullName = DeclaringType.FullName & "+" & Me.Name
                Else
                    If m_Namespace <> "" Then
                        m_FullName = m_Namespace & "." & Me.Name
                    Else
                        m_FullName = Me.Name
                    End If
                End If
            End If
            Return m_FullName
        End Get
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BaseType() As Mono.Cecil.TypeReference Implements IType.BaseType
        Get
            If m_CecilType Is Nothing Then
                Return Nothing
            End If
            Return m_CecilType.BaseType
        End Get
        Set(ByVal value As Mono.Cecil.TypeReference)
            m_CecilType.BaseType = Helper.GetTypeOrTypeReference(Compiler, value)
        End Set
    End Property

    Public ReadOnly Property Members() As MemberDeclarations Implements IType.Members
        Get
            Return m_Members
        End Get
    End Property

    Public Overridable ReadOnly Property CecilType() As Mono.Cecil.TypeDefinition Implements IType.CecilType
        Get
            Return m_CecilType
        End Get
    End Property

    ReadOnly Property IsNestedType() As Boolean Implements IType.IsNestedType
        Get
            Dim result As Boolean
            result = DeclaringType IsNot Nothing
            Helper.Assert(result = (Me.FindFirstParent(Of IType)() IsNot Nothing))
            Return result
        End Get
    End Property

    Public Overrides Function ResolveBaseType() As Boolean
        Dim result As Boolean = True

        For i As Integer = 0 To Me.Members.Count - 1
            Dim t As TypeDeclaration = TryCast(Members(i), TypeDeclaration)
            If t Is Nothing Then Continue For
            result = t.ResolveBaseType AndAlso result
        Next

        Return result
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveTypeReferences AndAlso result

        If File IsNot Nothing AndAlso File.IsOptionCompareText AndAlso m_AddedCompareTextAttribute = False Then
            m_AddedCompareTextAttribute = True
            AddCustomAttribute(New Attribute(Me, Compiler.TypeCache.MS_VB_CS_OptionTextAttribute))
        End If

        m_StaticVariables = New Generic.List(Of LocalVariableDeclaration)
        For Each method As MethodDeclaration In m_Members.GetSpecificMembers(Of MethodDeclaration)()
            If method.Code IsNot Nothing Then method.Code.FindStaticVariables(m_StaticVariables)
        Next
        For Each prop As PropertyDeclaration In m_Members.GetSpecificMembers(Of PropertyDeclaration)()
            If prop.GetDeclaration IsNot Nothing AndAlso prop.GetDeclaration.Code IsNot Nothing Then prop.GetDeclaration.Code.FindStaticVariables(m_StaticVariables)
            If prop.SetDeclaration IsNot Nothing AndAlso prop.SetDeclaration.Code IsNot Nothing Then prop.SetDeclaration.Code.FindStaticVariables(m_StaticVariables)
        Next

        'Create nested generic type parameters
        If Me.IsNestedType Then
            Dim parentType As TypeDeclaration = DeclaringType
            Dim parentGenericType As GenericTypeDeclaration
            Dim stack As New Generic.Stack(Of TypeParameter)
            Dim insertAt As Integer = 0

            Do
                parentGenericType = TryCast(parentType, GenericTypeDeclaration)
                If parentGenericType IsNot Nothing AndAlso parentGenericType.TypeParameters IsNot Nothing Then
                    For i As Integer = parentGenericType.TypeParameters.Parameters.Count - 1 To 0 Step -1
                        stack.Push(parentGenericType.TypeParameters.Parameters(i))
                    Next
                End If
                parentType = parentType.DeclaringType
            Loop While parentType IsNot Nothing

            Dim typeParameter As TypeParameter

            Do While stack.Count > 0
                typeParameter = stack.Pop
                CecilType.GenericParameters.Insert(insertAt, typeParameter.Clone(typeParameter.CecilBuilder, CecilType, CecilType.GenericParameters.Count))
                insertAt += 1
            Loop

            Dim enumDecl As EnumDeclaration = TryCast(Me, EnumDeclaration)
            If enumDecl IsNot Nothing AndAlso CecilType.GenericParameters.Count > 0 Then
                Dim enumFieldType As New Mono.Cecil.GenericInstanceType(CecilType)
                For i As Integer = 0 To CecilType.GenericParameters.Count - 1
                    enumFieldType.GenericArguments.Add(CecilType.GenericParameters(i))
                Next
                For i As Integer = 0 To enumDecl.Members.Count - 1
                    Dim enumField As EnumMemberDeclaration = TryCast(enumDecl.Members(i), EnumMemberDeclaration)
                    If enumField Is Nothing Then Continue For
                    enumField.FieldBuilder.FieldType = enumFieldType
                Next
            End If
        End If

        Return result
    End Function

    Public Overridable Function DefineTypeHierarchy() As Boolean
        Dim result As Boolean = True

        If BaseType Is Nothing Then
            If Me.IsInterface = False Then
                m_CecilType.BaseType = Helper.GetTypeOrTypeReference(Compiler, Compiler.TypeCache.System_Void)
            End If
        End If

        Return result
    End Function

    Public Function SetDefaultAttribute(ByVal Name As String) As Boolean
        Dim result As Boolean = True

        If CustomAttributes IsNot Nothing Then
            For Each att As Attribute In CustomAttributes
                result = att.ResolveCode(ResolveInfo.Default(Compiler)) AndAlso result
                If result = False Then Return result
                If Helper.CompareType(att.AttributeType, Compiler.TypeCache.System_Reflection_DefaultMemberAttribute) Then
                    Dim tmpName As String
                    tmpName = TryCast(att.GetArgument(0), String)
                    If tmpName IsNot Nothing AndAlso Helper.CompareNameOrdinal(Name, tmpName) = False Then
                        Compiler.Report.ShowMessage(Messages.VBNC32304, Location, Me.FullName, tmpName, Name)
                        Return False
                    End If
                    Return True
                End If
            Next
        End If

        'Helper.NotImplementedYet("Check that the property is indexed.")
        Dim attrib As Attribute
        attrib = New Attribute(Me, Compiler.TypeCache.System_Reflection_DefaultMemberAttribute, Name)
        result = attrib.ResolveCode(ResolveInfo.Default(Compiler)) AndAlso result
        AddCustomAttribute(attrib)
        Return result
    End Function

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        Compiler.Report.Trace("{0}: TypeDeclaration.ResolveCode: {1}", Me.Location, Me.FullName)

        result = MyBase.ResolveCode(Info) AndAlso result
        Compiler.VerifyConsistency(result, Location)
        result = m_Members.ResolveCode(Info) AndAlso result
        'vbnc.Helper.Assert(result = (Compiler.Report.Errors = 0))

        Return result
    End Function

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        result = MyBase.GenerateCode(Info) AndAlso result

        Return result
    End Function

    ReadOnly Property StaticVariables() As Generic.List(Of LocalVariableDeclaration)
        Get
            Return m_StaticVariables
        End Get
    End Property

    Property TypeAttributes() As Mono.Cecil.TypeAttributes Implements IType.TypeAttributes
        Get
            Return m_CecilType.Attributes
        End Get
        Set(ByVal value As Mono.Cecil.TypeAttributes)
            m_CecilType.Attributes = value
            m_CecilType.IsSerializable = m_Serializable
        End Set
    End Property

    ReadOnly Property IsInterface() As Boolean
        Get
            Return TypeOf Me Is InterfaceDeclaration
        End Get
    End Property

    ReadOnly Property IsClass() As Boolean
        Get
            Return TypeOf Me Is ClassDeclaration
        End Get
    End Property

    ReadOnly Property IsDelegate() As Boolean
        Get
            Return TypeOf Me Is DelegateDeclaration
        End Get
    End Property

    ReadOnly Property IsValueType() As Boolean
        Get
            Return TypeOf Me Is StructureDeclaration
        End Get
    End Property

    ReadOnly Property IsEnum() As Boolean
        Get
            Return TypeOf Me Is EnumDeclaration
        End Get
    End Property

    ReadOnly Property IsModule() As Boolean
        Get
            Return TypeOf Me Is ModuleDeclaration
        End Get
    End Property

    ReadOnly Property HasInstanceConstructors() As Boolean
        Get
            For i As Integer = 0 To Me.Members.Count - 1
                Dim cd As ConstructorDeclaration = TryCast(Me.Members(i), ConstructorDeclaration)
                If cd Is Nothing Then Continue For
                If cd.IsShared = False Then Return True
            Next
            Return False
        End Get
    End Property

    ReadOnly Property HasSharedConstantFields() As Boolean
        Get
            Return Me.Members.HasSpecificMember(Of ConstantDeclaration)()
        End Get
    End Property

    ReadOnly Property HasSharedFieldsWithInitializers() As Boolean
        Get
            For i As Integer = 0 To Me.Members.Count - 1
                Dim item As VariableDeclaration = TryCast(Me.Members(i), VariableDeclaration)
                If item IsNot Nothing AndAlso item.IsShared AndAlso item.HasInitializer Then Return True

                Dim cd As ConstantDeclaration = TryCast(Me.Members(i), ConstantDeclaration)
                If cd IsNot Nothing AndAlso cd.RequiresSharedInitialization Then Return True
            Next

            If m_StaticVariables IsNot Nothing Then
                For i As Integer = 0 To m_StaticVariables.Count - 1
                    Dim item As VariableDeclaration = m_StaticVariables(i)
                    If item.DeclaringMethod.IsShared AndAlso item.HasInitializer Then Return True
                Next
            End If

            Return False
        End Get
    End Property

    Public Overridable Function CreateImplicitInstanceConstructors() As Boolean
        Return True
    End Function

    Public Overridable Function CreateImplicitSharedConstructors() As Boolean
        Dim result As Boolean = True

        If Not NeedsSharedConstructor Then Return result
        If DefaultSharedConstructor IsNot Nothing Then Return result

        Me.FindDefaultConstructors()

        If DefaultSharedConstructor IsNot Nothing Then Return result

        DefaultSharedConstructor = New ConstructorDeclaration(Me)
        DefaultSharedConstructor.Init(New Modifiers(ModifierMasks.Shared), New SubSignature(DefaultSharedConstructor, ConstructorDeclaration.SharedConstructorName, New ParameterList(DefaultSharedConstructor)), New CodeBlock(DefaultSharedConstructor))
        Members.Add(DefaultSharedConstructor)
        result = DefaultSharedConstructor.CreateDefinition AndAlso result
        BeforeFieldInit = True

        Return result
    End Function

    Protected Overridable ReadOnly Property NeedsSharedConstructor As Boolean
        Get
            Return False
        End Get
    End Property
End Class
