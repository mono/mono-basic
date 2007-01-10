Public Module ArrayLocalsVariableShort1
    Function Main() As Int32
        Dim result As Int32

        Dim a As Short()
        Dim b As Short(,)
        Dim c As Short(,,)
        Dim d As Short(,,,)
        Dim aa() As Short
        Dim bb(,) As Short
        Dim cc(,,) As Short
        Dim dd(,,,) As Short

        a = New Short() {}
        b = New Short(,) {}
        c = New Short(,,) {}
        d = New Short(,,,) {}

        aa = New Short() {}
        bb = New Short(,) {}
        cc = New Short(,,) {}
        dd = New Short(,,,) {}

        result += ArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += ArrayVerifier.Report()
        If b.Length <> 0 Then result += ArrayVerifier.Report()
        If c.Length <> 0 Then result += ArrayVerifier.Report()
        If d.Length <> 0 Then result += ArrayVerifier.Report()

        If aa.Length <> 0 Then result += ArrayVerifier.Report()
        If bb.Length <> 0 Then result += ArrayVerifier.Report()
        If cc.Length <> 0 Then result += ArrayVerifier.Report()
        If dd.Length <> 0 Then result += ArrayVerifier.Report()

        a = New Short() {}
        b = New Short(,) {{}}
        c = New Short(,,) {{{}}}
        d = New Short(,,,) {{{{}}}}

        aa = New Short() {}
        bb = New Short(,) {{}}
        cc = New Short(,,) {{{}}}
        dd = New Short(,,,) {{{{}}}}

        result += ArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += ArrayVerifier.Report()
        If b.Length <> 0 Then result += ArrayVerifier.Report()
        If c.Length <> 0 Then result += ArrayVerifier.Report()
        If d.Length <> 0 Then result += ArrayVerifier.Report()

        If aa.Length <> 0 Then result += ArrayVerifier.Report()
        If bb.Length <> 0 Then result += ArrayVerifier.Report()
        If cc.Length <> 0 Then result += ArrayVerifier.Report()
        If dd.Length <> 0 Then result += ArrayVerifier.Report()

        a = New Short() {1S, 2S}
        b = New Short(,) {{10S, 11S}, {12S, 13S}}
        c = New Short(,,) {{{20S, 21S}, {22S, 23S}}, {{24S, 25S}, {26S, 27S}}}
        d = New Short(,,,) {{{{30S, 31S}, {32S, 33S}}, {{34S, 35S}, {36S, 37S}}}, {{{40S, 41S}, {42S, 43S}}, {{44S, 45S}, {46S, 47S}}}}

        aa = New Short() {1S, 2S}
        bb = New Short(,) {{10S, 11S}, {12S, 13S}}
        cc = New Short(,,) {{{20S, 21S}, {22S, 23S}}, {{24S, 25S}, {26S, 27S}}}
        dd = New Short(,,,) {{{{30S, 31S}, {32S, 33S}}, {{34S, 35S}, {36S, 37S}}}, {{{40S, 41S}, {42S, 43S}}, {{44S, 45S}, {46S, 47S}}}}

        result += ArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 2 Then result += ArrayVerifier.Report()
        If b.Length <> 4 Then result += ArrayVerifier.Report()
        If c.Length <> 8 Then result += ArrayVerifier.Report()
        If d.Length <> 16 Then result += ArrayVerifier.Report()

        If aa.Length <> 2 Then result += ArrayVerifier.Report()
        If bb.Length <> 4 Then result += ArrayVerifier.Report()
        If cc.Length <> 8 Then result += ArrayVerifier.Report()
        If dd.Length <> 16 Then result += ArrayVerifier.Report()

        a = New Short() {51S, 52S}
        b = New Short(,) {{50S, 51S}, {52S, 53S}}
        c = New Short(,,) {{{60S, 61S}, {62S, 63S}}, {{64S, 65S}, {66S, 67S}}}
        d = New Short(,,,) {{{{70S, 71S}, {72S, 73S}}, {{74S, 75S}, {76S, 77S}}}, {{{80S, 81S}, {82S, 83S}}, {{84S, 85S}, {86S, 87S}}}}

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
