Option Strict Off
Imports System

Structure S
    Dim a As String
    Const b As Integer = 25

    Class c
    End Class

    Function f(ByVal l As Long) As Long
        f = l
    End Function

    Structure S1
        Dim g As String
    End Structure

End Structure


Module M
    Function Main() As Integer
        Dim x As S

        x.a = 10
        If x.a <> 10 Then
            Throw New Exception("#A1, Unexpected result")
        End If

        Dim y As S = x

        x.a = 20
        If y.a <> 10 Then
            Throw New Exception("#A2, Unexpected result")
        End If
        If x.a <> 20 Then
            Throw New Exception("#A3, Unexpected result")
        End If

        If x.b <> 25 Then
            Throw New Exception("#A4, Unexpected result")
        End If
        'Console.WriteLine(x.b)

        If x.f(99) <> 99 Then
            Throw New Exception("#A5, Unexpected result")
        End If

    End Function
End Module
