Class EnumAccess2


    Shared Function Main() As Integer
        Return Tester
    End Function

    Shared Function Tester(Optional ByVal Param As Test = Test.Value2) As Integer
        Return 0
    End Function

    Enum Test
        Value1
        Value2
        Value3 = Value1
        Value4 = Value2
    End Enum

End Class