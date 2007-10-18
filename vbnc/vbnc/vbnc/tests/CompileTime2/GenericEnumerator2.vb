Imports System
Imports System.Collections
Imports System.Reflection

Namespace GenericEnumerator2
    Class C
        Shared Function Main() As Integer

        End Function
    End Class

    Class E
        Implements Generic.IEnumerator(Of C)

        Public ReadOnly Property Current() As C Implements System.Collections.Generic.IEnumerator(Of C).Current
            Get
            End Get
        End Property

        Public ReadOnly Property Current1() As Object Implements System.Collections.IEnumerator.Current
            Get
            End Get
        End Property

        Public Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext

        End Function

        Public Sub Reset() Implements System.Collections.IEnumerator.Reset

        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose

        End Sub
    End Class
End Namespace