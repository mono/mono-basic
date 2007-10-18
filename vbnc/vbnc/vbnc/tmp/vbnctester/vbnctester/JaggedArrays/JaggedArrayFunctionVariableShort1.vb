Public Module JaggedArrayFunctionVariableShort1
    Dim _a As Short()
    Dim _b As Short()()
    Dim _c As Short()()()
    Dim _d As Short()()()()
    Dim _aa() As Short
    Dim _bb()() As Short
    Dim _cc()()() As Short
    Dim _dd()()()() As Short

    Function a() As Short()
        Return _a
    End Function

    Function b() As Short()()
        Return _b
    End Function

    Function c() As Short()()()
        Return _c
    End Function

    Function d() As Short()()()()
        Return _d
    End Function

    Function aa() As Short()
        Return _aa
    End Function

    Function bb() As Short()()
        Return _bb
    End Function

    Function cc() As Short()()()
        Return _cc
    End Function

    Function dd() As Short()()()()
        Return _dd
    End Function

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
