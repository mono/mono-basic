Imports System

Module ConditionalStatementsA

    Function Main() As Integer

        Dim i As Integer = 0

        If i = 0 Then i = 1

        If i <> 1 Then System.Console.WriteLine("#CSA1") : Return 1 Else i = 2

        If i = 1 Then  Else i = 3

        If i <> 3 Then i = 2 Else  ' Should give compile time error

        If i <> 2 Then
            i = 3
        End If

        If i = 3 Then
        End If

        If i <> 3 Then
            System.Console.WriteLine("#CSA2") : Return 1
        Else
            i = 4
        End If

        If i <> 4 Then
            System.Console.WriteLine("#CSA3") : Return 1
        ElseIf i = 4 Then
            i = 5
        End If

        If i <> 5 Then
            System.Console.WriteLine("#CSA4") : Return 1
        ElseIf i = 6 Then
            System.Console.WriteLine("#CSA5") : Return 1
        ElseIf i = 5 Then
            i = 6
        Else
            System.Console.WriteLine("#CSA6") : Return 1
        End If

    End Function

End Module