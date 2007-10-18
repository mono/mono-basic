Imports System
Imports System.Collections
Imports System.Reflection

Namespace Enumerator1
    Class C

    End Class
    Class E1
        Implements IEnumerator

        Public ReadOnly Property Current() As Object Implements System.Collections.IEnumerator.Current
            Get
            End Get
        End Property

        Public Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext

        End Function

        Public Sub Reset() Implements System.Collections.IEnumerator.Reset

        End Sub

    End Class
End Namespace