Public Module ArrayFunctionVariableShort1
    Dim _a As Short()
    Dim _b As Short(,)
    Dim _c As Short(,,)
    Dim _d As Short(,,,)
    Dim _aa() As Short
    Dim _bb(,) As Short
    Dim _cc(,,) As Short
    Dim _dd(,,,) As Short

    Function a() As Short()
        Return _a
    End Function

    Function b() As Short(,)
        Return _b
    End Function

    Function c() As Short(,,)
        Return _c
    End Function

    Function d() As Short(,,,)
        Return _d
    End Function

    Function aa() As Short()
        Return _aa
    End Function

    Function bb() As Short(,)
        Return _bb
    End Function

    Function cc() As Short(,,)
        Return _cc
    End Function

    Function dd() As Short(,,,)
        Return _dd
    End Function

    Function Main() As Int32
        Dim result As Int32

        _a = New Short() {}
        _b = New Short(,) {}
        _c = New Short(,,) {}
        _d = New Short(,,,) {}

        _aa = New Short() {}
        _bb = New Short(,) {}
        _cc = New Short(,,) {}
        _dd = New Short(,,,) {}

        result += ArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += ArrayVerifier.Report()
        If b.Length <> 0 Then result += ArrayVerifier.Report()
        If c.Length <> 0 Then result += ArrayVerifier.Report()
        If d.Length <> 0 Then result += ArrayVerifier.Report()

        If aa.Length <> 0 Then result += ArrayVerifier.Report()
        If bb.Length <> 0 Then result += ArrayVerifier.Report()
        If cc.Length <> 0 Then result += ArrayVerifier.Report()
        If dd.Length <> 0 Then result += ArrayVerifier.Report()

        _a = New Short() {}
        _b = New Short(,) {{}}
        _c = New Short(,,) {{{}}}
        _d = New Short(,,,) {{{{}}}}

        _aa = New Short() {}
        _bb = New Short(,) {{}}
        _cc = New Short(,,) {{{}}}
        _dd = New Short(,,,) {{{{}}}}

        result += ArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += ArrayVerifier.Report()
        If b.Length <> 0 Then result += ArrayVerifier.Report()
        If c.Length <> 0 Then result += ArrayVerifier.Report()
        If d.Length <> 0 Then result += ArrayVerifier.Report()

        If aa.Length <> 0 Then result += ArrayVerifier.Report()
        If bb.Length <> 0 Then result += ArrayVerifier.Report()
        If cc.Length <> 0 Then result += ArrayVerifier.Report()
        If dd.Length <> 0 Then result += ArrayVerifier.Report()


        _a = New Short() {1S, 2S}
        _b = New Short(,) {{10S, 11S}, {12S, 13S}}
        _c = New Short(,,) {{{20S, 21S}, {22S, 23S}}, {{24S, 25S}, {26S, 27S}}}
        _d = New Short(,,,) {{{{30S, 31S}, {32S, 33S}}, {{34S, 35S}, {36S, 37S}}}, {{{40S, 41S}, {42S, 43S}}, {{44S, 45S}, {46S, 47S}}}}

        _aa = New Short() {1S, 2S}
        _bb = New Short(,) {{10S, 11S}, {12S, 13S}}
        _cc = New Short(,,) {{{20S, 21S}, {22S, 23S}}, {{24S, 25S}, {26S, 27S}}}
        _dd = New Short(,,,) {{{{30S, 31S}, {32S, 33S}}, {{34S, 35S}, {36S, 37S}}}, {{{40S, 41S}, {42S, 43S}}, {{44S, 45S}, {46S, 47S}}}}

        result += ArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 2 Then result += ArrayVerifier.Report()
        If b.Length <> 4 Then result += ArrayVerifier.Report()
        If c.Length <> 8 Then result += ArrayVerifier.Report()
        If d.Length <> 16 Then result += ArrayVerifier.Report()

        If aa.Length <> 2 Then result += ArrayVerifier.Report()
        If bb.Length <> 4 Then result += ArrayVerifier.Report()
        If cc.Length <> 8 Then result += ArrayVerifier.Report()
        If dd.Length <> 16 Then result += ArrayVerifier.Report()

        _a = New Short() {51S, 52S}
        _b = New Short(,) {{50S, 51S}, {52S, 53S}}
        _c = New Short(,,) {{{60S, 61S}, {62S, 63S}}, {{64S, 65S}, {66S, 67S}}}
        _d = New Short(,,,) {{{{70S, 71S}, {72S, 73S}}, {{74S, 75S}, {76S, 77S}}}, {{{80S, 81S}, {82S, 83S}}, {{84S, 85S}, {86S, 87S}}}}

        aa(0) = 51S
        aa(1) = 52S
        bb(0, 0) = 50S
        bb(0, 1) = 51S
        bb(1, 0) = 52S
        bb(1, 1) = 53S
        cc(0, 0, 0) = 60S
        cc(0, 0, 1) = 61S
        cc(0, 1, 0) = 62S
        cc(0, 1, 1) = 63S
        cc(1, 0, 0) = 64S
        cc(1, 0, 1) = 65S
        cc(1, 1, 0) = 66S
        cc(1, 1, 1) = 67S

        dd(0, 0, 0, 0) = 70S
        dd(0, 0, 0, 1) = 71S
        dd(0, 0, 1, 0) = 72S
        dd(0, 0, 1, 1) = 73S
        dd(0, 1, 0, 0) = 74S
        dd(0, 1, 0, 1) = 75S
        dd(0, 1, 1, 0) = 76S
        dd(0, 1, 1, 1) = 77S

        dd(1, 0, 0, 0) = 80S
        dd(1, 0, 0, 1) = 81S
        dd(1, 0, 1, 0) = 82S
        dd(1, 0, 1, 1) = 83S
        dd(1, 1, 0, 0) = 84S
        dd(1, 1, 0, 1) = 85S
        dd(1, 1, 1, 0) = 86S
        dd(1, 1, 1, 1) = 87S

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
