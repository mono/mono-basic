Class EnumValueComparison1
    Enum Test
        value1
        value2
    End Enum

    Shared Function Main() As Integer
        Dim tmp As Test = Test.value1
        If tmp = test.value1 Then
            Return 0
        Else
            Return 1
        End If
    End Function
End Class