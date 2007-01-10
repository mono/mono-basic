Class MethodResolution
    Class Base

    End Class
    Class Derived
        Inherits base

    End Class

    Shared Function method(ByVal var As base) As Integer
        Return 1
    End Function
    Shared Function method(ByVal var As derived) As Integer
        Return 0
    End Function

    Shared Function Main() As Integer
        Return method(New derived)
    End Function
End Class