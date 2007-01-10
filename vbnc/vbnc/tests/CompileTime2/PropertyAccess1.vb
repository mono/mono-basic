Imports System
Imports System.Collections
Imports System.Reflection

Namespace PropertyAccess1
    Class Test
        Private Shared m_Value As Integer
        Shared Property P() As Integer
            Get
                Return m_Value
            End Get
            Set(ByVal value As Integer)
                m_value = value
            End Set
        End Property
        Shared Function Main() As Integer
            p += 1
            Return p - 1
        End Function
    End Class
End Namespace