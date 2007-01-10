Module ByRefDate1
    Function Main() As Integer
        Dim result As Boolean
        Dim testvalue As Date

        testvalue = #12/31/1999 12:34:00 PM#
        Tester(testvalue)
        result = testvalue = #01/01/2000 12:34#

        If result = False Then
            System.Console.WriteLine("FAIL ByRefDate1")
            Return 1
        End If
    End Function

    Sub Tester(ByRef value As Date)
        value = #01/01/2000 12:34#
    End Sub
End Module

