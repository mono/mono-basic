Option Strict Off
Imports System
Module SingleLiteral
    Function Main() As Integer
        Dim a As Single = True
        If a <> -1 Then
            System.Console.WriteLine("SingleLiteralB:Failed") : Return 1
        End If
    End Function
End Module
