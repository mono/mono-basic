Namespace Generic1
    Class G1(Of T1)

    End Class

    Class G2(Of T2)
        Inherits G1(Of T2)
    End Class

    Class G3
        Inherits G2(Of Integer)
    End Class

    Class Tester
        Shared Function Main() As Integer
            Dim T1 As New G1(Of Integer)
            Dim T2 As New G2(Of String)
            Dim T3 As New G3
        End Function
    End Class
End Namespace