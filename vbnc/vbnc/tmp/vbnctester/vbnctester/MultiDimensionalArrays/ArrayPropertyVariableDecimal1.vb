Public Module ArrayPropertyVariableDecimal1
    Dim _a As Decimal()
    Dim _b As Decimal(,)
    Dim _c As Decimal(,,)
    Dim _d As Decimal(,,,)
    Dim _aa() As Decimal
    Dim _bb(,) As Decimal
    Dim _cc(,,) As Decimal
    Dim _dd(,,,) As Decimal

    Property a() As Decimal()
        Get
            Return _a
        End Get
        Set(ByVal value As Decimal())
            _a = value
        End Set
    End Property

    Property b() As Decimal(,)
        Get
            Return _b
        End Get
        Set(ByVal value As Decimal(,))
            _b = value
        End Set
    End Property

    Property c() As Decimal(,,)
        Get
            Return _c
        End Get
        Set(ByVal value As Decimal(,,))
            _c = value
        End Set
    End Property

    Property d() As Decimal(,,,)
        Get
            Return _d
        End Get
        Set(ByVal value As Decimal(,,,))
            _d = value
        End Set
    End Property
    Property aa() As Decimal()
        Get
            Return _aa
        End Get
        Set(ByVal value As Decimal())
            _aa = value
        End Set
    End Property

    Property bb() As Decimal(,)
        Get
            Return _bb
        End Get
        Set(ByVal value As Decimal(,))
            _bb = value
        End Set
    End Property

    Property cc() As Decimal(,,)
        Get
            Return _cc
        End Get
        Set(ByVal value As Decimal(,,))
            _cc = value
        End Set
    End Property

    Property dd() As Decimal(,,,)
        Get
            Return _dd
        End Get
        Set(ByVal value As Decimal(,,,))
            _dd = value
        End Set
    End Property

    Function Main() As Int32
        Dim result As Int32

        a = New Decimal() {}
        b = New Decimal(,) {}
        c = New Decimal(,,) {}
        d = New Decimal(,,,) {}

        aa = New Decimal() {}
        bb = New Decimal(,) {}
        cc = New Decimal(,,) {}
        dd = New Decimal(,,,) {}

        result += ArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += ArrayVerifier.Report()
        If b.Length <> 0 Then result += ArrayVerifier.Report()
        If c.Length <> 0 Then result += ArrayVerifier.Report()
        If d.Length <> 0 Then result += ArrayVerifier.Report()

        If aa.Length <> 0 Then result += ArrayVerifier.Report()
        If bb.Length <> 0 Then result += ArrayVerifier.Report()
        If cc.Length <> 0 Then result += ArrayVerifier.Report()
        If dd.Length <> 0 Then result += ArrayVerifier.Report()

        a = New Decimal() {}
        b = New Decimal(,) {{}}
        c = New Decimal(,,) {{{}}}
        d = New Decimal(,,,) {{{{}}}}

        aa = New Decimal() {}
        bb = New Decimal(,) {{}}
        cc = New Decimal(,,) {{{}}}
        dd = New Decimal(,,,) {{{{}}}}

        result += ArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += ArrayVerifier.Report()
        If b.Length <> 0 Then result += ArrayVerifier.Report()
        If c.Length <> 0 Then result += ArrayVerifier.Report()
        If d.Length <> 0 Then result += ArrayVerifier.Report()

        If aa.Length <> 0 Then result += ArrayVerifier.Report()
        If bb.Length <> 0 Then result += ArrayVerifier.Report()
        If cc.Length <> 0 Then result += ArrayVerifier.Report()
        If dd.Length <> 0 Then result += ArrayVerifier.Report()


        a = New Decimal() {1D, 2D}
        b = New Decimal(,) {{10D, 11D}, {12D, 13D}}
        c = New Decimal(,,) {{{20D, 21D}, {22D, 23D}}, {{24D, 25D}, {26D, 27D}}}
        d = New Decimal(,,,) {{{{30D, 31D}, {32D, 33D}}, {{34D, 35D}, {36D, 37D}}}, {{{40D, 41D}, {42D, 43D}}, {{44D, 45D}, {46D, 47D}}}}

        aa = New Decimal() {1D, 2D}
        bb = New Decimal(,) {{10D, 11D}, {12D, 13D}}
        cc = New Decimal(,,) {{{20D, 21D}, {22D, 23D}}, {{24D, 25D}, {26D, 27D}}}
        dd = New Decimal(,,,) {{{{30D, 31D}, {32D, 33D}}, {{34D, 35D}, {36D, 37D}}}, {{{40D, 41D}, {42D, 43D}}, {{44D, 45D}, {46D, 47D}}}}

        result += ArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 2 Then result += ArrayVerifier.Report()
        If b.Length <> 4 Then result += ArrayVerifier.Report()
        If c.Length <> 8 Then result += ArrayVerifier.Report()
        If d.Length <> 16 Then result += ArrayVerifier.Report()

        If aa.Length <> 2 Then result += ArrayVerifier.Report()
        If bb.Length <> 4 Then result += ArrayVerifier.Report()
        If cc.Length <> 8 Then result += ArrayVerifier.Report()
        If dd.Length <> 16 Then result += ArrayVerifier.Report()

        a = New Decimal() {51D, 52D}
        b = New Decimal(,) {{50D, 51D}, {52D, 53D}}
        c = New Decimal(,,) {{{60D, 61D}, {62D, 63D}}, {{64D, 65D}, {66D, 67D}}}
        d = New Decimal(,,,) {{{{70D, 71D}, {72D, 73D}}, {{74D, 75D}, {76D, 77D}}}, {{{80D, 81D}, {82D, 83D}}, {{84D, 85D}, {86D, 87D}}}}

        aa(0) = 51D
        aa(1) = 52D
        bb(0, 0) = 50D
        bb(0, 1) = 51D
        bb(1, 0) = 52D
        bb(1, 1) = 53D
        cc(0, 0, 0) = 60D
        cc(0, 0, 1) = 61D
        cc(0, 1, 0) = 62D
        cc(0, 1, 1) = 63D
        cc(1, 0, 0) = 64D
        cc(1, 0, 1) = 65D
        cc(1, 1, 0) = 66D
        cc(1, 1, 1) = 67D

        dd(0, 0, 0, 0) = 70D
        dd(0, 0, 0, 1) = 71D
        dd(0, 0, 1, 0) = 72D
        dd(0, 0, 1, 1) = 73D
        dd(0, 1, 0, 0) = 74D
        dd(0, 1, 0, 1) = 75D
        dd(0, 1, 1, 0) = 76D
        dd(0, 1, 1, 1) = 77D

        dd(1, 0, 0, 0) = 80D
        dd(1, 0, 0, 1) = 81D
        dd(1, 0, 1, 0) = 82D
        dd(1, 0, 1, 1) = 83D
        dd(1, 1, 0, 0) = 84D
        dd(1, 1, 0, 1) = 85D
        dd(1, 1, 1, 0) = 86D
        dd(1, 1, 1, 1) = 87D

        result += ArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 2 Then result += ArrayVerifier.Report()
        If b.Length <> 4 Then result += ArrayVerifier.Report()
        If c.Length <> 8 Then result += ArrayVerifier.Report()
        If d.Length <> 16 Then result += ArrayVerifier.Report()

        If aa.Length <> 2 Then result += ArrayVerifier.Report()
        If bb.Length <> 4 Then result += ArrayVerifier.Report()
        If cc.Length <> 8 Then result += ArrayVerifier.Report()
        If dd.Length <> 16 Then result += ArrayVerifier.Report()

        Return result
    End Function

End Module
