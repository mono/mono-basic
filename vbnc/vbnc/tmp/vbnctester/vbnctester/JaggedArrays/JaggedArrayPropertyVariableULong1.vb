Public Module JaggedArrayPropertyVariableULong1
    Dim _a As ULong()
    Dim _b As ULong()()
    Dim _c As ULong()()()
    Dim _d As ULong()()()()
    Dim _aa() As ULong
    Dim _bb()() As ULong
    Dim _cc()()() As ULong
    Dim _dd()()()() As ULong

    Property a() As ULong()
        Get
            Return _a
        End Get
        Set(ByVal value As ULong())
            _a = value
        End Set
    End Property

    Property b() As ULong()()
        Get
            Return _b
        End Get
        Set(ByVal value As ULong()())
            _b = value
        End Set
    End Property

    Property c() As ULong()()()
        Get
            Return _c
        End Get
        Set(ByVal value As ULong()()())
            _c = value
        End Set
    End Property

    Property d() As ULong()()()()
        Get
            Return _d
        End Get
        Set(ByVal value As ULong()()()())
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

    Property bb() As ULong()()
        Get
            Return _bb
        End Get
        Set(ByVal value As ULong()())
            _bb = value
        End Set
    End Property

    Property cc() As ULong()()()
        Get
            Return _cc
        End Get
        Set(ByVal value As ULong()()())
            _cc = value
        End Set
    End Property

    Property dd() As ULong()()()()
        Get
            Return _dd
        End Get
        Set(ByVal value As ULong()()()())
            _dd = value
        End Set
    End Property

    Function Main() As Int32
        Dim result As Int32
        _a = New ULong() {}
        _b = New ULong()() {}
        _c = New ULong()()() {}
        _d = New ULong()()()() {}

        _aa = New ULong() {}
        _bb = New ULong()() {}
        _cc = New ULong()()() {}
        _dd = New ULong()()()() {}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 0 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 0 Then result += JaggedArrayVerifier.Report()


        _a = New ULong() {}
        _b = New ULong()() {New ULong() {}}
        _c = New ULong()()() {New ULong()() {New ULong() {}}}
        _d = New ULong()()()() {New ULong()()() {New ULong()() {New ULong() {}}}}

        _aa = New ULong() {}
        _bb = New ULong()() {New ULong() {}}
        _cc = New ULong()()() {New ULong()() {New ULong() {}}}
        _dd = New ULong()()()() {New ULong()()() {New ULong()() {New ULong() {}}}}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 1 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 1 Then result += JaggedArrayVerifier.Report()

        _a = New ULong() {1UL, 2UL}
        _b = New ULong()() {New ULong() {10UL, 11UL}, New ULong() {12UL, 13UL}}
        _c = New ULong()()() {New ULong()() {New ULong() {20UL, 21UL}, New ULong() {22UL, 23UL}}, New ULong()() {New ULong() {24UL, 25UL}, New ULong() {26UL, 27UL}}}
        _d = New ULong()()()() {New ULong()()() {New ULong()() {New ULong() {30UL, 31UL}, New ULong() {32UL, 33UL}}, New ULong()() {New ULong() {34UL, 35UL}, New ULong() {36UL, 37UL}}}, New ULong()()() {New ULong()() {New ULong() {40UL, 41UL}, New ULong() {42UL, 43UL}}, New ULong()() {New ULong() {44UL, 45UL}, New ULong() {46UL, 47UL}}}}

        _aa = New ULong() {1UL, 2UL}
        _bb = New ULong()() {New ULong() {10UL, 11UL}, New ULong() {12UL, 13UL}}
        _cc = New ULong()()() {New ULong()() {New ULong() {20UL, 21UL}, New ULong() {22UL, 23UL}}, New ULong()() {New ULong() {24UL, 25UL}, New ULong() {26UL, 27UL}}}
        _dd = New ULong()()()() {New ULong()()() {New ULong()() {New ULong() {30UL, 31UL}, New ULong() {32UL, 33UL}}, New ULong()() {New ULong() {34UL, 35UL}, New ULong() {36UL, 37UL}}}, New ULong()()() {New ULong()() {New ULong() {40UL, 41UL}, New ULong() {42UL, 43UL}}, New ULong()() {New ULong() {44UL, 45UL}, New ULong() {46UL, 47UL}}}}

result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 2 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 2 Then result += JaggedArrayVerifier.Report()

        _a = New ULong() {51UL, 52UL}
        _b = New ULong()() {New ULong() {50UL, 51UL}, New ULong() {52UL, 53UL}}
        _c = New ULong()()() {New ULong()() {New ULong() {60UL, 61UL}, New ULong() {62UL, 63UL}}, New ULong()() {New ULong() {64UL, 65UL}, New ULong() {66UL, 67UL}}}
        _d = New ULong()()()() {New ULong()()() {New ULong()() {New ULong() {70UL, 71UL}, New ULong() {72UL, 73UL}}, New ULong()() {New ULong() {74UL, 75UL}, New ULong() {76UL, 77UL}}}, New ULong()()() {New ULong()() {New ULong() {80UL, 81UL}, New ULong() {82UL, 83UL}}, New ULong()() {New ULong() {84UL, 85UL}, New ULong() {86UL, 87UL}}}}

        aa(0) = 51UL
        aa(1) = 52UL
        bb(0)(0) = 50UL
        bb(0)(1) = 51UL
        bb(1)(0) = 52UL
        bb(1)(1) = 53UL
        cc(0)(0)(0) = 60UL
        cc(0)(0)(1) = 61UL
        cc(0)(1)(0) = 62UL
        cc(0)(1)(1) = 63UL
        cc(1)(0)(0) = 64UL
        cc(1)(0)(1) = 65UL
        cc(1)(1)(0) = 66UL
        cc(1)(1)(1) = 67UL

        dd(0)(0)(0)(0) = 70UL
        dd(0)(0)(0)(1) = 71UL
        dd(0)(0)(1)(0) = 72UL
        dd(0)(0)(1)(1) = 73UL
        dd(0)(1)(0)(0) = 74UL
        dd(0)(1)(0)(1) = 75UL
        dd(0)(1)(1)(0) = 76UL
        dd(0)(1)(1)(1) = 77UL

        dd(1)(0)(0)(0) = 80UL
        dd(1)(0)(0)(1) = 81UL
        dd(1)(0)(1)(0) = 82UL
        dd(1)(0)(1)(1) = 83UL
        dd(1)(1)(0)(0) = 84UL
        dd(1)(1)(0)(1) = 85UL
        dd(1)(1)(1)(0) = 86UL
        dd(1)(1)(1)(1) = 87UL

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
