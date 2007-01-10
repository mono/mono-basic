Module ByRefString1
    Function Main() As Integer
        Dim result As Boolean
        Dim testvalue As String

        testvalue = "failed"
        Tester(testvalue)
        result = testvalue = "testvalue"

        If result = False Then
            System.Console.WriteLine("FAIL ByRefString1")
            Return 1
        End If
    End Function

    Sub Tester(ByRef value As String)
        value = "testvalue"
    End Sub
End Module

