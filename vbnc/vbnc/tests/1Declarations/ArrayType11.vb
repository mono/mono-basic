Public Class ArrayType11
    Shared Function Main() As Integer
        Dim str As String() = New String() {"A", "AA"}
        Select Case str.Length
            Case 1
                Return 1
            Case 2
                Return 0
            Case Else
                Return 2
        End Select
    End Function
End Class