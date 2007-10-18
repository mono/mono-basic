Namespace Constructors3
    Class ClassA
        Sub New()
        End Sub
        Sub New(ByVal builtinvar As Integer)
        End Sub
        Sub New(ByRef builtinbyrefvar As Short)
        End Sub
        Sub New(ByVal builtinarrayvar() As Integer)
        End Sub
        Sub New(ByRef builtinarrayvar2 As Long())
        End Sub
        Sub New(ByVal objvar As Object)
        End Sub
        Sub New(ByRef byrefobjvar As String)
        End Sub
        Sub New(ByVal classvar As classa)
        End Sub
        Sub New(ByRef classbyrefvar As classb)
        End Sub
        Sub New(ByVal classarrayvar() As classa)
        End Sub
        Sub New(ByRef classbyrefarrayvar As classb())
        End Sub
        Sub New(ByVal structvar As structurea)
        End Sub
        Sub New(ByRef structbyrefvar As structureb)
        End Sub
        Sub New(ByVal structarrayvar As structurea())
        End Sub
        Sub New(ByRef structbyrefarrayvar() As structureb)
        End Sub
        Sub New(ByVal interfacevar As interfacea)
        End Sub
        Sub New(ByRef interfacebyrefvar As interfaceb)
        End Sub
        Sub New(ByVal interfacearrayvar As interfacea())
        End Sub
        Sub New(ByRef interfacebyrefarrayvar() As interfaceb)
        End Sub
        Sub New(ByVal delegatevar As delegatea)
        End Sub
        Sub New(ByRef delegatebyrefvar As delegateb)
        End Sub
        Sub New(ByVal delegatearrayvar As delegatea())
        End Sub
        Sub New(ByRef delegatebyrefarrayvar() As delegateb)
        End Sub
        Sub New(ByVal enumvar As enuma)
        End Sub
        Sub New(ByRef enumbyrefvar As enumb)
        End Sub
        Sub New(ByVal enumarrayvar As enuma())
        End Sub
        Sub New(ByRef enumbyrefarrayvar() As enumb)
        End Sub
    End Class
    Class ClassB
        Public Sub New()
        End Sub
        Private Sub New(ByVal privatenew As Integer)
        End Sub
        Protected Sub New(ByVal protectednew As Short)
        End Sub
        Friend Sub New(ByVal friendnew As Byte)
        End Sub
        Protected Friend Sub New(ByVal protectedfriendnew As Long)
        End Sub
    End Class
    Class ClassC
        Shared Sub New()
        End Sub
        Sub New()
        End Sub
    End Class
    Class ClassD
        Shared Sub New()
        End Sub
    End Class
    Class ClassE
    End Class
    Class ClassF
        Sub New()
        End Sub
    End Class
    Class ClassG
        Sub New(ByVal nodefaultconstructor As Date)
        End Sub
    End Class

    Structure StructureA
        Dim value As Integer

        Sub New(ByVal value As Integer)
        End Sub
        Shared Sub New()
        End Sub
    End Structure
    Structure StructureB
        Dim value As Integer
        Shared Sub New()
        End Sub
    End Structure

    Interface InterfaceA
    End Interface
    Interface InterfaceB
    End Interface

    Enum EnumA
        value
    End Enum
    Enum EnumB
        value
    End Enum

    Delegate Sub DelegateA()
    Delegate Function DelegateB() As String
End Namespace
