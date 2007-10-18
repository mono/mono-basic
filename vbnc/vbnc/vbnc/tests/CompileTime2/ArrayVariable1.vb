Imports System
Imports System.Collections
Imports System.Reflection

Namespace ArrayVariable1
    Class V
        Public Value As Integer
    End Class

    Class Test
        Shared Function Main() As Integer
            Dim arr(,) As V
            ReDim arr(2, 2)
            arr(1, 1) = New V
            arr(1, 1).value = 0
            Return arr(1, 1).value
        End Function
    End Class
End Namespace