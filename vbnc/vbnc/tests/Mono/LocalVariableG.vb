'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Module F
    Function fun() As Integer
        fun = 10
    End Function
    Function Main() As Integer
        Dim i As Integer = fun()
        If I <> 10 Then
            System.Console.WriteLine("Local Variables not working properly. Expected 10 but got" & i) : Return 1
        End If
    End Function
End Module
