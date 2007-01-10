Class DictionaryLookupExpression1
	Sub Test1()
        Dim i As DictionaryLookupExpression1
		Dim j As Object
		j = i!Test
    End Sub
    Default ReadOnly Property Prop(ByVal idx As String) As Object
        Get
            Return Nothing
        End Get
    End Property
End Class