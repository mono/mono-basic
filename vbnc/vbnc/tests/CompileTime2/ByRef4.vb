Class ByRef4
    Shared Function Main() As Integer
        Dim result As Boolean

        Dim testvalue As String = "FAILED"
        tester(testvalue)
        result = testvalue = "OK"

        If result = False Then
            System.Console.WriteLine("FAIL ByRef4")
            System.Console.WriteLine("(detailed message)")
            Return 1
        End If
    End Function
    Shared Sub Tester(ByRef value As String)
        value = "OK"
    End Sub
End Class
