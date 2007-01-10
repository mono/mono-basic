Module ByRefChar1
    Function Main() As Integer
        Dim result As Boolean
        Dim testvalue As Char

        testvalue = "X"c
        Tester(testvalue)
        result = testvalue = "C"c

        If result = False Then
            System.Console.WriteLine("FAIL ByRefChar1")
            Return 1
        End If
    End Function

    Sub Tester(ByRef value As Char)
        value = "C"c
    End Sub
End Module

