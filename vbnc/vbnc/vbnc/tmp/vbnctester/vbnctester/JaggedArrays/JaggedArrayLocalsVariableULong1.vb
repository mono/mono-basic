Public Module JaggedArrayLocalsVariableULong1
    Function Main() As Int32
        Dim result As Int32

        Dim a As ULong()
        Dim b As ULong()()
        Dim c As ULong()()()
        Dim d As ULong()()()()
        Dim aa() As ULong
        Dim bb()() As ULong
        Dim cc()()() As ULong
        Dim dd()()()() As ULong

        a = New ULong() {}
        b = New ULong()() {}
        c = New ULong()()() {}
        d = New ULong()()()() {}

        aa = New ULong() {}
        bb = New ULong()() {}
        cc = New ULong()()() {}
        dd = New ULong()()()() {}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 0 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 0 Then result += JaggedArrayVerifier.Report()


        a = New ULong() {}
        b = New ULong()() {New ULong() {}}
        c = New ULong()()() {New ULong()() {New ULong() {}}}
        d = New ULong()()()() {New ULong()()() {New ULong()() {New ULong() {}}}}

        aa = New ULong() {}
        bb = New ULong()() {New ULong() {}}
        cc = New ULong()()() {New ULong()() {New ULong() {}}}
        dd = New ULong()()()() {New ULong()()() {New ULong()() {New ULong() {}}}}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 1 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 1 Then result += JaggedArrayVerifier.Report()

        a = New ULong() {1UL, 2UL}
        b = New ULong()() {New ULong() {10UL, 11UL}, New ULong() {12UL, 13UL}}
        c = New ULong()()() {New ULong()() {New ULong() {20UL, 21UL}, New ULong() {22UL, 23UL}}, New ULong()() {New ULong() {24UL, 25UL}, New ULong() {26UL, 27UL}}}
        d = New ULong()()()() {New ULong()()() {New ULong()() {New ULong() {30UL, 31UL}, New ULong() {32UL, 33UL}}, New ULong()() {New ULong() {34UL, 35UL}, New ULong() {36UL, 37UL}}}, New ULong()()() {New ULong()() {New ULong() {40UL, 41UL}, New ULong() {42UL, 43UL}}, New ULong()() {New ULong() {44UL, 45UL}, New ULong() {46UL, 47UL}}}}

        aa = New ULong() {1UL, 2UL}
        bb = New ULong()() {New ULong() {10UL, 11UL}, New ULong() {12UL, 13UL}}
        cc = New ULong()()() {New ULong()() {New ULong() {20UL, 21UL}, New ULong() {22UL, 23UL}}, New ULong()() {New ULong() {24UL, 25UL}, New ULong() {26UL, 27UL}}}
        dd = New ULong()()()() {New ULong()()() {New ULong()() {New ULong() {30UL, 31UL}, New ULong() {32UL, 33UL}}, New ULong()() {New ULong() {34UL, 35UL}, New ULong() {36UL, 37UL}}}, New ULong()()() {New ULong()() {New ULong() {40UL, 41UL}, New ULong() {42UL, 43UL}}, New ULong()() {New ULong() {44UL, 45UL}, New ULong() {46UL, 47UL}}}}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 2 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 2 Then result += JaggedArrayVerifier.Report()

        a = New ULong() {51UL, 52UL}
        b = New ULong()() {New ULong() {50UL, 51UL}, New ULong() {52UL, 53UL}}
        c = New ULong()()() {New ULong()() {New ULong() {60UL, 61UL}, New ULong() {62UL, 63UL}}, New ULong()() {New ULong() {64UL, 65UL}, New ULong() {66UL, 67UL}}}
        d = New ULong()()()() {New ULong()()() {New ULong()() {New ULong() {70UL, 71UL}, New ULong() {72UL, 73UL}}, New ULong()() {New ULong() {74UL, 75UL}, New ULong() {76UL, 77UL}}}, New ULong()()() {New ULong()() {New ULong() {80UL, 81UL}, New ULong() {82UL, 83UL}}, New ULong()() {New ULong() {84UL, 85UL}, New ULong() {86UL, 87UL}}}}

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
