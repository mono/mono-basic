Imports System
Imports System.Collections
Imports System.Reflection

Namespace Enum2
    Enum En
        value
    End Enum

    Class Test
        Shared Function Main() As Integer
            Dim b As Boolean
            Dim e As [Enum]
            e = en.value
            b = TypeOf e Is [Enum]
            If b Then
                Return 0
            Else
                Return 1
            End If
        End Function
    End Class
End Namespace