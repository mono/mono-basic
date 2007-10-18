Option Strict Off
Imports System

Module M
    Public Enum E1 As Long
        A = 2.3
        B = 2.5
    End Enum

    Function Main() As Integer
        If E1.A <> 2 Then
            System.Console.WriteLine("#A1 Enum not working") : Return 1
        End If
    End Function



End Module
