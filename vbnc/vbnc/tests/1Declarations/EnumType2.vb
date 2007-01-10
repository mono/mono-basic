Namespace EnumType2
    Enum e1
        value
    End Enum
    Enum e2 As Byte
        value
    End Enum
    Enum e3 As Short
        value
    End Enum
    Enum e4 As Integer
        value
    End Enum
    Enum e5 As Long
        value
    End Enum
    Enum e6 As SByte
        value
    End Enum
    Enum e7 As UShort
        value
    End Enum
    Enum e8 As UInteger
        value
    End Enum
    Enum e9 As ULong
        value
    End Enum

    Public Enum ep1
        value
    End Enum
    Friend Enum ef1
        value
    End Enum

    Class classcontainer
        Enum e1
            value
        End Enum
        Enum e2 As Byte
            value
        End Enum
        Enum e3 As Short
            value
        End Enum
        Enum e4 As Integer
            value
        End Enum
        Enum e5 As Long
            value
        End Enum
        Enum e6 As SByte
            value
        End Enum
        Enum e7 As UShort
            value
        End Enum
        Enum e8 As UInteger
            value
        End Enum
        Enum e9 As ULong
            value
        End Enum

        Public Enum ep1
            value
        End Enum
        Protected Enum ep2
            value
        End Enum
        Protected Friend Enum epf1
            value
        End Enum
        Friend Enum ef1
            value
        End Enum
        Private Enum ep3
            value
        End Enum
    End Class
    Structure structurecontainer
        Public value As Integer
        Enum e1
            value
        End Enum
        Enum e2 As Byte
            value
        End Enum
        Enum e3 As Short
            value
        End Enum
        Enum e4 As Integer
            value
        End Enum
        Enum e5 As Long
            value
        End Enum
        Enum e6 As SByte
            value
        End Enum
        Enum e7 As UShort
            value
        End Enum
        Enum e8 As UInteger
            value
        End Enum
        Enum e9 As ULong
            value
        End Enum

        Public Enum ep1
            value
        End Enum
        'Protected Enum ep2
        '    value
        'End Enum
        'Protected Friend Enum epf1
        '    value
        'End Enum
        Friend Enum ef1
            value
        End Enum
        Private Enum ep3
            value
        End Enum
    End Structure
    Module modulecontainer
        Enum e1
            value
        End Enum
        Enum e2 As Byte
            value
        End Enum
        Enum e3 As Short
            value
        End Enum
        Enum e4 As Integer
            value
        End Enum
        Enum e5 As Long
            value
        End Enum
        Enum e6 As SByte
            value
        End Enum
        Enum e7 As UShort
            value
        End Enum
        Enum e8 As UInteger
            value
        End Enum
        Enum e9 As ULong
            value
        End Enum

        Public Enum ep1
            value
        End Enum
        'Protected Enum ep2
        '    value
        'End Enum
        'Protected Friend Enum epf1
        '    value
        'End Enum
        Friend Enum ef1
            value
        End Enum
        Private Enum ep3
            value
        End Enum
    End Module

    Interface interfacecontainer
        Enum e1
            value
        End Enum
        Enum e2 As Byte
            value
        End Enum
        Enum e3 As Short
            value
        End Enum
        Enum e4 As Integer
            value
        End Enum
        Enum e5 As Long
            value
        End Enum
        Enum e6 As SByte
            value
        End Enum
        Enum e7 As UShort
            value
        End Enum
        Enum e8 As UInteger
            value
        End Enum
        Enum e9 As ULong
            value
        End Enum

        'Public Enum ep1
        '    value
        'End Enum
        'Protected Enum ep2
        '    value
        'End Enum
        'Protected Friend Enum epf1
        '    value
        'End Enum
        'Friend Enum ef1
        '    value
        'End Enum
        'Private Enum ep3
        '    value
        'End Enum
    End Interface
End Namespace
