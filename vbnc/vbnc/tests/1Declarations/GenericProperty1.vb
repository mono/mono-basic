Class GenericProperty1
    Class Gen(Of T)
        Default ReadOnly Property Value(ByVal index As Integer) As T
            Get
                Return CType(CObj("laksdhf"), T)
            End Get
        End Property
    End Class

    Shared Function Main() As Integer
        Dim a As New gen(Of String)
        Dim d As String = a(0).substring(0, 1)
        Return 0
    End Function
End Class