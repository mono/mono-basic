Class TypeConversion
    Shared Sub Main()
        Dim ia As IA

        ia = CType(1, ia)
    End Sub
End Class

Interface IA

End Interface
Interface IB

End Interface
Interface IC
    Inherits IA, IB
End Interface