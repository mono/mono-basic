Imports System
Imports System.Collections
Imports System.Reflection

Namespace For7
    Class IteratorA1
        Public Shared Operator >=(ByVal op1 As IteratorA1, ByVal op2 As IteratorA1) As IteratorB1

        End Operator

        Public Shared Operator <=(ByVal op1 As IteratorA1, ByVal op2 As IteratorA1) As IteratorB1

        End Operator

        Public Shared Operator -(ByVal op1 As IteratorA1, ByVal op2 As IteratorA1) As IteratorA1

        End Operator

        Public Shared Operator +(ByVal op1 As IteratorA1, ByVal op2 As IteratorA1) As IteratorA1

        End Operator

    End Class

    Class IteratorA2
        Public Shared Operator >=(ByVal op1 As IteratorA2, ByVal op2 As IteratorA2) As IteratorB2

        End Operator

        Public Shared Operator <=(ByVal op1 As IteratorA2, ByVal op2 As IteratorA2) As IteratorB2

        End Operator

        Public Shared Operator -(ByVal op1 As IteratorA2, ByVal op2 As IteratorA2) As IteratorA2

        End Operator

        Public Shared Operator +(ByVal op1 As IteratorA2, ByVal op2 As IteratorA2) As IteratorA2

        End Operator

    End Class


    Class IteratorA3
        Public Shared Operator >=(ByVal op1 As IteratorA3, ByVal op3 As IteratorA3) As IteratorB3

        End Operator

        Public Shared Operator <=(ByVal op1 As IteratorA3, ByVal op3 As IteratorA3) As IteratorB3

        End Operator

        Public Shared Operator -(ByVal op1 As IteratorA3, ByVal op3 As IteratorA3) As IteratorA3

        End Operator

        Public Shared Operator +(ByVal op1 As IteratorA3, ByVal op3 As IteratorA3) As IteratorA3

        End Operator

    End Class

    Class IteratorB1
        Public Shared Operator IsTrue(ByVal v As IteratorB1) As Boolean
            Return True
        End Operator

        Public Shared Operator IsFalse(ByVal v As IteratorB1) As Boolean
            Return False
        End Operator
    End Class

    Class IteratorB2

        Public Shared Widening Operator CType(ByVal V As IteratorB2) As Boolean
            Return True
        End Operator
    End Class

    Class IteratorB3
        Public Shared Narrowing Operator CType(ByVal V As IteratorB3) As Boolean
            Return True
        End Operator
    End Class

    Class Test
        Shared Function Main() As Integer
            A1()
            A2()
            A3()
        End Function

        Shared Sub A1()
            Dim a As New IteratorA1, b As New IteratorA1
            For i As IteratorA1 = a To b

            Next
        End Sub

        Shared Sub A2()
            Dim a As New IteratorA2, b As New IteratorA2
            For k As Object = a To b

            Next
        End Sub

        Shared Sub A3()
            Dim a As New IteratorA3, b As New IteratorA3
            For i As Object = a To b

            Next
        End Sub

    End Class
End Namespace