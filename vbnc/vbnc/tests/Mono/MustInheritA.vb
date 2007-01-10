Option Strict Off
'Testing MustInherit classes with MustOverride methods that do not return a value

MustInherit Class C1
    Public Function F1()
        Dim a As Integer = 10
    End Function
    Public MustOverride Function F2()
End Class

Class C2
    Inherits C1
    Public Overrides Function F2()
    End Function
End Class

Module Module1
    Function Main() As Integer
        Dim x As C1 = Nothing
        x = New C2()
    End Function
End Module
