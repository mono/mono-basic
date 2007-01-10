Imports System

'Testing a inner class that inherits from it's outer class

Class C4
    Class C5
        Inherits C4
        Public Function F1() As Integer
            Return F()
        End Function
    End Class
    Public Function F() As Integer
        Return 1
    End Function
End Class


Module Inheritance
    Function Main() As Integer
        Dim myC As New C4.C5()
        If myC.F1() <> 1 Then
            System.Console.WriteLine("InheritanceI:Failed-Error inheriting an inner class from it's outer class") : Return 1
        End If
    End Function
End Module
