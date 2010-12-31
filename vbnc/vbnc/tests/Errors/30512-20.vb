Option Strict On

Imports System
Imports System.Collections
Imports System.Reflection

Class UserConversion1
    Shared Function Main() As Integer
       
        Dim a1 As wa1
        Dim b1 As wb1
        b1 = a1

        Dim a2 As wa2
        Dim b2 As wb2
        b2 = a2

        Return 0
    End Function

    Class WA1
        'Shared Widening Operator CType(ByVal v As WA1) As WB1
        '    Return Nothing
        'End Operator
    End Class
    Class WB1
        Shared Narrowing Operator CType(ByVal v As WA1) As WB1
            Return Nothing
        End Operator
    End Class

    Class WA2
        Shared Widening Operator CType(ByVal v As WA2) As WB2
            Return Nothing
        End Operator
    End Class
    Class WB2
        'Shared Widening Operator CType(ByVal v As WA2) As WB2
        '    Return Nothing
        'End Operator
    End Class
End Class