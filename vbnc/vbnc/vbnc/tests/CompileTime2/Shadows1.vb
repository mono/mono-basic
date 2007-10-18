Namespace Shadows1
    Class Base
        Function Test() As Integer
            Return 1
        End Function
    End Class

    Class Derived
        Shadows Function Test() As Integer
            Return 0
        End Function
    End Class

    Class Consumer
        Shared Function Main() As Integer
            Dim d As New Derived
            Return d.Test
        End Function
    End Class
End Namespace