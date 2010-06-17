' ClockTests.vb - NUnit Test Cases for Microsoft.VisualBasic.Devices.Clock
'
' Rolf Bjarne Kvinge  (RKvinge@novell.com)
'
' 
' Copyright (C) 2007 Novell, Inc (http://www.novell.com)
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

Imports Microsoft.VisualBasic.Devices

Namespace Devices
    <TestFixture()> _
    Public Class ClockTests
        <Test()> _
        Public Sub Test()
            Dim testname As String = "Test"
            Dim sc As New Clock

            If TimeSpan.FromTicks(Math.Abs(sc.GmtTime.Ticks - Date.UtcNow.Ticks)).TotalSeconds > 0.5 Then
                Assert.Fail(testname & "-1")
            End If

            If TimeSpan.FromTicks(Math.Abs(sc.LocalTime.Ticks - Date.Now.Ticks)).TotalSeconds > 0.5 Then
                Assert.Fail(testname & "-2")
            End If

            If TimeSpan.FromTicks(Math.Abs(sc.TickCount - Environment.TickCount)).TotalSeconds > 0.5 Then
                Assert.Fail(testname & "-3")
            End If
        End Sub
    End Class
End Namespace
