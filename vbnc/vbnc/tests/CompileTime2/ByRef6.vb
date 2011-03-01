Imports System
Imports System.Collections
Imports System.Reflection

Namespace ByRef6
    Class Test
        Shared ReadOnly Property Count() As Integer
            Get
                Return 0
            End Get
        End Property

        Shared Function Type() As Type
            Return GetType(String)
        End Function

        Shared ReadOnly Property StackState() As String
            Get
                Return ""
            End Get
        End Property

        Shared Function Main() As Integer
            Try
                Return Main2
            Catch ex As Exception
                Console.WriteLine(ex)
                Return 1
            End Try
        End Function

        Shared Function Main2() As Integer
            Console.WriteLine(String.Format("Pushed stack type ({1} left on the stack): {0,-30} Stack now: {2}", Type.ToString, Count.ToString, StackState))
            Return 0
        End Function
    End Class
End Namespace