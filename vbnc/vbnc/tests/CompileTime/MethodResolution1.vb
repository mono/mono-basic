Option Strict On
Option Explicit On

Imports Generic = System.Collections.Generic

Public Class Main

    Private Class Foo
        Public FooVal As Integer
    End Class

    Private Shared dict As New Generic.Dictionary(Of String, Foo)

    Public Shared Sub Main()
        Dim dictCopy As New Generic.Dictionary(Of String, Foo)(dict)
    End Sub

End Class
