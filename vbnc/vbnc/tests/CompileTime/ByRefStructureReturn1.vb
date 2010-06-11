Imports System
Class ByRefStructureReturn1
    Function I8(ByRef i As SByte) As SByte
        Return i
    End Function
    Function I16(ByRef i As Short) As Short
        Return i
    End Function
    Function I32(ByRef i As Integer) As Integer
        Return i
    End Function
    Function I64(ByRef i As Long) As Long
        Return i
    End Function
    Function U8(ByRef i As Byte) As Byte
        Return i
    End Function
    Function U16(ByRef i As UShort) As UShort
        Return i
    End Function
    Function U32(ByRef i As UInteger) As UInteger
        Return i
    End Function
    Function U64(ByRef i As ULong) As ULong
        Return i
    End Function
    Function C(ByRef i As Char) As Char
        Return i
    End Function
    Function DT(ByRef i As Date) As Date
        Return i
    End Function
    Function Dec(ByRef i As Decimal) As Decimal
        Return i
    End Function
    Function DBN(ByRef i As DBNull) As DBNull
        Return i
    End Function
    Function S(ByRef i As Single) As Single
        Return i
    End Function
    Function D(ByRef i As Double) As Double
        Return i
    End Function
    Function B(ByRef i As Boolean) As Boolean
        Return i
    End Function
    Function [Enum](ByRef i As TypeCode) As TypeCode
        Return i
    End Function
    Function [Delegate](ByRef i As AppDomainInitializer) As AppDomainInitializer
        Return i
    End Function
    Function [Structure](ByRef i As ConsoleKeyInfo) As ConsoleKeyInfo
        Return i
    End Function
    Function [Interface](ByRef i As IAppDomainSetup) As IAppDomainSetup
        Return i
    End Function
    Function [Class](ByRef i As AppDomain) As AppDomain
        Return i
    End Function
    Function S(ByRef i As String) As String
        Return i
    End Function

    Function NullableI32(ByRef i As Nullable(Of Integer)) As Nullable(Of Integer)
        Return i
    End Function
End Class