Module ByRefUInteger1
    Function Main() As Integer
        Dim result As Boolean
        Dim testvalue As UInteger

        testvalue = 61
        Tester(testvalue)
        result = testvalue = 60UI

        If result = False Then
            System.Console.WriteLine("FAIL ByRefUInteger1")
            Return 1
        End If
    End Function

    Sub Tester(ByRef value As UInteger)
        value = 60
    End Sub
End Module

