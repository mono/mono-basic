Class InterfaceInheritance2
    Interface IBase
        Function Method() As Integer
    End Interface
    Interface IIntermediate1
        Inherits IBase
    End Interface
    Interface IIntermediate2
        Inherits IBase
    End Interface
    Interface IDerived
        Inherits IIntermediate1, IIntermediate2
    End Interface

    Class Consumer
        Implements IDerived
        Function Method() As Integer Implements IBase.Method
            Return 0
        End Function
    End Class

    Shared Function Main() As Integer
        Dim c As New Consumer
        Dim i As IDerived
        i = c
        Return i.Method
    End Function
End Class
