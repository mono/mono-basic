Imports System
Imports System.Collections
Imports System.Reflection

Namespace Enum5
    Class Test
        Shared Function Main() As Integer
            Dim e As en
            Dim o As String
            o = t(e.ToString())
            Return o.Length - 8
        End Function

        Shared Function T(ByVal ParamArray P() As String) As String
            Return p(0)
        End Function
    End Class
    Enum En
        value222
    End Enum

End Namespace