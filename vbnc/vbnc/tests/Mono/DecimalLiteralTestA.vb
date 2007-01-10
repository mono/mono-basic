Imports System
Module DecimalLiteral
    Function Main() As Integer
        Dim a As Decimal = 1.23D
        Dim b As Decimal = 12300000000D
        Dim c As Decimal = 9223372036854780800D
        Dim d As Decimal = 0.23D
        Dim f As Decimal
        If a <> 1.23D Then
            System.Console.WriteLine("#A1-DecimalLiteralA:Failed") : Return 1
        End If
        If b <> 12300000000D Then
            System.Console.WriteLine("#A2-DecimalLiteralA:Failed") : Return 1
        End If
        If c <> 9223372036854780800D Then
            System.Console.WriteLine("#A3-DecimalLiteralA:Failed") : Return 1
        End If
        If d <> 0.23D Then
            System.Console.WriteLine("#A4-DecimalLiteralA:Failed") : Return 1
        End If
        If f <> 0 Then
            System.Console.WriteLine("#A5-DecimalLiteralA:Failed") : Return 1
        End If
    End Function
End Module
