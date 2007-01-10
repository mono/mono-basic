'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Module M
    Class A
        Public Shared i As Integer
    End Class
    Function Main() As Integer
        A.i = A.i + 1
        fun()
    End Function
    Function fun()
        A.i = A.i + 1
        If A.i <> 2 Then
            System.Console.WriteLine("Shared variable not working") : Return 1
        End If
    End Function
End Module
