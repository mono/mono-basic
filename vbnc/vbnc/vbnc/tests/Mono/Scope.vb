Option Strict Off
Module Scope
    Dim i As Integer = 5

    Function f1()
        If i <> 5 Then
            System.Console.WriteLine("#A1, value of i is not correct") : Return 1
        End If
    End Function

    Function f2()
        Dim i As Integer = 10
        If i <> 10 Then
            System.Console.WriteLine("#A2, value of i is not correct") : Return 1
        End If
        If Scope.i <> 5 Then
            System.Console.WriteLine("#A3, value of i is not correct") : Return 1
        End If
    End Function

    Function Main() As Integer
        f1()
        f2()
    End Function
End Module
