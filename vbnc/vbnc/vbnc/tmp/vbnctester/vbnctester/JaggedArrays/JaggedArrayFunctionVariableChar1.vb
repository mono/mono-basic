Public Module JaggedArrayFunctionVariableChar1
    Dim _a As Char()
    Dim _b As Char()()
    Dim _c As Char()()()
    Dim _d As Char()()()()
    Dim _aa() As Char
    Dim _bb()() As Char
    Dim _cc()()() As Char
    Dim _dd()()()() As Char

    Function a() As Char()
        Return _a
    End Function

    Function b() As Char()()
        Return _b
    End Function

    Function c() As Char()()()
        Return _c
    End Function

    Function d() As Char()()()()
        Return _d
    End Function

    Function aa() As Char()
        Return _aa
    End Function

    Function bb() As Char()()
        Return _bb
    End Function

    Function cc() As Char()()()
        Return _cc
    End Function

    Function dd() As Char()()()()
        Return _dd
    End Function

    Function Main() As Int32
        Dim result As Int32

        _a = New Char() {}
        _b = New Char()() {}
        _c = New Char()()() {}
        _d = New Char()()()() {}

        _aa = New Char() {}
        _bb = New Char()() {}
        _cc = New Char()()() {}
        _dd = New Char()()()() {}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 0 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 0 Then result += JaggedArrayVerifier.Report()


        _a = New Char() {}
        _b = New Char()() {New Char() {}}
        _c = New Char()()() {New Char()() {New Char() {}}}
        _d = New Char()()()() {New Char()()() {New Char()() {New Char() {}}}}

        _aa = New Char() {}
        _bb = New Char()() {New Char() {}}
        _cc = New Char()()() {New Char()() {New Char() {}}}
        _dd = New Char()()()() {New Char()()() {New Char()() {New Char() {}}}}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 1 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 1 Then result += JaggedArrayVerifier.Report()

        _a = New Char() {Chr(1), Chr(2)}
        _b = New Char()() {New Char() {Chr(10), Chr(11)}, New Char() {Chr(12), Chr(13)}}
        _c = New Char()()() {New Char()() {New Char() {Chr(20), Chr(21)}, New Char() {Chr(22), Chr(23)}}, New Char()() {New Char() {Chr(24), Chr(25)}, New Char() {Chr(26), Chr(27)}}}
        _d = New Char()()()() {New Char()()() {New Char()() {New Char() {Chr(30), Chr(31)}, New Char() {Chr(32), Chr(33)}}, New Char()() {New Char() {Chr(34), Chr(35)}, New Char() {Chr(36), Chr(37)}}}, New Char()()() {New Char()() {New Char() {Chr(40), Chr(41)}, New Char() {Chr(42), Chr(43)}}, New Char()() {New Char() {Chr(44), Chr(45)}, New Char() {Chr(46), Chr(47)}}}}

        _aa = New Char() {Chr(1), Chr(2)}
        _bb = New Char()() {New Char() {Chr(10), Chr(11)}, New Char() {Chr(12), Chr(13)}}
        _cc = New Char()()() {New Char()() {New Char() {Chr(20), Chr(21)}, New Char() {Chr(22), Chr(23)}}, New Char()() {New Char() {Chr(24), Chr(25)}, New Char() {Chr(26), Chr(27)}}}
        _dd = New Char()()()() {New Char()()() {New Char()() {New Char() {Chr(30), Chr(31)}, New Char() {Chr(32), Chr(33)}}, New Char()() {New Char() {Chr(34), Chr(35)}, New Char() {Chr(36), Chr(37)}}}, New Char()()() {New Char()() {New Char() {Chr(40), Chr(41)}, New Char() {Chr(42), Chr(43)}}, New Char()() {New Char() {Chr(44), Chr(45)}, New Char() {Chr(46), Chr(47)}}}}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 2 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 2 Then result += JaggedArrayVerifier.Report()

        _a = New Char() {Chr(51), Chr(52)}
        _b = New Char()() {New Char() {Chr(50), Chr(51)}, New Char() {Chr(52), Chr(53)}}
        _c = New Char()()() {New Char()() {New Char() {Chr(60), Chr(61)}, New Char() {Chr(62), Chr(63)}}, New Char()() {New Char() {Chr(64), Chr(65)}, New Char() {Chr(66), Chr(67)}}}
        _d = New Char()()()() {New Char()()() {New Char()() {New Char() {Chr(70), Chr(71)}, New Char() {Chr(72), Chr(73)}}, New Char()() {New Char() {Chr(74), Chr(75)}, New Char() {Chr(76), Chr(77)}}}, New Char()()() {New Char()() {New Char() {Chr(80), Chr(81)}, New Char() {Chr(82), Chr(83)}}, New Char()() {New Char() {Chr(84), Chr(85)}, New Char() {Chr(86), Chr(87)}}}}

        aa(0) = Chr(51)
        aa(1) = Chr(52)
        bb(0)(0) = Chr(50)
        bb(0)(1) = Chr(51)
        bb(1)(0) = Chr(52)
        bb(1)(1) = Chr(53)
        cc(0)(0)(0) = Chr(60)
        cc(0)(0)(1) = Chr(61)
        cc(0)(1)(0) = Chr(62)
        cc(0)(1)(1) = Chr(63)
        cc(1)(0)(0) = Chr(64)
        cc(1)(0)(1) = Chr(65)
        cc(1)(1)(0) = Chr(66)
        cc(1)(1)(1) = Chr(67)

        dd(0)(0)(0)(0) = Chr(70)
        dd(0)(0)(0)(1) = Chr(71)
        dd(0)(0)(1)(0) = Chr(72)
        dd(0)(0)(1)(1) = Chr(73)
        dd(0)(1)(0)(0) = Chr(74)
        dd(0)(1)(0)(1) = Chr(75)
        dd(0)(1)(1)(0) = Chr(76)
        dd(0)(1)(1)(1) = Chr(77)

        dd(1)(0)(0)(0) = Chr(80)
        dd(1)(0)(0)(1) = Chr(81)
        dd(1)(0)(1)(0) = Chr(82)
        dd(1)(0)(1)(1) = Chr(83)
        dd(1)(1)(0)(0) = Chr(84)
        dd(1)(1)(0)(1) = Chr(85)
        dd(1)(1)(1)(0) = Chr(86)
        dd(1)(1)(1)(1) = Chr(87)

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
