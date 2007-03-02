' 
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

    Private m_TypeDescriptor As TypeDescriptor

    'Information collected during parse phase.
    Private m_Members As MemberDeclarations
    Private m_Namespace As String
    Private m_Name As IdentifierToken

    'Information collected during resolve phase.
    Private m_BaseType As Type
    Private m_ImplementedTypes As Type()

    Private m_DefaultInstanceConstructor As ConstructorDeclaration
    Private m_DefaultSharedConstructor As ConstructorDeclaration
    Private m_StaticVariables As Generic.List(Of VariableDeclaration)
    Private m_BeforeFieldInit As Boolean

    'Information collected during define phase.
    Private m_TypeBuilder As TypeBuilder
    'Another hack for another bug in the ms runtime: you cannot create an attribute when the attribute's constructor has a enum parameter and the enum parameter is defined with a typebuilder, it only works if the enum parameter's type is defined with an enumbuilder.
    Private m_EnumBuilder As EnumBuilder

    Private m_FinalType As Type

    Private m_FullName As String

    Private m_AddHandlers As New Generic.List(Of AddOrRemoveHandlerStatement)

    ReadOnly Property AddHandlers() As Generic.List(Of AddOrRemoveHandlerStatement)
        Get
            Return m_AddHandlers
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject, ByVal [Namespace] As String)
        MyBase.New(Parent)
        m_TypeDescriptor = New TypeDescriptor(Me)

        m_Namespace = [Namespace]

        Helper.Assert(m_Namespace IsNot Nothing)
    End Sub

    Shadows Sub Init(ByVal CustomAttributes As Attributes, ByVal Modifiers As Modifiers, ByVal Members As MemberDeclarations, ByVal Name As IdentifierToken, ByVal TypeArgumentCount As Integer)
        MyBase.Init(CustomAttributes, Modifiers, Helper.CreateGenericTypename(Name.Name, TypeArgumentCount))

        m_Members = Members
        m_Name = Name

        Helper.Assert(DeclaringType IsNot Nothing OrElse TypeOf Me.Parent Is AssemblyDeclaration)
        Helper.Assert(m_Members IsNot Nothing)
        Helper.Assert(m_Namespace IsNot Nothing)
        Helper.Assert(m_Name IsNot Nothing)
    End Sub

    Protected Property BeforeFieldInit() As Boolean
        Get
            Return m_BeforeFieldInit
        End Get
        Set(ByVal value As Boolean)
            m_BeforeFieldInit = value
        End Set
    End Property

    Protected Property DefaultInstanceConstructor() As ConstructorDeclaration
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
        Dim ctors As Generic.List(Of ConstructorDeclaration)
        ctors = Me.Members.GetSpecificMembers(Of ConstructorDeclaration)()
        For Each ctor As ConstructorDeclaration In ctors
            Dim isdefault As Boolean
            isdefault = False
            If ctor.GetParameters.Length = 0 Then
                isdefault = True
            Else
                isdefault = ctor.GetParameters()(0).IsOptional
            End If
            If isdefault Then
                If ctor.IsShared Then
                    Helper.Assert(m_DefaultSharedConstructor Is Nothing OrElse m_DefaultSharedConstructor Is ctor)
                    m_DefaultSharedConstructor = ctor
                Else
                    Helper.Assert(m_DefaultInstanceConstructor Is Nothing OrElse m_DefaultInstanceConstructor Is ctor)
                    m_DefaultInstanceConstructor = ctor
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

    Property ImplementedTypes() As Type()
        Get
            Return m_ImplementedTypes
        End Get
        Protected Set(ByVal value As Type())
            m_ImplementedTypes = value
        End Set
    End Property

    ReadOnly Property Identifier() As IdentifierToken
        Get
            Return m_Name
        End Get
    End Property

    Public ReadOnly Property Created() As Boolean Implements ICreatableType.Created
        Get
            Helper.Assert(m_TypeBuilder IsNot Nothing)
            Return m_TypeBuilder.IsCreated
        End Get
    End Property

    Public Overrides ReadOnly Property IsShared() As Boolean
        Get
            Return TypeOf Me Is ModuleDeclaration
        End Get
    End Property

    Public Overrides ReadOnly Property MemberDescriptor() As System.Reflection.MemberInfo
        Get
            Return m_TypeDescriptor
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

    Overridable Function DefineTypeParameters() As Boolean
        Return True
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BaseType() As System.Type
        Get
            Return m_BaseType
        End Get
        Protected Set(ByVal value As System.Type)
            m_BaseType = value
            Helper.Assert(m_BaseType IsNot Nothing)
        End Set
    End Property

    Private ReadOnly Property BaseType2() As System.Type Implements IType.BaseType
        Get
            Return BaseType
        End Get
    End Property

    Public Property Members() As MemberDeclarations
        Get
            Return m_Members
        End Get
        Protected Set(ByVal value As MemberDeclarations)
            Helper.Assert(TypeOf Me Is PartialTypeDeclaration AndAlso DirectCast(Me, PartialTypeDeclaration).IsPartial)
            m_Members = value
        End Set
    End Property

    Private ReadOnly Property Members2() As MemberDeclarations Implements IType.Members
        Get
            Return m_Members
        End Get
    End Property

    Property EnumBuilder() As EnumBuilder
        Get
            Return m_EnumBuilder
        End Get
        Protected Set(ByVal value As EnumBuilder)
            m_EnumBuilder = value
        End Set
    End Property

    Public Overridable Property TypeBuilder() As System.Reflection.Emit.TypeBuilder
        Get
            Return m_TypeBuilder
        End Get
        Protected Set(ByVal value As System.Reflection.Emit.TypeBuilder)
            Compiler.TypeManager.RegisterReflectionType(value, Me.TypeDescriptor)
            m_TypeBuilder = value
        End Set
    End Property

    Private ReadOnly Property TypeBuilder2() As System.Reflection.Emit.TypeBuilder Implements IType.TypeBuilder
        Get
            Return TypeBuilder
        End Get
    End Property

    Public ReadOnly Property TypeDescriptor() As TypeDescriptor Implements IType.TypeDescriptor
        Get
            Return m_TypeDescriptor
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

    Public Overridable Function ResolveType() As Boolean Implements IType.ResolveType
        Dim result As Boolean = True

        Helper.Assert(m_BaseType IsNot Nothing)

        Return result
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveTypeReferences AndAlso result

        If Location.File IsNot Nothing AndAlso Location.File.IsOptionCompareText Then
            Dim textAttribute As Attribute
            textAttribute = New Attribute(Me, Compiler.TypeCache.MS_VB_CS_OptionTextAttribute)
            CustomAttributes.Add(textAttribute)
        End If

        m_StaticVariables = New Generic.List(Of VariableDeclaration)
        For Each method As MethodDeclaration In m_Members.GetSpecificMembers(Of MethodDeclaration)()
            If method.Code IsNot Nothing Then method.Code.FindStaticVariables(m_StaticVariables)
        Next
        For Each prop As PropertyDeclaration In m_Members.GetSpecificMembers(Of PropertyDeclaration)()
            If prop.GetDeclaration IsNot Nothing AndAlso prop.GetDeclaration.Code IsNot Nothing Then prop.GetDeclaration.Code.FindStaticVariables(m_StaticVariables)
            If prop.SetDeclaration IsNot Nothing AndAlso prop.SetDeclaration.Code IsNot Nothing Then prop.SetDeclaration.Code.FindStaticVariables(m_StaticVariables)
        Next

        Return result
    End Function

    Overridable Function DefineType() As Boolean Implements IDefinableType.DefineType
        Dim result As Boolean = True

        Helper.Assert(m_BaseType IsNot Nothing OrElse Me.TypeDescriptor.IsInterface)
        Helper.Assert(m_Name IsNot Nothing)

        'Create the type builder.
        Dim Attr As TypeAttributes
        Attr = Me.TypeAttributes
        If m_BeforeFieldInit Then
            Attr = Attr Or Reflection.TypeAttributes.BeforeFieldInit
        End If
        If IsNestedType Then
            Helper.Assert(DeclaringType IsNot Nothing)
            Helper.Assert(DeclaringType.TypeBuilder IsNot Nothing)
#If EXTENDEDDEBUG Then
            Compiler.Report.WriteLine("Defining nested type: " & Name & " with attributes: " & Attr.ToString & " = " & CInt(Attr))
#End If
            m_TypeBuilder = DeclaringType.TypeBuilder.DefineNestedType(Name, Attr)
        Else
#If EXTENDEDDEBUG Then
            Compiler.Report.WriteLine("Defining type: " & Name & " with attributes: " & Attr.ToString & " = " & CInt(Attr))
#End If
            m_TypeBuilder = Compiler.ModuleBuilder.DefineType(FullName, Attr)
        End If

        Compiler.TypeManager.RegisterReflectionType(m_TypeBuilder, Me.TypeDescriptor)

        Helper.Assert(m_TypeBuilder IsNot Nothing)

        Return result
    End Function

    Public Overridable Function DefineTypeHierarchy() As Boolean Implements IDefinableType.DefineTypeHierarchy
        Dim result As Boolean = True

        If m_TypeBuilder IsNot Nothing Then
            m_BaseType = Helper.GetTypeOrTypeBuilder(m_BaseType)

#If EXTENDEDDEBUG Then
            If Me.BaseType Is Nothing Then
                Compiler.Report.WriteLine("Setting parent of " & FullName & " to Nothing")
            Else
                Compiler.Report.WriteLine("Setting parent of " & FullName & " to " & Me.BaseType.FullName)
            End If
#End If
            m_TypeBuilder.SetParent(Me.BaseType)

            If m_ImplementedTypes IsNot Nothing Then
                For Each Type As Type In m_ImplementedTypes
                    Type = Helper.GetTypeOrTypeBuilder(Type)
                    Helper.Assert(Type.IsInterface)
#If EXTENDEDDEBUG Then
                    Compiler.Report.WriteLine("Setting implement," & FullName & " now implements " & Type.FullName)
#End If
                    m_TypeBuilder.AddInterfaceImplementation(Type)
                Next
            End If
        Else
            Helper.Assert(m_EnumBuilder IsNot Nothing)
        End If

        Return result
    End Function

    Public Function SetDefaultAttribute(ByVal Name As String) As Boolean
        Dim result As Boolean = True
        For Each att As Attribute In CustomAttributes
            If Helper.CompareType(att.AttributeType, Compiler.TypeCache.DefaultMemberAttribute) Then
                Dim tmpName As String
                tmpName = TryCast(att.GetArgument(0), String)
                If tmpName IsNot Nothing AndAlso NameResolution.CompareNameOrdinal(Name, tmpName) = False Then
                    Compiler.Report.ShowMessage(Messages.VBNC32304, Location, Me.FullName, tmpName, Name)
                    Return False
                End If
                Return True
            End If
        Next

        Helper.NotImplementedYet("Check that the property is indexed.")
        Dim attrib As Attribute
        attrib = New Attribute(Me, Compiler.TypeCache.DefaultMemberAttribute, Name)
        result = attrib.ResolveCode(ResolveInfo.Default(Compiler)) AndAlso result
        CustomAttributes.Add(attrib)
        Return result
    End Function

    ''' <summary>
    ''' Sets the entry point / Main function of the assembly
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetMainAttribute()

        'Find the main function
        Dim lstMethods As New Generic.List(Of SubDeclaration)
        If Compiler.CommandLine.Target = vbnc.CommandLine.Targets.Console Then
            If Compiler.CommandLine.Main <> "" Then
                If NameResolution.CompareName(Me.FullName, Compiler.CommandLine.Main) = False Then
                    Return
                End If
            End If

            For Each m As SubDeclaration In Members.GetSpecificMembers(Of SubDeclaration)()
                If Compiler.IsMainMethod(m.Descriptor) Then lstMethods.Add(m)
            Next
            'Set the entry point of the assembly
            If lstMethods.Count > 1 Then
                Return
            ElseIf lstMethods.Count = 0 Then
                Return
            Else
                lstMethods(0).CustomAttributes.Add(New Attribute(lstMethods(0), Compiler.TypeCache.STAThreadAttribute, New Object() {}))
            End If
        ElseIf Compiler.CommandLine.Target = vbnc.CommandLine.Targets.Winexe Then
            'Find a class inheriting from System.Forms.Form.
            Throw New NotImplementedException
        End If
    End Sub

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        Me.CheckCodeNotResolved()

        SetMainAttribute()

        result = MyBase.ResolveCode(Info) AndAlso result
        result = m_Members.ResolveCode(Info) AndAlso result
        vbnc.Helper.Assert(result = (Compiler.Report.Errors = 0))

        Return result
    End Function

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        For Each var As VariableDeclaration In m_StaticVariables
            result = var.DefineStaticMember AndAlso result
        Next

        result = MyBase.GenerateCode(Info) AndAlso result

        Return result
    End Function

    ''' <summary>
    ''' Creates the type if it has not already been created.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable Function CreateType() As Boolean Implements ICreatableType.CreateType
        If m_FinalType IsNot Nothing Then Return True

        'HACK: !!!
        If TypeOf Me Is ModuleDeclaration Then
            If Helper.IsOnMS Then
                'This is necessary, otherwise the typebuilder will create a default constructor for the module.
                'http://lab.msdn.microsoft.com/productfeedback/viewfeedback.aspx?feedbackid=9bde9cf4-61e3-4b02-9fb5-894359cb7849
                GetType(TypeBuilder).GetField("m_constructorCount", BindingFlags.NonPublic Or BindingFlags.Instance).SetValue(m_TypeBuilder, -1)
            End If
        End If
        'HACK!!!

#If EXTENDEDDEBUG Then
        Report.WriteLine(vbnc.Report.ReportLevels.Debug, "Creating: " & Me.FullName)
#End If
        If m_TypeBuilder IsNot Nothing Then
            m_FinalType = m_TypeBuilder.CreateType()
        ElseIf m_EnumBuilder IsNot Nothing Then
            m_FinalType = m_EnumBuilder.CreateType()
        Else
            Throw New InternalException(Me)
        End If
#If EXTENDEDDEBUG Then
        Report.WriteLine(vbnc.Report.ReportLevels.Debug, "Created:  " & Me.FullName)
#End If
        Return True
    End Function

    ReadOnly Property StaticVariables() As Generic.List(Of VariableDeclaration)
        Get
            Return m_StaticVariables
        End Get
    End Property

    MustOverride ReadOnly Property TypeAttributes() As System.Reflection.TypeAttributes Implements IType.TypeAttributes

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
            Dim ctors As Generic.List(Of ConstructorDeclaration)
            ctors = Me.Members.GetSpecificMembers(Of ConstructorDeclaration)()
            For Each item As ConstructorDeclaration In ctors
                If item.IsShared = False Then Return True
            Next
            Return False
        End Get
    End Property

    ReadOnly Property HasSharedConstantFields() As Boolean
        Get
            Dim ctors As Generic.List(Of ConstantDeclaration)
            ctors = Me.Members.GetSpecificMembers(Of ConstantDeclaration)()
            Return ctors.Count > 0
        End Get
    End Property

    ReadOnly Property HasSharedFieldsWithInitializers() As Boolean
        Get
            Dim ctors As Generic.List(Of VariableDeclaration)
            ctors = Me.Members.GetSpecificMembers(Of VariableDeclaration)()
            For Each item As VariableDeclaration In ctors
                If item.IsShared AndAlso item.HasInitializer Then Return True
            Next
            For Each item As VariableDeclaration In m_StaticVariables
                If item.DeclaringMethod.IsShared AndAlso item.HasInitializer Then Return True
            Next
            Return False
        End Get
    End Property

End Class
