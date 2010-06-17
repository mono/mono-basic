' ComputerTests.vb - NUnit Test Cases for Microsoft.VisualBasic.Devices.Computer
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

Namespace Devices
    <TestFixture()> _
    Public Class ComputerTests
        <Category("TargetJvmNotSupported")> _
        <Test()> _
        Public Sub Test()
            Dim testname As String = "Test"
            Dim sc As New Computer
            Assert.IsNotNull(sc.Clock, testname & "-1")
            Assert.IsNotNull(sc.FileSystem, testname & "-2")
            Assert.IsNotNull(sc.Info, testname & "-3")
            Assert.AreEqual(System.Environment.MachineName, sc.Name, testname & "-4")
            Assert.IsNotNull(sc.Network, testname & "-5")
            Assert.IsNotNull(sc.Registry, testname & "-6")

#If Not TARGET_JVM Then
            Assert.IsNotNull(sc.Audio, testname & "-7")
            Assert.IsNotNull(sc.Clipboard, testname & "-8")
            Assert.IsNotNull(sc.Keyboard, testname & "-9")
            Assert.IsNotNull(sc.Mouse, testname & "-10")
            Assert.IsNotNull(sc.Ports, testname & "-11")
            Assert.IsNotNull(sc.Screen, testname & "-12")
            Assert.AreSame(System.Windows.Forms.Screen.PrimaryScreen, sc.Screen, testname & "-13")
#End If
        End Sub
    End Class
End Namespace
#End If
