Option Strict Off

Imports System

Module LogicalOperatorsA

    Function Main() As Integer
        Dim a1, a2 As Integer
        a1 = f1() AndAlso f2()
        a2 = a1 OrElse f1()
        If a1 <> 0 Then
            System.Console.WriteLine("#A1 Logical Operator not working") : Return 1
        End If
        If a2 <> -1 Then
            System.Console.WriteLine("#A2 Logical Operator not working") : Return 1
        End If
    End Function

    Function f1() As Integer
        Return 1
    End Function

    Function f2() As Boolean
        Return False
    End Function

End Module