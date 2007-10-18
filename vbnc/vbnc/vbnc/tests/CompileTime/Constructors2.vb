'Test classes with constructors
Class Constructors2
    Class Test3
        Inherits System.Object

        Sub New(ByVal Test1 As String)
        End Sub
    End Class

    Class Test4
        Inherits Test3

        Sub New(ByVal Test1 As String)
            MyBase.New(Test1)
        End Sub
    End Class

End Class