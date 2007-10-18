
Namespace ImplementsClause1
    Public Class Class1 : Implements TestInterface
        Public ReadOnly Property Test() As Boolean Implements TestInterface.Test
            Get
                Return True
            End Get
        End Property

        Public Sub TestSub() Implements TestInterface.TestSub

        End Sub
    End Class
    Interface TestInterface
        Sub TestSub()
        ReadOnly Property Test() As Boolean
    End Interface

End Namespace
