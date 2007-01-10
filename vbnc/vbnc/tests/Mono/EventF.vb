Imports System

Class C
    Delegate Sub EH()
    Public Event E As EH

    Public Function S() As Object
        RaiseEvent E()
    End Function

    Sub bxh()
        Console.WriteLine("event called from other class")
    End Sub
End Class

Class C1
    Function call_S() As Object
        Dim x As C = New C()
        AddHandler x.E, AddressOf Me.xh
        AddHandler x.E, AddressOf x.bxh
        x.S()
    End Function

    Sub xh()
        Console.WriteLine("event called")
    End Sub
End Class

Module M
    Function Main() As Integer
        Dim y As New C1
        y.call_S()
    End Function

End Module
