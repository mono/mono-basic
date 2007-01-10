Option Strict Off
Imports System

Module WithStatementA
    Class C1
        Public a1 As Integer = 10
        Friend a2 As String = "Hello"
        Function f1()
            Dim flag As Boolean = True
            If a1 = 20 And a2 = "Hello World" Then
                flag = False
            End If
            If a1 = 3 And a2 = "In nested With statement" Then
                flag = False
            End If
            If a1 = 25 And a2 = "Me too" Then
                flag = False
            End If
            If flag <> False Then
                System.Console.WriteLine("#A WithStatement not working") : Return 1
            End If
        End Function
    End Class

    Function Main() As Integer
        Dim a As New C1()
        With a
            .a1 = 20
            .a2 = "Hello World"
            .f1()
            Dim x As New C1()
            x.a1 = 2
            With x
                .a1 = 3
                .a2 = "In nested With statement"
                .f1()
                a.a1 = 25
                a.a2 = "Me too"
                a.f1()
            End With
        End With

        With a     ' Empty With statement
        End With

    End Function

End Module