Public Module JaggedArrayPropertyVariableDouble1
    Dim _a As Double()
    Dim _b As Double()()
    Dim _c As Double()()()
    Dim _d As Double()()()()
    Dim _aa() As Double
    Dim _bb()() As Double
    Dim _cc()()() As Double
    Dim _dd()()()() As Double

    Property a() As Double()
        Get
            Return _a
        End Get
        Set(ByVal value As Double())
            _a = value
        End Set
    End Property

    Property b() As Double()()
        Get
            Return _b
        End Get
        Set(ByVal value As Double()())
            _b = value
        End Set
    End Property

    Property c() As Double()()()
        Get
            Return _c
        End Get
        Set(ByVal value As Double()()())
            _c = value
        End Set
    End Property

    Property d() As Double()()()()
        Get
            Return _d
        End Get
        Set(ByVal value As Double()()()())
            _d = value
        End Set
    End Property

    Property aa() As Double()
        Get
            Return _aa
        End Get
        Set(ByVal value As Double())
            _aa = value
        End Set
    End Property

    Property bb() As Double()()
        Get
            Return _bb
        End Get
        Set(ByVal value As Double()())
            _bb = value
        End Set
    End Property

    Property cc() As Double()()()
        Get
            Return _cc
        End Get
        Set(ByVal value As Double()()())
            _cc = value
        End Set
    End Property

    Property dd() As Double()()()()
        Get
            Return _dd
        End Get
        Set(ByVal value As Double()()()())
            _dd = value
        End Set
    End Property

    Function Main() As Int32
        Dim result As Int32
        _a = New Double() {}
        _b = New Double()() {}
        _c = New Double()()() {}
        _d = New Double()()()() {}

        _aa = New Double() {}
        _bb = New Double()() {}
        _cc = New Double()()() {}
        _dd = New Double()()()() {}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 0 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 0 Then result += JaggedArrayVerifier.Report()


        _a = New Double() {}
        _b = New Double()() {New Double() {}}
        _c = New Double()()() {New Double()() {New Double() {}}}
        _d = New Double()()()() {New Double()()() {New Double()() {New Double() {}}}}

        _aa = New Double() {}
        _bb = New Double()() {New Double() {}}
        _cc = New Double()()() {New Double()() {New Double() {}}}
        _dd = New Double()()()() {New Double()()() {New Double()() {New Double() {}}}}

        result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 1 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 0 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 1 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 1 Then result += JaggedArrayVerifier.Report()

        _a = New Double() {1#, 2#}
        _b = New Double()() {New Double() {10#, 11#}, New Double() {12#, 13#}}
        _c = New Double()()() {New Double()() {New Double() {20#, 21#}, New Double() {22#, 23#}}, New Double()() {New Double() {24#, 25#}, New Double() {26#, 27#}}}
        _d = New Double()()()() {New Double()()() {New Double()() {New Double() {30#, 31#}, New Double() {32#, 33#}}, New Double()() {New Double() {34#, 35#}, New Double() {36#, 37#}}}, New Double()()() {New Double()() {New Double() {40#, 41#}, New Double() {42#, 43#}}, New Double()() {New Double() {44#, 45#}, New Double() {46#, 47#}}}}

        _aa = New Double() {1#, 2#}
        _bb = New Double()() {New Double() {10#, 11#}, New Double() {12#, 13#}}
        _cc = New Double()()() {New Double()() {New Double() {20#, 21#}, New Double() {22#, 23#}}, New Double()() {New Double() {24#, 25#}, New Double() {26#, 27#}}}
        _dd = New Double()()()() {New Double()()() {New Double()() {New Double() {30#, 31#}, New Double() {32#, 33#}}, New Double()() {New Double() {34#, 35#}, New Double() {36#, 37#}}}, New Double()()() {New Double()() {New Double() {40#, 41#}, New Double() {42#, 43#}}, New Double()() {New Double() {44#, 45#}, New Double() {46#, 47#}}}}

result += JaggedArrayVerifier.Verify(a, b, c, d, aa, bb, cc, dd)

        If a.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If b.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If c.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If d.Length <> 2 Then result += JaggedArrayVerifier.Report()

        If aa.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If bb.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If cc.Length <> 2 Then result += JaggedArrayVerifier.Report()
        If dd.Length <> 2 Then result += JaggedArrayVerifier.Report()

        _a = New Double() {51#, 52#}
        _b = New Double()() {New Double() {50#, 51#}, New Double() {52#, 53#}}
        _c = New Double()()() {New Double()() {New Double() {60#, 61#}, New Double() {62#, 63#}}, New Double()() {New Double() {64#, 65#}, New Double() {66#, 67#}}}
        _d = New Double()()()() {New Double()()() {New Double()() {New Double() {70#, 71#}, New Double() {72#, 73#}}, New Double()() {New Double() {74#, 75#}, New Double() {76#, 77#}}}, New Double()()() {New Double()() {New Double() {80#, 81#}, New Double() {82#, 83#}}, New Double()() {New Double() {84#, 85#}, New Double() {86#, 87#}}}}

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
