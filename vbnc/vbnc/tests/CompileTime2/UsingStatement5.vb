Class UsingStatement5
    Class GenericArgument
        Implements System.IDisposable

        Function IntMethod() As Integer
            Return 0
        End Function

        Sub Dispose() Implements System.IDisposable.Dispose

        End Sub
    End Class

    Function Test(Of T)(ByVal Param As T) As T
        Return Param
    End Function

    Shared Function Main() As Integer
        Dim c As New UsingStatement5
        Using t As New GenericArgument
            Return c.test(Of GenericArgument)(t).IntMethod()
        End Using
    End Function
End Class