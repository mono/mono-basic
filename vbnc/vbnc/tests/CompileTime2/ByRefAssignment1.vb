Imports System
Imports System.Collections
Imports System.Reflection

Namespace ByRefAssignment1

    Class Test
        Shared Function Test(ByRef Value As Integer) As Integer
            Dim tmp As Integer
            value = 1
            value = tmp
            Return value
        End Function
        Shared Function Main() As Integer
            Return Test(2)
        End Function
    End Class
End Namespace