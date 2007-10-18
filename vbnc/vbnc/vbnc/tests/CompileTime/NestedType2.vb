Partial Class NestedType2
    Class Nested
        Public Value As Integer
        Sub New()
            Value = 1
        End Sub
    End Class
End Class

Partial Class NestedType2

    Function Main() As Integer
        Dim n As New NestedType2.Nested()
        Return n.Value - 1
    End Function
End Class