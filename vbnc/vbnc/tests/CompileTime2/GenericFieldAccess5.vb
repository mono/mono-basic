Imports System
Imports System.Collections
Imports System.Reflection

Namespace GenericFieldAccess5
    Class Base(Of T)
        Public Value As T
        Function M() As Integer
            Dim o As Object
            o = Value
            Return 0
        End Function
    End Class
    Class M
        Shared Function Main() As Integer
            Return 0
        End Function
    End Class
End Namespace