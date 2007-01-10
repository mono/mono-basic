Public Module JaggedArrayLocalsVariableDecimal1
    Function Main() As Int32
        Dim result As Int32

        Dim a As Decimal()
        Dim b As Decimal()()
        Dim c As Decimal()()()
        Dim d As Decimal()()()()
        Dim aa() As Decimal
        Dim bb()() As Decimal
        Dim cc()()() As Decimal
        Dim dd()()()() As Decimal

        a = New Decimal() {}
        b = New Decimal()() {}
        c = New Decimal()()() {}
        d = New Decimal()()()() {}

        aa = New Decimal() {}
        bb = New Decimal()() {}
        cc = New Decimal()()() {}
        dd = New Decimal()()()() {}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 0 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 0 Then result += JaggedArrayVerifier.Report()


        a = New Decimal() {}
        b = New Decimal()() {New Decimal() {}}
        c = New Decimal()()() {New Decimal()() {New Decimal() {}}}
        d = New Decimal()()()() {New Decimal()()() {New Decimal()() {New Decimal() {}}}}

        aa = New Decimal() {}
        bb = New Decimal()() {New Decimal() {}}
        cc = New Decimal()()() {New Decimal()() {New Decimal() {}}}
        dd = New Decimal()()()() {New Decimal()()() {New Decimal()() {New Decimal() {}}}}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 1 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 1 Then result += JaggedArrayVerifier.Report()

        a = New Decimal() {1D, 2D}
        b = New Decimal()() {New Decimal() {10D, 11D}, New Decimal() {12D, 13D}}
        c = New Decimal()()() {New Decimal()() {New Decimal() {20D, 21D}, New Decimal() {22D, 23D}}, New Decimal()() {New Decimal() {24D, 25D}, New Decimal() {26D, 27D}}}
        d = New Decimal()()()() {New Decimal()()() {New Decimal()() {New Decimal() {30D, 31D}, New Decimal() {32D, 33D}}, New Decimal()() {New Decimal() {34D, 35D}, New Decimal() {36D, 37D}}}, New Decimal()()() {New Decimal()() {New Decimal() {40D, 41D}, New Decimal() {42D, 43D}}, New Decimal()() {New Decimal() {44D, 45D}, New Decimal() {46D, 47D}}}}

        aa = New Decimal() {1D, 2D}
        bb = New Decimal()() {New Decimal() {10D, 11D}, New Decimal() {12D, 13D}}
        cc = New Decimal()()() {New Decimal()() {New Decimal() {20D, 21D}, New Decimal() {22D, 23D}}, New Decimal()() {New Decimal() {24D, 25D}, New Decimal() {26D, 27D}}}
        dd = New Decimal()()()() {New Decimal()()() {New Decimal()() {New Decimal() {30D, 31D}, New Decimal() {32D, 33D}}, New Decimal()() {New Decimal() {34D, 35D}, New Decimal() {36D, 37D}}}, New Decimal()()() {New Decimal()() {New Decimal() {40D, 41D}, New Decimal() {42D, 43D}}, New Decimal()() {New Decimal() {44D, 45D}, New Decimal() {46D, 47D}}}}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 2 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 2 Then result += JaggedArrayVerifier.Report()

        a = New Decimal() {51D, 52D}
        b = New Decimal()() {New Decimal() {50D, 51D}, New Decimal() {52D, 53D}}
        c = New Decimal()()() {New Decimal()() {New Decimal() {60D, 61D}, New Decimal() {62D, 63D}}, New Decimal()() {New Decimal() {64D, 65D}, New Decimal() {66D, 67D}}}
        d = New Decimal()()()() {New Decimal()()() {New Decimal()() {New Decimal() {70D, 71D}, New Decimal() {72D, 73D}}, New Decimal()() {New Decimal() {74D, 75D}, New Decimal() {76D, 77D}}}, New Decimal()()() {New Decimal()() {New Decimal() {80D, 81D}, New Decimal() {82D, 83D}}, New Decimal()() {New Decimal() {84D, 85D}, New Decimal() {86D, 87D}}}}

        aa(0) = 51D
        aa(1) = 52D
        bb(0)(0) = 50D
        bb(0)(1) = 51D
        bb(1)(0) = 52D
        bb(1)(1) = 53D
        cc(0)(0)(0) = 60D
        cc(0)(0)(1) = 61D
        cc(0)(1)(0) = 62D
        cc(0)(1)(1) = 63D
        cc(1)(0)(0) = 64D
        cc(1)(0)(1) = 65D
        cc(1)(1)(0) = 66D
        cc(1)(1)(1) = 67D

        dd(0)(0)(0)(0) = 70D
        dd(0)(0)(0)(1) = 71D
        dd(0)(0)(1)(0) = 72D
        dd(0)(0)(1)(1) = 73D
        dd(0)(1)(0)(0) = 74D
        dd(0)(1)(0)(1) = 75D
        dd(0)(1)(1)(0) = 76D
        dd(0)(1)(1)(1) = 77D

        dd(1)(0)(0)(0) = 80D
        dd(1)(0)(0)(1) = 81D
        dd(1)(0)(1)(0) = 82D
        dd(1)(0)(1)(1) = 83D
        dd(1)(1)(0)(0) = 84D
        dd(1)(1)(0)(1) = 85D
        dd(1)(1)(1)(0) = 86D
        dd(1)(1)(1)(1) = 87D

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
