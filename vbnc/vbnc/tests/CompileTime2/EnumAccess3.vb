Imports System
Imports System.Collections
Imports System.Reflection

Namespace EnumAccess3
    Enum E
        value
    End Enum

    Class Test
        Shared Function Main() As Integer
            Dim str As String
            str = E.value.ToString
            Return 0
        End Function
    End Class
End Namespace