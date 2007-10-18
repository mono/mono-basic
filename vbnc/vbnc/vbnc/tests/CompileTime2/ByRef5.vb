Module ByRef5
    Function Main() As Integer
        Dim result As Boolean

        testsub(1)
        result = True

        If result = False Then
            System.Console.WriteLine("FAIL ByRef5")
            System.Console.WriteLine("(detailed message)")
            Return 1
        End If
    End Function
    Sub testsub(Optional ByRef value As Integer = 1)
        value = 2
    End Sub
End Module
