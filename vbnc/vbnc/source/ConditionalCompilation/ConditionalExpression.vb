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

Option Compare Text

Public Class ConditionalExpression
    Inherits BaseObject

    Private m_Compiler As ConditionalCompiler

    <Obsolete()> Friend Overrides ReadOnly Property tm() As tm
        Get
            Return MyBase.tm 'Throw New InternalException("Don't use this")
        End Get
    End Property

    ReadOnly Property CurrentConstants() As ConditionalConstants
        Get
            Return m_Compiler.CurrentConstants
        End Get
    End Property

    Public Sub New(ByVal Compiler As ConditionalCompiler)
        MyBase.New(Compiler)
        m_Compiler = Compiler
    End Sub

    ReadOnly Property Reader() As ITokenReader
        Get
            Return m_Compiler.Reader
        End Get
    End Property

    Function RuleIdentifier(ByRef Result As Object) As Boolean
        'A value of 0 evaluates as false, anything else as true
        If Reader.Peek.Equals(KS.Nothing) Then
            Result = Nothing
            Reader.Next()
        ElseIf Reader.Peek.IsLiteral Then
            Dim tp As TypeCode = Reader.Peek.AsLiteral.LiteralType
            Select Case tp
                Case TypeCode.String
                    Result = Reader.Peek.AsStringLiteral.Literal
                Case TypeCode.Object
                    Throw New InternalException("Shouldn't happen, Nothing is a keyword.")
                Case TypeCode.Boolean
                    Throw New InternalException("Shouldn't happen, True and False are keywords.")
                Case TypeCode.DateTime
                    Result = Reader.Peek.AsDateLiteral.Literal
                Case Else
                    Helper.Assert(Compiler.TypeResolution.IsNumericType(Reader.Peek.AsLiteral.LiteralValue.GetType))
                    Result = CDbl(Reader.Peek.AsLiteral.LiteralValue) 'AsFloatingPointLiteral.Literal
            End Select
            'Result = CurrentToken.Value.Literal
            Reader.Next()
        ElseIf Reader.Peek.IsKeyword Then
            Dim tpType As Type = Compiler.TypeResolution.KeywordToType(Reader.Peek.AsKeyword.Keyword)
            If tpType Is Nothing Then
                If Reader.Peek.Equals(KS.True) Then
                    Result = True
                    Reader.Next()
                    Return True
                ElseIf Reader.Peek.Equals(KS.False) Then
                    Result = False
                    Reader.Next()
                    Return True
                Else 'TODO: Conversion functions (CInt...)
                    Compiler.Report.ShowMessage(Messages.VBNC30201)
                End If
                Reader.Next()
                Return False
            Else
                'A builtin type, i.e:
                '#Const a = Integer.MaxValue
                '#Const a = (user defined type).Constant is not allowed.
                Throw New InternalException("") 'TODO
            End If
        ElseIf Reader.Peek.IsIdentifier Then
            'Find the identifier in the list of defines.
            If CurrentConstants.ContainsKey(Reader.Peek.AsIdentifier.Identifier) Then
                Result = CurrentConstants.Item(Reader.Peek.AsIdentifier.Identifier).Value
                Reader.Next()
            Else
                Result = Nothing
                Reader.Next()
            End If
        End If

        Return True
    End Function

    Function RuleExponent(ByRef Result As Object) As Boolean
        Dim LSide As Object = Nothing, RSide As Object = Nothing

        If RuleIdentifier(LSide) = False Then Return False

        While Reader.Peek.Equals(KS.Power)
            Reader.Next()
            RuleExpression(RSide)

            Dim op1, op2 As Double
            Dim bErr As Boolean
            If ToDouble(LSide, op1) = False Then
                Compiler.Report.ShowMessage(Messages.VBNC30748, LSide.GetType.ToString, KS.Double.ToString)
                bErr = True
            End If
            If ToDouble(RSide, op2) = False Then
                Compiler.Report.ShowMessage(Messages.VBNC30748, RSide.GetType.ToString, KS.Double.ToString)
                bErr = True
            End If

            If bErr Then
                LSide = CDbl(0)
            Else
                LSide = op1 ^ op2
            End If
        End While

        Result = LSide
        Return True
    End Function

    Function RuleUnaryNegation(ByRef Result As Object) As Boolean
        Dim LSide As Object = Nothing

        If Reader.Peek.Equals(KS.Minus) Then
            Reader.Next()
            RuleUnaryNegation = RuleExponent(LSide)

            Dim op1 As Double
            If ToDouble(LSide, op1) = False Then
                Compiler.Report.ShowMessage(Messages.VBNC30748, LSide.GetType.ToString, KS.Double.ToString)
                LSide = 0
            Else
                LSide = -op1
            End If
        Else
            If RuleExponent(LSide) = False Then
                Return False
            End If
        End If
        Result = LSide
        Return True
    End Function

    Function RuleMultiplicationAndRealDivision(ByRef Result As Object) As Boolean
        Dim LSide As Object = Nothing, RSide As Object = Nothing

        If RuleUnaryNegation(LSide) = False Then Return False

        While Reader.Peek.Equals(KS.Mult, KS.RealDivision)
            Dim DoMult As Boolean
            DoMult = Reader.Peek.Equals(KS.Mult)
            Reader.Next()

            RuleExpression(RSide)

            Dim op1, op2 As Double
            Dim bErr As Boolean
            If ToDouble(LSide, op1) = False Then
                Compiler.Report.ShowMessage(Messages.VBNC30748, LSide.GetType.ToString, KS.Double.ToString)
                bErr = True
            End If
            If ToDouble(RSide, op2) = False Then
                Compiler.Report.ShowMessage(Messages.VBNC30748, RSide.GetType.ToString, KS.Double.ToString)
                bErr = True
            End If

            If bErr Then
                LSide = CDbl(0)
            ElseIf DoMult Then
                LSide = op1 * op2
            Else
                If op2 = 0 Then
                    Compiler.Report.ShowMessage(Messages.VBNC30542)
                    LSide = CDbl(0)
                Else
                    LSide = op1 / op2
                End If
            End If
        End While

        Result = LSide
        Return True
    End Function

    Function RuleIntegerDivision(ByRef Result As Object) As Boolean
        Dim LSide As Object = Nothing, RSide As Object = Nothing

        If RuleMultiplicationAndRealDivision(LSide) = False Then Return False

        While Reader.Peek.Equals(KS.IntDivision)
            Reader.Next()
            RuleExpression(RSide)

            Dim op1, op2 As Double
            Dim bErr As Boolean
            If ToDouble(LSide, op1) = False Then
                Compiler.Report.ShowMessage(Messages.VBNC30748, LSide.GetType.ToString, KS.Long.ToString)
                bErr = True
            End If
            If ToDouble(RSide, op2) = False Then
                Compiler.Report.ShowMessage(Messages.VBNC30748, RSide.GetType.ToString, KS.Long.ToString)
                bErr = True
            End If

            If bErr Then
                LSide = CDbl(0)
            ElseIf CLng(op2) = 0 Then
                Compiler.Report.ShowMessage(Messages.VBNC30542)
                LSide = CDbl(0)
            Else
                LSide = CDbl(CLng(op1) \ CLng(op2))
            End If
        End While

        Result = LSide
        Return True
    End Function

    Function RuleMod(ByRef Result As Object) As Boolean
        Dim LSide As Object = Nothing, RSide As Object = Nothing

        If RuleIntegerDivision(LSide) = False Then Return False

        While Reader.Peek.Equals(KS.Mod)
            Reader.Next()
            RuleExpression(RSide)

            Dim op1, op2 As Double
            Dim bErr As Boolean
            If ToDouble(LSide, op1) = False Then
                Compiler.Report.ShowMessage(Messages.VBNC30748, LSide.GetType.ToString, KS.Double.ToString)
                bErr = True
            End If
            If ToDouble(RSide, op2) = False Then
                Compiler.Report.ShowMessage(Messages.VBNC30748, RSide.GetType.ToString, KS.Double.ToString)
                bErr = True
            End If

            If bErr Then
                LSide = CLng(0)
            Else
                LSide = op1 Mod op2
            End If
        End While

        Result = LSide
        Return True
    End Function

    Function RuleAdditionSubtractionStringConcat(ByRef Result As Object) As Boolean
        Dim LSide As Object = Nothing, RSide As Object = Nothing

        If RuleMod(LSide) = False Then Return False

        While Reader.Peek.Equals(KS.Minus, KS.Add)
            Dim DoAdd As Boolean
            DoAdd = Reader.Peek.Equals(KS.Add)
            Reader.Next()
            RuleExpression(RSide)

            Dim bErr As Boolean
            If TypeOf LSide Is String AndAlso TypeOf RSide Is String Then
                'String concat
                LSide = CStr(LSide) & CStr(RSide)
            Else
                Dim op1, op2 As Double
                If TypeOf LSide Is String Then
                    op1 = Double.Parse(DirectCast(LSide, String))
                    If ToDouble(RSide, op2) = False Then
                        Compiler.Report.ShowMessage(Messages.VBNC30748, RSide.GetType.ToString, KS.Double.ToString)
                        bErr = True
                    End If
                ElseIf TypeOf RSide Is String Then
                    op2 = Double.Parse(DirectCast(RSide, String))
                    If ToDouble(LSide, op1) = False Then
                        Compiler.Report.ShowMessage(Messages.VBNC30748, LSide.GetType.ToString, KS.Double.ToString)
                        bErr = True
                    End If
                Else
                    If ToDouble(RSide, op2) = False Then
                        Compiler.Report.ShowMessage(Messages.VBNC30748, RSide.GetType.ToString, KS.Double.ToString)
                        bErr = True
                    End If
                    If ToDouble(LSide, op1) = False Then
                        Compiler.Report.ShowMessage(Messages.VBNC30748, LSide.GetType.ToString, KS.Double.ToString)
                        bErr = True
                    End If
                End If


                If bErr Then
                    LSide = CDbl(0)
                ElseIf DoAdd Then
                    LSide = op1 + op2
                Else
                    LSide = op1 - op2
                End If
            End If
        End While

        Result = LSide
        Return True
    End Function

    Function RuleStringConcat(ByRef Result As Object) As Boolean
        Dim LSide As Object = Nothing, RSide As Object = Nothing

        If RuleAdditionSubtractionStringConcat(LSide) = False Then Return False

        While Reader.Peek.Equals(KS.Concat)
            Reader.Next()
            RuleExpression(RSide)

            Dim op1, op2 As String
            Dim bErr As Boolean

            op1 = LSide.ToString
            op2 = RSide.ToString

            If bErr Then
                LSide = ""
            Else
                LSide = op1 & op2
            End If
        End While

        Result = LSide
        Return True
    End Function

    Function RuleArithmeticBitshift(ByRef Result As Object) As Boolean
        Dim LSide As Object = Nothing, RSide As Object = Nothing

        If RuleStringConcat(LSide) = False Then Return False

        While Reader.Peek.Equals(KS.ShiftLeft, KS.ShiftRight)
            Dim DoLeft As Boolean
            DoLeft = Reader.Peek.Equals(KS.ShiftLeft)
            Reader.Next()
            RuleExpression(RSide)

            Dim op1 As Double, op2 As Double
            Dim bErr As Boolean
            If ToDouble(LSide, op1) = False Then
                Compiler.Report.ShowMessage(Messages.VBNC30748, LSide.GetType.ToString, KS.Long.ToString)
                bErr = True
            End If
            If ToDouble(RSide, op2) = False Then
                Compiler.Report.ShowMessage(Messages.VBNC30748, RSide.GetType.ToString, KS.Integer.ToString)
                bErr = True
            End If
            If op1 < Long.MinValue OrElse op1 > Long.MaxValue Then
                Compiler.Report.ShowMessage(Messages.VBNC30439, KS.Long.ToString)
            ElseIf op2 < Integer.MinValue OrElse op2 > Integer.MaxValue Then
                Compiler.Report.ShowMessage(Messages.VBNC30439, KS.Integer.ToString)
            End If

            If bErr Then
                LSide = CDbl(0)
            ElseIf DoLeft Then
                LSide = CLng(op1) << CInt(op2)
            Else
                LSide = CLng(op1) >> CInt(op2)
            End If
        End While

        Result = LSide
        Return True
    End Function

    Function RuleRelational(ByRef Result As Object) As Boolean
        Dim LSide As Object = Nothing, RSide As Object = Nothing

        If RuleArithmeticBitshift(LSide) = False Then Return False

        While Reader.Peek.Equals(KS.Equals, KS.NotEqual, KS.GT, KS.LT, KS.GE, KS.LE)
            Dim DoWhat As KS = Reader.Peek.AsSymbol.Symbol
            Reader.Next()
            RuleExpression(RSide)

            'Compiler.Report.WriteLine(String.Format("RuleRelational: " & DoWhat.ToString() & ", Left={0}, Right={1}", LSide, RSide) & Reader.Current.Location.ToString())
            Try
                Select Case DoWhat
                    Case KS.Equals
                        If TypeOf LSide Is Boolean AndAlso TypeOf RSide Is Boolean Then
                            Result = CBool(LSide) = CBool(RSide)
                            'Compiler.Report.WriteLine(CStr(LSide))
                            Return True
                        ElseIf TypeOf LSide Is String AndAlso TypeOf RSide Is String Then
                            Result = String.Compare(CStr(LSide), CStr(RSide), True)
                            'Compiler.Report.WriteLine(CStr(LSide))
                            Return True
                        ElseIf LSide Is Nothing AndAlso TypeOf RSide Is Boolean Then
                            Result = False = CBool(RSide)
                            'Compiler.Report.WriteLine(CStr(LSide))
                            Return True
                        ElseIf TypeOf LSide Is Boolean AndAlso RSide Is Nothing Then
                            Result = False = CBool(LSide)
                            'Compiler.Report.WriteLine(CStr(LSide))
                            Return True
                        End If
                        LSide = Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(LSide, RSide, True)
                    Case KS.NotEqual
                        LSide = Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(LSide, RSide, True)
                    Case KS.GT
                        LSide = Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectGreater(LSide, RSide, True)
                    Case KS.LT
                        LSide = Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectLess(LSide, RSide, True)
                    Case KS.GE
                        LSide = Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectGreaterEqual(LSide, RSide, True)
                    Case KS.LE
                        LSide = Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectLessEqual(LSide, RSide, True)
                    Case Else
                        Throw New InternalException(Me)
                End Select
            Catch ex As Exception
                Helper.AddError(ex.Message & VB.vbNewLine & ex.StackTrace)
                Return False
            End Try
        End While

        Result = LSide
        Return True
    End Function

    Function RuleNot(ByRef Result As Object) As Boolean
        Dim LSide As Object = Nothing

        If Reader.Peek.Equals(KS.Not) Then
            Reader.Next()
            RuleNot = RuleRelational(LSide)

            Dim op1 As Boolean
            If ToBoolean(LSide, op1) = False Then
                Compiler.Report.ShowMessage(Messages.VBNC30748, LSide.GetType.ToString, KS.Boolean.ToString)
                LSide = 0
            Else
                LSide = Not op1
            End If
        Else
            If RuleRelational(LSide) = False Then
                Return False
            End If
        End If
        Result = LSide
        Return True
    End Function

    Function RuleAnd_AndAlso(ByRef Result As Object) As Boolean
        Dim LSide As Object = Nothing, RSide As Object = Nothing

        If RuleNot(LSide) = False Then Return False

        While Reader.Peek.Equals(KS.And, KS.AndAlso)
            Dim DoAlso As Boolean
            DoAlso = Reader.Peek.Equals(KS.AndAlso)
            Reader.Next()
            RuleExpression(RSide)

            Dim op1 As Boolean, op2 As Boolean
            Dim bErr As Boolean
            If ToBoolean(LSide, op1) = False Then
                Compiler.Report.ShowMessage(Messages.VBNC30748, LSide.GetType.ToString, KS.Boolean.ToString)
                bErr = True
            End If
            If ToBoolean(RSide, op2) = False Then
                Compiler.Report.ShowMessage(Messages.VBNC30748, RSide.GetType.ToString, KS.Boolean.ToString)
                bErr = True
            End If

            If bErr Then
                LSide = False
            ElseIf DoAlso Then
                LSide = op1 AndAlso op2 'Since its a constant expression, there's no difference between And & AndAlso
            Else
                LSide = op1 And op2
            End If
        End While

        Result = LSide
        Return True
    End Function

    Function RuleOr_OrElse(ByRef Result As Object) As Boolean
        Dim LSide As Object = Nothing, RSide As Object = Nothing

        If RuleAnd_AndAlso(LSide) = False Then Return False

        While Reader.Peek.Equals(KS.Or, KS.OrElse)
            Dim DoElse As Boolean
            DoElse = Reader.Peek.Equals(KS.OrElse)
            Reader.Next()
            RuleExpression(RSide)

            Dim op1 As Boolean, op2 As Boolean
            Dim bErr As Boolean
            If ToBoolean(LSide, op1) = False Then
                Compiler.Report.ShowMessage(Messages.VBNC30748, LSide.GetType.ToString, KS.Boolean.ToString)
                bErr = True
            End If
            If ToBoolean(RSide, op2) = False Then
                Compiler.Report.ShowMessage(Messages.VBNC30748, RSide.GetType.ToString, KS.Boolean.ToString)
                bErr = True
            End If

            If bErr Then
                LSide = False
            ElseIf DoElse Then
                LSide = op1 OrElse op2 'Since its a constant expression, there's no difference between Or & OrElse
            Else
                LSide = op1 Or op2
            End If
        End While

        Result = LSide
        Return True
    End Function

    Function RuleXor(ByRef Result As Object) As Boolean
        Dim LSide As Object = Nothing, RSide As Object = Nothing

        If RuleOr_OrElse(LSide) = False Then Return False

        While Reader.Peek.Equals(KS.Xor)
            Reader.Next()
            RuleExpression(RSide)

            Dim op1 As Boolean, op2 As Boolean
            Dim bErr As Boolean
            If ToBoolean(LSide, op1) = False Then
                Compiler.Report.ShowMessage(Messages.VBNC30748, LSide.GetType.ToString, KS.Boolean.ToString)
                bErr = True
            End If
            If ToBoolean(RSide, op2) = False Then
                Compiler.Report.ShowMessage(Messages.VBNC30748, RSide.GetType.ToString, KS.Boolean.ToString)
                bErr = True
            End If

            If bErr Then
                LSide = False
            Else
                LSide = op1 Xor op2
            End If
        End While

        Result = LSide
        Return True
    End Function

    Function RuleExpression(ByRef Result As Object) As Boolean
        Return RuleXor(Result)
    End Function

    Overloads Function Parse(ByRef Result As Object) As Boolean
        Parse = RuleExpression(Result)
    End Function

    'Helper conversion functions
    Private Function ToDouble(ByVal value As Object, ByRef Result As Double) As Boolean
        Dim vTp As Type = value.GetType
        If Helper.CompareType(vTp, Compiler.TypeCache.Byte) OrElse _
            Helper.CompareType(vTp, Compiler.TypeCache.Decimal) OrElse _
            Helper.CompareType(vTp, Compiler.TypeCache.Double) OrElse _
            Helper.CompareType(vTp, Compiler.TypeCache.Integer) OrElse _
            Helper.CompareType(vTp, Compiler.TypeCache.Long) OrElse _
            Helper.CompareType(vTp, Compiler.TypeCache.SByte) OrElse _
            Helper.CompareType(vTp, Compiler.TypeCache.Short) OrElse _
            Helper.CompareType(vTp, Compiler.TypeCache.Single) OrElse _
            Helper.CompareType(vTp, Compiler.TypeCache.UInteger) OrElse _
            Helper.CompareType(vTp, Compiler.TypeCache.ULong) OrElse _
            Helper.CompareType(vTp, Compiler.TypeCache.UShort) Then
            Result = CDbl(value)
            Return True
        Else
            Return False
        End If
    End Function

    Private Function ToBoolean(ByVal value As Object, ByRef Result As Boolean) As Boolean
        Result = CBool(value)
        Return True
    End Function
End Class