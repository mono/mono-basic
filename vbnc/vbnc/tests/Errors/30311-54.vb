Option Strict On

Imports System
Imports System.Collections
Imports System.Reflection

Class UserConversion1
    Shared Function Main() As Integer
        Dim a As wa
        Dim b As wb
        b = a
    End Function

    Class WA
        Shared Widening Operator CType(ByVal v As WA) As WB
            Return Nothing
        End Operator
    End Class
    Class WB
        Shared Widening Operator CType(ByVal v As WA) As WB
            Return Nothing
        End Operator
    End Class
End Class