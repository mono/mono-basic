Module Module1
    Sub TestVB6()
        On Error GoTo HandleErrors


        ' Do something in here that
        ' might raise an error.

ExitHere:
        ' Perform cleanup code here.
        ' Disregard errors in this
        ' cleanup code.
        On Error Resume Next
        ' Perform cleanup code.
        Exit Sub

HandleErrors:
        Select Case Err.Number
            ' Add cases for each
            ' error number you want to trap.
            Case Else
                ' Add "last-ditch" error handler.
                Console.WriteLine("Error: " & Err.Description)
        End Select
        Resume ExitHere2
    End Sub
End Module