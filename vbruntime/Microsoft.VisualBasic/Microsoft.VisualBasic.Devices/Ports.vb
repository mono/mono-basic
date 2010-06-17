'
' Ports.vb
'
' Authors:
'   Rolf Bjarne Kvinge (RKvinge@novell.com>
'
' Copyright (C) 2007 Novell (http://www.novell.com)
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
#If TARGET_JVM = False Then 'System.IO.Ports Not Supported by Grasshopper
Imports System.IO.Ports
Imports System.Collections.ObjectModel

Namespace Microsoft.VisualBasic.Devices
    Public Class Ports
        Public Sub New()
            'Empty
        End Sub

        Public Function OpenSerialPort(ByVal portName As String) As SerialPort
            Dim result As New SerialPort(portName)
            result.Open()
            Return result
        End Function

        Public Function OpenSerialPort(ByVal portName As String, ByVal baudRate As Integer) As SerialPort
            Dim result As New SerialPort(portName, baudRate)
            result.Open()
            Return result
        End Function

        Public Function OpenSerialPort(ByVal portName As String, ByVal baudRate As Integer, ByVal parity As Parity) As SerialPort
            Dim result As New SerialPort(portName, baudRate, parity)
            result.Open()
            Return result
        End Function

        Public Function OpenSerialPort(ByVal portName As String, ByVal baudRate As Integer, ByVal parity As Parity, ByVal dataBits As Integer) As SerialPort
            Dim result As New SerialPort(portName, baudRate, parity, dataBits)
            result.Open()
            Return result
        End Function

        Public Function OpenSerialPort(ByVal portName As String, ByVal baudRate As Integer, ByVal parity As Parity, ByVal dataBits As Integer, ByVal stopBits As StopBits) As SerialPort
            Dim result As New SerialPort(portName, baudRate, parity, dataBits, stopBits)
            result.Open()
            Return result
        End Function

        Public ReadOnly Property SerialPortNames() As ReadOnlyCollection(Of String)
            Get
                Return New ReadOnlyCollection(Of String)(SerialPort.GetPortNames)
            End Get
        End Property
    End Class
End Namespace
#End If
