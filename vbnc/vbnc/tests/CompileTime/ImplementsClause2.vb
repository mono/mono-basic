
Namespace ImplementsClause2
    Public Class Class1 : Implements TestInterface
        Public ReadOnly Property Test() As Boolean Implements TestInterface.Test
            Get
                Return True
            End Get
        End Property

        Public Sub TestSub() Implements TestInterface.TestSub

        End Sub

        Public Sub SubInterface() Implements SubInterface.SubInterface

        End Sub
    End Class
    Interface TestInterface : Inherits SubInterface
        Sub TestSub()
        ReadOnly Property Test() As Boolean
    End Interface

    Interface SubInterface
        Sub SubInterface()
    End Interface

End Namespace
