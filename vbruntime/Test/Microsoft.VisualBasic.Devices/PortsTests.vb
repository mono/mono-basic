' PortsTests.vb - NUnit Test Cases for Microsoft.VisualBasic.Devices.Ports
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
Imports System.IO.Ports

Namespace Devices
    <TestFixture()> _
    Public Class PortsTests
        <Category("TargetJvmNotSupported")> _
        <Test()> _
        Public Sub Test()
            Dim testname As String = "Test"
            Dim ports As New Ports
            Dim names() As String
            Dim vbNames As ObjectModel.ReadOnlyCollection(Of String)
            Dim realNames() As String = SerialPort.GetPortNames

            vbNames = ports.SerialPortNames
            ReDim names(vbNames.Count - 1)
            vbNames.CopyTo(names, 0)

            Array.Sort(names, 0, names.Length)
            Array.Sort(realNames, 0, realNames.Length)

            Assert.AreEqual(realNames.Length, names.Length, testname & "-1")
            Assert.AreEqual(Join(realNames, vbNewLine), Join(names, vbNewLine), testname & "-2")

            If vbNames.Count > 0 Then
                Dim portName As String = vbNames(0)
                Dim port As SerialPort

                port = ports.OpenSerialPort(portName)
                Assert.AreEqual(portName, port.PortName, testname & "-3")
                port.Close()

                port = ports.OpenSerialPort(portName, 9600)
                Assert.AreEqual(portName, port.PortName, testname & "-13")
                Assert.AreEqual(9600, port.BaudRate, testname & "-14")
                port.Close()

                port = ports.OpenSerialPort(portName, 9600, Parity.Even)
                Assert.AreEqual(portName, port.PortName, testname & "-23")
                Assert.AreEqual(9600, port.BaudRate, testname & "-24")
                Assert.AreEqual(Parity.Even, port.Parity, testname & "-25")
                port.Close()

                port = ports.OpenSerialPort(portName, 9600, Parity.Even, 7)
                Assert.AreEqual(portName, port.PortName, testname & "-33")
                Assert.AreEqual(9600, port.BaudRate, testname & "-34")
                Assert.AreEqual(Parity.Even, port.Parity, testname & "-35")
                Assert.AreEqual(7, port.DataBits, testname & "-36")
                port.Close()

                port = ports.OpenSerialPort(portName, 9600, Parity.Even, 7, StopBits.One)
                Assert.AreEqual(portName, port.PortName, testname & "-43")
                Assert.AreEqual(9600, port.BaudRate, testname & "-44")
                Assert.AreEqual(Parity.Even, port.Parity, testname & "-45")
                Assert.AreEqual(7, port.DataBits, testname & "-46")
                Assert.AreEqual(StopBits.One, port.StopBits, testname & "-47")
                port.Close()

            Else
                Assert.Ignore("No serial ports on this machine, skipping test")
            End If
        End Sub
    End Class
End Namespace
#End If