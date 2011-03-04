Option Strict Off

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
        e = o
        ia = o
        sa = o
        ca = o
        da = o
        ia = ib
        ib = ia
        da = sa

        e = Nothing
        v = Nothing
        d = Nothing
        ia = Nothing
        sa = Nothing
        da = Nothing
        c_ia = Nothing
    End Sub

End Class

Class Intrinsic
    Sub BooleanConversions()
        Dim out_b As Byte
        Dim out_sb As SByte
        Dim out_s As Short
        Dim out_us As UShort
        Dim out_i As Integer
        Dim out_ui As UInteger
        Dim out_l As Long
        Dim out_ul As ULong
        Dim out_dec As Decimal
        Dim out_bool As Boolean
        Dim out_sng As Single
        Dim out_dbl As Double
        Dim out_str As String
        Dim out_chr As Char
        Dim out_dt As Date
        Dim value As Boolean
        Dim in_b As Byte
        Dim in_sb As SByte
        Dim in_s As Short
        Dim in_us As UShort
        Dim in_i As Integer
        Dim in_ui As UInteger
        Dim in_l As Long
        Dim in_ul As ULong
        Dim in_dec As Decimal
        Dim in_bool As Boolean
        Dim in_sng As Single
        Dim in_dbl As Double
        Dim in_str As String
        Dim in_chr As Char
        Dim in_dt As Date

        out_b = value
        out_sb = value
        out_s = value
        out_us = value
        out_i = value
        out_ui = value
        out_l = value
        out_ul = value
        out_dec = value
        out_sng = value
        out_dbl = value

        value = in_b
        value = in_sb
        value = in_s
        value = in_us
        value = in_i
        value = in_ui
        value = in_l
        value = in_ul
        value = in_dec
        value = in_sng
        value = in_dbl
    End Sub

    Sub NumericConversions()
        Dim out_b As Byte
        Dim out_sb As SByte
        Dim out_s As Short
        Dim out_us As UShort
        Dim out_i As Integer
        Dim out_ui As UInteger
        Dim out_l As Long
        Dim out_ul As ULong
        Dim out_dec As Decimal
        Dim out_bool As Boolean
        Dim out_sng As Single
        Dim out_dbl As Double
        Dim out_str As String
        Dim out_chr As Char
        Dim out_dt As Date

        Dim in_b As Byte
        Dim in_sb As SByte
        Dim in_s As Short
        Dim in_us As UShort
        Dim in_i As Integer
        Dim in_ui As UInteger
        Dim in_l As Long
        Dim in_ul As ULong
        Dim in_dec As Decimal
        Dim in_bool As Boolean
        Dim in_sng As Single
        Dim in_dbl As Double
        Dim in_str As String
        Dim in_chr As Char
        Dim in_dt As Date

        out_sb = in_b

        out_b = in_sb
        out_us = in_sb
        out_ui = in_sb
        out_ul = in_sb

        out_b = in_us
        out_sb = in_us
        out_s = in_us

        out_b = in_s
        out_sb = in_s
        out_ui = in_s
        out_ul = in_s

        out_b = in_ui
        out_sb = in_ui
        out_us = in_ui
        out_s = in_ui
        out_i = in_ui

        out_b = in_i
        out_sb = in_i
        out_us = in_i
        out_s = in_i
        out_ui = in_i
        out_ul = in_i

        out_b = in_ul
        out_sb = in_ul
        out_us = in_ul
        out_s = in_ul
        out_ui = in_ul
        out_i = in_ul
        out_l = in_ul

        out_b = in_l
        out_sb = in_l
        out_us = in_l
        out_s = in_l
        out_ui = in_l
        out_i = in_l
        out_ul = in_l

        out_b = in_dec
        out_sb = in_dec
        out_us = in_dec
        out_s = in_dec
        out_ui = in_dec
        out_i = in_dec
        out_ul = in_dec
        out_l = in_dec

        out_b = in_sng
        out_sb = in_sng
        out_us = in_sng
        out_s = in_sng
        out_ui = in_sng
        out_i = in_sng
        out_ul = in_sng
        out_l = in_sng
        out_dec = in_sng

        out_b = in_dbl
        out_sb = in_dbl
        out_us = in_dbl
        out_s = in_dbl
        out_ui = in_dbl
        out_i = in_dbl
        out_ul = in_dbl
        out_l = in_dbl
        out_dec = in_dbl
        out_sng = in_dbl
    End Sub

    Sub NumericConversionsEnum()
        Dim e_b As e_b
        Dim e_i As e_i
        Dim b As Byte
        Dim i As Integer

        e_b = 2
        e_i = 4

        e_b = b
        e_i = i
        e_b = i
        e_i = b
        b = e_b
        b = e_i
        i = e_b
        i = e_i
        e_b = e_i
        e_i = e_b
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
        Dim in_s_id_byte As s_id(Of Byte)

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
    End Sub
End Class

Class String1
    Sub M()
        Dim c As Char
        Dim s As String
        Dim c_arr() As Char

        s = c
        s = c_arr
    End Sub
End Class

Class Nullable1
    Sub M()
        Dim in_i_n As Integer?
        Dim in_i As Integer
        Dim in_s As Short
        Dim in_s_n As Short?
        Dim in_s_ic_n As s_ic?
        Dim in_s_id_n As s_id(Of String)?

        Dim out_i_n As Integer?
        Dim out_i As Integer
        Dim out_s_n As Short?
        Dim out_ic As ic
        Dim out_ib As ib
        Dim out_id As id(Of String)

        out_i_n = in_i
        out_i_n = in_s
        out_s_n = in_s_n
        out_ic = in_s_ic_n
        out_ib = in_s_ic_n
        out_id = in_s_id_n
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
