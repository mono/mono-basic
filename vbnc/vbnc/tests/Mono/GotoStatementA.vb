'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Module gotostmt
    Function Main() As Integer
        Dim i As Integer
        GoTo a
        i = i + 1
a:
        i = i + 1
        If i <> 1 Then
            System.Console.WriteLine("Goto statement not working properly ") : Return 1
        End If
    End Function
End Module
