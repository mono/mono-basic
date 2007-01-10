Imports System
Imports System.Collections
Imports System.Reflection

Namespace Boolean1
    Class Test
        Shared Function Main() As Integer
            Dim b As Boolean
            b = Not True
            b = Not b
            b = Not (1 = 2)
        End Function
    End Class
End Namespace