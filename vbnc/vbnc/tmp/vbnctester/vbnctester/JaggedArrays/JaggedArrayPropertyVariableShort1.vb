Public Module JaggedArrayPropertyVariableShort1
    Dim _a As Short()
    Dim _b As Short()()
    Dim _c As Short()()()
    Dim _d As Short()()()()
    Dim _aa() As Short
    Dim _bb()() As Short
    Dim _cc()()() As Short
    Dim _dd()()()() As Short

    Property a() As Short()
        Get
            Return _a
        End Get
        Set(ByVal value As Short())
            _a = value
        End Set
    End Property

    Property b() As Short()()
        Get
            Return _b
        End Get
        Set(ByVal value As Short()())
            _b = value
        End Set
    End Property

    Property c() As Short()()()
        Get
            Return _c
        End Get
        Set(ByVal value As Short()()())
            _c = value
        End Set
    End Property

    Property d() As Short()()()()
        Get
            Return _d
        End Get
        Set(ByVal value As Short()()()())
            _d = value
        End Set
    End Property

    Property aa() As Short()
        Get
            Return _aa
        End Get
        Set(ByVal value As Short())
            _aa = value
        End Set
    End Property

    Property bb() As Short()()
        Get
            Return _bb
        End Get
        Set(ByVal value As Short()())
            _bb = value
        End Set
    End Property

    Property cc() As Short()()()
        Get
            Return _cc
        End Get
        Set(ByVal value As Short()()())
            _cc = value
        End Set
    End Property

    Property dd() As Short()()()()
        Get
            Return _dd
        End Get
        Set(ByVal value As Short()()()())
            _dd = value
        End Set
    End Property

    Function Main() As Int32
        Dim result As Int32
        _a = New Short() {}
        _b = New Short()() {}
        _c = New Short()()() {}
        _d = New Short()()()() {}

        _aa = New Short() {}
        _bb = New Short()() {}
        _cc = New Short()()() {}
        _dd = New Short()()()() {}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 0 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 0 Then result += JaggedArrayVerifier.Report()


        _a = New Short() {}
        _b = New Short()() {New Short() {}}
        _c = New Short()()() {New Short()() {New Short() {}}}
        _d = New Short()()()() {New Short()()() {New Short()() {New Short() {}}}}

        _aa = New Short() {}
        _bb = New Short()() {New Short() {}}
        _cc = New Short()()() {New Short()() {New Short() {}}}
        _dd = New Short()()()() {New Short()()() {New Short()() {New Short() {}}}}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 1 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 1 Then result += JaggedArrayVerifier.Report()

        _a = New Short() {1S, 2S}
        _b = New Short()() {New Short() {10S, 11S}, New Short() {12S, 13S}}
        _c = New Short()()() {New Short()() {New Short() {20S, 21S}, New Short() {22S, 23S}}, New Short()() {New Short() {24S, 25S}, New Short() {26S, 27S}}}
        _d = New Short()()()() {New Short()()() {New Short()() {New Short() {30S, 31S}, New Short() {32S, 33S}}, New Short()() {New Short() {34S, 35S}, New Short() {36S, 37S}}}, New Short()()() {New Short()() {New Short() {40S, 41S}, New Short() {42S, 43S}}, New Short()() {New Short() {44S, 45S}, New Short() {46S, 47S}}}}

        _aa = New Short() {1S, 2S}
        _bb = New Short()() {New Short() {10S, 11S}, New Short() {12S, 13S}}
        _cc = New Short()()() {New Short()() {New Short() {20S, 21S}, New Short() {22S, 23S}}, New Short()() {New Short() {24S, 25S}, New Short() {26S, 27S}}}
        _dd = New Short()()()() {New Short()()() {New Short()() {New Short() {30S, 31S}, New Short() {32S, 33S}}, New Short()() {New Short() {34S, 35S}, New Short() {36S, 37S}}}, New Short()()() {New Short()() {New Short() {40S, 41S}, New Short() {42S, 43S}}, New Short()() {New Short() {44S, 45S}, New Short() {46S, 47S}}}}

result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 2 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 2 Then result += JaggedArrayVerifier.Report()

        _a = New Short() {51S, 52S}
        _b = New Short()() {New Short() {50S, 51S}, New Short() {52S, 53S}}
        _c = New Short()()() {New Short()() {New Short() {60S, 61S}, New Short() {62S, 63S}}, New Short()() {New Short() {64S, 65S}, New Short() {66S, 67S}}}
        _d = New Short()()()() {New Short()()() {New Short()() {New Short() {70S, 71S}, New Short() {72S, 73S}}, New Short()() {New Short() {74S, 75S}, New Short() {76S, 77S}}}, New Short()()() {New Short()() {New Short() {80S, 81S}, New Short() {82S, 83S}}, New Short()() {New Short() {84S, 85S}, New Short() {86S, 87S}}}}

        aa(0) = 51S
        aa(1) = 52S
        bb(0)(0) = 50S
        bb(0)(1) = 51S
        bb(1)(0) = 52S
        bb(1)(1) = 53S
        cc(0)(0)(0) = 60S
        cc(0)(0)(1) = 61S
        cc(0)(1)(0) = 62S
        cc(0)(1)(1) = 63S
        cc(1)(0)(0) = 64S
        cc(1)(0)(1) = 65S
        cc(1)(1)(0) = 66S
        cc(1)(1)(1) = 67S

        dd(0)(0)(0)(0) = 70S
        dd(0)(0)(0)(1) = 71S
        dd(0)(0)(1)(0) = 72S
        dd(0)(0)(1)(1) = 73S
        dd(0)(1)(0)(0) = 74S
        dd(0)(1)(0)(1) = 75S
        dd(0)(1)(1)(0) = 76S
        dd(0)(1)(1)(1) = 77S

        dd(1)(0)(0)(0) = 80S
        dd(1)(0)(0)(1) = 81S
        dd(1)(0)(1)(0) = 82S
        dd(1)(0)(1)(1) = 83S
        dd(1)(1)(0)(0) = 84S
        dd(1)(1)(0)(1) = 85S
        dd(1)(1)(1)(0) = 86S
        dd(1)(1)(1)(1) = 87S

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 2 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 2 Then result += JaggedArrayVerifier.Report()

        Return result
    End Function
End Module
