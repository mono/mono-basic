Imports System

Class C
    Delegate Sub EH()
    Public Event E As EH

    Public Function S() As Object
        RaiseEvent E()
    End Function
End Class

Class C1
    Dim WithEvents x As C = New C()
    Function call_S() As Object
        x.S()
    End Function

    Sub EH() Handles x.E
        Console.WriteLine("event called")
    End Sub
End Class

Module M
    Function Main() As Integer
        Dim y As New C1
        y.call_S()
    End Function
End Module
