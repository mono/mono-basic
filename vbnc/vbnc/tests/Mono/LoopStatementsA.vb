Imports System

Module LoopStatementsA

    Function Main() As Integer
        Dim index As Integer = 0
        Dim count As Integer = 0

        Do
            count += 1
            index = 0
            While index <> 4
                index += 1
            End While
            If index <> 4 Then
                System.Console.WriteLine("#LSA1 - Loop Statement failed") : Return 1
            End If

            Do While index < 10
                index += 1
                If index = 8 Then
                    Exit Do
                End If
            Loop
            If index <> 8 Then
                System.Console.WriteLine("#LSA2 - Loop Statement failed") : Return 1
            End If

            Do
                index += 1
            Loop While index < 12
            If index <> 12 Then
                System.Console.WriteLine("#LSA3 - Loop Statement failed") : Return 1
            End If

            Do Until index <= 8
                index -= 1
            Loop
            If index <> 8 Then
                System.Console.WriteLine("#LSA4 - Loop Statenment failed") : Return 1
            End If

            Do
                index -= 1
                If index = 4 Then
                    Exit Do
                End If
            Loop Until index <= 3
            If index <> 4 Then
                System.Console.WriteLine("#LSA5 - Loop Statenment failed") : Return 1
            End If

            If count = 2 Then
                Exit Do
                Exit Do
            End If

        Loop

    End Function

End Module