Imports System
Imports System.Collections
Imports System.Reflection

Namespace Attributes4
    Class TAttribute
        Inherits Attribute
        Sub New(ByVal P1 As String, Optional ByVal P2 As String = Nothing)

        End Sub
    End Class
    <T("")> _
    Class Test
    End Class
End Namespace