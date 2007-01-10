Option Strict Off
Imports System

Class A
    Public i As Integer
    Sub New()
        i = 20
    End Sub
    Sub New(ByVal a As A)
        i = a.i
    End Sub
End Class

Module Test
    Public Function Main() As Integer
        Dim a As Object = New A()
        Dim j As Object = New A(a)
        If j.i <> 20 Then
            System.Console.WriteLine("Initializer not working") : Return 1
        End If
    End Function
End Module
