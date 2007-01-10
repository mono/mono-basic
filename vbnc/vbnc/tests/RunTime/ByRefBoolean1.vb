Module ByRefBoolean1
    Function Main() As Integer
        Dim result As Boolean
        Dim testvalue As Boolean

        testvalue = False
        Tester(testvalue)
        result = testvalue = True

        If result = False Then
            System.Console.WriteLine("FAIL ByRefBoolean1")
            Return 1
        End If
    End Function

    Sub Tester(ByRef value As Boolean)
        value = True
    End Sub
End Module

