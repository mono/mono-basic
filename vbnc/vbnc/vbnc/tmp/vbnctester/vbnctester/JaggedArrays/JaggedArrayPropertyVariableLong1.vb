Public Module JaggedArrayPropertyVariableLong1
    Dim _a As Long()
    Dim _b As Long()()
    Dim _c As Long()()()
    Dim _d As Long()()()()
    Dim _aa() As Long
    Dim _bb()() As Long
    Dim _cc()()() As Long
    Dim _dd()()()() As Long

    Property a() As Long()
        Get
            Return _a
        End Get
        Set(ByVal value As Long())
            _a = value
        End Set
    End Property

    Property b() As Long()()
        Get
            Return _b
        End Get
        Set(ByVal value As Long()())
            _b = value
        End Set
    End Property

    Property c() As Long()()()
        Get
            Return _c
        End Get
        Set(ByVal value As Long()()())
            _c = value
        End Set
    End Property

    Property d() As Long()()()()
        Get
            Return _d
        End Get
        Set(ByVal value As Long()()()())
            _d = value
        End Set
    End Property

    Property aa() As Long()
        Get
            Return _aa
        End Get
        Set(ByVal value As Long())
            _aa = value
        End Set
    End Property

    Property bb() As Long()()
        Get
            Return _bb
        End Get
        Set(ByVal value As Long()())
            _bb = value
        End Set
    End Property

    Property cc() As Long()()()
        Get
            Return _cc
        End Get
        Set(ByVal value As Long()()())
            _cc = value
        End Set
    End Property

    Property dd() As Long()()()()
        Get
            Return _dd
        End Get
        Set(ByVal value As Long()()()())
            _dd = value
        End Set
    End Property

    Function Main() As Int32
        Dim result As Int32
        _a = New Long() {}
        _b = New Long()() {}
        _c = New Long()()() {}
        _d = New Long()()()() {}

        _aa = New Long() {}
        _bb = New Long()() {}
        _cc = New Long()()() {}
        _dd = New Long()()()() {}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 0 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 0 Then result += JaggedArrayVerifier.Report()


        _a = New Long() {}
        _b = New Long()() {New Long() {}}
        _c = New Long()()() {New Long()() {New Long() {}}}
        _d = New Long()()()() {New Long()()() {New Long()() {New Long() {}}}}

        _aa = New Long() {}
        _bb = New Long()() {New Long() {}}
        _cc = New Long()()() {New Long()() {New Long() {}}}
        _dd = New Long()()()() {New Long()()() {New Long()() {New Long() {}}}}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 1 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 1 Then result += JaggedArrayVerifier.Report()

        _a = New Long() {1L, 2L}
        _b = New Long()() {New Long() {10L, 11L}, New Long() {12L, 13L}}
        _c = New Long()()() {New Long()() {New Long() {20L, 21L}, New Long() {22L, 23L}}, New Long()() {New Long() {24L, 25L}, New Long() {26L, 27L}}}
        _d = New Long()()()() {New Long()()() {New Long()() {New Long() {30L, 31L}, New Long() {32L, 33L}}, New Long()() {New Long() {34L, 35L}, New Long() {36L, 37L}}}, New Long()()() {New Long()() {New Long() {40L, 41L}, New Long() {42L, 43L}}, New Long()() {New Long() {44L, 45L}, New Long() {46L, 47L}}}}

        _aa = New Long() {1L, 2L}
        _bb = New Long()() {New Long() {10L, 11L}, New Long() {12L, 13L}}
        _cc = New Long()()() {New Long()() {New Long() {20L, 21L}, New Long() {22L, 23L}}, New Long()() {New Long() {24L, 25L}, New Long() {26L, 27L}}}
        _dd = New Long()()()() {New Long()()() {New Long()() {New Long() {30L, 31L}, New Long() {32L, 33L}}, New Long()() {New Long() {34L, 35L}, New Long() {36L, 37L}}}, New Long()()() {New Long()() {New Long() {40L, 41L}, New Long() {42L, 43L}}, New Long()() {New Long() {44L, 45L}, New Long() {46L, 47L}}}}

result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 2 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 2 Then result += JaggedArrayVerifier.Report()

        _a = New Long() {51L, 52L}
        _b = New Long()() {New Long() {50L, 51L}, New Long() {52L, 53L}}
        _c = New Long()()() {New Long()() {New Long() {60L, 61L}, New Long() {62L, 63L}}, New Long()() {New Long() {64L, 65L}, New Long() {66L, 67L}}}
        _d = New Long()()()() {New Long()()() {New Long()() {New Long() {70L, 71L}, New Long() {72L, 73L}}, New Long()() {New Long() {74L, 75L}, New Long() {76L, 77L}}}, New Long()()() {New Long()() {New Long() {80L, 81L}, New Long() {82L, 83L}}, New Long()() {New Long() {84L, 85L}, New Long() {86L, 87L}}}}

        aa(0) = 51L
        aa(1) = 52L
        bb(0)(0) = 50L
        bb(0)(1) = 51L
        bb(1)(0) = 52L
        bb(1)(1) = 53L
        cc(0)(0)(0) = 60L
        cc(0)(0)(1) = 61L
        cc(0)(1)(0) = 62L
        cc(0)(1)(1) = 63L
        cc(1)(0)(0) = 64L
        cc(1)(0)(1) = 65L
        cc(1)(1)(0) = 66L
        cc(1)(1)(1) = 67L

        dd(0)(0)(0)(0) = 70L
        dd(0)(0)(0)(1) = 71L
        dd(0)(0)(1)(0) = 72L
        dd(0)(0)(1)(1) = 73L
        dd(0)(1)(0)(0) = 74L
        dd(0)(1)(0)(1) = 75L
        dd(0)(1)(1)(0) = 76L
        dd(0)(1)(1)(1) = 77L

        dd(1)(0)(0)(0) = 80L
        dd(1)(0)(0)(1) = 81L
        dd(1)(0)(1)(0) = 82L
        dd(1)(0)(1)(1) = 83L
        dd(1)(1)(0)(0) = 84L
        dd(1)(1)(0)(1) = 85L
        dd(1)(1)(1)(0) = 86L
        dd(1)(1)(1)(1) = 87L

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
