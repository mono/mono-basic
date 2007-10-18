Class ArrayCreationExpression3
    Sub Test()
        Dim t(,) As Integer
        t = New Integer(1, 2) {{1, 2, 3}, {4, 5, 6}}
    End Sub
End Class