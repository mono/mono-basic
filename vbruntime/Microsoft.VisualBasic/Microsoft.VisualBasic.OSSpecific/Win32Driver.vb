'
' Win32Driver.vb
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

#If TARGET_JVM = False Then

Imports System.Runtime.InteropServices
Imports System

Namespace Microsoft.VisualBasic.OSSpecific
    Friend Class Win32Driver
        Inherits OSDriver

        Public Overrides Sub SetDate(ByVal newDate As Date)
            Dim time As SystemTime

            GetLocalTime(time)

            time.Year = CShort(newDate.Year)
            time.Month = CShort(newDate.Month)
            time.Day = CShort(newDate.Day)

            If SetLocalTime(time) = 0 Then
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error)
            End If
        End Sub

        Public Overrides Sub SetTime(ByVal newTime As Date)
            Dim time As SystemTime

            GetLocalTime(time)

            time.Hour = CShort(newTime.Hour)
            time.Minute = CShort(newTime.Minute)
            time.Second = CShort(newTime.Second)
            time.Milliseconds = CShort(newTime.Millisecond)

            If SetLocalTime(time) = 0 Then
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error)
            End If
        End Sub

        Declare Auto Sub GetLocalTime Lib "kernel32" (ByRef systime As SystemTime)
        Declare Auto Function SetLocalTime Lib "kernel32" (ByRef systime As SystemTime) As Integer

        <StructLayout(LayoutKind.Sequential)> _
        Friend Structure SystemTime
            <MarshalAs(UnmanagedType.U2)> Public Year As Short
            <MarshalAs(UnmanagedType.U2)> Public Month As Short
            <MarshalAs(UnmanagedType.U2)> Public DayOfWeek As Short
            <MarshalAs(UnmanagedType.U2)> Public Day As Short
            <MarshalAs(UnmanagedType.U2)> Public Hour As Short
            <MarshalAs(UnmanagedType.U2)> Public Minute As Short
            <MarshalAs(UnmanagedType.U2)> Public Second As Short
            <MarshalAs(UnmanagedType.U2)> Public Milliseconds As Short
        End Structure

    End Class
End Namespace
#End If