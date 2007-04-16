'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off
Module Test
    Function Main() As Integer
        Dim i As Integer = 10
        Select Case i
            Case 10.5
                i = 15
            Case 20.5
                i = 20
            Case 21.5
                i = 21
            Case 30.5
                i = 30
        End Select
        If i <> 15 Then
            System.Console.WriteLine("Select not working properly. Expected 15 but got " & i) : Return 1
        End If
    End Function
End Module

