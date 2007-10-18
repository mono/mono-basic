Imports System

Class C
    Delegate Sub EH(ByVal i As Integer)
    Public Event E As EH

    Public Function S() As Object
        RaiseEvent E(10)
    End Function
End Class

Class C1
    Dim WithEvents x As C = New C()

    Function call_S() As Object
        x.S()
    End Function

    Sub EH(ByVal i As Integer) Handles x.E
        If i <> 10 Then
            System.Console.WriteLine("#A1 Event call FAils") : m.failed = True
        End If
    End Sub
End Class

Module M
    Public failed As Boolean
    Function Main() As Integer
        Dim y As New C1
        y.call_S()
        If failed Then Return 1
    End Function
End Module


