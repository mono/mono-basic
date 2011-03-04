Option Strict On

Class TypeConversion
    Shared Sub Main()
        Dim ia As IA
        Dim sa As SA

        ia = CType("", IA)
        ia = CType(sa, SA)
    End Sub
End Class

Interface IA

End Interface
Interface IB

End Interface
Interface IC
    Inherits IA, IB
End Interface

Structure SA
    Dim v As Integer
End Structure