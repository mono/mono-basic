Module ArrayDateElements1
    Function Main() As Integer
        Dim result As Boolean
        Dim testvalue(14) As Date

        testvalue(14) = #1/1/2000 12:34:00 PM#
        testvalue(14) = testvalue(14)
        result = testvalue(14) = #01/01/2000 12:34#

        If result = False Then
            System.Console.WriteLine("FAIL ArrayDateElements1")
            Return 1
        End If
    End Function

End Module

