Imports System

Module ControlFlowA
    Dim result As Integer

    Function F1() As Object
        Dim i As Integer
        For i = 0 To 4
            'Stop
            result += i
            If i = 3 Then
                Exit Function
            End If
        Next i
        result = 4
    End Function

    Function Main() As Integer

        F1()
        If result <> 6 Then
            System.Console.WriteLine("#CFA1 - Exit Statement failed") : Return 1
        End If
        Console.WriteLine(result)
        Try
            End
            Console.WriteLine("After Stop Statement")
        Catch e As Exception
            Console.WriteLine(e.Message)
        Finally
            System.Console.WriteLine("#CFA2 - End Statement failed")
        End Try
        : Return 1
    End Function

End Module
