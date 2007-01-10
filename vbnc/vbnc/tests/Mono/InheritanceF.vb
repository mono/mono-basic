Option Strict Off
'Testing access to protected members of a class from it's derived class
Class C1
    Protected a As Integer
    Protected Function S()
        a = 47
    End Function
End Class
Class C2
    Inherits C1
    Public Function F() As Integer
        S()
        Return a
    End Function
End Class
Module Inheritence
    Function Main() As Integer
        Dim myC As New C2()
        Dim b As Integer = myC.F()
        If b <> 47 Then
            System.Console.WriteLine("InheritenceF:Failed-Error in accessing protected member from a derived class") : Return 1
        End If
    End Function
End Module
