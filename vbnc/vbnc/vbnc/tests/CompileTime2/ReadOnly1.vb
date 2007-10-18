Class ReadOnly1
    ReadOnly instance As Integer
    Shared ReadOnly not_instance As Integer
    Shared Sub New()
        not_instance = 3
    End Sub
    Sub New()
        instance = 2
    End Sub

    Shared Function Main() As Integer
        Return not_instance - 3
    End Function
End Class