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

    Private m_ConstantValue As Object
    Private m_ExpressionType As Type

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
    Private m_InvocationMethod As MethodInfo

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Shadows Sub Init(ByVal Expression As Expression, ByVal ArgumentList As ArgumentList)
        m_Expression = Expression
        m_ArgumentList = ArgumentList
    End Sub


    Public Overrides ReadOnly Property IsConstant() As Boolean
        Get
            If m_ConstantValue IsNot Nothing Then Return True
            If m_AscWExpression IsNot Nothing Then
                If m_AscWExpression.IsConstant Then
                    m_ConstantValue = Microsoft.VisualBasic.AscW(CChar(m_AscWExpression.ConstantValue))
                    Return True
                Else
                    Return False
                End If
            End If
            If m_ArgumentList.Count <> 1 Then Return False
            If m_Expression.Classification.IsMethodGroupClassification Then
                Dim param As Object
                Dim mi As MethodInfo = m_Expression.Classification.AsMethodGroupClassification.ResolvedMethodInfo

                If mi Is Nothing Then Return False
                If Not (m_ArgumentList(0).Expression IsNot Nothing AndAlso m_ArgumentList(0).Expression.IsConstant) Then Return False

                param = m_ArgumentList(0).Expression.ConstantValue
                If Compiler.NameResolver.IsConstantMethod(mi, param, m_ConstantValue) = False Then Return False

                Return True
            Else
                Return False
            End If
        End Get
    End Property

    Public Overrides ReadOnly Property ConstantValue() As Object
        Get
            If Me.IsConstant Then 'Necessary, since the property loads the constant value if it is a constant.
                Return m_ConstantValue
            Else
                Throw New InternalException(Me)
            End If
        End Get
    End Property

    ReadOnly Property Expression() As Expression
        Get
            Return m_Expression
        End Get
    End Property

    Overrides ReadOnly Property ExpressionType() As Type
        Get
            Helper.Assert(m_ExpressionType IsNot Nothing)
            Return m_ExpressionType
        End Get
    End Property

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        If m_AscWExpression IsNot Nothing Then
            result = m_AscWExpression.GenerateCode(Info.Clone(True, False, Compiler.TypeCache.Char)) AndAlso result
            Info.Stack.SwitchHead(Compiler.TypeCache.Char, Compiler.TypeCache.Integer)

            Return result
        End If

        If m_InvocationMethod IsNot Nothing Then
            result = Helper.EmitArgumentsAndCallOrCallVirt(Info, m_Expression, m_ArgumentList, m_InvocationMethod) AndAlso result
            Return True
        End If

        Select Case m_Expression.Classification.Classification
            Case ExpressionClassification.Classifications.MethodGroup
                With m_Expression.Classification.AsMethodGroupClassification
                    result = Helper.EmitArgumentsAndCallOrCallVirt(Info, .InstanceExpression, m_ArgumentList, .ResolvedMethod)
                End With
            Case ExpressionClassification.Classifications.Value
                If Info.IsRHS Then
                    If Me.Classification.IsVariableClassification Then
                        result = Me.Classification.GenerateCode(Info) AndAlso result
                    Else
                        result = m_Expression.GenerateCode(Info) AndAlso result
                    End If
                Else
                    Helper.NotImplemented()
                End If
            Case ExpressionClassification.Classifications.PropertyAccess
                If Info.IsRHS Then
                    If Me.Classification.IsVariableClassification Then
                        result = Me.Classification.GenerateCode(Info) AndAlso result
                    Else
                        result = m_Expression.GenerateCode(Info) AndAlso result
                    End If
                Else
                    Helper.NotImplemented()
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
                    Else
                        result = Classification.AsVariableClassification.GenerateCodeAsValue(Info) AndAlso result
                    End If
                Else
                    result = Classification.GenerateCode(Info) AndAlso result
                End If
            Case Else
                Throw New InternalException(Me)
        End Select

        Return result
    End Function

    Shared Function IsBinaryMe(ByVal tm As tm) As Boolean
        Return tm.CurrentToken.Equals(KS.LParenthesis)
    End Function

    Shared Function CreateAndParseTo(ByRef result As Expression) As Boolean
        Helper.NotImplemented()
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        result = m_Expression.ResolveTypeReferences AndAlso result
        If m_ArgumentList IsNot Nothing Then result = m_ArgumentList.ResolveTypeReferences AndAlso result

        Return result
    End Function

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = m_Expression.ResolveExpression(New ResolveInfo(Info.Compiler, True)) AndAlso result
        If m_ArgumentList IsNot Nothing Then result = m_ArgumentList.ResolveCode(ResolveInfo.Default(Info.Compiler)) AndAlso result

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
                    Case ExpressionClassification.Classifications.PropertyGroup
                        m_ArgumentList(i).Expression = m_ArgumentList(i).Expression.ReclassifyToPropertyAccessExpression
                        result = m_ArgumentList(i).Expression.ResolveExpression(ResolveInfo.Default(Info.Compiler)) AndAlso result
                    Case Else
                        'reclassify to value
                        If m_ArgumentList(i).Expression.Classification.CanBeValueClassification Then
                            m_ArgumentList(i).Expression = m_ArgumentList(i).Expression.ReclassifyToValueExpression
                            result = m_ArgumentList(i).Expression.ResolveExpression(ResolveInfo.Default(Info.Compiler)) AndAlso result
                        Else
                            Helper.AddError()
                        End If
                End Select
            End If
        Next

        Select Case m_Expression.Classification.Classification
            Case ExpressionClassification.Classifications.MethodGroup
                'This is an invocation expression.
                result = ResolveMethodInvocation() AndAlso result

            Case ExpressionClassification.Classifications.Value
                If m_Expression.ExpressionType.IsArray Then
                    result = ResolveArrayInvocation(m_Expression.ExpressionType) AndAlso result
                Else
                    result = ResolveIndexInvocation(Info.compiler, m_Expression.ExpressionType) AndAlso result
                End If
            Case ExpressionClassification.Classifications.PropertyAccess
                If m_Expression.ExpressionType.IsArray Then
                    result = ResolveArrayInvocation(m_Expression.ExpressionType) AndAlso result
                Else
                    result = ResolveIndexInvocation(Info.compiler, m_Expression.ExpressionType) AndAlso result
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
                Dim expType As Type = varexp.Type
                If expType.IsByRef Then
                    m_Expression = m_Expression.DereferenceByRef
                    expType = m_Expression.ExpressionType
                End If

                result = ResolveIndexInvocation(Info.compiler, expType) AndAlso result
            Case Else
                Helper.AddError("Some error...")
        End Select

        If m_ExpressionType Is Nothing Then
            m_ExpressionType = Classification.GetType(True)
        End If

        Helper.Assert(m_ExpressionType IsNot Nothing)

        Return result
    End Function

    Private Function ResolveIndexInvocation(ByVal Compiler As Compiler, ByVal VariableType As Type) As Boolean
        Dim result As Boolean = True

        Dim defaultProperties As Generic.List(Of PropertyInfo) = Nothing

        If VariableType.IsArray Then
            result = ResolveArrayInvocation(VariableType) AndAlso result
        ElseIf Helper.IsDelegate(Compiler, VariableType) Then
            'This is an invocation expression (the classification can be reclassified as value and the expression is a delegate expression)
            result = ResolveDelegateInvocation(Compiler, VariableType)
        ElseIf Helper.HasDefaultProperty(Compiler, VariableType, defaultProperties) Then
            Dim propGroup As New PropertyGroupClassification(Me, m_Expression, defaultProperties)
            result = propGroup.ResolveGroup(m_ArgumentList)
            Classification = propGroup
        Else
            result = False
            Helper.NotImplemented()
        End If

        Return result
    End Function

    Private Function ResolveArrayInvocation(ByVal ArrayType As Type) As Boolean
        Dim result As Boolean = True

        Helper.Assert(ArrayType.IsArray)

        Dim argtypes As Generic.List(Of Type)

        If m_ArgumentList.HasNamedArguments Then
            Helper.AddError("Array invocation cannot have named arguments.")
            Return False
        End If

        If ArrayType.GetArrayRank <> m_ArgumentList.Count Then
            Helper.AddError("Array dimensions are not correct.")
            Return False
        End If

        argtypes = m_ArgumentList.GetTypes
        For Each argtype As Type In argtypes
            If Compiler.TypeResolution.IsImplicitlyConvertible(Compiler, argtype, Compiler.TypeCache.Integer) = False Then
                Helper.AddError("Array argument must be implicitly convertible to Integer.")
                Return False
            End If
        Next

        m_ExpressionType = ArrayType.GetElementType

        Classification = New VariableClassification(Me, Me.m_Expression, m_ArgumentList)

        Return result
    End Function

    Private Function ResolveDelegateInvocation(ByVal Compiler As Compiler, ByVal DelegateType As Type) As Boolean
        Dim result As Boolean = True
        Dim invokeMethod As MethodInfo
        Dim params() As ParameterInfo
        Dim argTypes As Generic.List(Of Type)
        Dim paramTypes() As Type

        invokeMethod = Helper.GetInvokeMethod(compiler, DelegateType)
        params = Helper.GetParameters(compiler, invokeMethod)
        paramTypes = Helper.GetTypes(params)
        argTypes = m_ArgumentList.GetTypes

        If argTypes.Count <> paramTypes.Length Then Return False

        For i As Integer = 0 To argTypes.Count - 1
            If Compiler.TypeResolution.IsImplicitlyConvertible(Compiler, argTypes(i), paramTypes(i)) = False Then
                Helper.AddError("Cannot convert implicitly from '" & argTypes(i).Name & "' to '" & paramTypes(i).Name & "'")
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

        Dim tmpResult As Boolean
        tmpResult = m_Expression.Classification.AsPropertyGroup.ResolveGroup(m_ArgumentList)

        If tmpResult = False Then
            tmpResult = ResolveReclassifyToValueThenIndex()

            Helper.StopIfDebugging(tmpResult = False)

            Return tmpResult
        End If

        Classification = New PropertyAccessClassification(m_Expression.Classification.AsPropertyGroup)

        Return result
    End Function

    Private Function ResolveReclassifyToValueThenIndex() As Boolean
        Dim result As Boolean = True
        Dim tmpExp As Expression
        Dim oldExp As Expression

        tmpExp = m_Expression.ReclassifyToValueExpression
        result = tmpExp.ResolveExpression(ResolveInfo.Default(Parent.Compiler)) AndAlso result

        If result = False Then
            Helper.AddError()
            Return False
        End If

        oldExp = m_Expression
        m_Expression = tmpExp
        result = ResolveIndexInvocation(Compiler, m_Expression.ExpressionType) AndAlso result

        Helper.StopIfDebugging(result = False)

        Return result
    End Function

    Private Function ResolveMethodInvocation() As Boolean
        Dim result As Boolean = True
        Dim mgc As MethodGroupClassification = m_Expression.Classification.AsMethodGroupClassification

        Dim tmpResult As Boolean
        Dim finalArguments As Generic.List(Of Argument) = Nothing
        tmpResult = mgc.ResolveGroup(m_ArgumentList, finalArguments)
        If tmpResult = False Then
            tmpResult = ResolveReclassifyToValueThenIndex()

            Helper.StopIfDebugging(tmpResult = False)

            Return tmpResult
        Else
            m_ArgumentList.ReplaceAndVerifyArguments(finalArguments, mgc.ResolvedMethod)
        End If

        If mgc.ResolvedMethodInfo IsNot Nothing Then
            Dim methodInfo As MethodInfo = mgc.ResolvedMethodInfo

            If Compiler.CommandLine.NoVBRuntimeRef AndAlso methodInfo.DeclaringType.Module Is Compiler.ModuleBuilder AndAlso methodInfo.IsStatic AndAlso Helper.CompareNameOrdinal(methodInfo.Name, "AscW") Then
                Dim methodParameters() As ParameterInfo = Helper.GetParameters(Compiler, methodInfo)

                If methodParameters.Length <> 0 AndAlso Helper.CompareType(methodParameters(0).ParameterType, Compiler.TypeCache.Char) Then
                    m_AscWExpression = ArgumentList(0).Expression
                    m_ExpressionType = Compiler.TypeCache.Integer
                    Classification = New ValueClassification(Me, m_ExpressionType)

                    Return result
                End If
            End If

            If methodInfo.ReturnType Is Nothing Then
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

#If DEBUG Then
    Public Overrides Sub Dump(ByVal Dumper As IndentedTextWriter)
        m_Expression.Dump(Dumper)
        If m_ArgumentList IsNot Nothing Then
            Dumper.Write("(")
            Compiler.Dumper.Dump(m_ArgumentList)
            Dumper.Write(")")
        End If
    End Sub
#End If
End Class
