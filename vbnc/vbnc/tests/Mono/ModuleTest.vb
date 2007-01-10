Imports System
Module ModuleTest
    Dim a As Integer = 30
    Function Main() As Integer
        Dim a As Integer
        If a <> 0 Then
            System.Console.WriteLine("#A1: Module:Failed") : Return 1
        End If
        If ModuleTest.a <> 30 Then
            System.Console.WriteLine("#A2: Module: Failed") : Return 1
        End If
    End Function
End Module
