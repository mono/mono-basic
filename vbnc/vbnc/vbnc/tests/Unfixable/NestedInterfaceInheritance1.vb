'This test fails and I can't figure out why.
Class NestedInterfaceInheritance1
    Implements IDerived 'Change to IBase and it compiles.

    Interface IBase
        Function Method() As Integer
    End Interface
    Interface IDerived
        Inherits IBase
    End Interface

    Function Method() As Integer Implements IBase.Method
        Return 1
    End Function
End Class