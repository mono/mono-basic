Class DefaultProperty2
    Class Dictionary
        Inherits system.collections.generic.dictionary(Of String, Integer)
    End Class
    Private Shared dic As New Dictionary

    Class Test
        Sub New(ByVal i As Integer)

        End Sub
    End Class

    Shared Function Main() As Integer
        Dim i As Integer
        dic.add("1", 1)
        i = dic("1")
        Dim o As Object = New test(dic("1"))
        Return i - 1
    End Function
End Class