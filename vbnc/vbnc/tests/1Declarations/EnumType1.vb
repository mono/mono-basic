'Needs expression evaluation to pass test.
Namespace EnumType1
    Enum a
        b = 1
        c = 2
    End Enum
    Enum c As Byte
        b = 1
        c = 2
    End Enum
    Enum b As Short
        b = 1
        c = 2
    End Enum
    Enum d As Long
        b = 1
        c = 2L * b
    End Enum
    Enum e
        b = 1
        c = 2
    End Enum
End Namespace

