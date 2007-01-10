Option Strict Off
Imports System
Module DecimalLiteral
    Function Main() As Integer
        Dim a As Decimal = True
        If a <> -1 Then
            System.Console.WriteLine("DecimalLiteralB:Failed") : Return 1
        End If
    End Function
End Module
