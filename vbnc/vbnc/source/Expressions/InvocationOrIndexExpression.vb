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
''' InvocationExpression: Expression [ "(" [ ArgumentList ] ")" ]
''' IndexExpression:      Expression "(" [ ArgumentList ] ")"
''' Note that for the index expression the parenthesis are not optional.
''' This is reflected by the fact that m_ArgumentList is not nothing if 
''' parenthesis are provided.
''' Classifications: Value, Void (InvocationExpression) Value, Variable, PropertyAccess (IndexExpression)
''' 
''' If a parameter is a value parameter, the matching argument expression must be classified as a value. 
''' The value is converted to the type of the parameter and passed in as the parameter at run time. 
''' 
''' If the parameter is a reference parameter and the matching argument expression is classified as a variable
''' whose type is the same as the parameter, then a reference to the variable is passed in as the parameter at run time.
''' Otherwise, if the matching argument expression is classified as a variable, value, or property access, 
''' then a temporary variable of the type of the parameter is allocated. Before the method invocation at run time, 
''' the argument expression is reclassified as a value, converted to the type of the parameter, 
''' and assigned to the temporary variable. Then a reference to the temporary variable is passed in as the parameter. 
''' After the method invocation is evaluated, if the argument expression is classified as a variable or property access, 
''' the temporary variable is assigned to the variable expression or the property access expression. 
''' If the property access expression has no Set accessor, then the assignment is not performed.
''' </summary>
''' <remarks></remarks>
Public Class InvocationOrIndexExpression
    Inherits Expression

    Private m_Expression As Expression
    Private m_ArgumentList As ArgumentList

    Private m_ExpressionType As Mono.Cecil.TypeReference

    ' AscW is replaced with the first parameter casted to Integer when:
    ' /novbruntimeref is used
    ' the called AscW is declared in the module being compiled
    ' the called AscW is Shared or is declared in a Module
    ' the first argument of the the called AscW is a ByVal Char
    Private m_AscWExpression As Expression

    ''' <summary>
    ''' If this method is not nothing then the arguments are emitted and then the method is called.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_InvocationMethod As Mono.Cecil.MethodReference

    Private m_IsLateBoundArray As Boolean

    ReadOnly Property IsLateBoundArray() As Boolean
        Get
            Return m_IsLateBoundArray
        End Get
    End Property

    Public Overrides ReadOnly Property AsString() As String
        Get
            Return m_Expression.AsString & "(" & m_ArgumentList.AsString & ")"
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Shadows Sub Init(ByVal Expression As Expression, ByVal ArgumentList As ArgumentList)
        m_Expression = Expression
        m_ArgumentList = ArgumentList
    End Sub

    ReadOnly Property Expression() As Expression
        Get
            Return m_Expression
        End Get
    End Property

    Overrides ReadOnly Property ExpressionType() As Mono.Cecil.TypeReference
        Get
            Helper.Assert(m_ExpressionType IsNot Nothing)
            Return m_ExpressionType
        End Get
    End Property

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        If m_AscWExpression IsNot Nothing Then
            result = m_AscWExpression.GenerateCode(Info.Clone(Me, True, False, Compiler.TypeCache.System_Char)) AndAlso result

            Return result
        End If

        If m_InvocationMethod IsNot Nothing Then
            result = Helper.EmitArgumentsAndCallOrCallVirt(Info, m_Expression, m_ArgumentList, m_InvocationMethod) AndAlso result
            Return True
        End If

        If m_IsLateBoundArray Then
            Return Compiler.Report.ShowMessage(Messages.VBNC99997, Location)
            Return True
        End If

        If Classification.IsLateBoundClassification Then
            result = LateBoundAccessToExpression.EmitLateCall(Info, Classification.AsLateBoundAccess) AndAlso result
        Else
            Select Case m_Expression.Classification.Classification
                Case ExpressionClassification.Classifications.MethodGroup
                    With m_Expression.Classification.AsMethodGroupClassification
                        result = Helper.EmitArgumentsAndCallOrCallVirt(Info, .InstanceExpression, m_ArgumentList, .ResolvedMethod)
                    End With
                Case ExpressionClassification.Classifications.Value
                    If Info.IsRHS Then
                        If Me.Classification.IsVariableClassification Then
                            result = Me.Classification.GenerateCode(Info) AndAlso result
                        ElseIf Me.Classification.IsPropertyAccessClassification Then
                            result = Me.Classification.AsPropertyAccess.GenerateCode(Info) AndAlso result
                        Else
                            result = m_Expression.GenerateCode(Info) AndAlso result
                        End If
                    Else
                        Return Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
                    End If
                Case ExpressionClassification.Classifications.PropertyAccess
                    If Info.IsRHS Then
                        If Me.Classification.IsVariableClassification Then
                            result = Me.Classification.GenerateCode(Info) AndAlso result
                        Else
                            result = m_Expression.GenerateCode(Info) AndAlso result
                        End If
                    Else
                        Return Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
                    End If
                Case ExpressionClassification.Classifications.PropertyGroup
                    'Helper.NotImplemented()
                    Dim pgC As PropertyGroupClassification
                    pgC = m_Expression.Classification.AsPropertyGroup
                    result = pgC.GenerateCodeAsValue(Info) AndAlso result
                Case ExpressionClassification.Classifications.Variable
                    If Info.IsRHS Then
                        If Classification.IsValueClassification Then
                            result = Classification.AsValueClassification.GenerateCode(Info) AndAlso result
                        ElseIf Classification.IsPropertyGroupClassification Then
                            result = Classification.AsPropertyGroup.GenerateCodeAsValue(Info) AndAlso result
                        ElseIf Classification.IsPropertyAccessClassification Then
                            result = Classification.AsPropertyAccess.GenerateCode(Info) AndAlso result
                        Else
                            result = Classification.AsVariableClassification.GenerateCodeAsValue(Info) AndAlso result
                        End If
                    Else
                        result = Classification.GenerateCode(Info) AndAlso result
                    End If
                Case ExpressionClassification.Classifications.LateBoundAccess
                    result = LateBoundAccessToExpression.EmitLateCall(Info, Classification.AsLateBoundAccess) AndAlso result
                Case Else
                    Throw New InternalException(Me)
            End Select
        End If

        Return result
    End Function

    Shared Function IsBinaryMe(ByVal tm As tm) As Boolean
        Return tm.CurrentToken.Equals(KS.LParenthesis)
    End Function

    Shared Function CreateAndParseTo(ByRef result As Expression) As Boolean
        Return result.Compiler.Report.ShowMessage(Messages.VBNC99997, result.Location)
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        result = m_Expression.ResolveTypeReferences AndAlso result
        If m_ArgumentList IsNot Nothing Then result = m_ArgumentList.ResolveTypeReferences AndAlso result

        Return result
    End Function

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = m_Expression.ResolveExpression(New ResolveInfo(Info.Compiler, True, , False)) AndAlso result
        If result = False Then Return False
        If m_ArgumentList IsNot Nothing Then result = m_ArgumentList.ResolveCode(ResolveInfo.Default(Info.Compiler)) AndAlso result

        If result = False Then Return False
        'Check the classification of the arguments, can be value, variable, propertyaccess
        For i As Integer = 0 To m_ArgumentList.Count - 1
            If m_ArgumentList(i).Expression IsNot Nothing Then
                Select Case m_ArgumentList(i).Expression.Classification.Classification
                    Case ExpressionClassification.Classifications.Value
                        'ok
                    Case ExpressionClassification.Classifications.Variable
                        'ok
                    Case ExpressionClassification.Classifications.PropertyAccess
                        'ok
                    Case ExpressionClassification.Classifications.MethodPointer
                        'ok?
                    Case ExpressionClassification.Classifications.PropertyGroup
                        m_ArgumentList(i).Expression = m_ArgumentList(i).Expression.ReclassifyToPropertyAccessExpression
                        result = m_ArgumentList(i).Expression.ResolveExpression(ResolveInfo.Default(Info.Compiler)) AndAlso result
                    Case Else
                        'reclassify to value
                        If m_ArgumentList(i).Expression.Classification.CanBeValueClassification Then
                            m_ArgumentList(i).Expression = m_ArgumentList(i).Expression.ReclassifyToValueExpression
                            result = m_ArgumentList(i).Expression.ResolveExpression(ResolveInfo.Default(Info.Compiler)) AndAlso result
                        Else
                            Helper.AddError(Me)
                        End If
                End Select
            End If
        Next

        If result = False Then Return result

        Select Case m_Expression.Classification.Classification
            Case ExpressionClassification.Classifications.LateBoundAccess
                Dim lae As LateBoundAccessClassification = m_Expression.Classification.AsLateBoundAccess
                lae.Arguments = m_ArgumentList
                Classification = lae
            Case ExpressionClassification.Classifications.MethodGroup
                'This is an invocation expression.
                result = ResolveMethodInvocation() AndAlso result

            Case ExpressionClassification.Classifications.Value
                If CecilHelper.IsArray(m_Expression.ExpressionType) Then
                    result = ResolveArrayInvocation(Me, m_Expression.ExpressionType) AndAlso result
                Else
                    result = ResolveIndexInvocation(Me, m_Expression.ExpressionType) AndAlso result
                End If
            Case ExpressionClassification.Classifications.PropertyAccess
                If CecilHelper.IsArray(m_Expression.ExpressionType) Then
                    result = ResolveArrayInvocation(Me, m_Expression.ExpressionType) AndAlso result
                Else
                    result = ResolveIndexInvocation(Me, m_Expression.ExpressionType) AndAlso result
                End If
            Case ExpressionClassification.Classifications.PropertyGroup
                result = ResolvePropertyGroupInvocation() AndAlso result
            Case ExpressionClassification.Classifications.Variable
                'This is an index expression.

                '                An index expression results in an array element or reclassifies a property group into a property access. An index expression consists of, in order, an expression, an opening parenthesis, an index argument list, and a closing parenthesis. The target of the index expression must be classified as either a property group or a value. An index expression is processed as follows:
                '	If the target expression is classified as a value and if its type is not an array type, Object, or System.Array, the type must have a default property. The index is performed on a property group that represents all of the default properties of the type. Although it is not valid to declare a parameterless default property in Visual Basic, other languages may allow declaring such a property. Consequently, indexing a property with no arguments is allowed.
                '	If the expression results in a value of an array type, the number of arguments in the argument list must be the same as the rank of the array type and may not include named arguments. If any of the indexes are invalid at run time, a System.IndexOutOfRangeException exception is thrown. Each expression must be implicitly convertible to type Integer. The result of the index expression is the variable at the specified index and is classified as a variable.
                '	If the expression is classified as a property group, overload resolution is used to determine whether one of the properties is applicable to the index argument list. If the property group only contains one property that has a Get accessor and if that accessor takes no arguments, then the property group is interpreted as an index expression with an empty argument list. The result is used as the target of the current index expression. If no properties are applicable, then a compile-time error occurs. Otherwise, the expression results in a property access with the associated instance expression (if any) of the property group.
                '	If the expression is classified as a late-bound property group or as a value whose type is Object or System.Array, the processing of the index expression is deferred until run time and the indexing is late-bound. The expression results in a late-bound property access typed as Object. The associated instance expression is either the target expression, if it is a value, or the associated instance expression of the property group. At run time the expression is processed as follows:
                '	If the expression is classified as a late-bound property group, the expression may result in a method group, a property group, or a value (if the member is an instance or shared variable). If the result is a method group or property group, overload resolution is applied to the group to determine the correct method for the argument list. If overload resolution fails, a System.Reflection.AmbiguousMatchException exception is thrown. Then the result is processed either as a property access or as an invocation and the result is returned. If the invocation is of a subroutine, the result is Nothing.
                '	If the run-time type of the target expression is an array type or System.Array, the result of the index expression is the value of the variable at the specified index. 
                '	Otherwise, the run-time type of the expression must have a default property and the index is performed on the property group that represents all of the default properties on the type. If the type has no default property, then a System.MissingMemberException exception is thrown.

                Dim varexp As VariableClassification = m_Expression.Classification.AsVariableClassification
                Dim expType As Mono.Cecil.TypeReference = varexp.Type
                If CecilHelper.IsByRef(expType) Then
                    m_Expression = m_Expression.DereferenceByRef
                    expType = m_Expression.ExpressionType
                End If

                result = ResolveIndexInvocation(Me, expType) AndAlso result
            Case Else
                Helper.AddError(Me, "Some error...")
        End Select

        If result = False Then Return result

        If m_ExpressionType Is Nothing Then
            m_ExpressionType = Classification.GetType(True)
        End If

        Helper.Assert(m_ExpressionType IsNot Nothing)

        Return result
    End Function

    Public Overrides Function GetConstant(ByRef result As Object, ByVal ShowError As Boolean) As Boolean
        'Check for constant expression
        If m_AscWExpression IsNot Nothing Then
            If m_AscWExpression.GetConstant(result, False) Then
                result = Microsoft.VisualBasic.AscW(CChar(result))
                Return True
            End If
        ElseIf m_ArgumentList.Count = 1 AndAlso m_Expression.Classification.IsMethodGroupClassification Then
            Dim param As Object = Nothing
            Dim mgc As MethodGroupClassification = m_Expression.Classification.AsMethodGroupClassification

            If mgc.IsLateBound = False Then
                Dim mi As Mono.Cecil.MethodReference = mgc.ResolvedMethodInfo
                If mi IsNot Nothing Then
                    If m_ArgumentList(0).Expression IsNot Nothing Then
                        If m_ArgumentList(0).Expression.GetConstant(param, False) Then
                            If Compiler.NameResolver.IsConstantMethod(mi, param, param) Then
                                result = param
                                Return True
                            End If
                        End If
                    End If
                End If
            End If
        End If

        If ShowError Then Show30059()
        Return False
    End Function

    Private Function ResolveIndexInvocation(ByVal Context As ParsedObject, ByVal VariableType As Mono.Cecil.TypeReference) As Boolean
        Dim result As Boolean = True
        Dim Compiler As Compiler = Context.Compiler

        Dim defaultProperties As Mono.Collections.Generic.Collection(Of Mono.Cecil.PropertyReference) = Nothing

        If CecilHelper.IsArray(VariableType) Then
            result = ResolveArrayInvocation(Context, VariableType) AndAlso result
        ElseIf Helper.CompareType(VariableType, Compiler.TypeCache.System_Array) Then
            result = ResolveLateBoundArrayInvocation(Context) AndAlso result
        ElseIf Helper.IsDelegate(Compiler, VariableType) Then
            'This is an invocation expression (the classification can be reclassified as value and the expression is a delegate expression)
            result = ResolveDelegateInvocation(Context, VariableType)
        ElseIf Helper.HasDefaultProperty(Me, VariableType, defaultProperties) Then
            Dim propGroup As New PropertyGroupClassification(Me, m_Expression, defaultProperties)
            result = propGroup.ResolveGroup(m_ArgumentList)
            If result Then
                m_ArgumentList.ReplaceAndVerifyArguments(propGroup.FinalArguments, propGroup.ResolvedProperty, True)
            End If
            Classification = New PropertyAccessClassification(propGroup)
            'Classification = propGroup
        ElseIf Helper.CompareType(VariableType, Compiler.TypeCache.System_Object) Then
            Dim lbaClass As New LateBoundAccessClassification(Me, m_Expression, Nothing, Nothing)
            lbaClass.Arguments = m_ArgumentList
            Classification = lbaClass
        Else
            result = Compiler.Report.ShowMessage(Messages.VBNC30471, Location) AndAlso result
        End If

        Return result
    End Function

    Private Function ResolveLateBoundArrayInvocation(ByVal Context As ParsedObject) As Boolean
        Dim result As Boolean = True

        Classification = New LateBoundAccessClassification(Me, Expression, Nothing, Nothing)

        m_IsLateBoundArray = True

        Return result
    End Function

    Private Function ResolveArrayInvocation(ByVal Context As ParsedObject, ByVal ArrayType As Mono.Cecil.TypeReference) As Boolean
        Dim result As Boolean = True

        Helper.Assert(CecilHelper.IsArray(ArrayType))

        If m_ArgumentList.HasNamedArguments Then
            Compiler.Report.ShowMessage(Messages.VBNC30075, tm.CurrentLocation)
            Return False
        End If

        Dim arrayRank As Integer = CecilHelper.GetArrayRank(ArrayType)

        If m_ArgumentList.Count > arrayRank Then
            Compiler.Report.ShowMessage(Messages.VBNC30106, Location)
            Return False
        ElseIf m_ArgumentList.Count < arrayRank Then
            Compiler.Report.ShowMessage(Messages.VBNC30105, Location)
            Return False
        End If

        Dim isStrictOn As Boolean = Location.File(Compiler).IsOptionStrictOn

        For i As Integer = 0 To m_ArgumentList.Count - 1
            Dim arg As Argument = m_ArgumentList(i)
            Dim argtype As Mono.Cecil.TypeReference = arg.Expression.ExpressionType

            If Compiler.TypeResolution.IsImplicitlyConvertible(Context, argtype, Compiler.TypeCache.System_Int32) = False Then
                If isStrictOn Then
                    Helper.AddError(Me, "Array argument must be implicitly convertible to Integer.")
                    Return False
                End If
                Dim exp As Expression
                exp = Helper.CreateTypeConversion(Me, m_ArgumentList(i).Expression, Compiler.TypeCache.System_Int32, result)
                If result = False Then Return result
                m_ArgumentList(i).Expression = exp
            ElseIf Helper.CompareType(argtype, Compiler.TypeCache.System_Int32) = False Then
                Dim exp As Expression
                exp = Helper.CreateTypeConversion(Me, m_ArgumentList(i).Expression, Compiler.TypeCache.System_Int32, result)
                If result = False Then Return result
                If CecilHelper.IsByRef(exp.ExpressionType) Then
                    exp = New DeRefExpression(Me, exp)
                End If
                m_ArgumentList(i).Expression = exp
            End If
        Next

        Dim aT As Mono.Cecil.ArrayType = DirectCast(ArrayType, Mono.Cecil.ArrayType)
        m_ExpressionType = aT.ElementType

        Classification = New VariableClassification(Me, Me.m_Expression, m_ArgumentList)

        Return result
    End Function

    Private Function ResolveDelegateInvocation(ByVal Context As ParsedObject, ByVal DelegateType As Mono.Cecil.TypeReference) As Boolean
        Dim result As Boolean = True
        Dim invokeMethod As Mono.Cecil.MethodReference
        Dim params As Mono.Collections.Generic.Collection(Of ParameterDefinition)
        Dim argTypes As Generic.List(Of Mono.Cecil.TypeReference)
        Dim paramTypes() As Mono.Cecil.TypeReference
        Dim Compiler As Compiler = Context.Compiler

        invokeMethod = Helper.GetInvokeMethod(Compiler, DelegateType)
        params = Helper.GetParameters(Compiler, invokeMethod)
        paramTypes = Helper.GetTypes(params)
        argTypes = m_ArgumentList.GetTypes

        If argTypes.Count <> paramTypes.Length Then Return False

        For i As Integer = 0 To argTypes.Count - 1
            If Compiler.TypeResolution.IsImplicitlyConvertible(Context, argTypes(i), paramTypes(i)) = False Then
                Helper.AddError(Me, "Cannot convert implicitly from '" & argTypes(i).Name & "' to '" & paramTypes(i).Name & "'")
                Return False
            End If
        Next

        m_InvocationMethod = invokeMethod

        If invokeMethod.ReturnType IsNot Nothing Then
            Classification = New ValueClassification(Me, invokeMethod.ReturnType)
        Else
            Classification = New VoidClassification(Me)
        End If

        Return result
    End Function

    Private Function ResolvePropertyGroupInvocation() As Boolean
        Dim result As Boolean = True
        Dim propGroup As PropertyGroupClassification = m_Expression.Classification.AsPropertyGroup
        Dim tmpResult As Boolean

        tmpResult = propGroup.ResolveGroup(m_ArgumentList)

        If tmpResult = False Then
            tmpResult = ResolveReclassifyToValueThenIndex()

            Helper.StopIfDebugging(tmpResult = False)

            Return tmpResult
        Else
            result = m_ArgumentList.ReplaceAndVerifyArguments(propGroup.FinalArguments, propGroup.ResolvedProperty, True) AndAlso result
        End If

        Classification = New PropertyAccessClassification(propGroup)

        Return result
    End Function

    Private Function ResolveReclassifyToValueThenIndex() As Boolean
        Dim result As Boolean = True
        Dim tmpExp As Expression
        Dim oldExp As Expression

        tmpExp = m_Expression.ReclassifyToValueExpression
        result = tmpExp.ResolveExpression(ResolveInfo.Default(Parent.Compiler)) AndAlso result

        If result = False Then
            Helper.AddError(Me)
            Return False
        End If

        oldExp = m_Expression
        m_Expression = tmpExp
        result = ResolveIndexInvocation(Me, m_Expression.ExpressionType) AndAlso result

        Helper.StopIfDebugging(result = False)

        Return result
    End Function

    Private Function ResolveMethodInvocation() As Boolean
        Dim result As Boolean = True
        Dim mgc As MethodGroupClassification = m_Expression.Classification.AsMethodGroupClassification

        'If the method group only contains one method and that method takes no arguments and is a function, 
        'then the method group is interpreted as an invocation expression 
        'with an empty argument list and the result is used as the target of an index expression.

        Dim reclassifyToIndex As Boolean
        If mgc.Group.Count = 1 AndAlso m_ArgumentList.Count > 0 Then
            Dim method As Mono.Cecil.MethodReference = TryCast(mgc.Group(0), Mono.Cecil.MethodReference)

            reclassifyToIndex = method IsNot Nothing
            reclassifyToIndex = reclassifyToIndex AndAlso method.ReturnType IsNot Nothing
            reclassifyToIndex = reclassifyToIndex AndAlso Helper.CompareType(method.ReturnType, Compiler.TypeCache.System_Void) = False
            reclassifyToIndex = reclassifyToIndex AndAlso Helper.GetParameters(Compiler, method).Count = 0

        End If

        If reclassifyToIndex Then
            Return ResolveReclassifyToValueThenIndex()
        Else
            result = mgc.ResolveGroup(m_ArgumentList)
            If result Then
                If Not mgc.VerifyGroup(m_ArgumentList, True) Then Return False
            Else
                mgc.ResolveGroup(m_ArgumentList, True)
                Return False
            End If
        End If

        Helper.StopIfDebugging(result = False)

        If mgc.IsLateBound Then
            Dim lba As LateBoundAccessClassification = New LateBoundAccessClassification(Me, mgc.InstanceExpression, Nothing, mgc.Resolver.MethodName)
            lba.LateBoundType = mgc.Resolver.MethodDeclaringType
            lba.Arguments = m_ArgumentList
            Classification = lba
        ElseIf mgc.ResolvedMethodInfo IsNot Nothing Then
            Dim methodInfo As Mono.Cecil.MethodReference = mgc.ResolvedMethodInfo

            If String.IsNullOrEmpty(Compiler.CommandLine.VBRuntime) AndAlso Compiler.Assembly.IsDefinedHere(methodInfo) AndAlso CecilHelper.FindDefinition(methodInfo).IsStatic AndAlso Helper.CompareName(methodInfo.Name, "AscW") Then
                Dim methodParameters As Mono.Collections.Generic.Collection(Of ParameterDefinition) = Helper.GetParameters(Compiler, methodInfo)

                If methodParameters.Count <> 0 AndAlso Helper.CompareType(methodParameters(0).ParameterType, Compiler.TypeCache.System_Char) Then
                    m_AscWExpression = ArgumentList(0).Expression
                    m_ExpressionType = Compiler.TypeCache.System_Int32
                    Classification = New ValueClassification(Me, m_ExpressionType)

                    Return result
                End If
            End If

            If mgc.InstanceExpression Is Nothing AndAlso CecilHelper.IsStatic(methodInfo) = False Then
                Dim mae As MemberAccessExpression = TryCast(m_Expression, MemberAccessExpression)
                If mae IsNot Nothing AndAlso mae.FirstExpression.Classification.IsTypeClassification AndAlso mae.FirstExpression.Classification.AsTypeClassification.CanBeExpression Then
                    Dim exp As Expression = Nothing
                    result = mae.FirstExpression.Classification.AsTypeClassification.CreateAliasExpression(mae.FirstExpression, exp) AndAlso result
                    mgc.InstanceExpression = exp
                Else
                    result = Report.ShowMessage(Messages.VBNC30469, Me.Location)
                End If
            End If

            If methodInfo.ReturnType Is Nothing OrElse Helper.CompareType(methodInfo.ReturnType, Compiler.TypeCache.System_Void) Then
                Classification = New VoidClassification(Me)
            Else
                Classification = New ValueClassification(Me, methodInfo.ReturnType)
            End If
        ElseIf mgc.ResolvedConstructor IsNot Nothing Then
            Classification = New VoidClassification(Me)
        Else
            Throw New InternalException(Me)
        End If

        Return result
    End Function
    ''' <summary>
    ''' Returns the list of arguments for this expression.
    ''' Might be nothing.
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    ReadOnly Property ArgumentList() As ArgumentList
        Get
            Return m_ArgumentList
        End Get
    End Property
End Class

