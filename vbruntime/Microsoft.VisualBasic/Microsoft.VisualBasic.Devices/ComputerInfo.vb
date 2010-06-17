'
' ComputerInfo.vb
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
#If TARGET_JVM = False Then 'OSVersion Not Supported by Grasshopper
Imports System.Globalization
Imports System.Diagnostics

Namespace Microsoft.VisualBasic.Devices
    <DebuggerTypeProxy(GetType(ComputerInfo.ComputerInfoDebugView))> _
    Public Class ComputerInfo
        Public Sub New()
            'Empty
        End Sub

        <CLSCompliant(False)> _
        Public ReadOnly Property AvailablePhysicalMemory() As ULong
            Get
                Throw New NotImplementedException()
            End Get
        End Property

        <CLSCompliant(False)> _
        Public ReadOnly Property AvailableVirtualMemory() As ULong
            Get
                Throw New NotImplementedException()
            End Get
        End Property

        Public ReadOnly Property InstalledUICulture() As CultureInfo
            Get
                Return CultureInfo.InstalledUICulture
            End Get
        End Property

        Public ReadOnly Property OSFullName() As String
            Get
                Return OSPlatform
            End Get
        End Property

        Public ReadOnly Property OSPlatform() As String
            Get
                Return Environment.OSVersion.Platform.ToString
            End Get
        End Property

        Public ReadOnly Property OSVersion() As String
            Get
                Return Environment.OSVersion.Version.ToString
            End Get
        End Property

        <CLSCompliant(False)> _
        Public ReadOnly Property TotalPhysicalMemory() As ULong
            Get
                Throw New NotImplementedException()
            End Get
        End Property

        <CLSCompliant(False)> _
        Public ReadOnly Property TotalVirtualMemory() As ULong
            Get
                Throw New NotImplementedException()
            End Get
        End Property

        Friend Class ComputerInfoDebugView
            'If you want to view ComputerInfo classes in VS, implement me
        End Class
    End Class
End Namespace
#End If
