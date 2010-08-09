Option Explicit On
Option Strict On
Imports System

Public Class TestCase3

    Public Sub New()
        MyBase.New()
    End Sub

    Public Function ReturnAnArray(ByVal anyParam As Object, ByVal anotherParam As Object) As Integer()
        ReDim ReturnAnArray(-1)
        Dim ReturnValueUpperBound As Integer = ReturnAnArray.GetUpperBound(0)
    End Function

End Class
