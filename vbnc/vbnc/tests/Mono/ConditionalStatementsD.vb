Imports System

Module ConditionalStatementsD

    Function Main() As Integer

        Dim i As Integer
        Dim sarr() As String = {"cat", "awk", "zebra", "mouse", "snake", "tiger", "lion"}
        Dim str As String = "Lion"
        Dim arr(6) As Integer

        For i = 0 To 6

            Select Case sarr(i)
                Case "ant" To "cow"
                    arr(i) = 1
                Case Is < "dog", Is = "tiger", str
                    arr(i) = 2
                Case "lion"
                    arr(i) = 3
                Case Is >= "elepahant"
                    arr(i) = 4
                Case Else
                    arr(i) = 5
            End Select

        Next

        If arr(0) <> 1 Or arr(1) <> 1 Then
            System.Console.WriteLine("#CSD1 - Switch Statement failed") : Return 1
        ElseIf arr(5) <> 2 Then
            System.Console.WriteLine("#CSD2 - Switch Statement failed") : Return 1
        ElseIf arr(6) <> 3 Then
            System.Console.WriteLine("#CSD3 - Switch Statement failed") : Return 1
        ElseIf arr(2) <> 4 Or arr(3) <> 4 Or arr(4) <> 4 Then
            System.Console.WriteLine("#CSD4 - Switch Statement failed") : Return 1
        Else
            Console.WriteLine("OK")
        End If

    End Function

End Module