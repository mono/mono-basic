Imports System

Module ForA

    Function Main() As Integer
        Dim i, j, k As Integer

        For i = 0 To 10
            If i = 1 Then
                Exit For
            End If
            For j = 0 To 10 Step 2
                If j = 2 Then
                    Exit For
                End If
                For k = 0 To 10 Step 3
                    If k = 3 Then
                        Exit For
                    End If
                    If i <> 0 And j <> 0 And k <> 0 Then
                        System.Console.WriteLine("#A1 For not working") : Return 1
                    End If
                Next k
            Next
        Next i

        If i <> 1 Or j <> 2 Or k <> 3 Then
            System.Console.WriteLine("#ForA1 - For..Next Statement failed") : Return 1
        End If

    End Function

End Module