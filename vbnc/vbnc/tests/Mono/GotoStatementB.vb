' Author:
'   Maverson Eduardo Schulze Rosa (maverson@gmail.com)
'
' GrupoTIC - UFPR - Federal University of Paran

Module Test
    Public i As Integer
    Function Main() As Integer
        Try
            i = 5
out_of_block_backward_label:
            i += 2
            If (i <> 9) Then
                GoTo out_of_block_backward_label
            Else
                GoTo out_of_try_forward_label
            End If
        Finally
            i += 1
        End Try
out_of_try_forward_label:
        If i <> 10 Then
            System.Console.WriteLine("Finally block not working... Expected 10 but got " & i) : Return 1
        End If
    End Function
End Module
