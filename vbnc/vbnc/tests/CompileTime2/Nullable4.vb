Option Strict On

Class Nullable4
    Shared Sub Main()
        Dim bool As Boolean?
        Dim b As Byte?
        Dim sb As SByte?
        Dim s As Short?
        Dim us As UShort?
        Dim i As Integer?
        Dim ui As UInteger?
        Dim l As Long?
        Dim ul As Long?
        Dim dec As Decimal?
        Dim f As Single?
        Dim r As Double?
        Dim dt As Date?
        Dim c As Char?

        s = b
        s = CByte(1)
        s = sb
        s = CSByte(1)

        us = b
        us = CByte(1)

        i = b
        i = CByte(1)
        i = sb
        i = CSByte(1)
        i = s
        i = CShort(1)
        i = us
        i = CUShort(1)

        ui = b
        ui = CByte(1)
        ui = us
        ui = CUShort(1)

        l = b
        l = CByte(1)
        l = sb
        l = CSByte(1)
        l = s
        l = CShort(1)
        l = us
        l = CUShort(1)
        l = i
        l = CInt(1)
        l = ui
        l = CUInt(1)

        ul = b
        ul = CByte(1)
        ul = us
        ul = CUShort(1)
        ul = ui
        ul = CUInt(1)


        dec = b
        dec = CByte(1)
        dec = sb
        dec = CSByte(1)
        dec = s
        dec = CShort(1)
        dec = us
        dec = CUShort(1)
        dec = i
        dec = CInt(1)
        dec = ui
        dec = CUInt(1)
        dec = l
        dec = CLng(1)
        dec = ul
        dec = CULng(1)

        f = b
        f = CByte(1)
        f = sb
        f = CSByte(1)
        f = s
        f = CShort(1)
        f = us
        f = CUShort(1)
        f = i
        f = CInt(1)
        f = ui
        f = CUInt(1)
        f = l
        f = CLng(1)
        f = ul
        f = CULng(1)
        f = dec
        f = CDec(1)


        r = b
        r = CByte(1)
        r = sb
        r = CSByte(1)
        r = s
        r = CShort(1)
        r = us
        r = CUShort(1)
        r = i
        r = CInt(1)
        r = ui
        r = CUInt(1)
        r = l
        r = CLng(1)
        r = ul
        r = CULng(1)
        r = dec
        r = CDec(1)
        r = f
        r = CSng(1)

    End Sub
End Class