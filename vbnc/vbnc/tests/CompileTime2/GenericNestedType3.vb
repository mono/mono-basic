Imports System.Collections.Generic

Public Class Test
    ReadOnly Property Foo1 As Dictionary(Of String, String).ValueCollection
        Get
            Dim dic As Dictionary(Of String, String) = New Dictionary(Of String, String)
            Return dic.Values
        End Get
    End Property
End Class