'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Module Test
    Function Main() As Integer
        Dim a As Integer = 10
        Dim s As String
        s = a.toString()
        If s <> "10" Then
            System.Console.WriteLine("Assignment not working. Expected 10 but got " & s) : Return 1
        End If
    End Function
End Module
