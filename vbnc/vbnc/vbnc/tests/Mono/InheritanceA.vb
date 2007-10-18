Option Strict Off
Imports System
Public Class C1
    Dim t1 As Type = GetType(C1)

    Function f1(ByVal name As String) As Object
        If t1.name <> name Then
            System.Console.WriteLine("#A1 Unexpected result") : Return 1
        End If
    End Function

    Function f1(ByVal name As String, ByVal t As type) As Object
        If t.name <> name Then
            System.Console.WriteLine("#A2 Unexpected result") : Return 1
        End If
    End Function

    Function fn() As Integer
        Return 5
    End Function
End Class

Public Class C2
    Inherits C1
    Dim t As Type = GetType(C2)

    Function f2(ByVal name As String) As Object
        f1(name, t)
    End Function
End Class

Public Class C3
    Inherits C2
End Class

Module Inheritance
    Function Main() As Integer
        Dim a As Integer
        Dim c1 As Object = New C1()
        a = c1.f1("C1")

        Dim c2 As Object = New C2()
        a = c2.f1("C1")
        a = c2.f2("C2")
        a = c2.fn()

        Dim c3 As Object = New c3()
        c3.f1("C1")
        c3.f2("C2")
        c3.fn()
    End Function
End Module
