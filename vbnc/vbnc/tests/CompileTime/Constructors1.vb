'Test classes with constructors
Class Constructors1
    Class Test3
        Inherits System.Object
        Private Sub Main()
            Dim cl As Test4
            cl = New Test4("K")
        End Sub
    End Class

    Class Test4
        Inherits Test3
        Sub New(ByVal Test1 As String)
            Dim i As Integer
            i = 2
        End Sub
    End Class
End Class