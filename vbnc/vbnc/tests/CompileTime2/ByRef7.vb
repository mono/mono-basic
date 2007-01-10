Imports System
Imports System.Collections
Imports System.Reflection

Namespace ByRef7
    Class Test
        Shared Function Test(ByRef T As Object) As Integer
            t = CByte(2)
            Return 0
        End Function
        Shared Function Main() As Integer
            Return test(Nothing)
        End Function
    End Class
End Namespace