Module AddOperator2
    Class Operand
        Public Number As Integer
        Shared Operator +(ByVal op1 As operand) As Integer
            Return op1.Number + 1
        End Operator
    End Class

    Class Consumer
        Shared Function Main() As Integer
            Dim o1 As operand
            Dim i As Integer

            o1.Number = 1

            i = +o1

            If i = 2 Then
                Return 0
            Else
                Return 1
            End If
        End Function
    End Class
End Module
