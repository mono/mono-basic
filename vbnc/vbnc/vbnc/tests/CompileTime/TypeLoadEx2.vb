Namespace TypeLoadEx2
    Class Base(Of T)
    End Class
    Class Derived
        Inherits Base(Of Integer)
    End Class
    Public Class BaseObject
        Sub ErrorCreator(Of Type As Class)()
        End Sub
    End Class
End Namespace
