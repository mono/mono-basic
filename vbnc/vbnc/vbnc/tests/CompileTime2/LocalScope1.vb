Imports System
Imports System.Collections
Imports System.Reflection

Namespace LocalScope1
    Class Test
        Shared Function Main() As Integer
            If True Then
                Dim i As Integer
                i = 1
                console.writeline(i)
            Else
                Dim k As Integer
                k = 0
                console.writeline(k)
            End If
            Return 0
        End Function
    End Class
End Namespace