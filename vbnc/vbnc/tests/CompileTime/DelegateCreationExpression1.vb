Class DelegateCreationExpression1
    Delegate Function DoubleFunc(ByVal x As Double) As Double

    Sub Test()
        Dim d As doublefunc = New doublefunc(AddressOf Square)
    End Sub
    Overloads Shared Function Square(ByVal x As Double) As Double

    End Function
    Overloads Shared Function Square(ByVal x As Single) As Single

    End Function
End Class