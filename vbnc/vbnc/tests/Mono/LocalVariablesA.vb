Option Strict Off
Imports System

Module LocalVariablesA
    Dim failed As Boolean
    Function swap(ByVal a As Integer, ByVal b As Integer) As Integer
        Dim c As Integer
        c = a
        a = b
        b = c
        Return 0
    End Function

    ' Local variable having same name as sub containing it
    Sub f2()
        Dim f2 As Integer = 1
        f2 = f2 + 1
        If f2 <> 2 Then
            System.Console.WriteLine("#A1 Local Variables not working") : failed = True
        End If
    End Sub

    Function Main() As Integer
        Dim a, b As Integer
        a = 10 : b = 32
        If a <> 10 And b <> 32 Then
            System.Console.WriteLine("#A2 Local Variables not working") : Return 1
        End If
        swap(a, b)
        If a <> 10 And b <> 32 Then
            System.Console.WriteLine("#A3 Local Variables not working") : Return 1
        End If
        f2()
        If failed Then Return 1
    End Function

End Module