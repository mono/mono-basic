Class GenericMember2
    Class Generic(Of V)
        Public Value As Integer
        Function F1() As Integer
            Value = 1
            Return Value
        End Function
        Function F2() As V
            Value = 2
            Return Nothing
        End Function
        Function F3(ByVal Test As V) As Integer
            Value = 3
            Return Value
        End Function
    End Class

    Shared Function Main() As Integer
        Dim result As Boolean
        Dim g As New generic(Of Integer)

        g.f1()
        result = g.value = 1 AndAlso result
        g.f2()
        result = g.value = 2 AndAlso result
        g.f3(1)
        result = g.value = 3 AndAlso result

        Return CInt(result)
    End Function
End Class