'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Module Test
    Function Main() As Integer
        Dim x As Integer
        Dim i As Integer = 0
        x = 3
        Do While x <> 1
            i = i + 1
            x = x - 1
        Loop
        If i <> 2 Then
            System.Console.WriteLine("While not working properly. Expected 2 but got " & i) : Return 1
        End If
    End Function
End Module
