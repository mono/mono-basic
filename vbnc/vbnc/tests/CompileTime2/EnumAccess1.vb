Class EnumAccess1
    Enum Test
        Value1
        Value2
        Value3 = Value1
        Value4 = Value2
    End Enum

    Shared Function Tester(Optional ByVal Param As Test = Test.Value2) As Integer
        Return 0
    End Function

    Shared Function Main() As Integer
        Return Tester
    End Function
End Class