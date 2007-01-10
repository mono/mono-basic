Imports System
Imports System.Collections
Imports System.Reflection

Namespace StaticVariable5
    Class Test
        Shared Function Main() As Integer
            Dim t As New test
            Return CInt(t.var = "something") + 1
        End Function

        ReadOnly Property Var() As String
            Get
                Static tmp As String
                If tmp Is Nothing Then
                    tmp = "something"
                End If
                Return tmp
            End Get
        End Property
    End Class
End Namespace