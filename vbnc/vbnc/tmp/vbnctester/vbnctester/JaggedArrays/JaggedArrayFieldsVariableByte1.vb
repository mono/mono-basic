Module JaggedArrayFieldsVariableByte1
    Dim a As Byte()
    Dim b As Byte()()
    Dim c As Byte()()()
    Dim d As Byte()()()()
    Dim aa() As Byte
    Dim bb()() As Byte
    Dim cc()()() As Byte
    Dim dd()()()() As Byte

    Function Main() As Int32
        Dim result As Int32

        a = New Byte() {}
        b = New Byte()() {}
        c = New Byte()()() {}
        d = New Byte()()()() {}

        aa = New Byte() {}
        bb = New Byte()() {}
        cc = New Byte()()() {}
        dd = New Byte()()()() {}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 0 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 0 Then result += JaggedArrayVerifier.Report()


        a = New Byte() {}
        b = New Byte()() {New Byte() {}}
        c = New Byte()()() {New Byte()() {New Byte() {}}}
        d = New Byte()()()() {New Byte()()() {New Byte()() {New Byte() {}}}}

        aa = New Byte() {}
        bb = New Byte()() {New Byte() {}}
        cc = New Byte()()() {New Byte()() {New Byte() {}}}
        dd = New Byte()()()() {New Byte()()() {New Byte()() {New Byte() {}}}}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 1 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 1 Then result += JaggedArrayVerifier.Report()

        a = New Byte() {CByte(1), CByte(2)}
        b = New Byte()() {New Byte() {CByte(10), CByte(11)}, New Byte() {CByte(12), CByte(13)}}
        c = New Byte()()() {New Byte()() {New Byte() {CByte(20), CByte(21)}, New Byte() {CByte(22), CByte(23)}}, New Byte()() {New Byte() {CByte(24), CByte(25)}, New Byte() {CByte(26), CByte(27)}}}
        d = New Byte()()()() {New Byte()()() {New Byte()() {New Byte() {CByte(30), CByte(31)}, New Byte() {CByte(32), CByte(33)}}, New Byte()() {New Byte() {CByte(34), CByte(35)}, New Byte() {CByte(36), CByte(37)}}}, New Byte()()() {New Byte()() {New Byte() {CByte(40), CByte(41)}, New Byte() {CByte(42), CByte(43)}}, New Byte()() {New Byte() {CByte(44), CByte(45)}, New Byte() {CByte(46), CByte(47)}}}}

        aa = New Byte() {CByte(1), CByte(2)}
        bb = New Byte()() {New Byte() {CByte(10), CByte(11)}, New Byte() {CByte(12), CByte(13)}}
        cc = New Byte()()() {New Byte()() {New Byte() {CByte(20), CByte(21)}, New Byte() {CByte(22), CByte(23)}}, New Byte()() {New Byte() {CByte(24), CByte(25)}, New Byte() {CByte(26), CByte(27)}}}
        dd = New Byte()()()() {New Byte()()() {New Byte()() {New Byte() {CByte(30), CByte(31)}, New Byte() {CByte(32), CByte(33)}}, New Byte()() {New Byte() {CByte(34), CByte(35)}, New Byte() {CByte(36), CByte(37)}}}, New Byte()()() {New Byte()() {New Byte() {CByte(40), CByte(41)}, New Byte() {CByte(42), CByte(43)}}, New Byte()() {New Byte() {CByte(44), CByte(45)}, New Byte() {CByte(46), CByte(47)}}}}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 2 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 2 Then result += JaggedArrayVerifier.Report()

        a = New Byte() {CByte(51), CByte(52)}
        b = New Byte()() {New Byte() {CByte(50), CByte(51)}, New Byte() {CByte(52), CByte(53)}}
        c = New Byte()()() {New Byte()() {New Byte() {CByte(60), CByte(61)}, New Byte() {CByte(62), CByte(63)}}, New Byte()() {New Byte() {CByte(64), CByte(65)}, New Byte() {CByte(66), CByte(67)}}}
        d = New Byte()()()() {New Byte()()() {New Byte()() {New Byte() {CByte(70), CByte(71)}, New Byte() {CByte(72), CByte(73)}}, New Byte()() {New Byte() {CByte(74), CByte(75)}, New Byte() {CByte(76), CByte(77)}}}, New Byte()()() {New Byte()() {New Byte() {CByte(80), CByte(81)}, New Byte() {CByte(82), CByte(83)}}, New Byte()() {New Byte() {CByte(84), CByte(85)}, New Byte() {CByte(86), CByte(87)}}}}

        aa(0) = CByte(51)
        aa(1) = CByte(52)
        bb(0)(0) = CByte(50)
        bb(0)(1) = CByte(51)
        bb(1)(0) = CByte(52)
        bb(1)(1) = CByte(53)
        cc(0)(0)(0) = CByte(60)
        cc(0)(0)(1) = CByte(61)
        cc(0)(1)(0) = CByte(62)
        cc(0)(1)(1) = CByte(63)
        cc(1)(0)(0) = CByte(64)
        cc(1)(0)(1) = CByte(65)
        cc(1)(1)(0) = CByte(66)
        cc(1)(1)(1) = CByte(67)

        dd(0)(0)(0)(0) = CByte(70)
        dd(0)(0)(0)(1) = CByte(71)
        dd(0)(0)(1)(0) = CByte(72)
        dd(0)(0)(1)(1) = CByte(73)
        dd(0)(1)(0)(0) = CByte(74)
        dd(0)(1)(0)(1) = CByte(75)
        dd(0)(1)(1)(0) = CByte(76)
        dd(0)(1)(1)(1) = CByte(77)

        dd(1)(0)(0)(0) = CByte(80)
        dd(1)(0)(0)(1) = CByte(81)
        dd(1)(0)(1)(0) = CByte(82)
        dd(1)(0)(1)(1) = CByte(83)
        dd(1)(1)(0)(0) = CByte(84)
        dd(1)(1)(0)(1) = CByte(85)
        dd(1)(1)(1)(0) = CByte(86)
        dd(1)(1)(1)(1) = CByte(87)

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
