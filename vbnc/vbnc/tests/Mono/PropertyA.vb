Module M
    Private i As Integer

    Public Property p() As Integer
        Get
            Return i
        End Get

        Set(ByVal val As Integer)
            i = val
        End Set

    End Property

    Function Main() As Integer
        p = 10
        If p <> 10 Then
            System.Console.WriteLine("#A1 Property Not Working") : Return 1
        End If
    End Function

End Module
