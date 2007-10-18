Class InheritedSharedMethod1
    Class Base
        Shared Function Test() As Integer
            Return 0
        End Function
    End Class
    Class Derived
        Inherits base

    End Class

    Shared Function Main() As Integer
        Return Derived.Test()
    End Function
End Class