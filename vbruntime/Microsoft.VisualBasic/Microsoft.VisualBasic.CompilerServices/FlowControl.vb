'
' FlowControl.vb
'
' Author:
'   Mizrahi Rafael (rafim@mainsoft.com)
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

Imports System

Namespace Microsoft.VisualBasic.CompilerServices
    <System.ComponentModel.EditorBrowsable(ComponentModel.EditorBrowsableState.Never)> _
    Public NotInheritable Class FlowControl

        ' LoopFor reference object
        Private Class LoopForObject
            Public Start As Decimal
            Public Limit As Decimal
            Public StepValue As Decimal
        End Class

        ' Counter reference object
        'Private CounterObject As Object

        Private Sub New()
            'Nobody should see constructor
        End Sub

        Public Shared Sub CheckForSyncLockOnValueType(ByVal obj As Object)
            'obj can not be null (checked by monitor.enter)
            Dim t As Type = obj.GetType()
            If (t.IsValueType) Then
                Throw New ArgumentException("'SyncLock' operand cannot be of type '" + t.Name + "' because '" + t.Name + "' is not a reference type.")
            End If
        End Sub
        Public Shared Function ForEachInArr(ByVal ary As System.Array) As System.Collections.IEnumerator
            Throw New NotImplementedException
        End Function
        Public Shared Function ForEachInObj(ByVal obj As Object) As System.Collections.IEnumerator
            Throw New NotImplementedException
        End Function
        Public Shared Function ForEachNextObj(ByRef obj As Object, ByVal enumerator As System.Collections.IEnumerator) As Boolean
            Throw New NotImplementedException
        End Function
        ' Create and initialize LoopForObject and CounterObject reference objects
        ' returns an indication whether the code flow should enter the for-loop statement.
        ' CounterResult: Common code paths are having Counter and CounterResult as the same object.
        ' so Counter and CounterResult are casted to Decimal and not to some new private class.
        Public Shared Function ForLoopInitObj(ByVal Counter As Object, _
                                            ByVal Start As Object, _
                                            ByVal Limit As Object, _
                                            ByVal StepValue As Object, _
                                            ByRef LoopForResult As Object, _
                                            ByRef CounterResult As Object) _
                                            As Boolean

            ' set the Counter as Start
            Counter = Convert.ToDecimal(DecimalType.FromObject(Start))

            '
            ' insert the loop parameters into LoopForObject
            '
            Dim lfo As New LoopForObject

            ' Check the Object types (i.e. string) and convert them to Decimal
            lfo.Start = Convert.ToDecimal(DecimalType.FromObject(Start))
            lfo.Limit = Convert.ToDecimal(DecimalType.FromObject(Limit))
            lfo.StepValue = Convert.ToDecimal(DecimalType.FromObject(StepValue))
            ' set the out reference
            LoopForResult = lfo

            ' set the out reference
            CounterResult = Counter

            Return ForNextCheckDec(Convert.ToDecimal(CounterResult), lfo.Limit, lfo.StepValue)

        End Function

        '
        ' returns an indication whether the code flow should proceed with the next for-loop iteration.
        '
        Public Shared Function ForNextCheckDec(ByVal count As Decimal, ByVal limit As Decimal, ByVal StepValue As Decimal) As Boolean
            'if StepValue is positive or negative
            If StepValue > 0 Then
                If count <= limit Then
                    Return True
                Else
                    Return False
                End If
            Else
                If count >= limit Then
                    Return True
                Else
                    Return False
                End If
            End If

        End Function
        '
        ' Add a StepValue to the Counter and set its value into CounterResult
        ' returns an indication whether the code flow should proceed with the next for-loop iteration.
        '
        Public Shared Function ForNextCheckObj(ByVal Counter As Object, ByVal LoopObj As Object, ByRef CounterResult As Object) As Boolean

            ' Counter is a CounterObject
            Dim d_Counter As Decimal = DecimalType.FromObject(Counter)
            Dim lfo As LoopForObject = DirectCast(LoopObj, LoopForObject)
            Dim d_CounterResult As Decimal = DecimalType.FromObject(CounterResult)

            ' increment the counter with a step
            d_CounterResult = d_Counter + lfo.StepValue

            ' set the reference
            CounterResult = d_CounterResult

            ' check if there is another iteration
            Return ForNextCheckDec(d_CounterResult, lfo.Limit, lfo.StepValue)

        End Function
        Public Shared Function ForNextCheckR4(ByVal count As Single, ByVal limit As Single, ByVal StepValue As Single) As Boolean
            'if StepValue is positive or negative
            If StepValue > 0 Then
                If count <= limit Then
                    Return True
                Else
                    Return False
                End If
            Else
                If count >= limit Then
                    Return True
                Else
                    Return False
                End If
            End If
        End Function
        Public Shared Function ForNextCheckR8(ByVal count As Double, ByVal limit As Double, ByVal StepValue As Double) As Boolean
            'if StepValue is positive or negative
            If StepValue > 0 Then
                If count <= limit Then
                    Return True
                Else
                    Return False
                End If
            Else
                If count >= limit Then
                    Return True
                Else
                    Return False
                End If
            End If
        End Function
    End Class

End Namespace
