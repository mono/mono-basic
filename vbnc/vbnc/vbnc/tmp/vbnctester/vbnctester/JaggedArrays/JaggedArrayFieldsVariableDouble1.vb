Module JaggedArrayFieldsVariableDouble1
    Dim a As Double()
    Dim b As Double()()
    Dim c As Double()()()
    Dim d As Double()()()()
    Dim aa() As Double
    Dim bb()() As Double
    Dim cc()()() As Double
    Dim dd()()()() As Double

    Function Main() As Int32
        Dim result As Int32

        a = New Double() {}
        b = New Double()() {}
        c = New Double()()() {}
        d = New Double()()()() {}

        aa = New Double() {}
        bb = New Double()() {}
        cc = New Double()()() {}
        dd = New Double()()()() {}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 0 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 0 Then result += JaggedArrayVerifier.Report()


        a = New Double() {}
        b = New Double()() {New Double() {}}
        c = New Double()()() {New Double()() {New Double() {}}}
        d = New Double()()()() {New Double()()() {New Double()() {New Double() {}}}}

        aa = New Double() {}
        bb = New Double()() {New Double() {}}
        cc = New Double()()() {New Double()() {New Double() {}}}
        dd = New Double()()()() {New Double()()() {New Double()() {New Double() {}}}}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 1 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 1 Then result += JaggedArrayVerifier.Report()

        a = New Double() {1#, 2#}
        b = New Double()() {New Double() {10#, 11#}, New Double() {12#, 13#}}
        c = New Double()()() {New Double()() {New Double() {20#, 21#}, New Double() {22#, 23#}}, New Double()() {New Double() {24#, 25#}, New Double() {26#, 27#}}}
        d = New Double()()()() {New Double()()() {New Double()() {New Double() {30#, 31#}, New Double() {32#, 33#}}, New Double()() {New Double() {34#, 35#}, New Double() {36#, 37#}}}, New Double()()() {New Double()() {New Double() {40#, 41#}, New Double() {42#, 43#}}, New Double()() {New Double() {44#, 45#}, New Double() {46#, 47#}}}}

        aa = New Double() {1#, 2#}
        bb = New Double()() {New Double() {10#, 11#}, New Double() {12#, 13#}}
        cc = New Double()()() {New Double()() {New Double() {20#, 21#}, New Double() {22#, 23#}}, New Double()() {New Double() {24#, 25#}, New Double() {26#, 27#}}}
        dd = New Double()()()() {New Double()()() {New Double()() {New Double() {30#, 31#}, New Double() {32#, 33#}}, New Double()() {New Double() {34#, 35#}, New Double() {36#, 37#}}}, New Double()()() {New Double()() {New Double() {40#, 41#}, New Double() {42#, 43#}}, New Double()() {New Double() {44#, 45#}, New Double() {46#, 47#}}}}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 2 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 2 Then result += JaggedArrayVerifier.Report()

        a = New Double() {51#, 52#}
        b = New Double()() {New Double() {50#, 51#}, New Double() {52#, 53#}}
        c = New Double()()() {New Double()() {New Double() {60#, 61#}, New Double() {62#, 63#}}, New Double()() {New Double() {64#, 65#}, New Double() {66#, 67#}}}
        d = New Double()()()() {New Double()()() {New Double()() {New Double() {70#, 71#}, New Double() {72#, 73#}}, New Double()() {New Double() {74#, 75#}, New Double() {76#, 77#}}}, New Double()()() {New Double()() {New Double() {80#, 81#}, New Double() {82#, 83#}}, New Double()() {New Double() {84#, 85#}, New Double() {86#, 87#}}}}

        aa(0) = 51#
        aa(1) = 52#
        bb(0)(0) = 50#
        bb(0)(1) = 51#
        bb(1)(0) = 52#
        bb(1)(1) = 53#
        cc(0)(0)(0) = 60#
        cc(0)(0)(1) = 61#
        cc(0)(1)(0) = 62#
        cc(0)(1)(1) = 63#
        cc(1)(0)(0) = 64#
        cc(1)(0)(1) = 65#
        cc(1)(1)(0) = 66#
        cc(1)(1)(1) = 67#

        dd(0)(0)(0)(0) = 70#
        dd(0)(0)(0)(1) = 71#
        dd(0)(0)(1)(0) = 72#
        dd(0)(0)(1)(1) = 73#
        dd(0)(1)(0)(0) = 74#
        dd(0)(1)(0)(1) = 75#
        dd(0)(1)(1)(0) = 76#
        dd(0)(1)(1)(1) = 77#

        dd(1)(0)(0)(0) = 80#
        dd(1)(0)(0)(1) = 81#
        dd(1)(0)(1)(0) = 82#
        dd(1)(0)(1)(1) = 83#
        dd(1)(1)(0)(0) = 84#
        dd(1)(1)(0)(1) = 85#
        dd(1)(1)(1)(0) = 86#
        dd(1)(1)(1)(1) = 87#

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
