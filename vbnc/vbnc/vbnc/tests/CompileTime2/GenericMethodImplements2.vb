Imports System
Imports System.Collections
Imports System.Reflection

Namespace GenericMethodImplements2
    Class Test
        Implements Generic.IComparer(Of String)

        Function C(ByVal x As String, ByVal y As String) As Integer Implements Generic.IComparer(Of String).Compare

        End Function
    End Class
    Class T
        Shared Function Main() As Integer

        End Function
    End Class
End Namespace