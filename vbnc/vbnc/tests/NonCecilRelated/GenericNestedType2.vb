Imports System.Collections

Public Class TestList
    Dim z As a1.b1(Of Integer).c1(Of Long)
    Private m_List As Generic.Dictionary(Of Object, Object).ValueCollection
    Dim a As A1
    Dim b As a1.b1(Of Integer)
    Dim d As a1.b1(Of Integer).c1(Of Long)
    Dim e As a1.b2(Of Object, Object).c1(Of Object, Object)
    Dim f As a1.b1(Of Object).c2(Of Object, Object).d1
End Class

Class A1
    Class B1(Of Y)
        Class C1(Of X)

        End Class
        Class C2(Of X1, X2)
            Class D1

            End Class
            Class D2

            End Class
            Class D3

            End Class
        End Class
        Class C3(Of Y)

        End Class
    End Class
    Class B2(Of TKey, TValue)
        Class C1(Of TKey2, TValue)

        End Class
        Class C2
            Dim key As TKey
            Dim value As TValue
        End Class
    End Class
    Class B3

    End Class
End Class
