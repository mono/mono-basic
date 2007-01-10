Imports system

Module M
    Private i As Integer

    Public WriteOnly Property p() As Integer
        Set(ByVal val As Integer)
            i = val
        End Set
    End Property

    Public ReadOnly Property p1() As Integer
        Get
            Return i
        End Get
    End Property


    Function Main() As Integer
    End Function

End Module
