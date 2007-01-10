'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Module Test
    Public i As Integer
    Function fun() As Integer
        Try
            Return 1
        Finally
            i = 10
        End Try
    End Function
    Function Main() As Integer
        fun()
        If i <> 10 Then
            System.Console.WriteLine("Finally block not working... Expected 10 but got " & i) : Return 1
        End If
    End Function
End Module
