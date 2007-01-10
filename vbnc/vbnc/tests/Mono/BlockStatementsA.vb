Imports System

Module BlockStatementsA

    Function Main() As Integer
        Dim a As Integer = 10
        If a = 10 Then
            GoTo a
        End If

label:  a = 11

a:      a = 5
        If a = 5 Then
            GoTo 123
        End If

123:    a = 7
        If a = 7 Then
            GoTo _12ab
        End If

_12ab:  a = 8
        If a = 8 Then
            GoTo [class]
        End If

[class]: a = 0
        If a <> 0 Then
            System.Console.WriteLine("#A1 Block statements not working") : Return 1
        End If

        ' label declaration always takes precedence in any ambiguous situation

f1:     Console.WriteLine("Heh") : a = 1 : f1()

    End Function

    Function f1() As Boolean
        Console.WriteLine("Inside function f1()")
        Return True
    End Function

End Module