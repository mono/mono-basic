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

Class ArrayType1
    Sub M()
        Dim in_ia() As ia
        Dim in_ic() As ic
        Dim in_ca() As ca
        Dim in_cb() As cb
        Dim in_ia2(,) As ia
        Dim in_ia_D7(7) As ia
        Dim in_ia_R2()() As ia
        Dim in_c_ia() As c_ia
        Dim in_e_b() As e_b
        Dim in_e_i() As e_i
        Dim in_byte() As Byte
        Dim in_integer() As Integer
        Dim in_enum() As system.enum
        Dim in_vt() As system.valuetype

        Dim out_ia() As ia
        Dim out_ic() As ic
        Dim out_ca() As ca
        Dim out_cb() As cb
        Dim out_c_ia() As c_ia
        Dim out_ia_D6(6) As ia
        Dim out_e_b() As e_b
        Dim out_e_i() As e_i
        Dim out_byte() As Byte
        Dim out_integer() As Integer
        Dim out_enum() As system.enum
        Dim out_vt() As system.valuetype
        Dim out_obj() As Object
        Dim out As system.array

        out_ia = in_ia
        out_ia = in_ic

        out_ia = in_c_ia
        out_ca = in_cb

        out_ia_D6 = in_ia_D7
        out_e_b = in_e_b
        out_byte = in_e_b
        out_integer = in_e_i
        out_enum = in_enum

        out_obj = in_enum
        out_obj = in_vt
        out = in_enum
        out = in_vt
        out = in_integer
        out = in_e_i
    End Sub

    Sub TG1(Of X As Structure)()
        Dim in_a() As X
        Dim in_b() As X
        in_a = in_b
    End Sub
End Class

Class ValueType1
    Sub M()
        Dim in_sa As sa
        Dim in_sb As sb
        Dim in_s_ic As s_ic
        Dim in_ia As ia
        Dim in_ic As ic
        Dim in_id As id(Of Object)
        Dim in_s_id As s_id(Of Object)

        Dim out_sa As sa
        Dim out_sb As sb
        Dim out_s_ic As s_ic
        Dim out_ia As ia
        Dim out_ic As ic
        Dim out_vt As system.valuetype
        Dim out As Object
        Dim out_id As id(Of Object)
        Dim out_id_str As id(Of String)

        out_sa = in_sa
        out_ia = in_s_ic
        out_ic = in_s_ic
        out_vt = in_sa
        out_vt = in_s_ic
        out = in_sa
        out_id = in_s_id
        out_id_str = in_s_id
    End Sub
End Class

Class GenericType1A(Of C)
    Shared Sub GenericMethodTypeArgument(Of M)(ByVal a As C, ByVal b As M)
        Dim o As Object
        Dim l As Long

        o = a
        o = b
        l = CLng(CObj(a))
        l = CLng(CObj(b))
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

Interface IA

End Interface
Interface IB

End Interface
Interface IC
    Inherits IA, IB
End Interface
Interface ID(Of T)

End Interface

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

Enum E_B As Byte
    a
End Enum
Enum E_I As Integer
    a
End Enum

Structure SA
    Dim v As Integer
End Structure
Structure SB
    Dim v As Integer
End Structure
Structure S_IC
    Implements IC
    Dim v As Integer
End Structure
Structure S_ID(Of T)
    Implements ID(Of T)
    Dim v As Integer
End Structure
