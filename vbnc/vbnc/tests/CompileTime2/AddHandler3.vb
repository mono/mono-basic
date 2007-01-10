Class AddHandler3
    Shared Event a As aa
    Delegate Sub aa()

    Public Shared Value As Integer

    Shared Sub Test()
        AddHandler a, New aa(AddressOf Tester)
        RaiseEvent a()
    End Sub

    Shared Sub Tester()
        value = 1
    End Sub
    Shared Function Main() As Integer
        test()
        Return value - 1
    End Function
End Class