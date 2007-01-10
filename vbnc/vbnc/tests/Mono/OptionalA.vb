Option Strict Off
Imports System

Module Test
    Enum E
        A
        B
    End Enum
    Function F(Optional ByVal i As Integer = 42) As Integer
        F = i + 1
    End Function
    Function F2(Optional ByVal i As Integer = 42) As Integer
        F2 = i + 1
    End Function
    Function G(ByVal i As Integer, Optional ByVal j As Integer = 42) As Integer
        G = i + j
    End Function
    Function G(ByVal e As E) As Integer
        G = e
    End Function
    Function H(ByVal i As Integer, Optional ByVal j As Integer = 42, Optional ByVal k As Integer = 3) As Integer
        H = i + j + k
    End Function
    Function K(Optional ByRef i As Integer = 3) As Integer
        K = i
        i = i + 3
    End Function
    Function Main() As Integer
        If F() <> 43 Then
            System.Console.WriteLine("#A1: unexpected return value") : Return 1
        End If
        If F(99) <> 100 Then
            System.Console.WriteLine("#A2: unexpected return value") : Return 1
        End If
        If F2() <> 43 Then
            System.Console.WriteLine("#A3: unexpected return value") : Return 1
        End If
        If G(1) <> 43 Then
            System.Console.WriteLine("#A4: unexpected return value") : Return 1
        End If
        If G(E.A) <> 0 Then
            System.Console.WriteLine("#A5: unexpected return value") : Return 1
        End If
        If G(1, 99) <> 100 Then
            System.Console.WriteLine("#A6: unexpected return value") : Return 1
        End If
        If G(E.A, 99) <> 99 Then
            System.Console.WriteLine("#A7: unexpected return value") : Return 1
        End If
        If H(1) <> 46 Then
            System.Console.WriteLine("#A8: unexpected return value") : Return 1
        End If
        If H(1, 0) <> 4 Then
            System.Console.WriteLine("#A9: unexpected return value") : Return 1
        End If
        If H(E.A) <> 45 Then
            System.Console.WriteLine("#A10: unexpected return value") : Return 1
        End If
        If K() <> 3 Then
            System.Console.WriteLine("#A11: unexpected return value") : Return 1
        End If
        Dim i As Integer = 9
        If K(i) <> 9 OrElse i <> 12 Then
            System.Console.WriteLine("#A12: unexpected return value") : Return 1
        End If
    End Function
End Module
