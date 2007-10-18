Module ArrayFieldsVariableDecimal1
    Dim a As Decimal()
    Dim b As Decimal(,)
    Dim c As Decimal(,,)
    Dim d As Decimal(,,,)
    Dim aa() As Decimal
    Dim bb(,) As Decimal
    Dim cc(,,) As Decimal
    Dim dd(,,,) As Decimal

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
