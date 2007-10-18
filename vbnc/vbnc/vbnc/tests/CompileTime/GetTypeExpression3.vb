Imports System.Reflection
Class GetTypeExpression3
    Public Shared Value As Integer
    Shared Function Test() As Integer
        value = 1
        GetType(GetTypeExpression3).GetField("Value", BindingFlags.Public Or BindingFlags.Static).SetValue(Nothing, 0)
        Return Value
    End Function
End Class