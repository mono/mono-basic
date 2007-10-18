Imports System
Imports System.Collections
Imports System.Reflection

Namespace IfElse1
    Class Test
        Shared Function Main() As Integer
            If True Then
                Main = 0
            Else
                Main = 1
            End If
        End Function
    End Class
End Namespace