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
''' MemberAccessExpression ::= [ [ MemberAccessBase ] "." ] IdentifierOrKeyword
''' MemberAccessBase ::= Expression | BuiltInTypeName | "Global" | "MyClass" | "MyBase"
''' 
''' TODO: Is this correct? Is the "." optional in a MemberAccessExpression?
''' LAMESPEC: IdentifierOrKeyword should be followed by type parameters...
''' MemberAccessExpression ::= [ [ MemberAccessBase ] "." ] IdentifierOrKeyword [ TypeParametersList ]
''' </summary>
''' <remarks></remarks>
Public Class MemberAccessExpression
    Inherits Expression

    Protected m_First As Expression
    Protected m_Second As IdentifierOrKeyword
    'Private m_TypeArguments As TypeParameters

    Private m_WithStatement As WithStatement

    Public Overrides ReadOnly Property AsString() As String
        Get
            If m_First IsNot Nothing Then
                Return m_First.AsString & "." & m_Second.Identifier
            Else
                Return "." & m_Second.Identifier
            End If
        End Get
    End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        If m_First IsNot Nothing Then result = m_First.ResolveTypeReferences AndAlso result
        If m_Second IsNot Nothing Then result = m_Second.ResolveTypeReferences AndAlso result

        ' If m_TypeArguments IsNot Nothing Then result = m_TypeArguments.ResolveTypeReferences AndAlso result

        Return result
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal First As Expression, ByVal Second As IdentifierOrKeyword)
        m_First = First
        m_Second = Second
    End Sub

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Select Case Classification.Classification
            Case ExpressionClassification.Classifications.MethodGroup
                If m_First Is Nothing Then
                    Return Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
                Else
                    With Classification.AsMethodGroupClassification
                        If Info.IsRHS Then
                            Dim tmp As ValueClassification = .ReclassifyToValue
                            result = tmp.GenerateCode(Info) AndAlso result
                        Else
                            Return Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
                        End If
                    End With
                End If
            Case ExpressionClassification.Classifications.Variable
                If m_First Is Nothing Then
                    Return Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
                Else
                    With Classification.AsVariableClassification
                        result = .GenerateCode(Info) AndAlso result
                    End With
                End If
            Case ExpressionClassification.Classifications.Value
                With Classification.AsValueClassification
                    result = .GenerateCode(Info) AndAlso result
                End With
            Case ExpressionClassification.Classifications.PropertyGroup
                With Classification.AsPropertyGroup
                    If Info.IsRHS Then
                        Dim tmp As ValueClassification = .ReclassifyToValue
                        result = tmp.GenerateCode(Info) AndAlso result
                    Else
                        Return Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
                    End If
                End With
            Case ExpressionClassification.Classifications.LateBoundAccess
                If Info.IsLHS Then
                    If Info.RHSExpression Is Nothing Then
                        LateBoundAccessToExpression.EmitLateCall(Info, Me.Classification.AsLateBoundAccess)
                    Else
                        Return Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
                    End If
                Else
                    Return Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
                End If
            Case Else
                Return Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
        End Select

        Return result
    End Function

    Overrides ReadOnly Property ExpressionType() As Mono.Cecil.TypeReference
        Get
            Return Classification.GetType(True)
        End Get
    End Property

    Public Overrides Function GetConstant(ByRef result As Object, ByVal ShowError As Boolean) As Boolean
        Return Me.Classification.GetConstant(result, ShowError)
    End Function

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        '---------------------------------------------------------------------------------------------------------
        'A member access expression is used to access a member of an entity. A member access of the form E.I, 
        'where E is an expression, a built-in type, the keyword Global, or omitted and I is an identifier with an 
        'optional type argument list, is evaluated and classified as follows:
        '---------------------------------------------------------------------------------------------------------
        '* If E is omitted, then the expression from the immediately containing With statement is substituted for 
        '  E and the member access is performed. If there is no containing With statement, a compile-time 
        '  error occurs.
        '---------------------------------------------------------------------------------------------------------
        '* If E is a type parameter, then a compile-time error results.
        '---------------------------------------------------------------------------------------------------------
        '* If E is the keyword Global, and I is the name of an accessible type in the global namespace, 
        '  then the result is that type.
        '---------------------------------------------------------------------------------------------------------
        '* If E is classified as a namespace and I is the name of an accessible member of that namespace, 
        '  then the result is that member. The result is classified as a namespace or a type depending on the member.
        '---------------------------------------------------------------------------------------------------------
        '* If E is a built-in type or an expression classified as a type, and I is the name of an accessible 
        '  member of E, then E.I is evaluated and classified as follows:
        '** If I is the keyword New, then a compile-time error occurs.
        '** If I identifies a type, then the result is that type.
        '** If I identifies one or more methods, then the result is a method group with the associated 
        '   type argument list and no associated instance expression.
        '** If I identifies one or more properties, then the result is a property group with no associated 
        '   instance expression.
        '** If I identifies a shared variable, and if the variable is read-only, and the reference occurs 
        '   outside the shared constructor of the type in which the variable is declared, then the result is the 
        '   value of the shared variable I in E. Otherwise, the result is the shared variable I in E.
        '** If I identifies a shared event, the result is an event access with no associated instance expression.
        '** If I identifies a constant, then the result is the value of that constant.
        '** If I identifies an enumeration member, then the result is the value of that enumeration member.
        '** Otherwise, E.I is an invalid member reference, and a compile-time error occurs.
        '---------------------------------------------------------------------------------------------------------
        '* If E is classified as a variable or value, the type of which is T, and I is the name of an accessible 
        '  member of E, then E.I is evaluated and classified as follows:
        '** If I is the keyword New and E is an instance expression (Me, MyBase, or MyClass), then the result is 
        '   a method group representing the instance constructors of the type of E with an associated 
        '   instance expression of E and no type argument list. Otherwise, a compile-time error occurs.
        '** If I identifies one or more methods, then the result is a method group with the associated type 
        '   argument list and an associated instance expression of E.
        '** If I identifies one or more properties, then the result is a property group with an 
        '   associated instance expression of E.
        '** If I identifies a shared variable or an instance variable, and if the variable is read-only, 
        '   and the reference occurs outside a constructor of the class in which the variable is declared 
        '   appropriate for the kind of variable (shared or instance), then the result is the value of the 
        '   variable I in the object referenced by E. If T is a reference type, then the result is the variable 
        '   I in the object referenced by E. Otherwise, if T is a value type and the expression E is classified 
        '   as a variable, the result is a variable; otherwise the result is a value.
        '** If I identifies an event, the result is an event access with an associated instance expression of E.
        '** If I identifies a constant, then the result is the value of that constant.
        '** If I identifies an enumeration member, then the result is the value of that enumeration member.
        '** If T is Object, then the result is a late-bound member lookup classified as a late-bound access 
        '   with an associated instance expression of E.
        '** Otherwise, E.I is an invalid member reference, and a compile-time error occurs.
        '---------------------------------------------------------------------------------------------------------
        'If the member access expression includes a type argument list, then only types or methods 
        'with the same arity as the type argument list are considered.
        'When a member access expression begins with the keyword Global, the keyword represents the outermost 
        'unnamed namespace, which is useful in situations where a declaration shadows an enclosing namespace. The 
        'Global keyword allows "escaping" out to the outermost namespace in that situation. 
        '---------------------------------------------------------------------------------------------------------

        Dim Name As String = m_Second.Name
        Dim typeArguments As TypeArgumentList = Nothing

        Dim iokwta As IdentifierOrKeywordWithTypeArguments = TryCast(m_Second, IdentifierOrKeywordWithTypeArguments)
        If iokwta IsNot Nothing Then
            typeArguments = iokwta.TypeArguments
        End If

        Helper.Assert(Name IsNot Nothing AndAlso Name <> "")

        If m_First IsNot Nothing Then
            result = m_First.ResolveExpression(New ResolveInfo(Info.Compiler, False, Info.CanFail, Info.CanBeImplicitSimpleName)) AndAlso result
        Else
            '* If E is omitted, then the expression from the immediately containing With statement is substituted for
            '  E and the member access is performed. If there is no containing With statement, a compile-time 
            '  error occurs.
            m_WithStatement = Me.FindFirstParentOfCodeBlock(Of WithStatement)()
            If m_WithStatement Is Nothing Then
                Helper.AddError(Me)
                Return False
            Else
                m_First = m_WithStatement.WithVariableExpression
                ' Helper.Assert(m_First.IsResolved)
            End If
        End If

        If result = False Then Return result

        '* If E is a type parameter, then a compile-time error results.
        If m_First.Classification.IsTypeClassification AndAlso m_First.Classification.AsTypeClassification.IsTypeParameter Then
            result = Compiler.Report.ShowMessage(Messages.VBNC32098, Me.Location) AndAlso result
            If result = False Then Return result
        End If

        '* If E is the keyword Global, and I is the name of an accessible type in the global namespace, 
        '  then the result is that type.
        If TypeOf m_First Is GlobalExpression Then
            Dim foundType As Mono.Cecil.TypeReference
            foundType = Compiler.TypeManager.GetTypesByNamespace("").Item(Name)
            If foundType IsNot Nothing Then
                Classification = New TypeClassification(Me, foundType)
                Return True
            ElseIf Compiler.TypeManager.Namespaces.ContainsKey(Name) Then
                Classification = New NamespaceClassification(Me, Compiler.TypeManager.Namespaces(Name))
                Return True
            Else
                Return Compiler.Report.ShowMessage(Messages.VBNC30456, Location, Name, "<Default>") AndAlso result
            End If
        End If

        '* If E is classified as a namespace and I is the name of an accessible member of that namespace, 
        '  then the result is that member. The result is classified as a namespace or a type depending on the member.
        If m_First.Classification.IsNamespaceClassification Then
            Dim nstypes As Generic.List(Of Mono.Cecil.TypeReference) = Nothing
            Dim ns As [Namespace] = m_First.Classification.AsNamespaceClassification.Namespace
            Dim nsname As String = ns.Name
            Dim foundns As [Namespace]
            Dim foundType As Mono.Cecil.TypeReference

            foundns = Compiler.TypeManager.Namespaces.Item(ns, Name)
            foundType = Compiler.TypeManager.TypesByNamespace(nsname).Item(Name)

            If foundns IsNot Nothing AndAlso foundType Is Nothing Then
                Classification = New NamespaceClassification(Me, foundns)
                Return True
            ElseIf foundns Is Nothing AndAlso foundType IsNot Nothing Then
                Classification = New TypeClassification(Me, foundType)
                Return True
            ElseIf foundns IsNot Nothing AndAlso nstypes IsNot Nothing Then
                Helper.AddError(Me, "Found a namespace and a type with the same name.")
            End If

            'TODO: The spec is missing info about modules in namespaces. Doing check anyway.
            Dim modules As TypeDictionary = Compiler.TypeManager.GetModulesByNamespace(ns.ToString)
            Dim members As Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference) = Helper.GetMembersOfTypes(Compiler, modules, Name)
            If members IsNot Nothing AndAlso members.Count > 0 Then
                Dim first As Object = members(0)
                If Helper.IsMethodDeclaration(first) Then
                    Classification = New MethodGroupClassification(Me, Nothing, typeArguments, Nothing, members)
                    Return True
                ElseIf Helper.IsTypeDeclaration(first) Then
                    If members.Count = 1 Then
                        Classification = New TypeClassification(Me, first)
                        Return True
                    Else
                        Helper.AddError(Me)
                    End If
                ElseIf Helper.IsFieldDeclaration(first) Then
                    If members.Count = 1 Then
                        Classification = New VariableClassification(Me, DirectCast(first, Mono.Cecil.FieldReference), Nothing)
                        Return True
                    Else
                        Helper.AddError(Me)
                    End If
                ElseIf Helper.IsPropertyDeclaration(first) Then
                    Classification = New PropertyGroupClassification(Me, Nothing, members)
                    Return True
                Else
                    Helper.Stop()
                End If
            End If
        End If

        '* If E is a built-in type or an expression classified as a type, and I is the name of an accessible 
        '  member of E, then E.I is evaluated and classified as follows:
        '** If I is the keyword New, then a compile-time error occurs.
        '** If I identifies a type, then the result is that type.
        '** If I identifies one or more methods, then the result is a method group with the associated 
        '   type argument list and no associated instance expression.
        '** If I identifies one or more properties, then the result is a property group with no associated 
        '   instance expression.
        '** If I identifies a shared variable, and if the variable is read-only, and the reference occurs 
        '   outside the shared constructor of the type in which the variable is declared, then the result is the 
        '   value of the shared variable I in E. Otherwise, the result is the shared variable I in E.
        '** If I identifies a shared event, the result is an event access with no associated instance expression.
        '** If I identifies a constant, then the result is the value of that constant.
        '** If I identifies an enumeration member, then the result is the value of that enumeration member.
        '** Otherwise, E.I is an invalid member reference, and a compile-time error occurs.
        If m_First.Classification.IsTypeClassification Then
            'If m_Second.IsKeyword AndAlso m_Second.Keyword = KS.New Then
            '    '** If I is the keyword New, then a compile-time error occurs.
            '    Helper.AddError(Me)
            'End If
            Dim members As Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference) = Nothing
            Dim entry As MemberCacheEntry
            'members = Helper.FilterByName(Helper.GetMembers(Compiler, m_First.Classification.AsTypeClassification.Type), Name)
            'members = Helper.FilterByName(Compiler.TypeManager.GetCache(m_First.Classification.AsTypeClassification.Type).FlattenedCache.GetAllMembers.ToArray, Name)
            entry = Compiler.TypeManager.GetCache(m_First.Classification.AsTypeClassification.Type).LookupFlattened(Name)

            If entry Is Nothing Then
                Compiler.Report.ShowMessage(Messages.VBNC30456, Me.Location, Name, m_First.Classification.AsTypeClassification.Type.FullName)
                Return False
            End If

            If entry IsNot Nothing Then
                members = entry.Members
                If typeArguments IsNot Nothing Then
                    members = Helper.FilterByTypeArguments(members, typeArguments)
                    'For i As Integer = 0 To members.Count - 1
                    '    Dim mR As Mono.Cecil.MethodReference = TryCast(members(i), Mono.Cecil.MethodReference)
                    '    If mR Is Nothing Then
                    '        Helper.StopIfDebugging()
                    '        Continue For
                    '    End If
                    '    members(i) = CecilHelper.GetCorrectMember(mR, withTypeArgs.TypeArguments.ArgumentCollection)
                    'Next
                End If
                members = Helper.FilterExternalInaccessible(Me.Compiler, members)
            End If

            Helper.StopIfDebugging(members Is Nothing OrElse members.Count = 0)

            If members IsNot Nothing AndAlso members.Count > 0 Then
                Dim first As Object = members(0)
                '** If I identifies one or more methods, then the result is a method group with the associated 
                '   type argument list and no associated instance expression.
                If Helper.IsMethodDeclaration(first) Then
                    Classification = New MethodGroupClassification(Me, Nothing, typeArguments, Nothing, members)
                    Return True
                End If
                '** If I identifies one or more properties, then the result is a property group with no associated 
                '   instance expression.
                If Helper.IsPropertyDeclaration(first) Then
                    Dim instanceExpression As Expression = Nothing
                    If m_First.Classification.AsTypeClassification.CanBeExpression Then
                        'Ignore failures here, not sure if failures can actually happen for valid code
                        m_First.Classification.AsTypeClassification.CreateAliasExpression(m_First, instanceExpression)
                    End If
                    Classification = New PropertyGroupClassification(Me, instanceExpression, members)
                    Return True
                End If

                If members.Count > 1 Then Helper.Stop()

                '** If I identifies a type, then the result is that type.
                If Helper.IsTypeDeclaration(first) Then
                    Classification = New TypeClassification(Me, first)
                    Return True
                End If

                '** If I identifies a shared variable, and if the variable is read-only, and the reference occurs 
                '   outside the shared constructor of the type in which the variable is declared, then the result is the 
                '   value of the shared variable I in E. Otherwise, the result is the shared variable I in E.
                If Helper.IsFieldDeclaration(first) Then
                    Dim fld As Mono.Cecil.FieldReference = TryCast(first, Mono.Cecil.FieldReference)
                    If fld Is Nothing Then
                        Dim var As TypeVariableDeclaration = TryCast(first, TypeVariableDeclaration)
                        Helper.Assert(var IsNot Nothing)
                        fld = var.FieldBuilder
                    End If
                    Dim constructor As ConstructorDeclaration = Me.FindFirstParent(Of ConstructorDeclaration)()
                    Dim instanceExpression As Expression = Nothing

                    If Not CecilHelper.IsStatic(fld) AndAlso m_First.Classification.IsTypeClassification AndAlso m_First.Classification.AsTypeClassification.CanBeExpression Then
                        result = m_First.Classification.AsTypeClassification.CreateAliasExpression(m_First, instanceExpression) AndAlso result
                    End If

                    If Helper.IsShared(fld) AndAlso CBool(CecilHelper.FindDefinition(fld).Attributes And Mono.Cecil.FieldAttributes.InitOnly) AndAlso (constructor Is Nothing OrElse constructor.Modifiers.Is(ModifierMasks.Shared) = False) Then
                        Classification = New ValueClassification(Me, fld, Nothing)
                        Return True
                    Else
                        If instanceExpression Is Nothing AndAlso CecilHelper.IsStatic(fld) = False Then
                            Return Report.ShowMessage(Messages.VBNC30469, Me.Location)
                        End If
                        Classification = New VariableClassification(Me, fld, instanceExpression)
                        Return True
                    End If
                End If

                '** If I identifies a shared event, the result is an event access with no associated instance expression.
                If Helper.IsEventDeclaration(first) Then
                    Dim red As EventDeclaration = TryCast(first, EventDeclaration)
                    Dim red2 As Mono.Cecil.EventDefinition = CecilHelper.FindDefinition(TryCast(first, Mono.Cecil.EventReference))
                    If red IsNot Nothing AndAlso red.Modifiers.Is(ModifierMasks.Shared) Then
                        Classification = New EventAccessClassification(Me, red.EventDescriptor, Nothing)
                        Return True
                    ElseIf red2 IsNot Nothing AndAlso red2.AddMethod.IsStatic Then
                        Classification = New EventAccessClassification(Me, red2, Nothing)
                        Return True
                    End If
                End If

                '** If I identifies a constant, then the result is the value of that constant.
                Dim c As ConstantDeclaration = TryCast(first, ConstantDeclaration)
                If c IsNot Nothing Then
                    Classification = New ValueClassification(Me, c)
                    Return True
                End If

                '** If I identifies an enumeration member, then the result is the value of that enumeration member.
                Dim enummember As EnumMemberDeclaration = TryCast(first, EnumMemberDeclaration)
                If enummember IsNot Nothing Then
                    Classification = New ValueClassification(Me, enummember)
                    Return True
                End If

                '** Otherwise, E.I is an invalid member reference, and a compile-time error occurs.
                'Now we need to check for default instances
                If m_First.Classification.AsTypeClassification.CanBeExpression Then
                    Dim aliasExpression As Expression = Nothing
                    m_First.Classification.AsTypeClassification.CreateAliasExpression(m_First, aliasExpression)
                    m_First = aliasExpression
                Else
                    Return Compiler.Report.ShowMessage(Messages.VBNC30469, Me.Location)
                End If
            Else
                Helper.AddError(Me, "Could not resolve name '" & Name & "'" & "," & Me.Location.ToString(Compiler))
            End If
        End If


        '* If E is classified as a variable or value, the type of which is T, and I is the name of an accessible 
        '  member of E, then E.I is evaluated and classified as follows:
        '** If I is the keyword New and E is an instance expression (Me, MyBase, or MyClass), then the result is 
        '   a method group representing the instance constructors of the type of E with an associated 
        '   instance expression of E and no type argument list. Otherwise, a compile-time error occurs.
        '** If I identifies one or more methods, then the result is a method group with the associated type 
        '   argument list and an associated instance expression of E.
        '** If I identifies one or more properties, then the result is a property group with an 
        '   associated instance expression of E.
        '** If I identifies a shared variable or an instance variable, and if the variable is read-only, 
        '   and the reference occurs outside a constructor of the class in which the variable is declared 
        '   appropriate for the kind of variable (shared or instance), then the result is the value of the 
        '   variable I in the object referenced by E. If T is a reference type, then the result is the variable 
        '   I in the object referenced by E. Otherwise, if T is a value type and the expression E is classified 
        '   as a variable, the result is a variable; otherwise the result is a value.
        '** If I identifies an event, the result is an event access with an associated instance expression of E.
        '** If I identifies a constant, then the result is the value of that constant.
        '** If I identifies an enumeration member, then the result is the value of that enumeration member.
        '** If T is Object, then the result is a late-bound member lookup classified as a late-bound access 
        '   with an associated instance expression of E.
        '** Otherwise, E.I is an invalid member reference, and a compile-time error occurs.
        If m_First.Classification.IsValueClassification OrElse m_First.Classification.IsVariableClassification OrElse m_First.Classification.CanBeValueClassification Then
            Dim T As Mono.Cecil.TypeReference 'Descriptor

            If m_First.Classification.IsValueClassification Then
                T = m_First.Classification.AsValueClassification.Type
            ElseIf m_First.Classification.IsVariableClassification Then
                T = m_First.Classification.AsVariableClassification.Type
            ElseIf m_First.Classification.CanBeValueClassification Then
                m_First = m_First.ReclassifyToValueExpression
                result = m_First.ResolveExpression(ResolveInfo.Default(Info.Compiler)) AndAlso result
                If result = False Then
                    If Info.IsEventResolution Then
                        Return Compiler.Report.ShowMessage(Messages.VBNC30506, Location)
                    Else
                        Helper.AddError(Compiler, Location, "Huh?")
                    End If
                End If
                Helper.Assert(m_First.Classification IsNot Nothing)
                Helper.Assert(m_First.Classification.AsValueClassification IsNot Nothing)
                T = m_First.Classification.AsValueClassification.Type
            Else
                Throw New InternalException(Me)
            End If

            If CecilHelper.IsByRef(T) Then
                m_First = m_First.DereferenceByRef()
                T = m_First.ExpressionType
            End If

            '** If I is the keyword New and E is an instance expression (Me, MyBase, or MyClass), then the result is 
            '   a method group representing the instance constructors of the type of E with an associated 
            '   instance expression of E and no type argument list. Otherwise, a compile-time error occurs.
            If m_Second.IsKeyword AndAlso m_Second.Keyword = KS.New Then
                If TypeOf m_First Is InstanceExpression Then
                    Classification = New MethodGroupClassification(Me, m_First, typeArguments, Nothing, Helper.GetInstanceConstructors(T))
                    Return True
                Else
                    Helper.AddError(Me)
                End If
            End If

            Dim members As Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference)
            Dim member As MemberCacheEntry

            member = Compiler.TypeManager.GetCache(T).LookupFlattened(Name, Me.FindFirstParent_IType.CecilType)
            If member Is Nothing Then
                If Me.File.IsOptionStrictOn = False AndAlso Helper.CompareType(T, Compiler.TypeCache.System_Object) Then
                    Classification = New LateBoundAccessClassification(Me, m_First, Nothing, Name)
                    Return True
                End If

                member = Compiler.TypeManager.GetCache(T).LookupFlattened(Name, MemberVisibility.All)
                If member Is Nothing OrElse member.Members.Count = 0 Then
                    If Helper.CompareType(T, Compiler.TypeCache.System_Object) Then
                        Return Compiler.Report.ShowMessage(Messages.VBNC30574, Me.Location) AndAlso result
                    Else
                        Return Compiler.Report.ShowMessage(Messages.VBNC30456, Me.Location, Name, T.FullName) AndAlso result
                    End If
                ElseIf member.Members.Count = 1 Then
                    Return Compiler.Report.ShowMessage(Messages.VBNC30390, Me.Location, Name, T.FullName, Helper.GetVisibilityString(member.Members(0))) AndAlso result
                Else
                    Return Compiler.Report.ShowMessage(Messages.VBNC30517, Me.Location, Name) AndAlso result
                End If
            End If

            members = member.Members
            Dim withTypeArgs As IdentifierOrKeywordWithTypeArguments
            withTypeArgs = TryCast(m_Second, IdentifierOrKeywordWithTypeArguments)
            If withTypeArgs IsNot Nothing Then
                members = Helper.FilterByTypeArguments(members, withTypeArgs.TypeArguments)
            End If
            members = Helper.FilterExternalInaccessible(Me.Compiler, members)

            If members.Count = 0 Then
                result = Compiler.Report.ShowMessage(Messages.VBNC30456, Me.Location, Name, T.FullName) AndAlso result
                If result = False Then Return result
            End If

            If members.Count > 0 Then
                Dim first As Object = members(0)
                '** If I identifies one or more methods, then the result is a method group with the associated type 
                '   argument list and an associated instance expression of E.
                If Helper.IsMethodDeclaration(first) Then
                    m_First = m_First.GetObjectReference
                    Classification = New MethodGroupClassification(Me, m_First, typeArguments, Nothing, members)
                    Return True
                End If
                '** If I identifies one or more properties, then the result is a property group with an 
                '   associated instance expression of E.
                If Helper.IsPropertyDeclaration(first) Then
                    m_First = m_First.GetObjectReference
                    Classification = New PropertyGroupClassification(Me, m_First, members)
                    Return True
                End If

                If members.Count > 1 Then Throw New InternalException(Me)

                '** If I identifies a shared variable or an instance variable, and if the variable is read-only, 
                '   and the reference occurs outside a constructor of the class in which the variable is declared 
                '   appropriate for the kind of variable (shared or instance), then the result is the value of the 
                '   variable I in the object referenced by E. 
                '   If T is a reference type, then the result is the variable I in the object referenced by E. 
                '   Otherwise, if T is a value type and the expression E is classified as a variable, the result is 
                '   a variable; otherwise the result is a value.
                Dim var As VariableDeclaration = TryCast(first, VariableDeclaration)
                Dim fld As Mono.Cecil.FieldReference = TryCast(first, Mono.Cecil.FieldReference)

                If var IsNot Nothing Then
                    Dim constructor As ConstructorDeclaration = Me.FindFirstParent(Of ConstructorDeclaration)()

                    If var.Modifiers.Is(ModifierMasks.ReadOnly) AndAlso (constructor Is Nothing OrElse constructor.Modifiers.Is(ModifierMasks.Shared) <> var.Modifiers.Is(ModifierMasks.Shared)) Then
                        Classification = New ValueClassification(Me, var)
                        Return True
                    ElseIf CecilHelper.IsClass(T) Then
                        Classification = New VariableClassification(Me, var)
                        Return True
                    ElseIf CecilHelper.IsValueType(T) Then
                        If m_First.Classification.IsVariableClassification Then
                            Classification = New VariableClassification(Me, var)
                            Return True
                        ElseIf m_First.Classification.IsValueClassification Then
                            Classification = New ValueClassification(Me, var)
                            Return True
                        Else
                            Throw New InternalException(Me)
                        End If
                    Else
                        Throw New InternalException(Me)
                    End If
                ElseIf fld IsNot Nothing Then
                    Dim fD As Mono.Cecil.FieldDefinition = CecilHelper.FindDefinition(fld)
                    If Helper.IsAccessible(Me, fD.Attributes, fld.DeclaringType, Me.FindFirstParent_IType.CecilType) = False Then
                        Return Compiler.Report.ShowMessage(Messages.VBNC30390, Location, fld.DeclaringType.FullName, fld.Name, Helper.ToString(fD.Attributes))
                    End If

                    Dim constructor As ConstructorDeclaration = Me.FindFirstParent(Of ConstructorDeclaration)()
                    If fD.IsInitOnly AndAlso (constructor Is Nothing OrElse constructor.Modifiers.Is(ModifierMasks.Shared) <> fD.IsStatic) Then
                        If fD.IsStatic Then
                            Classification = New ValueClassification(Me, fld, Nothing)
                        Else
                            Classification = New ValueClassification(Me, fld, m_First)
                        End If
                        Return True
                    ElseIf CecilHelper.IsClass(T) Then
                        If fD.IsStatic Then
                            Classification = New VariableClassification(Me, fld, Nothing)
                        Else
                            Classification = New VariableClassification(Me, fld, m_First)
                        End If
                        Return True
                    ElseIf CecilHelper.IsValueType(T) Then
                        If m_First.Classification.IsVariableClassification Then
                            If Not TypeOf m_First Is InstanceExpression Then
                                m_First = m_First.GetObjectReference
                            End If
                            If fD.IsStatic Then
                                Classification = New VariableClassification(Me, fld, Nothing)
                            Else
                                Classification = New VariableClassification(Me, fld, m_First)
                            End If
                            Return True
                        ElseIf m_First.Classification.IsValueClassification Then
                            If fD.IsStatic Then
                                Classification = New ValueClassification(Me, fld, Nothing)
                            Else
                                Classification = New ValueClassification(Me, fld, m_First)
                            End If
                            Return True
                        ElseIf m_First.Classification.CanBeValueClassification Then
                            m_First = m_First.ReclassifyToValueExpression
                            result = m_First.ResolveExpression(ResolveInfo.Default(Info.Compiler)) AndAlso result

                            If fD.IsStatic Then
                                Classification = New ValueClassification(Me, fld, Nothing)
                            Else
                                Classification = New ValueClassification(Me, fld, m_First)
                            End If
                            Return True
                        Else
                            Throw New InternalException(Me)
                        End If
                    Else
                        Throw New InternalException(Me)
                    End If
                End If

                '** If I identifies an event, the result is an event access with an associated instance expression of E.
                If Helper.IsEventDeclaration(first) Then
                    Dim red As EventDeclaration = TryCast(first, EventDeclaration)
                    If red Is Nothing AndAlso TypeOf first Is Mono.Cecil.EventReference Then
                        red = DirectCast(CecilHelper.FindDefinition(DirectCast(first, Mono.Cecil.EventReference)).Annotations(Compiler), EventDeclaration)
                    End If
                    If red IsNot Nothing Then
                        Classification = New EventAccessClassification(Me, red.EventDescriptor, m_First)
                        Return True
                    End If
                    Dim eInfo As Mono.Cecil.EventReference = TryCast(first, Mono.Cecil.EventReference)
                    If eInfo IsNot Nothing Then
                        Classification = New EventAccessClassification(Me, eInfo, m_First)
                        Return True
                    End If
                End If

                '** If I identifies a constant, then the result is the value of that constant.
                Dim constant As ConstantDeclaration = TryCast(first, ConstantDeclaration)
                If constant IsNot Nothing Then
                    Classification = New ValueClassification(Me, constant)
                    Return True
                End If

                '** If I identifies an enumeration member, then the result is the value of that enumeration member.
                If Helper.IsEnumFieldDeclaration(Compiler, first) Then
                    Dim em As EnumMemberDeclaration = TryCast(first, EnumMemberDeclaration)
                    Dim emfld As Mono.Cecil.FieldReference = TryCast(first, Mono.Cecil.FieldReference)
                    If em IsNot Nothing Then
                        Classification = New ValueClassification(Me, em)
                        Return True
                    ElseIf emfld IsNot Nothing Then
                        Classification = New ValueClassification(Me, emfld, Nothing)
                        Return True
                    End If
                End If

                '** If T is Object, then the result is a late-bound member lookup classified as a late-bound access 
                '   with an associated instance expression of E.
                If T IsNot Nothing Then
                    'Dim td As TypeDescriptor = TryCast(T, TypeDescriptor)
                    Dim compresult As Boolean = Helper.CompareType(T, Compiler.TypeCache.System_Object)
                    If compresult Then
                        Return Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
                    End If
                End If
                '** Otherwise, E.I is an invalid member reference, and a compile-time error occurs.
                Compiler.Report.ShowMessage(Messages.VBNC30456, Me.Location, Name, T.FullName)
                result = False
            Else
                Compiler.Report.ShowMessage(Messages.VBNC30456, Me.Location, Name, T.FullName)
                result = False
            End If
        End If

        Compiler.Report.ShowMessage(Messages.VBNC30456, Location, Name, m_First.AsString)

        Return False
    End Function

    Shared Function CreateAndParseTo(ByRef result As Expression) As Boolean
        Return result.Compiler.Report.ShowMessage(Messages.VBNC99997, result.Location)
    End Function

    Shared Function IsUnaryMe(ByVal Tm As tm) As Boolean
        Return Tm.CurrentToken.Equals(KS.Dot)
    End Function

    Shared Function IsBinaryMe(ByVal tm As tm) As Boolean
        Return tm.CurrentToken.Equals(KS.Dot)
    End Function

    ReadOnly Property FirstExpression() As Expression
        Get
            Return m_First
        End Get
    End Property

    ReadOnly Property SecondExpression() As IdentifierOrKeyword
        Get
            Return m_Second
        End Get
    End Property

    ReadOnly Property CompleteName() As String
        Get
            Return m_First.ToString & "." & m_Second.Name
        End Get
    End Property
End Class

