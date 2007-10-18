Module ByRefByte1
    Function Main() As Integer
        Dim result As Boolean
        Dim testvalue As Byte

        testvalue = 11
        Tester(testvalue)
        result = testvalue = 10

        If result = False Then
            System.Console.WriteLine("FAIL ByRefByte1")
            Return 1
        End If
    End Function

    Sub Tester(ByRef value As Byte)
        value = 10
    End Sub
End Module

