Class AddressOf1
    Delegate Sub a(ByVal b As Char)
    Sub Test()
        Dim d As a
        d = AddressOf test
    End Sub
    Sub test(ByVal a As String)

    End Sub
End Class