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
''' (not in documentation)
''' BinaryExpression ::= 
'''   AndAlsoExpression | AndExpression | BinaryAddExpression | BinarySubExpression |
'''   ConcatExpression | EqualsExpression | ExponentExpression | GEExpression |
'''   GTExpression | IntDivisionExpression | IsExpression | IsNotExpression |
'''   LEExpression | LikeExpression | LShiftExpression | LTExpression |
'''   ModExpression | MultExpression | NotEqualsExpression | OrElseExpression |
'''   OrExpression | RealDivisionExpression | RShiftExpression | XOrExpression
''' </summary>
''' <remarks></remarks>
Public MustInherit Class BinaryExpression
    Inherits OperatorExpression

    Protected m_LeftExpression As Expression
    Protected m_RightExpression As Expression

    Private m_ExpressionType As Mono.Cecil.TypeReference

    ''' <summary>
    ''' If this is an overloadable operator.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Overridable ReadOnly Property IsOverloadable() As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        result = m_LeftExpression.ResolveTypeReferences AndAlso result
        result = m_RightExpression.ResolveTypeReferences AndAlso result

        Return result
    End Function

    Private Function GetValueType(ByVal tp As Mono.Cecil.TypeReference) As Mono.Cecil.TypeReference
        Helper.Assert(tp IsNot Nothing)
        If TypeOf tp Is ByReferenceType Then
            Return tp.GetElementType
        Else
            Return tp
        End If
    End Function

    ReadOnly Property LeftType() As Mono.Cecil.TypeReference
        Get
            Return GetValueType(m_LeftExpression.ExpressionType)
        End Get
    End Property

    ReadOnly Property RightType() As Mono.Cecil.TypeReference
        Get
            Return GetValueType(m_RightExpression.ExpressionType)
        End Get
    End Property

    ReadOnly Property LeftTypeCode() As TypeCode
        Get
            Return Helper.GetTypeCode(Compiler, LeftType)
        End Get
    End Property

    ReadOnly Property RightTypeCode() As TypeCode
        Get
            Return Helper.GetTypeCode(Compiler, RightType)
        End Get
    End Property

    Shared Function IsMe(ByVal tm As tm) As Boolean
        Return tm.CurrentToken.Equals(Enums.BinaryOperators)
    End Function

    Protected Sub ValidateBeforeGenerateCode(ByVal Info As EmitInfo)
        Helper.Assert(m_LeftExpression.Classification.CanBeValueClassification, "Left expression cannot be a value classification: " & m_LeftExpression.Classification.Classification.ToString)
        Helper.Assert(m_RightExpression.Classification.CanBeValueClassification, "Right expression cannot be a value classification: " & m_RightExpression.Classification.Classification.ToString)
        Helper.Assert(Info.IsRHS, "Expression is not a right hand side expression.")
    End Sub

    ReadOnly Property OperandType() As Mono.Cecil.TypeReference
        Get
            Return Compiler.TypeResolution.TypeCodeToType(OperandTypeCode)
        End Get
    End Property

    ReadOnly Property OperandTypeCode() As TypeCode
        Get
            Dim result As TypeCode

            result = TypeConverter.GetBinaryOperandType(Compiler, Me.Keyword, LeftType, RightType)

            Return result
        End Get
    End Property

    Protected Overridable Function ResolveExpressions(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = m_LeftExpression.ResolveExpression(Info) AndAlso result
        result = m_RightExpression.ResolveExpression(Info) AndAlso result

        If result = False Then Return False

        If m_LeftExpression.Classification.IsValueClassification = False Then
            result = Helper.VerifyValueClassification(m_LeftExpression, Info) AndAlso result
        End If

        If m_RightExpression.Classification.IsValueClassification = False Then
            result = Helper.VerifyValueClassification(m_RightExpression, Info) AndAlso result
        End If
        Return result
    End Function

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True
        Dim operandType As TypeCode
        Dim rightOperandType As TypeCode
        Dim leftOperandType As TypeCode

        result = ResolveExpressions(Info) AndAlso result

        If result = False Then Return False

        If Helper.CompareType(m_LeftExpression.ExpressionType, m_RightExpression.ExpressionType) = False Then
            If Helper.CompareType(m_LeftExpression.ExpressionType, Compiler.TypeCache.Nothing) Then
                m_LeftExpression = New CTypeExpression(Me, m_LeftExpression, m_RightExpression.ExpressionType)
                result = m_LeftExpression.ResolveExpression(ResolveInfo.Default(Info.Compiler)) AndAlso result
            ElseIf Helper.CompareType(m_RightExpression.ExpressionType, Compiler.TypeCache.Nothing) Then
                m_RightExpression = New CTypeExpression(Me, m_RightExpression, m_LeftExpression.ExpressionType)
                result = m_RightExpression.ResolveExpression(ResolveInfo.Default(Info.Compiler)) AndAlso result
            End If
        End If

        operandType = Me.OperandTypeCode
        rightOperandType = Me.RightOperandTypeCode
        leftOperandType = Me.LeftOperandTypeCode

        If operandType = TypeCode.Empty Then
            'Try operator overloading
            Dim showErrors As Boolean = Me.RightTypeCode = TypeCode.DBNull OrElse Me.LeftTypeCode = TypeCode.DBNull

            result = DoOperatorOverloading(showErrors) AndAlso result

            If result = False AndAlso showErrors = False Then
                If (Me.Keyword = KS.ShiftLeft OrElse Me.Keyword = KS.ShiftRight) AndAlso Helper.CompareType(Me.LeftType, Compiler.TypeCache.System_Char) = False AndAlso Helper.CompareType(Me.LeftType, Compiler.TypeCache.System_DateTime) = False Then
                    If Helper.CompareType(Me.RightType, Compiler.TypeCache.System_Char) Then
                        Compiler.Report.ShowMessage(Messages.VBNC32006, Location, Helper.ToString(Compiler, Compiler.TypeCache.System_Int32))
                    ElseIf Helper.CompareType(Me.RightType, Compiler.TypeCache.System_DateTime) Then
                        Compiler.Report.ShowMessage(Messages.VBNC30311, Location, Helper.ToString(Compiler, Me.RightType), Helper.ToString(Compiler, Compiler.TypeCache.System_Int32))
                    Else
                        Compiler.Report.ShowMessage(Messages.VBNC30452, Location, Enums.strSpecial(Me.Keyword), Helper.ToString(Compiler, Compiler.TypeCache.System_Int32), Helper.ToString(Compiler, Me.RightType))
                    End If
                Else
                    If Me.Keyword = KS.AndAlso AndAlso (Me.LeftTypeCode = TypeCode.DBNull OrElse Me.RightTypeCode = TypeCode.DBNull) Then
                        Compiler.Report.ShowMessage(Messages.VBNC30452, Location, "And", Helper.ToString(Compiler, Me.LeftType), Helper.ToString(Compiler, Me.RightType))
                    ElseIf Me.Keyword = KS.OrElse AndAlso (Me.LeftTypeCode = TypeCode.DBNull OrElse Me.RightTypeCode = TypeCode.DBNull) Then
                        Compiler.Report.ShowMessage(Messages.VBNC30452, Location, "Or", Helper.ToString(Compiler, Me.LeftType), Helper.ToString(Compiler, Me.RightType))
                    Else
                        Compiler.Report.ShowMessage(Messages.VBNC30452, Location, Enums.strSpecial(Me.Keyword), Helper.ToString(Compiler, Me.LeftType), Helper.ToString(Compiler, Me.RightType))
                    End If
                End If
            End If
        Else
            'If X and Y are both intrinsic types, look up the result type in our operator tables and use that.
            'If X is an intrinsic type, then
            '- Collect all of the intrinsic types that Y converts to.
            '- Choose the most encompassed type, T, from the list. If there is no single most encompassed type, then we don't consider an intrinsic operator.
            '- Lookup up the intrinsic operator for X and T, and call it O. If there is no intrinsic operator defined for those two types, then we don't consider an intrinsic operator.
            '- The set of operators to be considered is all the user-defined operators in Y, plus O, if it exists.
            'If Y is an intrinsic type, then perform the same steps as for X (obviously, both can't be intrinsic types at this point).
            'Do overload resolution on the set of operators to be considered.
            Dim isLeftIntrinsic As Boolean = Me.LeftTypeCode <> TypeCode.Object OrElse Helper.CompareType(Compiler.TypeCache.System_Object, Me.LeftType)
            Dim isRightIntrinsic As Boolean = Me.RightTypeCode <> TypeCode.Object OrElse Helper.CompareType(Compiler.TypeCache.System_Object, Me.RightType)
            Dim doOpOverloading As Boolean = False
            Dim isStrict As Boolean?

            If isLeftIntrinsic AndAlso isRightIntrinsic OrElse IsOverloadable = False Then
                Dim destinationType As Mono.Cecil.TypeReference
                m_ExpressionType = Compiler.TypeResolution.TypeCodeToType(TypeConverter.GetBinaryResultType(Keyword, LeftTypeCode, RightTypeCode))

                If Keyword <> KS.Is AndAlso Keyword <> KS.IsNot Then
                    If Location.File(Compiler).IsOptionStrictOn Then
                        If Helper.CompareType(m_LeftExpression.ExpressionType, Compiler.TypeCache.System_Object) Then
                            If Keyword = KS.Equals OrElse Keyword = KS.NotEqual Then
                                result = Compiler.Report.ShowMessage(Messages.VBNC32013, Me.Location, Enums.strSpecial(Keyword))
                            Else
                                result = Compiler.Report.ShowMessage(Messages.VBNC30038, Me.Location, Enums.strSpecial(Keyword))
                            End If
                        End If
                        If Helper.CompareType(m_RightExpression.ExpressionType, Compiler.TypeCache.System_Object) Then
                            If Keyword = KS.Equals OrElse Keyword = KS.NotEqual Then
                                result = Compiler.Report.ShowMessage(Messages.VBNC32013, Me.Location, Enums.strSpecial(Keyword))
                            Else
                                result = Compiler.Report.ShowMessage(Messages.VBNC30038, Me.Location, Enums.strSpecial(Keyword))
                            End If
                        End If
                    End If
                End If

                If LeftTypeCode = TypeCode.String AndAlso RightTypeCode = TypeCode.String AndAlso (Keyword = KS.Concat OrElse Keyword = KS.Add) Then
                    isStrict = False
                End If

                If LeftTypeCode <> leftOperandType Then
                    destinationType = Compiler.TypeResolution.TypeCodeToType(leftOperandType)
                    result = Helper.IsConvertible(Me, m_LeftExpression, m_LeftExpression.ExpressionType, destinationType, True, m_LeftExpression, True, isStrict)
                End If

                If RightTypeCode <> rightOperandType Then
                    destinationType = Compiler.TypeResolution.TypeCodeToType(rightOperandType)
                    result = Helper.IsConvertible(Me, m_RightExpression, m_RightExpression.ExpressionType, destinationType, True, m_RightExpression, True, isStrict)
                End If
                Classification = New ValueClassification(Me)

            ElseIf isRightIntrinsic = False AndAlso isLeftIntrinsic = True Then
                Dim convertsTo As TypeCode() = TypeResolution.GetIntrinsicTypesImplicitlyConvertibleFrom(Compiler, RightType)
                convertsTo = Helper.GetMostEncompassedTypes(Compiler, convertsTo)

                If convertsTo IsNot Nothing AndAlso convertsTo.Length = 1 Then
                    m_RightExpression = Helper.CreateTypeConversion(Me, m_RightExpression, Compiler.TypeResolution.TypeCodeToType(convertsTo(0)), result)
                    m_ExpressionType = Compiler.TypeResolution.TypeCodeToType(TypeConverter.GetBinaryResultType(Keyword, LeftTypeCode, RightTypeCode))
                    Classification = New ValueClassification(Me)
                Else
                    doOpOverloading = True
                End If
            ElseIf isRightIntrinsic = True AndAlso isLeftIntrinsic = False Then
                Dim convertsTo As TypeCode() = TypeResolution.GetIntrinsicTypesImplicitlyConvertibleFrom(Compiler, LeftType)
                convertsTo = Helper.GetMostEncompassedTypes(Compiler, convertsTo)

                If convertsTo IsNot Nothing AndAlso convertsTo.Length = 1 Then
                    m_LeftExpression = Helper.CreateTypeConversion(Me, m_LeftExpression, Compiler.TypeResolution.TypeCodeToType(convertsTo(0)), result)
                    m_ExpressionType = Compiler.TypeResolution.TypeCodeToType(TypeConverter.GetBinaryResultType(Keyword, LeftTypeCode, RightTypeCode))
                    Classification = New ValueClassification(Me)
                Else
                    doOpOverloading = True
                End If
            Else
                doOpOverloading = True
            End If

            If doOpOverloading Then
                result = DoOperatorOverloading(True) AndAlso result
            End If
        End If

        Return result
    End Function

    Function DoOperatorOverloading(ByVal ShowErrors As Boolean) As Boolean
        Dim result As Boolean = True
        Dim methods As New Generic.List(Of Mono.Cecil.MethodReference)
        Dim methodClassification As MethodGroupClassification
        Dim arguments As ArgumentList

        If Me.Keyword = KS.AndAlso OrElse Me.Keyword = KS.OrElse Then
            If ShowErrors Then
                If Me.Keyword = KS.AndAlso AndAlso (Me.LeftTypeCode = TypeCode.DBNull OrElse Me.RightTypeCode = TypeCode.DBNull) Then
                    Compiler.Report.ShowMessage(Messages.VBNC30452, Location, "And", Helper.ToString(Compiler, Me.LeftType), Helper.ToString(Compiler, Me.RightType))
                ElseIf Me.Keyword = KS.OrElse AndAlso (Me.LeftTypeCode = TypeCode.DBNull OrElse Me.RightTypeCode = TypeCode.DBNull) Then
                    Compiler.Report.ShowMessage(Messages.VBNC30452, Location, "Or", Helper.ToString(Compiler, Me.LeftType), Helper.ToString(Compiler, Me.RightType))
                Else
                    Compiler.Report.ShowMessage(Messages.VBNC30452, Location, Enums.strSpecial(Me.Keyword), Helper.ToString(Compiler, Me.LeftType), Helper.ToString(Compiler, Me.RightType))
                End If
            End If
            Return False
        End If

        If Me.Keyword = KS.Minus Then
            'This is a special case, because Date has a custom - operator that takes (Date, Date), and both string and object
            'are implicitly convertible to Date, so if we don't special case it we'll end up using that operator
            If (Me.LeftTypeCode = TypeCode.String OrElse Helper.CompareType(Me.LeftType, Compiler.TypeCache.System_Object)) AndAlso Me.RightTypeCode = TypeCode.DateTime Then
                If ShowErrors Then
                    Compiler.Report.ShowMessage(Messages.VBNC30452, Location, "-", Helper.ToString(Compiler, Me.LeftType), Helper.ToString(Compiler, Me.RightType))
                End If
                Return False
            ElseIf Me.LeftTypeCode = TypeCode.DateTime AndAlso (Me.RightTypeCode = TypeCode.String OrElse Helper.CompareType(Me.RightType, Compiler.TypeCache.System_Object)) Then
                If ShowErrors Then
                    Compiler.Report.ShowMessage(Messages.VBNC30452, Location, "-", Helper.ToString(Compiler, Me.LeftType), Helper.ToString(Compiler, Me.RightType))
                End If
                Return False
            End If
        End If

        methods = Helper.GetBinaryOperators(Compiler, CType(Me.Keyword, BinaryOperators), Me.LeftType)
        If Helper.CompareType(Me.LeftType, Me.RightType) = False Then
            Dim methods2 As New Generic.List(Of Mono.Cecil.MethodReference)
            methods2 = Helper.GetBinaryOperators(Compiler, CType(Me.Keyword, BinaryOperators), Me.RightType)
            For Each method As Mono.Cecil.MethodReference In methods2
                If methods.Contains(method) = False Then methods.Add(method)
            Next
        End If
        If methods.Count = 0 Then
            If ShowErrors Then Compiler.Report.ShowMessage(Messages.VBNC30452, Me.Location, Enums.strSpecial(Me.Keyword), Helper.ToString(Compiler, Me.LeftType), Helper.ToString(Compiler, Me.RightType))
            Return False
        End If
        methodClassification = New MethodGroupClassification(Me, Nothing, Nothing, New Expression() {Me.m_LeftExpression, Me.m_RightExpression}, methods.ToArray)
        arguments = New ArgumentList(Me, Me.m_LeftExpression, m_RightExpression)
        result = methodClassification.ResolveGroup(arguments, ShowErrors, False) AndAlso result
        result = methodClassification.SuccessfullyResolved AndAlso result
        If result = False Then Return result
        result = methodClassification.VerifyGroup(arguments, ShowErrors) AndAlso result
        If result = False Then Return result
        m_ExpressionType = methodClassification.ResolvedMethodInfo.ReturnType
        Classification = methodClassification
        Return result
    End Function

    Overridable ReadOnly Property RightOperandTypeCode() As TypeCode
        Get
            Return OperandTypeCode
        End Get
    End Property

    Overridable ReadOnly Property LeftOperandTypeCode() As TypeCode
        Get
            Return OperandTypeCode
        End Get
    End Property

    Protected Sub New(ByVal Parent As ParsedObject)
        MyBase.new(Parent)
    End Sub

    Protected Sub New(ByVal Parent As ParsedObject, ByVal LExp As Expression, ByVal RExp As Expression)
        MyBase.New(Parent)
        m_LeftExpression = LExp
        m_LeftExpression.Parent = Me
        m_RightExpression = RExp
        m_RightExpression.Parent = Me
    End Sub

    Protected Sub Init(ByVal LExp As Expression, ByVal RExp As Expression)
        m_LeftExpression = LExp
        m_RightExpression = RExp
    End Sub

    Overrides ReadOnly Property ExpressionType() As Mono.Cecil.TypeReference
        Get
            Return m_ExpressionType
        End Get
    End Property

    Public Overrides Function GetConstant(ByRef result As Object, ByVal ShowError As Boolean) As Boolean
        Dim rvalue As Object = Nothing
        Dim lvalue As Object = Nothing

        If Not m_LeftExpression.GetConstant(lvalue, ShowError) Then Return False
        If Not m_RightExpression.GetConstant(rvalue, ShowError) Then Return False

        If lvalue Is Nothing Or rvalue Is Nothing Then
            result = Nothing
            Return True
        End If

        If Not GetConstant(result, lvalue, rvalue) Then
            If ShowError Then Show30059()
            Return False
        End If

        Return True
    End Function

    Public Overridable Overloads Function GetConstant(ByRef m_ConstantValue As Object, ByVal lvalue As Object, ByVal rvalue As Object) As Boolean
        Return False
    End Function

#If DEBUG Then
    Protected MustOverride Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
#End If
    MustOverride ReadOnly Property Keyword() As KS
End Class

