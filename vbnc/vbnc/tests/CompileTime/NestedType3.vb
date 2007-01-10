Class NestedType3
    Private Class Nested
        Public Value As Integer
        Sub New()
            Value = 1
        End Sub
    End Class

    Function Main() As Integer
        Dim n As New NestedType3.Nested()
        Return n.Value - 1
    End Function
End Class
