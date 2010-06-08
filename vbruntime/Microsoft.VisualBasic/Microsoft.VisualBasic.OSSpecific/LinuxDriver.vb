'
' LinuxDriver.vb
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
Imports System
Imports System.Runtime.InteropServices

Namespace Microsoft.VisualBasic.OSSpecific
    Friend Class LinuxDriver
        Inherits OSDriver

        Public Overrides Sub SetDate(ByVal Value As Date)
            Dim Now As System.DateTime = DateTime.Now
            Dim NewDate As System.DateTime = New DateTime(Value.Year, Value.Month, Value.Day, Now.Hour, Now.Minute, Now.Second, Now.Millisecond)
            Dim secondsTimeSpan As System.TimeSpan = NewDate.ToUniversalTime().Subtract(New DateTime(1970, 1, 1, 0, 0, 0))
            Dim seconds As Integer = CType(secondsTimeSpan.TotalSeconds, Integer)

#If TARGET_JVM = False Then
            If (stime(seconds) = -1) Then
                Throw New UnauthorizedAccessException("The caller is not the super-user.")
            End If
#Else
            MyBase.SetTime (Value)
#End If
        End Sub


        Public Overrides Sub SetTime(ByVal Value As Date)
            Dim Now As System.DateTime = DateTime.Now
            Dim NewDate As System.DateTime = New DateTime(Now.Year, Now.Month, Now.Day, Value.Hour, Value.Minute, Value.Second, Value.Millisecond)
            Dim secondsTimeSpan As System.TimeSpan = NewDate.ToUniversalTime().Subtract(New DateTime(1970, 1, 1, 0, 0, 0))
            Dim seconds As Integer = CType(secondsTimeSpan.TotalSeconds, Integer)

#If TARGET_JVM = False Then
            If (stime(seconds) = -1) Then
                Throw New UnauthorizedAccessException("The caller is not the super-user.")
            End If
#Else
            MyBase.SetTime (Value)
#End If
        End Sub

#If TARGET_JVM = False Then
        <DllImport("libc", EntryPoint:="stime", _
           SetLastError:=True, CharSet:=CharSet.Unicode, _
           ExactSpelling:=True, _
           CallingConvention:=CallingConvention.StdCall)> _
        Friend Shared Function stime(ByRef t As Integer) As Integer
            ' Leave function empty - DllImport attribute forwards calls to stime to
            ' stime in libc.dll
        End Function
#End If

    End Class
End Namespace