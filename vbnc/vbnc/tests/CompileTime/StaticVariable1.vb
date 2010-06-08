Class StaticVariable1
    Function T() As Integer
        Static i As Integer
        i = 1
    End Function

    Shared Function Main() As Integer
        Return 0
    End Function
End Class