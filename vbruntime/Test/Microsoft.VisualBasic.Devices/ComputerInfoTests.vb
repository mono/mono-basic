' ComputerInfoTests.vb - NUnit Test Cases for Microsoft.VisualBasic.Devices.ComputerInfo
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
'Imports System.Windows.Forms

Namespace Devices
    <TestFixture()> _
    Public Class ComputerInfoTests
        <Category("TargetJvmNotSupported")> _
        <Category("UI")> _
        <Test()> _
        Public Sub Test()
            Dim testname As String = "Test"
            Dim ci As New ComputerInfo()

            Assert.AreEqual(System.Globalization.CultureInfo.InstalledUICulture.Name, ci.InstalledUICulture.Name, testname & "-1")
            Assert.AreEqual(Environment.OSVersion.Platform.ToString, ci.OSFullName, testname & "-2")
            Assert.AreEqual(Environment.OSVersion.Platform.ToString, ci.OSPlatform, testname & "-3")
            Assert.AreEqual(Environment.OSVersion.Version.ToString, ci.OSVersion, testname & "-4")
            'Assert.AreEqual(?, ci.AvailablePhysicalMemory, testname & "-5")
            'Assert.AreEqual(?, ci.AvailableVirtualMemory, testname & "-6")
            'Assert.AreEqual(?, ci.TotalPhysicalMemory, testname & "-7")
            'Assert.AreEqual(?, ci.TotalVirtualMemory, testname & "-8")
        End Sub
    End Class
End Namespace
#End If
