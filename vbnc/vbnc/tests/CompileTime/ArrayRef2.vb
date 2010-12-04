Class P

End Class

Class ArrayRef2
    Private Sub C(ByRef arr() As Integer, ByRef B As Byte, ByRef SB As SByte, ByRef S As Short, ByRef US As UShort, ByRef I As Integer, ByRef UI As UInteger, ByRef L As Long, ByRef UL As ULong, ByRef D As Decimal, ByRef F As Single, ByRef R As Double)
        arr(B) = 2
        arr(sb) = 2
        arr(s) = 2
        arr(us) = 2
        arr(i) = 2
        arr(ui) = 2
        arr(l) = 2
        arr(ul) = 2
        arr(d) = 2
        arr(f) = 2
        arr(r) = 2
    End Sub

    Shared Function Main() As Integer

    End Function
End Class