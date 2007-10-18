
Imports System

Module ArithmeticOperatorsA

    Function Main() As Integer

        Dim a1, a2 As Double
        a1 = 3 / 2
        If a1 <> 1.5 Then
            System.Console.WriteLine("#A1-RegularDivisionOperator:Failed") : Return 1
        End If

        Dim b1 As Integer

        b1 = 12 \ CLng(2.5)
        If b1 <> 6 Then
            System.Console.WriteLine("#A2-IntegerDivisionOperator:Failed") : Return 1
        End If

        b1 = 11 \ 4
        If b1 <> 2 Then
            System.Console.WriteLine("#A3-IntegerDivisionOperator:Failed") : Return 1
        End If

        b1 = 67 \ -4
        If b1 <> -16 Then
            System.Console.WriteLine("#A4-IntegerDivisionOperator:Failed") : Return 1
        End If

        a1 = 12 Mod 2
        If a1 <> 0 Then
            System.Console.WriteLine("#A5-ModOperator:Failed") : Return 1
        End If

        'a1 = 12.6 Mod 5
        'If a1 <> 2.6 Then
        'System.Console.WriteLine("#A6-ModOperator:Failed"):return 1
        'End If

        a1 = 2 ^ 3
        If a1 <> 8 Then
            System.Console.WriteLine("#A7-ExponentialOperator:Failed") : Return 1
        End If

        a1 = (-2) ^ 3
        If a1 <> -8 Then
            System.Console.WriteLine("#A8-ExponentialOperator:Failed") : Return 1
        End If


    End Function

End Module