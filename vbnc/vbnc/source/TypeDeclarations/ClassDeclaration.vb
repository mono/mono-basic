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

Imports System.Reflection
Imports System.Reflection.Emit

''' <summary>
''' ClassDeclaration  ::=
'''	[  Attributes  ]  [  ClassModifier+  ]  "Class"  Identifier  [  TypeParameters  ]  StatementTerminator
'''	[  ClassBase  ]
'''	[  TypeImplementsClause+  ]
'''	[  ClassMemberDeclaration+  ]
'''	"End" "Class" StatementTerminator
''' 
''' ClassBase ::= Inherits NonArrayTypeName StatementTerminator
''' </summary>
''' <remarks></remarks>
Public Class ClassDeclaration
    Inherits PartialTypeDeclaration

    'Due to partial classes there may be more than one inherits clause per class
    Private m_InheritsClauses As Generic.List(Of NonArrayTypeName)
    Private m_InheritsType As Mono.Cecil.TypeReference
    Private m_CreatedImplicitMembers As Boolean
    Private m_HasImplicitInstanceConstructor As Boolean

    Sub New(ByVal Parent As ParsedObject, ByVal [Namespace] As String, ByVal Name As Identifier, ByVal TypeParameters As TypeParameters)
        MyBase.New(Parent, [Namespace], Name, TypeParameters)
    End Sub

    Sub AddInheritsClause(ByVal Clause As NonArrayTypeName)
        If m_InheritsClauses Is Nothing Then
            m_InheritsClauses = New Generic.List(Of NonArrayTypeName)
        End If
        m_InheritsClauses.Add(Clause)
    End Sub

    ''' <summary>
    ''' Returns the default constructor (non-private, non-shared, with no parameters) for the base type (if any).         
    ''' If no constructor found, returns nothing.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBaseDefaultConstructor() As Mono.Cecil.MethodReference
        If CecilHelper.IsGenericType(Me.BaseType) Then
            Return Helper.GetDefaultGenericConstructor(Me.BaseType)
        Else
            Return Helper.GetDefaultConstructor(Me.BaseType)
        End If
    End Function

    ''' <summary>
    ''' Returns the default constructor (non-private, non-shared, with no parameters) for the base type (if any).          
    ''' If no constructor found, returns nothing.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBaseDefaultConstructorCecil() As Mono.Cecil.MethodReference
        If CecilHelper.IsGenericType(Me.BaseType) Then
            Return Helper.GetDefaultGenericConstructor(Me.BaseType)
        Else
            Return Helper.GetDefaultConstructor(Me.BaseType)
        End If
    End Function

    Public Overrides Function CreateDefinition() As Boolean
        Dim result As Boolean = True

        result = MyBase.CreateDefinition AndAlso result

        If Me.Modifiers.Is(ModifierMasks.MustInherit) Then
            TypeAttributes = TypeAttributes Or Mono.Cecil.TypeAttributes.Abstract
        ElseIf Me.Modifiers.Is(ModifierMasks.NotInheritable) Then
            TypeAttributes = TypeAttributes Or Mono.Cecil.TypeAttributes.Sealed
        End If

        Return result
    End Function

    Public Overrides Function ResolveBaseType() As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveBaseType() AndAlso result

        If m_InheritsClauses IsNot Nothing AndAlso m_InheritsClauses.Count > 0 Then
            For i As Integer = 0 To m_InheritsClauses.Count - 1
                result = m_InheritsClauses(i).ResolveTypeReferences AndAlso result
            Next
            If result = False Then Return False

            For i As Integer = 1 To m_InheritsClauses.Count - 1
                If Helper.CompareType(m_InheritsClauses(0).ResolvedType, m_InheritsClauses(i).ResolvedType) = False Then
                    Helper.AddError(Compiler, Me.Location, "Class specifies different base classes")
                    result = False
                End If
            Next
            If result = False Then Return False
            BaseType = m_InheritsClauses(0).ResolvedType
        Else
            BaseType = Compiler.TypeCache.System_Object
        End If

        'We need to clear the member cache here since our base type has changed
        Compiler.TypeManager.ClearCache(Me.CecilType)

        Return result
    End Function

    Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        Helper.Assert(m_InheritsClauses Is Nothing OrElse m_InheritsClauses.Count > 0) 'Perf check

        result = MyBase.ResolveTypeReferences AndAlso result

        Return result
    End Function

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = VerifyImplicitConstructor() AndAlso result

        result = MyBase.ResolveCode(Info) AndAlso result

        Return result
    End Function

    Private Function AddInitializeComponentCall(ByVal Ctor As ConstructorDeclaration) As Boolean
        Dim result As Boolean = True

        'If the class has the Microsoft.VisualBasic.CompilerServices.DesignerGeneratedAttribute, we call InitializeComponent.
        'InitializeComponent must be correctly spelled (case-sensitive), have no parameters (not even optional) and must be private
        Dim attribs As Generic.List(Of Attribute)
        attribs = Me.FindAttributes(Compiler.TypeCache.MS_VB_CS_DesignerGeneratedAttribute)

        If attribs Is Nothing OrElse attribs.Count = 0 Then Return result

        Dim methods As Generic.List(Of INameable)

        methods = Members.Index.Item("InitializeComponent")

        If methods Is Nothing OrElse methods.Count = 0 Then Return result

        methods = New Generic.List(Of INameable)(methods)

        For i As Integer = methods.Count - 1 To 0 Step -1
            Dim subD As SubDeclaration
            subD = TryCast(methods(i), SubDeclaration)
            If subD Is Nothing Then
                methods.RemoveAt(i)
                Continue For
            End If

            If subD.Signature.ReturnType IsNot Nothing AndAlso Helper.CompareType(subD.Signature.ReturnType, Compiler.TypeCache.System_Void) = False Then
                methods.RemoveAt(i)
                Continue For
            End If

            If subD.Signature.Parameters.Count <> 0 Then
                methods.RemoveAt(i)
                Continue For
            End If

            If subD.Modifiers.Is(ModifierMasks.Private) = False Then
                methods.RemoveAt(i)
                Continue For
            End If
        Next

        If methods.Count = 0 Then Return result

        If methods.Count > 1 Then
            'Huh?
            Helper.StopIfDebugging(True)
        Else
            Dim callStmt As New CallStatement(Me)
            Dim sExp As New SimpleNameExpression(callStmt)
            sExp.Init(New Identifier(Me, "InitializeComponent", Me.Location, TypeCharacters.Characters.None), Nothing)
            callStmt.Init(sExp)
            Ctor.Code.AddStatement(callStmt)
        End If

        Return result
    End Function

    Public Overrides Function CreateImplicitInstanceConstructors() As Boolean
        Dim result As Boolean = True

        'Find the default constructors for this class
        Me.FindDefaultConstructors()

        result = MyBase.CreateImplicitInstanceConstructors() AndAlso result

        'If a type contains no instance constructor declarations, a default constructor 
        'is automatically provided. The default constructor simply invokes the 
        'parameterless constructor of the direct base type. If the direct 
        'base type does not have an accessible parameterless constructor, 
        'a compile-time error occurs. 
        'The declared access type for the default constructor is always Public. 
        If HasInstanceConstructors = False Then
            Dim ctor As ConstructorDeclaration
            Dim modifiers As Modifiers

            If Me.Modifiers.Is(ModifierMasks.MustInherit) Then
                modifiers.AddModifier(KS.Protected)
            End If

            ctor = New ConstructorDeclaration(Me)
            ctor.Init(modifiers, New SubSignature(ctor, ConstructorDeclaration.ConstructorName, New ParameterList(ctor)), New CodeBlock(ctor))

            Members.Add(ctor)

            result = ctor.CreateDefinition AndAlso result
            result = AddInitializeComponentCall(ctor) AndAlso result

            DefaultInstanceConstructor = ctor
            m_HasImplicitInstanceConstructor = True
        End If

        Return result
    End Function

    Protected Overrides ReadOnly Property NeedsSharedConstructor As Boolean
        Get
            Return Me.HasSharedFieldsWithInitializers
        End Get
    End Property

    Private Function VerifyImplicitConstructor() As Boolean
        Dim result As Boolean = True
        Dim baseDefaultCtor As Mono.Cecil.MethodReference

        If m_HasImplicitInstanceConstructor = False Then Return result

        baseDefaultCtor = Me.GetBaseDefaultConstructor()

        If baseDefaultCtor IsNot Nothing Then
            If Helper.IsPrivate(baseDefaultCtor) Then
                result = Compiler.Report.ShowMessage(Messages.VBNC30387, Location, Name, BaseType.Name) AndAlso result
            Else
                result = AddInitializeComponentCall(Me.DefaultInstanceConstructor) AndAlso result
            End If
        Else
            result = Compiler.Report.ShowMessage(Messages.VBNC30387, Location, Name, BaseType.Name) AndAlso result
        End If

        Return result
    End Function

    Public Function CreateMyGroupMembers() As Boolean
        Dim result As Boolean = True

        If Me.CustomAttributes Is Nothing Then Return result

        Dim attrib As Attribute
        Dim attribs As Generic.List(Of Attribute)

        attribs = Me.CustomAttributes.FindAttributes(Compiler.TypeCache.MS_VB_MyGroupCollectionAttribute)

        If attribs Is Nothing Then Return result
        If attribs.Count <> 1 Then Return result

        attrib = attribs(0)

        Dim groupData As New MyGroupData
        Dim typeToCollect As String
        Dim createInstanceMethodName As String
        Dim disposeInstanceMethodName As String
        Dim defaultInstanceAlias As String

        If Not attrib.ResolveCode(ResolveInfo.Default(Compiler)) Then
            'The attribute is not instantiated correctly, this will cause an error on the attribute
            'no need to show more errors here.
            Return result
        End If

        'Check the number of arguments and their types
        'There should be 4 string arguments, if not there's something wrong with
        'the MyGroupCollectionAttribute (won't reach here if the code is wrong
        'because we resolve the attribute first)
        'It's also safe to index the arguments, since attributes can't have named constructor parameters.
        Dim args As Object() = attrib.Arguments
        If args Is Nothing OrElse args.Length <> 4 Then
            Throw New InternalException("Weird MyGroupCollectionAttribute, should have 4 arguments.")
        Else
            For Each arg As Object In args
                If arg Is Nothing Then Continue For
                If TypeOf arg Is String Then Continue For
                Throw New InternalException("Weird MyGroupCollectionAttribute, non-string argument?")
            Next
        End If
        typeToCollect = DirectCast(args(0), String)
        createInstanceMethodName = DirectCast(args(1), String)
        disposeInstanceMethodName = DirectCast(args(2), String)
        defaultInstanceAlias = DirectCast(args(3), String)

        If typeToCollect = String.Empty Then Return result
        If createInstanceMethodName = String.Empty Then Return result
        If disposeInstanceMethodName = String.Empty Then Return result

        Dim collectType As Mono.Cecil.TypeReference
        Dim foundTypes As Generic.List(Of Mono.Cecil.TypeReference)
        foundTypes = Compiler.TypeManager.GetType(typeToCollect, False)
        If foundTypes.Count <> 1 Then
            Return result
        End If
        collectType = foundTypes(0)
        groupData.TypeToCollect = collectType

        For Each mi As MethodDeclaration In Members.GetSpecificMembers(Of MethodDeclaration)()
            If mi.IsShared AndAlso Helper.CompareName(createInstanceMethodName, mi.Name) Then
                If mi.Signature.Parameters.Count <> 1 Then Continue For
                If mi.Signature.TypeParameters Is Nothing OrElse mi.Signature.TypeParameters.Parameters.Count <> 1 Then Continue For
                If mi.Signature.ReturnType Is Nothing Then Continue For

                Dim T As TypeParameter = mi.Signature.TypeParameters.Parameters(0)
                If T.TypeParameterConstraints Is Nothing Then Continue For

                Dim constraints As ConstraintList = T.TypeParameterConstraints.Constraints
                If constraints.Count <> 2 Then Continue For

                If Not (constraints(0).Special = KS.[New] OrElse constraints(1).Special = KS.[New]) Then Continue For

                Dim tn As TypeName
                tn = constraints(0).TypeName
                If tn Is Nothing Then tn = constraints(1).TypeName
                If tn Is Nothing Then Continue For
                If Not Helper.CompareType(tn.ResolvedType, groupData.TypeToCollect) Then Continue For

                If Helper.CompareType(mi.Signature.Parameters(0).ParameterType, T.CecilBuilder) = False Then Continue For
                If Helper.CompareType(mi.Signature.ReturnType, T.CecilBuilder) = False Then Continue For


                If groupData.CreateInstanceMethod IsNot Nothing Then Continue For
                groupData.CreateInstanceMethod = mi.CecilBuilder
            ElseIf mi.IsShared = False AndAlso Helper.CompareName(disposeInstanceMethodName, mi.Name) Then
                If mi.Signature.Parameters.Count <> 1 Then Continue For
                If mi.Signature.TypeParameters Is Nothing OrElse mi.Signature.TypeParameters.Parameters.Count <> 1 Then Continue For
                If mi.Signature.ReturnType IsNot Nothing AndAlso Helper.CompareType(mi.Signature.ReturnType, Compiler.TypeCache.System_Void) = False Then Continue For

                Dim T As TypeParameter = mi.Signature.TypeParameters.Parameters(0)
                'SPECBUG: the MyWebServices group class doesn't have a constraint here
                'https://connect.microsoft.com/VisualStudio/feedback/details/566024/vb-10-specification-is-inaccurate-about-dispose-methods-in-group-classes
                If T.TypeParameterConstraints Is Nothing OrElse T.TypeParameterConstraints.Constraints Is Nothing OrElse T.TypeParameterConstraints.Constraints.Count <> 1 Then Continue For
                If Not Helper.CompareType(T.TypeParameterConstraints.Constraints(0).TypeName.ResolvedType, groupData.TypeToCollect) Then Continue For

                If Helper.CompareType(mi.Signature.Parameters(0).ParameterType, Compiler.TypeManager.MakeByRefType(Me, T.CecilBuilder)) = False Then Continue For

                If groupData.DisposeInstanceMethod IsNot Nothing Then Continue For
                groupData.DisposeInstanceMethod = mi.CecilBuilder
            End If
            If groupData.DisposeInstanceMethod IsNot Nothing AndAlso groupData.CreateInstanceMethod IsNot Nothing Then Exit For
        Next

        If groupData.CreateInstanceMethod Is Nothing Then Return result
        If groupData.DisposeInstanceMethod Is Nothing Then Return result

        If Compiler.Assembly.GroupedClasses Is Nothing Then Compiler.Assembly.GroupedClasses = New Generic.List(Of MyGroupData)
        Compiler.Assembly.GroupedClasses.Add(groupData)

        'Parse the alias
        If defaultInstanceAlias <> String.Empty Then
            Dim scanner As New Scanner(Compiler, defaultInstanceAlias)
            Dim parser As New Parser(Compiler, scanner)
            Dim alias_exp As Expression
            'TODO: We'll show parser errors here in the compiler if there are any errors
            alias_exp = parser.ParseExpression(Me)

            If alias_exp IsNot Nothing Then
                Dim alias_result As Boolean
                alias_result = alias_exp.ResolveExpression(New ResolveInfo(Compiler))
                If alias_result Then
                    groupData.DefaultInstanceAlias = alias_exp
                End If
            End If
        End If


        'Find all non-generic types that inherit from the type to collect
        Dim typesCollected As New Generic.List(Of TypeDeclaration)
        Dim namesUsed As New Generic.Dictionary(Of String, Object)(Helper.StringComparer)
        Dim namesClashed As New Generic.Dictionary(Of String, Object)(Helper.StringComparer)
        For Each type As TypeDeclaration In Compiler.theAss.Types
            Dim classType As ClassDeclaration = TryCast(type, ClassDeclaration)

            If classType Is Nothing Then Continue For
            If classType.TypeParameters IsNot Nothing AndAlso classType.TypeParameters.Parameters.Count > 0 Then Continue For

            If Helper.IsSubclassOf(collectType, type.CecilType) Then
                typesCollected.Add(type)
                If namesUsed.ContainsKey(type.Name) Then
                    namesClashed.Add(type.Name, Nothing)
                Else
                    namesUsed.Add(type.Name, Nothing)
                End If
            End If
        Next

        For Each type As TypeDeclaration In typesCollected
            Dim propertyName As String
            Dim fieldName As String

            If namesClashed.ContainsKey(type.Name) Then
                propertyName = type.FullName.Replace(".", "_")
            Else
                propertyName = type.Name
            End If
            fieldName = "m_" & propertyName

            Dim field As New TypeVariableDeclaration(Me)
            Dim prop As New PropertyDeclaration(Me)
            Dim modifiers As New Modifiers(ModifierMasks.Public)

            field.Init(modifiers, fieldName, type.CecilType)
            prop.Init(modifiers, propertyName, type.CecilType)

            type.MyGroupField = field

            Dim setter As MethodDeclaration
            Dim getter As MethodDeclaration

            getter = prop.GetDeclaration
            setter = prop.SetDeclaration

            getter.Code = New CodeBlock(getter)
            setter.Code = New CodeBlock(setter)

            Dim get_1 As New AssignmentStatement(getter.Code)
            Dim get_1_left As New SimpleNameExpression(get_1)
            Dim get_1_right As New InvocationOrIndexExpression(get_1)
            Dim get_1_right_instance_exp As New SimpleNameExpression(get_1_right)
            Dim get_1_right_instance_exp_typeargs As New TypeArgumentList(get_1_right_instance_exp)
            Dim get_1_right_instance_exp_typeargs_1 As New TypeName(get_1_right_instance_exp_typeargs)
            Dim get_1_right_arg1 As New SimpleNameExpression(get_1_right)
            Dim get_1_right_arglist As New ArgumentList(get_1_right, get_1_right_arg1)
            Dim get_1_right_field_token As Identifier = New Identifier(get_1_right, fieldName, attrib.Location, TypeCharacters.Characters.None)
            Dim get_1_right_method_token As Identifier = New Identifier(get_1_right, createInstanceMethodName, attrib.Location, TypeCharacters.Characters.None)

            get_1_left.Init(get_1_right_field_token, Nothing)

            get_1_right_instance_exp_typeargs_1.Init(type.CecilType)
            get_1_right_instance_exp_typeargs.Add(get_1_right_instance_exp_typeargs_1)
            get_1_right_instance_exp.Init(get_1_right_method_token, get_1_right_instance_exp_typeargs)
            get_1_right_arg1.Init(get_1_right_field_token, Nothing)
            get_1_right.Init(get_1_right_instance_exp, get_1_right_arglist)
            get_1.Init(get_1_left, get_1_right)

            Dim get_2 As New ReturnStatement(getter.Code)
            Dim get_2_exp As New SimpleNameExpression(get_2)
            get_2_exp.Init(get_1_right_field_token, Nothing)
            get_2.Init(get_2_exp)

            getter.Code.AddStatement(get_1)
            getter.Code.AddStatement(get_2)

            Dim set_if1 As New IfStatement(setter.Code)
            Dim value_token As Identifier = New Identifier(set_if1, "Value", attrib.Location, TypeCharacters.Characters.None)
            Dim field_token As Identifier = New Identifier(set_if1, fieldName, attrib.Location, TypeCharacters.Characters.None)
            Dim set_if1_condition_left As New SimpleNameExpression(set_if1)
            Dim set_if1_condition_right As New SimpleNameExpression(set_if1)
            Dim set_if1_condition As New Is_IsNotExpression(set_if1, set_if1_condition_left, set_if1_condition_right, KS.IsNot)
            Dim set_if1_code As New CodeBlock(set_if1)
            Dim set_if2 As New IfStatement(set_if1)
            Dim set_if2_condition_right As New NothingConstantExpression(set_if2)
            Dim set_if2_condition As New Is_IsNotExpression(set_if2, set_if1_condition_left, set_if2_condition_right, KS.IsNot)
            Dim set_if2_code As New CodeBlock(set_if2)
            Dim set_throw As New ThrowStatement(set_if2_code)
            Dim set_throw_creation As New DelegateOrObjectCreationExpression(set_throw)
            Dim set_throw_arg1 As New ConstantExpression(set_throw_creation, "Property can only be set to Nothing", Compiler.TypeCache.System_String)
            Dim set_throw_args As New ArgumentList(set_throw_creation, set_throw_arg1)
            Dim set_dispose As New CallStatement(set_if1)
            Dim set_dispose_invocation As New InvocationOrIndexExpression(set_dispose)
            Dim set_dispose_invocation_instance_exp As New SimpleNameExpression(set_dispose_invocation)
            Dim set_dispose_invocation_instance_exp_typeargs As New TypeArgumentList(set_dispose_invocation_instance_exp)
            Dim set_dispose_invocation_instance_exp_typeargs_1 As New TypeName(set_dispose_invocation_instance_exp_typeargs)
            Dim set_dispose_invocation_arg1 As New SimpleNameExpression(set_dispose_invocation)
            Dim set_dispose_invocation_arglist As New ArgumentList(set_dispose_invocation, set_dispose_invocation_arg1)
            Dim set_dispose_invocation_field_token As Identifier = New Identifier(set_dispose_invocation, fieldName, attrib.Location, TypeCharacters.Characters.None)
            Dim set_dispose_invocation_method_token As Identifier = New Identifier(set_dispose_invocation, disposeInstanceMethodName, attrib.Location, TypeCharacters.Characters.None)

            set_throw_creation.Init(Compiler.TypeCache.System_ArgumentException, set_throw_args)
            set_throw.Init(set_throw_creation)

            set_if2_code.AddStatement(set_throw)

            set_dispose_invocation_instance_exp_typeargs_1.Init(type.CecilType)
            set_dispose_invocation_instance_exp_typeargs.Add(set_dispose_invocation_instance_exp_typeargs_1)
            set_dispose_invocation_instance_exp.Init(set_dispose_invocation_method_token, set_dispose_invocation_instance_exp_typeargs)
            set_dispose_invocation_arg1.Init(set_dispose_invocation_field_token, Nothing)
            set_dispose_invocation.Init(set_dispose_invocation_instance_exp, set_dispose_invocation_arglist)
            set_dispose.Init(set_dispose_invocation)

            set_if1_code.AddStatement(set_if2)
            set_if1_code.AddStatement(set_dispose)

            set_if1_condition_left.Init(value_token, Nothing)
            set_if1_condition_right.Init(field_token, Nothing)

            set_if1.Init(set_if1_condition, Nothing, set_if1_code, False, Nothing)
            set_if2.Init(set_if2_condition, Nothing, set_if2_code, False, Nothing)

            setter.Code.AddStatement(set_if1)

            Members.Add(field)
            Members.Add(prop)

            result = field.CreateDefinition AndAlso result
            result = prop.CreateDefinition AndAlso result

            result = setter.ResolveTypeReferences AndAlso result
            result = getter.ResolveTypeReferences AndAlso result

            'Me.TypeDescriptor.ClearCache()

            If Compiler.TypeManager.ContainsCache(Me.CecilType) Then
                Dim cache As MemberCache = Compiler.TypeManager.GetCache(Me.CecilType)
                'cache.Cache.Add(New MemberCacheEntry(field.FieldDescriptor))
                'cache.Cache.Add(New MemberCacheEntry(prop.MemberDescriptor))
                'cache.FlattenedCache.Add(New MemberCacheEntry(field.FieldDescriptor))
                'cache.FlattenedCache.Add(New MemberCacheEntry(prop.MemberDescriptor))
                cache.ClearAll()
            End If
        Next

        Return result
    End Function

    Public Overrides Function DefineTypeHierarchy() As Boolean
        Dim result As Boolean = True

        'Define type parameters
        result = MyBase.DefineTypeHierarchy AndAlso result

        Return result
    End Function

    Shared Function IsMe(ByVal tm As tm) As Boolean
        Dim i As Integer
        While tm.PeekToken(i).Equals(ModifierMasks.ClassModifiers)
            i += 1
        End While
        Return tm.PeekToken(i).Equals(KS.Class)
    End Function
End Class
