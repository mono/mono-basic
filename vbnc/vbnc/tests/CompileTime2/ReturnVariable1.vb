Imports System
Imports System.Collections
Imports System.Reflection

Namespace ReturnVariable1
    Class Test
        Shared Function Main() As Integer
            Return Main
        End Function
    End Class
End Namespace