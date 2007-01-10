Imports System
Imports System.Collections
Imports System.Reflection

Namespace PropertyAccess3
    Class Test
        Shared ReadOnly Property GetParameters() As System.Reflection.ParameterInfo()
            Get
                Return New ParameterInfo() {Nothing}
            End Get
        End Property

        Shared Function Main() As Integer
            Dim tmp As ParameterInfo
            tmp = GetParameters()(0)
            Return 0
        End Function
    End Class
End Namespace