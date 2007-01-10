Module ArrayUShortElements1
    Function Main() As Integer
        Dim result As Boolean
        Dim testvalue(3) As UShort

        testvalue(3) = 40
        result = testvalue(3) = 40

        If result = False Then
            System.Console.WriteLine("FAIL ArrayUShortElements1")
            Return 1
        End If
    End Function

End Module

