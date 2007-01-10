Option Strict Off
Imports System

Public Class C1
    Public Overridable Function F1(ByVal name As String)
        Dim t As Type = GetType(C1)
        If t.name <> name Then
            Throw New Exception("#A1, Should not some here")
        End If
    End Function
End Class

Public Class C2
    Inherits C1

    Public Overrides Function F1(ByVal name As String)
        Dim t As Type = GetType(C2)
        If t.name <> name Then
            Throw New Exception("#A2, Should not some here")
        End If
    End Function
End Class

Module InheritanceE
    Function Main() As Integer
        Dim b As Object = New C1()
        b.F1("C1")

        Dim d As Object = New C2()
        d.F1("C2")
    End Function
End Module



