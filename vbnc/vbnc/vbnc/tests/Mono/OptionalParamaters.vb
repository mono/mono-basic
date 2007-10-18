Option Strict Off
'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Public Enum E
    A = 11
    B
    C
End Enum

Module M
    Function Main() As Integer
        fun()
    End Function
    Function fun(Optional ByVal i As E = 1, Optional ByVal j As Integer = 20)
        If i <> 1 Or j <> 20 Then
            System.Console.WriteLine("#A1 Not working") : Return 1
        End If
    End Function
End Module
