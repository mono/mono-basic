Class End2
    Shared Sub Main()
        End
    End Sub

    Sub InstanceSub()
        End
    End Sub

    Function InstanceFunction() As Object
        End
    End Function

    Shared Function SharedFunction() As Object
        End
    End Function

    ReadOnly Property ReadOnlyInstanceProperty As String
        Get
            End
        End Get
    End Property

    WriteOnly Property WriteOnlyInstanceProperty As String
        Set(ByVal value As String)
            End
        End Set
    End Property

    Property InstanceProperty As String
        Get
            End
        End Get
        Set(ByVal value As String)
            End
        End Set
    End Property

    Shared ReadOnly Property SharedReadOnlyInstanceProperty As String
        Get
            End
        End Get
    End Property

    Shared WriteOnly Property SharedWriteOnlyInstanceProperty As String
        Set(ByVal value As String)
            End
        End Set
    End Property

    Shared Property SharedInstanceProperty As String
        Get
            End
        End Get
        Set(ByVal value As String)
            End
        End Set
    End Property

End Class
