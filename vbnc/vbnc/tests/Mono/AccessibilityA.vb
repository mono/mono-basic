Imports System
Class C1
    Protected Friend a As Integer
End Class
Class C2
    Inherits C1
    Public Function S() As Object
        a = 100
    End Function
End Class

Module Accessibility
    Function Main() As Integer
        Dim myC1 As New C1()
        myC1.a = 1000
        Dim myC2 As New C2()
        myC2.S()

    End Function
End Module
