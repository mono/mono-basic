Class Nullable3
    Public Shared Function Main() As Integer
        Dim d As Decimal

        i(1)
        dt(DateTime.MinValue)
        dec(d)
    End Function
    Shared Sub I(ByVal v As Integer?)

    End Sub
    Shared Sub DT(ByVal v As Date?)

    End Sub
    Shared Sub Dec(ByVal dec As Decimal?)

    End Sub
    Shared Sub Dec(ByVal dec As String)

    End Sub
End Class