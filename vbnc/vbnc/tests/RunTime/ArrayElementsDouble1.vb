Module ArrayDoubleElements1
    Function Main() As Integer
        Dim result As Boolean
        Dim testvalue(10) As Double

        testvalue(10) = 110.011
        result = testvalue(10) = 110.011

        If result = False Then
            System.Console.WriteLine("FAIL ArrayDoubleElements1")
            Return 1
        End If
    End Function

End Module

