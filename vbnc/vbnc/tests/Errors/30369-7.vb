Option Strict On
Option Explicit On

Public Class C
    Public Shared Event WizBangChanged As EventHandler
End Class

Class C2
    Dim C As C

    Shared Sub Accesses()
        Dim eh As New EventHandler(AddressOf SharedMethodA)
        AddHandler C.WizBangChanged, eh
    End Sub

    Public Shared Sub SharedMethodA(ByVal sender As Object, ByVal e As EventArgs)
        Console.WriteLine("curious!")
    End Sub
End Class
