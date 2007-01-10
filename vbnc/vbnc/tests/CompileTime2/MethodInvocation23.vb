Imports System
Imports System.Collections
Imports System.Reflection
Imports System.Reflection.Emit

Namespace MethodInvocation23
    Class Test
        Shared Function F(ByVal vals As Object()) As String
            Return vals(0).GetType.Name
        End Function

        Shared Function Main() As Integer
            Dim objs() As Object
            objs = New Object() {System.Diagnostics.DebuggableAttribute.DebuggingModes.DisableOptimizations Or Diagnostics.DebuggableAttribute.DebuggingModes.Default}
            If F(objs) <> "DebuggingModes" Then
                Return -1
            End If

            Return 0
        End Function
    End Class
End Namespace