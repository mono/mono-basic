Imports System

Module ConditionalStatementsC

    Function Main() As Integer

        Dim i As Integer
        Dim arr(10) As Integer
        Dim flag As Boolean

        For i = 0 To 10

            Select Case i
                Case 0 To 2
                    arr(i) = 1
                Case Is < 2, 3, 6 To 7
                    arr(i) = 2
                    Select Case i           ' Nested Select Case Statement
                        Case 0 To 2
                            flag = False
                        Case 3, 6 To 7
                            flag = True
                        Case Else
                            flag = False
                    End Select
                Case Is <= 8, Is >= 7
                    arr(i) = 3
                Case Else
                    arr(i) = 4
            End Select

        Next

        If arr(0) <> 1 Or arr(1) <> 1 Or arr(2) <> 1 Then
            System.Console.WriteLine("#CSC1 - Select Case Statement failed") : Return 1
        ElseIf arr(3) <> 2 Or arr(6) <> 2 Or arr(7) <> 2 Then
            System.Console.WriteLine("#CSC2 - Select Case Statement failed") : Return 1
        ElseIf arr(4) <> 3 Or arr(5) <> 3 Or arr(8) <> 3 Or arr(9) <> 3 Or arr(10) <> 3 Then
            System.Console.WriteLine("#CSC3 - Select Case Statement failed") : Return 1
        ElseIf flag = False Then
            System.Console.WriteLine("#CSC4 - Nested Select Case Statement failed") : Return 1
        Else
            Console.WriteLine("OK")
        End If

    End Function

End Module