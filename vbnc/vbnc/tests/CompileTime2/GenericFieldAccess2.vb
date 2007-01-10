Imports System
Imports System.Collections
Imports System.Reflection

Namespace GenericFieldAccess2
    Class Base(Of T)
        ReadOnly Property Literal() As T
            Get

            End Get
        End Property

        Overrides Function ToString() As String
            Return Literal.ToString()
        End Function
    End Class

    Class Test
        Shared Function Main() As Integer

        End Function
    End Class
End Namespace