Option Strict Off
Imports System

Module WithStatementC
    Class C1
        Public a1 As Integer = 10
        Public a2 As String = "Hello"
        Function f1()
            Console.WriteLine("Class C1: {0} {1}", a1, a2)
        End Function
    End Class

    Function Main() As Integer
        Dim a As New C1()
        With a
            .a2 = "Hello World"
            GoTo labelA
            ' Exit before all statements in With have been executed  
            .a1 = 20
            Dim x As New C1()
            a = x
            If .a1 = a.a1 Or .a2 = a.a2 Then
                System.Console.WriteLine("#WS1 - With Statement failed") : Return 1
            End If
            a.f1()
            .f1()
labelA:
        End With
        If a.a1 = 20 Then
            System.Console.WriteLine("#WS2- Exit from With Statement failed") : Return 1
        End If
    End Function

End Module