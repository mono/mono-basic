Imports System
Imports System.Collections
Imports System.Reflection

Namespace GenericMethodImplements3
    Class Test(Of T)
        Implements Generic.IComparer(Of T)

        Function C(ByVal x As T, ByVal y As T) As Integer Implements Generic.IComparer(Of T).Compare

        End Function

    End Class
    Class T
        Shared Function Main() As Integer

        End Function
    End Class
End Namespace