Imports System
Imports System.Collections
Imports System.Reflection

Namespace MethodInvocations
    Class Test
        Function A1() As Integer
            Return 1
        End Function

        Function A1(ByVal p1 As Integer) As Integer
            Return 2
        End Function

        Function A1(ByVal ParamArray ps() As Integer) As Integer
            Return 3
        End Function

        Function A1(ByVal p1 As String, Optional ByVal p2 As String = "", Optional ByVal p3 As Integer = 3, Optional ByVal p4 As Integer = 2) As Integer
            Return 4
        End Function

        Function A() As Integer
            Dim result As Boolean = True
            result = a1() = 1 AndAlso result
            result = a1(1) = 2 AndAlso result
            result = a1(1, 2, 3) = 3 AndAlso result
            result = a1(New Integer() {1}) = 3 AndAlso result
            result = a1("") = 4 AndAlso result
            result = a1("", "") = 4 AndAlso result
            result = a1("", , 2) = 4 AndAlso result
            result = a1("", "", , 2) = 4 AndAlso result
            result = a1("", "", 2, 3) = 4 AndAlso result

            If result Then
                Return 0
            Else
                Return 1
            End If
        End Function

        Shared Function Main() As Integer
            Dim t As New Test
            Dim result As Boolean = True

            result = t.a = 0 AndAlso result

            If result Then
                Return 0
            Else
                Return 1
            End If
        End Function
    End Class
End Namespace