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

        Dim from As Nullable(Of From)
        Dim [to] As Nullable(Of [To])

        [to] = [from]

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

        s = f


    End Function

    Structure From
        Dim i As Integer
        Public Shared Widening Operator CType(ByVal a As From) As [To]
            Return New [To]
        End Operator
    End Structure

    Structure [To]
        Dim i As Integer
    End Structure

End Class