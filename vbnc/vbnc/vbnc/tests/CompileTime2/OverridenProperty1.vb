Class OverridenProperty1
    Class Base
        Overridable ReadOnly Property Test() As Integer
            Get
                Return 1
            End Get
        End Property
    End Class
    Class Derived
        Inherits base
        Overrides ReadOnly Property Test() As Integer
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