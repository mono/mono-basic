Public Module ArrayFunctionVariableULong1
    Dim _a As ULong()
    Dim _b As ULong(,)
    Dim _c As ULong(,,)
    Dim _d As ULong(,,,)
    Dim _aa() As ULong
    Dim _bb(,) As ULong
    Dim _cc(,,) As ULong
    Dim _dd(,,,) As ULong

    Function a() As ULong()
        Return _a
    End Function

    Function b() As ULong(,)
        Return _b
    End Function

    Function c() As ULong(,,)
        Return _c
    End Function

    Function d() As ULong(,,,)
        Return _d
    End Function

    Function aa() As ULong()
        Return _aa
    End Function

    Function bb() As ULong(,)
        Return _bb
    End Function

    Function cc() As ULong(,,)
        Return _cc
    End Function

    Function dd() As ULong(,,,)
        Return _dd
    End Function

    Function Main() As Int32
        Dim result As Int32

        _a = New ULong() {}
        _b = New ULong(,) {}
        _c = New ULong(,,) {}
        _d = New ULong(,,,) {}

        _aa = New ULong() {}
        _bb = New ULong(,) {}
        _cc = New ULong(,,) {}
        _dd = New ULong(,,,) {}

        result += ArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += ArrayVerifier.Report()
        If b.Length <> 0 Then result += ArrayVerifier.Report()
        If c.Length <> 0 Then result += ArrayVerifier.Report()
        If d.Length <> 0 Then result += ArrayVerifier.Report()

        If aa.Length <> 0 Then result += ArrayVerifier.Report()
        If bb.Length <> 0 Then result += ArrayVerifier.Report()
        If cc.Length <> 0 Then result += ArrayVerifier.Report()
        If dd.Length <> 0 Then result += ArrayVerifier.Report()

        _a = New ULong() {}
        _b = New ULong(,) {{}}
        _c = New ULong(,,) {{{}}}
        _d = New ULong(,,,) {{{{}}}}

        _aa = New ULong() {}
        _bb = New ULong(,) {{}}
        _cc = New ULong(,,) {{{}}}
        _dd = New ULong(,,,) {{{{}}}}

        result += ArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += ArrayVerifier.Report()
        If b.Length <> 0 Then result += ArrayVerifier.Report()
        If c.Length <> 0 Then result += ArrayVerifier.Report()
        If d.Length <> 0 Then result += ArrayVerifier.Report()

        If aa.Length <> 0 Then result += ArrayVerifier.Report()
        If bb.Length <> 0 Then result += ArrayVerifier.Report()
        If cc.Length <> 0 Then result += ArrayVerifier.Report()
        If dd.Length <> 0 Then result += ArrayVerifier.Report()


        _a = New ULong() {1UL, 2UL}
        _b = New ULong(,) {{10UL, 11UL}, {12UL, 13UL}}
        _c = New ULong(,,) {{{20UL, 21UL}, {22UL, 23UL}}, {{24UL, 25UL}, {26UL, 27UL}}}
        _d = New ULong(,,,) {{{{30UL, 31UL}, {32UL, 33UL}}, {{34UL, 35UL}, {36UL, 37UL}}}, {{{40UL, 41UL}, {42UL, 43UL}}, {{44UL, 45UL}, {46UL, 47UL}}}}

        _aa = New ULong() {1UL, 2UL}
        _bb = New ULong(,) {{10UL, 11UL}, {12UL, 13UL}}
        _cc = New ULong(,,) {{{20UL, 21UL}, {22UL, 23UL}}, {{24UL, 25UL}, {26UL, 27UL}}}
        _dd = New ULong(,,,) {{{{30UL, 31UL}, {32UL, 33UL}}, {{34UL, 35UL}, {36UL, 37UL}}}, {{{40UL, 41UL}, {42UL, 43UL}}, {{44UL, 45UL}, {46UL, 47UL}}}}

        result += ArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 2 Then result += ArrayVerifier.Report()
        If b.Length <> 4 Then result += ArrayVerifier.Report()
        If c.Length <> 8 Then result += ArrayVerifier.Report()
        If d.Length <> 16 Then result += ArrayVerifier.Report()

        If aa.Length <> 2 Then result += ArrayVerifier.Report()
        If bb.Length <> 4 Then result += ArrayVerifier.Report()
        If cc.Length <> 8 Then result += ArrayVerifier.Report()
        If dd.Length <> 16 Then result += ArrayVerifier.Report()

        _a = New ULong() {51UL, 52UL}
        _b = New ULong(,) {{50UL, 51UL}, {52UL, 53UL}}
        _c = New ULong(,,) {{{60UL, 61UL}, {62UL, 63UL}}, {{64UL, 65UL}, {66UL, 67UL}}}
        _d = New ULong(,,,) {{{{70UL, 71UL}, {72UL, 73UL}}, {{74UL, 75UL}, {76UL, 77UL}}}, {{{80UL, 81UL}, {82UL, 83UL}}, {{84UL, 85UL}, {86UL, 87UL}}}}

        aa(0) = 51UL
        aa(1) = 52UL
        bb(0, 0) = 50UL
        bb(0, 1) = 51UL
        bb(1, 0) = 52UL
        bb(1, 1) = 53UL
        cc(0, 0, 0) = 60UL
        cc(0, 0, 1) = 61UL
        cc(0, 1, 0) = 62UL
        cc(0, 1, 1) = 63UL
        cc(1, 0, 0) = 64UL
        cc(1, 0, 1) = 65UL
        cc(1, 1, 0) = 66UL
        cc(1, 1, 1) = 67UL

        dd(0, 0, 0, 0) = 70UL
        dd(0, 0, 0, 1) = 71UL
        dd(0, 0, 1, 0) = 72UL
        dd(0, 0, 1, 1) = 73UL
        dd(0, 1, 0, 0) = 74UL
        dd(0, 1, 0, 1) = 75UL
        dd(0, 1, 1, 0) = 76UL
        dd(0, 1, 1, 1) = 77UL

        dd(1, 0, 0, 0) = 80UL
        dd(1, 0, 0, 1) = 81UL
        dd(1, 0, 1, 0) = 82UL
        dd(1, 0, 1, 1) = 83UL
        dd(1, 1, 0, 0) = 84UL
        dd(1, 1, 0, 1) = 85UL
        dd(1, 1, 1, 0) = 86UL
        dd(1, 1, 1, 1) = 87UL

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
