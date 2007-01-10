Class EnumValueComparison2
    Enum Test
        value1
        value2
    End Enum

    Shared Function Tester(ByRef T As Test) As Integer
        If t = test.value1 Then
            Return 0
        Else
            Return 1
        End If
    End Function

    Shared Function Main() As Integer
        Return tester(test.value1)
    End Function
End Class