Option Strict Off
Imports System
Module StringTypeCharTest
    Function Main() As Integer
        Dim m As String
        m = f(20)
        If m <> 20 Then
            System.Console.WriteLine("StringTypeCharTest: failed") : Return 1
        End If
        Exit Function
    End Function

    Function f$(ByVal param$)
        f$ = param
    End Function
End Module
