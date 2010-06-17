' KeyboardTests.vb - NUnit Test Cases for Microsoft.VisualBasic.Devices.Keyboard
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

#If Not TARGET_JVM Then
Imports Microsoft.VisualBasic.Devices
Imports System.Windows.Forms
Imports NUnit.Framework

Namespace Devices
    <TestFixture()> _
    Public Class KeyboardTests
        <Category("TargetJvmNotSupported")> _
        <Category("UI")> _
        <Test()> _
        Public Sub Test()
            Dim testname As String = "Test"
            Dim k As New Keyboard()

            MsgBox("Press the NumLock key (unless it's already on)")
            Assert.AreEqual(True, k.NumLock, testname & "-01")
            MsgBox("Press the NumLock key again (to turn it off)")
            Assert.AreEqual(False, k.NumLock, testname & "-02")

            MsgBox("Press the ScrollLock key (unless it's already on)")
            Assert.AreEqual(True, k.ScrollLock, testname & "-11")
            MsgBox("Press the ScrollLock key again (to turn it off)")
            Assert.AreEqual(False, k.ScrollLock, testname & "-12")

            MsgBox("Press the CapsLock key (unless it's already on)")
            Assert.AreEqual(True, k.CapsLock, testname & "-21")
            MsgBox("Press the CapsLock key again (to turn it off)")
            Assert.AreEqual(False, k.CapsLock, testname & "-22")

            Assert.AreEqual(False, k.AltKeyDown, testname & "-31")
            MsgBox("Press the ALT key while clicking OK")
            Assert.AreEqual(True, k.AltKeyDown, testname & "-32")

            Assert.AreEqual(False, k.CtrlKeyDown, testname & "-41")
            MsgBox("Press the CTLR key while clicking OK")
            Assert.AreEqual(True, k.CtrlKeyDown, testname & "-42")

            Assert.AreEqual(False, k.ShiftKeyDown, testname & "-51")
            MsgBox("Press the SHIFT key while clicking OK")
            Assert.AreEqual(True, k.ShiftKeyDown, testname & "-52")

            MsgBox("Keyboard test finished")
        End Sub
    End Class
End Namespace
#End If
