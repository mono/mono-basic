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

    Private m_ExpressionType As Type

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

    Private Function GetValueType(ByVal tp As Type) As Type
        Helper.Assert(tp IsNot Nothing)
        If tp.IsByRef Then
            Return tp.GetElementType
        Else
            Return tp
        End If
    End Function

    ReadOnly Property LeftType() As Type
        Get
            Return GetValueType(m_LeftExpression.ExpressionType)
        End Get
    End Property

    ReadOnly Property RightType() As Type
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

    ReadOnly Property OperandType() As Type
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
            If DoOperatorOverloading() = False Then
                If (Me.Keyword = KS.ShiftLeft OrElse Me.Keyword = KS.ShiftRight) AndAlso Helper.CompareType(Me.LeftType, Compiler.TypeCache.System_Char) = False AndAlso Helper.CompareType(Me.LeftType, Compiler.TypeCache.System_DateTime) = False Then
                    If Helper.CompareType(Me.RightType, Compiler.TypeCache.System_Char) Then
                        Compiler.Report.ShowMessage(Messages.VBNC32006, Location, Me.LeftType.Name)
                    ElseIf Helper.CompareType(Me.RightType, Compiler.TypeCache.System_DateTime) Then
                        Compiler.Report.ShowMessage(Messages.VBNC30311, Location, Me.LeftType.Name, Me.RightType.Name)
                    Else
                        Compiler.Report.ShowMessage(Messages.VBNC30452, Location, Enums.GetKSStringAttribute(Me.Keyword).FriendlyValue, Me.LeftType.Name, Me.RightType.Name)
                    End If
                Else
                    Compiler.Report.ShowMessage(Messages.VBNC30452, Location, Enums.GetKSStringAttribute(Me.Keyword).FriendlyValue, Me.LeftType.Name, Me.RightType.Name)
                End If
                result = False
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

            If isLeftIntrinsic AndAlso isRightIntrinsic OrElse IsOverloadable = False Then
                Dim destinationType As Type
                m_ExpressionType = Compiler.TypeResolution.TypeCodeToType(TypeConverter.GetBinaryResultType(Keyword, LeftTypeCode, RightTypeCode))
                If LeftTypeCode <> leftOperandType Then
                    destinationType = Compiler.TypeResolution.TypeCodeToType(leftOperandType)
                    m_LeftExpression = Helper.CreateTypeConversion(Me, m_LeftExpression, destinationType, result)
                End If

                If RightTypeCode <> rightOperandType Then
                    destinationType = Compiler.TypeResolution.TypeCodeToType(rightOperandType)
                    m_RightExpression = Helper.CreateTypeConversion(Me, m_RightExpression, destinationType, result)
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
                result = DoOperatorOverloading() AndAlso result
            End If
        End If

        Return result
    End Function

    Function DoOperatorOverloading() As Boolean
        Dim result As Boolean = True
        Dim methods As New Generic.List(Of MethodInfo)
        Dim methodClassification As MethodGroupClassification
        methods = Helper.GetBinaryOperators(Compiler, CType(Me.Keyword, BinaryOperators), Me.LeftType)
        If Helper.CompareType(Me.LeftType, Me.RightType) = False Then
            Dim methods2 As New Generic.List(Of MethodInfo)
            methods2 = Helper.GetBinaryOperators(Compiler, CType(Me.Keyword, BinaryOperators), Me.RightType)
            For Each method As MethodInfo In methods2
                If methods.Contains(method) = False Then methods.Add(method)
            Next
        End If
        If methods.Count = 0 Then
            result = Compiler.Report.ShowMessage(Messages.VBNC30452, Me.Location, Enums.GetKSStringAttribute(Me.Keyword).Value, Me.LeftType.FullName, Me.RightType.FullName) AndAlso result
            If result = False Then Return result
        End If
        methodClassification = New MethodGroupClassification(Me, Nothing, New Expression() {Me.m_LeftExpression, Me.m_RightExpression}, methods.ToArray)
        result = methodClassification.ResolveGroup(New ArgumentList(Me, Me.m_LeftExpression, m_RightExpression), Nothing) AndAlso result
        result = methodClassification.SuccessfullyResolved AndAlso result
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

    Public Overrides ReadOnly Property IsConstant() As Boolean
        Get
            Return m_LeftExpression.IsConstant AndAlso m_RightExpression.IsConstant
        End Get
    End Property

    Protected Sub New(ByVal Parent As ParsedObject)
        MyBase.new(Parent)
    End Sub

    Protected Sub New(ByVal Parent As ParsedObject, ByVal LExp As Expression, ByVal RExp As Expression)
        MyBase.New(Parent)
        m_LeftExpression = LExp
        m_RightExpression = RExp
    End Sub

    Protected Sub Init(ByVal LExp As Expression, ByVal RExp As Expression)
        m_LeftExpression = LExp
        m_RightExpression = RExp
    End Sub

    Overrides ReadOnly Property ExpressionType() As Type
        Get
            Return m_ExpressionType
        End Get
    End Property

#If DEBUG Then
    Protected MustOverride Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
#End If
    MustOverride ReadOnly Property Keyword() As KS

#If DEBUG Then
    Public Overrides Sub Dump(ByVal Dumper As IndentedTextWriter)
        m_LeftExpression.Dump(Dumper)
        Dumper.Write(" " & Enums.GetKSStringAttribute(Me.Keyword).FriendlyValue & " ")
        m_RightExpression.Dump(Dumper)
    End Sub
#End If
End Class
