Public Module JaggedArrayPropertyVariableSByte1
    Dim _a As SByte()
    Dim _b As SByte()()
    Dim _c As SByte()()()
    Dim _d As SByte()()()()
    Dim _aa() As SByte
    Dim _bb()() As SByte
    Dim _cc()()() As SByte
    Dim _dd()()()() As SByte

    Property a() As SByte()
        Get
            Return _a
        End Get
        Set(ByVal value As SByte())
            _a = value
        End Set
    End Property

    Property b() As SByte()()
        Get
            Return _b
        End Get
        Set(ByVal value As SByte()())
            _b = value
        End Set
    End Property

    Property c() As SByte()()()
        Get
            Return _c
        End Get
        Set(ByVal value As SByte()()())
            _c = value
        End Set
    End Property

    Property d() As SByte()()()()
        Get
            Return _d
        End Get
        Set(ByVal value As SByte()()()())
            _d = value
        End Set
    End Property

    Property aa() As SByte()
        Get
            Return _aa
        End Get
        Set(ByVal value As SByte())
            _aa = value
        End Set
    End Property

    Property bb() As SByte()()
        Get
            Return _bb
        End Get
        Set(ByVal value As SByte()())
            _bb = value
        End Set
    End Property

    Property cc() As SByte()()()
        Get
            Return _cc
        End Get
        Set(ByVal value As SByte()()())
            _cc = value
        End Set
    End Property

    Property dd() As SByte()()()()
        Get
            Return _dd
        End Get
        Set(ByVal value As SByte()()()())
            _dd = value
        End Set
    End Property

    Function Main() As Int32
        Dim result As Int32
        _a = New SByte() {}
        _b = New SByte()() {}
        _c = New SByte()()() {}
        _d = New SByte()()()() {}

        _aa = New SByte() {}
        _bb = New SByte()() {}
        _cc = New SByte()()() {}
        _dd = New SByte()()()() {}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 0 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 0 Then result += JaggedArrayVerifier.Report()


        _a = New SByte() {}
        _b = New SByte()() {New SByte() {}}
        _c = New SByte()()() {New SByte()() {New SByte() {}}}
        _d = New SByte()()()() {New SByte()()() {New SByte()() {New SByte() {}}}}

        _aa = New SByte() {}
        _bb = New SByte()() {New SByte() {}}
        _cc = New SByte()()() {New SByte()() {New SByte() {}}}
        _dd = New SByte()()()() {New SByte()()() {New SByte()() {New SByte() {}}}}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 1 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 1 Then result += JaggedArrayVerifier.Report()

        _a = New SByte() {CSByte(1), CSByte(2)}
        _b = New SByte()() {New SByte() {CSByte(10), CSByte(11)}, New SByte() {CSByte(12), CSByte(13)}}
        _c = New SByte()()() {New SByte()() {New SByte() {CSByte(20), CSByte(21)}, New SByte() {CSByte(22), CSByte(23)}}, New SByte()() {New SByte() {CSByte(24), CSByte(25)}, New SByte() {CSByte(26), CSByte(27)}}}
        _d = New SByte()()()() {New SByte()()() {New SByte()() {New SByte() {CSByte(30), CSByte(31)}, New SByte() {CSByte(32), CSByte(33)}}, New SByte()() {New SByte() {CSByte(34), CSByte(35)}, New SByte() {CSByte(36), CSByte(37)}}}, New SByte()()() {New SByte()() {New SByte() {CSByte(40), CSByte(41)}, New SByte() {CSByte(42), CSByte(43)}}, New SByte()() {New SByte() {CSByte(44), CSByte(45)}, New SByte() {CSByte(46), CSByte(47)}}}}

        _aa = New SByte() {CSByte(1), CSByte(2)}
        _bb = New SByte()() {New SByte() {CSByte(10), CSByte(11)}, New SByte() {CSByte(12), CSByte(13)}}
        _cc = New SByte()()() {New SByte()() {New SByte() {CSByte(20), CSByte(21)}, New SByte() {CSByte(22), CSByte(23)}}, New SByte()() {New SByte() {CSByte(24), CSByte(25)}, New SByte() {CSByte(26), CSByte(27)}}}
        _dd = New SByte()()()() {New SByte()()() {New SByte()() {New SByte() {CSByte(30), CSByte(31)}, New SByte() {CSByte(32), CSByte(33)}}, New SByte()() {New SByte() {CSByte(34), CSByte(35)}, New SByte() {CSByte(36), CSByte(37)}}}, New SByte()()() {New SByte()() {New SByte() {CSByte(40), CSByte(41)}, New SByte() {CSByte(42), CSByte(43)}}, New SByte()() {New SByte() {CSByte(44), CSByte(45)}, New SByte() {CSByte(46), CSByte(47)}}}}

result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 2 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 2 Then result += JaggedArrayVerifier.Report()

        _a = New SByte() {CSByte(51), CSByte(52)}
        _b = New SByte()() {New SByte() {CSByte(50), CSByte(51)}, New SByte() {CSByte(52), CSByte(53)}}
        _c = New SByte()()() {New SByte()() {New SByte() {CSByte(60), CSByte(61)}, New SByte() {CSByte(62), CSByte(63)}}, New SByte()() {New SByte() {CSByte(64), CSByte(65)}, New SByte() {CSByte(66), CSByte(67)}}}
        _d = New SByte()()()() {New SByte()()() {New SByte()() {New SByte() {CSByte(70), CSByte(71)}, New SByte() {CSByte(72), CSByte(73)}}, New SByte()() {New SByte() {CSByte(74), CSByte(75)}, New SByte() {CSByte(76), CSByte(77)}}}, New SByte()()() {New SByte()() {New SByte() {CSByte(80), CSByte(81)}, New SByte() {CSByte(82), CSByte(83)}}, New SByte()() {New SByte() {CSByte(84), CSByte(85)}, New SByte() {CSByte(86), CSByte(87)}}}}

        aa(0) = CSByte(51)
        aa(1) = CSByte(52)
        bb(0)(0) = CSByte(50)
        bb(0)(1) = CSByte(51)
        bb(1)(0) = CSByte(52)
        bb(1)(1) = CSByte(53)
        cc(0)(0)(0) = CSByte(60)
        cc(0)(0)(1) = CSByte(61)
        cc(0)(1)(0) = CSByte(62)
        cc(0)(1)(1) = CSByte(63)
        cc(1)(0)(0) = CSByte(64)
        cc(1)(0)(1) = CSByte(65)
        cc(1)(1)(0) = CSByte(66)
        cc(1)(1)(1) = CSByte(67)

        dd(0)(0)(0)(0) = CSByte(70)
        dd(0)(0)(0)(1) = CSByte(71)
        dd(0)(0)(1)(0) = CSByte(72)
        dd(0)(0)(1)(1) = CSByte(73)
        dd(0)(1)(0)(0) = CSByte(74)
        dd(0)(1)(0)(1) = CSByte(75)
        dd(0)(1)(1)(0) = CSByte(76)
        dd(0)(1)(1)(1) = CSByte(77)

        dd(1)(0)(0)(0) = CSByte(80)
        dd(1)(0)(0)(1) = CSByte(81)
        dd(1)(0)(1)(0) = CSByte(82)
        dd(1)(0)(1)(1) = CSByte(83)
        dd(1)(1)(0)(0) = CSByte(84)
        dd(1)(1)(0)(1) = CSByte(85)
        dd(1)(1)(1)(0) = CSByte(86)
        dd(1)(1)(1)(1) = CSByte(87)

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
