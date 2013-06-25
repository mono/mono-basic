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

Public Class TypeNameResolutionInfo
    Private m_FoundObjects As New Generic.List(Of Object)

    Private Name As ParsedObject
    Private FromWhere As BaseObject
    Private m_IsImportsResolution As Boolean
    Private m_Qualifier As TypeNameResolutionInfo
    Private m_TypeArgumentCount As Integer
    Private m_IsAttributeTypeName As Boolean


    Property IsAttributeTypeName() As Boolean
        Get
            Return m_IsAttributeTypeName
        End Get
        Set(ByVal value As Boolean)
            m_IsAttributeTypeName = value
        End Set
    End Property

    ''' <summary>
    ''' Returns true if the resolved object is the "Global" keyword.
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    ReadOnly Property IsGlobal() As Boolean
        Get
            Return m_FoundObjects.Count = 1 AndAlso ((TypeOf m_FoundObjects(0) Is Token AndAlso DirectCast(m_FoundObjects(0), Token).Equals(KS.Global)) OrElse (TypeOf m_FoundObjects(0) Is GlobalExpression))
        End Get
    End Property

    Public ReadOnly Property FoundOnlyOneObject() As Boolean
        Get
            Return m_FoundObjects.Count = 1
        End Get
    End Property

    Function FoundAsType() As Mono.Cecil.TypeReference 'Descriptor
        Dim found As Object = FoundObject
        If TypeOf found Is IType Then
            Return DirectCast(found, IType).CecilType
            'ElseIf TypeOf found Is Type Then
            '    Return DirectCast(found, Type)
            'ElseIf TypeOf found Is TypeDescriptor Then
            '    Return DirectCast(found, TypeDescriptor)
        ElseIf TypeOf found Is TypeParameter Then
            Return DirectCast(found, TypeParameter).CecilBuilder
        ElseIf TypeOf found Is Mono.Cecil.TypeReference Then
            Return DirectCast(found, Mono.Cecil.TypeReference)
        Else
            Throw New InternalException("")
        End If
    End Function

    Function FoundIsType() As Boolean
        Dim found As Object = FoundObject
        Return TypeOf found Is IType OrElse TypeOf found Is Type OrElse TypeOf found Is Mono.Cecil.TypeReference
    End Function

    Function FoundIs(Of Type)() As Boolean
        If FoundOnlyOneObject Then
            Return TypeOf FoundObject Is Type
        Else
            Throw New InternalException("")
        End If
    End Function

    Function FoundAs(Of Type)() As Type
        Return CType(CObj(FoundObject), Type)
    End Function

    ReadOnly Property FoundObject() As Object
        Get
            If FoundOnlyOneObject = False Then
                Throw New InternalException("")
            Else
                Return m_FoundObjects(0)
            End If
        End Get
    End Property

    Public ReadOnly Property FoundObjects() As Generic.List(Of Object)
        Get
            Return m_FoundObjects
        End Get
    End Property

    Property IsImportsResolution() As Boolean
        Get
            Return m_IsImportsResolution
        End Get
        Set(ByVal value As Boolean)
            m_IsImportsResolution = value
        End Set
    End Property

    Property TypeArgumentCount() As Integer
        Get
            Return m_TypeArgumentCount
        End Get
        Set(ByVal value As Integer)
            m_TypeArgumentCount = value
        End Set
    End Property

    Sub New(ByVal Name As ConstructedTypeName, ByVal FromWhere As BaseObject, Optional ByVal TypeArgumentCount As Integer = 0)
        Me.Name = Name
        Me.FromWhere = FromWhere
        Me.TypeArgumentCount = TypeArgumentCount
    End Sub

    Sub New(ByVal Name As QualifiedIdentifier, ByVal FromWhere As BaseObject, Optional ByVal TypeArgumentCount As Integer = 0)
        Me.Name = Name
        Me.FromWhere = FromWhere
        Me.TypeArgumentCount = TypeArgumentCount
    End Sub

    Sub New(ByVal Name As Identifier, ByVal FromWhere As BaseObject, Optional ByVal TypeArgumentCount As Integer = 0)
        Me.Name = Name
        Me.FromWhere = FromWhere
        Me.TypeArgumentCount = TypeArgumentCount
    End Sub

    Sub New(ByVal Name As GlobalExpression, ByVal FromWhere As BaseObject, Optional ByVal TypeArgumentCount As Integer = 0)
        Me.Name = Name
        Me.FromWhere = FromWhere
        Me.TypeArgumentCount = TypeArgumentCount
    End Sub

    Function Resolve() As Boolean
        Dim result As Boolean = True
        Dim tmp As TypeNameResolutionInfo

        Dim glob As GlobalExpression = TryCast(Name, GlobalExpression)
        Dim id As Identifier = TryCast(Name, Identifier)
        Dim qi As QualifiedIdentifier = TryCast(Name, QualifiedIdentifier)
        Dim ctn As ConstructedTypeName = TryCast(Name, ConstructedTypeName)

        If ctn IsNot Nothing Then
            qi = ctn.QualifiedIdentifier
            Helper.Assert(TypeArgumentCount > 0)
        End If

        If qi IsNot Nothing Then
            If qi.IsFirstQualifiedIdentifier Then
                If Token.IsSomething(qi.Second) Then
                    tmp = New TypeNameResolutionInfo(qi.FirstAsQualifiedIdentifier, FromWhere, 0)
                Else
                    tmp = New TypeNameResolutionInfo(qi.FirstAsQualifiedIdentifier, FromWhere, Me.TypeArgumentCount)
                    tmp.IsAttributeTypeName = Me.IsAttributeTypeName
                End If
                'Helper.Assert(qi.Second IsNot Nothing) 'A qualified identifier can perfectly be only an identifier
            ElseIf qi.IsFirstGlobal Then
                Helper.Assert(TypeArgumentCount = 0)
                tmp = New TypeNameResolutionInfo(qi.FirstAsGlobal, FromWhere)
                'Helper.Assert(qi.Second IsNot Nothing)
            ElseIf qi.IsFirstIdentifier Then
                If Token.IsSomething(qi.Second) = False Then
                    tmp = New TypeNameResolutionInfo(qi.FirstAsIdentifier, FromWhere, Me.TypeArgumentCount)
                    tmp.IsAttributeTypeName = Me.IsAttributeTypeName
                Else
                    tmp = New TypeNameResolutionInfo(qi.FirstAsIdentifier, FromWhere, 0)
                End If
            Else
                Throw New InternalException(FromWhere)
            End If

            tmp.IsImportsResolution = Me.IsImportsResolution
            result = tmp.Resolve AndAlso result
            If result = False Then Return result

            If Token.IsSomething(qi.Second) = False Then
                Me.m_FoundObjects = tmp.m_FoundObjects
            Else
                If Me.IsAttributeTypeName Then
                    result = ResolveQualifiedName(tmp, qi.Second.IdentiferOrKeywordIdentifier & "Attribute", qi.Second.IdentiferOrKeywordIdentifier, Me.TypeArgumentCount) AndAlso result
                Else
                    result = ResolveQualifiedName(tmp, qi.Second.IdentiferOrKeywordIdentifier, Nothing, Me.TypeArgumentCount) AndAlso result
                End If
            End If
        ElseIf glob IsNot Nothing Then
            m_FoundObjects.Add(glob)
            result = True
        ElseIf id IsNot Nothing Then
            If Me.IsImportsResolution Then
                result = Me.CheckOutermostNamespace(id.Name, Me.TypeArgumentCount) AndAlso result
            Else
                Dim names() As String
                If Me.IsAttributeTypeName Then
                    names = New String() {id.Name & "Attribute", id.Name}
                Else
                    names = New String() {id.Name}
                End If
                result = ResolveUnqualifiedName(names, Me.TypeArgumentCount) AndAlso result
            End If
        ElseIf ctn IsNot Nothing Then
            Return Name.Compiler.Report.ShowMessage(Messages.VBNC99997, Name.Location)
        Else
            Return Name.Compiler.Report.ShowMessage(Messages.VBNC99997, Name.Location)
        End If

        Return result
    End Function

    Private Function ResolveQualifiedName(ByVal Qualifier As TypeNameResolutionInfo, ByVal R1 As String, ByVal R2 As String, ByVal TypeArgumentCount As Integer) As Boolean
        Dim result As Boolean = True

        result = ResolveQualifiedNameInternal(Qualifier, R1, R2 Is Nothing, TypeArgumentCount) AndAlso result

        If result = False AndAlso R2 IsNot Nothing Then
            result = ResolveQualifiedNameInternal(Qualifier, R2, True, TypeArgumentCount) 'AndAlso result
        End If

        Return result
    End Function

    Private Function ResolveQualifiedNameInternal(ByVal Qualifier As TypeNameResolutionInfo, ByVal R As String, ByVal ShowError As Boolean, ByVal TypeArgumentCount As Integer) As Boolean
        '---------------------------------------------------------------------------------------------------------
        '*************************************** Qualified Name Resolution '**************************************
        '---------------------------------------------------------------------------------------------------------
        '* Given a qualified namespace or type name of the form N.R, where R is the rightmost identifier in the 
        '* qualified name, the following steps describe how to determine to which namespace or type the qualified 
        '* Name(refers)
        '**	Resolve N, which may be either a qualified or unqualified name.
        '**	If resolution of N fails, resolves to a type parameter, or does not resolve to a namespace or type, a
        '**  compile-time error occurs. If R matches the name of a namespace or type in N, then the qualified name 
        '** refers to that namespace or type.
        '**	If N contains one or more standard modules, and R matches the name of a type in exactly one standard 
        '** module, then the qualified name refers to that type. If R matches the name of types in more than one 
        '** standard module, a compile-time error occurs.
        '**	Otherwise, a compile-time error occurs. 
        ' * Note   An implication of this resolution process is that type members do not shadow namespaces or types 
        ' * when resolving namespace or type names.
        '---------------------------------------------------------------------------------------------------------

        Me.m_Qualifier = Qualifier

        If Qualifier.FoundOnlyOneObject Then
            Dim modules As TypeList = Nothing
            If Qualifier.IsGlobal Then
                'Helper.NotImplemented()
                If CheckOutermostNamespace(R, TypeArgumentCount) Then Return True
            ElseIf Qualifier.FoundIs(Of [Namespace])() OrElse Qualifier.FoundIs(Of ImportsClause)() Then
                'Helper.NotImplemented()
                '** If R matches the name of a namespace or type in N, 
                '** then the qualified name refers to that namespace or type.
                Dim strNS As String

                If Qualifier.FoundIs(Of [Namespace])() Then
                    Dim ns As [Namespace]
                    ns = Qualifier.FoundAs(Of [Namespace])()
                    strNS = ns.FullName
                ElseIf Qualifier.FoundIs(Of ImportsClause)() Then
                    Dim ic As ImportsClause
                    ic = Qualifier.FoundAs(Of ImportsClause)()
                    If ic.IsAliasClause Then
                        Dim ac As ImportsAliasClause = ic.AsAliasClause
                        If ac.NamespaceClause.IsNamespaceImport Then
                            Dim ns As [Namespace]
                            ns = ac.NamespaceClause.NamespaceImported
                            strNS = ns.FullName
                        ElseIf ac.NamespaceClause.IsTypeImport Then
                            strNS = ac.NamespaceClause.TypeImported.FullName
                        Else
                            Throw New InternalException(FromWhere)
                        End If
                    Else
                        Throw New InternalException(FromWhere)
                    End If
                Else
                    Throw New InternalException(FromWhere)
                End If

                Dim nsp As [Namespace]
                nsp = FromWhere.Compiler.TypeManager.Namespaces.FindNamespace(strNS, R)
                If nsp IsNot Nothing Then
                    m_FoundObjects.Add(nsp)
                    Return True
                End If

                Dim types As TypeDictionary
                types = FromWhere.Compiler.TypeManager.TypesByNamespace(strNS)
                If types IsNot Nothing AndAlso types.Count > 0 Then
                    Dim genericName As String
                    genericName = vbnc.Helper.CreateGenericTypename(R, TypeArgumentCount)

                    'Dim typesByName As Generic.List(Of Mono.Cecil.TypeReference) = Nothing
                    'Dim tmp As Boolean
                    'tmp = FromWhere.Compiler.TypeManager.TypesByName.TryGetValue(genericName, typesByName)

                    For Each tp As Mono.Cecil.TypeReference In types.Values
                        'If TypeOf tp Is TypeDescriptor Then
                        If Helper.CompareName(tp.Name, genericName) Then
                            m_FoundObjects.Add(tp)
                        End If
                        'Else
                        'If typesByName IsNot Nothing AndAlso typesByName.Contains(tp) Then
                        '    m_FoundObjects.Add(tp)
                        'End If
                        'End If
                    Next
                    'Return True
                End If

                '**	If N contains one or more standard modules, and R matches the name of a type in 
                '** exactly one standard module, then the qualified name refers to that type. If R 
                '** matches the name of types in more than one standard module, a compile-time error occurs.
                If m_FoundObjects.Count = 0 Then
                    Dim dic As TypeDictionary = FromWhere.Compiler.TypeManager.GetModulesByNamespace(strNS)
                    If dic IsNot Nothing Then
                        modules = dic.ToTypeList
                    Else
                        modules = Nothing
                    End If
                    'Return True
                End If

                'Throw New InternalException("(1) Could not resolve: " & R & ", strNS:" & strNS & ", stCombinedNs: " & ", Location: " & FromWhere.Location.AsString)

                'Return False
            ElseIf Qualifier.FoundIs(Of IType)() Then
                '** If R matches the name of a namespace or type in N, 
                '** then the qualified name refers to that namespace or type.
                Dim tp As IType = Qualifier.FoundAs(Of IType)()
                Dim types As Generic.List(Of IType) = tp.Members.GetSpecificMembers(Of IType)()
                For Each t As IType In types
                    If Helper.CompareName(t.Name, R) Then
                        m_FoundObjects.Add(t)
                    End If
                Next

                '**	If N contains one or more standard modules, and R matches the name of a type in 
                '** exactly one standard module, then the qualified name refers to that type. If R 
                '** matches the name of types in more than one standard module, a compile-time error occurs.
                If m_FoundObjects.Count = 0 Then
                    modules = Helper.CreateList(tp.Members.GetSpecificMembers(Of ModuleDeclaration)())
                End If
            ElseIf Qualifier.FoundIs(Of Mono.Cecil.TypeReference)() Then
                '** If R matches the name of a namespace or type in N, 
                '** then the qualified name refers to that namespace or type.
                Dim tp As Mono.Cecil.TypeReference = Qualifier.FoundAs(Of Mono.Cecil.TypeReference)()
                Dim nestedtp As Mono.Cecil.TypeReference = CecilHelper.GetNestedType(tp, Helper.CreateGenericTypename(R, TypeArgumentCount))

                If nestedtp IsNot Nothing Then m_FoundObjects.Add(nestedtp)

                '**	If N contains one or more standard modules, and R matches the name of a type in 
                '** exactly one standard module, then the qualified name refers to that type. If R 
                '** matches the name of types in more than one standard module, a compile-time error occurs.
                If m_FoundObjects.Count = 0 Then
                    Return Name.Compiler.Report.ShowMessage(Messages.VBNC30002, Name.Location, tp.FullName.Replace("/"c, "."c) & "." & R)
                End If
            Else
                '**	If resolution of N fails, resolves to a type parameter, or does not resolve to a namespace 
                '** or type, a compile-time error occurs.  (..)
                If ShowError = False Then Return False
                'Helper.AddError("(2) Could not resolve: " & R & ", Location: " & FromWhere.Location.AsString)
                Return Name.Compiler.Report.ShowMessage(Messages.VBNC99997, Name.Location)
            End If

            '**	If N contains one or more standard modules, and R matches the name of a type in 
            '** exactly one standard module, then the qualified name refers to that type. If R 
            '** matches the name of types in more than one standard module, a compile-time error occurs.

            If modules IsNot Nothing AndAlso modules.Count > 0 AndAlso CheckModules(modules, R, TypeArgumentCount) Then Return True

            If m_FoundObjects.Count = 0 Then
                If ShowError = False Then Return False

                If Not IsImportsResolution Then FromWhere.Compiler.Report.ShowMessage(Messages.VBNC30456, FromWhere.Location, R, Qualifier.FoundObject.ToString)
                Return False
            ElseIf m_FoundObjects.Count > 1 Then
                If ShowError = False Then Return False
                Helper.AddError(Name, "Found " & m_FoundObjects.Count.ToString & " members in type or namespace '" & Qualifier.FoundObject.ToString & "'")
                Return False
            Else
                Return True
            End If
        Else
            '**	If resolution of N fails, (...)
            If ShowError = False Then Return False
            Helper.AddError(Name, "Qualifying member '" & Qualifier.m_Qualifier.FoundObject.ToString & "' resolves to '" & Qualifier.FoundObjects.Count.ToString & " objects" & "(R = " & R & ")")
            Return False
        End If

        Return False
    End Function

    Private Function CheckCurrentFunctionForTypeParameters(ByVal R As String, ByVal TypeArgumentCount As Integer) As Boolean
        Dim signature As SubSignature = Nothing
        Dim method As MethodBaseDeclaration = FromWhere.FindFirstParent(Of MethodBaseDeclaration)()

        If method IsNot Nothing Then
            signature = method.Signature
        Else
            Dim del As DelegateDeclaration = FromWhere.FindFirstParent(Of DelegateDeclaration)()
            If del IsNot Nothing Then
                signature = del.Signature
            End If
        End If

        If signature IsNot Nothing AndAlso signature.TypeParameters IsNot Nothing Then
            Dim item As TypeParameter = signature.TypeParameters.Parameters.Item(R)
            If item IsNot Nothing Then
                m_FoundObjects.Add(item)
                Return True
            End If
        End If

        Return False
    End Function

    Private Function CheckNestedTypesOrTypeParameters(ByVal R As String, ByVal TypeArgumentCount As Integer) As Boolean
        '---------------------------------------------------------------------------------------------------------
        '* For each nested type containing the name reference, starting from the innermost type and going to the
        '* outermost, if R matches the name of an accessible nested type or a type parameter in the current type, 
        '* then the unqualified name refers to that type or type parameter.
        '---------------------------------------------------------------------------------------------------------

        'SPEC OMISSION:
        'Spec does not say anything about type parameters declared in a method.
        If CheckCurrentFunctionForTypeParameters(R, TypeArgumentCount) Then Return True


        Dim tp As IType = FromWhere.FindFirstParent(Of IType)()
        Dim obj As BaseObject = FromWhere
        Do
            tp = obj.FindFirstParent(Of IType)()
            If tp Is Nothing Then Exit Do
            obj = DirectCast(tp, BaseObject)

            'First check if there are nested types with the corresponding name.
            Dim nestedName As String = Helper.CreateGenericTypename(R, TypeArgumentCount)
            Dim nestedType As TypeDefinition = tp.CecilType

            Do
                For i As Integer = 0 To nestedType.NestedTypes.Count - 1
                    If Helper.CompareName(nestedType.NestedTypes(i).Name, nestedName) Then
                        m_FoundObjects.Add(nestedType.NestedTypes(i))
                        Return True 'There can only be one nested type with the same name
                    End If
                Next
                nestedType = CecilHelper.FindDefinition(nestedType.BaseType)
            Loop While nestedType IsNot Nothing

            'Then check if there are type parameters with the corresponding name
            'in the type (only if the current type is a class or a structure)
            Dim tpConstructable As IConstructable = TryCast(tp, IConstructable)
            If tpConstructable IsNot Nothing AndAlso tpConstructable.TypeParameters IsNot Nothing Then
                Dim param As TypeParameter
                param = tpConstructable.TypeParameters.Parameters.Item(R)
                If param IsNot Nothing Then
                    m_FoundObjects.Add(param)
                    Return True
                End If
            End If
        Loop

        Return False
    End Function

    Private Function CheckOutermostNamespace(ByVal R As String, ByVal TypeArgumentCount As Integer) As Boolean
        '---------------------------------------------------------------------------------------------------------
        '* (...)
        '**	If R matches the name of an accessible type or nested namespace in the current namespace, then the
        '** unqualified name refers to that type or nested namespace.
        '*
        '**	If the namespace contains one or more accessible standard modules, and R matches the name of an 
        '** accessible nested type in exactly one standard module, then the unqualified name refers to that nested type
        '*
        '** If R matches the name of accessible nested types in more than one standard module, a compile-time 
        '** error occurs.
        '--------------------------------------------------------------------------------------------------------- 

        '**	If R matches the name of an accessible type or nested namespace in the current namespace, then the
        '** unqualified name refers to that type or nested namespace.
        Dim types As TypeDictionary = Nothing
        Dim modules As TypeList
        Dim foundType As Mono.Cecil.TypeReference

        Dim RName As String = Helper.CreateGenericTypename(R, TypeArgumentCount)
        foundType = FromWhere.Compiler.TypeManager.TypesByNamespace("").Item(RName)
        If foundType IsNot Nothing Then
            m_FoundObjects.Add(foundType)
        End If
        If TypeArgumentCount = 0 AndAlso FromWhere.Compiler.TypeManager.Namespaces.IsNamespace(R, True) Then
            m_FoundObjects.Add(FromWhere.Compiler.TypeManager.Namespaces.Item(R))
        End If

        If m_FoundObjects.Count > 0 Then Return True

        '**	If the namespace contains one or more accessible standard modules, and R matches the name of an 
        '** accessible nested type in exactly one standard module, then the unqualified name refers to that nested type

        If types Is Nothing Then Return False 'There are no types (nor modules) in the outermost namespace.

        modules = FromWhere.Compiler.TypeManager.GetModulesByNamespace("").ToTypeList

        If CheckModules(modules, R, TypeArgumentCount) Then Return True

        Return False
    End Function

    Private Function CheckModules(ByVal moduletypes As TypeList, ByVal R As String, ByVal TypeArgumentCount As Integer) As Boolean
        '**	(...), and R matches the name of an 
        '** accessible nested type in exactly one standard module, (...)
#If DEBUG Then
        For Each t As Mono.Cecil.TypeReference In moduletypes
            Helper.Assert(Helper.IsModule(FromWhere.Compiler, t))
        Next
#End If
        Dim allModuleTypes As New Generic.List(Of Mono.Cecil.TypeReference) 'Descriptor)

        For Each t As Mono.Cecil.TypeReference In moduletypes
            Dim tFound As Mono.Cecil.TypeReference
            tFound = CecilHelper.GetNestedType(t, R)
            If tFound IsNot Nothing Then
                allModuleTypes.Add(tFound)
            End If
        Next

        If allModuleTypes.Count = 1 Then
            m_FoundObjects.Add(allModuleTypes.Item(0))
            Return True
        ElseIf allModuleTypes.Count > 1 Then
            '** If R matches the name of accessible nested types in more than one standard module, a compile-time 
            '** error occurs.
            Helper.AddError(Name)
            Return False
        End If

        Return False
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="R"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckNamespace(ByVal R As String, ByVal Types As TypeDictionary, ByVal TypeArgumentCount As Integer) As Boolean
        '---------------------------------------------------------------------------------------------------------
        '* (...)
        '**	If R matches the name of an accessible type or nested namespace in the current namespace, then the
        '** unqualified name refers to that type or nested namespace.
        '*
        '**	If the namespace contains one or more accessible standard modules, and R matches the name of an 
        '** accessible nested type in exactly one standard module, then the unqualified name refers to that nested type
        '*
        '** If R matches the name of accessible nested types in more than one standard module, a compile-time 
        '** error occurs.
        '---------------------------------------------------------------------------------------------------------

        '**	If R matches the name of an accessible type or nested namespace in the current namespace, then the
        '** unqualified name refers to that type or nested namespace.
        Dim RName As String = vbnc.Helper.CreateGenericTypename(R, TypeArgumentCount)

        Dim foundType As Mono.Cecil.TypeReference

        foundType = Types.Item(RName)
        If foundType IsNot Nothing Then
            m_FoundObjects.Add(foundType)
            Return True
        End If

        '**	If the namespace contains one or more accessible standard modules, and R matches the name of an 
        '** accessible nested type in exactly one standard module, then the unqualified name refers to that nested type
        Dim foundModules As Generic.List(Of Mono.Cecil.TypeReference)
        foundModules = Helper.FilterToModules(FromWhere.Compiler, Types)
        If foundModules.Count > 0 Then
            Dim typesInAllModules As New Generic.List(Of Mono.Cecil.TypeReference)
            For Each [module] As Mono.Cecil.TypeReference In foundModules
                Dim typeInCurrentModule As Mono.Cecil.TypeReference
                typeInCurrentModule = CecilHelper.GetNestedType([module], RName)
                If typeInCurrentModule IsNot Nothing Then typesInAllModules.Add(typeInCurrentModule)
            Next
            If typesInAllModules.Count = 1 Then
                m_FoundObjects.AddRange(typesInAllModules.ToArray)
                Return True
            ElseIf typesInAllModules.Count > 1 Then
                '** If R matches the name of accessible nested types in more than one standard module, a compile-time 
                '** error occurs.
                Helper.AddError(Name)
                Return False
            End If
        End If

        Return False
    End Function

    Private Function CheckNamespaces(ByVal R As String, ByVal TypeArgumentCount As Integer) As Boolean
        '---------------------------------------------------------------------------------------------------------
        '* For each nested namespace containing the name reference, starting from the innermost namespace and 
        '* going to the outermost namespace, do the following:
        '** (...)
        '---------------------------------------------------------------------------------------------------------
        'Check all the namespaces up to the outermost namespace (not including)
        Dim declaringtype As TypeDeclaration = FromWhere.FindFirstParent(Of TypeDeclaration)()
        Dim ns As String
        If declaringtype IsNot Nothing Then
            ns = declaringtype.Namespace
        Else
            ns = FromWhere.Compiler.CommandLine.RootNamespace
        End If
        If ns Is Nothing Then ns = String.Empty
        Dim current As BaseObject = FromWhere
        'Dim dotR As String = "." & R
        'Dim nsDotR As String = ns & dotR
        Do
            If CheckNamespace(R, FromWhere.Compiler.TypeManager.GetTypesByNamespace(ns), TypeArgumentCount) Then Return True

            If ns.Length > R.Length + 1 AndAlso ns.EndsWith(R, Helper.StringComparison) AndAlso ns(ns.Length - R.Length - 1) = "."c Then
                m_FoundObjects.Add(FromWhere.Compiler.TypeManager.Namespaces(ns))
                Return True
            End If

            If ns <> String.Empty AndAlso TypeArgumentCount = 0 Then
                Dim nSpace As [Namespace]
                nSpace = FromWhere.Compiler.TypeManager.Namespaces.FindNamespace(ns, R)
                If nSpace IsNot Nothing Then
                    m_FoundObjects.Add(nSpace)
                    Return True
                End If
            End If

            If Helper.CompareName(ns, R) Then
                m_FoundObjects.Add(FromWhere.Compiler.TypeManager.Namespaces(ns))
                Return True
            End If
            ns = vbnc.Helper.GetNamespaceParent(ns)
            'nsDotR = ns & dotR
        Loop Until ns Is Nothing

        'Check the outermost namespace
        'First the current compiling outermost namespace
        If CheckNamespace(R, FromWhere.Compiler.TypeManager.GetTypesByNamespace(String.Empty), TypeArgumentCount) Then Return True

        'then all the namespaces in the referenced assemblies
        Return CheckOutermostNamespace(R, TypeArgumentCount)
    End Function

    Private Function CheckImportsAlias(ByVal R As String, ByVal [Imports] As ImportsClauses, ByVal TypeArgumentCount As Integer) As Boolean
        '---------------------------------------------------------------------------------------------------------
        '* If the source file has one or more import aliases, and R matches the name of one of them, then 
        '* the unqualified name refers to that import alias.
        '---------------------------------------------------------------------------------------------------------
        ' (...)
        '---------------------------------------------------------------------------------------------------------
        '* If the compilation environment defines one or more import aliases, and R matches the name of one of 
        '* them, then the unqualified name refers to that import alias.
        '---------------------------------------------------------------------------------------------------------
        For Each import As ImportsClause In [Imports]
            If import.IsAliasClause Then
                If Helper.CompareName(import.AsAliasClause.Name, R) Then
                    m_FoundObjects.Add(import)
                    Return True
                End If
            End If
        Next
        Return False
    End Function

    Private Function CheckImports(ByVal R As String, ByVal [Imports] As ImportsClauses, ByVal TypeArgumentCount As Integer, ByRef wasError As Boolean) As Boolean
        '---------------------------------------------------------------------------------------------------------
        '*	If the source file containing the name reference has one or more imports:
        '**	If R matches the name of an accessible type in exactly one import, then the unqualified name refers to 
        '** that type. If R matches the name of an accessible type in more than one import and all are not the 
        '** same entity, a compile-time error occurs.
        '**	If R matches the name of a namespace in exactly one import, then the unqualified name refers to that
        '** namespace. If R matches the name of a namespace in more than one import and all are not the same entity, a 
        '** compile-time error occurs.
        '**	If the imports contain one or more accessible standard modules, and R matches the name of an accessible 
        '** nested type in exactly one standard module, then the unqualified name refers to that type. If R matches 
        '** the name of accessible nested types in more than one standard module, a compile-time error occurs.
        '---------------------------------------------------------------------------------------------------------
        ' (...)
        '---------------------------------------------------------------------------------------------------------
        '* If the compilation environment defines one or more imports:
        '**	If R matches the name of an accessible type in exactly one import, then the unqualified name refers to 
        '** that type. If R matches the name of an accessible type in more than one import, a compile-time error 
        '** occurs.
        '**	If R matches the name of a namespace in exactly one import, then the unqualified name refers to that
        '** namespace. If R matches the name of a namespace in more than one import, a compile-time error occurs.
        '**	If the imports contain one or more accessible standard modules, and R matches the name of an accessible
        '** nested type in exactly one standard module, then the unqualified name refers to that type. If R matches the       
        '** name of accessible nested types in more than one standard module, a compile-time error occurs.
        '---------------------------------------------------------------------------------------------------------
        Dim nsclauses As New Generic.List(Of ImportsNamespaceClause)
        For Each imp As ImportsClause In [Imports]
            If imp.IsNamespaceClause Then nsclauses.Add(imp.AsNamespaceClause)
        Next

        Dim tpFound As New Generic.List(Of Object)
        Dim genericR As String = Helper.CreateGenericTypename(R, TypeArgumentCount)
        '**	If R matches the name of an accessible type in exactly one import, then the unqualified name refers to 
        '** that type. If R matches the name of an accessible type in more than one import, a compile-time error 
        '** occurs.

        For Each nsimp As ImportsNamespaceClause In nsclauses
            If nsimp.IsTypeImport Then
                Dim tp As Mono.Cecil.TypeReference
                tp = CecilHelper.GetNestedType(nsimp.TypeImported, genericR)
                If tp IsNot Nothing Then tpFound.Add(tp)
            ElseIf nsimp.IsNamespaceImport Then
                Dim nsName As String = nsimp.NamespaceImported.FullName
                If FromWhere.Compiler.TypeManager.TypesByNamespace.ContainsKey(nsName) Then
                    Dim foundType As Mono.Cecil.TypeReference
                    foundType = FromWhere.Compiler.TypeManager.TypesByNamespace(nsName).Item(genericR)
                    If foundType IsNot Nothing Then tpFound.Add(foundType)
                End If
            Else
                Continue For
            End If
        Next
        If tpFound.Count = 1 Then
            m_FoundObjects.Add(tpFound(0))
            Return True
        ElseIf tpFound.Count > 0 Then
            Dim lst As New Generic.List(Of String)
            For Each found As Object In tpFound
                Dim m As MemberReference = TryCast(found, MemberReference)
                If m IsNot Nothing Then
                    lst.Add(m.DeclaringType.FullName)
                Else
                    lst.Add(found.ToString())
                End If
            Next
            lst.Reverse()
            wasError = True
            Return FromWhere.Compiler.Report.ShowMessage(Messages.VBNC30561, FromWhere.Location, R, String.Join(", ", lst.ToArray()))
        End If

        '**	If R matches the name of a namespace in exactly one import, then the unqualified name refers to that
        '** namespace. If R matches the name of a namespace in more than one import, a compile-time error occurs.
        'Helper.Stop()
        For Each nsimp As ImportsNamespaceClause In nsclauses
            If nsimp.IsNamespaceImport Then
                Dim nsName As String = nsimp.NamespaceImported.FullName
                Dim nsCombined As String = String.Concat(nsName, ".", Helper.CreateGenericTypename(R, TypeArgumentCount))
                If FromWhere.Compiler.TypeManager.Namespaces.ContainsKey(nsCombined) Then
                    tpFound.Add(FromWhere.Compiler.TypeManager.Namespaces(nsCombined))
                End If
            ElseIf nsimp.IsTypeImport Then
                'Skip this
            Else
                Continue For
            End If
        Next
        If tpFound.Count = 1 Then
            m_FoundObjects.Add(tpFound(0))
            Return True
        ElseIf tpFound.Count > 0 Then
            wasError = True
            Helper.AddError(Name)
            Return False
        End If

        '**	If the imports contain one or more accessible standard modules, and R matches the name of an accessible
        '** nested type in exactly one standard module, then the unqualified name refers to that type. If R matches the                 '** name of accessible nested types in more than one standard module, a compile-time error occurs.
        'Helper.Stop()
        Dim modules As New TypeList
        For Each nsimp As ImportsNamespaceClause In nsclauses
            If nsimp.IsTypeImport Then
                Dim tp As Mono.Cecil.TypeReference
                tp = CecilHelper.GetNestedType(nsimp.TypeImported, genericR)
                If tp IsNot Nothing AndAlso Helper.IsModule(FromWhere.Compiler, tp) Then modules.Add(tp)
            ElseIf nsimp.IsNamespaceImport Then
                Dim nsName As String = nsimp.NamespaceImported.FullName
                Dim importedModules As TypeDictionary = FromWhere.Compiler.TypeManager.GetModulesByNamespace(nsName)
                If importedModules IsNot Nothing Then modules.AddRange(importedModules.ToTypeList)
            Else
                Continue For
            End If
        Next
        If CheckModules(modules, R, TypeArgumentCount) Then Return True

        Return False
    End Function

    Private Function ResolveUnqualifiedName(ByVal Rs As String(), ByVal TypeArgumentCount As Integer) As Boolean
        Dim wasError As Boolean

        For Each R As String In Rs
            '---------------------------------------------------------------------------------------------------------
            ' Given an unqualified name R, the following steps describe how to determine to which namespace or type an 
            ' unqualified name refers:
            '---------------------------------------------------------------------------------------------------------

            '---------------------------------------------------------------------------------------------------------
            '* For each nested type containing the name reference, starting from the innermost type and going to the
            '* outermost, if R matches the name of an accessible nested type or a type parameter in the current type, 
            '* then the unqualified name refers to that type or type parameter.
            '---------------------------------------------------------------------------------------------------------
            If CheckNestedTypesOrTypeParameters(R, TypeArgumentCount) Then Return True

            '---------------------------------------------------------------------------------------------------------
            '* For each nested namespace containing the name reference, starting from the innermost namespace and 
            '* going to the outermost namespace, do the following:
            '*
            '**	If R matches the name of an accessible type or nested namespace in the current namespace, then the
            '** unqualified name refers to that type or nested namespace.
            '*
            '**	If the namespace contains one or more accessible standard modules, and R matches the name of an 
            '** accessible nested type in exactly one standard module, then the unqualified name refers to that nested type
            '*
            '** If R matches the name of accessible nested types in more than one standard module, a compile-time 
            '** error occurs.
            '---------------------------------------------------------------------------------------------------------
            If CheckNamespaces(R, TypeArgumentCount) Then Return True

            '---------------------------------------------------------------------------------------------------------
            '* If the source file has one or more import aliases, and R matches the name of one of them, then 
            '* the unqualified name refers to that import alias.
            '---------------------------------------------------------------------------------------------------------
            Helper.Assert(FromWhere IsNot Nothing)
            'Helper.Assert(FromWhere.HasLocation)
            Helper.Assert(FromWhere.File IsNot Nothing)
            Helper.Assert(FromWhere.File.Imports IsNot Nothing)

            If CheckImportsAlias(R, FromWhere.File.Imports, TypeArgumentCount) Then Return True

            '---------------------------------------------------------------------------------------------------------
            '*	If the source file containing the name reference has one or more imports:
            '**	If R matches the name of an accessible type in exactly one import, then the unqualified name refers to 
            '** that type. If R matches the name of an accessible type in more than one import and all are not the 
            '** same entity, a compile-time error occurs.
            '**	If R matches the name of a namespace in exactly one import, then the unqualified name refers to that
            '** namespace. If R matches the name of a namespace in more than one import and all are not the same entity, a 
            '** compile-time error occurs.
            '**	If the imports contain one or more accessible standard modules, and R matches the name of an accessible 
            '** nested type in exactly one standard module, then the unqualified name refers to that type. If R matches 
            '** the name of accessible nested types in more than one standard module, a compile-time error occurs.
            '---------------------------------------------------------------------------------------------------------
            If CheckImports(R, FromWhere.File.Imports, TypeArgumentCount, wasError) Then
                Return True
            ElseIf wasError Then
                Return False
            End If

            '---------------------------------------------------------------------------------------------------------
            '* If the compilation environment defines one or more import aliases, and R matches the name of one of 
            '* them, then the unqualified name refers to that import alias.
            '---------------------------------------------------------------------------------------------------------
            If CheckImportsAlias(R, FromWhere.Compiler.CommandLine.Imports.Clauses, TypeArgumentCount) Then Return True

            '---------------------------------------------------------------------------------------------------------
            '* If the compilation environment defines one or more imports:
            '**	If R matches the name of an accessible type in exactly one import, then the unqualified name refers to 
            '** that type. If R matches the name of an accessible type in more than one import, a compile-time error 
            '** occurs.
            '**	If R matches the name of a namespace in exactly one import, then the unqualified name refers to that
            '** namespace. If R matches the name of a namespace in more than one import, a compile-time error occurs.
            '**	If the imports contain one or more accessible standard modules, and R matches the name of an accessible
            '** nested type in exactly one standard module, then the unqualified name refers to that type. If R matches the                 
            '** name of accessible nested types in more than one standard module, a compile-time error occurs.
            '---------------------------------------------------------------------------------------------------------
            If CheckImports(R, FromWhere.Compiler.CommandLine.Imports.Clauses, TypeArgumentCount, wasError) Then
                Return True
            ElseIf wasError Then
                Return False
            End If
        Next
        '---------------------------------------------------------------------------------------------------------
        '* Otherwise, a compile-time error occurs.
        '---------------------------------------------------------------------------------------------------------

        FromWhere.Compiler.Report.ShowMessage(Messages.VBNC30451, FromWhere.Location, Rs(0))

        '---------------------------------------------------------------------------------------------------------
        '* Note   
        '* An implication of this resolution process is that type members do not shadow namespaces or types 
        '* when resolving namespace or type names.
        '* If the type name is a constructed type name (i.e. it includes a type argument list), then only types 
        '* with the same arity as the type argument list are matched.
        '---------------------------------------------------------------------------------------------------------
        Return False
    End Function
End Class
