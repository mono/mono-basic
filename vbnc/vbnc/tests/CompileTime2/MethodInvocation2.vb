Imports System.Collections

Namespace MethodInvocation2
    Class Test1

    End Class

    Enum Test2
        Value1
    End Enum

    Class Tester
        Shared Function Test(ByVal Parent As Test1, ByVal List As Generic.List(Of Test2)) As Integer
            Return 1
        End Function

        Shared Function Test(ByVal Parent As Test1, ByVal ParamArray List As Test2()) As Integer
            Return 0
        End Function

        Shared Function Main() As Integer
            Return Test(Nothing, Test2.Value1)
        End Function
    End Class
End Namespace