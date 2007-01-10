Class Constructors2
    Class Test1
        Sub New()
            MyBase.new()
        End Sub
        Sub New(ByVal Test As String)
        End Sub
    End Class

    Class Test2
        Inherits Test1
        Sub New(ByVal Test1 As String)
            Dim i As Integer
            i = 2
        End Sub
        Sub New()
            MyBase.new("")
        End Sub
    End Class
End Class