Public Module ArrayLocalsVariableInteger1
    Function Main() As Int32
        Dim result As Int32

        Dim a As Integer()
        Dim b As Integer(,)
        Dim c As Integer(,,)
        Dim d As Integer(,,,)
        Dim aa() As Integer
        Dim bb(,) As Integer
        Dim cc(,,) As Integer
        Dim dd(,,,) As Integer

        a = New Integer() {}
        b = New Integer(,) {}
        c = New Integer(,,) {}
        d = New Integer(,,,) {}

        aa = New Integer() {}
        bb = New Integer(,) {}
        cc = New Integer(,,) {}
        dd = New Integer(,,,) {}

        result += ArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += ArrayVerifier.Report()
        If b.Length <> 0 Then result += ArrayVerifier.Report()
        If c.Length <> 0 Then result += ArrayVerifier.Report()
        If d.Length <> 0 Then result += ArrayVerifier.Report()

        If aa.Length <> 0 Then result += ArrayVerifier.Report()
        If bb.Length <> 0 Then result += ArrayVerifier.Report()
        If cc.Length <> 0 Then result += ArrayVerifier.Report()
        If dd.Length <> 0 Then result += ArrayVerifier.Report()

        a = New Integer() {}
        b = New Integer(,) {{}}
        c = New Integer(,,) {{{}}}
        d = New Integer(,,,) {{{{}}}}

        aa = New Integer() {}
        bb = New Integer(,) {{}}
        cc = New Integer(,,) {{{}}}
        dd = New Integer(,,,) {{{{}}}}

        result += ArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += ArrayVerifier.Report()
        If b.Length <> 0 Then result += ArrayVerifier.Report()
        If c.Length <> 0 Then result += ArrayVerifier.Report()
        If d.Length <> 0 Then result += ArrayVerifier.Report()

        If aa.Length <> 0 Then result += ArrayVerifier.Report()
        If bb.Length <> 0 Then result += ArrayVerifier.Report()
        If cc.Length <> 0 Then result += ArrayVerifier.Report()
        If dd.Length <> 0 Then result += ArrayVerifier.Report()

        a = New Integer() {1I, 2I}
        b = New Integer(,) {{10I, 11I}, {12I, 13I}}
        c = New Integer(,,) {{{20I, 21I}, {22I, 23I}}, {{24I, 25I}, {26I, 27I}}}
        d = New Integer(,,,) {{{{30I, 31I}, {32I, 33I}}, {{34I, 35I}, {36I, 37I}}}, {{{40I, 41I}, {42I, 43I}}, {{44I, 45I}, {46I, 47I}}}}

        aa = New Integer() {1I, 2I}
        bb = New Integer(,) {{10I, 11I}, {12I, 13I}}
        cc = New Integer(,,) {{{20I, 21I}, {22I, 23I}}, {{24I, 25I}, {26I, 27I}}}
        dd = New Integer(,,,) {{{{30I, 31I}, {32I, 33I}}, {{34I, 35I}, {36I, 37I}}}, {{{40I, 41I}, {42I, 43I}}, {{44I, 45I}, {46I, 47I}}}}

        result += ArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 2 Then result += ArrayVerifier.Report()
        If b.Length <> 4 Then result += ArrayVerifier.Report()
        If c.Length <> 8 Then result += ArrayVerifier.Report()
        If d.Length <> 16 Then result += ArrayVerifier.Report()

        If aa.Length <> 2 Then result += ArrayVerifier.Report()
        If bb.Length <> 4 Then result += ArrayVerifier.Report()
        If cc.Length <> 8 Then result += ArrayVerifier.Report()
        If dd.Length <> 16 Then result += ArrayVerifier.Report()

        a = New Integer() {51I, 52I}
        b = New Integer(,) {{50I, 51I}, {52I, 53I}}
        c = New Integer(,,) {{{60I, 61I}, {62I, 63I}}, {{64I, 65I}, {66I, 67I}}}
        d = New Integer(,,,) {{{{70I, 71I}, {72I, 73I}}, {{74I, 75I}, {76I, 77I}}}, {{{80I, 81I}, {82I, 83I}}, {{84I, 85I}, {86I, 87I}}}}

        aa(0) = 51I
        aa(1) = 52I
        bb(0, 0) = 50I
        bb(0, 1) = 51I
        bb(1, 0) = 52I
        bb(1, 1) = 53I
        cc(0, 0, 0) = 60I
        cc(0, 0, 1) = 61I
        cc(0, 1, 0) = 62I
        cc(0, 1, 1) = 63I
        cc(1, 0, 0) = 64I
        cc(1, 0, 1) = 65I
        cc(1, 1, 0) = 66I
        cc(1, 1, 1) = 67I

        dd(0, 0, 0, 0) = 70I
        dd(0, 0, 0, 1) = 71I
        dd(0, 0, 1, 0) = 72I
        dd(0, 0, 1, 1) = 73I
        dd(0, 1, 0, 0) = 74I
        dd(0, 1, 0, 1) = 75I
        dd(0, 1, 1, 0) = 76I
        dd(0, 1, 1, 1) = 77I

        dd(1, 0, 0, 0) = 80I
        dd(1, 0, 0, 1) = 81I
        dd(1, 0, 1, 0) = 82I
        dd(1, 0, 1, 1) = 83I
        dd(1, 1, 0, 0) = 84I
        dd(1, 1, 0, 1) = 85I
        dd(1, 1, 1, 0) = 86I
        dd(1, 1, 1, 1) = 87I

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
