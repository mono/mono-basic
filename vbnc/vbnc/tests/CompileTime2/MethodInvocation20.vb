Imports System
Imports System.Collections
Imports System.Reflection

Namespace MethodInvocation20
    Class Test
        Shared ReadOnly Property Infos() As ParameterInfo()
            Get
                Return Nothing
            End Get
        End Property
        Shared Function Main() As Integer
            Dim b As Boolean
            If b Then
                b = infos()(0).IsOptional
                Return 1
            Else
                Return 0
            End If
        End Function
    End Class
End Namespace