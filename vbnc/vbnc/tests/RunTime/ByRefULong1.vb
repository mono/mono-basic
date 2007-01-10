Module ByRefULong1
    Function Main() As Integer
        Dim result As Boolean
        Dim testvalue As ULong

        testvalue = 81
        Tester(testvalue)
        result = testvalue = 80UL

        If result = False Then
            System.Console.WriteLine("FAIL ByRefULong1")
            Return 1
        End If
    End Function

    Sub Tester(ByRef value As ULong)
        value = 80
    End Sub
End Module

