Namespace GenericTypeParameters9
    Class G2

    End Class
    Class G1
        Sub T(Of V As g2)(ByVal P As V)
            Dim A As G2 = DirectCast(P, G2)
            Dim B As G2 = CType(P, G2)
            Dim C As G2 = P
            Dim D As G2 = TryCast(P, G2)
        End Sub
    End Class
End Namespace