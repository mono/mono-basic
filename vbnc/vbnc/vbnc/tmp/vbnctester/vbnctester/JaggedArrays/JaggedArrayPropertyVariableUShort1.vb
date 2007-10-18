Public Module JaggedArrayPropertyVariableUShort1
    Dim _a As UShort()
    Dim _b As UShort()()
    Dim _c As UShort()()()
    Dim _d As UShort()()()()
    Dim _aa() As UShort
    Dim _bb()() As UShort
    Dim _cc()()() As UShort
    Dim _dd()()()() As UShort

    Property a() As UShort()
        Get
            Return _a
        End Get
        Set(ByVal value As UShort())
            _a = value
        End Set
    End Property

    Property b() As UShort()()
        Get
            Return _b
        End Get
        Set(ByVal value As UShort()())
            _b = value
        End Set
    End Property

    Property c() As UShort()()()
        Get
            Return _c
        End Get
        Set(ByVal value As UShort()()())
            _c = value
        End Set
    End Property

    Property d() As UShort()()()()
        Get
            Return _d
        End Get
        Set(ByVal value As UShort()()()())
            _d = value
        End Set
    End Property

    Property aa() As UShort()
        Get
            Return _aa
        End Get
        Set(ByVal value As UShort())
            _aa = value
        End Set
    End Property

    Property bb() As UShort()()
        Get
            Return _bb
        End Get
        Set(ByVal value As UShort()())
            _bb = value
        End Set
    End Property

    Property cc() As UShort()()()
        Get
            Return _cc
        End Get
        Set(ByVal value As UShort()()())
            _cc = value
        End Set
    End Property

    Property dd() As UShort()()()()
        Get
            Return _dd
        End Get
        Set(ByVal value As UShort()()()())
            _dd = value
        End Set
    End Property

    Function Main() As Int32
        Dim result As Int32
        _a = New UShort() {}
        _b = New UShort()() {}
        _c = New UShort()()() {}
        _d = New UShort()()()() {}

        _aa = New UShort() {}
        _bb = New UShort()() {}
        _cc = New UShort()()() {}
        _dd = New UShort()()()() {}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 0 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 0 Then result += JaggedArrayVerifier.Report()


        _a = New UShort() {}
        _b = New UShort()() {New UShort() {}}
        _c = New UShort()()() {New UShort()() {New UShort() {}}}
        _d = New UShort()()()() {New UShort()()() {New UShort()() {New UShort() {}}}}

        _aa = New UShort() {}
        _bb = New UShort()() {New UShort() {}}
        _cc = New UShort()()() {New UShort()() {New UShort() {}}}
        _dd = New UShort()()()() {New UShort()()() {New UShort()() {New UShort() {}}}}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 1 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 1 Then result += JaggedArrayVerifier.Report()

        _a = New UShort() {1US, 2US}
        _b = New UShort()() {New UShort() {10US, 11US}, New UShort() {12US, 13US}}
        _c = New UShort()()() {New UShort()() {New UShort() {20US, 21US}, New UShort() {22US, 23US}}, New UShort()() {New UShort() {24US, 25US}, New UShort() {26US, 27US}}}
        _d = New UShort()()()() {New UShort()()() {New UShort()() {New UShort() {30US, 31US}, New UShort() {32US, 33US}}, New UShort()() {New UShort() {34US, 35US}, New UShort() {36US, 37US}}}, New UShort()()() {New UShort()() {New UShort() {40US, 41US}, New UShort() {42US, 43US}}, New UShort()() {New UShort() {44US, 45US}, New UShort() {46US, 47US}}}}

        _aa = New UShort() {1US, 2US}
        _bb = New UShort()() {New UShort() {10US, 11US}, New UShort() {12US, 13US}}
        _cc = New UShort()()() {New UShort()() {New UShort() {20US, 21US}, New UShort() {22US, 23US}}, New UShort()() {New UShort() {24US, 25US}, New UShort() {26US, 27US}}}
        _dd = New UShort()()()() {New UShort()()() {New UShort()() {New UShort() {30US, 31US}, New UShort() {32US, 33US}}, New UShort()() {New UShort() {34US, 35US}, New UShort() {36US, 37US}}}, New UShort()()() {New UShort()() {New UShort() {40US, 41US}, New UShort() {42US, 43US}}, New UShort()() {New UShort() {44US, 45US}, New UShort() {46US, 47US}}}}

result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 2 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 2 Then result += JaggedArrayVerifier.Report()

        _a = New UShort() {51US, 52US}
        _b = New UShort()() {New UShort() {50US, 51US}, New UShort() {52US, 53US}}
        _c = New UShort()()() {New UShort()() {New UShort() {60US, 61US}, New UShort() {62US, 63US}}, New UShort()() {New UShort() {64US, 65US}, New UShort() {66US, 67US}}}
        _d = New UShort()()()() {New UShort()()() {New UShort()() {New UShort() {70US, 71US}, New UShort() {72US, 73US}}, New UShort()() {New UShort() {74US, 75US}, New UShort() {76US, 77US}}}, New UShort()()() {New UShort()() {New UShort() {80US, 81US}, New UShort() {82US, 83US}}, New UShort()() {New UShort() {84US, 85US}, New UShort() {86US, 87US}}}}

        aa(0) = 51US
        aa(1) = 52US
        bb(0)(0) = 50US
        bb(0)(1) = 51US
        bb(1)(0) = 52US
        bb(1)(1) = 53US
        cc(0)(0)(0) = 60US
        cc(0)(0)(1) = 61US
        cc(0)(1)(0) = 62US
        cc(0)(1)(1) = 63US
        cc(1)(0)(0) = 64US
        cc(1)(0)(1) = 65US
        cc(1)(1)(0) = 66US
        cc(1)(1)(1) = 67US

        dd(0)(0)(0)(0) = 70US
        dd(0)(0)(0)(1) = 71US
        dd(0)(0)(1)(0) = 72US
        dd(0)(0)(1)(1) = 73US
        dd(0)(1)(0)(0) = 74US
        dd(0)(1)(0)(1) = 75US
        dd(0)(1)(1)(0) = 76US
        dd(0)(1)(1)(1) = 77US

        dd(1)(0)(0)(0) = 80US
        dd(1)(0)(0)(1) = 81US
        dd(1)(0)(1)(0) = 82US
        dd(1)(0)(1)(1) = 83US
        dd(1)(1)(0)(0) = 84US
        dd(1)(1)(0)(1) = 85US
        dd(1)(1)(1)(0) = 86US
        dd(1)(1)(1)(1) = 87US

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
