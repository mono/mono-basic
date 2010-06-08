Class DelegateCreationExpression2
    Delegate Sub DoubleFunc(ByVal x As Double)

    Sub A()

    End Sub
    Sub A(ByVal x As Double)

    End Sub

    Sub Test()
        Dim d As doublefunc = New doublefunc(AddressOf A)
    End Sub
End Class