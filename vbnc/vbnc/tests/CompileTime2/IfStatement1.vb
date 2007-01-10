Class IfStatement1
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
        Dim t As New IfStatement1
        If True Then
            Return t.test(Of GenericArgument)(c).IntMethod()
        ElseIf False = False Then
            Return t.test(Of GenericArgument)(c).IntMethod()
        Else
            Return t.test(Of GenericArgument)(c).IntMethod()
        End If
    End Function
End Class