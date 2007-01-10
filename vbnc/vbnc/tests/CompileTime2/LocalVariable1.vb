Imports System
Imports System.Collections
Imports System.Reflection

Namespace LocalVariable1
    Enum KS
        value
    End Enum
    Class Test
        Sub T()
            Dim field As fieldinfo
            Dim value As KS
            value = DirectCast(field.GetValue(Nothing), KS)
        End Sub
        Shared Function Main() As Integer

        End Function
    End Class
End Namespace