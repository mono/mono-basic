Namespace EnumConstant4
    Enum E
        V
    End Enum
    Class C
        Shared Sub S(ByVal i As Long)
            S(E.V)
        End Sub
    End Class
End Namespace