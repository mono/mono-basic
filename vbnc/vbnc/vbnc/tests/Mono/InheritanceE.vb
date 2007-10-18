Option Strict Off
Public Class C1
    Public Overridable Function F1()
    End Function
End Class

Public Class C2
    Inherits C1

    Public Overrides Function F1()
    End Function
End Class

Module InheritanceE
    Function Main() As Integer
        Dim b As New C1()
        b.F1()

        Dim d As C2 = New C2()
        d.F1()
    End Function
End Module



