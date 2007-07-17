Namespace TypeParameter1
    Class C1(Of Y)
    End Class
    Class C2(Of Z)
        Inherits C1(Of Z)
    End Class
    Class C3
        Sub M1(Of A As New)()
        End Sub
        Sub M2(Of B)()
        End Sub
    End Class
End Namespace