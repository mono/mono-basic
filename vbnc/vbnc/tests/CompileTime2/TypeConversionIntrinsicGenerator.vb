#If GENERATOR Then
Option Strict On
Imports System.IO

Module Generator
    '0 = A Empty          A
    '1 = B Object         B
    '2 = C DBNull         C
    '3 = D Boolean        D
    '4 = E Char           E
    '5 = F SByte          F
    '6 = G Byte           G
    '7 = H Int16(Short)   H
    '8 = I UInt16(UShort) I
    '9 = J Int32          J
    '10= K UInt32         K 
    '11= L Int64(Long)    L
    '12= M UInt64(ULong)  M
    '13= N Single         N
    '14= O Double         O
    '15= P Decimal        P
    '16= Q DateTime       Q
    '17= - 17             -
    '18= S String         S

    ''' X=?
    ''' I=Implicit ok
    ''' 0=Explicit ok
    ''' 1=30311
    ''' 2=32007
    ''' 3=30533
    ''' 4=32006
    ''' 5=30532
    ''' 6=30533
    ''' A=30311, only explicit

    Public LikeResultType As String = "" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXBBBBBBBBBBBBBB-B" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXDDDDDDDDDDDDDD-D" & _
            "XBXDDDDDDDDDDDDDD-D" & _
            "XBXDDDDDDDDDDDDDD-D" & _
            "XBXDDDDDDDDDDDDDD-D" & _
            "XBXDDDDDDDDDDDDDD-D" & _
            "XBXDDDDDDDDDDDDDD-D" & _
            "XBXDDDDDDDDDDDDDD-D" & _
            "XBXDDDDDDDDDDDDDD-D" & _
            "XBXDDDDDDDDDDDDDD-D" & _
            "XBXDDDDDDDDDDDDDD-D" & _
            "XBXDDDDDDDDDDDDDD-D" & _
            "XBXDDDDDDDDDDDDDD-D" & _
            "XBXDDDDDDDDDDDDDD-D" & _
            "XBXDDDDDDDDDDDDDD-D" & _
            "-------------------" & _
            "XBXDDDDDDDDDDDDDD-D"
    Public LikeOperandType As String = "" & _
                "XXXXXXXXXXXXXXXXX-X" & _
                "XBXBBBBBBBBBBBBBB-B" & _
                "XXXXXXXXXXXXXXXXX-X" & _
                "XBXSSSSSSSSSSSSSS-S" & _
                "XBXSSSSSSSSSSSSSS-S" & _
                "XBXSSSSSSSSSSSSSS-S" & _
                "XBXSSSSSSSSSSSSSS-S" & _
                "XBXSSSSSSSSSSSSSS-S" & _
                "XBXSSSSSSSSSSSSSS-S" & _
                "XBXSSSSSSSSSSSSSS-S" & _
                "XBXSSSSSSSSSSSSSS-S" & _
                "XBXSSSSSSSSSSSSSS-S" & _
                "XBXSSSSSSSSSSSSSS-S" & _
                "XBXSSSSSSSSSSSSSS-S" & _
                "XBXSSSSSSSSSSSSSS-S" & _
                "XBXSSSSSSSSSSSSSS-S" & _
                "XBXSSSSSSSSSSSSSS-S" & _
                "-------------------" & _
                "XBXSSSSSSSSSSSSSS-S"
    Public LikeAllowedType As String =
            "XXXXXXXXXXXXXXXXX-X" & _
            "X0X00000000000000-0" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "X0X00000000000000-0" & _
            "X0X0I000000000000-I" & _
            "X0X00000000000000-0" & _
            "X0X00000000000000-0" & _
            "X0X00000000000000-0" & _
            "X0X00000000000000-0" & _
            "X0X00000000000000-0" & _
            "X0X00000000000000-0" & _
            "X0X00000000000000-0" & _
            "X0X00000000000000-0" & _
            "X0X00000000000000-0" & _
            "X0X00000000000000-0" & _
            "X0X00000000000000-0" & _
            "X0X00000000000000-0" & _
            "-------------------" & _
            "X0X0I000000000000-I"

    Public ConcatResultType As String = LikeOperandType
    Public ConcatOperandType As String = LikeOperandType
    Public ConcatAllowedType As String =
            "XXXXXXXXXXXXXXXXX-X" & _
            "X0X00000000000000-0" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "X0XIIIIIIIIIIIIII-I" & _
            "X0XIIIIIIIIIIIIII-I" & _
            "X0XIIIIIIIIIIIIII-I" & _
            "X0XIIIIIIIIIIIIII-I" & _
            "X0XIIIIIIIIIIIIII-I" & _
            "X0XIIIIIIIIIIIIII-I" & _
            "X0XIIIIIIIIIIIIII-I" & _
            "X0XIIIIIIIIIIIIII-I" & _
            "X0XIIIIIIIIIIIIII-I" & _
            "X0XIIIIIIIIIIIIII-I" & _
            "X0XIIIIIIIIIIIIII-I" & _
            "X0XIIIIIIIIIIIIII-I" & _
            "X0XIIIIIIIIIIIIII-I" & _
            "X0XIIIIIIIIIIIIII-I" & _
            "-------------------" & _
            "X0XIIIIIIIIIIIIII-I"

    Public ModResultType As String = "" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXBXBBBBBBBBBBBX-B" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXHXFHHJJLLPNOPX-O" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXFXFHHJJLLPNOPX-O" & _
            "XBXHXHGHIJKLMNOPX-O" & _
            "XBXHXHHHJJLLPNOPX-O" & _
            "XBXJXJIJIJKLMNOPX-O" & _
            "XBXJXJJJJJLLPNOPX-O" & _
            "XBXLXLKLKLKLMNOPX-O" & _
            "XBXLXLLLLLLLPNOPX-O" & _
            "XBXPXPMPMPMPMNOPX-O" & _
            "XBXNXNNNNNNNNNONX-O" & _
            "XBXOXOOOOOOOOOOOX-O" & _
            "XBXPXPPPPPPPPNOPX-O" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "-------------------" & _
            "XBXOXOOOOOOOOOOOX-O"
    Public ModAllowedType As String =
            "XXXXXXXXXXXXXXXXX-X" & _
            "X0X0X00000000000X-0" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "X0X0X00000000000X-0" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "-------------------" & _
            "X0X0X00000000000X-0"

    Public IntDivResultTypes As String = "" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXBXBBBBBBBBBBBX-B" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXHXFHHJJLLLLLLX-L" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXFXFHHJJLLLLLLX-L" & _
            "XBXHXHGHIJKLMLLLX-L" & _
            "XBXHXHHHJJLLLLLLX-L" & _
            "XBXJXJIJIJKLMLLLX-L" & _
            "XBXJXJJJJJLLLLLLX-L" & _
            "XBXLXLKLKLKLMLLLX-L" & _
            "XBXLXLLLLLLLLLLLX-L" & _
            "XBXLXLMLMLMLMLLLX-L" & _
            "XBXLXLLLLLLLLLLLX-L" & _
            "XBXLXLLLLLLLLLLLX-L" & _
            "XBXLXLLLLLLLLLLLX-L" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "-------------------" & _
            "XBXLXLLLLLLLLLLLX-L"
    Public IntDivAllowedType As String =
            "XXXXXXXXXXXXXXXXX-X" & _
            "X0X0X00000000000X-0" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "X0X0X00000000000X-0" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "X0X0XIIIIIII0000X-0" & _
            "X0X0XIIIIIIII000X-0" & _
            "X0X0XIIIIIII0000X-0" & _
            "X0X0XIIIIIIII000X-0" & _
            "X0X0XIIIIIII0000X-0" & _
            "X0X0XIIIIIIII000X-0" & _
            "X0X0XIIIIIII0000X-0" & _
            "X0X0X0I0I0I0I000X-0" & _
            "X0X0X00000000000X-0" & _
            "X0X0X00000000000X-0" & _
            "X0X0X00000000000X-0" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "-------------------" & _
            "X0X0X00000000000X-0"

    Public RealDivResultTypes As String = "" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXBXBBBBBBBBBBBX-B" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXOXOOOOOOOONOPX-O" & _
            "XXXXXXXXXXXXXXXXX-O" & _
            "XBXOXOOOOOOOONOPX-O" & _
            "XBXOXOOOOOOOONOPX-O" & _
            "XBXOXOOOOOOOONOPX-O" & _
            "XBXOXOOOOOOOONOPX-O" & _
            "XBXOXOOOOOOOONOPX-O" & _
            "XBXOXOOOOOOOONOPX-O" & _
            "XBXOXOOOOOOOONOPX-O" & _
            "XBXOXOOOOOOOONOPX-O" & _
            "XBXNXNNNNNNNNNONX-O" & _
            "XBXOXOOOOOOOOOOOX-O" & _
            "XBXPXPPPPPPPPNOPX-O" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "-------------------" & _
            "XBXOOOOOOOOOOOOOX-O"
    Public RealDivAllowedType As String =
            "XXXXXXXXXXXXXXXXX-X" & _
            "X0X0X00000000000X-0" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "X0XOXOOOOOOOOOOOX-O" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "X0X0XIIIIIIIIIIIX-O" & _
            "X0X0XIIIIIIIIIIIX-O" & _
            "X0X0XIIIIIIIIIIIX-O" & _
            "X0X0XIIIIIIIIIIIX-O" & _
            "X0X0XIIIIIIIIIIIX-O" & _
            "X0X0XIIIIIIIIIIIX-O" & _
            "X0X0XIIIIIIIIIIIX-O" & _
            "X0X0XIIIIIIIIIIIX-O" & _
            "X0X0XIIIIIIIIIIIX-O" & _
            "X0X0XIIIIIIIIIIIX-O" & _
            "X0X0XIIIIIIIIIIIX-O" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "-------------------" & _
            "X0X0X00000000000X-O"

    Public AddResultType As String = "" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXBBBBBBBBBBBBBB-B" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXHXFHHJJLLPNOPX-O" & _
            "XBXXSXXXXXXXXXXXX-S" & _
            "XBXFXFHHJJLLPNOPX-O" & _
            "XBXHXHGHIJKLMNOPX-O" & _
            "XBXHXHHHJJLLPNOPX-O" & _
            "XBXJXJIJIJKLMNOPX-O" & _
            "XBXJXJJJJJLLPNOPX-O" & _
            "XBXLXLKLKLKLMNOPX-O" & _
            "XBXLXLLLLLLLPNOPX-O" & _
            "XBXPXPMPMPMPMNOPX-O" & _
            "XBXNXNNNNNNNNNONX-O" & _
            "XBXOXOOOOOOOOOOOX-O" & _
            "XBXPXPPPPPPPPNOPX-O" & _
            "XBXXXXXXXXXXXXXXX-S" & _
            "-------------------" & _
            "XBXOSOOOOOOOOOOOS-S"
    Public AddAllowedType As String =
            "XXXXXXXXXXXXXXXXX-X" & _
            "X0X00000000000000-0" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "X0X0X00000000000X-0" & _
            "X0XXXXXXXXXXXXXXX-X" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0XXXXXXXXXXXXXXX-0" & _
            "-------------------" & _
            "X0X00000000000000-0"

    Public SubResultType As String = "" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXBXBBBBBBBBBBBX-B" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXHXFHHJJLLPNOPX-O" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXFXFHHJJLLPNOPX-O" & _
            "XBXHXHGHIJKLMNOPX-O" & _
            "XBXHXHHHJJLLPNOPX-O" & _
            "XBXJXJIJIJKLMNOPX-O" & _
            "XBXJXJJJJJLLPNOPX-O" & _
            "XBXLXLKLKLKLMNOPX-O" & _
            "XBXLXLLLLLLLPNOPX-O" & _
            "XBXPXPMPMPMPMNOPX-O" & _
            "XBXNXNNNNNNNNNONX-O" & _
            "XBXOXOOOOOOOOOOOX-O" & _
            "XBXPXPPPPPPPPNOPX-O" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "-------------------" & _
            "XBXOXOOOOOOOOOOOX-O"
    Public SubAllowedType As String =
            "XXXXXXXXXXXXXXXXX-X" & _
            "X0X0X00000000000X-0" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "X0X0X00000000000X-0" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "-------------------" & _
            "X0X0X00000000000X-0"

    Public MultResultType As String = SubResultType
    Public MultAllowedType As String =
            "XXXXXXXXXXXXXXXXX-X" & _
            "X0X0X00000000000X-0" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "X0X0X00000000000X-0" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "X0X0XIIIIIIIIIIIX-0" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "-------------------" & _
            "X0X0X00000000000X-0"

    Public ShortcircuitResultType As String = "" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXBXBBBBBBBBBBBX-B" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXDXDDDDDDDDDDDX-D" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXDXDDDDDDDDDDDX-D" & _
            "XBXDXDDDDDDDDDDDX-D" & _
            "XBXDXDDDDDDDDDDDX-D" & _
            "XBXDXDDDDDDDDDDDX-D" & _
            "XBXDXDDDDDDDDDDDX-D" & _
            "XBXDXDDDDDDDDDDDX-D" & _
            "XBXDXDDDDDDDDDDDX-D" & _
            "XBXDXDDDDDDDDDDDX-D" & _
            "XBXDXDDDDDDDDDDDX-D" & _
            "XBXDXDDDDDDDDDDDX-D" & _
            "XBXDXDDDDDDDDDDDX-D" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "-------------------" & _
            "XBXDXDDDDDDDDDDDX-D"
    Public ShortcircuitAllowedType As String =
            "XXXXXXXXXXXXXXXXX-X" & _
            "X0X0X00000000000X-0" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "X0XIX00000000000X-0" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "X0X0X00000000000X-0" & _
            "X0X0X00000000000X-0" & _
            "X0X0X00000000000X-0" & _
            "X0X0X00000000000X-0" & _
            "X0X0X00000000000X-0" & _
            "X0X0X00000000000X-0" & _
            "X0X0X00000000000X-0" & _
            "X0X0X00000000000X-0" & _
            "X0X0X00000000000X-0" & _
            "X0X0X00000000000X-0" & _
            "X0X0X00000000000X-0" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "-------------------" & _
            "X0X0X00000000000X-0"

    Public LogicalOperatorResultType As String = "" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXBXBBBBBBBBBBBX-B" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXDXFHHJJLLLLLLX-D" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXFXFHHJJLLLLLLX-L" & _
            "XBXHXHGHIJKLMLLLX-L" & _
            "XBXHXHHHJJLLLLLLX-L" & _
            "XBXJXJIJIJKLMLLLX-L" & _
            "XBXJXJJJJJLLLLLLX-L" & _
            "XBXLXLKLKLKLMLLLX-L" & _
            "XBXLXLLLLLLLLLLLX-L" & _
            "XBXLXLMLMLMLMLLLX-L" & _
            "XBXLXLLLLLLLLLLLX-L" & _
            "XBXLXLLLLLLLLLLLX-L" & _
            "XBXLXLLLLLLLLLLLX-L" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "-------------------" & _
            "XBXDXLLLLLLLLLLLX-L"
    Public LogicalAllowedType As String =
            "XXXXXXXXXXXXXXXXX-X" & _
            "X0X0X00000000000X-0" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "X0XIX00000000000X-0" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "X0X0XIIIIIII0000X-0" & _
            "X0X0XIIIIIII0000X-0" & _
            "X0X0XIIIIIII0000X-0" & _
            "X0X0XIIIIIII0000X-0" & _
            "X0X0XIIIIIII0000X-0" & _
            "X0X0XIIIIIII0000X-0" & _
            "X0X0XIIIIIII0000X-0" & _
            "X0X0X00000000000X-0" & _
            "X0X0X00000000000X-0" & _
            "X0X0X00000000000X-0" & _
            "X0X0X00000000000X-0" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "-------------------" & _
            "X0X0X00000000000X-0"

    Public RelationalOperandTypes As String = _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXBBBBBBBBBBBBBB-B" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXDXFHHJJPLPNOPX-D" & _
            "XBXXEXXXXXXXXXXXX-S" & _
            "XBXFXFHHJJPLPNOPX-O" & _
            "XBXHXHGHIJKLMNOPX-O" & _
            "XBXHXHHHJJPLPNOPX-O" & _
            "XBXIXJIJIJKLMNOPX-O" & _
            "XBXJXJJJJJPLPNOPX-O" & _
            "XBXPXPKPKPKLMNOPX-O" & _
            "XBXLXLLLLLLLPNOPX-O" & _
            "XBXPXPMPMPMPMNOPX-O" & _
            "XBXNXNNNNNNNNNOPX-O" & _
            "XBXOXOOOOOOOOOOPX-O" & _
            "XBXPXPPPPPPPPPPPX-O" & _
            "XBXXXXXXXXXXXXXXQ-Q" & _
            "-------------------" & _
            "XBXDSOOOOOOOOOOOQ-S"
    Public RelationalResultType As String = _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXBXBBBBBBBBBBBB-B" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXDXDDDDDDDDDDDX-D" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXDXDDDDDDDDDDDX-D" & _
            "XBXDXDDDDDDDDDDDX-D" & _
            "XBXDXDDDDDDDDDDDX-D" & _
            "XBXDXDDDDDDDDDDDX-D" & _
            "XBXDXDDDDDDDDDDDX-D" & _
            "XBXDXDDDDDDDDDDDX-D" & _
            "XBXDXDDDDDDDDDDDX-D" & _
            "XBXDXDDDDDDDDDDDX-D" & _
            "XBXDXDDDDDDDDDDDX-D" & _
            "XBXDXDDDDDDDDDDDX-D" & _
            "XBXDXDDDDDDDDDDDX-D" & _
            "XBXXXXXXXXXXXXXXD-X" & _
            "-------------------" & _
            "XBXDXDDDDDDDDDDDX-D"
    Public RelationalAllowedType As String =
            "XXXXXXXXXXXXXXXXX-X" & _
            "X0X0X000000000000-0" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "X0XXXXXXXXXXXXXXX-X" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "X0XXXIIIIIIIIIIIX-X" & _
            "X0XXXIIIIIIIIIIIX-X" & _
            "X0XXXIIIIIIIIIIIX-X" & _
            "X0XXXIIIIIIIIIIIX-X" & _
            "X0XXXIIIIIIIIIIIX-X" & _
            "X0XXXIIIIIIIIIIIX-X" & _
            "X0XXXIIIIIIIIIIIX-X" & _
            "X0XXXIIIIIIIIIIIX-X" & _
            "X0XXXIIIIIIIIIIIX-X" & _
            "X0XXXIIIIIIIIIIIX-X" & _
            "X0XXXIIIIIIIIIIIX-X" & _
            "X0XXXXXXXXXXXXXXI-X" & _
            "-------------------" & _
            "X0XXXXXXXXXXXXXXX-I"

    Public IsIsNotOperandTypes As String = _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXBBBBBBBBBBBBBB-B" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXDXFHHJJPLPNOPX-D" & _
            "XBXXEXXXXXXXXXXXX-S" & _
            "XBXFXFHHJJPLPNOPX-O" & _
            "XBXHXHGHIJKLMNOPX-O" & _
            "XBXHXHHHJJPLPNOPX-O" & _
            "XBXIXJIJIJKLMNOPX-O" & _
            "XBXJXJJJJJPLPNOPX-O" & _
            "XBXPXPKPKPKLMNOPX-O" & _
            "XBXLXLLLLLLLPNOPX-O" & _
            "XBXPXPMPMPMPMNOPX-O" & _
            "XBXNXNNNNNNNNNOPX-O" & _
            "XBXOXOOOOOOOOOOPX-O" & _
            "XBXPXPPPPPPPPPPPX-O" & _
            "XBXXXXXXXXXXXXXXQ-Q" & _
            "-------------------" & _
            "XBXDSOOOOOOOOOOOQ-S"
    Public IsIsNotResultType As String = _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XDXXXXXXXXXXXXXXX-D" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "-------------------" & _
            "XDXXXXXXXXXXXXXXX-D"
    Public IsIsNotAllowedType As String =
            "XXXXXXXXXXXXXXXXX-X" & _
            "XIXXXXXXXXXXXXXXX-I" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "-------------------" & _
            "XIXXXXXXXXXXXXXXX-I"

    Public ExponentResultTypes As String = _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXBXBBBBBBBBBBBX-B" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXOXOOOOOOOOOOOX-O" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXOXOOOOOOOOOOOX-O" & _
            "XBXOXOOOOOOOOOOOX-O" & _
            "XBXOXOOOOOOOOOOOX-O" & _
            "XBXOXOOOOOOOOOOOX-O" & _
            "XBXOXOOOOOOOOOOOX-O" & _
            "XBXOXOOOOOOOOOOOX-O" & _
            "XBXOXOOOOOOOOOOOX-O" & _
            "XBXOXOOOOOOOOOOOX-O" & _
            "XBXOXOOOOOOOOOOOX-O" & _
            "XBXOXOOOOOOOOOOOX-O" & _
            "XBXOXOOOOOOOOOOOX-O" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "-------------------" & _
            "XBXOXOOOOOOOOOOOX-O"
    Public ExponentAllowedType As String =
            "XXXXXXXXXXXXXXXXX-X" & _
            "X0X0X00000000000X-X" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "X0X0X00000000000X-X" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "X0X0XIIIIIIIIIIIX-X" & _
            "X0X0XIIIIIIIIIIIX-X" & _
            "X0X0XIIIIIIIIIIIX-X" & _
            "X0X0XIIIIIIIIIIIX-X" & _
            "X0X0XIIIIIIIIIIIX-X" & _
            "X0X0XIIIIIIIIIIIX-X" & _
            "X0X0XIIIIIIIIIIIX-X" & _
            "X0X0XIIIIIIIIIIIX-X" & _
            "X0X0XIIIIIIIIIIIX-X" & _
            "X0X0XIIIIIIIIIIIX-X" & _
            "X0X0XIIIIIIIIIIIX-X" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "-------------------" & _
            "XXXXXXXXXXXXXXXXX-X"


    Public ShiftResultType As String = _
             "XXXXXXXXXXXXXXXXX-X" & _
             "XBXBXBBBBBBBBBBBX-B" & _
             "XXXXXXXXXXXXXXXXX-X" & _
             "XBXHXFGHIJKLMLLLX-L" & _
             "XBXXXXXXXXXXXXXXX-X" & _
             "XBXHXFGHIJKLMLLLX-L" & _
             "XBXHXFGHIJKLMLLLX-L" & _
             "XBXHXFGHIJKLMLLLX-L" & _
             "XBXHXFGHIJKLMLLLX-L" & _
             "XBXHXFGHIJKLMLLLX-L" & _
             "XBXHXFGHIJKLMLLLX-L" & _
             "XBXHXFGHIJKLMLLLX-L" & _
             "XBXHXFGHIJKLMLLLX-L" & _
             "XBXHXFGHIJKLMLLLX-L" & _
             "XBXHXFGHIJKLMLLLX-L" & _
             "XBXHXFGHIJKLMLLLX-L" & _
             "XBXXXXXXXXXXXXXXX-X" & _
             "-------------------" & _
             "XBXHXFGHIJKLMLLLX-L"
    Public ShiftAllowedType As String =
            "XXXXXXXXXXXXXXXXX-X" & _
            "X0X0X00000000000X-0" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "X0X0X00000000000X-0" & _
            "X0XXXXXXXXXXXXXXX-X" & _
            "X0X0XIIIIIIII000X-0" & _
            "X0X0XIIIIIIII000X-0" & _
            "X0X0XIIIIIIII000X-0" & _
            "X0X0XIIIIIIII000X-0" & _
            "X0X0XIIIIIIII000X-0" & _
            "X0X0X00000000000X-0" & _
            "X0X0X00000000000X-0" & _
            "X0X0X00000000000X-0" & _
            "X0X0X00000000000X-0" & _
            "X0X0X00000000000X-0" & _
            "X0X0X00000000000X-0" & _
            "X0XXXXXXXXXXXXXXX-X" & _
            "-------------------" & _
            "X0X0X00000000000X-0"

    Dim NotOperatorResultType1 As String = "XBXDXFGHIJKLMLLLX-L"
    Dim NotOperatorResultType2 As String = "X0XIXIIIIIIII000X-0"
    Dim UnaryPlusResultType1 As String = "XBXHXFGHIJKLMNOPX-O"
    Dim UnaryPlusResultType2 As String = "X0X0XIIIIIIIIIIIX-0"
    Dim UnaryMinusResultType1 As String = "XBXHXFHHJJLLPNOPX-O"
    Dim UnaryMinusResultType2 As String = "X0X0XIIIIIIIIIIIX-0"

    Public ConversionResultType As String = _
            "XXXXXXXXXXXXXXXXX-X" & _
            "X0X00000000000000-0" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XIXI1000000000001-0" & _
            "XIX1I222222221111-0" & _
            "XIX04I00000000001-0" & _
            "XIX040I0000000001-0" & _
            "XIX0400I000000001-0" & _
            "XIX04000I00000001-0" & _
            "XIX040000I0000001-0" & _
            "XIX0400000I000001-0" & _
            "XIX04000000I00001-0" & _
            "XIX040000000I0001-0" & _
            "XIX0100000000I001-0" & _
            "XIX01000000000I05-0" & _
            "XIX010000000000I1-0" & _
            "XIX1111111111161I-0" & _
            "-------------------" & _
            "XIX00000000000000-I"

    Dim types() As TypeCode = New TypeCode() {TypeCode.Boolean, TypeCode.Byte, TypeCode.SByte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64, TypeCode.Decimal, TypeCode.Single, TypeCode.Double, TypeCode.Char, TypeCode.String, TypeCode.DateTime, TypeCode.DBNull, TypeCode.Object}
    Dim values() As String = New String() {"bool", "b", "sb", "s", "us", "i", "ui", "l", "ul", "dec", "sng", "dbl", "chr", "str", "dt", "dbnull", "obj"}
    Dim binary_operators() As String = New String() {"AndAlso", "And", "BinaryAdd", "BinarySub", "Concat", "Equals", "Exponent", "GE", "GT", "IntDivision", "Is", "IsNot", "LE", "Like", "LShift", "LT", "Mod", "Mult", "NotEquals", "OrElse", "Or", "RealDivision", "RShift", "Xor"}
    Dim binary_ops() As String = New String() {"AndAlso", "And", "+", "-", "&", "=", "^", ">=", ">", "\", "Is", "IsNot", "<=", "Like", "<<", "<", "Mod", "*", "<>", "OrElse", "Or", "/", ">>", "Xor"}
    Dim binary_res() As String = New String() {ShortcircuitResultType, LogicalOperatorResultType, AddResultType, SubResultType, ConcatResultType, RelationalResultType, ExponentResultTypes, RelationalResultType, RelationalResultType, IntDivResultTypes, IsIsNotResultType, IsIsNotResultType, RelationalResultType, LikeResultType, ShiftResultType, RelationalResultType, ModResultType, MultResultType, RelationalResultType, ShortcircuitResultType, LogicalOperatorResultType, RealDivResultTypes, ShiftResultType, LogicalOperatorResultType}
    Dim binary_imp() As String = New String() {ShortcircuitAllowedType, LogicalAllowedType, AddAllowedType, SubAllowedType, ConcatAllowedType, RelationalAllowedType, ExponentAllowedType, RelationalAllowedType, RelationalAllowedType, IntDivAllowedType, IsIsNotAllowedType, IsIsNotAllowedType, RelationalAllowedType, LikeAllowedType, ShiftAllowedType, RelationalAllowedType, ModAllowedType, MultAllowedType, RelationalAllowedType, ShortcircuitAllowedType, LogicalOperatorResultType, RealDivAllowedType, ShiftAllowedType, LogicalOperatorResultType}

    Dim unary_operators() As String = New String() {"UnaryMinus", "UnaryNot", "UnaryPlus"}
    Dim unary_ops() As String = New String() {"-", "Not", "+"}
    Dim unary_res() As String = New String() {UnaryMinusResultType1, NotOperatorResultType1, UnaryPlusResultType1}
    Dim unary_imp() As String = New String() {UnaryMinusResultType2, NotOperatorResultType2, UnaryPlusResultType2}

    Private Function GetConv(ByVal op1 As TypeCode, ByVal op2 As TypeCode) As Char
        Return ConversionResultType.Chars(op1 + op2 * 19)
    End Function

    Sub Main()
        Dim dir As String = "D:\Documentos\Rolf\Proyectos\VB.NET\vbnc\mono-basic\vbnc\vbnc\tests\CompileTime2"
        GenerateTypeConversions(dir)
        GenerateUnaryOperators(dir)
        GenerateBinaryOperators(dir)
    End Sub

    Sub GenerateUnaryOperators(ByVal dir As String)
        Dim id As Integer = 2251

        For i As Integer = 0 To unary_operators.Length - 1
            Using fs As New FileStream(Path.Combine(dir, "OperatorIntrinsic" + unary_operators(i) + ".vb"), FileMode.Create)
                GenerateUnaryOperators(fs, unary_operators(i), unary_ops(i), unary_res(i), unary_imp(i))
            End Using

            Debug.WriteLine(String.Format(
"	<test id=""{1}"" name=""OperatorIntrinsic{0}""  target=""exe"" mytype=""empty"">" &
"		<file>CompileTime2\OperatorIntrinsic{0}.vb</file>" &
"		<file>CompileTime2\OperatorIntrinsic.vb</file>" &
"	</test>", unary_operators(i), id))
            Debug.WriteLine(String.Format(
"	<test id=""{1}"" name=""OperatorIntrinsic{0}Strict"" mytype=""empty"">" &
"		<arguments>/optionstrict+ /define:STRICT</arguments>" &
"		<file>CompileTime2\OperatorIntrinsic{0}.vb</file>" &
"		<file>CompileTime2\OperatorIntrinsic.vb</file>" &
"	</test>", unary_operators(i), id + 1))
            Debug.WriteLine(String.Format(
"	<test id=""{1}"" name=""OperatorIntrinsic{0}Error"" mytype=""empty"" expectedexitcode=""1"">" &
"		<arguments>/define:ERRORS</arguments>" &
"		<file>CompileTime2\OperatorIntrinsic{0}.vb</file>" &
"		<file>CompileTime2\OperatorIntrinsic.vb</file>" &
"	</test>", unary_operators(i), id + 2))
            Debug.WriteLine(String.Format(
"	<test id=""{1}"" name=""OperatorIntrinsic{0}StrictError"" mytype=""empty"" expectedexitcode=""1"">" &
"		<arguments>/optionstrict+ /define:ERRORS /define:STRICT</arguments>" &
"		<file>CompileTime2\OperatorIntrinsic{0}.vb</file>" &
"		<file>CompileTime2\OperatorIntrinsic.vb</file>" &
"	</test>", unary_operators(i), id + 3))
            id += 4
        Next
    End Sub

    Sub GenerateBinaryOperators(ByVal dir As String)
        Dim id As Integer = 2251 + 12

        For i As Integer = 0 To binary_operators.Length - 1
            Debug.WriteLine(String.Format(
"	<test id=""{1}"" name=""OperatorIntrinsic_{0}_""  target=""exe"" mytype=""empty"">" &
"		<file>CompileTime2\OperatorIntrinsic{0}.vb</file>" &
"		<file>CompileTime2\OperatorIntrinsic.vb</file>" &
"	</test>", binary_operators(i), id))
            Debug.WriteLine(String.Format(
"	<test id=""{1}"" name=""OperatorIntrinsic_{0}_Strict"" mytype=""empty"">" &
"		<arguments>/optionstrict+ /define:STRICT</arguments>" &
"		<file>CompileTime2\OperatorIntrinsic{0}.vb</file>" &
"		<file>CompileTime2\OperatorIntrinsic.vb</file>" &
"	</test>", binary_operators(i), id + 1))
            id += 2

            Using fs As New FileStream(Path.Combine(dir, "OperatorIntrinsic" + binary_operators(i) + ".vb"), FileMode.Create)
                GenerateBinaryOperators(fs, binary_operators(i), binary_ops(i), binary_res(i), binary_imp(i))
            End Using

            For l As Integer = 0 To types.Length - 1
                Debug.WriteLine(String.Format(
    "	<test id=""{1}"" name=""OperatorIntrinsic_{0}_{2}Error"" mytype=""empty"" expectedexitcode=""1"">" &
    "		<arguments>/define:{2}_ERRORS</arguments>" &
    "		<file>CompileTime2\OperatorIntrinsic{0}.vb</file>" &
    "		<file>CompileTime2\OperatorIntrinsic.vb</file>" &
    "	</test>", binary_operators(i), id, types(l).ToString()))
                Debug.WriteLine(String.Format(
    "	<test id=""{1}"" name=""OperatorIntrinsic_{0}_{2}StrictError"" mytype=""empty"" expectedexitcode=""1"">" &
    "		<arguments>/optionstrict+ /define:{2}_ERRORS /define:STRICT</arguments>" &
    "		<file>CompileTime2\OperatorIntrinsic{0}.vb</file>" &
    "		<file>CompileTime2\OperatorIntrinsic.vb</file>" &
    "	</test>", binary_operators(i), id + 1, types(l).ToString()))
                id += 2
            Next
        Next
    End Sub

    Sub GenerateUnaryOperators(ByVal stream As Stream, ByVal opname As String, ByVal op As String, ByVal result As String, ByVal conv As String)
        Using w As New StreamWriter(stream)
            w.WriteLine("Class UnaryOperator" & opname)
            w.WriteLine("    Inherits IntrinsicOperatorTests")
            w.WriteLine("    Shared Function Main As Integer")
            w.WriteLine("        Try")
            For i As Integer = 0 To types.Count - 1
                Dim tc As Char = result(CInt(types(i)))
                Dim c As Char = conv(CInt(types(i)))

                If c = "I" Then
                    WriteExpectedTC(w, tc)
                    w.WriteLine("        M({0} {1}, ""{0} {1}"")", op, values(i), op, values(i))
                ElseIf c = "0" Then
                    w.WriteLine("#If Not STRICT Or ERRORS")
                    WriteExpectedTC(w, tc)
                    w.WriteLine("        M({0} {1}, ""{0} {1}"")", op, values(i), op, values(i))
                    w.WriteLine("#End If")
                Else
                    w.WriteLine("#If ERRORS")
                    w.WriteLine("        M({0} {1}, ""{0} {1}"")", op, values(i), op, values(i))
                    w.WriteLine("#End If")
                End If
            Next
            w.WriteLine("    If failures > 0 Then Return 1")
            w.WriteLine("    Catch ex As Exception")
            w.WriteLine("        Console.WriteLine (""Exception: {0}"", ex)")
            w.WriteLine("        Return 2")
            w.WriteLine("    End Try")
            w.WriteLine("    End Function")
            w.WriteLine("End Class")
        End Using
    End Sub

    Sub GenerateBinaryOperators(ByVal stream As Stream, ByVal opname As String, ByVal op As String, ByVal result As String, ByVal conv As String)
        Using w As New StreamWriter(stream)
            w.WriteLine("Class BinaryOperator" & opname)
            w.WriteLine("    Inherits IntrinsicOperatorTests")
            w.WriteLine("    Shared Function Main As Integer")
            w.WriteLine("        Try")
            If opname = "IntDivision" OrElse opname = "RealDivision" OrElse opname = "Mod" Then
                w.WriteLine("            obj = 1")
            End If
            For i As Integer = 0 To types.Count - 1
                For j As Integer = 0 To types.Count - 1
                    Dim tci As TypeCode = types(i)
                    Dim tcj As TypeCode = types(j)
                    Dim tc As Char = result(tci + tcj * 19)
                    Dim c As Char = conv(tci + tcj * 19)

                    If (opname = "LShift" OrElse opname = "RShift") AndAlso tci = TypeCode.Object AndAlso (tcj = TypeCode.Char OrElse tcj = TypeCode.DateTime) Then
                        'https://connect.microsoft.com/VisualStudio/feedback/details/669572/vb10-shift-operators-allows-char-as-second-operand
                        w.WriteLine("#If Not STRICT Or {0}_ERRORS", tci.ToString())
                        w.WriteLine("        Try")
                        WriteExpectedTC(w, tc)
                        w.WriteLine("            M({1} {0} {2}, ""{1} {0} {2}"")", op, values(i), values(j))
                        w.WriteLine("        Catch ex as InvalidCastException")
                        w.WriteLine("        End Try")
                        w.WriteLine("#End If")
                    ElseIf c = "I" Then
                        WriteExpectedTC(w, tc)
                        w.WriteLine("            M({1} {0} {2}, ""{1} {0} {2}"")", op, values(i), values(j))
                    ElseIf c = "0" Then
                        w.WriteLine("#If Not STRICT Or {0}_ERRORS", tci.ToString())
                        WriteExpectedTC(w, tc)
                        w.WriteLine("            M({1} {0} {2}, ""{1} {0} {2}"")", op, values(i), values(j))
                        w.WriteLine("#End If")
                    Else
                        w.WriteLine("#If {0}_ERRORS", tci.ToString())
                        w.WriteLine("            M({1} {0} {2}, ""{1} {0} {2}"")", op, values(i), values(j))
                        w.WriteLine("#End If")
                    End If
                Next
            Next
            w.WriteLine("        If failures > 0 Then Return 1")
            w.WriteLine("    Catch ex As Exception")
            w.WriteLine("        Console.WriteLine (""Exception: {0}"", ex)")
            w.WriteLine("        Return 2")
            w.WriteLine("    End Try")
            w.WriteLine("    Return 0")
            w.WriteLine("    End Function")
            w.WriteLine("End Class")
        End Using
    End Sub

    Sub WriteExpectedTC(ByVal w As StreamWriter, ByVal tc As Char)
        '0 = A Empty          A
        '1 = B Object         B
        '2 = C DBNull         C
        '3 = D Boolean        D
        '4 = E Char           E
        '5 = F SByte          F
        '6 = G Byte           G
        '7 = H Int16(Short)   H
        '8 = I UInt16(UShort) I
        '9 = J Int32          J
        '10= K UInt32         K 
        '11= L Int64(Long)    L
        '12= M UInt64(ULong)  M
        '13= N Single         N
        '14= O Double         O
        '15= P Decimal        P
        '16= Q DateTime       Q
        '17= - 17             -
        '18= S String         S
        w.WriteLine("            expected_tc = TypeCode.{0}", CType(Convert.ToInt16(tc) - Convert.ToInt16("A"c), TypeCode).ToString())
    End Sub

    Sub GenerateTypeConversions(ByVal dir As String)
        For i As Integer = 0 To types.Count - 1
            Dim tc As TypeCode = types(i)
            If types(i) = TypeCode.Empty Then Continue For
            Using fs As New FileStream(Path.Combine(dir, "TypeConversionIntrinsic" + tc.ToString() + ".vb"), FileMode.Create)
                GenerateTypeConversions(fs, tc)
            End Using
        Next
    End Sub

    Sub GenerateTypeConversions(ByVal stream As Stream, ByVal tc As TypeCode)
        Using w As New StreamWriter(stream)
            w.WriteLine("Class TypeConversionIntrinsic")

            w.WriteLine("    Sub LocalVariables")
            WriteVariables(w, "", "i", "        Dim ", Environment.NewLine)
            WriteVariables(w, "", "o", "        Dim ", Environment.NewLine)
            WriteConversions(w, tc)
            w.WriteLine("    End Sub")

            w.Write("    Sub InLocalOutRef(")
            WriteVariables(w, "ByRef", "o", "", ")" + Environment.NewLine)
            WriteVariables(w, "", "i", "        Dim ", Environment.NewLine)
            WriteConversions(w, tc)
            w.WriteLine("    End Sub")

            w.Write("    Sub InRefOutLocal(")
            WriteVariables(w, "ByRef", "i", "", ")" + Environment.NewLine)
            WriteVariables(w, "", "o", "        Dim ", Environment.NewLine)
            WriteConversions(w, tc)
            w.WriteLine("    End Sub")

            w.Write("    Sub InRefOutRef(")
            WriteVariables(w, "ByRef", "i", "", ", ")
            WriteVariables(w, "ByRef", "o", "", ")" + Environment.NewLine)
            WriteConversions(w, tc)
            w.WriteLine("    End Sub")

            w.WriteLine("End Class")
        End Using
    End Sub

    Sub WriteVariables(ByVal w As StreamWriter, ByVal access As String, ByVal prefix As String, ByVal pre As String, ByVal post As String)
        w.Write("{2}{0} {1}_bool As Boolean, {0} {1}_byte As Byte, {0} {1}_sbyte As SByte, {0} {1}_short As Short, {0} {1}_ushort As UShort, {0} {1}_int As Integer, {0} {1}_uint as UInteger, {0} {1}_long As Long, {0} {1}_ulong As ULong, {0} {1}_dec As Decimal, {0} {1}_sng As Single,{0} {1}_dbl As Double, {0} {1}_chr As Char, {0} {1}_str As String, {0} {1}_dt As Date, {0} {1}_dbnull As DBNull, {0} {1}_obj As Object{3}", access, prefix, pre, post)
    End Sub

    Sub WriteConversions(ByVal w As StreamWriter, ByVal tc As TypeCode)
        Dim suffix() As String

        ReDim suffix(20)

        suffix(TypeCode.Boolean) = "bool"
        suffix(TypeCode.Byte) = "byte"
        suffix(TypeCode.SByte) = "sbyte"
        suffix(TypeCode.Int16) = "short"
        suffix(TypeCode.UInt16) = "ushort"
        suffix(TypeCode.Int32) = "int"
        suffix(TypeCode.UInt32) = "uint"
        suffix(TypeCode.Int64) = "long"
        suffix(TypeCode.UInt64) = "ulong"
        suffix(TypeCode.Decimal) = "dec"
        suffix(TypeCode.Single) = "sng"
        suffix(TypeCode.Double) = "dbl"
        suffix(TypeCode.String) = "str"
        suffix(TypeCode.Char) = "chr"
        suffix(TypeCode.Object) = "obj"
        suffix(TypeCode.DateTime) = "dt"
        suffix(TypeCode.DBNull) = "dbnull"

        For i As Integer = 0 To types.Length - 1
            For o As Integer = 0 To types.Length - 1
                Dim c As Char

                If types(i) = TypeCode.Empty OrElse types(o) = TypeCode.Empty Then Continue For
                If types(i) <> tc Then Continue For

                c = GetConv(types(i), types(o))
                If c = "I" Then
                    w.WriteLine("        i_{0} = o_{1}", suffix(types(i)), suffix(types(o)))
                ElseIf c = "0" Then
                    w.WriteLine("#If Not STRICT Or ERRORS")
                    w.WriteLine("        i_{0} = o_{1}", suffix(types(i)), suffix(types(o)))
                    w.WriteLine("#End If")
                Else
                    w.WriteLine("#If ERRORS")
                    w.WriteLine("        i_{0} = o_{1}", suffix(types(i)), suffix(types(o)))
                    w.WriteLine("#End If")
                End If
            Next
        Next
    End Sub
End Module
#End If

