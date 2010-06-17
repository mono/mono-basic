'
' ObjectFlowControl.vb
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
'MONOTODO: implement this public class. if needed.
Namespace Microsoft.VisualBasic.CompilerServices
    <System.ComponentModel.EditorBrowsable(ComponentModel.EditorBrowsableState.Never)> _
    Public NotInheritable Class ObjectFlowControl
        Private Sub New()
            'Nobody should see constructor
        End Sub
        Public Shared Sub CheckForSyncLockOnValueType(ByVal Expression As Object)
            FlowControl.CheckForSyncLockOnValueType(Expression)
        End Sub

        <System.ComponentModel.EditorBrowsable(ComponentModel.EditorBrowsableState.Never)> _
        Public NotInheritable Class ForLoopControl
            Private Sub New()
                'Nobody should see constructor
            End Sub
            Public Shared Function ForLoopInitObj(ByVal Counter As Object, ByVal Start As Object, ByVal Limit As Object, ByVal StepValue As Object, ByRef LoopForResult As Object, ByRef CounterResult As Object) As Boolean
                Return FlowControl.ForLoopInitObj(Counter, Start, Limit, StepValue, LoopForResult, CounterResult)
            End Function
            Public Shared Function ForNextCheckDec(ByVal count As Decimal, ByVal limit As Decimal, ByVal StepValue As Decimal) As Boolean
                Return FlowControl.ForNextCheckDec(count, limit, StepValue)
            End Function
            Public Shared Function ForNextCheckObj(ByVal Counter As Object, ByVal LoopObj As Object, ByRef CounterResult As Object) As Boolean
                Return FlowControl.ForNextCheckObj(Counter, LoopObj, CounterResult)
            End Function
            Public Shared Function ForNextCheckR4(ByVal count As Single, ByVal limit As Single, ByVal StepValue As Single) As Boolean
                Return FlowControl.ForNextCheckR4(count, limit, StepValue)
            End Function
            Public Shared Function ForNextCheckR8(ByVal count As Double, ByVal limit As Double, ByVal StepValue As Double) As Boolean
                Return FlowControl.ForNextCheckR8(count, limit, StepValue)
            End Function
        End Class
    End Class
End Namespace
