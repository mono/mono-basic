'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off
Module Test
    Function Main() As Integer
        Dim i As Integer = 10
        Select Case i
            Case 20 - 10.5
                i = 15
        End Select
        If i <> 15 Then
            System.Console.WriteLine("Select not working properly. Expected 15 but got " & i) : Return 1
        End If
    End Function
End Module

