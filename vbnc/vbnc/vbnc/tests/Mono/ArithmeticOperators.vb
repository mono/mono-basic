
Imports System

Module ArithmeticOperators

    Function Main() As Integer
        Dim a1, a3 As Integer
        Dim a2 As String

        a1 = 2 + 3
        If a1 <> 5 Then
            System.Console.WriteLine("#A1-AdditionOperator:Failed") : Return 1
        End If

        a1 = CInt(1204.08 + 3433)
        If a1 <> 4637 Then
            System.Console.WriteLine("#A2-AdditionOperator:Failed") : Return 1
        End If

        a3 = 2
        a2 = "235"
        a1 = CInt(a2) + a3
        If a1 <> 237 Then
            System.Console.WriteLine("#A3-AdditionOperator:Failed") : Return 1
        End If

        a1 = a3 + Nothing
        If a1 <> 2 Then
            System.Console.WriteLine("#A4-AdditionOperator:Failed") : Return 1
        End If

        Dim b1, b2, b3 As Char
        b1 = CChar("a")
        b2 = CChar("c")
        b3 = CChar(b1 + b2)
        If b3 <> "a" Then
            System.Console.WriteLine("#A5-AdditionOperator:Failed") : Return 1
        End If

        Dim c1 As Double
        c1 = 463.338 - 338.333
        If c1 <> 125.005 Then
            System.Console.WriteLine("#A6-SubtractionOperator:Failed") : Return 1
        End If

        c1 = 463.338 * 338.3
        If c1 <> 156747.2454 Then
            System.Console.WriteLine("#A7-MultiplicationOperator:Failed") : Return 1
        End If

    End Function

End Module