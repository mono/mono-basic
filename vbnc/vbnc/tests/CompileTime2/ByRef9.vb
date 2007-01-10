Imports System
Imports System.Collections
Imports System.Reflection

Namespace ByRef9
    Class Test
        Shared Function BR1(ByRef value As Boolean) As Integer
            Dim result As Boolean
            result = value = True
            Return CInt(result)
        End Function
        Shared Function Main() As Integer
            Return br1(False)
        End Function
    End Class
End Namespace