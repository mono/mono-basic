Imports system

Module M
    Private i As Integer

    Public Property p(ByVal x As Integer) As Integer
        Get
            Return i
        End Get

        Set(ByVal val As Integer)
            i = val
        End Set

    End Property

    Function Main() As Integer
        p(5) = 10
        'Console.WriteLine(p)
    End Function

End Module
