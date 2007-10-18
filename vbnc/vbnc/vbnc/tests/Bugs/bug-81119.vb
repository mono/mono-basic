Public Class PropTestClass

    Private poActiveObject As Object = Nothing

    Public Property ActiveObject() As Object
        Get
            Return poActiveObject
        End Get
        Set(ByVal aoValue As Object)
            poActiveObject = aoValue
        End Set
    End Property

    Shared Sub Main()

    End Sub
End Class