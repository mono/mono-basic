Public Module ArrayPropertyVariableULong1
    Dim _a As ULong()
    Dim _b As ULong(,)
    Dim _c As ULong(,,)
    Dim _d As ULong(,,,)
    Dim _aa() As ULong
    Dim _bb(,) As ULong
    Dim _cc(,,) As ULong
    Dim _dd(,,,) As ULong

    Property a() As ULong()
        Get
            Return _a
        End Get
        Set(ByVal value As ULong())
            _a = value
        End Set
    End Property

    Property b() As ULong(,)
        Get
            Return _b
        End Get
        Set(ByVal value As ULong(,))
            _b = value
        End Set
    End Property

    Property c() As ULong(,,)
        Get
            Return _c
        End Get
        Set(ByVal value As ULong(,,))
            _c = value
        End Set
    End Property

    Property d() As ULong(,,,)
        Get
            Return _d
        End Get
        Set(ByVal value As ULong(,,,))
            _d = value
        End Set
    End Property
    Property aa() As ULong()
        Get
            Return _aa
        End Get
        Set(ByVal value As ULong())
            _aa = value
        End Set
    End Property

    Property bb() As ULong(,)
        Get
            Return _bb
        End Get
        Set(ByVal value As ULong(,))
            _bb = value
        End Set
    End Property

    Property cc() As ULong(,,)
        Get
            Return _cc
        End Get
        Set(ByVal value As ULong(,,))
            _cc = value
        End Set
    End Property

    Property dd() As ULong(,,,)
        Get
            Return _dd
        End Get
        Set(ByVal value As ULong(,,,))
            _dd = value
        End Set
    End Property

    Function Main() As Int32
        Dim result As Int32

        a = New ULong() {}
        b = New ULong(,) {}
        c = New ULong(,,) {}
        d = New ULong(,,,) {}

        aa = New ULong() {}
        bb = New ULong(,) {}
        cc = New ULong(,,) {}
        dd = New ULong(,,,) {}

        result += ArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += ArrayVerifier.Report()
        If b.Length <> 0 Then result += ArrayVerifier.Report()
        If c.Length <> 0 Then result += ArrayVerifier.Report()
        If d.Length <> 0 Then result += ArrayVerifier.Report()

        If aa.Length <> 0 Then result += ArrayVerifier.Report()
        If bb.Length <> 0 Then result += ArrayVerifier.Report()
        If cc.Length <> 0 Then result += ArrayVerifier.Report()
        If dd.Length <> 0 Then result += ArrayVerifier.Report()

        a = New ULong() {}
        b = New ULong(,) {{}}
        c = New ULong(,,) {{{}}}
        d = New ULong(,,,) {{{{}}}}

        aa = New ULong() {}
        bb = New ULong(,) {{}}
        cc = New ULong(,,) {{{}}}
        dd = New ULong(,,,) {{{{}}}}

        result += ArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += ArrayVerifier.Report()
        If b.Length <> 0 Then result += ArrayVerifier.Report()
        If c.Length <> 0 Then result += ArrayVerifier.Report()
        If d.Length <> 0 Then result += ArrayVerifier.Report()

        If aa.Length <> 0 Then result += ArrayVerifier.Report()
        If bb.Length <> 0 Then result += ArrayVerifier.Report()
        If cc.Length <> 0 Then result += ArrayVerifier.Report()
        If dd.Length <> 0 Then result += ArrayVerifier.Report()


        a = New ULong() {1UL, 2UL}
        b = New ULong(,) {{10UL, 11UL}, {12UL, 13UL}}
        c = New ULong(,,) {{{20UL, 21UL}, {22UL, 23UL}}, {{24UL, 25UL}, {26UL, 27UL}}}
        d = New ULong(,,,) {{{{30UL, 31UL}, {32UL, 33UL}}, {{34UL, 35UL}, {36UL, 37UL}}}, {{{40UL, 41UL}, {42UL, 43UL}}, {{44UL, 45UL}, {46UL, 47UL}}}}

        aa = New ULong() {1UL, 2UL}
        bb = New ULong(,) {{10UL, 11UL}, {12UL, 13UL}}
        cc = New ULong(,,) {{{20UL, 21UL}, {22UL, 23UL}}, {{24UL, 25UL}, {26UL, 27UL}}}
        dd = New ULong(,,,) {{{{30UL, 31UL}, {32UL, 33UL}}, {{34UL, 35UL}, {36UL, 37UL}}}, {{{40UL, 41UL}, {42UL, 43UL}}, {{44UL, 45UL}, {46UL, 47UL}}}}

        result += ArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 2 Then result += ArrayVerifier.Report()
        If b.Length <> 4 Then result += ArrayVerifier.Report()
        If c.Length <> 8 Then result += ArrayVerifier.Report()
        If d.Length <> 16 Then result += ArrayVerifier.Report()

        If aa.Length <> 2 Then result += ArrayVerifier.Report()
        If bb.Length <> 4 Then result += ArrayVerifier.Report()
        If cc.Length <> 8 Then result += ArrayVerifier.Report()
        If dd.Length <> 16 Then result += ArrayVerifier.Report()

        a = New ULong() {51UL, 52UL}
        b = New ULong(,) {{50UL, 51UL}, {52UL, 53UL}}
        c = New ULong(,,) {{{60UL, 61UL}, {62UL, 63UL}}, {{64UL, 65UL}, {66UL, 67UL}}}
        d = New ULong(,,,) {{{{70UL, 71UL}, {72UL, 73UL}}, {{74UL, 75UL}, {76UL, 77UL}}}, {{{80UL, 81UL}, {82UL, 83UL}}, {{84UL, 85UL}, {86UL, 87UL}}}}

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
