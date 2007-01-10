Imports System
Module SingleLiteral
    Function Main() As Integer
        Dim a As Single = 1.23F
        Dim b As Single = 1.23E+10F
        Dim c As Single = 9.223372E+18F
        Dim d As Single = 0.23F
        Dim f As Single
        If a <> 1.23F Then
            System.Console.WriteLine("#A1-SingleLiteralA:Failed") : Return 1
        End If
        If b <> 1.23E+10F Then
            System.Console.WriteLine("#A2-SingleLiteralA:Failed") : Return 1
        End If
        If c <> 9.223372E+18F Then
            System.Console.WriteLine("#A3-SingleLiteralA:Failed") : Return 1
        End If
        If d <> 0.23F Then
            System.Console.WriteLine("#A4-SingleLiteralA:Failed") : Return 1
        End If
        If f <> 0 Then
            System.Console.WriteLine("#A5-SingleLiteralA:Failed") : Return 1
        End If
    End Function
End Module
