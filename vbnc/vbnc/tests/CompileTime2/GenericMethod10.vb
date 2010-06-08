Class C(Of T)

    Shadows Sub Test()
        Dim i As T
        Add(i)
    End Sub

    Shadows Sub Add(ByVal Base As T)
    End Sub

End Class

Class TT
    Shared Function Main() As Integer
        Dim c As New C(Of Object)
        c.Test()
    End Function
End Class