Module ByRefSByte1
    Function Main() As Integer
        Dim result As Boolean
        Dim testvalue As SByte

        testvalue = 21
        Tester(testvalue)
        result = testvalue = 20

        If result = False Then
            System.Console.WriteLine("FAIL ByRefSByte1")
            Return 1
        End If
    End Function

    Sub Tester(ByRef value As SByte)
        value = 20
    End Sub
End Module

