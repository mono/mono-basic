Class TypeParameterExpression1
    Shared Function Main() As Integer
        Return Test(Of Integer)(0)
    End Function

    Shared Function Test(Of T)(ByVal value As T) As T
        Return value
    End Function
End Class