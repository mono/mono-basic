Public Module ArrayPropertyVariableString1
    Dim _a As String()
    Dim _b As String(,)
    Dim _c As String(,,)
    Dim _d As String(,,,)
    Dim _aa() As String
    Dim _bb(,) As String
    Dim _cc(,,) As String
    Dim _dd(,,,) As String

    Property a() As String()
        Get
            Return _a
        End Get
        Set(ByVal value As String())
            _a = value
        End Set
    End Property

    Property b() As String(,)
        Get
            Return _b
        End Get
        Set(ByVal value As String(,))
            _b = value
        End Set
    End Property

    Property c() As String(,,)
        Get
            Return _c
        End Get
        Set(ByVal value As String(,,))
            _c = value
        End Set
    End Property

    Property d() As String(,,,)
        Get
            Return _d
        End Get
        Set(ByVal value As String(,,,))
            _d = value
        End Set
    End Property
    Property aa() As String()
        Get
            Return _aa
        End Get
        Set(ByVal value As String())
            _aa = value
        End Set
    End Property

    Property bb() As String(,)
        Get
            Return _bb
        End Get
        Set(ByVal value As String(,))
            _bb = value
        End Set
    End Property

    Property cc() As String(,,)
        Get
            Return _cc
        End Get
        Set(ByVal value As String(,,))
            _cc = value
        End Set
    End Property

    Property dd() As String(,,,)
        Get
            Return _dd
        End Get
        Set(ByVal value As String(,,,))
            _dd = value
        End Set
    End Property

    Function Main() As Int32
        Dim result As Int32

        a = New String() {}
        b = New String(,) {}
        c = New String(,,) {}
        d = New String(,,,) {}

        aa = New String() {}
        bb = New String(,) {}
        cc = New String(,,) {}
        dd = New String(,,,) {}

        result += ArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += ArrayVerifier.Report()
        If b.Length <> 0 Then result += ArrayVerifier.Report()
        If c.Length <> 0 Then result += ArrayVerifier.Report()
        If d.Length <> 0 Then result += ArrayVerifier.Report()

        If aa.Length <> 0 Then result += ArrayVerifier.Report()
        If bb.Length <> 0 Then result += ArrayVerifier.Report()
        If cc.Length <> 0 Then result += ArrayVerifier.Report()
        If dd.Length <> 0 Then result += ArrayVerifier.Report()

        a = New String() {}
        b = New String(,) {{}}
        c = New String(,,) {{{}}}
        d = New String(,,,) {{{{}}}}

        aa = New String() {}
        bb = New String(,) {{}}
        cc = New String(,,) {{{}}}
        dd = New String(,,,) {{{{}}}}

        result += ArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += ArrayVerifier.Report()
        If b.Length <> 0 Then result += ArrayVerifier.Report()
        If c.Length <> 0 Then result += ArrayVerifier.Report()
        If d.Length <> 0 Then result += ArrayVerifier.Report()

        If aa.Length <> 0 Then result += ArrayVerifier.Report()
        If bb.Length <> 0 Then result += ArrayVerifier.Report()
        If cc.Length <> 0 Then result += ArrayVerifier.Report()
        If dd.Length <> 0 Then result += ArrayVerifier.Report()


        a = New String() {"1", "2"}
        b = New String(,) {{"10", "11"}, {"12", "13"}}
        c = New String(,,) {{{"20", "21"}, {"22", "23"}}, {{"24", "25"}, {"26", "27"}}}
        d = New String(,,,) {{{{"30", "31"}, {"32", "33"}}, {{"34", "35"}, {"36", "37"}}}, {{{"40", "41"}, {"42", "43"}}, {{"44", "45"}, {"46", "47"}}}}

        aa = New String() {"1", "2"}
        bb = New String(,) {{"10", "11"}, {"12", "13"}}
        cc = New String(,,) {{{"20", "21"}, {"22", "23"}}, {{"24", "25"}, {"26", "27"}}}
        dd = New String(,,,) {{{{"30", "31"}, {"32", "33"}}, {{"34", "35"}, {"36", "37"}}}, {{{"40", "41"}, {"42", "43"}}, {{"44", "45"}, {"46", "47"}}}}

        result += ArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 2 Then result += ArrayVerifier.Report()
        If b.Length <> 4 Then result += ArrayVerifier.Report()
        If c.Length <> 8 Then result += ArrayVerifier.Report()
        If d.Length <> 16 Then result += ArrayVerifier.Report()

        If aa.Length <> 2 Then result += ArrayVerifier.Report()
        If bb.Length <> 4 Then result += ArrayVerifier.Report()
        If cc.Length <> 8 Then result += ArrayVerifier.Report()
        If dd.Length <> 16 Then result += ArrayVerifier.Report()

        a = New String() {"51", "52"}
        b = New String(,) {{"50", "51"}, {"52", "53"}}
        c = New String(,,) {{{"60", "61"}, {"62", "63"}}, {{"64", "65"}, {"66", "67"}}}
        d = New String(,,,) {{{{"70", "71"}, {"72", "73"}}, {{"74", "75"}, {"76", "77"}}}, {{{"80", "81"}, {"82", "83"}}, {{"84", "85"}, {"86", "87"}}}}

        aa(0) = "51"
        aa(1) = "52"
        bb(0, 0) = "50"
        bb(0, 1) = "51"
        bb(1, 0) = "52"
        bb(1, 1) = "53"
        cc(0, 0, 0) = "60"
        cc(0, 0, 1) = "61"
        cc(0, 1, 0) = "62"
        cc(0, 1, 1) = "63"
        cc(1, 0, 0) = "64"
        cc(1, 0, 1) = "65"
        cc(1, 1, 0) = "66"
        cc(1, 1, 1) = "67"

        dd(0, 0, 0, 0) = "70"
        dd(0, 0, 0, 1) = "71"
        dd(0, 0, 1, 0) = "72"
        dd(0, 0, 1, 1) = "73"
        dd(0, 1, 0, 0) = "74"
        dd(0, 1, 0, 1) = "75"
        dd(0, 1, 1, 0) = "76"
        dd(0, 1, 1, 1) = "77"

        dd(1, 0, 0, 0) = "80"
        dd(1, 0, 0, 1) = "81"
        dd(1, 0, 1, 0) = "82"
        dd(1, 0, 1, 1) = "83"
        dd(1, 1, 0, 0) = "84"
        dd(1, 1, 0, 1) = "85"
        dd(1, 1, 1, 0) = "86"
        dd(1, 1, 1, 1) = "87"

        result += ArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 2 Then result += ArrayVerifier.Report()
        If b.Length <> 4 Then result += ArrayVerifier.Report()
        If c.Length <> 8 Then result += ArrayVerifier.Report()
        If d.Length <> 16 Then result += ArrayVerifier.Report()

        If aa.Length <> 2 Then result += ArrayVerifier.Report()
        If bb.Length <> 4 Then result += ArrayVerifier.Report()
        If cc.Length <> 8 Then result += ArrayVerifier.Report()
        If dd.Length <> 16 Then result += ArrayVerifier.Report()

        Return result
    End Function

End Module
