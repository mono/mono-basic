Class C
End Class
Interface I
End Interface

Class G(Of A As I)

End Class

Module ArgumentTypeInference

    Sub C2(Of T)(ByVal z As G(Of T))
    End Sub
End Module