Module ByRefUShort1
    Function Main() As Integer
        Dim result As Boolean
        Dim testvalue As UShort

        testvalue = 41
        Tester(testvalue)
        result = testvalue = 40

        If result = False Then
            System.Console.WriteLine("FAIL ByRefUShort1")
            Return 1
        End If
    End Function

    Sub Tester(ByRef value As UShort)
        value = 40
    End Sub
End Module

