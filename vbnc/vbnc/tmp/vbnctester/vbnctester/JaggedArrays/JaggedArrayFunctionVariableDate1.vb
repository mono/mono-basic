Public Module JaggedArrayFunctionVariableDate1
    Dim _a As Date()
    Dim _b As Date()()
    Dim _c As Date()()()
    Dim _d As Date()()()()
    Dim _aa() As Date
    Dim _bb()() As Date
    Dim _cc()()() As Date
    Dim _dd()()()() As Date

    Function a() As Date()
        Return _a
    End Function

    Function b() As Date()()
        Return _b
    End Function

    Function c() As Date()()()
        Return _c
    End Function

    Function d() As Date()()()()
        Return _d
    End Function

    Function aa() As Date()
        Return _aa
    End Function

    Function bb() As Date()()
        Return _bb
    End Function

    Function cc() As Date()()()
        Return _cc
    End Function

    Function dd() As Date()()()()
        Return _dd
    End Function

    Function Main() As Int32
        Dim result As Int32

        _a = New Date() {}
        _b = New Date()() {}
        _c = New Date()()() {}
        _d = New Date()()()() {}

        _aa = New Date() {}
        _bb = New Date()() {}
        _cc = New Date()()() {}
        _dd = New Date()()()() {}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 0 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 0 Then result += JaggedArrayVerifier.Report()


        _a = New Date() {}
        _b = New Date()() {New Date() {}}
        _c = New Date()()() {New Date()() {New Date() {}}}
        _d = New Date()()()() {New Date()()() {New Date()() {New Date() {}}}}

        _aa = New Date() {}
        _bb = New Date()() {New Date() {}}
        _cc = New Date()()() {New Date()() {New Date() {}}}
        _dd = New Date()()()() {New Date()()() {New Date()() {New Date() {}}}}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 1 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 1 Then result += JaggedArrayVerifier.Report()

        _a = New Date() {#1/1/1#, #1/1/2#}
        _b = New Date()() {New Date() {#1/1/10#, #1/1/11#}, New Date() {#1/1/12#, #1/1/13#}}
        _c = New Date()()() {New Date()() {New Date() {#1/1/20#, #1/1/21#}, New Date() {#1/1/22#, #1/1/23#}}, New Date()() {New Date() {#1/1/24#, #1/1/25#}, New Date() {#1/1/26#, #1/1/27#}}}
        _d = New Date()()()() {New Date()()() {New Date()() {New Date() {#1/1/30#, #1/1/31#}, New Date() {#1/1/32#, #1/1/33#}}, New Date()() {New Date() {#1/1/34#, #1/1/35#}, New Date() {#1/1/36#, #1/1/37#}}}, New Date()()() {New Date()() {New Date() {#1/1/40#, #1/1/41#}, New Date() {#1/1/42#, #1/1/43#}}, New Date()() {New Date() {#1/1/44#, #1/1/45#}, New Date() {#1/1/46#, #1/1/47#}}}}

        _aa = New Date() {#1/1/1#, #1/1/2#}
        _bb = New Date()() {New Date() {#1/1/10#, #1/1/11#}, New Date() {#1/1/12#, #1/1/13#}}
        _cc = New Date()()() {New Date()() {New Date() {#1/1/20#, #1/1/21#}, New Date() {#1/1/22#, #1/1/23#}}, New Date()() {New Date() {#1/1/24#, #1/1/25#}, New Date() {#1/1/26#, #1/1/27#}}}
        _dd = New Date()()()() {New Date()()() {New Date()() {New Date() {#1/1/30#, #1/1/31#}, New Date() {#1/1/32#, #1/1/33#}}, New Date()() {New Date() {#1/1/34#, #1/1/35#}, New Date() {#1/1/36#, #1/1/37#}}}, New Date()()() {New Date()() {New Date() {#1/1/40#, #1/1/41#}, New Date() {#1/1/42#, #1/1/43#}}, New Date()() {New Date() {#1/1/44#, #1/1/45#}, New Date() {#1/1/46#, #1/1/47#}}}}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 2 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 2 Then result += JaggedArrayVerifier.Report()

        _a = New Date() {#1/1/51#, #1/1/52#}
        _b = New Date()() {New Date() {#1/1/50#, #1/1/51#}, New Date() {#1/1/52#, #1/1/53#}}
        _c = New Date()()() {New Date()() {New Date() {#1/1/60#, #1/1/61#}, New Date() {#1/1/62#, #1/1/63#}}, New Date()() {New Date() {#1/1/64#, #1/1/65#}, New Date() {#1/1/66#, #1/1/67#}}}
        _d = New Date()()()() {New Date()()() {New Date()() {New Date() {#1/1/70#, #1/1/71#}, New Date() {#1/1/72#, #1/1/73#}}, New Date()() {New Date() {#1/1/74#, #1/1/75#}, New Date() {#1/1/76#, #1/1/77#}}}, New Date()()() {New Date()() {New Date() {#1/1/80#, #1/1/81#}, New Date() {#1/1/82#, #1/1/83#}}, New Date()() {New Date() {#1/1/84#, #1/1/85#}, New Date() {#1/1/86#, #1/1/87#}}}}

        aa(0) = #1/1/51#
        aa(1) = #1/1/52#
        bb(0)(0) = #1/1/50#
        bb(0)(1) = #1/1/51#
        bb(1)(0) = #1/1/52#
        bb(1)(1) = #1/1/53#
        cc(0)(0)(0) = #1/1/60#
        cc(0)(0)(1) = #1/1/61#
        cc(0)(1)(0) = #1/1/62#
        cc(0)(1)(1) = #1/1/63#
        cc(1)(0)(0) = #1/1/64#
        cc(1)(0)(1) = #1/1/65#
        cc(1)(1)(0) = #1/1/66#
        cc(1)(1)(1) = #1/1/67#

        dd(0)(0)(0)(0) = #1/1/70#
        dd(0)(0)(0)(1) = #1/1/71#
        dd(0)(0)(1)(0) = #1/1/72#
        dd(0)(0)(1)(1) = #1/1/73#
        dd(0)(1)(0)(0) = #1/1/74#
        dd(0)(1)(0)(1) = #1/1/75#
        dd(0)(1)(1)(0) = #1/1/76#
        dd(0)(1)(1)(1) = #1/1/77#

        dd(1)(0)(0)(0) = #1/1/80#
        dd(1)(0)(0)(1) = #1/1/81#
        dd(1)(0)(1)(0) = #1/1/82#
        dd(1)(0)(1)(1) = #1/1/83#
        dd(1)(1)(0)(0) = #1/1/84#
        dd(1)(1)(0)(1) = #1/1/85#
        dd(1)(1)(1)(0) = #1/1/86#
        dd(1)(1)(1)(1) = #1/1/87#

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
