Imports System
Imports System.Collections
Imports System.Reflection

Namespace Operator1
    Class Test
        Shared Function Main() As Integer
            Dim a, b As Date
            Dim c As timespan
            c = a - b
            Return CInt(c.Ticks)
        End Function
    End Class
End Namespace