Option Strict Off
Interface I
    Function F1(ByVal i As Integer)
    Function F2(ByVal i As Integer)
End Interface

Class C1
    Implements I

    Public Function F1(ByVal i As Integer) Implements I.F1
    End Function
    Public Function F2(ByVal i As Integer) Implements I.F2
    End Function
End Class

Module InterfaceD
    Function Main() As Integer
        Dim myC As C1 = New C1()
        myC.F1(10)
        myC.F2(20)
    End Function
End Module
