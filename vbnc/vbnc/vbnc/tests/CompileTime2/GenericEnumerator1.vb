Imports System
Imports System.Collections
Imports System.Reflection

Namespace GenericEnumerator1
    Class C
        Shared Function Main() As Integer

        End Function
    End Class
    Class E
        Implements Generic.IEnumerable(Of C)

        Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of C) Implements System.Collections.Generic.IEnumerable(Of C).GetEnumerator
        End Function

        Public Function GetEnumerator1() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator

        End Function
    End Class
End Namespace