Option Strict Off

Imports System

Module ShiftOperatorsA

    Function Main() As Integer
        Dim a1 As Double = 200.93
        a1 = a1 >> 109.95
        If a1 <> 0 Then
            System.Console.WriteLine("#A1 Shift operator not working") : Return 1
        End If
    End Function

End Module