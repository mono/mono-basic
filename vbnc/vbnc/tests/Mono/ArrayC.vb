Imports System

Module VariableC
    Dim a() As Integer = {1, 2, 3, 4, 5}

    Function Main() As Integer
        ReDim Preserve a(10)

        a(7) = 8
        If a(7) <> 8 Then
            System.Console.WriteLine("#A1, Unexpected result") : Return 1
        End If

        If a(2) <> 3 Then
            System.Console.WriteLine("#A2, Unexpected result - Preserve keyword not working") : Return 1
        End If
    End Function
End Module
