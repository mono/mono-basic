Module Array1
    Dim a As Integer()
    Dim b As Integer()()
    Dim c As Integer()()()
    Dim d As Integer()()()()
    Dim aa() As Integer
    Dim bb()() As Integer
    Dim cc()()() As Integer
    Dim dd()()()() As Integer

    Function Report(Optional ByVal Message As String = "(detailed message") As Integer
        System.Diagnostics.Debug.WriteLine("FAIL Array5")
        System.Diagnostics.Debug.WriteLine(Message)
        Return 1
    End Function

    Function Main() As Integer
        Dim result As Integer = 0

        a = New Integer() {}
        b = New Integer()() {New Integer() {}}
        c = New Integer()()() {New Integer()() {New Integer() {}}}
        d = New Integer()()()() {New Integer()()() {New Integer()() {New Integer() {}}}}
        aa = New Integer() {}
        bb = New Integer()() {New Integer() {}}
        cc = New Integer()()() {New Integer()() {New Integer() {}}}
        dd = New Integer()()()() {New Integer()()() {New Integer()() {New Integer() {}}}}

        a = New Integer() {1}
        b = New Integer()() {New Integer() {2}}
        c = New Integer()()() {New Integer()() {New Integer() {3}}}
        d = New Integer()()()() {New Integer()()() {New Integer()() {New Integer() {4}}}}
        aa = New Integer() {5}
        bb = New Integer()() {New Integer() {6}}
        cc = New Integer()()() {New Integer()() {New Integer() {7}}}
        dd = New Integer()()()() {New Integer()()() {New Integer()() {New Integer() {8}}}}

        a(0) = 10
        If a(0) <> 10 Then result += Report()
        b(0) = a
        If b(0)(0) <> 10 Then result += Report()
        b(0)(0) = 20
        If b(0)(0) <> 20 Then result += Report()
        c(0) = b
        If c(0)(0)(0) <> 20 Then result += Report()
        c(0)(0) = a
        If c(0)(0)(0) <> 20 Then result += Report()
        c(0)(0)(0) = 30
        If c(0)(0)(0) <> 30 Then result += Report()
        d(0) = c
        If d(0)(0)(0)(0) <> 30 Then result += Report()
        d(0)(0) = b
        If d(0)(0)(0)(0) <> 30 Then result += Report()
        d(0)(0)(0) = a
        If d(0)(0)(0)(0) <> 30 Then result += Report()
        d(0)(0)(0)(0) = 40
        If d(0)(0)(0)(0) <> 40 Then result += Report()

        aa(0) = 10
        If aa(0) <> 10 Then result += Report()
        bb(0) = aa
        If bb(0)(0) <> 10 Then result += Report()
        bb(0)(0) = 20
        If bb(0)(0) <> 20 Then result += Report()
        cc(0) = bb
        If cc(0)(0)(0) <> 20 Then result += Report()
        cc(0)(0) = aa
        If cc(0)(0)(0) <> 20 Then result += Report()
        cc(0)(0)(0) = 30
        If cc(0)(0)(0) <> 30 Then result += Report()
        dd(0) = cc
        If dd(0)(0)(0)(0) <> 30 Then result += Report()
        dd(0)(0) = bb
        If dd(0)(0)(0)(0) <> 30 Then result += Report()
        dd(0)(0)(0) = aa
        If dd(0)(0)(0)(0) <> 30 Then result += Report()
        dd(0)(0)(0)(0) = 40
        If dd(0)(0)(0)(0) <> 40 Then result += Report()

        Return result
    End Function
End Module
