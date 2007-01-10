Module ByRefLong1
    Function Main() As Integer
        Dim result As Boolean
        Dim testvalue As Long

        testvalue = 71
        Tester(testvalue)
        result = testvalue = 70

        If result = False Then
            System.Console.WriteLine("FAIL ByRefLong1")
            Return 1
        End If
    End Function

    Sub Tester(ByRef value As Long)
        value = 70
    End Sub
End Module

