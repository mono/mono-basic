Module ByRefDouble1
    Function Main() As Integer
        Dim result As Boolean
        Dim testvalue As Double

        testvalue = 111.011
        Tester(testvalue)
        result = testvalue = 110.011

        If result = False Then
            System.Console.WriteLine("FAIL ByRefDouble1")
            Return 1
        End If
    End Function

    Sub Tester(ByRef value As Double)
        value = 110.011
    End Sub
End Module

