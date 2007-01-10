Imports System
Imports System.Collections
Imports System.Reflection

Namespace GenericArrayAccess1
    Class Test
        Shared Function CreateArray(Of T)(ByVal Value As T, ByVal Length As Integer) As T()
            Dim result(Length - 1) As T
            For i As Integer = 0 To Length - 1
                result(i) = Value
            Next
            Return result
        End Function
        Shared Function Main() As Integer

        End Function
    End Class
End Namespace