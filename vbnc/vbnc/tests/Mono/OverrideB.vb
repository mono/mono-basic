Option Strict Off
Class B
    Overridable Function F() As Integer
        Return 5
    End Function
End Class

Class D
    Inherits B

    Overrides Function F() As Integer
        ' you should be able to access 
        ' the members of base class 
        ' using 'MyBase' as follows
        MyBase.F()

        Return 10
    End Function
End Class

Module OverrideA
    Function Main() As Integer
        Dim x As Object

        x = New B()
        If x.F() <> 5 Then
            System.Console.WriteLine("#A1, unexpected result from base class") : Return 1
        End If

        x = New D()
        If x.F() <> 10 Then
            System.Console.WriteLine("#A2, unexpected result from derived class") : Return 1
        End If
    End Function
End Module
