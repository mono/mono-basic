Public Module JaggedArrayFunctionVariableInteger1
    Dim _a As Integer()
    Dim _b As Integer()()
    Dim _c As Integer()()()
    Dim _d As Integer()()()()
    Dim _aa() As Integer
    Dim _bb()() As Integer
    Dim _cc()()() As Integer
    Dim _dd()()()() As Integer

    Function a() As Integer()
        Return _a
    End Function

    Function b() As Integer()()
        Return _b
    End Function

    Function c() As Integer()()()
        Return _c
    End Function

    Function d() As Integer()()()()
        Return _d
    End Function

    Function aa() As Integer()
        Return _aa
    End Function

    Function bb() As Integer()()
        Return _bb
    End Function

    Function cc() As Integer()()()
        Return _cc
    End Function

    Function dd() As Integer()()()()
        Return _dd
    End Function

    Function Main() As Int32
        Dim result As Int32

        _a = New Integer() {}
        _b = New Integer()() {}
        _c = New Integer()()() {}
        _d = New Integer()()()() {}

        _aa = New Integer() {}
        _bb = New Integer()() {}
        _cc = New Integer()()() {}
        _dd = New Integer()()()() {}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 0 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 0 Then result += JaggedArrayVerifier.Report()


        _a = New Integer() {}
        _b = New Integer()() {New Integer() {}}
        _c = New Integer()()() {New Integer()() {New Integer() {}}}
        _d = New Integer()()()() {New Integer()()() {New Integer()() {New Integer() {}}}}

        _aa = New Integer() {}
        _bb = New Integer()() {New Integer() {}}
        _cc = New Integer()()() {New Integer()() {New Integer() {}}}
        _dd = New Integer()()()() {New Integer()()() {New Integer()() {New Integer() {}}}}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 1 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 1 Then result += JaggedArrayVerifier.Report()

        _a = New Integer() {1I, 2I}
        _b = New Integer()() {New Integer() {10I, 11I}, New Integer() {12I, 13I}}
        _c = New Integer()()() {New Integer()() {New Integer() {20I, 21I}, New Integer() {22I, 23I}}, New Integer()() {New Integer() {24I, 25I}, New Integer() {26I, 27I}}}
        _d = New Integer()()()() {New Integer()()() {New Integer()() {New Integer() {30I, 31I}, New Integer() {32I, 33I}}, New Integer()() {New Integer() {34I, 35I}, New Integer() {36I, 37I}}}, New Integer()()() {New Integer()() {New Integer() {40I, 41I}, New Integer() {42I, 43I}}, New Integer()() {New Integer() {44I, 45I}, New Integer() {46I, 47I}}}}

        _aa = New Integer() {1I, 2I}
        _bb = New Integer()() {New Integer() {10I, 11I}, New Integer() {12I, 13I}}
        _cc = New Integer()()() {New Integer()() {New Integer() {20I, 21I}, New Integer() {22I, 23I}}, New Integer()() {New Integer() {24I, 25I}, New Integer() {26I, 27I}}}
        _dd = New Integer()()()() {New Integer()()() {New Integer()() {New Integer() {30I, 31I}, New Integer() {32I, 33I}}, New Integer()() {New Integer() {34I, 35I}, New Integer() {36I, 37I}}}, New Integer()()() {New Integer()() {New Integer() {40I, 41I}, New Integer() {42I, 43I}}, New Integer()() {New Integer() {44I, 45I}, New Integer() {46I, 47I}}}}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 2 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 2 Then result += JaggedArrayVerifier.Report()

        _a = New Integer() {51I, 52I}
        _b = New Integer()() {New Integer() {50I, 51I}, New Integer() {52I, 53I}}
        _c = New Integer()()() {New Integer()() {New Integer() {60I, 61I}, New Integer() {62I, 63I}}, New Integer()() {New Integer() {64I, 65I}, New Integer() {66I, 67I}}}
        _d = New Integer()()()() {New Integer()()() {New Integer()() {New Integer() {70I, 71I}, New Integer() {72I, 73I}}, New Integer()() {New Integer() {74I, 75I}, New Integer() {76I, 77I}}}, New Integer()()() {New Integer()() {New Integer() {80I, 81I}, New Integer() {82I, 83I}}, New Integer()() {New Integer() {84I, 85I}, New Integer() {86I, 87I}}}}

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
