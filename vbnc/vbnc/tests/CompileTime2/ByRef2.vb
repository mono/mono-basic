Class ByRef2
    Shared Function Main() As Integer
        Dim testvalue As Integer
        test(testvalue)
        If testvalue <> 1 Then Return 1
    End Function
    Shared Sub test(ByRef value As Integer)
        value = value + 1
    End Sub
End Class