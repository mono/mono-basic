#If STRICT Then
Option Strict On
#Else
Option Strict Off
#End If

Class TypeConversion
    Sub M()
        Dim out_o As Object
        Dim out_e As System.Enum
        Dim out_v As System.ValueType
        Dim out_d As System.Delegate
        Dim out_ia As IA
        Dim out_ib As IB
        Dim out_ic As IC
        Dim out_sa As SA
        Dim out_da As DA
        Dim out_ca As CA
        Dim out_c_ia As C_IA
        Dim out_e_i As E_I

        Dim in_o As Object
        Dim in_ia As IA
        Dim in_ib As IB
        Dim in_sa As SA
        Dim in_da As DA
        Dim in_ca As CA
        Dim in_c_ia As C_IA
        Dim in_c_ib As C_IB
        Dim in_c_ic As C_IC
        Dim in_str As String
        Dim in_v As System.ValueType
        Dim in_e As System.Enum
        Dim in_d As System.Delegate

        out_ia = CType(in_str, IA)
        out_ia = CType(in_ia, IA)
        out_ia = CType(in_ib, IA)
        out_ia = CType(in_da, IA)
        out_ia = CType(in_ca, IA)
        out_ia = CType(in_c_ia, IA)
        out_ia = in_c_ia
        out_ia = CType(in_c_ib, IA)
        out_ia = in_c_ic
        out_ia = CType(in_o, IA)

        out_ic = in_c_ic

        out_o = in_ia

#If Not STRICT Then
        out_e = in_o
        out_ia = in_o
        out_sa = in_o
        out_ca = in_o
        out_da = in_o
        out_ia = in_ib
        out_ib = in_ia
        out_v = in_sa
        out_sa = in_v
        out_e_i = in_e
        out_ca = in_ia
#End If

        out_e = Nothing
        out_v = Nothing
        out_d = Nothing
        out_ia = Nothing
        out_sa = Nothing
        out_da = Nothing
        out_c_ia = Nothing
    End Sub

    Sub M_InRef(ByRef in_o As Object, ByRef in_ia As IA, ByRef in_ib As IB, ByRef in_sa As SA, ByRef in_da As da, _
          ByRef in_ca As ca, ByRef in_c_ia As c_ia, ByRef in_c_ib As c_ib, ByRef in_c_ic As c_ic, ByRef in_str As String, _
          ByRef in_v As system.valuetype, ByRef in_d As system.delegate, ByRef in_e As system.enum)
        Dim out_o As Object
        Dim out_e As System.Enum
        Dim out_v As System.ValueType
        Dim out_d As System.Delegate
        Dim out_ia As IA
        Dim out_ib As IB
        Dim out_ic As IC
        Dim out_sa As SA
        Dim out_da As DA
        Dim out_ca As CA
        Dim out_c_ia As C_IA
        Dim out_e_i As E_I

        out_ia = CType(in_str, IA)
        out_ia = CType(in_ia, IA)
        out_ia = CType(in_ib, IA)
        out_ia = CType(in_da, IA)
        out_ia = CType(in_ca, IA)
        out_ia = CType(in_c_ia, IA)
        out_ia = in_c_ia
        out_ia = CType(in_c_ib, IA)
        out_ia = in_c_ic
        out_ia = CType(in_o, IA)

        out_ic = in_c_ic

        out_o = in_ia

#If Not STRICT Then
        out_e = in_o
        out_ia = in_o
        out_sa = in_o
        out_ca = in_o
        out_da = in_o
        out_ia = in_ib
        out_ib = in_ia
        out_v = in_sa
        out_sa = in_v
        out_e_i = in_e
        out_ca = in_ia
#End If

        out_e = Nothing
        out_v = Nothing
        out_d = Nothing
        out_ia = Nothing
        out_sa = Nothing
        out_da = Nothing
        out_c_ia = Nothing
    End Sub

    Sub M_OutRef(ByRef out_o As Object, ByRef out_e As system.enum, ByRef out_v As system.valuetype, ByRef out_d As system.delegate, _
                 ByRef out_ia As ia, ByRef out_ib As ib, ByRef out_ic As ic, ByRef out_sa As sa, ByRef out_da As da, ByRef out_ca As ca, _
                 ByRef out_c_ia As c_ia, ByRef out_e_i As e_I)
        Dim in_o As Object
        Dim in_ia As IA
        Dim in_ib As IB
        Dim in_sa As SA
        Dim in_da As DA
        Dim in_ca As CA
        Dim in_c_ia As C_IA
        Dim in_c_ib As C_IB
        Dim in_c_ic As C_IC
        Dim in_str As String
        Dim in_v As System.ValueType
        Dim in_e As System.Enum
        Dim in_d As System.Delegate

        out_ia = CType(in_str, IA)
        out_ia = CType(in_ia, IA)
        out_ia = CType(in_ib, IA)
        out_ia = CType(in_da, IA)
        out_ia = CType(in_ca, IA)
        out_ia = CType(in_c_ia, IA)
        out_ia = in_c_ia
        out_ia = CType(in_c_ib, IA)
        out_ia = in_c_ic
        out_ia = CType(in_o, IA)

        out_ic = in_c_ic

        out_o = in_ia

#If Not STRICT Then
        out_e = in_o
        out_ia = in_o
        out_sa = in_o
        out_ca = in_o
        out_da = in_o
        out_ia = in_ib
        out_ib = in_ia
        out_v = in_sa
        out_sa = in_v
        out_e_i = in_e
        out_ca = in_ia
#End If

        out_e = Nothing
        out_v = Nothing
        out_d = Nothing
        out_ia = Nothing
        out_sa = Nothing
        out_da = Nothing
        out_c_ia = Nothing
    End Sub

    Sub M_InOutRef(ByRef out_o As Object, ByRef out_e As system.enum, ByRef out_v As system.valuetype, ByRef out_d As system.delegate, _
                 ByRef out_ia As ia, ByRef out_ib As ib, ByRef out_ic As ic, ByRef out_sa As sa, ByRef out_da As da, ByRef out_ca As ca, _
                 ByRef out_c_ia As c_ia, ByRef out_e_i As e_I, _
                 ByRef in_o As Object, ByRef in_ia As IA, ByRef in_ib As IB, ByRef in_sa As SA, ByRef in_da As da, _
          ByRef in_ca As ca, ByRef in_c_ia As c_ia, ByRef in_c_ib As c_ib, ByRef in_c_ic As c_ic, ByRef in_str As String, _
          ByRef in_v As system.valuetype, ByRef in_d As system.delegate, ByRef in_e As system.enum)

        out_ia = CType(in_str, IA)
        out_ia = CType(in_ia, IA)
        out_ia = CType(in_ib, IA)
        out_ia = CType(in_da, IA)
        out_ia = CType(in_ca, IA)
        out_ia = CType(in_c_ia, IA)
        out_ia = in_c_ia
        out_ia = CType(in_c_ib, IA)
        out_ia = in_c_ic
        out_ia = CType(in_o, IA)

        out_ic = in_c_ic

        out_o = in_ia

#If Not STRICT Then
        out_e = in_o
        out_ia = in_o
        out_sa = in_o
        out_ca = in_o
        out_da = in_o
        out_ia = in_ib
        out_ib = in_ia
        out_v = in_sa
        out_sa = in_v
        out_e_i = in_e
        out_ca = in_ia
#End If

        out_e = Nothing
        out_v = Nothing
        out_d = Nothing
        out_ia = Nothing
        out_sa = Nothing
        out_da = Nothing
        out_c_ia = Nothing
    End Sub
End Class

Class Intrinsic
    Sub NumericConversionsEnum()
        Dim e_b As e_b
        Dim e_i As e_i
        Dim b As Byte
        Dim i As Integer
#If Not STRICT Then
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
#End If
    End Sub

    Sub StringConversions()
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
#If Not STRICT Then
        out_b = in_str
        out_sb = in_str
        out_us = in_str
        out_s = in_str
        out_ui = in_str
        out_i = in_str
        out_ul = in_str
        out_l = in_str
        out_dec = in_str
        out_sng = in_str
        out_dbl = in_str
        out_dt = in_str

        out_str = in_b
        out_str = in_sb
        out_str = in_us
        out_str = in_s
        out_str = in_ui
        out_str = in_i
        out_str = in_ul
        out_str = in_l
        out_str = in_dec
        out_str = in_sng
        out_str = in_dbl
        out_str = in_dt
#End If
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
#If Not STRICT Then
        out_ic = in_ia
#End If

        out_ia = in_c_ia
        out_ca = in_cb

        out_ia_D6 = in_ia_D7
        out_e_b = in_e_b
        out_byte = in_e_b
        out_integer = in_e_i
        out_enum = in_enum
#If Not STRICT Then
        out_e_i = in_integer
#End If

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
#If Not STRICT Then
        out_s_ic = in_ic
#End If
    End Sub
End Class

Class String1
    Sub M()
        Dim c As Char
        Dim s As String
        Dim c_arr() As Char

        s = c
        s = c_arr
#If Not STRICT Then
        c = s
        c_arr = s
#End If
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
        Dim out_s As Short
        Dim out_ic As ic
        Dim out_ib As ib
        Dim out_id As id(Of String)

        out_i_n = in_i
        out_i_n = in_s
        out_s_n = in_s_n
        out_ic = in_s_ic_n
        out_ib = in_s_ic_n
        out_id = in_s_id_n

#If Not STRICT Then
        out_i = in_i_n
        out_s_n = in_i_n
        out_s_n = in_i
        out_s = in_i_n
#End If
        out_ic = in_s_ic_n
    End Sub
End Class

Class GenericType1A(Of C)
    Shared Sub GenericMethodTypeArgument(Of M)(ByVal a As C, ByVal b As M)
        Dim o As Object
        Dim l As Long

        o = a
        o = b
#If Not STRICT Then
        a = o
        b = o
#End If
        l = CLng(CObj(a))
        l = CLng(CObj(b))
    End Sub
End Class
Class GenericType1B(Of C As Structure)
    Shared Sub GenericMethodTypeArgument(Of M As Structure)(ByVal a As C, ByVal b As M)
        Dim o As Object

        o = a
        o = b
#If Not STRICT Then
        a = o
        b = o
#End If
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
        Dim o2 As IB

        o = a
        o = b
#If Not STRICT Then
        a = o
        b = o
        a = o2
        b = o2
#End If
    End Sub
End Class

Class GenericType2B(Of C As IC)
    Shared Sub GenericMethodTypeArgument(Of M As IC)(ByVal a As C, ByVal b As M)
        Dim o As IA
        Dim o2 As IB
        Dim o3 As IC

        o = a
        o = b
#If Not STRICT Then
        o2 = a
        o2 = b
        o3 = a
        o3 = b
#End If
    End Sub
End Class

Class GenericType3A(Of C As C_IA)
    Shared Sub GenericMethodTypeArgument(Of M As C_IA)(ByVal a As C, ByVal b As M)
        Dim o As IA
        Dim o2 As C_IA_D
        o = a
        o = b
    End Sub
End Class

Class GenericType4A(Of C As C_IA)
    Shared Sub GenericMethodTypeArgument(Of M As C_IA)(ByVal a As C, ByVal b As M)
        Dim enumerableC As system.collections.generic.ienumerable(Of C)
        Dim arrC() As M
        Dim enumerableA As system.collections.generic.ienumerable(Of M)
        Dim arrA() As M

        enumerableC = arrC
        enumerableA = arrA
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
Class C_IA_D
    Inherits C_IA
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
