Module ByRefShort1
    Function Main() As Integer
        Dim result As Boolean
        Dim testvalue As Short

        testvalue = 31
        Tester(testvalue)
        result = testvalue = 30

        If result = False Then
            System.Console.WriteLine("FAIL ByRefShort1")
            Return 1
        End If
    End Function

    Sub Tester(ByRef value As Short)
        value = 30
    End Sub
End Module

