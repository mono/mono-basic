Imports System
Imports System.Collections
Imports System.Reflection

Namespace NothingValueType1
    Structure S
        Public i As Integer
    End Structure
    Class Test
        Function Test() As S
            Return Nothing
        End Function

        Shared Function Main() As Integer
            Return 0
        End Function
    End Class
End Namespace