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
''' A single identifier followed by an optional type argument list.
''' Classifications: Variable, Type, Value, Namespace
''' 
''' SimpleNameExpression ::= Identifier [ "(" "Of" TypeArgumentList ")" ]
''' </summary>
''' <remarks></remarks>
Public Class SimpleNameExpression
    Inherits Expression

    Private m_Identifier As Identifier
    Private m_TypeArgumentList As TypeArgumentList
    'When Infer is possible, Resolve returns false, no errors are reported, and InferPossible is true
    Public InferEnabled As Boolean
    Public InferPossible As Boolean

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal Identifier As Identifier, ByVal TypeArgumentList As TypeArgumentList)
        m_Identifier = Identifier
        m_TypeArgumentList = TypeArgumentList
    End Sub

    ReadOnly Property TypeArguments As TypeArgumentList
        Get
            Return m_TypeArgumentList
        End Get
    End Property

    Public Overrides ReadOnly Property AsString() As String
        Get
            If m_TypeArgumentList Is Nothing OrElse m_TypeArgumentList.Count = 0 Then
                Return m_Identifier.Identifier
            Else
                Return m_Identifier.Identifier & "(Of <type arguments>)"
            End If
        End Get
    End Property

    Property Identifier() As Identifier
        Get
            Return m_Identifier
        End Get
        Set(ByVal value As Identifier)
            m_Identifier = value
        End Set
    End Property

    Overrides ReadOnly Property ExpressionType() As Mono.Cecil.TypeReference
        Get
            Dim result As Mono.Cecil.TypeReference
            Select Case Classification.Classification
                Case ExpressionClassification.Classifications.Value
                    result = Classification.AsValueClassification.Type
                Case ExpressionClassification.Classifications.Variable
                    result = Classification.AsVariableClassification.Type
                Case ExpressionClassification.Classifications.Type
                    Compiler.Report.ShowMessage(Messages.VBNC99997, Location)
                    result = Nothing
                Case ExpressionClassification.Classifications.Namespace
                    Compiler.Report.ShowMessage(Messages.VBNC99997, Location)
                    result = Nothing
                Case ExpressionClassification.Classifications.PropertyGroup
                    result = Classification.AsPropertyGroup.Type
                Case ExpressionClassification.Classifications.PropertyAccess
                    result = Classification.AsPropertyAccess.Type
                Case ExpressionClassification.Classifications.MethodGroup
                    result = Classification.AsMethodGroupClassification.Type
                Case Else
                    Throw New InternalException(Me)
            End Select
            Helper.Assert(result IsNot Nothing)
            Return result
        End Get
    End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        If m_TypeArgumentList IsNot Nothing Then result = m_TypeArgumentList.ResolveTypeReferences AndAlso result

        Return result
    End Function

    Public Overrides Function ToString() As String
        Return m_Identifier.Identifier
    End Function

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Helper.Assert(Info.DesiredType IsNot Nothing OrElse Info.RHSExpression IsNot Nothing)

        If Info.IsRHS Then
            If Info.DesiredType IsNot Nothing Then
                If CecilHelper.IsGenericParameter(Info.DesiredType) AndAlso CecilHelper.IsGenericParameter(Me.ExpressionType) Then
                    Helper.Assert(Me.Classification.CanBeValueClassification)
                    Dim tmp As Expression
                    tmp = Me.ReclassifyToValueExpression()
                    result = tmp.ResolveExpression(ResolveInfo.Default(Info.Compiler)) AndAlso result
                    result = tmp.GenerateCode(Info) AndAlso result
                ElseIf CecilHelper.IsByRef(Info.DesiredType) = False AndAlso CecilHelper.IsGenericParameter(Me.ExpressionType) = False Then
                    If Me.Classification.CanBeValueClassification Then
                        Dim tmp As Expression
                        tmp = Me.ReclassifyToValueExpression()
                        result = tmp.ResolveExpression(ResolveInfo.Default(Info.Compiler)) AndAlso result
                        result = tmp.Classification.GenerateCode(Info) AndAlso result
                    Else
                        Throw New InternalException(Me)
                    End If
                Else
                    If Me.Classification.IsVariableClassification Then
                        If CecilHelper.IsByRef(Me.ExpressionType) OrElse CecilHelper.IsArray(Me.ExpressionType) Then
                            Emitter.EmitLoadVariable(Info, Me.Classification.AsVariableClassification)
                        Else
                            Emitter.EmitLoadVariableLocation(Info, Me.Classification.AsVariableClassification)
                        End If
                    Else
                        Throw New InternalException(Me)
                    End If
                End If
            Else
                If Me.Classification.CanBeValueClassification Then
                    Dim tmp As Expression
                    tmp = Me.ReclassifyToValueExpression()
                    result = tmp.ResolveExpression(ResolveInfo.Default(Info.Compiler)) AndAlso result
                    result = tmp.GenerateCode(Info) AndAlso result
                Else
                    Throw New InternalException(Me)
                End If
            End If
        ElseIf Info.IsLHS Then
            If Me.Classification.IsVariableClassification Then
                result = Me.Classification.AsVariableClassification.GenerateCode(Info) AndAlso result
            ElseIf Me.Classification.IsPropertyGroupClassification Then
                Return Compiler.Report.ShowMessage(Messages.VBNC99997, Location)
            ElseIf Me.Classification.IsValueClassification Then
                Throw New InternalException(Me)
            Else
                Throw New InternalException(Me)
            End If
        Else
            Throw New InternalException(Me)
        End If

        Return result
    End Function

    Public Overrides Function GetConstant(ByRef result As Object, ByVal ShowError As Boolean) As Boolean
        Return Classification.GetConstant(result, ShowError)
    End Function

    Shared Function IsMe(ByVal tm As tm) As Boolean
        Return tm.CurrentToken.IsIdentifier
    End Function

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim Name As String = m_Identifier.Identifier
        Dim wasError As Boolean

        '---------------------------------------------------------------------------------------------------------
        'A simple name expression consists of a single identifier followed by an optional type argument list. 
        'The name is resolved and classified as follows:
        '---------------------------------------------------------------------------------------------------------
        '* Starting with the immediately enclosing block and continuing with each enclosing outer block (if any),
        '  if the identifier matches the name of a local variable, static variable, constant local, method type 
        '  parameter, or parameter, then the identifier refers to the matching entity. The expression is 
        '  classified as a variable if it is a local variable, static variable, or parameter. The expression 
        '  is classified as a type if it is a method type parameter. The expression is classified as a value 
        '  if it is a constant local with the following exception. If the local variable matched is the 
        '  implicit function or Get accessor return local variable, and the expression is part of an 
        '  invocation expression, invocation statement, or an AddressOf expression, then no match occurs and 
        '  resolution continues.
        '---------------------------------------------------------------------------------------------------------
        '* For each nested type containing the expression, starting from the innermost and going to the 
        '  outermost, if a lookup of the identifier in the type produces a match with an accessible member:
        '** If the matching type member is a type parameter, then the result is classified as a type and 
        '   is the matching type parameter.
        '** Otherwise, if the type is the immediately enclosing type and the lookup identifies a non-shared 
        '   type member, then the result is the same as a member access of the form Me.E, where E is 
        '   the identifier.
        '** Otherwise, the result is exactly the same as a member access of the form T.E, where T is the 
        '   type containing the matching member and E is the identifier. In this case, it is an error for the 
        '   identifier to refer to a non-shared member.
        '---------------------------------------------------------------------------------------------------------
        '* For each nested namespace, starting from the innermost and going to the outermost namespace, 
        '  do the following:
        '** If the namespace contains an accessible namespace member with the given name, then the identifier
        '   refers to that member and, depending on the member, is classified as a namespace or a type.
        '** Otherwise, if the namespace contains one or more accessible standard modules, and a member name 
        '   lookup of the identifier produces an accessible match in exactly one standard module, then the 
        '   result is exactly the same as a member access of the form M.E, where M is the standard module 
        '   containing the matching member and E is the identifier. If the identifier matches accessible type 
        '   members in more than one standard module, a compile-time error occurs.
        '---------------------------------------------------------------------------------------------------------
        '* If the source file has one or more import aliases, and the identifier matches the name of one of them,
        '   then the identifier refers to that namespace or type.
        '---------------------------------------------------------------------------------------------------------
        '* If the source file containing the name reference has one or more imports:
        '** If the identifier matches the name of an accessible type or type member in exactly one import, 
        '   then the identifier refers to that type or type member. If the identifier matches the name of 
        '   an accessible type or type member in more than one import, a compile-time error occurs.
        '** If the identifier matches the name of a namespace in exactly one import, then the identifier 
        '   refers to that namespace. If the identifier matches the name of a namespace in more than one import, 
        '   a compile-time error occurs.
        '** Otherwise, if the imports contain one or more accessible standard modules, and a member name 
        '   lookup of the identifier produces an accessible match in exactly one standard module, then 
        '   the result is exactly the same as a member access of the form M.E, where M is the standard 
        '   module containing the matching member and E is the identifier. If the identifier matches 
        '   accessible type members in more than one standard module, a compile-time error occurs.
        '---------------------------------------------------------------------------------------------------------
        '* If the compilation environment defines one or more import aliases, and the identifier matches 
        '  the name of one of them, then the identifier refers to that namespace or type.
        '---------------------------------------------------------------------------------------------------------
        '* If the compilation environment defines one or more imports:
        '** If the identifier matches the name of an accessible type or type member in exactly one import, 
        '   then the identifier refers to that type or type member. If the identifier matches the name 
        '   of an accessible type or type member in more than one import, a compile-time error occurs.
        '** If the identifier matches the name of a namespace in exactly one import, then the identifier 
        '   refers to that namespace. If the identifier matches the name of a namespace in more than 
        '   one import, a compile-time error occurs.
        '** Otherwise, if the imports contain one or more accessible standard modules, and a member name 
        '   lookup of the identifier produces an accessible match in exactly one standard module, then the result 
        '   is exactly the same as a member access of the form M.E, where M is the standard module containing 
        '   the matching member and E is the identifier. If the identifier matches accessible type members in 
        '   more than one standard module, a compile-time error occurs.
        '---------------------------------------------------------------------------------------------------------
        '* Otherwise, the name given by the identifier is undefined and a compile-time error occurs.
        '---------------------------------------------------------------------------------------------------------
        'If a simple name with a type argument list resolves to anything other than a type or method, 
        'a compile time error occurs. If a type argument list is supplied, only types with the same arity as 
        'the type argument list are considered but type members, including methods with different arities, 
        'are still considered. This is because type inference can be used to fill in missing type arguments. 
        'As a result, names with type arguments may bind differently to types and methods:
        '---------------------------------------------------------------------------------------------------------

        If m_TypeArgumentList IsNot Nothing Then If m_TypeArgumentList.ResolveCode(Info) = False Then Return False

        '* Starting with the immediately enclosing block and continuing with each enclosing outer block (if any),
        '  if the identifier matches the name of a local variable, static variable, constant local, method type 
        '  parameter, or parameter, then the identifier refers to the matching entity. 
        '  - The expression is classified as a variable if it is a local variable, static variable, or parameter.
        '  - The expression is classified as a type if it is a method type parameter. 
        '  - The expression is classified as a value if it is a constant local.
        '  * With the following exception:
        '  If the local variable matched is the implicit function or Get accessor return local variable, 
        '  and the expression is part of an  invocation expression, invocation statement, 
        '  or an AddressOf expression, then no match occurs and resolution continues.
        Dim block As CodeBlock = Me.FindFirstParent(Of CodeBlock)()
        While block IsNot Nothing
            Dim var As IAttributableNamedDeclaration
            var = block.FindVariable(Name)
            If TypeOf var Is ConstantDeclaration Then
                'The expression is classified as a value if it is a constant local (...)
                Classification = New ValueClassification(Me, DirectCast(var, ConstantDeclaration))
                Return True
            ElseIf TypeOf var Is VariableDeclaration Then
                'The expression is classified as a variable if it is a local variable, static variable (...)
                Dim varDecl As VariableDeclaration
                varDecl = DirectCast(var, VariableDeclaration)
                varDecl.IsReferenced = True
                If varDecl.Modifiers.Is(ModifierMasks.Static) AndAlso varDecl.DeclaringMethod.IsShared = False Then
                    Classification = New VariableClassification(Me, varDecl, CreateMeExpression)
                ElseIf varDecl.Modifiers.Is(ModifierMasks.Const) Then
                    Classification = New ValueClassification(Me, varDecl)
                Else
                    Classification = New VariableClassification(Me, varDecl)
                End If

                If var.Location > Me.Location Then
                    Return Compiler.Report.ShowMessage(Messages.VBNC32000, Me.Location, var.Name)
                End If

                Return True
            ElseIf var IsNot Nothing Then
                Throw New InternalException(Me)
            End If
            block = block.FindFirstParent(Of CodeBlock)()
        End While

        Dim method As IMethod
        method = Me.FindFirstParent(Of IMethod)()

        If Me.FindFirstParent(Of Parameter)() IsNot Nothing Then
            method = Nothing
        End If

        If method IsNot Nothing Then
            If method.Signature.TypeParameters IsNot Nothing Then
                Dim typeparam As TypeParameter = method.Signature.TypeParameters.Parameters.Item(Name)
                If typeparam IsNot Nothing Then
                    'The expression is classified as a type if it is a method type parameter. 
                    Classification = New TypeClassification(Me, typeparam)
                    Return True
                End If
            End If
        End If

        If method IsNot Nothing Then
            If method.Signature.Parameters IsNot Nothing Then
                Dim param As Parameter = method.Signature.Parameters.Item(Name)
                If param IsNot Nothing Then
                    'The expression is classified as a variable if it is a (...) parameter
                    Classification = New VariableClassification(Me, param)
                    Return True
                End If
            End If
        End If

        '  If the local variable matched is the implicit function or Get accessor return local variable, 
        '  and the expression is part of an  invocation expression, invocation statement, 
        '  or an AddressOf expression, then no match occurs and resolution continues.
        If method IsNot Nothing Then
            If method.HasReturnValue AndAlso Info.SkipFunctionReturnVariable = False Then
                Dim pgd As PropertyGetDeclaration = TryCast(method, PropertyGetDeclaration)
                If pgd IsNot Nothing AndAlso Helper.CompareName(pgd.PropertySignature.Name, Name) Then
                    'The expression is classified as a variable if it is a local variable, static variable (...)
                    Classification = New VariableClassification(Me, method)
                    Return True
                ElseIf Helper.CompareName(method.Name, Name) Then
                    'The expression is classified as a variable if it is a local variable, static variable (...)
                    Classification = New VariableClassification(Me, method)
                    Return True
                End If
            End If
        End If

        '* For each nested type containing the expression, starting from the innermost and going to the 
        '  outermost, if a lookup of the identifier in the type produces a match with an accessible member:
        '** If the matching type member is a type parameter, then the result is classified as a type and 
        '   is the matching type parameter.
        '** Otherwise, if the type is the immediately enclosing type and the lookup identifies a non-shared 
        '   type member, then the result is the same as a member access of the form Me.E, where E is 
        '   the identifier.
        '** Otherwise, the result is exactly the same as a member access of the form T.E, where T is the 
        '   type containing the matching member and E is the identifier. In this case, it is an error for the 
        '   identifier to refer to a non-shared member.
        Dim firstcontainer As IType = Me.FindFirstParent(Of IType)()
        Dim container As IType = firstcontainer
        While container IsNot Nothing
            Dim constructable As IConstructable = TryCast(container, IConstructable)
            If constructable IsNot Nothing AndAlso constructable.TypeParameters IsNot Nothing Then
                Dim typeparam As TypeParameter = constructable.TypeParameters.Parameters.Item(Name)
                If typeparam IsNot Nothing Then
                    'If the matching type member is a type parameter, then the result is classified 
                    'as a type and is the matching type parameter.
                    Classification = New TypeClassification(Me, typeparam)
                    Return True
                End If
            End If

            Dim cache As MemberCacheEntry
            Dim members As Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference)
            cache = Compiler.TypeManager.GetCache(container.CecilType).LookupFlattened(Name)
            If cache Is Nothing Then
                members = New Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference)
            Else
                members = cache.Members
                members = Helper.FilterExternalInaccessible(Compiler, members)
            End If

#If EXTENDEDDEBUG Then
            Compiler.Report.WriteLine("Found " & membersArray.Length & " members, after filtering by name it's " & members.Count & " members")
#End If

            Helper.ApplyTypeArguments(Me, members, m_TypeArgumentList)

            If members.Count > 0 Then
                'Otherwise, if the type is the immediately enclosing type and the lookup identifies a non-shared 
                'type member, then the result is the same as a member access of the form Me.E, where E is 
                'the identifier.

                'Otherwise, the result is exactly the same as a member access of the form T.E, where T is the 
                'type containing the matching member and E is the identifier. In this case, it is an error for the 
                'identifier to refer to a non-shared member.

                'NOTE: it is not possible to determine yet if the resolved member is shared or not 
                '(it can resolve to a method group with several methods, some shared, some not. 
                'So we create a classification with an instance expression, if the member is 
                'shared, the instance expression should not be used.
                Dim instanceCount As Integer

                For i As Integer = 0 To members.Count - 1
                    Dim member As Mono.Cecil.MemberReference = members(i)
                    If CecilHelper.GetMemberType(member) = MemberTypes.TypeInfo OrElse CecilHelper.GetMemberType(member) = MemberTypes.NestedType Then
                        '
                    ElseIf Helper.IsShared(member) Then
                        '
                    Else
                        instanceCount += 1
                    End If
                Next

                If container Is firstcontainer AndAlso instanceCount > 0 Then
                    'Otherwise, if the type is the immediately enclosing type and the lookup identifies a non-shared 
                    'type member, then the result is the same as a member access of the form Me.E, where E is 
                    'the identifier.
                    If instanceCount = members.Count AndAlso IsSharedContext() Then
                        Dim show30369 As Boolean = True

                        If instanceCount = 1 Then
                            Dim fd As FieldReference
                            Dim pd As PropertyReference

                            'this is allowed: 11.6.1 Identical Type and Member Names
                            fd = TryCast(members(0), FieldReference)
                            If fd IsNot Nothing Then
                                show30369 = Helper.CompareName(fd.Name, fd.FieldType.Name) = False
                            Else
                                pd = TryCast(members(0), PropertyReference)
                                If pd IsNot Nothing Then
                                    show30369 = Helper.CompareName(pd.Name, pd.PropertyType.Name) = False
                                End If
                            End If
                            If show30369 Then Return Report.ShowMessage(Messages.VBNC30369, Me.Location)
                        End If
                    End If
                    Classification = GetMeClassification(members, firstcontainer)
                    Return True
                Else
                    'Otherwise, the result is exactly the same as a member access of the form T.E, where T is the 
                    'type containing the matching member and E is the identifier. In this case, it is an error for the                    
                    'identifier to refer to a non-shared member.
                    Classification = GetTypeClassification(members, firstcontainer)
                    Return Classification IsNot Nothing
                End If
            End If
            container = DirectCast(container, BaseObject).FindFirstParent(Of IType)()
        End While

        '* For each nested namespace, starting from the innermost and going to the outermost namespace, 
        '  do the following:
        '** If the namespace contains an accessible namespace member with the given name, then the identifier
        '   refers to that member and, depending on the member, is classified as a namespace or a type.
        '** Otherwise, if the namespace contains one or more accessible standard modules, and a member name 
        '   lookup of the identifier produces an accessible match in exactly one standard module, then the 
        '   result is exactly the same as a member access of the form M.E, where M is the standard module 
        '   containing the matching member and E is the identifier. If the identifier matches accessible type 
        '   members in more than one standard module, a compile-time error occurs.
        Dim currentNS As String = Nothing
        If firstcontainer IsNot Nothing Then currentNS = firstcontainer.Namespace
        While currentNS IsNot Nothing
            Dim foundType As Mono.Cecil.TypeReference
            foundType = Compiler.TypeManager.GetTypesByNamespace(currentNS).Item(Name)
            If foundType IsNot Nothing Then
                Classification = New TypeClassification(Me, foundType)
                Return True
            End If
            If currentNS <> "" Then
                Dim foundNS As [Namespace]
                foundNS = Compiler.TypeManager.Namespaces(currentNS & "." & Name)
                If foundNS IsNot Nothing Then
                    Classification = New NamespaceClassification(Me, foundNS)
                    Return True
                End If
            End If

            'Otherwise, if the namespace contains one or more accessible standard modules, and a member name 
            'lookup of the identifier produces an accessible match in exactly one standard module, then the 
            'result is exactly the same as a member access of the form M.E, where M is the standard module 
            'containing the matching member and E is the identifier. If the identifier matches accessible type 
            'members in more than one standard module, a compile-time error occurs.
            Dim modulemembers As Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference)
            modulemembers = Helper.GetMembersOfTypes(Compiler, Compiler.TypeManager.GetModulesByNamespace(currentNS), Name)
            If modulemembers Is Nothing Then
                'do nothing
            ElseIf modulemembers.Count >= 1 Then
                'Check that they're all from the same module
                For i As Integer = 1 To modulemembers.Count - 1
                    If Helper.CompareType(modulemembers(0).DeclaringType, modulemembers(i).DeclaringType) = False Then
                        Return Compiler.Report.ShowMessage(Messages.VBNC30562, Me.Location, Name, modulemembers(0).DeclaringType.Name, modulemembers(i).DeclaringType.Name)
                    End If
                Next
                Return SetClassificationOfModuleMembers(modulemembers)
            End If

            currentNS = Helper.GetNamespaceParent(currentNS)
        End While
        If CheckOutermostNamespace(Name) Then Return True

        '* If the source file has one or more import aliases, and the identifier matches the name of one of them,
        '   then the identifier refers to that namespace or type.
        If ResolveAliasImports(Me.Location.File(Compiler).Imports, Name) Then Return True

        '* If the source file containing the name reference has one or more imports:
        '** If the identifier matches the name of an accessible type or type member in exactly one import, 
        '   then the identifier refers to that type or type member. If the identifier matches the name of 
        '   an accessible type or type member in more than one import, a compile-time error occurs.
        '** If the identifier matches the name of a namespace in exactly one import, then the identifier 
        '   refers to that namespace. If the identifier matches the name of a namespace in more than one import, 
        '   a compile-time error occurs.
        '** Otherwise, if the imports contain one or more accessible standard modules, and a member name 
        '   lookup of the identifier produces an accessible match in exactly one standard module, then 
        '   the result is exactly the same as a member access of the form M.E, where M is the standard 
        '   module containing the matching member and E is the identifier. If the identifier matches 
        '   accessible type members in more than one standard module, a compile-time error occurs.
        If ResolveImports(Me.Location.File(Compiler).Imports, Name, wasError) Then
            Return True
        ElseIf wasError Then
            Return False
        End If

        '* If the compilation environment defines one or more import aliases, and the identifier matches 
        '  the name of one of them, then the identifier refers to that namespace or type.
        If ResolveAliasImports(Me.Compiler.CommandLine.Imports.Clauses, Name) Then Return True

        '* If the compilation environment defines one or more imports:
        '** If the identifier matches the name of an accessible type or type member in exactly one import, 
        '   then the identifier refers to that type or type member. If the identifier matches the name 
        '   of an accessible type or type member in more than one import, a compile-time error occurs.
        '** If the identifier matches the name of a namespace in exactly one import, then the identifier 
        '   refers to that namespace. If the identifier matches the name of a namespace in more than 
        '   one import, a compile-time error occurs.
        '** Otherwise, if the imports contain one or more accessible standard modules, and a member name 
        '   lookup of the identifier produces an accessible match in exactly one standard module, then the result 
        '   is exactly the same as a member access of the form M.E, where M is the standard module containing 
        '   the matching member and E is the identifier. If the identifier matches accessible type members in 
        '   more than one standard module, a compile-time error occurs.
        If ResolveImports(Me.Compiler.CommandLine.Imports.Clauses, Name, wasError) Then
            Return True
        ElseIf wasError Then
            Return False
        End If

        If Location.File(Compiler).IsOptionExplicitOn = False AndAlso Info.CanBeImplicitSimpleName Then
            Dim parent_method As MethodBaseDeclaration
            parent_method = Me.FindFirstParent(Of MethodBaseDeclaration)()

            If method IsNot Nothing Then
                Dim varD As LocalVariableDeclaration
                Dim varType As Mono.Cecil.TypeReference
                If m_Identifier.HasTypeCharacter Then
                    varType = TypeCharacters.TypeCharacterToType(Compiler, m_Identifier.TypeCharacter)
                Else
                    varType = Compiler.TypeCache.System_Object
                End If
                varD = New LocalVariableDeclaration(parent_method.Code, Nothing, m_Identifier, False, Nothing, Nothing, Nothing)
                varD.Init(Nothing, m_Identifier.Identifier, varType)
                parent_method.Code.AddVariable(varD)
                Me.Classification = New VariableClassification(Me, varD)
                Return True
            End If
        End If

        'Check if Local Type Inference is enabled        
        If InferEnabled Then
            InferPossible = True
            Return False
        End If

        '* Otherwise, the name given by the identifier is undefined and a compile-time error occurs.
        Compiler.Report.ShowMessage(Messages.VBNC30451, Me.Location, Name)

        Return False
    End Function

    Private Function GetTypeClassification(ByVal members As Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference), ByVal type As IType) As ExpressionClassification
        'Otherwise, the result is exactly the same as a member access of the form T.E, where T is the 
        'type containing the matching member and E is the identifier. In this case, it is an error for the 
        'identifier to refer to a non-shared member.

        Dim first As Mono.Cecil.MemberReference = members(0)
        '* If E is a built-in type or an expression classified as a type, and I is the name of an accessible 
        '  member of E, then E.I is evaluated and classified as follows:

        '** If I is the keyword New, then a compile-time error occurs.
        '(not applicable)

        '** If I identifies one or more methods, then the result is a method group with the associated 
        '   type argument list and no associated instance expression.
        If Helper.IsMethodDeclaration(first) Then
            Return New MethodGroupClassification(Me, Nothing, m_TypeArgumentList, Nothing, members)
        End If

        '** If I identifies one or more properties, then the result is a property group with no associated 
        '   instance expression.
        If Helper.IsPropertyDeclaration(first) Then
            Return New PropertyGroupClassification(Me, Nothing, members)
        End If

        If members.Count > 1 Then Throw New InternalException(Me)

        '** If I identifies a type, then the result is that type.
        If Helper.IsTypeDeclaration(first) Then
            Return New TypeClassification(Me, first)
        End If

        '** If I identifies a shared variable, and if the variable is read-only, and the reference occurs 
        '   outside the shared constructor of the type in which the variable is declared, then the result is the 
        '   value of the shared variable I in E. Otherwise, the result is the shared variable I in E.
        If Helper.IsFieldDeclaration(first) Then
            Dim var As Mono.Cecil.FieldReference = TryCast(first, Mono.Cecil.FieldReference)
            Dim varD As Mono.Cecil.FieldDefinition = CecilHelper.FindDefinition(var)
            Dim constructor As ConstructorDeclaration = Me.FindFirstParent(Of ConstructorDeclaration)()
            If varD.IsStatic AndAlso varD.IsInitOnly AndAlso _
             (constructor Is Nothing OrElse constructor.Modifiers.Is(ModifierMasks.Shared) = False) Then
                Return New ValueClassification(Me, var, Nothing)
            ElseIf Not varD.IsStatic Then
                Compiler.Report.ShowMessage(Messages.VBNC30469, Me.Location)
                Return Nothing
            Else
                Return New VariableClassification(Me, var, Nothing)
            End If
        End If

        '** If I identifies a shared event, the result is an event access with no associated instance expression.
        If Helper.IsEventDeclaration(first) Then
            Dim red As Mono.Cecil.EventReference = DirectCast(first, Mono.Cecil.EventReference)
            Dim redD As Mono.Cecil.EventDefinition = CecilHelper.FindDefinition(red)
            If redD.AddMethod.IsStatic OrElse redD.RemoveMethod.IsStatic Then
                Return New EventAccessClassification(Me, red, Nothing)
            End If
        End If

        '** If I identifies a constant, then the result is the value of that constant.
        If CecilHelper.GetMemberType(first) = MemberTypes.Field AndAlso CecilHelper.FindDefinition(DirectCast(first, Mono.Cecil.FieldReference)).IsLiteral Then
            Return New ValueClassification(Me, DirectCast(first, Mono.Cecil.FieldReference), Nothing)
        End If

        '** If I identifies an enumeration member, then the result is the value of that enumeration member.
        If Helper.IsEnumFieldDeclaration(Compiler, first) Then
            Return New ValueClassification(Me, DirectCast(first, Mono.Cecil.FieldReference), Nothing)
        End If

        '** Otherwise, E.I is an invalid member reference, and a compile-time error occurs.
        Helper.AddError(Me)

        Return Nothing
    End Function

    Private Function CreateMeExpression() As MeExpression
        Dim result As New MeExpression(Me)
        If result.ResolveExpression(ResolveInfo.Default(Parent.Compiler)) = False Then Throw New InternalException(Me)
        Return result
    End Function

    Private Function GetMeClassification(ByVal members As Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference), ByVal type As IType) As ExpressionClassification
        Dim result As ExpressionClassification
        Dim first As Mono.Cecil.MemberReference = members(0)

        'Otherwise, if the type is the immediately enclosing type and the lookup identifies a non-shared 
        'type member, then the result is the same as a member access of the form Me.E, where E is 
        'the identifier.


        '* If E is classified as a variable or value, the type of which is T, and I is the name of an accessible 
        '  member of E, then E.I is evaluated and classified as follows:

        '** If I is the keyword New and E is an instance expression (Me, MyBase, or MyClass), then the result is 
        '   a method group representing the instance constructors of the type of E with an associated 
        '   instance expression of E and no type argument list. Otherwise, a compile-time error occurs.
        '(not applicable)

        '** If I identifies one or more methods, then the result is a method group with the associated type 
        '   argument list and an associated instance expression of E.
        If Helper.IsMethodDeclaration(first) Then
            result = New MethodGroupClassification(Me, CreateMeExpression, m_TypeArgumentList, Nothing, members)
            Return result
        End If

        '** If I identifies one or more properties, then the result is a property group with an 
        '   associated instance expression of E.
        If Helper.IsPropertyDeclaration(first) Then
            result = New PropertyGroupClassification(Me, CreateMeExpression, members)
            Return result
        End If

        If members.Count > 1 Then
            Compiler.Report.WriteLine("Found " & members.Count & " members for SimpleNameExpression = " & Me.ToString & ", " & Me.Location.ToString(Compiler))
            For i As Integer = 0 To members.Count - 1
                Compiler.Report.WriteLine(">#" & (i + 1).ToString & ".MemberType=" & CecilHelper.GetMemberType(members(i)).ToString & ",DeclaringType=" & members(i).DeclaringType.FullName)
            Next
            Helper.Stop()
        End If

        '** If I identifies a shared variable or an instance variable, and if the variable is read-only, 
        '   and the reference occurs outside a constructor of the class in which the variable is declared 
        '   appropriate for the kind of variable (shared or instance), then the result is the value of the 
        '   variable I in the object referenced by E. 
        '   If T is a reference type, then the result is the variable 
        '   I in the object referenced by E. 
        '   Otherwise, if T is a value type and the expression E is classified 
        '   as a variable, the result is a variable; otherwise the result is a value.
        If Helper.IsFieldDeclaration(first) Then
            Dim var As Mono.Cecil.FieldReference = DirectCast(first, Mono.Cecil.FieldReference)
            Dim varD As Mono.Cecil.FieldDefinition = CecilHelper.FindDefinition(var)
            Helper.Assert(Parent.FindFirstParent(Of EnumDeclaration)() Is Nothing)

            Dim ctorParent As ConstructorDeclaration
            Dim methodParent As IMethod
            Dim typeParent As TypeDeclaration
            Dim isNotInCtorAndReadOnly As Boolean
            ctorParent = FindFirstParent(Of ConstructorDeclaration)()
            methodParent = FindFirstParent(Of IMethod)()
            typeParent = FindFirstParent(Of TypeDeclaration)()

            isNotInCtorAndReadOnly = varD.IsInitOnly AndAlso (ctorParent Is Nothing OrElse ctorParent.Modifiers.Is(ModifierMasks.Shared) <> varD.IsStatic) AndAlso (typeParent Is Nothing OrElse typeParent.IsShared <> varD.IsStatic)

            If isNotInCtorAndReadOnly Then ' >?? (Parent.FindFirstParent(Of IMethod).Modifiers.Is(KS.Shared) <> var.IsStatic) Then
                Return New ValueClassification(Me, var, CreateMeExpression)
            ElseIf TypeOf type Is ClassDeclaration Then
                Return New VariableClassification(Me, var, CreateMeExpression)
            ElseIf TypeOf type Is StructureDeclaration Then
                Return New VariableClassification(Me, var, CreateMeExpression)
            Else
                Throw New InternalException(Me)
            End If
        End If

        '** If I identifies an event, the result is an event access with an associated instance expression of E.
        If Helper.IsEventDeclaration(first) Then
            If TypeOf first Is Mono.Cecil.EventReference Then
                Return New EventAccessClassification(Me, DirectCast(first, Mono.Cecil.EventReference), CreateMeExpression)
            Else
                Throw New InternalException(Me)
            End If
        End If

        '** If I identifies a constant, then the result is the value of that constant.
        If CecilHelper.GetMemberType(first) = MemberTypes.Field AndAlso CecilHelper.FindDefinition(DirectCast(first, Mono.Cecil.FieldReference)).IsLiteral Then
            Return New ValueClassification(Me, DirectCast(first, Mono.Cecil.FieldReference), Nothing)
        End If

        '** If I identifies an enumeration member, then the result is the value of that enumeration member.
        '(not applicable)

        '** If T is Object, then the result is a late-bound member lookup classified as a late-bound access 
        '   with an associated instance expression of E.
        '(not applicable)

        '** Otherwise, E.I is an invalid member reference, and a compile-time error occurs.
        Helper.AddError(Me)

        Return Nothing
    End Function

    Private Function CheckOutermostNamespace(ByVal R As String) As Boolean

        '---------------------------------------------------------------------------------------------------------
        '* For each nested namespace, starting from the innermost and going to the outermost namespace, 
        '  do the following:
        '** If the namespace contains an accessible namespace member with the given name, then the identifier
        '   refers to that member and, depending on the member, is classified as a namespace or a type.
        '** Otherwise, if the namespace contains one or more accessible standard modules, and a member name 
        '   lookup of the identifier produces an accessible match in exactly one standard module, then the 
        '   result is exactly the same as a member access of the form M.E, where M is the standard module 
        '   containing the matching member and E is the identifier. If the identifier matches accessible type 
        '   members in more than one standard module, a compile-time error occurs.
        '**	If R matches the name of an accessible type or nested namespace in the current namespace, then the
        '** unqualified name refers to that type or nested namespace.
        '---------------------------------------------------------------------------------------------------------
        Dim foundNamespace As [Namespace] = Nothing
        Dim foundType As Mono.Cecil.TypeReference

        foundType = Compiler.TypeManager.TypesByNamespace("").Item(R)
        If foundType Is Nothing AndAlso Compiler.Assembly.Name <> "" Then
            foundType = Compiler.TypeManager.TypesByNamespace(Compiler.Assembly.Name).Item(R)
        End If

        foundNamespace = Compiler.TypeManager.Namespaces(R)
        If foundNamespace IsNot Nothing AndAlso foundType Is Nothing Then
            Classification = New NamespaceClassification(Me, foundNamespace)
            Return True
        ElseIf foundNamespace Is Nothing AndAlso foundType IsNot Nothing Then
            Classification = New TypeClassification(Me, foundType)
            Return True
        ElseIf foundNamespace IsNot Nothing AndAlso foundType IsNot Nothing Then
            Return Helper.AddError(Me)
        End If

        If foundNamespace Is Nothing Then Return False

        Dim modules As TypeDictionary
        Dim members As Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference)
        modules = Compiler.TypeManager.GetModulesByNamespace(foundNamespace.ToString)
        members = Helper.GetMembersOfTypes(Compiler, modules, R)
        If members.Count = 1 Then
            Helper.Assert(Helper.IsTypeDeclaration(members(0)))
            Classification = New TypeClassification(Me, members(0))
        ElseIf members.Count > 1 Then
            Return Helper.AddError(Me)
        End If

        Return False
    End Function

    Private Function ResolveAliasImports(ByVal imps As ImportsClauses, ByVal Name As String) As Boolean
        Dim import As ImportsClause = imps.Item(Name)
        Dim nsimport As ImportsNamespaceClause
        If import IsNot Nothing Then
            nsimport = import.AsAliasClause.NamespaceClause
            If nsimport.IsNamespaceImport Then
                Classification = New NamespaceClassification(Me, nsimport.NamespaceImported)
                Return True
            ElseIf nsimport.IsTypeImport Then
                Classification = New TypeClassification(Me, nsimport.TypeImported)
                Return True
            Else
                Throw New InternalException(Me)
            End If
        End If
        Return False
    End Function

    Private Function ResolveImports(ByVal imps As ImportsClauses, ByVal Name As String, ByRef wasError As Boolean) As Boolean
        '---------------------------------------------------------------------------------------------------------
        '* If the (source file / compilation environment) containing the name reference has one or more imports:
        '** If the identifier matches the name of an accessible type or type member in exactly one import, 
        '   then the identifier refers to that type or type member. If the identifier matches the name of 
        '   an accessible type or type member in more than one import, a compile-time error occurs.
        '** If the identifier matches the name of a namespace in exactly one import, then the identifier 
        '   refers to that namespace. If the identifier matches the name of a namespace in more than one import, 
        '   a compile-time error occurs.
        '** Otherwise, if the imports contain one or more accessible standard modules, and a member name 
        '   lookup of the identifier produces an accessible match in exactly one standard module, then 
        '   the result is exactly the same as a member access of the form M.E, where M is the standard 
        '   module containing the matching member and E is the identifier. If the identifier matches 
        '   accessible type members in more than one standard module, a compile-time error occurs.
        '---------------------------------------------------------------------------------------------------------
        Dim impmembers As New Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference)
        Dim result As Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference) = Nothing
        For Each imp As ImportsClause In imps
            If imp.IsNamespaceClause Then
                If imp.AsNamespaceClause.IsNamespaceImport Then
                    'The specified name can only be a type.
                    If Compiler.TypeManager.TypesByNamespace.ContainsKey(imp.AsNamespaceClause.Name) Then
                        result = Compiler.TypeManager.GetTypesByNamespaceAndName(imp.AsNamespaceClause.Name, Name)
                        'Helper.FilterByName(Compiler.TypeManager.TypesByNamespace(imp.AsNamespaceClause.Name).ToTypeList, Name, result)
                    End If
                ElseIf imp.AsNamespaceClause.IsTypeImport Then
                    'result.AddRange(Helper.FilterByName(imp.AsNamespaceClause.TypeImported.GetMembers, Name))
                    'result.AddRange(Compiler.TypeManager.GetCache(imp.AsNamespaceClause.TypeImported).LookupMembersFlattened(Name))
                    result = Compiler.TypeManager.GetCache(imp.AsNamespaceClause.TypeImported).LookupMembersFlattened(Name)
                Else
                    Continue For 'This import was not resolved correctly, so don't use it.
                End If
            End If
            If result IsNot Nothing AndAlso result.Count > 0 Then
                If impmembers.Count > 0 Then
                    Dim lst As New Generic.List(Of String)
                    For Each lst2 As IEnumerable In New IEnumerable() {impmembers, result}
                        For Each memb As MemberReference In lst2
                            Dim membname As String = memb.DeclaringType.FullName
                            If Not lst.Contains(membname) Then lst.Add(memb.DeclaringType.FullName)
                        Next
                    Next
                    wasError = True
                    lst.Reverse()
                    Return Compiler.Report.ShowMessage(Messages.VBNC30561, Me.Location, Name, String.Join(", ", lst.ToArray()))
                End If
                impmembers.AddRange(result)
                result = Nothing
            End If
        Next

        If impmembers.Count > 0 Then
            'If the identifier matches the name of an accessible type or type member in exactly one import, 
            'then the identifier refers to that type or type member. If the identifier matches the name of 
            'an accessible type or type member in more than one import, a compile-time error occurs.
            If Helper.IsMethodDeclaration(impmembers(0)) Then
                Classification = New MethodGroupClassification(Me, Nothing, m_TypeArgumentList, Nothing, impmembers)
                Return True
            End If
            If Helper.IsTypeDeclaration(impmembers(0)) Then
                Classification = New TypeClassification(Me, impmembers(0))
                Return True
            End If
            If Helper.IsFieldDeclaration(impmembers(0)) Then
                Classification = New VariableClassification(Me, DirectCast(impmembers(0), Mono.Cecil.FieldReference), Nothing)
                Return True
            End If
            If Helper.IsPropertyDeclaration(impmembers(0)) Then
                Classification = New PropertyGroupClassification(Me, Nothing, impmembers)
                Return True
            End If
            If Helper.IsEventDeclaration(impmembers(0)) Then
                Classification = New EventAccessClassification(Me, DirectCast(impmembers(0), Mono.Cecil.EventReference), Nothing)
                Return True
            End If
            wasError = True
            Return Compiler.Report.ShowMessage(Messages.VBNC99997, Location)
        End If

        Dim nsmembers As Generic.List(Of [Namespace])
        nsmembers = imps.GetNamespaces(Me, Name)
        If nsmembers.Count = 1 Then
            'If the identifier matches the name of a namespace in exactly one import, then the identifier 
            'refers to that namespace. If the identifier matches the name of a namespace in more than one import, 
            'a compile-time error occurs.
            Classification = New NamespaceClassification(Me, nsmembers(0))
            Return True
        ElseIf nsmembers.Count > 1 Then
            wasError = True
            Return Helper.AddError(Me)
        End If

        'Otherwise, if the imports contain one or more accessible standard modules, and a member name 
        'lookup of the identifier produces an accessible match in exactly one standard module, then 
        'the result is exactly the same as a member access of the form M.E, where M is the standard 
        'module containing the matching member and E is the identifier. If the identifier matches 
        'accessible type members in more than one standard module, a compile-time error occurs.
        Dim modules As TypeList = imps.GetModules(Me)
        Dim found As Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference)
        found = Helper.GetMembersOfTypes(Compiler, modules, Name)
        If SetClassificationOfModuleMembers(found) Then
            Return True
        End If

        Return False
    End Function

    Private Function SetClassificationOfModuleMembers(ByVal found As Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference)) As Boolean
        If found Is Nothing OrElse found.Count <= 0 Then
            Return False
        End If

        If Helper.IsMethodDeclaration(found(0)) Then
            Classification = New MethodGroupClassification(Me, Nothing, m_TypeArgumentList, Nothing, found)
            Return True
        End If
        If Helper.IsPropertyDeclaration(found(0)) Then
            Classification = New PropertyGroupClassification(Me, Nothing, found)
            Return True
        End If
        If found.Count > 1 Then
            Return Helper.AddError(Me)
        End If
        If Helper.IsTypeDeclaration(found(0)) Then
            Classification = New TypeClassification(Me, found(0))
            Return True
        End If
        Dim first As Mono.Cecil.MemberReference = found(0)
        If Helper.IsFieldDeclaration(first) Then
            Dim var As Mono.Cecil.FieldReference = DirectCast(first, Mono.Cecil.FieldReference)
            Helper.Assert(Parent.FindFirstParent(Of EnumDeclaration)() Is Nothing)

            Classification = New VariableClassification(Me, var, Nothing)
            Return True
        End If
        If Helper.IsPropertyDeclaration(first) Then
            Classification = New PropertyGroupClassification(Me, Nothing, found)
            Return True
        End If
        If Helper.IsEventDeclaration(first) Then
            Classification = New EventAccessClassification(Me, DirectCast(first, EventReference))
            Return True
        End If
        Return Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
        Return True
    End Function
End Class

