Imports System

Public Class C
    Public Event E()

    Function S() As Object
        RaiseEvent E()
    End Function
End Class


Module M
    Public failed As Boolean = True
    Function S1() As Object
        Dim x As New C()
        AddHandler x.E, AddressOf EH
        x.S()
    End Function

    Sub EH()
        Console.WriteLine("Event fired")
        m.failed = False
    End Sub

    Function Main() As Integer
        S1()
        If failed Then Return 1
    End Function

End Module

