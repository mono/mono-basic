Option Strict Off
Imports System

Module ConstantA
    Public Const a As Integer = 10
    Const b As Boolean = True, c As Long = 20
    Const d = 20
    Const e% = 10
    Const f% = 10, g# = 20
    Function Main() As Integer
        If a.GetTypeCode() <> TypeCode.Int32 Then
            System.Console.WriteLine("#A1, Type mismatch found: {0}", a.GetTypeCode()) : Return 1
        End If
        If b.GetTypeCode() <> TypeCode.Boolean Then
            System.Console.WriteLine("#A2, Type mismatch found") : Return 1
        End If
        If c.GetTypeCode() <> TypeCode.Int64 Then
            System.Console.WriteLine("#A3, Type mismatch found") : Return 1
        End If
        If d.GetTypeCode() <> TypeCode.Int32 Then
            System.Console.WriteLine("#A4, Type mismatch found") : Return 1
        End If
        If e.GetTypeCode() <> TypeCode.Int32 Then
            System.Console.WriteLine("#A5, Type mismatch found") : Return 1
        End If
        If f.GetTypeCode() <> TypeCode.Int32 Then
            System.Console.WriteLine("#A6, Type mismatch found") : Return 1
        End If
        If g.GetTypeCode() <> TypeCode.Double Then
            System.Console.WriteLine("#A7, Type mismatch found") : Return 1
        End If
    End Function
End Module
