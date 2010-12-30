Class Nothing1
    Dim bo As Boolean = Nothing
    Dim b As Byte = Nothing
    Dim sb As SByte = Nothing
    Dim s As Short = Nothing
    Dim us As UShort = Nothing
    Dim i As Integer = Nothing
    Dim ui As UInteger = Nothing
    Dim l As Long = Nothing
    Dim ul As ULong = Nothing
    Dim f As Single = Nothing
    Dim r As Double = Nothing
    Dim d As Decimal = Nothing
    Dim dt As Date = Nothing
    Dim c As Char = Nothing
    Dim n As dbnull = Nothing
End Class
Class Nothing2
    Dim bo As Boolean = CBool(Nothing)
    Dim b As Byte = CByte(Nothing)
    Dim sb As SByte = CSByte(Nothing)
    Dim s As Short = CShort(Nothing)
    Dim us As UShort = CUShort(Nothing)
    Dim i As Integer = CInt(Nothing)
    Dim ui As UInteger = CUInt(Nothing)
    Dim l As Long = CLng(Nothing)
    Dim ul As ULong = CULng(Nothing)
    Dim f As Single = CSng(Nothing)
    Dim r As Double = CDbl(Nothing)
    Dim d As Decimal = CDec(Nothing)
    Dim dt As Date = CDate(Nothing)
    Dim c As Char = CChar(Nothing)
End Class
Class Nothing3
    Dim bo As Boolean = CType(Nothing, Boolean)
    Dim b As Byte = CType(Nothing, Byte)
    Dim sb As SByte = CType(Nothing, SByte)
    Dim s As Short = CType(Nothing, Short)
    Dim us As Short = CType(Nothing, UShort)
    Dim i As Integer = CType(Nothing, Integer)
    Dim ui As UInteger = CType(Nothing, UInteger)
    Dim l As Long = CType(Nothing, Long)
    Dim ul As ULong = CType(Nothing, ULong)
    Dim f As Single = CType(Nothing, Single)
    Dim r As Double = CType(Nothing, Double)
    Dim d As Decimal = CType(Nothing, Decimal)
    Dim dt As Date = CType(Nothing, Date)
    Dim c As Char = CType(Nothing, Char)
    Dim n As dbnull = CType(Nothing, dbnull)
End Class