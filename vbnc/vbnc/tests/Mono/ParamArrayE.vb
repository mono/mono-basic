Option Strict Off
Imports System

Module Module1
    Class C
        Function F(ByVal ParamArray a As Object()) As Integer
            Return 0
        End Function

        Function F()
            Return 1
        End Function

        Function F(ByVal a As Object, ByVal b As Object)
            Return 2
        End Function

        Function F(ByVal a As Object, ByVal b As Object, _
         ByVal ParamArray c As Object())
            Return 3
        End Function
    End Class

    Function Main() As Integer
        Dim obj As Object = New C()
        If obj.F() <> 1 Then
            Throw New exception("#A1 - Overload resolution not working in Late binding")
        End If
        If obj.F(1) <> 0 Then
            Throw New exception("#A2 - Overload resolution not working in Late binding")
        End If
        If obj.F(1, 2) <> 2 Then
            Throw New exception("#A3 - Overload resolution not working in Late binding")
        End If
        If obj.F(1, 2, 3) <> 3 Then
            Throw New exception("#A4 - Overload resolution not working in Late binding")
        End If

    End Function
End Module
