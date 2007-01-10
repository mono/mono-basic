Imports System
Imports System.Collections
Imports System.Reflection

Namespace GenericBaseMethod1
    Class Base
        Function SomeMethod() As Integer
            Return 0
        End Function
    End Class
    Class Derived1(Of T)
        Inherits Base

    End Class
    Class Derived2(Of T)
        Inherits Derived1(Of T)

    End Class
    Class Derived3
        Inherits Derived2(Of String)

    End Class

    Class Test
        Shared Function Main() As Integer
            Dim t As New Derived3
            Return t.SomeMethod
        End Function
    End Class
End Namespace