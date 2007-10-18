Imports System

Module ConditionalStatementsB

    Function Main() As Integer

        Dim i As Integer = 0

        ' With the single-line form, it is possible to have multiple 
        ' statements executed as the result of an If...Then decision.

        If i = 0 Then i += 1 : i += 2 : i += 3


        If i <> 6 Then System.Console.WriteLine("#CSB1 - LineIfThenStatement failed") : Return 1 _
         Else i += 6 : i += 12


        If i <> 24 Then
            System.Console.WriteLine("#CSB2 - LineIfThenStatement failed") : Return 1
        End If

        ' Execution of a Case block is not permitted to "fall through" to 
        ' next switch section

        Dim j As Integer = 0
        For i = 0 To 3
            Select Case i
                Case 0
                Case 2
                    j += 2
                Case 1
                Case 3
                    j += 3
            End Select
        Next

        If j <> 5 Then
            System.Console.WriteLine("#CSB3 - Switch Case Statement failed") : Return 1
        End If

    End Function

End Module