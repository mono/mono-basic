Imports System
Module DoubleLiteral
    Function Main() As Integer
        Dim a As Double = 1.23R
        Dim b As Double = 12300000000.0R
        Dim c As Double = 9.22337203685478E+18
        Dim d As Double = 0.23R
        Dim f As Double
        If a <> 1.23R Then
            System.Console.WriteLine("#A1-DoubleLiteralA:Failed") : Return 1
        End If
        If b <> 12300000000.0R Then
            System.Console.WriteLine("#A2-DoubleLiteralA:Failed") : Return 1
        End If
        If c <> 9.22337203685478E+18 Then
            System.Console.WriteLine("#A3-DoubleLiteralA:Failed") : Return 1
        End If
        If d <> 0.23R Then
            System.Console.WriteLine("#A4-DoubleLiteralA:Failed") : Return 1
        End If
        If f <> 0 Then
            System.Console.WriteLine("#A5-DoubleLiteralA:Failed") : Return 1
        End If
    End Function
End Module
