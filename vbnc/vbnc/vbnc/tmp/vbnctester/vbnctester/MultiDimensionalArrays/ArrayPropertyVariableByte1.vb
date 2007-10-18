Public Module ArrayPropertyVariableByte1
    Dim _a As Byte()
    Dim _b As Byte(,)
    Dim _c As Byte(,,)
    Dim _d As Byte(,,,)
    Dim _aa() As Byte
    Dim _bb(,) As Byte
    Dim _cc(,,) As Byte
    Dim _dd(,,,) As Byte

    Property a() As Byte()
        Get
            Return _a
        End Get
        Set(ByVal value As Byte())
            _a = value
        End Set
    End Property

    Property b() As Byte(,)
        Get
            Return _b
        End Get
        Set(ByVal value As Byte(,))
            _b = value
        End Set
    End Property

    Property c() As Byte(,,)
        Get
            Return _c
        End Get
        Set(ByVal value As Byte(,,))
            _c = value
        End Set
    End Property

    Property d() As Byte(,,,)
        Get
            Return _d
        End Get
        Set(ByVal value As Byte(,,,))
            _d = value
        End Set
    End Property
    Property aa() As Byte()
        Get
            Return _aa
        End Get
        Set(ByVal value As Byte())
            _aa = value
        End Set
    End Property

    Property bb() As Byte(,)
        Get
            Return _bb
        End Get
        Set(ByVal value As Byte(,))
            _bb = value
        End Set
    End Property

    Property cc() As Byte(,,)
        Get
            Return _cc
        End Get
        Set(ByVal value As Byte(,,))
            _cc = value
        End Set
    End Property

    Property dd() As Byte(,,,)
        Get
            Return _dd
        End Get
        Set(ByVal value As Byte(,,,))
            _dd = value
        End Set
    End Property

    Function Main() As Int32
        Dim result As Int32

        a = New Byte() {}
        b = New Byte(,) {}
        c = New Byte(,,) {}
        d = New Byte(,,,) {}

        aa = New Byte() {}
        bb = New Byte(,) {}
        cc = New Byte(,,) {}
        dd = New Byte(,,,) {}

        result += ArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += ArrayVerifier.Report()
        If b.Length <> 0 Then result += ArrayVerifier.Report()
        If c.Length <> 0 Then result += ArrayVerifier.Report()
        If d.Length <> 0 Then result += ArrayVerifier.Report()

        If aa.Length <> 0 Then result += ArrayVerifier.Report()
        If bb.Length <> 0 Then result += ArrayVerifier.Report()
        If cc.Length <> 0 Then result += ArrayVerifier.Report()
        If dd.Length <> 0 Then result += ArrayVerifier.Report()

        a = New Byte() {}
        b = New Byte(,) {{}}
        c = New Byte(,,) {{{}}}
        d = New Byte(,,,) {{{{}}}}

        aa = New Byte() {}
        bb = New Byte(,) {{}}
        cc = New Byte(,,) {{{}}}
        dd = New Byte(,,,) {{{{}}}}

        result += ArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += ArrayVerifier.Report()
        If b.Length <> 0 Then result += ArrayVerifier.Report()
        If c.Length <> 0 Then result += ArrayVerifier.Report()
        If d.Length <> 0 Then result += ArrayVerifier.Report()

        If aa.Length <> 0 Then result += ArrayVerifier.Report()
        If bb.Length <> 0 Then result += ArrayVerifier.Report()
        If cc.Length <> 0 Then result += ArrayVerifier.Report()
        If dd.Length <> 0 Then result += ArrayVerifier.Report()


        a = New Byte() {CByte(1), CByte(2)}
        b = New Byte(,) {{CByte(10), CByte(11)}, {CByte(12), CByte(13)}}
        c = New Byte(,,) {{{CByte(20), CByte(21)}, {CByte(22), CByte(23)}}, {{CByte(24), CByte(25)}, {CByte(26), CByte(27)}}}
        d = New Byte(,,,) {{{{CByte(30), CByte(31)}, {CByte(32), CByte(33)}}, {{CByte(34), CByte(35)}, {CByte(36), CByte(37)}}}, {{{CByte(40), CByte(41)}, {CByte(42), CByte(43)}}, {{CByte(44), CByte(45)}, {CByte(46), CByte(47)}}}}

        aa = New Byte() {CByte(1), CByte(2)}
        bb = New Byte(,) {{CByte(10), CByte(11)}, {CByte(12), CByte(13)}}
        cc = New Byte(,,) {{{CByte(20), CByte(21)}, {CByte(22), CByte(23)}}, {{CByte(24), CByte(25)}, {CByte(26), CByte(27)}}}
        dd = New Byte(,,,) {{{{CByte(30), CByte(31)}, {CByte(32), CByte(33)}}, {{CByte(34), CByte(35)}, {CByte(36), CByte(37)}}}, {{{CByte(40), CByte(41)}, {CByte(42), CByte(43)}}, {{CByte(44), CByte(45)}, {CByte(46), CByte(47)}}}}

        result += ArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 2 Then result += ArrayVerifier.Report()
        If b.Length <> 4 Then result += ArrayVerifier.Report()
        If c.Length <> 8 Then result += ArrayVerifier.Report()
        If d.Length <> 16 Then result += ArrayVerifier.Report()

        If aa.Length <> 2 Then result += ArrayVerifier.Report()
        If bb.Length <> 4 Then result += ArrayVerifier.Report()
        If cc.Length <> 8 Then result += ArrayVerifier.Report()
        If dd.Length <> 16 Then result += ArrayVerifier.Report()

        a = New Byte() {CByte(51), CByte(52)}
        b = New Byte(,) {{CByte(50), CByte(51)}, {CByte(52), CByte(53)}}
        c = New Byte(,,) {{{CByte(60), CByte(61)}, {CByte(62), CByte(63)}}, {{CByte(64), CByte(65)}, {CByte(66), CByte(67)}}}
        d = New Byte(,,,) {{{{CByte(70), CByte(71)}, {CByte(72), CByte(73)}}, {{CByte(74), CByte(75)}, {CByte(76), CByte(77)}}}, {{{CByte(80), CByte(81)}, {CByte(82), CByte(83)}}, {{CByte(84), CByte(85)}, {CByte(86), CByte(87)}}}}

        aa(0) = CByte(51)
        aa(1) = CByte(52)
        bb(0, 0) = CByte(50)
        bb(0, 1) = CByte(51)
        bb(1, 0) = CByte(52)
        bb(1, 1) = CByte(53)
        cc(0, 0, 0) = CByte(60)
        cc(0, 0, 1) = CByte(61)
        cc(0, 1, 0) = CByte(62)
        cc(0, 1, 1) = CByte(63)
        cc(1, 0, 0) = CByte(64)
        cc(1, 0, 1) = CByte(65)
        cc(1, 1, 0) = CByte(66)
        cc(1, 1, 1) = CByte(67)

        dd(0, 0, 0, 0) = CByte(70)
        dd(0, 0, 0, 1) = CByte(71)
        dd(0, 0, 1, 0) = CByte(72)
        dd(0, 0, 1, 1) = CByte(73)
        dd(0, 1, 0, 0) = CByte(74)
        dd(0, 1, 0, 1) = CByte(75)
        dd(0, 1, 1, 0) = CByte(76)
        dd(0, 1, 1, 1) = CByte(77)

        dd(1, 0, 0, 0) = CByte(80)
        dd(1, 0, 0, 1) = CByte(81)
        dd(1, 0, 1, 0) = CByte(82)
        dd(1, 0, 1, 1) = CByte(83)
        dd(1, 1, 0, 0) = CByte(84)
        dd(1, 1, 0, 1) = CByte(85)
        dd(1, 1, 1, 0) = CByte(86)
        dd(1, 1, 1, 1) = CByte(87)

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
