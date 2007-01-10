Option Strict Off
Imports System


Module WithStatementB
    Class C1
        Public a1 As Integer = 10
        Friend a2 As String = "Hello"
        Function f1()
            Dim flag As Boolean = True
            If a1 = 20 And a2 = "Hello World" Then
                flag = False
            End If
            If a1 = 10 And a2 = "Hello" Then
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
            a.a1 = 20
            .a2 = "Hello World"
            Dim x As New C1()
            a = x  ' Tried reassiging the object inside With statement
            If .a1 = a.a1 Or .a2 = a.a2 Then
                System.Console.WriteLine("#WS1 - With Statement failed") : Return 1
            End If
            a.f1()
            .f1()
        End With

    End Function

End Module