Imports System
Imports System.Collections
Imports System.Reflection

Namespace ParamArray5
    Class Test
        Shared Function ShowMessage(ByVal ParamArray Parameters() As String) As Integer
            Return -1
        End Function

        Shared Function ShowMessage(ByVal Location As Test, ByVal ParamArray Parameters() As String) As Integer
            Return 0
        End Function

        Shared Function Main() As Integer
            Dim Parameters As String()
            Return ShowMessage(Nothing, Parameters)
        End Function
    End Class
End Namespace