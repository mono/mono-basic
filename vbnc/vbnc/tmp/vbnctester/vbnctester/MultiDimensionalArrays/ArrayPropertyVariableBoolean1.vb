Public Module ArrayPropertyVariableBoolean1
    Dim _a As Boolean()
    Dim _b As Boolean(,)
    Dim _c As Boolean(,,)
    Dim _d As Boolean(,,,)
    Dim _aa() As Boolean
    Dim _bb(,) As Boolean
    Dim _cc(,,) As Boolean
    Dim _dd(,,,) As Boolean

    Property a() As Boolean()
        Get
            Return _a
        End Get
        Set(ByVal value As Boolean())
            _a = value
        End Set
    End Property

    Property b() As Boolean(,)
        Get
            Return _b
        End Get
        Set(ByVal value As Boolean(,))
            _b = value
        End Set
    End Property

    Property c() As Boolean(,,)
        Get
            Return _c
        End Get
        Set(ByVal value As Boolean(,,))
            _c = value
        End Set
    End Property

    Property d() As Boolean(,,,)
        Get
            Return _d
        End Get
        Set(ByVal value As Boolean(,,,))
            _d = value
        End Set
    End Property
    Property aa() As Boolean()
        Get
            Return _aa
        End Get
        Set(ByVal value As Boolean())
            _aa = value
        End Set
    End Property

    Property bb() As Boolean(,)
        Get
            Return _bb
        End Get
        Set(ByVal value As Boolean(,))
            _bb = value
        End Set
    End Property

    Property cc() As Boolean(,,)
        Get
            Return _cc
        End Get
        Set(ByVal value As Boolean(,,))
            _cc = value
        End Set
    End Property

    Property dd() As Boolean(,,,)
        Get
            Return _dd
        End Get
        Set(ByVal value As Boolean(,,,))
            _dd = value
        End Set
    End Property

    Function Main() As Int32
        Dim result As Int32

        a = New Boolean() {}
        b = New Boolean(,) {}
        c = New Boolean(,,) {}
        d = New Boolean(,,,) {}

        aa = New Boolean() {}
        bb = New Boolean(,) {}
        cc = New Boolean(,,) {}
        dd = New Boolean(,,,) {}

        result += ArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += ArrayVerifier.Report()
        If b.Length <> 0 Then result += ArrayVerifier.Report()
        If c.Length <> 0 Then result += ArrayVerifier.Report()
        If d.Length <> 0 Then result += ArrayVerifier.Report()

        If aa.Length <> 0 Then result += ArrayVerifier.Report()
        If bb.Length <> 0 Then result += ArrayVerifier.Report()
        If cc.Length <> 0 Then result += ArrayVerifier.Report()
        If dd.Length <> 0 Then result += ArrayVerifier.Report()

        a = New Boolean() {}
        b = New Boolean(,) {{}}
        c = New Boolean(,,) {{{}}}
        d = New Boolean(,,,) {{{{}}}}

        aa = New Boolean() {}
        bb = New Boolean(,) {{}}
        cc = New Boolean(,,) {{{}}}
        dd = New Boolean(,,,) {{{{}}}}

        result += ArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += ArrayVerifier.Report()
        If b.Length <> 0 Then result += ArrayVerifier.Report()
        If c.Length <> 0 Then result += ArrayVerifier.Report()
        If d.Length <> 0 Then result += ArrayVerifier.Report()

        If aa.Length <> 0 Then result += ArrayVerifier.Report()
        If bb.Length <> 0 Then result += ArrayVerifier.Report()
        If cc.Length <> 0 Then result += ArrayVerifier.Report()
        If dd.Length <> 0 Then result += ArrayVerifier.Report()


        a = New Boolean() {CBool(1), CBool(2)}
        b = New Boolean(,) {{CBool(10), CBool(11)}, {CBool(12), CBool(13)}}
        c = New Boolean(,,) {{{CBool(20), CBool(21)}, {CBool(22), CBool(23)}}, {{CBool(24), CBool(25)}, {CBool(26), CBool(27)}}}
        d = New Boolean(,,,) {{{{CBool(30), CBool(31)}, {CBool(32), CBool(33)}}, {{CBool(34), CBool(35)}, {CBool(36), CBool(37)}}}, {{{CBool(40), CBool(41)}, {CBool(42), CBool(43)}}, {{CBool(44), CBool(45)}, {CBool(46), CBool(47)}}}}

        aa = New Boolean() {CBool(1), CBool(2)}
        bb = New Boolean(,) {{CBool(10), CBool(11)}, {CBool(12), CBool(13)}}
        cc = New Boolean(,,) {{{CBool(20), CBool(21)}, {CBool(22), CBool(23)}}, {{CBool(24), CBool(25)}, {CBool(26), CBool(27)}}}
        dd = New Boolean(,,,) {{{{CBool(30), CBool(31)}, {CBool(32), CBool(33)}}, {{CBool(34), CBool(35)}, {CBool(36), CBool(37)}}}, {{{CBool(40), CBool(41)}, {CBool(42), CBool(43)}}, {{CBool(44), CBool(45)}, {CBool(46), CBool(47)}}}}

        result += ArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 2 Then result += ArrayVerifier.Report()
        If b.Length <> 4 Then result += ArrayVerifier.Report()
        If c.Length <> 8 Then result += ArrayVerifier.Report()
        If d.Length <> 16 Then result += ArrayVerifier.Report()

        If aa.Length <> 2 Then result += ArrayVerifier.Report()
        If bb.Length <> 4 Then result += ArrayVerifier.Report()
        If cc.Length <> 8 Then result += ArrayVerifier.Report()
        If dd.Length <> 16 Then result += ArrayVerifier.Report()

        a = New Boolean() {CBool(51), CBool(52)}
        b = New Boolean(,) {{CBool(50), CBool(51)}, {CBool(52), CBool(53)}}
        c = New Boolean(,,) {{{CBool(60), CBool(61)}, {CBool(62), CBool(63)}}, {{CBool(64), CBool(65)}, {CBool(66), CBool(67)}}}
        d = New Boolean(,,,) {{{{CBool(70), CBool(71)}, {CBool(72), CBool(73)}}, {{CBool(74), CBool(75)}, {CBool(76), CBool(77)}}}, {{{CBool(80), CBool(81)}, {CBool(82), CBool(83)}}, {{CBool(84), CBool(85)}, {CBool(86), CBool(87)}}}}

        aa(0) = CBool(51)
        aa(1) = CBool(52)
        bb(0, 0) = CBool(50)
        bb(0, 1) = CBool(51)
        bb(1, 0) = CBool(52)
        bb(1, 1) = CBool(53)
        cc(0, 0, 0) = CBool(60)
        cc(0, 0, 1) = CBool(61)
        cc(0, 1, 0) = CBool(62)
        cc(0, 1, 1) = CBool(63)
        cc(1, 0, 0) = CBool(64)
        cc(1, 0, 1) = CBool(65)
        cc(1, 1, 0) = CBool(66)
        cc(1, 1, 1) = CBool(67)

        dd(0, 0, 0, 0) = CBool(70)
        dd(0, 0, 0, 1) = CBool(71)
        dd(0, 0, 1, 0) = CBool(72)
        dd(0, 0, 1, 1) = CBool(73)
        dd(0, 1, 0, 0) = CBool(74)
        dd(0, 1, 0, 1) = CBool(75)
        dd(0, 1, 1, 0) = CBool(76)
        dd(0, 1, 1, 1) = CBool(77)

        dd(1, 0, 0, 0) = CBool(80)
        dd(1, 0, 0, 1) = CBool(81)
        dd(1, 0, 1, 0) = CBool(82)
        dd(1, 0, 1, 1) = CBool(83)
        dd(1, 1, 0, 0) = CBool(84)
        dd(1, 1, 0, 1) = CBool(85)
        dd(1, 1, 1, 0) = CBool(86)
        dd(1, 1, 1, 1) = CBool(87)

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
