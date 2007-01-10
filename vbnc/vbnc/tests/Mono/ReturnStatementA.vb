'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off
Module stopstmt
    Function fun()
        Return 10
    End Function
    Function fun1()
        Return "Hello"
    End Function
    Function Main() As Integer
        Dim i As Integer = fun()
        Dim s As String = fun1()
        If i <> 10 Or s <> "Hello" Then
            System.Console.WriteLine("Return not working") : Return 1
        End If
    End Function
End Module
