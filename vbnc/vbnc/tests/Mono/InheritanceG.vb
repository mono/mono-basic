Imports System

'Testing a class that inherits from an inner class of another class

Class C2
    Class C3
        Public Function F() As Integer
            Return 1
        End Function
    End Class
End Class
Class C1
    Inherits C2.C3
    Public Function F1() As Integer
        Return F()
    End Function
End Class

Module Inheritance
    Function Main() As Integer
        Dim myC As New C1()
        Dim a As Integer = myC.F1()
        If a <> 1 Then
            System.Console.WriteLine("InheritanceG:Failed - Error inheriting an inner class") : Return 1
        End If
    End Function
End Module
