Option Strict Off
Imports System

'Testing simple and multi-level inheritence with all methods declared public

Public Class C1
    Public Function f1() As Integer
        Return 1
    End Function

    Public Function fn() As Integer
        Return 5
    End Function
End Class

Public Class C2
    Inherits C1
    Public Function f2() As Integer
        Return f1()
    End Function
End Class

Public Class c3
    Inherits C2
End Class

Module Inheritance
    Function Main() As Integer

        Dim c1 As Object = New C1()
        Dim a As Integer = c1.f1()
        If a <> 1 Then
            System.Console.WriteLine("#A1- Inheritence:Failed") : Return 1
        End If

        Dim c2 As Object = New C2()
        Dim b As Integer = c2.f1()
        Dim c As Integer = c2.f2()
        Dim d As Integer = c2.fn()

        If b <> 1 Then
            System.Console.WriteLine("#A2- Inheritence:Failed") : Return 1
        End If
        If c <> 1 Then
            System.Console.WriteLine("#A2- Inheritence:Failed") : Return 1
        End If
        If d <> 5 Then
            System.Console.WriteLine("#A2- Inheritence:Failed") : Return 1
        End If


        Dim c3 As Object = New c3()
        b = c3.f1()
        c = c3.f2()
        d = c3.fn()

        If b <> 1 Then
            System.Console.WriteLine("#A3- Inheritence:Failed") : Return 1
        End If
        If c <> 1 Then
            System.Console.WriteLine("#A3- Inheritence:Failed") : Return 1
        End If
        If d <> 5 Then
            System.Console.WriteLine("#A3- Inheritence:Failed") : Return 1
        End If

    End Function
End Module
