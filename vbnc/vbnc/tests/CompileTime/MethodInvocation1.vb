Class MethodInvocation1_Base
    Overridable Sub Dispose(ByVal disposing As Boolean)

    End Sub
    Sub Dispose()

    End Sub
End Class

Class MethodInvocation1_Derived
    Inherits MethodInvocation1_Base

    Overrides Sub Dispose(ByVal disposing As Boolean)

    End Sub

    Sub Test()
        Dim m As New MethodInvocation1_Derived
        m.Dispose()
    End Sub

End Class