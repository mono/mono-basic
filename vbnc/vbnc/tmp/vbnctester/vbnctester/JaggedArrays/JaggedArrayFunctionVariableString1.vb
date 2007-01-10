Public Module JaggedArrayFunctionVariableString1
    Dim _a As String()
    Dim _b As String()()
    Dim _c As String()()()
    Dim _d As String()()()()
    Dim _aa() As String
    Dim _bb()() As String
    Dim _cc()()() As String
    Dim _dd()()()() As String

    Function a() As String()
        Return _a
    End Function

    Function b() As String()()
        Return _b
    End Function

    Function c() As String()()()
        Return _c
    End Function

    Function d() As String()()()()
        Return _d
    End Function

    Function aa() As String()
        Return _aa
    End Function

    Function bb() As String()()
        Return _bb
    End Function

    Function cc() As String()()()
        Return _cc
    End Function

    Function dd() As String()()()()
        Return _dd
    End Function

    Function Main() As Int32
        Dim result As Int32

        _a = New String() {}
        _b = New String()() {}
        _c = New String()()() {}
        _d = New String()()()() {}

        _aa = New String() {}
        _bb = New String()() {}
        _cc = New String()()() {}
        _dd = New String()()()() {}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 0 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 0 Then result += JaggedArrayVerifier.Report()


        _a = New String() {}
        _b = New String()() {New String() {}}
        _c = New String()()() {New String()() {New String() {}}}
        _d = New String()()()() {New String()()() {New String()() {New String() {}}}}

        _aa = New String() {}
        _bb = New String()() {New String() {}}
        _cc = New String()()() {New String()() {New String() {}}}
        _dd = New String()()()() {New String()()() {New String()() {New String() {}}}}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 1 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 1 Then result += JaggedArrayVerifier.Report()

        _a = New String() {"1", "2"}
        _b = New String()() {New String() {"10", "11"}, New String() {"12", "13"}}
        _c = New String()()() {New String()() {New String() {"20", "21"}, New String() {"22", "23"}}, New String()() {New String() {"24", "25"}, New String() {"26", "27"}}}
        _d = New String()()()() {New String()()() {New String()() {New String() {"30", "31"}, New String() {"32", "33"}}, New String()() {New String() {"34", "35"}, New String() {"36", "37"}}}, New String()()() {New String()() {New String() {"40", "41"}, New String() {"42", "43"}}, New String()() {New String() {"44", "45"}, New String() {"46", "47"}}}}

        _aa = New String() {"1", "2"}
        _bb = New String()() {New String() {"10", "11"}, New String() {"12", "13"}}
        _cc = New String()()() {New String()() {New String() {"20", "21"}, New String() {"22", "23"}}, New String()() {New String() {"24", "25"}, New String() {"26", "27"}}}
        _dd = New String()()()() {New String()()() {New String()() {New String() {"30", "31"}, New String() {"32", "33"}}, New String()() {New String() {"34", "35"}, New String() {"36", "37"}}}, New String()()() {New String()() {New String() {"40", "41"}, New String() {"42", "43"}}, New String()() {New String() {"44", "45"}, New String() {"46", "47"}}}}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 2 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 2 Then result += JaggedArrayVerifier.Report()

        _a = New String() {"51", "52"}
        _b = New String()() {New String() {"50", "51"}, New String() {"52", "53"}}
        _c = New String()()() {New String()() {New String() {"60", "61"}, New String() {"62", "63"}}, New String()() {New String() {"64", "65"}, New String() {"66", "67"}}}
        _d = New String()()()() {New String()()() {New String()() {New String() {"70", "71"}, New String() {"72", "73"}}, New String()() {New String() {"74", "75"}, New String() {"76", "77"}}}, New String()()() {New String()() {New String() {"80", "81"}, New String() {"82", "83"}}, New String()() {New String() {"84", "85"}, New String() {"86", "87"}}}}

        aa(0) = "51"
        aa(1) = "52"
        bb(0)(0) = "50"
        bb(0)(1) = "51"
        bb(1)(0) = "52"
        bb(1)(1) = "53"
        cc(0)(0)(0) = "60"
        cc(0)(0)(1) = "61"
        cc(0)(1)(0) = "62"
        cc(0)(1)(1) = "63"
        cc(1)(0)(0) = "64"
        cc(1)(0)(1) = "65"
        cc(1)(1)(0) = "66"
        cc(1)(1)(1) = "67"

        dd(0)(0)(0)(0) = "70"
        dd(0)(0)(0)(1) = "71"
        dd(0)(0)(1)(0) = "72"
        dd(0)(0)(1)(1) = "73"
        dd(0)(1)(0)(0) = "74"
        dd(0)(1)(0)(1) = "75"
        dd(0)(1)(1)(0) = "76"
        dd(0)(1)(1)(1) = "77"

        dd(1)(0)(0)(0) = "80"
        dd(1)(0)(0)(1) = "81"
        dd(1)(0)(1)(0) = "82"
        dd(1)(0)(1)(1) = "83"
        dd(1)(1)(0)(0) = "84"
        dd(1)(1)(0)(1) = "85"
        dd(1)(1)(1)(0) = "86"
        dd(1)(1)(1)(1) = "87"

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
