Class G(Of A As New)

End Class

Module ArgumentTypeInference

    Sub C2(Of T)(ByVal z As G(Of T))
    End Sub
End Module