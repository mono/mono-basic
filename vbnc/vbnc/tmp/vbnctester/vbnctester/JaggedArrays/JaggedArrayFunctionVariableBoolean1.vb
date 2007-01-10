Public Module JaggedArrayFunctionVariableBoolean1
    Dim _a As Boolean()
    Dim _b As Boolean()()
    Dim _c As Boolean()()()
    Dim _d As Boolean()()()()
    Dim _aa() As Boolean
    Dim _bb()() As Boolean
    Dim _cc()()() As Boolean
    Dim _dd()()()() As Boolean

    Function a() As Boolean()
        Return _a
    End Function

    Function b() As Boolean()()
        Return _b
    End Function

    Function c() As Boolean()()()
        Return _c
    End Function

    Function d() As Boolean()()()()
        Return _d
    End Function

    Function aa() As Boolean()
        Return _aa
    End Function

    Function bb() As Boolean()()
        Return _bb
    End Function

    Function cc() As Boolean()()()
        Return _cc
    End Function

    Function dd() As Boolean()()()()
        Return _dd
    End Function

    Function Main() As Int32
        Dim result As Int32

        _a = New Boolean() {}
        _b = New Boolean()() {}
        _c = New Boolean()()() {}
        _d = New Boolean()()()() {}

        _aa = New Boolean() {}
        _bb = New Boolean()() {}
        _cc = New Boolean()()() {}
        _dd = New Boolean()()()() {}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 0 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 0 Then result += JaggedArrayVerifier.Report()


        _a = New Boolean() {}
        _b = New Boolean()() {New Boolean() {}}
        _c = New Boolean()()() {New Boolean()() {New Boolean() {}}}
        _d = New Boolean()()()() {New Boolean()()() {New Boolean()() {New Boolean() {}}}}

        _aa = New Boolean() {}
        _bb = New Boolean()() {New Boolean() {}}
        _cc = New Boolean()()() {New Boolean()() {New Boolean() {}}}
        _dd = New Boolean()()()() {New Boolean()()() {New Boolean()() {New Boolean() {}}}}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 1 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 1 Then result += JaggedArrayVerifier.Report()

        _a = New Boolean() {CBool(1), CBool(2)}
        _b = New Boolean()() {New Boolean() {CBool(10), CBool(11)}, New Boolean() {CBool(12), CBool(13)}}
        _c = New Boolean()()() {New Boolean()() {New Boolean() {CBool(20), CBool(21)}, New Boolean() {CBool(22), CBool(23)}}, New Boolean()() {New Boolean() {CBool(24), CBool(25)}, New Boolean() {CBool(26), CBool(27)}}}
        _d = New Boolean()()()() {New Boolean()()() {New Boolean()() {New Boolean() {CBool(30), CBool(31)}, New Boolean() {CBool(32), CBool(33)}}, New Boolean()() {New Boolean() {CBool(34), CBool(35)}, New Boolean() {CBool(36), CBool(37)}}}, New Boolean()()() {New Boolean()() {New Boolean() {CBool(40), CBool(41)}, New Boolean() {CBool(42), CBool(43)}}, New Boolean()() {New Boolean() {CBool(44), CBool(45)}, New Boolean() {CBool(46), CBool(47)}}}}

        _aa = New Boolean() {CBool(1), CBool(2)}
        _bb = New Boolean()() {New Boolean() {CBool(10), CBool(11)}, New Boolean() {CBool(12), CBool(13)}}
        _cc = New Boolean()()() {New Boolean()() {New Boolean() {CBool(20), CBool(21)}, New Boolean() {CBool(22), CBool(23)}}, New Boolean()() {New Boolean() {CBool(24), CBool(25)}, New Boolean() {CBool(26), CBool(27)}}}
        _dd = New Boolean()()()() {New Boolean()()() {New Boolean()() {New Boolean() {CBool(30), CBool(31)}, New Boolean() {CBool(32), CBool(33)}}, New Boolean()() {New Boolean() {CBool(34), CBool(35)}, New Boolean() {CBool(36), CBool(37)}}}, New Boolean()()() {New Boolean()() {New Boolean() {CBool(40), CBool(41)}, New Boolean() {CBool(42), CBool(43)}}, New Boolean()() {New Boolean() {CBool(44), CBool(45)}, New Boolean() {CBool(46), CBool(47)}}}}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 2 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 2 Then result += JaggedArrayVerifier.Report()

        _a = New Boolean() {CBool(51), CBool(52)}
        _b = New Boolean()() {New Boolean() {CBool(50), CBool(51)}, New Boolean() {CBool(52), CBool(53)}}
        _c = New Boolean()()() {New Boolean()() {New Boolean() {CBool(60), CBool(61)}, New Boolean() {CBool(62), CBool(63)}}, New Boolean()() {New Boolean() {CBool(64), CBool(65)}, New Boolean() {CBool(66), CBool(67)}}}
        _d = New Boolean()()()() {New Boolean()()() {New Boolean()() {New Boolean() {CBool(70), CBool(71)}, New Boolean() {CBool(72), CBool(73)}}, New Boolean()() {New Boolean() {CBool(74), CBool(75)}, New Boolean() {CBool(76), CBool(77)}}}, New Boolean()()() {New Boolean()() {New Boolean() {CBool(80), CBool(81)}, New Boolean() {CBool(82), CBool(83)}}, New Boolean()() {New Boolean() {CBool(84), CBool(85)}, New Boolean() {CBool(86), CBool(87)}}}}

        aa(0) = CBool(51)
        aa(1) = CBool(52)
        bb(0)(0) = CBool(50)
        bb(0)(1) = CBool(51)
        bb(1)(0) = CBool(52)
        bb(1)(1) = CBool(53)
        cc(0)(0)(0) = CBool(60)
        cc(0)(0)(1) = CBool(61)
        cc(0)(1)(0) = CBool(62)
        cc(0)(1)(1) = CBool(63)
        cc(1)(0)(0) = CBool(64)
        cc(1)(0)(1) = CBool(65)
        cc(1)(1)(0) = CBool(66)
        cc(1)(1)(1) = CBool(67)

        dd(0)(0)(0)(0) = CBool(70)
        dd(0)(0)(0)(1) = CBool(71)
        dd(0)(0)(1)(0) = CBool(72)
        dd(0)(0)(1)(1) = CBool(73)
        dd(0)(1)(0)(0) = CBool(74)
        dd(0)(1)(0)(1) = CBool(75)
        dd(0)(1)(1)(0) = CBool(76)
        dd(0)(1)(1)(1) = CBool(77)

        dd(1)(0)(0)(0) = CBool(80)
        dd(1)(0)(0)(1) = CBool(81)
        dd(1)(0)(1)(0) = CBool(82)
        dd(1)(0)(1)(1) = CBool(83)
        dd(1)(1)(0)(0) = CBool(84)
        dd(1)(1)(0)(1) = CBool(85)
        dd(1)(1)(1)(0) = CBool(86)
        dd(1)(1)(1)(1) = CBool(87)

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
