Class TypeParameterExpression2

    Class Constraint
        Function Method() As Integer
            Return 0
        End Function
    End Class

    Shared Function Method(Of T As Constraint)(ByVal Param As T) As Integer
        Return Param.method()
    End Function

    Shared Function Main() As Integer
        Dim c As New Constraint
        Return method(Of Constraint)(c)
    End Function
End Class