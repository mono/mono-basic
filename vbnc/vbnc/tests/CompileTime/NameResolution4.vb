Class NameResolution4_Type
End Class

Class NameResolution4_TestInheriter
    Inherits nameresolution4_test
    Dim value As New nameresolution4_testinheriter(nameresolution4_type)
    Sub New(ByVal param As nameresolution4_type)
    End Sub
End Class

Class NameResolution4_Test
    ReadOnly Property NameResolution4_Type() As NameResolution4_Type
        Get
        End Get
    End Property
End Class