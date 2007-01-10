'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

'To prove "Else if" and "Elseif" are same

Module Test
    Function Main() As Integer
        Dim i As Integer = 1000
        If CBool(i) Then
            i = 4
        Else
            i = 1
        End If
        If i <> 4 Then
            System.Console.WriteLine("If else if not working properly. Expected 4 but got " & i) : Return 1
        End If
    End Function
End Module

