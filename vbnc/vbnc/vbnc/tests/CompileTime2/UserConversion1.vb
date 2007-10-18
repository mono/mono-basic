Imports System
Imports System.Collections
Imports System.Reflection

Namespace UserConversion1
    Class Test
        Shared Function Main() As Integer
            Dim t As New Test
            Dim s As String
            s = CStr(t)
            Return CInt(s = "") + 1
        End Function

        Shared Widening Operator CType(ByVal v As Test) As String
            Return ""
        End Operator
    End Class
End Namespace