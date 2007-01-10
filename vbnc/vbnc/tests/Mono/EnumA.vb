Imports System

Module M
    Enum E
        A
        B
        C
        D
        E = 10
        F
        G = 5
        H
        I = D
        J = -10
        K
    End Enum


    Public Enum E1 As Long
        A = 10
        B = 20
    End Enum

    Function Main() As Integer
        Dim i As Integer
        i = E.A
        If i <> 0 Then
            System.Console.WriteLine("#A1, unexpected result") : Return 1
        End If

        i = E.B
        If i <> 1 Then
            System.Console.WriteLine("#A2, unexpected result") : Return 1
        End If

        i = E.C
        If i <> 2 Then
            System.Console.WriteLine("#A3, unexpected result") : Return 1
        End If
        i = E.D
        If i <> 3 Then
            System.Console.WriteLine("#A4, unexpected result") : Return 1
        End If
        i = E.E
        If i <> 10 Then
            System.Console.WriteLine("#A5, unexpected result") : Return 1
        End If
        i = E.F
        If i <> 11 Then
            System.Console.WriteLine("#A6, unexpected result") : Return 1
        End If
        i = E.G
        If i <> 5 Then
            System.Console.WriteLine("#A7, unexpected result") : Return 1
        End If
        i = E.H
        If i <> 6 Then
            System.Console.WriteLine("#A8, unexpected result") : Return 1
        End If
        i = E.I
        If i <> 3 Then
            System.Console.WriteLine("#A9, unexpected result") : Return 1
        End If
        i = E.J
        If i <> -10 Then
            System.Console.WriteLine("#A10, unexpected result") : Return 1
        End If
        i = E.K
        If i <> -9 Then
            System.Console.WriteLine("#A11, unexpected result") : Return 1
        End If

        '        Console.WriteLine(E.A)
        '        Console.WriteLine(E.B)
        '        Console.WriteLine(E.C)
        '        Console.WriteLine(E.D)
        '        Console.WriteLine(E.E)
        '        Console.WriteLine(E.F)
        '        Console.WriteLine(E.G)
        '        Console.WriteLine(E.H)
        '        Console.WriteLine(E.I)
        '        Console.WriteLine(E.J)
        '        Console.WriteLine(E.K)
    End Function



End Module
