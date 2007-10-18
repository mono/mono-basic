'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Module M
    Function Main() As Integer
        Dim count As Integer
        For i As Integer = 15 To 10 Step 1
            count = count + 1
        Next i
        If count <> 0 Then
            System.Console.WriteLine("#A1 For not working") : Return 1
        End If
    End Function
End Module
