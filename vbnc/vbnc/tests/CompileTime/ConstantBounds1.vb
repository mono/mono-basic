Option Explicit Off

Class ConstantBounds1
    Sub One()
        Dim b(CByte(1))
        Dim sb(CSByte(1))
        Dim s(1S)
        Dim us(1US)
        Dim i(1I)
        Dim ui(1UI)
        Dim l(1L)
        Dim ul(1UL)
        Dim d(1D)
        Dim f(1.0F)
        Dim r(1.0R)
        Dim sgl(1.0!)
        Dim dbl(1.0#)
        Dim dec(1@)
    End Sub
End Class