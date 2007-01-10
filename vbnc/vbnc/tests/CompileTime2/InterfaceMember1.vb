Class InterfaceMember1
    Interface I1
        Function F(Of T)() As T
    End Interface
    Class C1
        Implements I1

        Function F(Of T)() As T Implements I1.F

        End Function
    End Class
    Shared Function Main() As Integer

    End Function
End Class