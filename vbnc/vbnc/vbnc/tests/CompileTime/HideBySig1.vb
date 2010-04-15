Imports System
Namespace HideBySig1
    Class A1
        Sub [GetType]()

        End Sub
    End Class
    Class A2
        Sub [GetType](ByVal i As String)

        End Sub
    End Class
    Class A3
        Inherits A2

        Sub [GetType](ByVal i As Integer)

        End Sub
        Sub [GetType](ByVal i As String)

        End Sub
    End Class
    Class A4
        Overloads Sub [GetType](Of X)()

        End Sub
        Overloads Function [GetType]() As Type

        End Function
    End Class
End Namespace