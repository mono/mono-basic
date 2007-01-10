Option Strict Off
Imports System

Module Variables
    Function Main() As Integer
        Dim a As Integer
        If a <> 0 Then
            System.Console.WriteLine("Variables : Failed-Error assigning default value to variables") : Return 1
        End If
        Dim b 'Default type is Object
        If b <> "" Then
            System.Console.WriteLine("Variables : Failed-Error in implicit conversion of Object to string") : Return 1
        End If
        If b <> 0 Then
            System.Console.WriteLine("Variables : Failed-Error in implicit conversion of Object to integer") : Return 1
        End If
        If b <> 0.0 Then
            System.Console.WriteLine("Variables : Failed-Error in implicit conversion of Object to double") : Return 1
        End If
    End Function
End Module
