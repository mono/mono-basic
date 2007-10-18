'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Module Test
    Function Main() As Integer
        Dim s1 As String = "abcdefg"
        Dim s2 As String = "1234567"

        Mid$(s1, 3, 3) = s2
        If s1 <> "ab123fg" Then
            System.Console.WriteLine("Mid Assingnment is not working. Excpected ab123fg but got " & s1) : Return 1
        End If
    End Function
End Module
