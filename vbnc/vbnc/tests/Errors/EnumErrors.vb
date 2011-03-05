'No variables
Enum E2
End Enum

'Syntax error
Enum E3 as +
    e
End Enum

'Syntax error
Enum Enum
    e
End Enum

'Syntax error
Enum +
    e
End Enum

'Type characters
Enum I4%
    e
End Enum
Enum L4&
    e
End Enum

'Junk at end
enum IA as byte as
    e
End Enum

'junk at end of end enum
Enum IB
    e
End Enum junk

