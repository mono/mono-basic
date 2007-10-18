Imports System
Imports System.Collections
Imports System.Reflection
Imports System.Runtime.InteropServices

Namespace Attribute1
    Class Test
        <DllImport("libc", EntryPoint:="stime", _
            SetLastError:=True, CharSet:=CharSet.Unicode, _
            ExactSpelling:=True, _
            CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function stime(ByVal t As Integer) As Integer
            ' Leave function empty - DllImport attribute forwards calls to stime to
            ' stime in libc.dll
        End Function

        Shared Function Main() As Integer

        End Function
    End Class
End Namespace