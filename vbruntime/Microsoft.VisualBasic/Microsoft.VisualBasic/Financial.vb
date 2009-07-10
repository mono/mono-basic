'
' Financial.vb
'
' Author:
'   Mizrahi Rafael (rafim@mainsoft.com)
'   Boris Kirzner (borisk@mainsoft.com)
'

'
' Copyright (C) 2002-2006 Mainsoft Corporation.
' Copyright (C) 2004-2006 Novell, Inc (http://www.novell.com)
'
' Permission is hereby granted, free of charge, to any person obtaining
' a copy of this software and associated documentation files (the
' "Software"), to deal in the Software without restriction, including
' without limitation the rights to use, copy, modify, merge, publish,
' distribute, sublicense, and/or sell copies of the Software, and to
' permit persons to whom the Software is furnished to do so, subject to
' the following conditions:
' 
' The above copyright notice and this permission notice shall be
' included in all copies or substantial portions of the Software.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
' EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
' MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
' NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
' LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
' OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
' WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
'
Imports Microsoft.VisualBasic.CompilerServices
Imports System

Namespace Microsoft.VisualBasic
    <StandardModule()> _
    Public NotInheritable Class Financial

        Public Shared Function DDB(ByVal Cost As Double, ByVal Salvage As Double, _
                                    ByVal Life As Double, ByVal Period As Double, _
                                    Optional ByVal Factor As Double = 2.0) As Double
            If Period > Life Or Factor <= 0 Or Salvage < 0 Or Life < 0 Or Period <= 0 Then
                Throw New ArgumentException("Argument 'Factor' is not a valid value.")
            End If

            ' LAMESPEC: MSDN claims the exception should be thrown in this case
            'If Cost < 0 Then
            'Throw New ArgumentException("Argument 'Factor' is not a valid value.")
            'End If

            ' Below we use power of (Period - 1).
            ' Tested Period < 1 and it behaves like Period = 1
            If Period < 1 Then
                Period = 1
            End If

            Dim Rate As Double = Factor / Life
            Dim PreviousValue As Double = Cost * (1 - Rate) ^ (Period - 1)
            PreviousValue = Math.Max(PreviousValue, Salvage)
            Dim CurrentValue As Double = Cost * (1 - Rate) ^ Period
            CurrentValue = Math.Max(CurrentValue, Salvage)
            Return PreviousValue - CurrentValue

        End Function

        Public Shared Function SLN(ByVal Cost As Double, ByVal Salvage As Double, _
                                    ByVal Life As Double) As Double
            If Life = 0 Then
                Throw New ArgumentException("Argument 'Life' cannot be zero.")
            End If

            Return ((Cost - Salvage) / Life)
        End Function

        Public Shared Function SYD(ByVal Cost As Double, ByVal Salvage As Double, _
                                    ByVal Life As Double, ByVal Period As Double) As Double
            If Salvage < 0 Then
                Throw New ArgumentException("Argument 'Salvage' must be greater than or equal to zero.")
            End If
            If Period > Life Then
                Throw New ArgumentException("Argument 'Period' must be less than or equal to argument 'Life'.")
            End If
            If Period <= 0 Then
                Throw New ArgumentException("Argument 'Period' must be greater than zero.")
            End If
            Return ((Cost - Salvage) * (Life - Period + 1) * 2 / Life) / (Life + 1)
        End Function

        Public Shared Function FV(ByVal Rate As Double, ByVal NPer As Double, ByVal Pmt As Double, _
                                    Optional ByVal PV As Double = 0, Optional ByVal Due As DueDate = DueDate.EndOfPeriod) As Double

            If NPer < 0 Then
                If Due = DueDate.EndOfPeriod Then
                    Due = DueDate.BegOfPeriod
                Else
                    Due = DueDate.EndOfPeriod
                End If
                Return FV(1 / (1 + Rate) - 1, -NPer, -Pmt, PV, Due)
            End If

            If NPer = 0 Or Rate = 0 Then
                Return -(PV + Pmt * NPer)
            End If

            If Due = DueDate.BegOfPeriod Then
                Pmt = Pmt * (1 + Rate)
            End If

            Dim ExpRate As Double = (1 + Rate) ^ NPer
            Return -(PV * ExpRate + Pmt * (ExpRate - 1) / Rate)

        End Function

        Public Shared Function Rate(ByVal NPer As Double, ByVal Pmt As Double, ByVal PV As Double, _
                                    Optional ByVal FV As Double = 0, Optional ByVal Due As DueDate = DueDate.EndOfPeriod, _
                                    Optional ByVal Guess As Double = 0.1) As Double
            Throw New NotImplementedException
        End Function

        Public Shared Function IRR(ByRef ValueArray() As Double, Optional ByVal Guess As Double = 0.1) As Double
            Throw New NotImplementedException
        End Function

        Public Shared Function MIRR(ByRef ValueArray() As Double, ByVal FinanceRate As Double, _
                                    ByVal ReinvestRate As Double) As Double

            If FinanceRate <= -1 Then
                Throw New ArgumentException("Argument 'FinanceRate' is not a valid value.")
            End If
            If ReinvestRate <= -1 Then
                Throw New ArgumentException("Argument 'ReinvestRate' is not a valid value.")
            End If

            Dim pnpv1 As Double = PNPV(ValueArray, ReinvestRate)
            Dim nnpv1 As Double = NNPV(ValueArray, FinanceRate)

            Dim n As Integer = ValueArray.Length
            Dim intermediate As Double = (-pnpv1 * (1 + ReinvestRate) ^ n) / (nnpv1 * (1 + FinanceRate))
            Dim result As Double = Math.Abs(intermediate) ^ (1 / (n - 1)) - 1

            If intermediate < 0 Then
                Return -result
            Else
                Return result
            End If

        End Function

        Private Shared Function PNPV(ByVal ValueArray() As Double, ByVal Rate As Double) As Double
            Dim result As Double = 0
            For i As Integer = 1 To ValueArray.Length
                Dim value As Double = ValueArray(i - 1)
                If value >= 0 Then
                    result = result + value / (1 + Rate) ^ i
                End If
            Next
            Return result
        End Function

        Private Shared Function NNPV(ByVal ValueArray() As Double, ByVal Rate As Double) As Double
            Dim result As Double = 0
            For i As Integer = 1 To ValueArray.Length
                Dim value As Double = ValueArray(i - 1)
                If value < 0 Then
                    result = result + value / (1 + Rate) ^ i
                End If
            Next
            Return result
        End Function

        Public Shared Function NPer(ByVal Rate As Double, ByVal Pmt As Double, ByVal PV As Double, _
                                    Optional ByVal FV As Double = 0, Optional ByVal Due As DueDate = DueDate.EndOfPeriod) As Double


            If Rate = -1 Then
                Throw New ArgumentException("Argument 'Rate' is not a valid value.")
            End If
            If Pmt = 0 Then
                If Rate = 0 Then
                    Throw New ArgumentException("Argument 'Pmt' is not a valid value.")
                Else
                    Throw New ArgumentException("Cannot calculate number of periods using the arguments provided.")
                End If
            End If
            Dim iDue As Integer = Due
            Dim tmp As Double = (Pmt * (1D + Rate * iDue)) / Rate
            Dim ret As Double = Math.Log((tmp - FV) / (tmp + PV)) / Math.Log(1D + Rate)
            Return ret
            'If Rate = -1 Then
            '    Throw New ArgumentException("Argument 'Rate' is not a valid value.")
            'End If
            'If Pmt = 0 Then
            '    If Rate = 0 Then
            '        Throw New ArgumentException("Argument 'Pmt' is not a valid value.")
            '    Else
            '        Throw New ArgumentException("Cannot calculate number of periods using the arguments provided.")
            '    End If
            'End If

            'Dim current As Double = 0
            '' FIXME : what is the meaning of double period value ?
            'Dim pperiod As Integer = 0
            'Dim fperiod As Integer = 0
            'Dim apmt As Double = Math.Abs(Pmt)

            'If PV <> 0 Then
            '    If PV * Pmt < 0 Then
            '        current = Math.Abs(PV)
            '        If Due = DueDate.BegOfPeriod Then
            '            current = current - current * Rate
            '        End If
            '        While current > 0
            '            current = current + current * Rate
            '            current = current + apmt
            '            pperiod = pperiod + 1
            '        End While
            '    Else
            '        current = apmt
            '        If Due = DueDate.BegOfPeriod Then
            '            PV = PV * (1 + Rate)
            '        End If
            '        While current < Math.Abs(PV)
            '            current = current + current * Rate
            '            current = current + apmt
            '            pperiod = pperiod - 1
            '        End While
            '    End If
            'End If

            'If FV <> 0 Then
            '    If FV * Pmt < 0 Then
            '        current = apmt
            '        If Due = DueDate.EndOfPeriod Then
            '            current = 0
            '        End If
            '        While current < Math.Abs(FV)
            '            current = current + current * Rate
            '            current = current + apmt
            '            fperiod = fperiod + 1
            '        End While
            '    Else
            '        fperiod = 1
            '        current = Math.Abs(FV)
            '        If Due = DueDate.BegOfPeriod Then
            '            current = current - current * Rate
            '        End If
            '        While current > 0
            '            current = current + current * Rate
            '            current = current - apmt
            '            fperiod = fperiod - 1
            '        End While
            '    End If
            'End If
            'Return pperiod + fperiod
        End Function

        Public Shared Function IPmt(ByVal Rate As Double, ByVal Per As Double, ByVal NPer As Double, _
                                    ByVal PV As Double, Optional ByVal FV As Double = 0, _
                                    Optional ByVal Due As DueDate = DueDate.EndOfPeriod) As Double

            If Per <= 0 Or Per > NPer Or NPer < 0 Then
                Throw New ArgumentException("Argument 'Per' is not a valid value.")
            End If

            If Per = 1 AndAlso Due = DueDate.BegOfPeriod Then
                Return 0 ' The formula below doesn't cover this special case
            End If

            Dim Pmt As Double = Financial.Pmt(Rate, NPer, PV, FV, Due)
            Dim PreviousPV As Double = Financial.FV(Rate, Per - 1, Pmt, PV, Due)
            If Due = DueDate.BegOfPeriod Then
                PreviousPV = PreviousPV / (1 + Rate)
            End If
            Return PreviousPV * Rate

        End Function

        Public Shared Function Pmt(ByVal Rate As Double, ByVal NPer As Double, ByVal PV As Double, _
                                    Optional ByVal FV As Double = 0, _
                                    Optional ByVal Due As DueDate = DueDate.EndOfPeriod) As Double

            If NPer = 0 Then
                Throw New ArgumentException("Argument 'NPer' is not a valid value.")
            End If

            If NPer < 0 Then
                Return -Pmt(Rate, -NPer, FV, PV, Due)
            End If

            If Rate = 0 Then
                Return -(PV + FV) / NPer
            End If

            Dim ExpRate As Double = (1 + Rate) ^ NPer
            Dim dfpv As Double = (1 - 1 / ExpRate) / Rate
            Dim dffv As Double = (ExpRate - 1) / Rate

            If Due = DueDate.BegOfPeriod Then
                dffv = dffv * (1 + Rate)
                dfpv = dfpv * (1 + Rate)
            End If

            Return -(PV / dfpv + FV / dffv)

        End Function

        Public Shared Function PPmt(ByVal Rate As Double, ByVal Per As Double, ByVal NPer As Double, _
                                    ByVal PV As Double, Optional ByVal FV As Double = 0, _
                                    Optional ByVal Due As DueDate = DueDate.EndOfPeriod) As Double

            Return Pmt(Rate, NPer, PV, FV, Due) - IPmt(Rate, Per, NPer, PV, FV, Due)
        End Function

        Public Shared Function NPV(ByVal Rate As Double, ByRef ValueArray() As Double) As Double

            If ValueArray Is Nothing Then
                Throw New ArgumentException("Argument 'ValueArray' is Nothing.")
            End If
            If Rate = -1 Then
                Throw New ArgumentException("Argument 'Rate' is not a valid value.")
            End If

            Dim result As Double = 0
            For i As Integer = 1 To ValueArray.Length
                result = result + (ValueArray(i - 1) / ((1 + Rate) ^ i))
            Next
            Return result

        End Function

        Public Shared Function PV(ByVal Rate As Double, ByVal NPer As Double, ByVal Pmt As Double, _
                                    Optional ByVal FV As Double = 0, _
                                    Optional ByVal Due As DueDate = DueDate.EndOfPeriod) As Double
            Dim result As Double = 0
            Dim d As Double = (1 + Rate) ^ NPer
            Dim n As Double
            If Due = DueDate.EndOfPeriod Then
                n = -FV - Pmt * (d - 1) / Rate
            Else
                n = -FV - Pmt * (1 + Rate) * (d - 1) / Rate
            End If
            result = n / d
            Return result
        End Function
    End Class
End Namespace
