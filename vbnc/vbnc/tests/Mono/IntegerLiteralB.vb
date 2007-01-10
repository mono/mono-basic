Imports System
Module IntegerLiteral
    Function Main() As Integer
        Dim a As Integer
        If a <> 0 Then
            System.Console.WriteLine("IntegerLiteralC:Failed-Default value assigned to integer variable should be 0") : Return 1
        End If
    End Function
End Module
