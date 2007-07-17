<System.Reflection.DefaultMember("Item")> _
Class DefaultProperty3
    Default ReadOnly Property Item(ByVal i As Integer) As String
        Get
            Return Nothing
        End Get
    End Property
    Default ReadOnly Property Item(ByVal i As String) As String
        Get
            Return Nothing
        End Get
    End Property

    Shared Function Main() As Integer
        Try
            Dim obj As Object
            obj = System.Attribute.GetCustomAttribute(GetType(DefaultProperty3), GetType(System.Reflection.DefaultMemberAttribute), False)
        Catch ex As Exception
            System.Console.WriteLine(ex.Message)
            Return 1
        End Try
    End Function
End Class