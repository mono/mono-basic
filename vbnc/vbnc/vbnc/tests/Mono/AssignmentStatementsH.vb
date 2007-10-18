'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

'Expected  System.ArgumentException: Argument 'Start' is not a valid value

Module Test
    Function Main() As Integer
        Dim s1 As String = "abcdefg"
        Dim s2 As String = "1234567"
        Dim i As Integer = 0
        Try
            Mid$(s1, -1, 3) = s2
        Catch e As System.Exception
            i = 1
        End Try
        If i <> 1 Then
            System.Console.WriteLine("Mid Assignment statement is not working properly") : Return 1
        End If
    End Function
End Module

