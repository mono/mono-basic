Public Module JaggedArrayVerifier
    Function Verify(ByVal a As Integer(), ByVal b As Integer()(), ByVal c As Integer()()(), ByVal d As Integer()()()(), ByVal aa() As Integer, ByVal bb()() As Integer, ByVal cc()()() As Integer, ByVal dd()()()() As Integer) As Int32
        Dim result As Int32

        If UBound(a) <> UBound(aa) Then result += Report("UBound call failed.")
        If UBound(a, 1) <> UBound(aa, 1) Then result += Report("UBound call failed.")
        If UBound(b) <> UBound(bb) Then result += Report("UBound call failed.")
        If UBound(b, 1) <> UBound(bb, 1) Then result += Report("UBound call failed.")
        If UBound(c) <> UBound(cc) Then result += Report("UBound call failed.")
        If UBound(c, 1) <> UBound(cc, 1) Then result += Report("UBound call failed.")
        If UBound(d) <> UBound(dd) Then result += Report("UBound call failed.")
        If UBound(d, 1) <> UBound(dd, 1) Then result += Report("UBound call failed.")

        For i As Int32 = 0 To UBound(b)
            If UBound(b(i)) <> UBound(bb(i)) Then result += Report("UBound call failed.")
        Next
        For i As Int32 = 0 To UBound(c)
            If UBound(c(i)) <> UBound(cc(i)) Then result += Report("UBound call failed.")
            For j As Int32 = 0 To UBound(c(i))
                If UBound(c(i)(j)) <> UBound(cc(i)(j)) Then result += Report("UBound call failed.")
            Next
        Next
        For i As Int32 = 0 To UBound(d)
            If UBound(d(i)) <> UBound(dd(i)) Then result += Report("UBound call failed.")
            For j As Int32 = 0 To UBound(d(i))
                If UBound(d(i)(j)) <> UBound(dd(i)(j)) Then result += Report("UBound call failed.")
                For k As Int32 = 0 To UBound(d(i)(j))
                    If UBound(d(i)(j)(k)) <> UBound(dd(i)(j)(k)) Then result += Report("UBound call failed.")
                Next
            Next
        Next

        If a.Length <> aa.Length Then result += Report("Array.Length call failed.")
        If b.Length <> bb.Length Then result += Report("Array.Length call failed.")
        If c.Length <> cc.Length Then result += Report("Array.Length call failed.")
        If d.Length <> dd.Length Then result += Report("Array.Length call failed.")

        If a.Rank <> aa.Rank Then result += Report("Array.Rank call failed.")
        If b.Rank <> bb.Rank Then result += Report("Array.Rank call failed.")
        If c.Rank <> cc.Rank Then result += Report("Array.Rank call failed.")
        If d.Rank <> dd.Rank Then result += Report("Array.Rank call failed.")


        For i As Int32 = 0 To UBound(a)
            If a(i) <> aa(i) Then result += ArrayVerifier.Report()
        Next

        For i As Int32 = 0 To UBound(b)
            For j As Int32 = 0 To UBound(b(i))
                If b(i)(j) <> bb(i)(j) Then result += ArrayVerifier.Report()
            Next
        Next

        For i As Int32 = 0 To UBound(c)
            For j As Int32 = 0 To UBound(c(i))
                For k As Int32 = 0 To UBound(c(i)(j))
                    If c(i)(j)(k) <> cc(i)(j)(k) Then result += ArrayVerifier.Report()
                Next
            Next
        Next

        For i As Int32 = 0 To UBound(d)
            For j As Int32 = 0 To UBound(d(i))
                For k As Int32 = 0 To UBound(d(i)(j))
                    For l As Int32 = 0 To UBound(d(i)(j)(k))
                        If d(i)(j)(k)(l) <> d(i)(j)(k)(l) Then result += ArrayVerifier.Report()
                    Next
                Next
            Next
        Next


        Return result
    End Function

    Function Report(Optional ByVal Message As String = "(detailed message)") As Int32
        Debug.WriteLine("FAIL ArrayFieldsVariable1")
        Debug.WriteLine(Message)
        Return 1
    End Function
End Module
