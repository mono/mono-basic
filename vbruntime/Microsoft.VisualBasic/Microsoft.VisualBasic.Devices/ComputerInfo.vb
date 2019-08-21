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
Imports System.ComponentModel
Imports System.Globalization
Imports System.Diagnostics
Imports System.Runtime.InteropServices

Namespace Microsoft.VisualBasic.Devices
    <DebuggerTypeProxy(GetType(ComputerInfo.ComputerInfoDebugView))> _
    Public Class ComputerInfo
        Public Sub New()
            'Empty
        End Sub

        Private Function IsOnWindows() As Boolean
            Return Environment.OSVersion.Platform <> PlatformID.Unix AndAlso Environment.OSVersion.Platform <> 128
        End Function

        <StructLayout(LayoutKind.Sequential)>
        Private Structure MEMORYSTATUSEX
            Public dwLength As Integer
            Public dwMemoryLoad As Integer
            Public ullTotalPhys As ULong
            Public ullAvailPhys As ULong
            Public ullTotalPageFile As ULong
            Public ullAvailPageFile As ULong
            Public ullTotalVirtual As ULong
            Public ullAvailVirtual As ULong
            Public ullAvailExtendedVirtual As ULong
        End Structure

        <DllImport("kernel32", CallingConvention:=CallingConvention.StdCall)>
        Private Shared Function GlobalMemoryStatusEx(ByRef buf As MEMORYSTATUSEX) As Boolean
        End Function

        Private Function GetMemoryInfo() As MEMORYSTATUSEX
            Dim buf As New MEMORYSTATUSEX()
            buf.dwLength = Marshal.SizeOf(buf)
            If GlobalMemoryStatusEx(buf) Then
                Return buf
            Else
                Throw New Win32Exception()
            End If
        End Function

        <CLSCompliant(False)> _
        Public ReadOnly Property AvailablePhysicalMemory() As ULong
            Get
                If IsOnWindows() Then
                    Return GetMemoryInfo().ullAvailPhys
                Else
                    Throw New NotImplementedException()
                End If
            End Get
        End Property

        <CLSCompliant(False)> _
        Public ReadOnly Property AvailableVirtualMemory() As ULong
            Get
                If IsOnWindows() Then
                    Return GetMemoryInfo().ullAvailVirtual
                Else
                    Throw New NotImplementedException()
                End If
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
                If IsOnWindows() Then
                    Return GetMemoryInfo().ullTotalPhys
                Else
                    Throw New NotImplementedException()
                End If
            End Get
        End Property

        <CLSCompliant(False)> _
        Public ReadOnly Property TotalVirtualMemory() As ULong
            Get
                If IsOnWindows() Then
                    Return GetMemoryInfo().ullTotalVirtual
                Else
                    Throw New NotImplementedException()
                End If
            End Get
        End Property

        Friend Class ComputerInfoDebugView
            'If you want to view ComputerInfo classes in VS, implement me
        End Class
    End Class
End Namespace
#End If
