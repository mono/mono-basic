Imports System
Module LongLiteral
    Function Main() As Integer
        Dim a As Long
        If a <> 0 Then
            System.Console.WriteLine("LongLiteralC:Failed-Default value assigned to long variable should be 0") : Return 1
        End If
    End Function
End Module
