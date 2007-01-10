Class ShadowedProperty1
    Class Base
        ReadOnly Property Test() As Integer
            Get
                Return 1
            End Get
        End Property
    End Class
    Class Derived
        Inherits base
        Shadows ReadOnly Property Test() As Integer
            Get
                Return 0
            End Get
        End Property
    End Class

    Shared Function Main() As Integer
        Dim d As New derived
        Return d.test
    End Function
End Class