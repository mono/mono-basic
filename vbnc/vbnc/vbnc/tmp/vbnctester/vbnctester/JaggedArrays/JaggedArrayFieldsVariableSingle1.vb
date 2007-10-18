Module JaggedArrayFieldsVariableSingle1
    Dim a As Single()
    Dim b As Single()()
    Dim c As Single()()()
    Dim d As Single()()()()
    Dim aa() As Single
    Dim bb()() As Single
    Dim cc()()() As Single
    Dim dd()()()() As Single

    Function Main() As Int32
        Dim result As Int32

        a = New Single() {}
        b = New Single()() {}
        c = New Single()()() {}
        d = New Single()()()() {}

        aa = New Single() {}
        bb = New Single()() {}
        cc = New Single()()() {}
        dd = New Single()()()() {}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 0 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 0 Then result += JaggedArrayVerifier.Report()


        a = New Single() {}
        b = New Single()() {New Single() {}}
        c = New Single()()() {New Single()() {New Single() {}}}
        d = New Single()()()() {New Single()()() {New Single()() {New Single() {}}}}

        aa = New Single() {}
        bb = New Single()() {New Single() {}}
        cc = New Single()()() {New Single()() {New Single() {}}}
        dd = New Single()()()() {New Single()()() {New Single()() {New Single() {}}}}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 1 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 1 Then result += JaggedArrayVerifier.Report()

        a = New Single() {1!, 2!}
        b = New Single()() {New Single() {10!, 11!}, New Single() {12!, 13!}}
        c = New Single()()() {New Single()() {New Single() {20!, 21!}, New Single() {22!, 23!}}, New Single()() {New Single() {24!, 25!}, New Single() {26!, 27!}}}
        d = New Single()()()() {New Single()()() {New Single()() {New Single() {30!, 31!}, New Single() {32!, 33!}}, New Single()() {New Single() {34!, 35!}, New Single() {36!, 37!}}}, New Single()()() {New Single()() {New Single() {40!, 41!}, New Single() {42!, 43!}}, New Single()() {New Single() {44!, 45!}, New Single() {46!, 47!}}}}

        aa = New Single() {1!, 2!}
        bb = New Single()() {New Single() {10!, 11!}, New Single() {12!, 13!}}
        cc = New Single()()() {New Single()() {New Single() {20!, 21!}, New Single() {22!, 23!}}, New Single()() {New Single() {24!, 25!}, New Single() {26!, 27!}}}
        dd = New Single()()()() {New Single()()() {New Single()() {New Single() {30!, 31!}, New Single() {32!, 33!}}, New Single()() {New Single() {34!, 35!}, New Single() {36!, 37!}}}, New Single()()() {New Single()() {New Single() {40!, 41!}, New Single() {42!, 43!}}, New Single()() {New Single() {44!, 45!}, New Single() {46!, 47!}}}}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 2 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 2 Then result += JaggedArrayVerifier.Report()

        a = New Single() {51!, 52!}
        b = New Single()() {New Single() {50!, 51!}, New Single() {52!, 53!}}
        c = New Single()()() {New Single()() {New Single() {60!, 61!}, New Single() {62!, 63!}}, New Single()() {New Single() {64!, 65!}, New Single() {66!, 67!}}}
        d = New Single()()()() {New Single()()() {New Single()() {New Single() {70!, 71!}, New Single() {72!, 73!}}, New Single()() {New Single() {74!, 75!}, New Single() {76!, 77!}}}, New Single()()() {New Single()() {New Single() {80!, 81!}, New Single() {82!, 83!}}, New Single()() {New Single() {84!, 85!}, New Single() {86!, 87!}}}}

        aa(0) = 51!
        aa(1) = 52!
        bb(0)(0) = 50!
        bb(0)(1) = 51!
        bb(1)(0) = 52!
        bb(1)(1) = 53!
        cc(0)(0)(0) = 60!
        cc(0)(0)(1) = 61!
        cc(0)(1)(0) = 62!
        cc(0)(1)(1) = 63!
        cc(1)(0)(0) = 64!
        cc(1)(0)(1) = 65!
        cc(1)(1)(0) = 66!
        cc(1)(1)(1) = 67!

        dd(0)(0)(0)(0) = 70!
        dd(0)(0)(0)(1) = 71!
        dd(0)(0)(1)(0) = 72!
        dd(0)(0)(1)(1) = 73!
        dd(0)(1)(0)(0) = 74!
        dd(0)(1)(0)(1) = 75!
        dd(0)(1)(1)(0) = 76!
        dd(0)(1)(1)(1) = 77!

        dd(1)(0)(0)(0) = 80!
        dd(1)(0)(0)(1) = 81!
        dd(1)(0)(1)(0) = 82!
        dd(1)(0)(1)(1) = 83!
        dd(1)(1)(0)(0) = 84!
        dd(1)(1)(0)(1) = 85!
        dd(1)(1)(1)(0) = 86!
        dd(1)(1)(1)(1) = 87!

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
