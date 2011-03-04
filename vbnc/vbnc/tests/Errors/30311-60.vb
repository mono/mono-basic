Option Strict On

Class TypeConversion
    Shared Sub Main()
        Dim o As Object
        Dim e As System.Enum
        Dim v As System.ValueType
        Dim d As System.Delegate

        Dim ia As IA
        Dim ib As IB
        Dim ic As IC
        Dim sa As SA
        Dim da As DA
        Dim ca As CA
        Dim c_ia As C_IA
        Dim c_ib As C_IB
        Dim c_ic As C_IC

        ia = CType("", IA)
        ia = CType(ia, IA)
        ia = CType(ib, IA)
        ia = CType(da, IA)
        ia = CType(ca, IA)
        ia = CType(c_ia, IA)
        ia = c_ia
        ia = CType(c_ib, IA)
        ia = c_ic
        ia = CType(o, IA)

        ic = c_ic

        o = ia

    End Sub

End Class

Class GenericType1A(Of C)
    Shared Sub GenericMethodTypeArgument(Of M)(ByVal a As C, ByVal b As M)
        Dim o As Object

        o = a
        o = b
    End Sub
End Class
Class GenericType1B(Of C As Structure)
    Shared Sub GenericMethodTypeArgument(Of M As Structure)(ByVal a As C, ByVal b As M)
        Dim o As Object

        o = a
        o = b
    End Sub
End Class
Class GenericType1C(Of C As Class)
    Shared Sub GenericMethodTypeArgument(Of M As Class)(ByVal a As C, ByVal b As M)
        Dim o As Object

        o = a
        o = b
    End Sub
End Class
Class GenericType1D(Of C As New)
    Shared Sub GenericMethodTypeArgument(Of M)(ByVal a As C, ByVal b As M)
        Dim o As Object

        o = a
        o = b
    End Sub
End Class

Class GenericType2A(Of C As IA)
    Shared Sub GenericMethodTypeArgument(Of M As IA)(ByVal a As C, ByVal b As M)
        Dim o As IA

        o = a
        o = b
    End Sub
End Class

Class GenericType2B(Of C As IC)
    Shared Sub GenericMethodTypeArgument(Of M As IC)(ByVal a As C, ByVal b As M)
        Dim o As IA
        Dim o2 As IB
        Dim o3 As IC

        o = a
        o = b
        o2 = a
        o2 = b
        o3 = a
        o3 = b
    End Sub
End Class

Class GenericType3A(Of C As C_IA)
    Shared Sub GenericMethodTypeArgument(Of M As C_IA)(ByVal a As C, ByVal b As M)
        Dim o As IA

        o = a
        o = b
    End Sub
End Class

Class GenericType4A(Of C1 As C_IA, C2 As C_IA)
    Shared Sub GenericMethodTypeArgument(Of M1 As C_IA, M2 As C_IA)(ByVal a As C1, ByVal b As M1)
        'Dim aa As C2
        Dim bb As M2
        'aa = a
        bb = b
    End Sub
End Class

Interface IA

End Interface
Interface IB

End Interface
Interface IC
    Inherits IA, IB
End Interface

Structure SA
    Dim v As Integer
End Structure

Delegate Sub DA()

Class CA
End Class

Class CB
    Inherits CA
End Class

Class C_IA
    Implements IA
End Class
Class C_IB
    Implements IB
End Class
Class C_IC
    Implements IC
End Class