Imports System
Imports System.Collections
Imports System.Reflection

Namespace Redim2
    Class Test
        Shared Function Main() As Integer
            Dim v() As Integer
            ReDim v(5)
            Return v.Length - 6
        End Function
    End Class
End Namespace