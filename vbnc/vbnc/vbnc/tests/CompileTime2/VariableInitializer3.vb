Class VariableInitializer3
    Class GenericArgument
        Function IntMethod() As Integer
            Return 0
        End Function
    End Class

    Function Test(Of T)(ByVal Param As T) As T
        Return Param
    End Function

    Shared Function Main() As Integer
        Dim c As New GenericArgument
        Dim t As New VariableInitializer3
        Dim i As Integer = t.test(Of GenericArgument)(c).IntMethod()
        Return i
    End Function
End Class
