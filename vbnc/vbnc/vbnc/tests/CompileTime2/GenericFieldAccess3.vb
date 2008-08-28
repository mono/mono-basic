Imports System
Imports System.Collections
Imports System.Reflection

Namespace GenericFieldAccess3
    Class Base(Of T)
        Public Value As T
    End Class

    Class Test
        Shared Function Main() As Integer
            Dim c As New Base(Of String)
            c.Value = ""
        End Function
    End Class
End Namespace