Imports System
Imports System.Collections
Imports System.Reflection

Namespace GenericParameterAccess1
    Class G(Of T)
        Sub Test(ByVal value As T)
            Dim str As String
            str = value.ToString
        End Sub

    End Class
    Class Test
        Shared Function Main() As Integer

        End Function
    End Class
End Namespace