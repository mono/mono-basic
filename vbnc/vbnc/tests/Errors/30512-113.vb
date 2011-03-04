Option Strict On

Class TypeConversion
    Shared Sub Main()
        Dim ia As IA
        Dim ib As IB
        Dim ic As IC
        Dim sa As SA
        Dim da As DA
        Dim ca As CA

        ia = CType("", IA)
        ia = CType(ia, IA)
        ia = CType(ib, IA)
        ia = CType(da, IA)
        ia = CType(ca, IA)

        ia = ib
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

Delegate Sub DA()

Class CA
End Class