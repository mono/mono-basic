Class DirectCast1
    Public Shared Function ToGenericParameter(Of T)(ByVal Value As Object) As T
        Return DirectCast(Value, T)
    End Function
End Class
