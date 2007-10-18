Module ByRefInteger1
    Function Main() As Integer
        Dim result As Boolean
        Dim testvalue As Integer

        testvalue = 51
        Tester(testvalue)
        result = testvalue = 50

        If result = False Then
            System.Console.WriteLine("FAIL ByRefInteger1")
            Return 1
        End If
    End Function

    Sub Tester(ByRef value As Integer)
        value = 50
    End Sub
End Module

