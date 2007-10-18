Public Module ArrayFunctionVariableSByte1
    Dim _a As SByte()
    Dim _b As SByte(,)
    Dim _c As SByte(,,)
    Dim _d As SByte(,,,)
    Dim _aa() As SByte
    Dim _bb(,) As SByte
    Dim _cc(,,) As SByte
    Dim _dd(,,,) As SByte

    Function a() As SByte()
        Return _a
    End Function

    Function b() As SByte(,)
        Return _b
    End Function

    Function c() As SByte(,,)
        Return _c
    End Function

    Function d() As SByte(,,,)
        Return _d
    End Function

    Function aa() As SByte()
        Return _aa
    End Function

    Function bb() As SByte(,)
        Return _bb
    End Function

    Function cc() As SByte(,,)
        Return _cc
    End Function

    Function dd() As SByte(,,,)
        Return _dd
    End Function

    Function Main() As Int32
        Dim result As Int32

        _a = New SByte() {}
        _b = New SByte(,) {}
        _c = New SByte(,,) {}
        _d = New SByte(,,,) {}

        _aa = New SByte() {}
        _bb = New SByte(,) {}
        _cc = New SByte(,,) {}
        _dd = New SByte(,,,) {}

        result += ArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += ArrayVerifier.Report()
        If b.Length <> 0 Then result += ArrayVerifier.Report()
        If c.Length <> 0 Then result += ArrayVerifier.Report()
        If d.Length <> 0 Then result += ArrayVerifier.Report()

        If aa.Length <> 0 Then result += ArrayVerifier.Report()
        If bb.Length <> 0 Then result += ArrayVerifier.Report()
        If cc.Length <> 0 Then result += ArrayVerifier.Report()
        If dd.Length <> 0 Then result += ArrayVerifier.Report()

        _a = New SByte() {}
        _b = New SByte(,) {{}}
        _c = New SByte(,,) {{{}}}
        _d = New SByte(,,,) {{{{}}}}

        _aa = New SByte() {}
        _bb = New SByte(,) {{}}
        _cc = New SByte(,,) {{{}}}
        _dd = New SByte(,,,) {{{{}}}}

        result += ArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += ArrayVerifier.Report()
        If b.Length <> 0 Then result += ArrayVerifier.Report()
        If c.Length <> 0 Then result += ArrayVerifier.Report()
        If d.Length <> 0 Then result += ArrayVerifier.Report()

        If aa.Length <> 0 Then result += ArrayVerifier.Report()
        If bb.Length <> 0 Then result += ArrayVerifier.Report()
        If cc.Length <> 0 Then result += ArrayVerifier.Report()
        If dd.Length <> 0 Then result += ArrayVerifier.Report()


        _a = New SByte() {CSByte(1), CSByte(2)}
        _b = New SByte(,) {{CSByte(10), CSByte(11)}, {CSByte(12), CSByte(13)}}
        _c = New SByte(,,) {{{CSByte(20), CSByte(21)}, {CSByte(22), CSByte(23)}}, {{CSByte(24), CSByte(25)}, {CSByte(26), CSByte(27)}}}
        _d = New SByte(,,,) {{{{CSByte(30), CSByte(31)}, {CSByte(32), CSByte(33)}}, {{CSByte(34), CSByte(35)}, {CSByte(36), CSByte(37)}}}, {{{CSByte(40), CSByte(41)}, {CSByte(42), CSByte(43)}}, {{CSByte(44), CSByte(45)}, {CSByte(46), CSByte(47)}}}}

        _aa = New SByte() {CSByte(1), CSByte(2)}
        _bb = New SByte(,) {{CSByte(10), CSByte(11)}, {CSByte(12), CSByte(13)}}
        _cc = New SByte(,,) {{{CSByte(20), CSByte(21)}, {CSByte(22), CSByte(23)}}, {{CSByte(24), CSByte(25)}, {CSByte(26), CSByte(27)}}}
        _dd = New SByte(,,,) {{{{CSByte(30), CSByte(31)}, {CSByte(32), CSByte(33)}}, {{CSByte(34), CSByte(35)}, {CSByte(36), CSByte(37)}}}, {{{CSByte(40), CSByte(41)}, {CSByte(42), CSByte(43)}}, {{CSByte(44), CSByte(45)}, {CSByte(46), CSByte(47)}}}}

        result += ArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 2 Then result += ArrayVerifier.Report()
        If b.Length <> 4 Then result += ArrayVerifier.Report()
        If c.Length <> 8 Then result += ArrayVerifier.Report()
        If d.Length <> 16 Then result += ArrayVerifier.Report()

        If aa.Length <> 2 Then result += ArrayVerifier.Report()
        If bb.Length <> 4 Then result += ArrayVerifier.Report()
        If cc.Length <> 8 Then result += ArrayVerifier.Report()
        If dd.Length <> 16 Then result += ArrayVerifier.Report()

        _a = New SByte() {CSByte(51), CSByte(52)}
        _b = New SByte(,) {{CSByte(50), CSByte(51)}, {CSByte(52), CSByte(53)}}
        _c = New SByte(,,) {{{CSByte(60), CSByte(61)}, {CSByte(62), CSByte(63)}}, {{CSByte(64), CSByte(65)}, {CSByte(66), CSByte(67)}}}
        _d = New SByte(,,,) {{{{CSByte(70), CSByte(71)}, {CSByte(72), CSByte(73)}}, {{CSByte(74), CSByte(75)}, {CSByte(76), CSByte(77)}}}, {{{CSByte(80), CSByte(81)}, {CSByte(82), CSByte(83)}}, {{CSByte(84), CSByte(85)}, {CSByte(86), CSByte(87)}}}}

        aa(0) = CSByte(51)
        aa(1) = CSByte(52)
        bb(0, 0) = CSByte(50)
        bb(0, 1) = CSByte(51)
        bb(1, 0) = CSByte(52)
        bb(1, 1) = CSByte(53)
        cc(0, 0, 0) = CSByte(60)
        cc(0, 0, 1) = CSByte(61)
        cc(0, 1, 0) = CSByte(62)
        cc(0, 1, 1) = CSByte(63)
        cc(1, 0, 0) = CSByte(64)
        cc(1, 0, 1) = CSByte(65)
        cc(1, 1, 0) = CSByte(66)
        cc(1, 1, 1) = CSByte(67)

        dd(0, 0, 0, 0) = CSByte(70)
        dd(0, 0, 0, 1) = CSByte(71)
        dd(0, 0, 1, 0) = CSByte(72)
        dd(0, 0, 1, 1) = CSByte(73)
        dd(0, 1, 0, 0) = CSByte(74)
        dd(0, 1, 0, 1) = CSByte(75)
        dd(0, 1, 1, 0) = CSByte(76)
        dd(0, 1, 1, 1) = CSByte(77)

        dd(1, 0, 0, 0) = CSByte(80)
        dd(1, 0, 0, 1) = CSByte(81)
        dd(1, 0, 1, 0) = CSByte(82)
        dd(1, 0, 1, 1) = CSByte(83)
        dd(1, 1, 0, 0) = CSByte(84)
        dd(1, 1, 0, 1) = CSByte(85)
        dd(1, 1, 1, 0) = CSByte(86)
        dd(1, 1, 1, 1) = CSByte(87)

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
