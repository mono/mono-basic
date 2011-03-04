Option Strict On
Class E
    Sub Test()
        Dim b As Byte
        Dim sb As SByte
        Dim s As Short
        Dim us As UShort
        Dim i As Integer
        Dim ui As UInteger
        Dim l As Long
        Dim ul As ULong

        Dim eb As b
        Dim esb As sb
        Dim es As s
        Dim eus As us
        Dim ei As i
        Dim eui As ui
        Dim el As l
        Dim eul As ul

        b = eb
        b = Not eb
        b = eb And (+eb) Or eb
        b = eb Or eb
        b = eb And eb
        b = eb And (Not eb) Or eb
        eb = eb And (Not eb) Or eb

        eus = eus.a And (Not (eus.b Or eus.c))
        Dim eus2 As us = eus.a And (Not (eus.b Or eus.c))

    End Sub
End Class

Enum B As Byte
    a
End Enum
Enum SB As SByte
    a
End Enum
Enum S As Short
    a
End Enum
Enum US As UShort
    a
    b
    c
End Enum
Enum I As Integer
    a
End Enum
Enum UI As UInteger
    a
End Enum
Enum L As Long
    a
End Enum
Enum UL As ULong
    a
End Enum