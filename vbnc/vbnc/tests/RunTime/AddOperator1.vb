Module AddOperator1
    Class Operand
        Public Number As Integer
        Shared Operator +(ByVal op1 As operand, ByVal op2 As operand) As Integer
            Return op1.Number + op2.Number
        End Operator
    End Class

    Class Consumer
        Shared Function Main() As Integer
            Dim o1, o2 As operand
            Dim i As Integer

            o1.Number = 1
            o2.Number = 2

            i = o1 + o2

            If i = 3 Then
                Return 0
            Else
                Return 1
            End If
        End Function
    End Class
End Module
