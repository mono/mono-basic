Module ByRefObject1
    Function Main() As Integer
        Dim result As Boolean
        Dim testvalue As Object

        testvalue = "Something"
        Tester(testvalue)
        result = testvalue Is Nothing

        If result = False Then
            System.Console.WriteLine("FAIL ByRefObject1")
            Return 1
        End If
    End Function

    Sub Tester(ByRef value As Object)
        value = Nothing
    End Sub
End Module

