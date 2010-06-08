Imports System
Imports System.Collections
Imports System.Reflection

Namespace ByRefAssignment1

    Class Test
        Shared Function Test(ByRef Value As Decimal) As Integer
            value = New Decimal(1)
            'value = tmp
            Return 0
        End Function
        Shared Function Main() As Integer
            Return Test(2)
        End Function
    End Class
End Namespace