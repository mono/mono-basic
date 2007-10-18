Option Explicit Off
Option Strict Off

Class TestingHelper
    Public Shared i As Integer
End Class

Module ExplicitOff
    Private funCount As Integer = 0

    Function Main() As Integer
        Dim result As Object
        result += DoItExplicitly()
        result += DoItExplicitlyLateBound()
        result += DoItImplicitlyTypingLateBound()
        result += DoItImplicitlyLateBound()
        Return result
    End Function

    Function DoItExplicitly() As Object
        Dim o As TestingHelper = New TestingHelper()
        o.i = o.i + 1
        fun()
    End Function

    Function DoItExplicitlyLateBound() As Object
        Dim o As Object = New TestingHelper()
        o.i = o.i + 1
        fun()
    End Function

    Function DoItImplicitlyTypingLateBound() As Object
        Dim o = New TestingHelper()
        o.i = o.i + 1
        fun()
    End Function

    Function DoItImplicitlyLateBound() As Object
        anything = New TestingHelper()
        anything.i = anything.i + 1
        fun()
    End Function

    Function fun() As Object
        funCount = funCount + 1
        System.Console.WriteLine("FunCounting: {0} - TestingHelper.i {1}", funCount, TestingHelper.i)
        TestingHelper.i = TestingHelper.i + 1
        If TestingHelper.i <> (2 * funCount) Then
            System.Console.WriteLine("Shared variable not working") : Return 1
        End If
    End Function
End Module
