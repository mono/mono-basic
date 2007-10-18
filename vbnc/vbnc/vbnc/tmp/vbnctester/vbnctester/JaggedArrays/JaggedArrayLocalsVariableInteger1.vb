Public Module JaggedArrayLocalsVariableInteger1
    Function Main() As Int32
        Dim result As Int32

        Dim a As Integer()
        Dim b As Integer()()
        Dim c As Integer()()()
        Dim d As Integer()()()()
        Dim aa() As Integer
        Dim bb()() As Integer
        Dim cc()()() As Integer
        Dim dd()()()() As Integer

        a = New Integer() {}
        b = New Integer()() {}
        c = New Integer()()() {}
        d = New Integer()()()() {}

        aa = New Integer() {}
        bb = New Integer()() {}
        cc = New Integer()()() {}
        dd = New Integer()()()() {}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 0 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 0 Then result += JaggedArrayVerifier.Report()


        a = New Integer() {}
        b = New Integer()() {New Integer() {}}
        c = New Integer()()() {New Integer()() {New Integer() {}}}
        d = New Integer()()()() {New Integer()()() {New Integer()() {New Integer() {}}}}

        aa = New Integer() {}
        bb = New Integer()() {New Integer() {}}
        cc = New Integer()()() {New Integer()() {New Integer() {}}}
        dd = New Integer()()()() {New Integer()()() {New Integer()() {New Integer() {}}}}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 1 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 1 Then result += JaggedArrayVerifier.Report()

        a = New Integer() {1I, 2I}
        b = New Integer()() {New Integer() {10I, 11I}, New Integer() {12I, 13I}}
        c = New Integer()()() {New Integer()() {New Integer() {20I, 21I}, New Integer() {22I, 23I}}, New Integer()() {New Integer() {24I, 25I}, New Integer() {26I, 27I}}}
        d = New Integer()()()() {New Integer()()() {New Integer()() {New Integer() {30I, 31I}, New Integer() {32I, 33I}}, New Integer()() {New Integer() {34I, 35I}, New Integer() {36I, 37I}}}, New Integer()()() {New Integer()() {New Integer() {40I, 41I}, New Integer() {42I, 43I}}, New Integer()() {New Integer() {44I, 45I}, New Integer() {46I, 47I}}}}

        aa = New Integer() {1I, 2I}
        bb = New Integer()() {New Integer() {10I, 11I}, New Integer() {12I, 13I}}
        cc = New Integer()()() {New Integer()() {New Integer() {20I, 21I}, New Integer() {22I, 23I}}, New Integer()() {New Integer() {24I, 25I}, New Integer() {26I, 27I}}}
        dd = New Integer()()()() {New Integer()()() {New Integer()() {New Integer() {30I, 31I}, New Integer() {32I, 33I}}, New Integer()() {New Integer() {34I, 35I}, New Integer() {36I, 37I}}}, New Integer()()() {New Integer()() {New Integer() {40I, 41I}, New Integer() {42I, 43I}}, New Integer()() {New Integer() {44I, 45I}, New Integer() {46I, 47I}}}}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 2 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 2 Then result += JaggedArrayVerifier.Report()

        a = New Integer() {51I, 52I}
        b = New Integer()() {New Integer() {50I, 51I}, New Integer() {52I, 53I}}
        c = New Integer()()() {New Integer()() {New Integer() {60I, 61I}, New Integer() {62I, 63I}}, New Integer()() {New Integer() {64I, 65I}, New Integer() {66I, 67I}}}
        d = New Integer()()()() {New Integer()()() {New Integer()() {New Integer() {70I, 71I}, New Integer() {72I, 73I}}, New Integer()() {New Integer() {74I, 75I}, New Integer() {76I, 77I}}}, New Integer()()() {New Integer()() {New Integer() {80I, 81I}, New Integer() {82I, 83I}}, New Integer()() {New Integer() {84I, 85I}, New Integer() {86I, 87I}}}}

        aa(0) = 51I
        aa(1) = 52I
        bb(0)(0) = 50I
        bb(0)(1) = 51I
        bb(1)(0) = 52I
        bb(1)(1) = 53I
        cc(0)(0)(0) = 60I
        cc(0)(0)(1) = 61I
        cc(0)(1)(0) = 62I
        cc(0)(1)(1) = 63I
        cc(1)(0)(0) = 64I
        cc(1)(0)(1) = 65I
        cc(1)(1)(0) = 66I
        cc(1)(1)(1) = 67I

        dd(0)(0)(0)(0) = 70I
        dd(0)(0)(0)(1) = 71I
        dd(0)(0)(1)(0) = 72I
        dd(0)(0)(1)(1) = 73I
        dd(0)(1)(0)(0) = 74I
        dd(0)(1)(0)(1) = 75I
        dd(0)(1)(1)(0) = 76I
        dd(0)(1)(1)(1) = 77I

        dd(1)(0)(0)(0) = 80I
        dd(1)(0)(0)(1) = 81I
        dd(1)(0)(1)(0) = 82I
        dd(1)(0)(1)(1) = 83I
        dd(1)(1)(0)(0) = 84I
        dd(1)(1)(0)(1) = 85I
        dd(1)(1)(1)(0) = 86I
        dd(1)(1)(1)(1) = 87I

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
