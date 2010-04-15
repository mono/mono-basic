Class GenericNestedType1

    Class External(Of T)
        Class Nested
            Function Value(ByVal Param As T) As Integer
                Return 0
            End Function
        End Class
        Function Test() As Nested
            Return New Nested
        End Function
    End Class

    Shared Function Main() As Integer
        Dim v As New external(Of String)
        Return v.Test.value("")
    End Function
End Class