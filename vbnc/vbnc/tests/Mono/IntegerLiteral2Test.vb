Imports System

Module IntegerLiteral2Test
    Function Main() As Integer
        Dim i As Integer
        i = &H2B
        If (i <> 43) Then
            System.Console.WriteLine("#A1 : Unexpected behaviour") : Return 1
        End If

        i = &O35
        If (i <> 29) Then
            System.Console.WriteLine("#A2 : Unexpected behaviour") : Return 1
        End If
    End Function
End Module
