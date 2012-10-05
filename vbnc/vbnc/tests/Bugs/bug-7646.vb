Module Module1
    Function Main() As Integer
        On Error GoTo HandleErrors

        Console.WriteLine("Throwing exception")
        Throw New Exception()

ExitHere:
        Console.WriteLine("In resume label")
        Return 0

HandleErrors:
        Console.WriteLine("In error handler")
        Resume ExitHere

        Return 1
    End Function
End Module