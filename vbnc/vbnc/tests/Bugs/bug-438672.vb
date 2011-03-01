Class c
    Const b As Byte = CType(2UL, Byte)
    Const sb As SByte = CType(2UL, SByte)
    Const s As Short = CType(2UL, Short)
    Const us As UShort = CType(2UL, UShort)
    Const i As Integer = CType(2UL, Integer)
    Const ui As UInteger = CType(2UL, UInteger)
    Const l As Long = CType(2UL, Long)
    Const ul As ULong = CType(2D, ULong)
    Const sng As Single = CType(2.0, Single)
    Const dbl As Double = CType(2D, Double)
    Const dec As Decimal = CType(1, Decimal)
    Const dt As Date = CType(#12:00:00 AM#, Date)
    Const chr As Char = CType("a", Char)
    Const str As String = CType("bb", String)
    Const boo As Boolean = CType(True, Boolean)

    Shared Sub main()
    End Sub
End Class

Class c2
    Const b As Byte = CType(Nothing, Byte)
    Const sb As SByte = CType(Nothing, SByte)
    Const s As Short = CType(Nothing, Short)
    Const us As UShort = CType(Nothing, UShort)
    Const i As Integer = CType(Nothing, Integer)
    Const ui As UInteger = CType(Nothing, UInteger)
    Const l As Long = CType(Nothing, Long)
    Const ul As ULong = CType(Nothing, ULong)
    Const sng As Single = CType(Nothing, Single)
    Const dbl As Double = CType(Nothing, Double)
    Const dec As Decimal = CType(Nothing, Decimal)
    Const dt As Date = CType(Nothing, Date)
    Const chr As Char = CType(Nothing, Char)
    Const str As String = CType(Nothing, String)
    Const boo As Boolean = CType(Nothing, Boolean)
End Class