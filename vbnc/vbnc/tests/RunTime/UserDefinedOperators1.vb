Option Strict Off
Module UserDefinedOperators1
    Class Operand1
        Public Value As Double

        Shared Operator +(ByVal O1 As Operand1, ByVal O2 As Operand1) As Double
            Return o1.value + o2.value
        End Operator
        Shared Operator -(ByVal O1 As Operand1, ByVal O2 As Operand1) As Double
            Return o1.value - o2.value
        End Operator
        Shared Operator /(ByVal O1 As Operand1, ByVal O2 As Operand1) As Double
            Return o1.value / o2.value
        End Operator
        Shared Operator \(ByVal O1 As Operand1, ByVal O2 As Operand1) As Double
            Return o1.value \ o2.value
        End Operator
        Shared Operator *(ByVal O1 As Operand1, ByVal O2 As Operand1) As Double
            Return o1.value * o2.value
        End Operator
        Shared Operator &(ByVal O1 As Operand1, ByVal O2 As Operand1) As Double
            Return o1.value & o2.value
        End Operator
        Shared Operator <<(ByVal O1 As Operand1, ByVal O2 As Integer) As Double
            Return o1.value << o2
        End Operator
        Shared Operator >>(ByVal O1 As Operand1, ByVal O2 As Integer) As Double
            Return o1.value >> o2
        End Operator
        Shared Operator =(ByVal O1 As Operand1, ByVal O2 As Operand1) As Boolean
            Return o1.value = o2.value
        End Operator
        Shared Operator <>(ByVal O1 As Operand1, ByVal O2 As Operand1) As Boolean
            Return o1.value <> o2.value
        End Operator
        Shared Operator >(ByVal O1 As Operand1, ByVal O2 As Operand1) As Boolean
            Return o1.value > o2.value
        End Operator
        Shared Operator <(ByVal O1 As Operand1, ByVal O2 As Operand1) As Boolean
            Return o1.value < o2.value
        End Operator
        Shared Operator <=(ByVal O1 As Operand1, ByVal O2 As Operand1) As Boolean
            Return o1.value <= o2.value
        End Operator
        Shared Operator >=(ByVal O1 As Operand1, ByVal O2 As Operand1) As Boolean
            Return o1.value >= o2.value
        End Operator
        Shared Operator ^(ByVal O1 As Operand1, ByVal O2 As Operand1) As Double
            Return o1.value + o2.value
        End Operator
        Shared Operator Like(ByVal O1 As Operand1, ByVal O2 As Operand1) As Boolean
            Return o1.value Like o2.value
        End Operator
        Shared Operator Mod(ByVal O1 As Operand1, ByVal O2 As Operand1) As Boolean
            Return o1.value Mod o2.value
        End Operator
        Shared Operator Or(ByVal O1 As Operand1, ByVal O2 As Operand1) As Boolean
            Return o1.value Or o2.value
        End Operator
        Shared Operator Xor(ByVal O1 As Operand1, ByVal O2 As Operand1) As Boolean
            Return o1.value Xor o2.value
        End Operator
        Shared Operator And(ByVal O1 As Operand1, ByVal O2 As Operand1) As Double
            Return o1.value And o2.value
        End Operator


        Shared Narrowing Operator CType(ByVal O1 As Operand1) As Integer
            Return CInt(O1.Value)
        End Operator

        Shared Widening Operator CType(ByVal O1 As Operand1) As Decimal
            Return CDec(O1.Value)
        End Operator


        Shared Operator +(ByVal O1 As Operand1) As Double
            Return +o1.value
        End Operator
        Shared Operator -(ByVal O1 As Operand1) As Double
            Return -o1.value
        End Operator
        Shared Operator Not(ByVal O1 As Operand1) As Double
            Return Not o1.value
        End Operator
        Shared Operator IsTrue(ByVal O1 As Operand1) As Boolean
            Return o1.value = True
        End Operator
        Shared Operator IsFalse(ByVal O1 As Operand1) As Boolean
            Return o1.value = False
        End Operator
    End Class

    Class Consumer
        Shared Function Main() As Integer
            Dim o1 As New operand1, o2 As New Operand1
            Dim result As Double

            o1.value = 3
            o2.value = 7

            result = o1 + o2
            If result <> 10 Then System.Console.WriteLine("Add operator failed.") : Return 1

            result = o1 - o2
            If result <> -4 Then System.Console.WriteLine("Subtract operator failed.") : Return 1

            result = o1 \ o2
            If result <> (3 \ 7) Then System.Console.WriteLine("Integer division operator failed.") : Return 1

            result = o1 / o2
            If result <> (3 / 7) Then System.Console.WriteLine("Real division operator failed.") : Return 1
            result = o1 * o2

            If result <> 21 Then System.Console.WriteLine("Multiply operator failed.") : Return 1

            result = o1 & o2
            If result <> 37 Then System.Console.WriteLine("Concat operator failed.") : Return 1

            result = o1 << 2
            If result <> (3 << 2) Then System.Console.WriteLine("Left shift operator failed.") : Return 1

            result = o1 >> 5
            If result <> (3 >> 5) Then System.Console.WriteLine("Right shift operator failed.") : Return 1

            If o1 = o2 Then System.Console.WriteLine("Equals operator failed.") : Return 1

            If Not (o1 <> o2) Then System.Console.WriteLine("Not equals operator failed.") : Return 1

            If o1 > o2 Then System.Console.WriteLine("Greater than operator failed.") : Return 1

            If Not (o1 < o2) Then System.Console.WriteLine("Less than operator failed.") : Return 1

            If Not (o1 <= o2) Then System.Console.WriteLine("Less than or equal operator failed.") : Return 1

            If o1 >= o2 Then System.Console.WriteLine("Greater than or equal operator failed.") : Return 1

            result = o1 ^ o2
            If result <> 2187 Then System.Console.WriteLine("Integer division operator failed.") : Return 1

            If o1 Like o2 Then System.Console.WriteLine("Like operator failed.") : Return 1

            result = o1 Mod o2
            If result <> (3 Mod 7) Then System.Console.WriteLine("Mod operator failed.") : Return 1

            result = o1 Or o2
            If result <> (3 Or 7) Then System.Console.WriteLine("Or operator failed.") : Return 1

            result = o1 Xor o2
            If result <> (3 Xor 7) Then System.Console.WriteLine("Xor operator failed.") : Return 1

            result = o1 And o2
            If result <> (3 And 7) Then System.Console.WriteLine("And operator failed.") : Return 1

            result = +o1
            If result <> 3 Then System.Console.WriteLine("Unary plus operator failed.") : Return 1

            result = -o2
            If result <> -7 Then System.Console.WriteLine("Unary minus operator failed.") : Return 1

            result = Not o1
            If Not 3 <> result Then System.Console.WriteLine("Not operator failed.") : Return 1

        End Function
    End Class
End Module