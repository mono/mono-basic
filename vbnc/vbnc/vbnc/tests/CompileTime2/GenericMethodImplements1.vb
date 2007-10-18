Imports System
Imports System.Collections
Imports System.Reflection

Namespace GenericMethodImplements1
    Public Class BaseList(Of T)
        Implements Generic.IEnumerable(Of T)

        Public Function GetEnumerator3() As System.Collections.Generic.IEnumerator(Of T) Implements System.Collections.Generic.IEnumerable(Of T).GetEnumerator
        End Function

        Private Function GetEnumerator2() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        End Function

    End Class
    Class T
        Shared Function Main() As Integer

        End Function
    End Class
End Namespace