Module ByRefSingle1
    Function Main() As Integer
        Dim result As Boolean
        Dim testvalue As Single

        testvalue = 101.001!
        Tester(testvalue)
        result = testvalue = 100.001!

        If result = False Then
            System.Console.WriteLine("FAIL ByRefSingle1")
            Return 1
        End If
    End Function

    Sub Tester(ByRef value As Single)
        value = 100.001!
    End Sub
End Module

