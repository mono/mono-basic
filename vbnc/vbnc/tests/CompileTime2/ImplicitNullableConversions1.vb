Option Strict On
Class ImplicitNullableConversions1
    Shared Function Main() As Integer
        Dim b As Nullable(Of Byte)
        Dim sb As Nullable(Of SByte)
        Dim i16 As Nullable(Of Short)
        Dim u16 As Nullable(Of UShort)
        Dim i32 As Nullable(Of Integer)
        Dim u32 As Nullable(Of UInteger)
        Dim i64 As Nullable(Of Long)
        Dim u64 As Nullable(Of ULong)
        Dim s As Nullable(Of Single)
        Dim f As Nullable(Of Double)
        Dim d As Nullable(Of Decimal)
        Dim dt As Nullable(Of Date)
        Dim c As Nullable(Of Char)
        Dim boo As Nullable(Of Boolean)

        d = u64
        d = u32
        d = u16
        d = b
        d = i64
        d = i32
        d = i16
        d = sb

        f = d
        f = s
        f = u64
        f = u32
        f = u16
        f = b
        f = i64
        f = i32
        f = i16
        f = sb

        s = u64
        s = u32
        s = u16
        s = b
        s = i64
        s = i32
        s = i16
        s = sb

        u64 = u32
        u64 = u16
        u64 = b

        u32 = u16
        u32 = b

        u16 = b

        i64 = i32
        i64 = i16
        i64 = sb
        i64 = u32
        i64 = u16
        i64 = b

        i32 = i16
        i32 = sb
        i32 = u16
        i32 = b

        i16 = sb
        i16 = b

    End Function
End Class