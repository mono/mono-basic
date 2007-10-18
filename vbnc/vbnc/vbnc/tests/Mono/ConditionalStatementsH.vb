'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Module Test
    Function Main() As Integer
        Dim i As Integer = 5
        Select Case i
            Case 5
                i = 10
            Case 10
                i = 20
            Case 20
                i = 30
            Case 30
                i = 5
            Case Else
                i = 6
        End Select
        If i <> 10 Then
            System.Console.WriteLine("Select not working properly. Expected 10 but got " & i) : Return 1
        End If
    End Function
End Module

