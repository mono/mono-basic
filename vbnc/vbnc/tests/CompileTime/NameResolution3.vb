Class NameResolution3_Type
End Class
Class NameResolution3_Test
    ReadOnly Property NameResolution3_Type() As NameResolution3_Type
        Get
        End Get
    End Property
End Class
Class NameResolution3_TestInheriter
    Inherits nameresolution3_test
    Sub test()
        Dim i As nameresolution3_type
        i = nameresolution3_type
    End Sub
End Class