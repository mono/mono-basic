Imports System
Imports System.Collections
Imports System.Reflection

Namespace GenericFieldAccess5
    Class Base(Of T)
        Public Value As T
        Public Value2 As T()
    End Class

    Class Test
        Shared Function Main() As Integer
            Dim c As New Base(Of String)
            c.Value2 = Nothing
        End Function
    End Class
End Namespace