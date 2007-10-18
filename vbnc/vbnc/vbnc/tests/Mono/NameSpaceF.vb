Option Strict Off
'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

'Checking For Alias

Imports A = Thisisaverylongname

Namespace Thisisaverylongname
    Module B
        Function C()
            Return 10
        End Function
    End Module
End Namespace

Module NamespaceA
    Function Main() As Integer
        If A.B.C() <> 10 Then
            System.Console.WriteLine("Alias Not Working") : Return 1
        End If
    End Function
End Module

