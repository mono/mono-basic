Imports System
Imports System.Collections
Imports System.Reflection

Namespace Boolean2
    Class Test
        Shared failures As Integer

        Shared Sub Fail(ByVal msg As String)
            failures += 1
            Console.WriteLine(msg)
        End Sub

        Shared Sub Check(ByVal actual As Object, ByVal expected As Object, ByVal msg As String)
            If TypeOf actual Is Boolean AndAlso TypeOf expected Is Boolean Then
                Dim a As Boolean = DirectCast(actual, Boolean)
                Dim e As Boolean = DirectCast(expected, Boolean)
                If a <> e Then Fail(msg)
            ElseIf TypeOf actual Is Long AndAlso TypeOf expected Is Long Then
                Dim a As Long = DirectCast(actual, Long)
                Dim e As Long = DirectCast(expected, Long)
                If a <> e Then Fail(msg)
            ElseIf TypeOf actual Is UShort AndAlso TypeOf expected Is UShort Then
                Dim a As UShort = DirectCast(actual, UShort)
                Dim e As UShort = DirectCast(expected, UShort)
                If a <> e Then Fail(msg)
            ElseIf TypeOf actual Is Integer AndAlso TypeOf expected Is Integer Then
                Dim a As Integer = DirectCast(actual, Integer)
                Dim e As Integer = DirectCast(expected, Integer)
                If a <> e Then Fail(msg)
            ElseIf TypeOf actual Is UInteger AndAlso TypeOf expected Is UInteger Then
                Dim a As UInteger = DirectCast(actual, UInteger)
                Dim e As UInteger = DirectCast(expected, UInteger)
                If a <> e Then Fail(msg)
            ElseIf TypeOf actual Is Short AndAlso TypeOf expected Is Short Then
                Dim a As Short = DirectCast(actual, Short)
                Dim e As Short = DirectCast(expected, Short)
                If a <> e Then Fail(msg)
            ElseIf TypeOf actual Is SByte AndAlso TypeOf expected Is SByte Then
                Dim a As SByte = DirectCast(actual, SByte)
                Dim e As SByte = DirectCast(expected, SByte)
                If a <> e Then Fail(msg)
            ElseIf TypeOf actual Is ULong AndAlso TypeOf expected Is ULong Then
                Dim a As ULong = DirectCast(actual, ULong)
                Dim e As ULong = DirectCast(expected, ULong)
                If a <> e Then Fail(msg)
            ElseIf TypeOf actual Is Byte AndAlso TypeOf expected Is Byte Then
                Dim a As Byte = DirectCast(actual, Byte)
                Dim e As Byte = DirectCast(expected, Byte)
                If a <> e Then Fail(msg)
            Else
                Fail(String.Format("Types not implemented: {0} and {1} ({2})", actual.GetType().FullName, expected.GetType().FullName, msg))
            End If
        End Sub

        Shared Sub CheckAnd()
            Dim a As Boolean
            Dim b As Boolean

            ' boolean and boolean
            Check(True And True, True, "true and true")
            Check(True And False, False, "true and false")
            Check(False And True, False, "false and true")
            Check(False And False, False, "false and false")

            a = False
            b = False
            Check(a And b, False, "a[false] and b[false]")
            a = True
            b = False
            Check(a And b, False, "a[true] and b [false]")
            a = False
            b = True
            Check(a And b, False, "a[false] and b[true]")
            a = True
            b = True
            Check(a And b, True, "a[true] and b[true]")

            'boolean and sbyte
            Check(True And CSByte(1), CSByte(1), "true and csbyte(1)")
            Check(True And CSByte(50), CSByte(50), "true and csbyte(50)")
            Check(True And CSByte(0), CSByte(0), "true and csbyte(0)")
            Check(False And CSByte(1), CSByte(0), "false and csbyte(1)")
            Check(False And CSByte(50), CSByte(0), "false and csbyte(50)")
            Check(False And CSByte(0), CSByte(0), "false and csbyte(0)")

            'boolean and byte
            Check(True And CByte(1), CShort(1), "true and cbyte(1)")
            Check(True And CByte(50), CShort(50), "true and cbyte(50)")
            Check(True And CByte(0), CShort(0), "true and cbyte(0)")
            Check(False And CByte(1), CShort(0), "false and cbyte(1)")
            Check(False And CByte(50), CShort(0), "false and cbyte(50)")
            Check(False And CByte(0), CShort(0), "false and cbyte(0)")

            'boolean and short
            Check(True And CShort(1), CShort(1), "true and cshort(1)")
            Check(True And CShort(50), CShort(50), "true and cshort(50)")
            Check(True And CShort(0), CShort(0), "true and cshort(0)")
            Check(False And CShort(1), CShort(0), "false and cshort(1)")
            Check(False And CShort(50), CShort(0), "false and cshort(50)")
            Check(False And CShort(0), CShort(0), "false and cshort(0)")

            'boolean and ushort
            Check(True And CUShort(1), CInt(1), "true and cushort(1)")
            Check(True And CUShort(50), CInt(50), "true and cushort(50)")
            Check(True And CUShort(0), CInt(0), "true and cushort(0)")
            Check(False And CUShort(1), CInt(0), "false and cushort(1)")
            Check(False And CUShort(50), CInt(0), "false and cushort(50)")
            Check(False And CUShort(0), CInt(0), "false and cushort(0)")

            'boolean and int
            Check(True And CInt(1), CInt(1), "true and cint(1)")
            Check(True And CInt(50), CInt(50), "true and cint(50)")
            Check(True And CInt(0), CInt(0), "true and cint(0)")
            Check(False And CInt(1), CInt(0), "false and cint(1)")
            Check(False And CInt(50), CInt(0), "false and cint(50)")
            Check(False And CInt(0), CInt(0), "false and cint(0)")

            'boolean and uint
            Check(True And CUInt(1), CLng(1), "true and cuint(1)")
            Check(True And CUInt(50), CLng(50), "true and cuint(50)")
            Check(True And CUInt(0), CLng(0), "true and cuint(0)")
            Check(False And CUInt(1), CLng(0), "false and cuint(1)")
            Check(False And CUInt(50), CLng(0), "false and cuint(50)")
            Check(False And CUInt(0), CLng(0), "false and cuint(0)")

            'boolean and long
            Check(True And CLng(1), CLng(1), "true and clng(1)")
            Check(True And CLng(50), CLng(50), "true and clng(50)")
            Check(True And CLng(0), CLng(0), "true and clng(0)")
            Check(False And CLng(1), CLng(0), "false and clng(1)")
            Check(False And CLng(50), CLng(0), "false and clng(50)")
            Check(False And CLng(0), CLng(0), "false and clng(0)")

            'boolean and ulong
            Check(True And CULng(1), CLng(1), "true and culng(1)")
            Check(True And CULng(50), CLng(50), "true and culng(50)")
            Check(True And CULng(0), CLng(0), "true and culng(0)")
            Check(False And CULng(1), CLng(0), "false and culng(1)")
            Check(False And CULng(50), CLng(0), "false and culng(50)")
            Check(False And CULng(0), CLng(0), "false and culng(0)")

            'boolean and decimal
            Check(True And CDec(1), CLng(1), "true and cdec(1)")
            Check(True And CDec(50), CLng(50), "true and cdec(50)")
            Check(True And CDec(0), CLng(0), "true and cdec(0)")
            Check(False And CDec(1), CLng(0), "false and cdec(1)")
            Check(False And CDec(50), CLng(0), "false and cdec(50)")
            Check(False And CDec(0), CLng(0), "false and cdec(0)")

            'boolean and single
            Check(True And CSng(1), CLng(1), "true and csng(1)")
            Check(True And CSng(50), CLng(50), "true and csng(50)")
            Check(True And CSng(0), CLng(0), "true and csng(0)")
            Check(True And CSng(0.9), CLng(1), "true and csng(0.9)")
            Check(True And CSng(1.9), CLng(2), "true and csng(1.9)")
            Check(True And CSng(1.5), CLng(2), "true and csng(1.5)")
            Check(True And CSng(1.1), CLng(1), "true and csng(1.1)")
            Check(False And CSng(1), CLng(0), "false and csng(1)")
            Check(False And CSng(50), CLng(0), "false and csng(50)")
            Check(False And CSng(0), CLng(0), "false and csng(0)")
            Check(False And CSng(0.6), CLng(0), "false and csng(0.6)")

            'boolean and double
            Check(True And CDbl(1), CLng(1), "true and cdbl(1)")
            Check(True And CDbl(50), CLng(50), "true and cdbl(50)")
            Check(True And CDbl(0), CLng(0), "true and cdbl(0)")
            Check(True And CDbl(0.9), CLng(1), "true and cdbl(0.9)")
            Check(True And CDbl(1.9), CLng(2), "true and cdbl(1.9)")
            Check(True And CDbl(1.5), CLng(2), "true and cdbl(1.5)")
            Check(True And CDbl(1.4999), CLng(1), "true and cdbl(1.4999)")
            Check(True And CDbl(2.4999), CLng(2), "true and cdbl(2.4999)")
            Check(True And CDbl(1.1), CLng(1), "true and cdbl(1.1)")
            Check(False And CDbl(1), CLng(0), "false and cdbl(1)")
            Check(False And CDbl(50), CLng(0), "false and cdbl(50)")
            Check(False And CDbl(0), CLng(0), "false and cdbl(0)")
            Check(False And CDbl(0.6), CLng(0), "false and cdbl(0.6)")

            'boolean and date
            'boolean and char

            'boolean and string
            Check(True And "true", True, "true and 'true'")
            Check(True And "false", False, "true and 'false'")
            Check(False And "true", False, "false and 'true'")
            Check(False And "false", False, "false and 'false'")
            Check(True And "tRue", True, "true and 'tRue'")
            Check(True And "falsE", False, "true and 'falsE'")
            Check(False And "TRUE", False, "false and 'TRUE'")
            Check(False And "false", False, "false and 'false'")
            Check(True And "1", True, "true and '1'")
            Check(True And "0", False, "true and '0'")
            Check(False And "1", False, "false and '1'")
            Check(False And "0", False, "false and '0'")

            'string and boolean
            Check("true" And True, True, "'true' and true")
            Check("false" And True, False, "'false' and true")
            Check("true" And False, False, "'true' and false")
            Check("false" And False, False, "'false' and false")
            Check("tRue" And True, True, "'tRue' and true")
            Check("falsE" And True, False, "'falsE' and true")
            Check("TRUE" And False, False, "'TRUE' and and false")
            Check("false" And False, False, "'false' and false")
            Check("1" And True, True, "'1' and true")
            Check("0" And True, False, "'0' and true")
            Check("1" And False, False, "'1' and false")
            Check("0" And False, False, "'0' and false")

            'boolean and object
            Check(True And CObj(1), CInt(1), "true and cobj(1)")
            Check(False And CObj(0), CInt(0), "false and cobj(0)")

            ''''''

            'sbyte and byte
            Check(CSByte(0) And CByte(1), CShort(0), "csbyte(0) and cbyte(1)")
            Check(CSByte(1) And CByte(50), CShort(0), "csbyte(1) and cbyte(50)")
            Check(CSByte(83) And CByte(0), CShort(0), "csbyte(83) and cbyte(0)")
            Check(CSByte(0) And CByte(1), CShort(0), "csbyte(0) and cbyte(1)")
            Check(CSByte(1) And CByte(50), CShort(0), "csbyte(1) and cbyte(50)")
            Check(CSByte(42) And CByte(0), CShort(0), "csbyte(42) and cbyte(0)")

            'sbyte and short
            Check(CSByte(0) And CShort(1), CShort(0), "csbyte(0) and cshort(1)")
            Check(CSByte(1) And CShort(50), CShort(0), "csbyte(1) and cshort(50)")
            Check(CSByte(83) And CShort(0), CShort(0), "csbyte(83) and cshort(0)")
            Check(CSByte(0) And CShort(1), CShort(0), "csbyte(0) and cshort(1)")
            Check(CSByte(1) And CShort(50), CShort(0), "csbyte(1) and cshort(50)")
            Check(CSByte(42) And CShort(0), CShort(0), "csbyte(42) and cshort(0)")

            'sbyte and ushort
            Check(CSByte(0) And CUShort(1), CInt(0), "csbyte(0) and cushort(1)")
            Check(CSByte(1) And CUShort(50), CInt(0), "csbyte(1) and cushort(50)")
            Check(CSByte(83) And CUShort(0), CInt(0), "csbyte(83) and cushort(0)")
            Check(CSByte(0) And CUShort(1), CInt(0), "csbyte(0) and cushort(1)")
            Check(CSByte(1) And CUShort(50), CInt(0), "csbyte(1) and cushort(50)")
            Check(CSByte(42) And CUShort(0), CInt(0), "csbyte(42) and cushort(0)")

            'sbyte and int
            Check(CSByte(0) And CInt(1), CInt(0), "csbyte(0) and cint(1)")
            Check(CSByte(1) And CInt(50), CInt(0), "csbyte(1) and cint(50)")
            Check(CSByte(83) And CInt(0), CInt(0), "csbyte(83) and cint(0)")
            Check(CSByte(0) And CInt(1), CInt(0), "csbyte(0) and cint(1)")
            Check(CSByte(1) And CInt(50), CInt(0), "csbyte(1) and cint(50)")
            Check(CSByte(42) And CInt(0), CInt(0), "csbyte(42) and cint(0)")

            'sbyte and uint
            Check(CSByte(0) And CUInt(1), CLng(0), "csbyte(0) and cuint(1)")
            Check(CSByte(1) And CUInt(50), CLng(0), "csbyte(1) and cuint(50)")
            Check(CSByte(83) And CUInt(0), CLng(0), "csbyte(83) and cuint(0)")
            Check(CSByte(0) And CUInt(1), CLng(0), "csbyte(0) and cuint(1)")
            Check(CSByte(1) And CUInt(50), CLng(0), "csbyte(1) and cuint(50)")
            Check(CSByte(42) And CUInt(0), CLng(0), "csbyte(42) and cuint(0)")

            'sbyte and long
            Check(CSByte(0) And CLng(1), CLng(0), "csbyte(0) and clng(1)")
            Check(CSByte(1) And CLng(50), CLng(0), "csbyte(1) and clng(50)")
            Check(CSByte(83) And CLng(0), CLng(0), "csbyte(83) and clng(0)")
            Check(CSByte(0) And CLng(1), CLng(0), "csbyte(0) and clng(1)")
            Check(CSByte(1) And CLng(50), CLng(0), "csbyte(1) and clng(50)")
            Check(CSByte(42) And CLng(0), CLng(0), "csbyte(42) and clng(0)")

            'sbyte and ulong
            Check(CSByte(0) And CULng(1), CLng(0), "csbyte(0) and culng(1)")
            Check(CSByte(1) And CULng(50), CLng(0), "csbyte(1) and culng(50)")
            Check(CSByte(83) And CULng(0), CLng(0), "csbyte(83) and culng(0)")
            Check(CSByte(0) And CULng(1), CLng(0), "csbyte(0) and culng(1)")
            Check(CSByte(1) And CULng(50), CLng(0), "csbyte(1) and culng(50)")
            Check(CSByte(42) And CULng(0), CLng(0), "csbyte(42) and culng(0)")

            'sbyte and decimal
            Check(CSByte(0) And CDec(1), CLng(0), "csbyte(0) and cdec(1)")
            Check(CSByte(1) And CDec(50), CLng(0), "csbyte(1) and cdec(50)")
            Check(CSByte(83) And CDec(0), CLng(0), "csbyte(83) and cdec(0)")
            Check(CSByte(0) And CDec(1), CLng(0), "csbyte(0) and culng(1)")
            Check(CSByte(1) And CDec(50), CLng(0), "csbyte(1) and cdec(50)")
            Check(CSByte(42) And CDec(0), CLng(0), "csbyte(42) and v(0)")

            'sbyte and single
            Check(CSByte(1) And CSng(1), CLng(1), "csbyte(1) and csng(1)")
            Check(CSByte(83) And CSng(50), CLng(18), "csbyte(83) and csng(50)")
            Check(CSByte(1) And CSng(0), CLng(0), "csbyte(1) and csng(0)")
            Check(CSByte(1) And CSng(0.9), CLng(1), "csbyte(1) and csng(0.9)")
            Check(CSByte(83) And CSng(1.9), CLng(2), "csbyte(83) and csng(1.9)")
            Check(CSByte(1) And CSng(1.5), CLng(0), "csbyte(1) and csng(1.5)")
            Check(CSByte(1) And CSng(1.4999), CLng(1), "csbyte(1) and csng(1.4999)")
            Check(CSByte(1) And CSng(2.4999), CLng(0), "csbyte(1) and csng(2.4999)")
            Check(CSByte(83) And CSng(1.1), CLng(1), "csbyte(83) and csng(1.1)")
            Check(CSByte(0) And CSng(1), CLng(0), "csbyte(0) and csng(1)")
            Check(CSByte(0) And CSng(50), CLng(0), "csbyte(0) and csng(50)")
            Check(CSByte(0) And CSng(0), CLng(0), "csbyte(0) and csng(0)")
            Check(CSByte(0) And CSng(0.6), CLng(0), "csbyte(0) and csng(0.6)")

            'sbyte and double
            Check(CSByte(1) And CDbl(1), CLng(1), "csbyte(1) and cdbl(1)")
            Check(CSByte(83) And CDbl(50), CLng(18), "csbyte(83) and cdbl(50)")
            Check(CSByte(1) And CDbl(0), CLng(0), "csbyte(1) and cdbl(0)")
            Check(CSByte(1) And CDbl(0.9), CLng(1), "csbyte(1) and cdbl(0.9)")
            Check(CSByte(83) And CDbl(1.9), CLng(2), "csbyte(83) and cdbl(1.9)")
            Check(CSByte(1) And CDbl(1.5), CLng(0), "csbyte(1) and cdbl(1.5)")
            Check(CSByte(1) And CDbl(1.4999), CLng(1), "csbyte(1) and cdbl(1.4999)")
            Check(CSByte(1) And CDbl(2.4999), CLng(0), "csbyte(1) and cdbl(2.4999)")
            Check(CSByte(83) And CDbl(1.1), CLng(1), "csbyte(83) and cdbl(1.1)")
            Check(CSByte(0) And CDbl(1), CLng(0), "csbyte(0) and cdbl(1)")
            Check(CSByte(0) And CDbl(50), CLng(0), "csbyte(0) and cdbl(50)")
            Check(CSByte(0) And CDbl(0), CLng(0), "csbyte(0) and cdbl(0)")
            Check(CSByte(0) And CDbl(0.6), CLng(0), "csbyte(0) and cdbl(0.6)")

            'sbyte and date
            'sbyte and char

            'sbyte and string
            Check(CSByte(1) And "1", 1L, "csbyte(1) and '1'")
            Check(CSByte(1) And "0", 0L, "csbyte(1) and '0'")
            Check(CSByte(0) And "1", 0L, "csbyte(0) and '1'")
            Check(CSByte(0) And "0", 0L, "csbyte(0) and '0'")

            'sbyte and object
            Check(CSByte(1) And CObj(1), CInt(1), "csbyte(1) and cobj(1)")
            Check(CSByte(0) And CObj(0), CInt(0), "csbyte(0) and cobj(0)")

            ''''''

            'byte and short
            Check(CByte(0) And CShort(1), CShort(0), "cbyte(0) and cshort(1)")
            Check(CByte(1) And CShort(50), CShort(0), "cbyte(1) and cshort(50)")
            Check(CByte(83) And CShort(0), CShort(0), "cbyte(83) and cshort(0)")
            Check(CByte(0) And CShort(1), CShort(0), "cbyte(0) and cshort(1)")
            Check(CByte(1) And CShort(50), CShort(0), "cbyte(1) and cshort(50)")
            Check(CByte(42) And CShort(0), CShort(0), "cbyte(42) and cshort(0)")

            'byte and ushort
            Check(CByte(0) And CUShort(1), CUShort(0), "cbyte(0) and cushort(1)")
            Check(CByte(1) And CUShort(50), CUShort(0), "cbyte(1) and cushort(50)")
            Check(CByte(83) And CUShort(0), CUShort(0), "cbyte(83) and cushort(0)")
            Check(CByte(0) And CUShort(1), CUShort(0), "cbyte(0) and cushort(1)")
            Check(CByte(1) And CUShort(50), CUShort(0), "cbyte(1) and cushort(50)")
            Check(CByte(42) And CUShort(0), CUShort(0), "cbyte(42) and cushort(0)")

            'byte and int
            Check(CByte(0) And CInt(1), CInt(0), "cbyte(0) and cint(1)")
            Check(CByte(1) And CInt(50), CInt(0), "cbyte(1) and cint(50)")
            Check(CByte(83) And CInt(0), CInt(0), "cbyte(83) and cint(0)")
            Check(CByte(0) And CInt(1), CInt(0), "cbyte(0) and cint(1)")
            Check(CByte(1) And CInt(50), CInt(0), "cbyte(1) and cint(50)")
            Check(CByte(42) And CInt(0), CInt(0), "cbyte(42) and cint(0)")

            'byte and uint
            Check(CByte(0) And CUInt(1), CUInt(0), "cbyte(0) and cuint(1)")
            Check(CByte(1) And CUInt(50), CUInt(0), "cbyte(1) and cuint(50)")
            Check(CByte(83) And CUInt(0), CUInt(0), "cbyte(83) and cuint(0)")
            Check(CByte(0) And CUInt(1), CUInt(0), "cbyte(0) and cuint(1)")
            Check(CByte(1) And CUInt(50), CUInt(0), "cbyte(1) and cuint(50)")
            Check(CByte(42) And CUInt(0), CUInt(0), "cbyte(42) and cuint(0)")

            'byte and long
            Check(CByte(0) And CLng(1), CLng(0), "cbyte(0) and clng(1)")
            Check(CByte(1) And CLng(50), CLng(0), "cbyte(1) and clng(50)")
            Check(CByte(83) And CLng(0), CLng(0), "cbyte(83) and clng(0)")
            Check(CByte(0) And CLng(1), CLng(0), "cbyte(0) and clng(1)")
            Check(CByte(1) And CLng(50), CLng(0), "cbyte(1) and clng(50)")
            Check(CByte(42) And CLng(0), CLng(0), "cbyte(42) and clng(0)")

            'byte and ulong
            Check(CByte(0) And CULng(1), CULng(0), "cbyte(0) and culng(1)")
            Check(CByte(1) And CULng(50), CULng(0), "cbyte(1) and culng(50)")
            Check(CByte(83) And CULng(0), CULng(0), "cbyte(83) and culng(0)")
            Check(CByte(0) And CULng(1), CULng(0), "cbyte(0) and culng(1)")
            Check(CByte(1) And CULng(50), CULng(0), "cbyte(1) and culng(50)")
            Check(CByte(42) And CULng(0), CULng(0), "cbyte(42) and culng(0)")

            'byte and decimal
            Check(CByte(0) And CDec(1), CLng(0), "cbyte(0) and cdec(1)")
            Check(CByte(1) And CDec(50), CLng(0), "cbyte(1) and cdec(50)")
            Check(CByte(83) And CDec(0), CLng(0), "cbyte(83) and cdec(0)")
            Check(CByte(0) And CDec(1), CLng(0), "cbyte(0) and culng(1)")
            Check(CByte(1) And CDec(50), CLng(0), "cbyte(1) and cdec(50)")
            Check(CByte(42) And CDec(0), CLng(0), "cbyte(42) and v(0)")

            'byte and single
            Check(CByte(1) And CSng(1), CLng(1), "cbyte(1) and csng(1)")
            Check(CByte(83) And CSng(50), CLng(18), "cbyte(83) and csng(50)")
            Check(CByte(1) And CSng(0), CLng(0), "cbyte(1) and csng(0)")
            Check(CByte(1) And CSng(0.9), CLng(1), "cbyte(1) and csng(0.9)")
            Check(CByte(83) And CSng(1.9), CLng(2), "cbyte(83) and csng(1.9)")
            Check(CByte(1) And CSng(1.5), CLng(0), "cbyte(1) and csng(1.5)")
            Check(CByte(1) And CSng(1.4999), CLng(1), "cbyte(1) and csng(1.4999)")
            Check(CByte(1) And CSng(2.4999), CLng(0), "cbyte(1) and csng(2.4999)")
            Check(CByte(83) And CSng(1.1), CLng(1), "cbyte(83) and csng(1.1)")
            Check(CByte(0) And CSng(1), CLng(0), "cbyte(0) and csng(1)")
            Check(CByte(0) And CSng(50), CLng(0), "cbyte(0) and csng(50)")
            Check(CByte(0) And CSng(0), CLng(0), "cbyte(0) and csng(0)")
            Check(CByte(0) And CSng(0.6), CLng(0), "cbyte(0) and csng(0.6)")

            'byte and double
            Check(CByte(1) And CDbl(1), CLng(1), "cbyte(1) and cdbl(1)")
            Check(CByte(83) And CDbl(50), CLng(18), "cbyte(83) and cdbl(50)")
            Check(CByte(1) And CDbl(0), CLng(0), "cbyte(1) and cdbl(0)")
            Check(CByte(1) And CDbl(0.9), CLng(1), "cbyte(1) and cdbl(0.9)")
            Check(CByte(83) And CDbl(1.9), CLng(2), "cbyte(83) and cdbl(1.9)")
            Check(CByte(1) And CDbl(1.5), CLng(0), "cbyte(1) and cdbl(1.5)")
            Check(CByte(1) And CDbl(1.4999), CLng(1), "cbyte(1) and cdbl(1.4999)")
            Check(CByte(1) And CDbl(2.4999), CLng(0), "cbyte(1) and cdbl(2.4999)")
            Check(CByte(83) And CDbl(1.1), CLng(1), "cbyte(83) and cdbl(1.1)")
            Check(CByte(0) And CDbl(1), CLng(0), "cbyte(0) and cdbl(1)")
            Check(CByte(0) And CDbl(50), CLng(0), "cbyte(0) and cdbl(50)")
            Check(CByte(0) And CDbl(0), CLng(0), "cbyte(0) and cdbl(0)")
            Check(CByte(0) And CDbl(0.6), CLng(0), "cbyte(0) and cdbl(0.6)")

            'byte and date
            'byte and char

            'byte and string
            Check(CByte(1) And "1", 1L, "cbyte(1) and '1'")
            Check(CByte(1) And "0", 0L, "cbyte(1) and '0'")
            Check(CByte(0) And "1", 0L, "cbyte(0) and '1'")
            Check(CByte(0) And "0", 0L, "cbyte(0) and '0'")

            'byte and object
            Check(CByte(1) And CObj(1), CInt(1), "cbyte(1) and cobj(1)")
            Check(CByte(0) And CObj(0), CInt(0), "cbyte(0) and cobj(0)")

            ''''''

            'short and ushort
            Check(CShort(0) And CUShort(1), CInt(0), "cshort(0) and cushort(1)")
            Check(CShort(1) And CUShort(50), CInt(0), "cshort(1) and cushort(50)")
            Check(CShort(83) And CUShort(0), CInt(0), "cshort(83) and cushort(0)")
            Check(CShort(0) And CUShort(1), CInt(0), "cshort(0) and cushort(1)")
            Check(CShort(1) And CUShort(50), CInt(0), "cshort(1) and cushort(50)")
            Check(CShort(42) And CUShort(0), CInt(0), "cshort(42) and cushort(0)")

            'short and int
            Check(CShort(0) And CInt(1), CInt(0), "cshort(0) and cint(1)")
            Check(CShort(1) And CInt(50), CInt(0), "cshort(1) and cint(50)")
            Check(CShort(83) And CInt(0), CInt(0), "cshort(83) and cint(0)")
            Check(CShort(0) And CInt(1), CInt(0), "cshort(0) and cint(1)")
            Check(CShort(1) And CInt(50), CInt(0), "cshort(1) and cint(50)")
            Check(CShort(42) And CInt(0), CInt(0), "cshort(42) and cint(0)")

            'short and uint
            Check(CShort(0) And CUInt(1), CLng(0), "cshort(0) and cuint(1)")
            Check(CShort(1) And CUInt(50), CLng(0), "cshort(1) and cuint(50)")
            Check(CShort(83) And CUInt(0), CLng(0), "cshort(83) and cuint(0)")
            Check(CShort(0) And CUInt(1), CLng(0), "cshort(0) and cuint(1)")
            Check(CShort(1) And CUInt(50), CLng(0), "cshort(1) and cuint(50)")
            Check(CShort(42) And CUInt(0), CLng(0), "cshort(42) and cuint(0)")

            'short and long
            Check(CShort(0) And CLng(1), CLng(0), "cshort(0) and clng(1)")
            Check(CShort(1) And CLng(50), CLng(0), "cshort(1) and clng(50)")
            Check(CShort(83) And CLng(0), CLng(0), "cshort(83) and clng(0)")
            Check(CShort(0) And CLng(1), CLng(0), "cshort(0) and clng(1)")
            Check(CShort(1) And CLng(50), CLng(0), "cshort(1) and clng(50)")
            Check(CShort(42) And CLng(0), CLng(0), "cshort(42) and clng(0)")

            'short and ulong
            Check(CShort(0) And CULng(1), CLng(0), "cshort(0) and culng(1)")
            Check(CShort(1) And CULng(50), CLng(0), "cshort(1) and culng(50)")
            Check(CShort(83) And CULng(0), CLng(0), "cshort(83) and culng(0)")
            Check(CShort(0) And CULng(1), CLng(0), "cshort(0) and culng(1)")
            Check(CShort(1) And CULng(50), CLng(0), "cshort(1) and culng(50)")
            Check(CShort(42) And CULng(0), CLng(0), "cshort(42) and culng(0)")

            'short and decimal
            Check(CShort(0) And CDec(1), CLng(0), "cshort(0) and cdec(1)")
            Check(CShort(1) And CDec(50), CLng(0), "cshort(1) and cdec(50)")
            Check(CShort(83) And CDec(0), CLng(0), "cshort(83) and cdec(0)")
            Check(CShort(0) And CDec(1), CLng(0), "cshort(0) and culng(1)")
            Check(CShort(1) And CDec(50), CLng(0), "cshort(1) and cdec(50)")
            Check(CShort(42) And CDec(0), CLng(0), "cshort(42) and v(0)")

            'short and single
            Check(CShort(1) And CSng(1), CLng(1), "cshort(1) and csng(1)")
            Check(CShort(83) And CSng(50), CLng(18), "cshort(83) and csng(50)")
            Check(CShort(1) And CSng(0), CLng(0), "cshort(1) and csng(0)")
            Check(CShort(1) And CSng(0.9), CLng(1), "cshort(1) and csng(0.9)")
            Check(CShort(83) And CSng(1.9), CLng(2), "cshort(83) and csng(1.9)")
            Check(CShort(1) And CSng(1.5), CLng(0), "cshort(1) and csng(1.5)")
            Check(CShort(1) And CSng(1.4999), CLng(1), "cshort(1) and csng(1.4999)")
            Check(CShort(1) And CSng(2.4999), CLng(0), "cshort(1) and csng(2.4999)")
            Check(CShort(83) And CSng(1.1), CLng(1), "cshort(83) and csng(1.1)")
            Check(CShort(0) And CSng(1), CLng(0), "cshort(0) and csng(1)")
            Check(CShort(0) And CSng(50), CLng(0), "cshort(0) and csng(50)")
            Check(CShort(0) And CSng(0), CLng(0), "cshort(0) and csng(0)")
            Check(CShort(0) And CSng(0.6), CLng(0), "cshort(0) and csng(0.6)")

            'short and double
            Check(CShort(1) And CDbl(1), CLng(1), "cshort(1) and cdbl(1)")
            Check(CShort(83) And CDbl(50), CLng(18), "cshort(83) and cdbl(50)")
            Check(CShort(1) And CDbl(0), CLng(0), "cshort(1) and cdbl(0)")
            Check(CShort(1) And CDbl(0.9), CLng(1), "cshort(1) and cdbl(0.9)")
            Check(CShort(83) And CDbl(1.9), CLng(2), "cshort(83) and cdbl(1.9)")
            Check(CShort(1) And CDbl(1.5), CLng(0), "cshort(1) and cdbl(1.5)")
            Check(CShort(1) And CDbl(1.4999), CLng(1), "cshort(1) and cdbl(1.4999)")
            Check(CShort(1) And CDbl(2.4999), CLng(0), "cshort(1) and cdbl(2.4999)")
            Check(CShort(83) And CDbl(1.1), CLng(1), "cshort(83) and cdbl(1.1)")
            Check(CShort(0) And CDbl(1), CLng(0), "cshort(0) and cdbl(1)")
            Check(CShort(0) And CDbl(50), CLng(0), "cshort(0) and cdbl(50)")
            Check(CShort(0) And CDbl(0), CLng(0), "cshort(0) and cdbl(0)")
            Check(CShort(0) And CDbl(0.6), CLng(0), "cshort(0) and cdbl(0.6)")

            'short and date
            'short and char

            'short and string
            Check(CShort(1) And "1", 1L, "cshort(1) and '1'")
            Check(CShort(1) And "0", 0L, "cshort(1) and '0'")
            Check(CShort(0) And "1", 0L, "cshort(0) and '1'")
            Check(CShort(0) And "0", 0L, "cshort(0) and '0'")

            'short and object
            Check(CShort(1) And CObj(1), CInt(1), "cshort(1) and cobj(1)")
            Check(CShort(0) And CObj(0), CInt(0), "cshort(0) and cobj(0)")

            ''''''

            'ushort and int
            Check(CUShort(0) And CInt(1), CInt(0), "cushort(0) and cint(1)")
            Check(CUShort(1) And CInt(50), CInt(0), "cushort(1) and cint(50)")
            Check(CUShort(83) And CInt(0), CInt(0), "cushort(83) and cint(0)")
            Check(CUShort(0) And CInt(1), CInt(0), "cushort(0) and cint(1)")
            Check(CUShort(1) And CInt(50), CInt(0), "cushort(1) and cint(50)")
            Check(CUShort(42) And CInt(0), CInt(0), "cushort(42) and cint(0)")

            'ushort and uint
            Check(CUShort(0) And CUInt(1), CUInt(0), "cushort(0) and cuint(1)")
            Check(CUShort(1) And CUInt(50), CUInt(0), "cushort(1) and cuint(50)")
            Check(CUShort(83) And CUInt(0), CUInt(0), "cushort(83) and cuint(0)")
            Check(CUShort(0) And CUInt(1), CUInt(0), "cushort(0) and cuint(1)")
            Check(CUShort(1) And CUInt(50), CUInt(0), "cushort(1) and cuint(50)")
            Check(CUShort(42) And CUInt(0), CUInt(0), "cushort(42) and cuint(0)")

            'ushort and long
            Check(CUShort(0) And CLng(1), CLng(0), "cushort(0) and clng(1)")
            Check(CUShort(1) And CLng(50), CLng(0), "cushort(1) and clng(50)")
            Check(CUShort(83) And CLng(0), CLng(0), "cushort(83) and clng(0)")
            Check(CUShort(0) And CLng(1), CLng(0), "cushort(0) and clng(1)")
            Check(CUShort(1) And CLng(50), CLng(0), "cushort(1) and clng(50)")
            Check(CUShort(42) And CLng(0), CLng(0), "cushort(42) and clng(0)")

            'ushort and ulong
            Check(CUShort(0) And CULng(1), CULng(0), "cushort(0) and culng(1)")
            Check(CUShort(1) And CULng(50), CULng(0), "cushort(1) and culng(50)")
            Check(CUShort(83) And CULng(0), CULng(0), "cushort(83) and culng(0)")
            Check(CUShort(0) And CULng(1), CULng(0), "cushort(0) and culng(1)")
            Check(CUShort(1) And CULng(50), CULng(0), "cushort(1) and culng(50)")
            Check(CUShort(42) And CULng(0), CULng(0), "cushort(42) and culng(0)")

            'ushort and decimal
            Check(CUShort(0) And CDec(1), CLng(0), "cushort(0) and cdec(1)")
            Check(CUShort(1) And CDec(50), CLng(0), "cushort(1) and cdec(50)")
            Check(CUShort(83) And CDec(0), CLng(0), "cushort(83) and cdec(0)")
            Check(CUShort(0) And CDec(1), CLng(0), "cushort(0) and culng(1)")
            Check(CUShort(1) And CDec(50), CLng(0), "cushort(1) and cdec(50)")
            Check(CUShort(42) And CDec(0), CLng(0), "cushort(42) and v(0)")

            'ushort and single
            Check(CUShort(1) And CSng(1), CLng(1), "cushort(1) and csng(1)")
            Check(CUShort(83) And CSng(50), CLng(18), "cushort(83) and csng(50)")
            Check(CUShort(1) And CSng(0), CLng(0), "cushort(1) and csng(0)")
            Check(CUShort(1) And CSng(0.9), CLng(1), "cushort(1) and csng(0.9)")
            Check(CUShort(83) And CSng(1.9), CLng(2), "cushort(83) and csng(1.9)")
            Check(CUShort(1) And CSng(1.5), CLng(0), "cushort(1) and csng(1.5)")
            Check(CUShort(1) And CSng(1.4999), CLng(1), "cushort(1) and csng(1.4999)")
            Check(CUShort(1) And CSng(2.4999), CLng(0), "cushort(1) and csng(2.4999)")
            Check(CUShort(83) And CSng(1.1), CLng(1), "cushort(83) and csng(1.1)")
            Check(CUShort(0) And CSng(1), CLng(0), "cushort(0) and csng(1)")
            Check(CUShort(0) And CSng(50), CLng(0), "cushort(0) and csng(50)")
            Check(CUShort(0) And CSng(0), CLng(0), "cushort(0) and csng(0)")
            Check(CUShort(0) And CSng(0.6), CLng(0), "cushort(0) and csng(0.6)")

            'ushort and double
            Check(CUShort(1) And CDbl(1), CLng(1), "cushort(1) and cdbl(1)")
            Check(CUShort(83) And CDbl(50), CLng(18), "cushort(83) and cdbl(50)")
            Check(CUShort(1) And CDbl(0), CLng(0), "cushort(1) and cdbl(0)")
            Check(CUShort(1) And CDbl(0.9), CLng(1), "cushort(1) and cdbl(0.9)")
            Check(CUShort(83) And CDbl(1.9), CLng(2), "cushort(83) and cdbl(1.9)")
            Check(CUShort(1) And CDbl(1.5), CLng(0), "cushort(1) and cdbl(1.5)")
            Check(CUShort(1) And CDbl(1.4999), CLng(1), "cushort(1) and cdbl(1.4999)")
            Check(CUShort(1) And CDbl(2.4999), CLng(0), "cushort(1) and cdbl(2.4999)")
            Check(CUShort(83) And CDbl(1.1), CLng(1), "cushort(83) and cdbl(1.1)")
            Check(CUShort(0) And CDbl(1), CLng(0), "cushort(0) and cdbl(1)")
            Check(CUShort(0) And CDbl(50), CLng(0), "cushort(0) and cdbl(50)")
            Check(CUShort(0) And CDbl(0), CLng(0), "cushort(0) and cdbl(0)")
            Check(CUShort(0) And CDbl(0.6), CLng(0), "cushort(0) and cdbl(0.6)")

            'ushort and date
            'ushort and char

            'ushort and string
            Check(CUShort(1) And "1", 1L, "cushort(1) and '1'")
            Check(CUShort(1) And "0", 0L, "cushort(1) and '0'")
            Check(CUShort(0) And "1", 0L, "cushort(0) and '1'")
            Check(CUShort(0) And "0", 0L, "cushort(0) and '0'")

            'ushort and object
            Check(CUShort(1) And CObj(1), CInt(1), "cushort(1) and cobj(1)")
            Check(CUShort(0) And CObj(0), CInt(0), "cushort(0) and cobj(0)")

            ''''''

            'int and uint
            Check(CInt(0) And CUInt(1), CLng(0), "cint(0) and cuint(1)")
            Check(CInt(1) And CUInt(50), CLng(0), "cint(1) and cuint(50)")
            Check(CInt(83) And CUInt(0), CLng(0), "cint(83) and cuint(0)")
            Check(CInt(0) And CUInt(1), CLng(0), "cint(0) and cuint(1)")
            Check(CInt(1) And CUInt(50), CLng(0), "cint(1) and cuint(50)")
            Check(CInt(42) And CUInt(0), CLng(0), "cint(42) and cuint(0)")

            'int and long
            Check(CInt(0) And CLng(1), CLng(0), "cint(0) and clng(1)")
            Check(CInt(1) And CLng(50), CLng(0), "cint(1) and clng(50)")
            Check(CInt(83) And CLng(0), CLng(0), "cint(83) and clng(0)")
            Check(CInt(0) And CLng(1), CLng(0), "cint(0) and clng(1)")
            Check(CInt(1) And CLng(50), CLng(0), "cint(1) and clng(50)")
            Check(CInt(42) And CLng(0), CLng(0), "cint(42) and clng(0)")

            'int and ulong
            Check(CInt(0) And CULng(1), CLng(0), "cint(0) and culng(1)")
            Check(CInt(1) And CULng(50), CLng(0), "cint(1) and culng(50)")
            Check(CInt(83) And CULng(0), CLng(0), "cint(83) and culng(0)")
            Check(CInt(0) And CULng(1), CLng(0), "cint(0) and culng(1)")
            Check(CInt(1) And CULng(50), CLng(0), "cint(1) and culng(50)")
            Check(CInt(42) And CULng(0), CLng(0), "cint(42) and culng(0)")

            'int and decimal
            Check(CInt(0) And CDec(1), CLng(0), "cint(0) and cdec(1)")
            Check(CInt(1) And CDec(50), CLng(0), "cint(1) and cdec(50)")
            Check(CInt(83) And CDec(0), CLng(0), "cint(83) and cdec(0)")
            Check(CInt(0) And CDec(1), CLng(0), "cint(0) and culng(1)")
            Check(CInt(1) And CDec(50), CLng(0), "cint(1) and cdec(50)")
            Check(CInt(42) And CDec(0), CLng(0), "cint(42) and v(0)")

            'int and single
            Check(CInt(1) And CSng(1), CLng(1), "cint(1) and csng(1)")
            Check(CInt(83) And CSng(50), CLng(18), "cint(83) and csng(50)")
            Check(CInt(1) And CSng(0), CLng(0), "cint(1) and csng(0)")
            Check(CInt(1) And CSng(0.9), CLng(1), "cint(1) and csng(0.9)")
            Check(CInt(83) And CSng(1.9), CLng(2), "cint(83) and csng(1.9)")
            Check(CInt(1) And CSng(1.5), CLng(0), "cint(1) and csng(1.5)")
            Check(CInt(1) And CSng(1.4999), CLng(1), "cint(1) and csng(1.4999)")
            Check(CInt(1) And CSng(2.4999), CLng(0), "cint(1) and csng(2.4999)")
            Check(CInt(83) And CSng(1.1), CLng(1), "cint(83) and csng(1.1)")
            Check(CInt(0) And CSng(1), CLng(0), "cint(0) and csng(1)")
            Check(CInt(0) And CSng(50), CLng(0), "cint(0) and csng(50)")
            Check(CInt(0) And CSng(0), CLng(0), "cint(0) and csng(0)")
            Check(CInt(0) And CSng(0.6), CLng(0), "cint(0) and csng(0.6)")

            'int and double
            Check(CInt(1) And CDbl(1), CLng(1), "cint(1) and cdbl(1)")
            Check(CInt(83) And CDbl(50), CLng(18), "cint(83) and cdbl(50)")
            Check(CInt(1) And CDbl(0), CLng(0), "cint(1) and cdbl(0)")
            Check(CInt(1) And CDbl(0.9), CLng(1), "cint(1) and cdbl(0.9)")
            Check(CInt(83) And CDbl(1.9), CLng(2), "cint(83) and cdbl(1.9)")
            Check(CInt(1) And CDbl(1.5), CLng(0), "cint(1) and cdbl(1.5)")
            Check(CInt(1) And CDbl(1.4999), CLng(1), "cint(1) and cdbl(1.4999)")
            Check(CInt(1) And CDbl(2.4999), CLng(0), "cint(1) and cdbl(2.4999)")
            Check(CInt(83) And CDbl(1.1), CLng(1), "cint(83) and cdbl(1.1)")
            Check(CInt(0) And CDbl(1), CLng(0), "cint(0) and cdbl(1)")
            Check(CInt(0) And CDbl(50), CLng(0), "cint(0) and cdbl(50)")
            Check(CInt(0) And CDbl(0), CLng(0), "cint(0) and cdbl(0)")
            Check(CInt(0) And CDbl(0.6), CLng(0), "cint(0) and cdbl(0.6)")

            'int and date
            'int and char

            'int and string
            Check(CInt(1) And "1", 1L, "cint(1) and '1'")
            Check(CInt(1) And "0", 0L, "cint(1) and '0'")
            Check(CInt(0) And "1", 0L, "cint(0) and '1'")
            Check(CInt(0) And "0", 0L, "cint(0) and '0'")

            'int and object
            Check(CInt(1) And CObj(1), CInt(1), "cint(1) and cobj(1)")
            Check(CInt(0) And CObj(0), CInt(0), "cint(0) and cobj(0)")

            ''''''

            'uint and long
            Check(CUInt(0) And CLng(1), CLng(0), "cuint(0) and clng(1)")
            Check(CUInt(1) And CLng(50), CLng(0), "cuint(1) and clng(50)")
            Check(CUInt(83) And CLng(0), CLng(0), "cuint(83) and clng(0)")
            Check(CUInt(0) And CLng(1), CLng(0), "cuint(0) and clng(1)")
            Check(CUInt(1) And CLng(50), CLng(0), "cuint(1) and clng(50)")
            Check(CUInt(42) And CLng(0), CLng(0), "cuint(42) and clng(0)")

            'uint and ulong
            Check(CUInt(0) And CULng(1), CULng(0), "cuint(0) and culng(1)")
            Check(CUInt(1) And CULng(50), CULng(0), "cuint(1) and culng(50)")
            Check(CUInt(83) And CULng(0), CULng(0), "cuint(83) and culng(0)")
            Check(CUInt(0) And CULng(1), CULng(0), "cuint(0) and culng(1)")
            Check(CUInt(1) And CULng(50), CULng(0), "cuint(1) and culng(50)")
            Check(CUInt(42) And CULng(0), CULng(0), "cuint(42) and culng(0)")

            'uint and decimal
            Check(CUInt(0) And CDec(1), CLng(0), "cuint(0) and cdec(1)")
            Check(CUInt(1) And CDec(50), CLng(0), "cuint(1) and cdec(50)")
            Check(CUInt(83) And CDec(0), CLng(0), "cuint(83) and cdec(0)")
            Check(CUInt(0) And CDec(1), CLng(0), "cuint(0) and culng(1)")
            Check(CUInt(1) And CDec(50), CLng(0), "cuint(1) and cdec(50)")
            Check(CUInt(42) And CDec(0), CLng(0), "cuint(42) and v(0)")

            'uint and single
            Check(CUInt(1) And CSng(1), CLng(1), "cuint(1) and csng(1)")
            Check(CUInt(83) And CSng(50), CLng(18), "cuint(83) and csng(50)")
            Check(CUInt(1) And CSng(0), CLng(0), "cuint(1) and csng(0)")
            Check(CUInt(1) And CSng(0.9), CLng(1), "cuint(1) and csng(0.9)")
            Check(CUInt(83) And CSng(1.9), CLng(2), "cuint(83) and csng(1.9)")
            Check(CUInt(1) And CSng(1.5), CLng(0), "cuint(1) and csng(1.5)")
            Check(CUInt(1) And CSng(1.4999), CLng(1), "cuint(1) and csng(1.4999)")
            Check(CUInt(1) And CSng(2.4999), CLng(0), "cuint(1) and csng(2.4999)")
            Check(CUInt(83) And CSng(1.1), CLng(1), "cuint(83) and csng(1.1)")
            Check(CUInt(0) And CSng(1), CLng(0), "cuint(0) and csng(1)")
            Check(CUInt(0) And CSng(50), CLng(0), "cuint(0) and csng(50)")
            Check(CUInt(0) And CSng(0), CLng(0), "cuint(0) and csng(0)")
            Check(CUInt(0) And CSng(0.6), CLng(0), "cuint(0) and csng(0.6)")

            'uint and double
            Check(CUInt(1) And CDbl(1), CLng(1), "cuint(1) and cdbl(1)")
            Check(CUInt(83) And CDbl(50), CLng(18), "cuint(83) and cdbl(50)")
            Check(CUInt(1) And CDbl(0), CLng(0), "cuint(1) and cdbl(0)")
            Check(CUInt(1) And CDbl(0.9), CLng(1), "cuint(1) and cdbl(0.9)")
            Check(CUInt(83) And CDbl(1.9), CLng(2), "cuint(83) and cdbl(1.9)")
            Check(CUInt(1) And CDbl(1.5), CLng(0), "cuint(1) and cdbl(1.5)")
            Check(CUInt(1) And CDbl(1.4999), CLng(1), "cuint(1) and cdbl(1.4999)")
            Check(CUInt(1) And CDbl(2.4999), CLng(0), "cuint(1) and cdbl(2.4999)")
            Check(CUInt(83) And CDbl(1.1), CLng(1), "cuint(83) and cdbl(1.1)")
            Check(CUInt(0) And CDbl(1), CLng(0), "cuint(0) and cdbl(1)")
            Check(CUInt(0) And CDbl(50), CLng(0), "cuint(0) and cdbl(50)")
            Check(CUInt(0) And CDbl(0), CLng(0), "cuint(0) and cdbl(0)")
            Check(CUInt(0) And CDbl(0.6), CLng(0), "cuint(0) and cdbl(0.6)")

            'uint and date
            'uint and char

            'uint and string
            Check(CUInt(1) And "1", 1L, "cuint(1) and '1'")
            Check(CUInt(1) And "0", 0L, "cuint(1) and '0'")
            Check(CUInt(0) And "1", 0L, "cuint(0) and '1'")
            Check(CUInt(0) And "0", 0L, "cuint(0) and '0'")

            'uint and object
            Check(CUInt(1) And CObj(1), CLng(1), "cuint(1) and cobj(1)")
            Check(CUInt(0) And CObj(0), CLng(0), "cuint(0) and cobj(0)")

            ''''''

            'long and ulong
            Check(CLng(0) And CULng(1), CLng(0), "clng(0) and culng(1)")
            Check(CLng(1) And CULng(50), CLng(0), "clng(1) and culng(50)")
            Check(CLng(83) And CULng(0), CLng(0), "clng(83) and culng(0)")
            Check(CLng(0) And CULng(1), CLng(0), "clng(0) and culng(1)")
            Check(CLng(1) And CULng(50), CLng(0), "clng(1) and culng(50)")
            Check(CLng(42) And CULng(0), CLng(0), "clng(42) and culng(0)")

            'long and decimal
            Check(CLng(0) And CDec(1), CLng(0), "clng(0) and cdec(1)")
            Check(CLng(1) And CDec(50), CLng(0), "clng(1) and cdec(50)")
            Check(CLng(83) And CDec(0), CLng(0), "clng(83) and cdec(0)")
            Check(CLng(0) And CDec(1), CLng(0), "clng(0) and culng(1)")
            Check(CLng(1) And CDec(50), CLng(0), "clng(1) and cdec(50)")
            Check(CLng(42) And CDec(0), CLng(0), "clng(42) and v(0)")

            'long and single
            Check(CLng(1) And CSng(1), CLng(1), "clng(1) and csng(1)")
            Check(CLng(83) And CSng(50), CLng(18), "clng(83) and csng(50)")
            Check(CLng(1) And CSng(0), CLng(0), "clng(1) and csng(0)")
            Check(CLng(1) And CSng(0.9), CLng(1), "clng(1) and csng(0.9)")
            Check(CLng(83) And CSng(1.9), CLng(2), "clng(83) and csng(1.9)")
            Check(CLng(1) And CSng(1.5), CLng(0), "clng(1) and csng(1.5)")
            Check(CLng(1) And CSng(1.4999), CLng(1), "clng(1) and csng(1.4999)")
            Check(CLng(1) And CSng(2.4999), CLng(0), "clng(1) and csng(2.4999)")
            Check(CLng(83) And CSng(1.1), CLng(1), "clng(83) and csng(1.1)")
            Check(CLng(0) And CSng(1), CLng(0), "clng(0) and csng(1)")
            Check(CLng(0) And CSng(50), CLng(0), "clng(0) and csng(50)")
            Check(CLng(0) And CSng(0), CLng(0), "clng(0) and csng(0)")
            Check(CLng(0) And CSng(0.6), CLng(0), "clng(0) and csng(0.6)")

            'long and double
            Check(CLng(1) And CDbl(1), CLng(1), "clng(1) and cdbl(1)")
            Check(CLng(83) And CDbl(50), CLng(18), "clng(83) and cdbl(50)")
            Check(CLng(1) And CDbl(0), CLng(0), "clng(1) and cdbl(0)")
            Check(CLng(1) And CDbl(0.9), CLng(1), "clng(1) and cdbl(0.9)")
            Check(CLng(83) And CDbl(1.9), CLng(2), "clng(83) and cdbl(1.9)")
            Check(CLng(1) And CDbl(1.5), CLng(0), "clng(1) and cdbl(1.5)")
            Check(CLng(1) And CDbl(1.4999), CLng(1), "clng(1) and cdbl(1.4999)")
            Check(CLng(1) And CDbl(2.4999), CLng(0), "clng(1) and cdbl(2.4999)")
            Check(CLng(83) And CDbl(1.1), CLng(1), "clng(83) and cdbl(1.1)")
            Check(CLng(0) And CDbl(1), CLng(0), "clng(0) and cdbl(1)")
            Check(CLng(0) And CDbl(50), CLng(0), "clng(0) and cdbl(50)")
            Check(CLng(0) And CDbl(0), CLng(0), "clng(0) and cdbl(0)")
            Check(CLng(0) And CDbl(0.6), CLng(0), "clng(0) and cdbl(0.6)")

            'long and date
            'long and char

            'long and string
            Check(CLng(1) And "1", 1L, "clng(1) and '1'")
            Check(CLng(1) And "0", 0L, "clng(1) and '0'")
            Check(CLng(0) And "1", 0L, "clng(0) and '1'")
            Check(CLng(0) And "0", 0L, "clng(0) and '0'")

            'long and object
            Check(CLng(1) And CObj(1), CLng(1), "clng(1) and cobj(1)")
            Check(CLng(0) And CObj(0), CLng(0), "clng(0) and cobj(0)")

            ''''''

            'ulong and decimal
            Check(CULng(0) And CDec(1), CLng(0), "culng(0) and cdec(1)")
            Check(CULng(1) And CDec(50), CLng(0), "culng(1) and cdec(50)")
            Check(CULng(83) And CDec(0), CLng(0), "culng(83) and cdec(0)")
            Check(CULng(0) And CDec(1), CLng(0), "culng(0) and culng(1)")
            Check(CULng(1) And CDec(50), CLng(0), "culng(1) and cdec(50)")
            Check(CULng(42) And CDec(0), CLng(0), "culng(42) and v(0)")

            'ulong and single
            Check(CULng(1) And CSng(1), CLng(1), "culng(1) and csng(1)")
            Check(CULng(83) And CSng(50), CLng(18), "culng(83) and csng(50)")
            Check(CULng(1) And CSng(0), CLng(0), "culng(1) and csng(0)")
            Check(CULng(1) And CSng(0.9), CLng(1), "culng(1) and csng(0.9)")
            Check(CULng(83) And CSng(1.9), CLng(2), "culng(83) and csng(1.9)")
            Check(CULng(1) And CSng(1.5), CLng(0), "culng(1) and csng(1.5)")
            Check(CULng(1) And CSng(1.4999), CLng(1), "culng(1) and csng(1.4999)")
            Check(CULng(1) And CSng(2.4999), CLng(0), "culng(1) and csng(2.4999)")
            Check(CULng(83) And CSng(1.1), CLng(1), "culng(83) and csng(1.1)")
            Check(CULng(0) And CSng(1), CLng(0), "culng(0) and csng(1)")
            Check(CULng(0) And CSng(50), CLng(0), "culng(0) and csng(50)")
            Check(CULng(0) And CSng(0), CLng(0), "culng(0) and csng(0)")
            Check(CULng(0) And CSng(0.6), CLng(0), "culng(0) and csng(0.6)")

            'ulong and double
            Check(CULng(1) And CDbl(1), CLng(1), "culng(1) and cdbl(1)")
            Check(CULng(83) And CDbl(50), CLng(18), "culng(83) and cdbl(50)")
            Check(CULng(1) And CDbl(0), CLng(0), "culng(1) and cdbl(0)")
            Check(CULng(1) And CDbl(0.9), CLng(1), "culng(1) and cdbl(0.9)")
            Check(CULng(83) And CDbl(1.9), CLng(2), "culng(83) and cdbl(1.9)")
            Check(CULng(1) And CDbl(1.5), CLng(0), "culng(1) and cdbl(1.5)")
            Check(CULng(1) And CDbl(1.4999), CLng(1), "culng(1) and cdbl(1.4999)")
            Check(CULng(1) And CDbl(2.4999), CLng(0), "culng(1) and cdbl(2.4999)")
            Check(CULng(83) And CDbl(1.1), CLng(1), "culng(83) and cdbl(1.1)")
            Check(CULng(0) And CDbl(1), CLng(0), "culng(0) and cdbl(1)")
            Check(CULng(0) And CDbl(50), CLng(0), "culng(0) and cdbl(50)")
            Check(CULng(0) And CDbl(0), CLng(0), "culng(0) and cdbl(0)")
            Check(CULng(0) And CDbl(0.6), CLng(0), "culng(0) and cdbl(0.6)")

            'ulong and date
            'ulong and char

            'ulong and string
            Check(CULng(1) And "1", 1L, "culng(1) and '1'")
            Check(CULng(1) And "0", 0L, "culng(1) and '0'")
            Check(CULng(0) And "1", 0L, "culng(0) and '1'")
            Check(CULng(0) And "0", 0L, "culng(0) and '0'")

            'ulong and object
            Check(CULng(1) And CObj(1), CLng(1), "culng(1) and cobj(1)")
            Check(CULng(0) And CObj(0), CLng(0), "culng(0) and cobj(0)")

            ''''''

            'dec and single
            Check(CDec(1) And CSng(1), CLng(1), "cdec(1) and csng(1)")
            Check(CDec(83) And CSng(50), CLng(18), "cdec(83) and csng(50)")
            Check(CDec(1) And CSng(0), CLng(0), "cdec(1) and csng(0)")
            Check(CDec(1) And CSng(0.9), CLng(1), "cdec(1) and csng(0.9)")
            Check(CDec(83) And CSng(1.9), CLng(2), "cdec(83) and csng(1.9)")
            Check(CDec(1) And CSng(1.5), CLng(0), "cdec(1) and csng(1.5)")
            Check(CDec(1) And CSng(1.4999), CLng(1), "cdec(1) and csng(1.4999)")
            Check(CDec(1) And CSng(2.4999), CLng(0), "cdec(1) and csng(2.4999)")
            Check(CDec(83) And CSng(1.1), CLng(1), "cdec(83) and csng(1.1)")
            Check(CDec(0) And CSng(1), CLng(0), "cdec(0) and csng(1)")
            Check(CDec(0) And CSng(50), CLng(0), "cdec(0) and csng(50)")
            Check(CDec(0) And CSng(0), CLng(0), "cdec(0) and csng(0)")
            Check(CDec(0) And CSng(0.6), CLng(0), "cdec(0) and csng(0.6)")

            'dec and double
            Check(CDec(1) And CDbl(1), CLng(1), "cdec(1) and cdbl(1)")
            Check(CDec(83) And CDbl(50), CLng(18), "cdec(83) and cdbl(50)")
            Check(CDec(1) And CDbl(0), CLng(0), "cdec(1) and cdbl(0)")
            Check(CDec(1) And CDbl(0.9), CLng(1), "cdec(1) and cdbl(0.9)")
            Check(CDec(83) And CDbl(1.9), CLng(2), "cdec(83) and cdbl(1.9)")
            Check(CDec(1) And CDbl(1.5), CLng(0), "cdec(1) and cdbl(1.5)")
            Check(CDec(1) And CDbl(1.4999), CLng(1), "cdec(1) and cdbl(1.4999)")
            Check(CDec(1) And CDbl(2.4999), CLng(0), "cdec(1) and cdbl(2.4999)")
            Check(CDec(83) And CDbl(1.1), CLng(1), "cdec(83) and cdbl(1.1)")
            Check(CDec(0) And CDbl(1), CLng(0), "cdec(0) and cdbl(1)")
            Check(CDec(0) And CDbl(50), CLng(0), "cdec(0) and cdbl(50)")
            Check(CDec(0) And CDbl(0), CLng(0), "cdec(0) and cdbl(0)")
            Check(CDec(0) And CDbl(0.6), CLng(0), "cdec(0) and cdbl(0.6)")

            'dec and date
            'dec and char

            'dec and string
            Check(CDec(1) And "1", 1L, "cdec(1) and '1'")
            Check(CDec(1) And "0", 0L, "cdec(1) and '0'")
            Check(CDec(0) And "1", 0L, "cdec(0) and '1'")
            Check(CDec(0) And "0", 0L, "cdec(0) and '0'")

            'dec and object
            Check(CDec(1) And CObj(1), CLng(1), "cdec(1) and cobj(1)")
            Check(CDec(0) And CObj(0), CLng(0), "cdec(0) and cobj(0)")

            ''''''

            'single and double
            Check(CSng(1) And CDbl(1), CLng(1), "csng(1) and cdbl(1)")
            Check(CSng(83) And CDbl(50), CLng(18), "csng(83) and cdbl(50)")
            Check(CSng(1) And CDbl(0), CLng(0), "csng(1) and cdbl(0)")
            Check(CSng(1) And CDbl(0.9), CLng(1), "csng(1) and cdbl(0.9)")
            Check(CSng(83) And CDbl(1.9), CLng(2), "csng(83) and cdbl(1.9)")
            Check(CSng(1) And CDbl(1.5), CLng(0), "csng(1) and cdbl(1.5)")
            Check(CSng(1) And CDbl(1.4999), CLng(1), "csng(1) and cdbl(1.4999)")
            Check(CSng(1) And CDbl(2.4999), CLng(0), "csng(1) and cdbl(2.4999)")
            Check(CSng(83) And CDbl(1.1), CLng(1), "csng(83) and cdbl(1.1)")
            Check(CSng(0) And CDbl(1), CLng(0), "csng(0) and cdbl(1)")
            Check(CSng(0) And CDbl(50), CLng(0), "csng(0) and cdbl(50)")
            Check(CSng(0) And CDbl(0), CLng(0), "csng(0) and cdbl(0)")
            Check(CSng(0) And CDbl(0.6), CLng(0), "csng(0) and cdbl(0.6)")

            'single and date
            'single and char

            'single and string
            Check(CSng(1) And "1", 1L, "csng(1) and '1'")
            Check(CSng(1) And "0", 0L, "csng(1) and '0'")
            Check(CSng(0) And "1", 0L, "csng(0) and '1'")
            Check(CSng(0) And "0", 0L, "csng(0) and '0'")

            'single and object
            Check(CSng(1) And CObj(1), CLng(1), "csng(1) and cobj(1)")
            Check(CSng(0) And CObj(0), CLng(0), "csng(0) and cobj(0)")

            ''''''

            'double and date
            'double and char

            'double and string
            Check(CDbl(1) And "1", 1L, "cdbl(1) and '1'")
            Check(CDbl(1) And "0", 0L, "cdbl(1) and '0'")
            Check(CDbl(0) And "1", 0L, "cdbl(0) and '1'")
            Check(CDbl(0) And "0", 0L, "cdbl(0) and '0'")

            'double and object
            Check(CDbl(1) And CObj(1), CLng(1), "cdbl(1) and cobj(1)")
            Check(CDbl(0) And CObj(0), CLng(0), "cdbl(0) and cobj(0)")

            ''''''

            'date and char
            'date and string
            'date and object

            ''''''

            'char and string
            'char and object

            ''''''

            'string and string
            Try
                Check("true" And "true", True, "'true' and 'true")
                Fail("'true' and 'true': Expected InvalidCastException")
            Catch ex As InvalidCastException
                'do nothing
            End Try
            Try
                Check("true" And "false", False, "'true' and 'false")
                Fail("'true' and 'false': Expected InvalidCastException")
            Catch ex As InvalidCastException
                'do nothing
            End Try
            Try
                Check("false" And "true", False, "'false' and 'true'")
                Fail("'false' and 'true': Expected InvalidCastException")
            Catch ex As InvalidCastException
                'do nothing
            End Try
            Try
                Check("false" And "false", False, "'false' and 'false'")
                Fail("'false' and 'false': Expected InvalidCastException")
            Catch ex As InvalidCastException
                'do nothing
            End Try
            Check("1" And "1", 1L, "'1' and '1'")
            Check("1" And "0", 0L, "'1' and '0'")
            Check("0" And "1", 0L, "'0' and '1'")
            Check("0" And "0", 0L, "'0' and '0'")
            Check("7" And "89", 1L, "'7' and '89'")
            Check(Long.MaxValue.ToString() And Long.MinValue.ToString(), 0L, "Long.MaxValue.ToString () and Long.MinValue.ToString ()")


            'string and object
            Check("1" And CObj(1), CLng(1), "'1' and cobj(1)")
            Check("0" And CObj(0), CLng(0), "'0' and cobj(0)")

            ''''''

            'object and object
            Check(CObj(1) And CObj(1), CInt(1), "CObj(1) and cobj(1)")
            Check(CObj(0) And CObj(0), CInt(0), "CObj(0) and cobj(0)")
        End Sub

        Shared Sub CheckOr()
            Dim a As Boolean
            Dim b As Boolean

            ' boolean or boolean
            Check(True Or True, True, "true or true")
            Check(True Or False, True, "true or false")
            Check(False Or True, True, "false or true")
            Check(False Or False, False, "false or false")

            a = False
            b = False
            Check(a Or b, False, "a[false] or b[false]")
            a = True
            b = False
            Check(a Or b, True, "a[true] or b [false]")
            a = False
            b = True
            Check(a Or b, True, "a[false] or b[true]")
            a = True
            b = True
            Check(a Or b, True, "a[true] or b[true]")

            'boolean or sbyte
            Check(True Or CSByte(1), CSByte(-1), "true or csbyte(1)")
            Check(True Or CSByte(50), CSByte(-1), "true or csbyte(50)")
            Check(True Or CSByte(0), CSByte(-1), "true or csbyte(0)")
            Check(False Or CSByte(1), CSByte(1), "false or csbyte(1)")
            Check(False Or CSByte(50), CSByte(50), "false or csbyte(50)")
            Check(False Or CSByte(0), CSByte(0), "false or csbyte(0)")

            'boolean or byte
            Check(True Or CByte(1), CShort(-1), "true or cbyte(1)")
            Check(True Or CByte(50), CShort(-1), "true or cbyte(50)")
            Check(True Or CByte(0), CShort(-1), "true or cbyte(0)")
            Check(False Or CByte(1), CShort(1), "false or cbyte(1)")
            Check(False Or CByte(50), CShort(50), "false or cbyte(50)")
            Check(False Or CByte(0), CShort(0), "false or cbyte(0)")

            'boolean or short
            Check(True Or CShort(1), CShort(-1), "true or cshort(1)")
            Check(True Or CShort(50), CShort(-1), "true or cshort(50)")
            Check(True Or CShort(0), CShort(-1), "true or cshort(0)")
            Check(False Or CShort(1), CShort(1), "false or cshort(1)")
            Check(False Or CShort(50), CShort(50), "false or cshort(50)")
            Check(False Or CShort(0), CShort(0), "false or cshort(0)")

            'boolean or ushort
            Check(True Or CUShort(1), CInt(-1), "true or cushort(1)")
            Check(True Or CUShort(50), CInt(-1), "true or cushort(50)")
            Check(True Or CUShort(0), CInt(-1), "true or cushort(0)")
            Check(False Or CUShort(1), CInt(1), "false or cushort(1)")
            Check(False Or CUShort(50), CInt(50), "false or cushort(50)")
            Check(False Or CUShort(0), CInt(0), "false or cushort(0)")

            'boolean or int
            Check(True Or CInt(1), CInt(-1), "true or cint(1)")
            Check(True Or CInt(50), CInt(-1), "true or cint(50)")
            Check(True Or CInt(0), CInt(-1), "true or cint(0)")
            Check(False Or CInt(1), CInt(1), "false or cint(1)")
            Check(False Or CInt(50), CInt(50), "false or cint(50)")
            Check(False Or CInt(0), CInt(0), "false or cint(0)")

            'boolean or uint
            Check(True Or CUInt(1), CLng(-1), "true or cuint(1)")
            Check(True Or CUInt(50), CLng(-1), "true or cuint(50)")
            Check(True Or CUInt(0), CLng(-1), "true or cuint(0)")
            Check(False Or CUInt(1), CLng(1), "false or cuint(1)")
            Check(False Or CUInt(50), CLng(50), "false or cuint(50)")
            Check(False Or CUInt(0), CLng(0), "false or cuint(0)")

            'boolean or long
            Check(True Or CLng(1), CLng(-1), "true or clng(1)")
            Check(True Or CLng(50), CLng(-1), "true or clng(50)")
            Check(True Or CLng(0), CLng(-1), "true or clng(0)")
            Check(False Or CLng(1), CLng(1), "false or clng(1)")
            Check(False Or CLng(50), CLng(50), "false or clng(50)")
            Check(False Or CLng(0), CLng(0), "false or clng(0)")

            'boolean or ulong
            Check(True Or CULng(1), CLng(-1), "true or culng(1)")
            Check(True Or CULng(50), CLng(-1), "true or culng(50)")
            Check(True Or CULng(0), CLng(-1), "true or culng(0)")
            Check(False Or CULng(1), CLng(1), "false or culng(1)")
            Check(False Or CULng(50), CLng(50), "false or culng(50)")
            Check(False Or CULng(0), CLng(0), "false or culng(0)")

            'boolean or decimal
            Check(True Or CDec(1), CLng(-1), "true or cdec(1)")
            Check(True Or CDec(50), CLng(-1), "true or cdec(50)")
            Check(True Or CDec(0), CLng(-1), "true or cdec(0)")
            Check(False Or CDec(1), CLng(1), "false or cdec(1)")
            Check(False Or CDec(50), CLng(50), "false or cdec(50)")
            Check(False Or CDec(0), CLng(0), "false or cdec(0)")

            'boolean or single
            Check(True Or CSng(1), CLng(-1), "true or csng(1)")
            Check(True Or CSng(50), CLng(-1), "true or csng(50)")
            Check(True Or CSng(0), CLng(-1), "true or csng(0)")
            Check(True Or CSng(0.9), CLng(-1), "true or csng(0.9)")
            Check(True Or CSng(1.9), CLng(-1), "true or csng(1.9)")
            Check(True Or CSng(1.5), CLng(-1), "true or csng(1.5)")
            Check(True Or CSng(1.1), CLng(-1), "true or csng(1.1)")
            Check(False Or CSng(1), CLng(1), "false or csng(1)")
            Check(False Or CSng(50), CLng(50), "false or csng(50)")
            Check(False Or CSng(0), CLng(0), "false or csng(0)")
            Check(False Or CSng(0.6), CLng(1), "false or csng(0.6)")

            'boolean or double
            Check(True Or CDbl(1), CLng(-1), "true or cdbl(1)")
            Check(True Or CDbl(50), CLng(-1), "true or cdbl(50)")
            Check(True Or CDbl(0), CLng(-1), "true or cdbl(0)")
            Check(True Or CDbl(0.9), CLng(-1), "true or cdbl(0.9)")
            Check(True Or CDbl(1.9), CLng(-1), "true or cdbl(1.9)")
            Check(True Or CDbl(1.5), CLng(-1), "true or cdbl(1.5)")
            Check(True Or CDbl(1.4999), CLng(-1), "true or cdbl(1.4999)")
            Check(True Or CDbl(2.4999), CLng(-1), "true or cdbl(2.4999)")
            Check(True Or CDbl(1.1), CLng(-1), "true or cdbl(1.1)")
            Check(False Or CDbl(1), CLng(1), "false or cdbl(1)")
            Check(False Or CDbl(50), CLng(50), "false or cdbl(50)")
            Check(False Or CDbl(0), CLng(0), "false or cdbl(0)")
            Check(False Or CDbl(0.6), CLng(1), "false or cdbl(0.6)")

            'boolean or date
            'boolean or char

            'boolean or string
            Check(True Or "true", True, "true or 'true'")
            Check(True Or "false", True, "true or 'false'")
            Check(False Or "true", True, "false or 'true'")
            Check(False Or "false", False, "false or 'false'")
            Check(True Or "tRue", True, "true or 'tRue'")
            Check(True Or "falsE", True, "true or 'falsE'")
            Check(False Or "TRUE", True, "false or 'TRUE'")
            Check(False Or "false", False, "false or 'false'")
            Check(True Or "1", True, "true or '1'")
            Check(True Or "0", True, "true or '0'")
            Check(False Or "1", True, "false or '1'")
            Check(False Or "0", False, "false or '0'")

            'string or boolean
            Check("true" Or True, True, "'true' or true")
            Check("false" Or True, True, "'false' or true")
            Check("true" Or False, True, "'true' or false")
            Check("false" Or False, False, "'false' or false")
            Check("tRue" Or True, True, "'tRue' or true")
            Check("falsE" Or True, True, "'falsE' or true")
            Check("TRUE" Or False, True, "'TRUE' or and false")
            Check("false" Or False, False, "'false' or false")
            Check("1" Or True, True, "'1' or true")
            Check("0" Or True, True, "'0' or true")
            Check("1" Or False, True, "'1' or false")
            Check("0" Or False, False, "'0' or false")

            'boolean or object
            Check(True Or CObj(1), CInt(-1), "true or cobj(1)")
            Check(False Or CObj(0), CInt(0), "false or cobj(0)")

            ''''''

            'sbyte or byte
            Check(CSByte(0) Or CByte(1), CShort(1), "csbyte(0) or cbyte(1)")
            Check(CSByte(1) Or CByte(50), CShort(51), "csbyte(1) or cbyte(50)")
            Check(CSByte(83) Or CByte(0), CShort(83), "csbyte(83) or cbyte(0)")
            Check(CSByte(0) Or CByte(1), CShort(1), "csbyte(0) or cbyte(1)")
            Check(CSByte(1) Or CByte(50), CShort(51), "csbyte(1) or cbyte(50)")
            Check(CSByte(42) Or CByte(0), CShort(42), "csbyte(42) or cbyte(0)")

            'sbyte or short
            Check(CSByte(0) Or CShort(1), CShort(1), "csbyte(0) or cshort(1)")
            Check(CSByte(1) Or CShort(50), CShort(51), "csbyte(1) or cshort(50)")
            Check(CSByte(83) Or CShort(0), CShort(83), "csbyte(83) or cshort(0)")
            Check(CSByte(0) Or CShort(1), CShort(1), "csbyte(0) or cshort(1)")
            Check(CSByte(1) Or CShort(50), CShort(51), "csbyte(1) or cshort(50)")
            Check(CSByte(42) Or CShort(0), CShort(42), "csbyte(42) or cshort(0)")

            'sbyte or ushort
            Check(CSByte(0) Or CUShort(1), CInt(1), "csbyte(0) or cushort(1)")
            Check(CSByte(1) Or CUShort(50), CInt(51), "csbyte(1) or cushort(50)")
            Check(CSByte(83) Or CUShort(0), CInt(83), "csbyte(83) or cushort(0)")
            Check(CSByte(0) Or CUShort(1), CInt(1), "csbyte(0) or cushort(1)")
            Check(CSByte(1) Or CUShort(50), CInt(51), "csbyte(1) or cushort(50)")
            Check(CSByte(42) Or CUShort(0), CInt(42), "csbyte(42) or cushort(0)")

            'sbyte or int
            Check(CSByte(0) Or CInt(1), CInt(1), "csbyte(0) or cint(1)")
            Check(CSByte(1) Or CInt(50), CInt(51), "csbyte(1) or cint(50)")
            Check(CSByte(83) Or CInt(0), CInt(83), "csbyte(83) or cint(0)")
            Check(CSByte(0) Or CInt(1), CInt(1), "csbyte(0) or cint(1)")
            Check(CSByte(1) Or CInt(50), CInt(51), "csbyte(1) or cint(50)")
            Check(CSByte(42) Or CInt(0), CInt(42), "csbyte(42) or cint(0)")

            'sbyte or uint
            Check(CSByte(0) Or CUInt(1), CLng(1), "csbyte(0) or cuint(1)")
            Check(CSByte(1) Or CUInt(50), CLng(51), "csbyte(1) or cuint(50)")
            Check(CSByte(83) Or CUInt(0), CLng(83), "csbyte(83) or cuint(0)")
            Check(CSByte(0) Or CUInt(1), CLng(1), "csbyte(0) or cuint(1)")
            Check(CSByte(1) Or CUInt(50), CLng(51), "csbyte(1) or cuint(50)")
            Check(CSByte(42) Or CUInt(0), CLng(42), "csbyte(42) or cuint(0)")

            'sbyte or long
            Check(CSByte(0) Or CLng(1), CLng(1), "csbyte(0) or clng(1)")
            Check(CSByte(1) Or CLng(50), CLng(51), "csbyte(1) or clng(50)")
            Check(CSByte(83) Or CLng(0), CLng(83), "csbyte(83) or clng(0)")
            Check(CSByte(0) Or CLng(1), CLng(1), "csbyte(0) or clng(1)")
            Check(CSByte(1) Or CLng(50), CLng(51), "csbyte(1) or clng(50)")
            Check(CSByte(42) Or CLng(0), CLng(42), "csbyte(42) or clng(0)")

            'sbyte or ulong
            Check(CSByte(0) Or CULng(1), CLng(1), "csbyte(0) or culng(1)")
            Check(CSByte(1) Or CULng(50), CLng(51), "csbyte(1) or culng(50)")
            Check(CSByte(83) Or CULng(0), CLng(83), "csbyte(83) or culng(0)")
            Check(CSByte(0) Or CULng(1), CLng(1), "csbyte(0) or culng(1)")
            Check(CSByte(1) Or CULng(50), CLng(51), "csbyte(1) or culng(50)")
            Check(CSByte(42) Or CULng(0), CLng(42), "csbyte(42) or culng(0)")

            'sbyte or decimal
            Check(CSByte(0) Or CDec(1), CLng(1), "csbyte(0) or cdec(1)")
            Check(CSByte(1) Or CDec(50), CLng(51), "csbyte(1) or cdec(50)")
            Check(CSByte(83) Or CDec(0), CLng(83), "csbyte(83) or cdec(0)")
            Check(CSByte(0) Or CDec(1), CLng(1), "csbyte(0) or culng(1)")
            Check(CSByte(1) Or CDec(50), CLng(51), "csbyte(1) or cdec(50)")
            Check(CSByte(42) Or CDec(0), CLng(42), "csbyte(42) or v(0)")

            'sbyte or single
            Check(CSByte(1) Or CSng(1), CLng(1), "csbyte(1) or csng(1)")
            Check(CSByte(83) Or CSng(50), CLng(115), "csbyte(83) or csng(50)")
            Check(CSByte(1) Or CSng(0), CLng(1), "csbyte(1) or csng(0)")
            Check(CSByte(1) Or CSng(0.9), CLng(1), "csbyte(1) or csng(0.9)")
            Check(CSByte(83) Or CSng(1.9), CLng(83), "csbyte(83) or csng(1.9)")
            Check(CSByte(1) Or CSng(1.5), CLng(3), "csbyte(1) or csng(1.5)")
            Check(CSByte(1) Or CSng(1.4999), CLng(1), "csbyte(1) or csng(1.4999)")
            Check(CSByte(1) Or CSng(2.4999), CLng(3), "csbyte(1) or csng(2.4999)")
            Check(CSByte(83) Or CSng(1.1), CLng(83), "csbyte(83) or csng(1.1)")
            Check(CSByte(0) Or CSng(1), CLng(1), "csbyte(0) or csng(1)")
            Check(CSByte(0) Or CSng(50), CLng(50), "csbyte(0) or csng(50)")
            Check(CSByte(0) Or CSng(0), CLng(0), "csbyte(0) or csng(0)")
            Check(CSByte(0) Or CSng(0.6), CLng(1), "csbyte(0) or csng(0.6)")

            'sbyte or double
            Check(CSByte(1) Or CDbl(1), CLng(1), "csbyte(1) or cdbl(1)")
            Check(CSByte(83) Or CDbl(50), CLng(115), "csbyte(83) or cdbl(50)")
            Check(CSByte(1) Or CDbl(0), CLng(1), "csbyte(1) or cdbl(0)")
            Check(CSByte(1) Or CDbl(0.9), CLng(1), "csbyte(1) or cdbl(0.9)")
            Check(CSByte(83) Or CDbl(1.9), CLng(83), "csbyte(83) or cdbl(1.9)")
            Check(CSByte(1) Or CDbl(1.5), CLng(3), "csbyte(1) or cdbl(1.5)")
            Check(CSByte(1) Or CDbl(1.4999), CLng(1), "csbyte(1) or cdbl(1.4999)")
            Check(CSByte(1) Or CDbl(2.4999), CLng(3), "csbyte(1) or cdbl(2.4999)")
            Check(CSByte(83) Or CDbl(1.1), CLng(83), "csbyte(83) or cdbl(1.1)")
            Check(CSByte(0) Or CDbl(1), CLng(1), "csbyte(0) or cdbl(1)")
            Check(CSByte(0) Or CDbl(50), CLng(50), "csbyte(0) or cdbl(50)")
            Check(CSByte(0) Or CDbl(0), CLng(0), "csbyte(0) or cdbl(0)")
            Check(CSByte(0) Or CDbl(0.6), CLng(1), "csbyte(0) or cdbl(0.6)")

            'sbyte or date
            'sbyte or char

            'sbyte or string
            Check(CSByte(1) Or "1", 1L, "csbyte(1) or '1'")
            Check(CSByte(1) Or "0", 1L, "csbyte(1) or '0'")
            Check(CSByte(0) Or "1", 1L, "csbyte(0) or '1'")
            Check(CSByte(0) Or "0", 0L, "csbyte(0) or '0'")

            'sbyte or object
            Check(CSByte(1) Or CObj(1), CInt(1), "csbyte(1) or cobj(1)")
            Check(CSByte(0) Or CObj(0), CInt(0), "csbyte(0) or cobj(0)")

            ''''''

            'byte or short
            Check(CByte(0) Or CShort(1), CShort(1), "cbyte(0) or cshort(1)")
            Check(CByte(1) Or CShort(50), CShort(51), "cbyte(1) or cshort(50)")
            Check(CByte(83) Or CShort(0), CShort(83), "cbyte(83) or cshort(0)")
            Check(CByte(0) Or CShort(1), CShort(1), "cbyte(0) or cshort(1)")
            Check(CByte(1) Or CShort(50), CShort(51), "cbyte(1) or cshort(50)")
            Check(CByte(42) Or CShort(0), CShort(42), "cbyte(42) or cshort(0)")

            'byte or ushort
            Check(CByte(0) Or CUShort(1), CUShort(1), "cbyte(0) or cushort(1)")
            Check(CByte(1) Or CUShort(50), CUShort(51), "cbyte(1) or cushort(50)")
            Check(CByte(83) Or CUShort(0), CUShort(83), "cbyte(83) or cushort(0)")
            Check(CByte(0) Or CUShort(1), CUShort(1), "cbyte(0) or cushort(1)")
            Check(CByte(1) Or CUShort(50), CUShort(51), "cbyte(1) or cushort(50)")
            Check(CByte(42) Or CUShort(0), CUShort(42), "cbyte(42) or cushort(0)")

            'byte or int
            Check(CByte(0) Or CInt(1), CInt(1), "cbyte(0) or cint(1)")
            Check(CByte(1) Or CInt(50), CInt(51), "cbyte(1) or cint(50)")
            Check(CByte(83) Or CInt(0), CInt(83), "cbyte(83) or cint(0)")
            Check(CByte(0) Or CInt(1), CInt(1), "cbyte(0) or cint(1)")
            Check(CByte(1) Or CInt(50), CInt(51), "cbyte(1) or cint(50)")
            Check(CByte(42) Or CInt(0), CInt(42), "cbyte(42) or cint(0)")

            'byte or uint
            Check(CByte(0) Or CUInt(1), CUInt(1), "cbyte(0) or cuint(1)")
            Check(CByte(1) Or CUInt(50), CUInt(51), "cbyte(1) or cuint(50)")
            Check(CByte(83) Or CUInt(0), CUInt(83), "cbyte(83) or cuint(0)")
            Check(CByte(0) Or CUInt(1), CUInt(1), "cbyte(0) or cuint(1)")
            Check(CByte(1) Or CUInt(50), CUInt(51), "cbyte(1) or cuint(50)")
            Check(CByte(42) Or CUInt(0), CUInt(42), "cbyte(42) or cuint(0)")

            'byte or long
            Check(CByte(0) Or CLng(1), CLng(1), "cbyte(0) or clng(1)")
            Check(CByte(1) Or CLng(50), CLng(51), "cbyte(1) or clng(50)")
            Check(CByte(83) Or CLng(0), CLng(83), "cbyte(83) or clng(0)")
            Check(CByte(0) Or CLng(1), CLng(1), "cbyte(0) or clng(1)")
            Check(CByte(1) Or CLng(50), CLng(51), "cbyte(1) or clng(50)")
            Check(CByte(42) Or CLng(0), CLng(42), "cbyte(42) or clng(0)")

            'byte or ulong
            Check(CByte(0) Or CULng(1), CULng(1), "cbyte(0) or culng(1)")
            Check(CByte(1) Or CULng(50), CULng(51), "cbyte(1) or culng(50)")
            Check(CByte(83) Or CULng(0), CULng(83), "cbyte(83) or culng(0)")
            Check(CByte(0) Or CULng(1), CULng(1), "cbyte(0) or culng(1)")
            Check(CByte(1) Or CULng(50), CULng(51), "cbyte(1) or culng(50)")
            Check(CByte(42) Or CULng(0), CULng(42), "cbyte(42) or culng(0)")

            'byte or decimal
            Check(CByte(0) Or CDec(1), CLng(1), "cbyte(0) or cdec(1)")
            Check(CByte(1) Or CDec(50), CLng(51), "cbyte(1) or cdec(50)")
            Check(CByte(83) Or CDec(0), CLng(83), "cbyte(83) or cdec(0)")
            Check(CByte(0) Or CDec(1), CLng(1), "cbyte(0) or culng(1)")
            Check(CByte(1) Or CDec(50), CLng(51), "cbyte(1) or cdec(50)")
            Check(CByte(42) Or CDec(0), CLng(42), "cbyte(42) or v(0)")

            'byte or single
            Check(CByte(1) Or CSng(1), CLng(1), "cbyte(1) or csng(1)")
            Check(CByte(83) Or CSng(50), CLng(115), "cbyte(83) or csng(50)")
            Check(CByte(1) Or CSng(0), CLng(1), "cbyte(1) or csng(0)")
            Check(CByte(1) Or CSng(0.9), CLng(1), "cbyte(1) or csng(0.9)")
            Check(CByte(83) Or CSng(1.9), CLng(83), "cbyte(83) or csng(1.9)")
            Check(CByte(1) Or CSng(1.5), CLng(3), "cbyte(1) or csng(1.5)")
            Check(CByte(1) Or CSng(1.4999), CLng(1), "cbyte(1) or csng(1.4999)")
            Check(CByte(1) Or CSng(2.4999), CLng(3), "cbyte(1) or csng(2.4999)")
            Check(CByte(83) Or CSng(1.1), CLng(83), "cbyte(83) or csng(1.1)")
            Check(CByte(0) Or CSng(1), CLng(1), "cbyte(0) or csng(1)")
            Check(CByte(0) Or CSng(50), CLng(50), "cbyte(0) or csng(50)")
            Check(CByte(0) Or CSng(0), CLng(0), "cbyte(0) or csng(0)")
            Check(CByte(0) Or CSng(0.6), CLng(1), "cbyte(0) or csng(0.6)")

            'byte or double
            Check(CByte(1) Or CDbl(1), CLng(1), "cbyte(1) or cdbl(1)")
            Check(CByte(83) Or CDbl(50), CLng(115), "cbyte(83) or cdbl(50)")
            Check(CByte(1) Or CDbl(0), CLng(1), "cbyte(1) or cdbl(0)")
            Check(CByte(1) Or CDbl(0.9), CLng(1), "cbyte(1) or cdbl(0.9)")
            Check(CByte(83) Or CDbl(1.9), CLng(83), "cbyte(83) or cdbl(1.9)")
            Check(CByte(1) Or CDbl(1.5), CLng(3), "cbyte(1) or cdbl(1.5)")
            Check(CByte(1) Or CDbl(1.4999), CLng(1), "cbyte(1) or cdbl(1.4999)")
            Check(CByte(1) Or CDbl(2.4999), CLng(3), "cbyte(1) or cdbl(2.4999)")
            Check(CByte(83) Or CDbl(1.1), CLng(83), "cbyte(83) or cdbl(1.1)")
            Check(CByte(0) Or CDbl(1), CLng(1), "cbyte(0) or cdbl(1)")
            Check(CByte(0) Or CDbl(50), CLng(50), "cbyte(0) or cdbl(50)")
            Check(CByte(0) Or CDbl(0), CLng(0), "cbyte(0) or cdbl(0)")
            Check(CByte(0) Or CDbl(0.6), CLng(1), "cbyte(0) or cdbl(0.6)")

            'byte or date
            'byte or char

            'byte or string
            Check(CByte(1) Or "1", 1L, "cbyte(1) or '1'")
            Check(CByte(1) Or "0", 1L, "cbyte(1) or '0'")
            Check(CByte(0) Or "1", 1L, "cbyte(0) or '1'")
            Check(CByte(0) Or "0", 0L, "cbyte(0) or '0'")

            'byte or object
            Check(CByte(1) Or CObj(1), CInt(1), "cbyte(1) or cobj(1)")
            Check(CByte(0) Or CObj(0), CInt(0), "cbyte(0) or cobj(0)")

            ''''''

            'short or ushort
            Check(CShort(0) Or CUShort(1), CInt(1), "cshort(0) or cushort(1)")
            Check(CShort(1) Or CUShort(50), CInt(51), "cshort(1) or cushort(50)")
            Check(CShort(83) Or CUShort(0), CInt(83), "cshort(83) or cushort(0)")
            Check(CShort(0) Or CUShort(1), CInt(1), "cshort(0) or cushort(1)")
            Check(CShort(1) Or CUShort(50), CInt(51), "cshort(1) or cushort(50)")
            Check(CShort(42) Or CUShort(0), CInt(42), "cshort(42) or cushort(0)")

            'short or int
            Check(CShort(0) Or CInt(1), CInt(1), "cshort(0) or cint(1)")
            Check(CShort(1) Or CInt(50), CInt(51), "cshort(1) or cint(50)")
            Check(CShort(83) Or CInt(0), CInt(83), "cshort(83) or cint(0)")
            Check(CShort(0) Or CInt(1), CInt(1), "cshort(0) or cint(1)")
            Check(CShort(1) Or CInt(50), CInt(51), "cshort(1) or cint(50)")
            Check(CShort(42) Or CInt(0), CInt(42), "cshort(42) or cint(0)")

            'short or uint
            Check(CShort(0) Or CUInt(1), CLng(1), "cshort(0) or cuint(1)")
            Check(CShort(1) Or CUInt(50), CLng(51), "cshort(1) or cuint(50)")
            Check(CShort(83) Or CUInt(0), CLng(83), "cshort(83) or cuint(0)")
            Check(CShort(0) Or CUInt(1), CLng(1), "cshort(0) or cuint(1)")
            Check(CShort(1) Or CUInt(50), CLng(51), "cshort(1) or cuint(50)")
            Check(CShort(42) Or CUInt(0), CLng(42), "cshort(42) or cuint(0)")

            'short or long
            Check(CShort(0) Or CLng(1), CLng(1), "cshort(0) or clng(1)")
            Check(CShort(1) Or CLng(50), CLng(51), "cshort(1) or clng(50)")
            Check(CShort(83) Or CLng(0), CLng(83), "cshort(83) or clng(0)")
            Check(CShort(0) Or CLng(1), CLng(1), "cshort(0) or clng(1)")
            Check(CShort(1) Or CLng(50), CLng(51), "cshort(1) or clng(50)")
            Check(CShort(42) Or CLng(0), CLng(42), "cshort(42) or clng(0)")

            'short or ulong
            Check(CShort(0) Or CULng(1), CLng(1), "cshort(0) or culng(1)")
            Check(CShort(1) Or CULng(50), CLng(51), "cshort(1) or culng(50)")
            Check(CShort(83) Or CULng(0), CLng(83), "cshort(83) or culng(0)")
            Check(CShort(0) Or CULng(1), CLng(1), "cshort(0) or culng(1)")
            Check(CShort(1) Or CULng(50), CLng(51), "cshort(1) or culng(50)")
            Check(CShort(42) Or CULng(0), CLng(42), "cshort(42) or culng(0)")

            'short or decimal
            Check(CShort(0) Or CDec(1), CLng(1), "cshort(0) or cdec(1)")
            Check(CShort(1) Or CDec(50), CLng(51), "cshort(1) or cdec(50)")
            Check(CShort(83) Or CDec(0), CLng(83), "cshort(83) or cdec(0)")
            Check(CShort(0) Or CDec(1), CLng(1), "cshort(0) or culng(1)")
            Check(CShort(1) Or CDec(50), CLng(51), "cshort(1) or cdec(50)")
            Check(CShort(42) Or CDec(0), CLng(42), "cshort(42) or v(0)")

            'short or single
            Check(CShort(1) Or CSng(1), CLng(1), "cshort(1) or csng(1)")
            Check(CShort(83) Or CSng(50), CLng(115), "cshort(83) or csng(50)")
            Check(CShort(1) Or CSng(0), CLng(1), "cshort(1) or csng(0)")
            Check(CShort(1) Or CSng(0.9), CLng(1), "cshort(1) or csng(0.9)")
            Check(CShort(83) Or CSng(1.9), CLng(83), "cshort(83) or csng(1.9)")
            Check(CShort(1) Or CSng(1.5), CLng(3), "cshort(1) or csng(1.5)")
            Check(CShort(1) Or CSng(1.4999), CLng(1), "cshort(1) or csng(1.4999)")
            Check(CShort(1) Or CSng(2.4999), CLng(3), "cshort(1) or csng(2.4999)")
            Check(CShort(83) Or CSng(1.1), CLng(83), "cshort(83) or csng(1.1)")
            Check(CShort(0) Or CSng(1), CLng(1), "cshort(0) or csng(1)")
            Check(CShort(0) Or CSng(50), CLng(50), "cshort(0) or csng(50)")
            Check(CShort(0) Or CSng(0), CLng(0), "cshort(0) or csng(0)")
            Check(CShort(0) Or CSng(0.6), CLng(1), "cshort(0) or csng(0.6)")

            'short or double
            Check(CShort(1) Or CDbl(1), CLng(1), "cshort(1) or cdbl(1)")
            Check(CShort(83) Or CDbl(50), CLng(115), "cshort(83) or cdbl(50)")
            Check(CShort(1) Or CDbl(0), CLng(1), "cshort(1) or cdbl(0)")
            Check(CShort(1) Or CDbl(0.9), CLng(1), "cshort(1) or cdbl(0.9)")
            Check(CShort(83) Or CDbl(1.9), CLng(83), "cshort(83) or cdbl(1.9)")
            Check(CShort(1) Or CDbl(1.5), CLng(3), "cshort(1) or cdbl(1.5)")
            Check(CShort(1) Or CDbl(1.4999), CLng(1), "cshort(1) or cdbl(1.4999)")
            Check(CShort(1) Or CDbl(2.4999), CLng(3), "cshort(1) or cdbl(2.4999)")
            Check(CShort(83) Or CDbl(1.1), CLng(83), "cshort(83) or cdbl(1.1)")
            Check(CShort(0) Or CDbl(1), CLng(1), "cshort(0) or cdbl(1)")
            Check(CShort(0) Or CDbl(50), CLng(50), "cshort(0) or cdbl(50)")
            Check(CShort(0) Or CDbl(0), CLng(0), "cshort(0) or cdbl(0)")
            Check(CShort(0) Or CDbl(0.6), CLng(1), "cshort(0) or cdbl(0.6)")

            'short or date
            'short or char

            'short or string
            Check(CShort(1) Or "1", 1L, "cshort(1) or '1'")
            Check(CShort(1) Or "0", 1L, "cshort(1) or '0'")
            Check(CShort(0) Or "1", 1L, "cshort(0) or '1'")
            Check(CShort(0) Or "0", 0L, "cshort(0) or '0'")

            'short or object
            Check(CShort(1) Or CObj(1), CInt(1), "cshort(1) or cobj(1)")
            Check(CShort(0) Or CObj(0), CInt(0), "cshort(0) or cobj(0)")

            ''''''

            'ushort or int
            Check(CUShort(0) Or CInt(1), CInt(1), "cushort(0) or cint(1)")
            Check(CUShort(1) Or CInt(50), CInt(51), "cushort(1) or cint(50)")
            Check(CUShort(83) Or CInt(0), CInt(83), "cushort(83) or cint(0)")
            Check(CUShort(0) Or CInt(1), CInt(1), "cushort(0) or cint(1)")
            Check(CUShort(1) Or CInt(50), CInt(51), "cushort(1) or cint(50)")
            Check(CUShort(42) Or CInt(0), CInt(42), "cushort(42) or cint(0)")

            'ushort or uint
            Check(CUShort(0) Or CUInt(1), CUInt(1), "cushort(0) or cuint(1)")
            Check(CUShort(1) Or CUInt(50), CUInt(51), "cushort(1) or cuint(50)")
            Check(CUShort(83) Or CUInt(0), CUInt(83), "cushort(83) or cuint(0)")
            Check(CUShort(0) Or CUInt(1), CUInt(1), "cushort(0) or cuint(1)")
            Check(CUShort(1) Or CUInt(50), CUInt(51), "cushort(1) or cuint(50)")
            Check(CUShort(42) Or CUInt(0), CUInt(42), "cushort(42) or cuint(0)")

            'ushort or long
            Check(CUShort(0) Or CLng(1), CLng(1), "cushort(0) or clng(1)")
            Check(CUShort(1) Or CLng(50), CLng(51), "cushort(1) or clng(50)")
            Check(CUShort(83) Or CLng(0), CLng(83), "cushort(83) or clng(0)")
            Check(CUShort(0) Or CLng(1), CLng(1), "cushort(0) or clng(1)")
            Check(CUShort(1) Or CLng(50), CLng(51), "cushort(1) or clng(50)")
            Check(CUShort(42) Or CLng(0), CLng(42), "cushort(42) or clng(0)")

            'ushort or ulong
            Check(CUShort(0) Or CULng(1), CULng(1), "cushort(0) or culng(1)")
            Check(CUShort(1) Or CULng(50), CULng(51), "cushort(1) or culng(50)")
            Check(CUShort(83) Or CULng(0), CULng(83), "cushort(83) or culng(0)")
            Check(CUShort(0) Or CULng(1), CULng(1), "cushort(0) or culng(1)")
            Check(CUShort(1) Or CULng(50), CULng(51), "cushort(1) or culng(50)")
            Check(CUShort(42) Or CULng(0), CULng(42), "cushort(42) or culng(0)")

            'ushort or decimal
            Check(CUShort(0) Or CDec(1), CLng(1), "cushort(0) or cdec(1)")
            Check(CUShort(1) Or CDec(50), CLng(51), "cushort(1) or cdec(50)")
            Check(CUShort(83) Or CDec(0), CLng(83), "cushort(83) or cdec(0)")
            Check(CUShort(0) Or CDec(1), CLng(1), "cushort(0) or culng(1)")
            Check(CUShort(1) Or CDec(50), CLng(51), "cushort(1) or cdec(50)")
            Check(CUShort(42) Or CDec(0), CLng(42), "cushort(42) or v(0)")

            'ushort or single
            Check(CUShort(1) Or CSng(1), CLng(1), "cushort(1) or csng(1)")
            Check(CUShort(83) Or CSng(50), CLng(115), "cushort(83) or csng(50)")
            Check(CUShort(1) Or CSng(0), CLng(1), "cushort(1) or csng(0)")
            Check(CUShort(1) Or CSng(0.9), CLng(1), "cushort(1) or csng(0.9)")
            Check(CUShort(83) Or CSng(1.9), CLng(83), "cushort(83) or csng(1.9)")
            Check(CUShort(1) Or CSng(1.5), CLng(3), "cushort(1) or csng(1.5)")
            Check(CUShort(1) Or CSng(1.4999), CLng(1), "cushort(1) or csng(1.4999)")
            Check(CUShort(1) Or CSng(2.4999), CLng(3), "cushort(1) or csng(2.4999)")
            Check(CUShort(83) Or CSng(1.1), CLng(83), "cushort(83) or csng(1.1)")
            Check(CUShort(0) Or CSng(1), CLng(1), "cushort(0) or csng(1)")
            Check(CUShort(0) Or CSng(50), CLng(50), "cushort(0) or csng(50)")
            Check(CUShort(0) Or CSng(0), CLng(0), "cushort(0) or csng(0)")
            Check(CUShort(0) Or CSng(0.6), CLng(1), "cushort(0) or csng(0.6)")

            'ushort or double
            Check(CUShort(1) Or CDbl(1), CLng(1), "cushort(1) or cdbl(1)")
            Check(CUShort(83) Or CDbl(50), CLng(115), "cushort(83) or cdbl(50)")
            Check(CUShort(1) Or CDbl(0), CLng(1), "cushort(1) or cdbl(0)")
            Check(CUShort(1) Or CDbl(0.9), CLng(1), "cushort(1) or cdbl(0.9)")
            Check(CUShort(83) Or CDbl(1.9), CLng(83), "cushort(83) or cdbl(1.9)")
            Check(CUShort(1) Or CDbl(1.5), CLng(3), "cushort(1) or cdbl(1.5)")
            Check(CUShort(1) Or CDbl(1.4999), CLng(1), "cushort(1) or cdbl(1.4999)")
            Check(CUShort(1) Or CDbl(2.4999), CLng(3), "cushort(1) or cdbl(2.4999)")
            Check(CUShort(83) Or CDbl(1.1), CLng(83), "cushort(83) or cdbl(1.1)")
            Check(CUShort(0) Or CDbl(1), CLng(1), "cushort(0) or cdbl(1)")
            Check(CUShort(0) Or CDbl(50), CLng(50), "cushort(0) or cdbl(50)")
            Check(CUShort(0) Or CDbl(0), CLng(0), "cushort(0) or cdbl(0)")
            Check(CUShort(0) Or CDbl(0.6), CLng(1), "cushort(0) or cdbl(0.6)")

            'ushort or date
            'ushort or char

            'ushort or string
            Check(CUShort(1) Or "1", 1L, "cushort(1) or '1'")
            Check(CUShort(1) Or "0", 1L, "cushort(1) or '0'")
            Check(CUShort(0) Or "1", 1L, "cushort(0) or '1'")
            Check(CUShort(0) Or "0", 0L, "cushort(0) or '0'")

            'ushort or object
            Check(CUShort(1) Or CObj(1), CInt(1), "cushort(1) or cobj(1)")
            Check(CUShort(0) Or CObj(0), CInt(0), "cushort(0) or cobj(0)")

            ''''''

            'int or uint
            Check(CInt(0) Or CUInt(1), CLng(1), "cint(0) or cuint(1)")
            Check(CInt(1) Or CUInt(50), CLng(51), "cint(1) or cuint(50)")
            Check(CInt(83) Or CUInt(0), CLng(83), "cint(83) or cuint(0)")
            Check(CInt(0) Or CUInt(1), CLng(1), "cint(0) or cuint(1)")
            Check(CInt(1) Or CUInt(50), CLng(51), "cint(1) or cuint(50)")
            Check(CInt(42) Or CUInt(0), CLng(42), "cint(42) or cuint(0)")

            'int or long
            Check(CInt(0) Or CLng(1), CLng(1), "cint(0) or clng(1)")
            Check(CInt(1) Or CLng(50), CLng(51), "cint(1) or clng(50)")
            Check(CInt(83) Or CLng(0), CLng(83), "cint(83) or clng(0)")
            Check(CInt(0) Or CLng(1), CLng(1), "cint(0) or clng(1)")
            Check(CInt(1) Or CLng(50), CLng(51), "cint(1) or clng(50)")
            Check(CInt(42) Or CLng(0), CLng(42), "cint(42) or clng(0)")

            'int or ulong
            Check(CInt(0) Or CULng(1), CLng(1), "cint(0) or culng(1)")
            Check(CInt(1) Or CULng(50), CLng(51), "cint(1) or culng(50)")
            Check(CInt(83) Or CULng(0), CLng(83), "cint(83) or culng(0)")
            Check(CInt(0) Or CULng(1), CLng(1), "cint(0) or culng(1)")
            Check(CInt(1) Or CULng(50), CLng(51), "cint(1) or culng(50)")
            Check(CInt(42) Or CULng(0), CLng(42), "cint(42) or culng(0)")

            'int or decimal
            Check(CInt(0) Or CDec(1), CLng(1), "cint(0) or cdec(1)")
            Check(CInt(1) Or CDec(50), CLng(51), "cint(1) or cdec(50)")
            Check(CInt(83) Or CDec(0), CLng(83), "cint(83) or cdec(0)")
            Check(CInt(0) Or CDec(1), CLng(1), "cint(0) or culng(1)")
            Check(CInt(1) Or CDec(50), CLng(51), "cint(1) or cdec(50)")
            Check(CInt(42) Or CDec(0), CLng(42), "cint(42) or v(0)")

            'int or single
            Check(CInt(1) Or CSng(1), CLng(1), "cint(1) or csng(1)")
            Check(CInt(83) Or CSng(50), CLng(115), "cint(83) or csng(50)")
            Check(CInt(1) Or CSng(0), CLng(1), "cint(1) or csng(0)")
            Check(CInt(1) Or CSng(0.9), CLng(1), "cint(1) or csng(0.9)")
            Check(CInt(83) Or CSng(1.9), CLng(83), "cint(83) or csng(1.9)")
            Check(CInt(1) Or CSng(1.5), CLng(3), "cint(1) or csng(1.5)")
            Check(CInt(1) Or CSng(1.4999), CLng(1), "cint(1) or csng(1.4999)")
            Check(CInt(1) Or CSng(2.4999), CLng(3), "cint(1) or csng(2.4999)")
            Check(CInt(83) Or CSng(1.1), CLng(83), "cint(83) or csng(1.1)")
            Check(CInt(0) Or CSng(1), CLng(1), "cint(0) or csng(1)")
            Check(CInt(0) Or CSng(50), CLng(50), "cint(0) or csng(50)")
            Check(CInt(0) Or CSng(0), CLng(0), "cint(0) or csng(0)")
            Check(CInt(0) Or CSng(0.6), CLng(1), "cint(0) or csng(0.6)")

            'int or double
            Check(CInt(1) Or CDbl(1), CLng(1), "cint(1) or cdbl(1)")
            Check(CInt(83) Or CDbl(50), CLng(115), "cint(83) or cdbl(50)")
            Check(CInt(1) Or CDbl(0), CLng(1), "cint(1) or cdbl(0)")
            Check(CInt(1) Or CDbl(0.9), CLng(1), "cint(1) or cdbl(0.9)")
            Check(CInt(83) Or CDbl(1.9), CLng(83), "cint(83) or cdbl(1.9)")
            Check(CInt(1) Or CDbl(1.5), CLng(3), "cint(1) or cdbl(1.5)")
            Check(CInt(1) Or CDbl(1.4999), CLng(1), "cint(1) or cdbl(1.4999)")
            Check(CInt(1) Or CDbl(2.4999), CLng(3), "cint(1) or cdbl(2.4999)")
            Check(CInt(83) Or CDbl(1.1), CLng(83), "cint(83) or cdbl(1.1)")
            Check(CInt(0) Or CDbl(1), CLng(1), "cint(0) or cdbl(1)")
            Check(CInt(0) Or CDbl(50), CLng(50), "cint(0) or cdbl(50)")
            Check(CInt(0) Or CDbl(0), CLng(0), "cint(0) or cdbl(0)")
            Check(CInt(0) Or CDbl(0.6), CLng(1), "cint(0) or cdbl(0.6)")

            'int or date
            'int or char

            'int or string
            Check(CInt(1) Or "1", 1L, "cint(1) or '1'")
            Check(CInt(1) Or "0", 1L, "cint(1) or '0'")
            Check(CInt(0) Or "1", 1L, "cint(0) or '1'")
            Check(CInt(0) Or "0", 0L, "cint(0) or '0'")

            'int or object
            Check(CInt(1) Or CObj(1), CInt(1), "cint(1) or cobj(1)")
            Check(CInt(0) Or CObj(0), CInt(0), "cint(0) or cobj(0)")

            ''''''

            'uint or long
            Check(CUInt(0) Or CLng(1), CLng(1), "cuint(0) or clng(1)")
            Check(CUInt(1) Or CLng(50), CLng(51), "cuint(1) or clng(50)")
            Check(CUInt(83) Or CLng(0), CLng(83), "cuint(83) or clng(0)")
            Check(CUInt(0) Or CLng(1), CLng(1), "cuint(0) or clng(1)")
            Check(CUInt(1) Or CLng(50), CLng(51), "cuint(1) or clng(50)")
            Check(CUInt(42) Or CLng(0), CLng(42), "cuint(42) or clng(0)")

            'uint or ulong
            Check(CUInt(0) Or CULng(1), CULng(1), "cuint(0) or culng(1)")
            Check(CUInt(1) Or CULng(50), CULng(51), "cuint(1) or culng(50)")
            Check(CUInt(83) Or CULng(0), CULng(83), "cuint(83) or culng(0)")
            Check(CUInt(0) Or CULng(1), CULng(1), "cuint(0) or culng(1)")
            Check(CUInt(1) Or CULng(50), CULng(51), "cuint(1) or culng(50)")
            Check(CUInt(42) Or CULng(0), CULng(42), "cuint(42) or culng(0)")

            'uint or decimal
            Check(CUInt(0) Or CDec(1), CLng(1), "cuint(0) or cdec(1)")
            Check(CUInt(1) Or CDec(50), CLng(51), "cuint(1) or cdec(50)")
            Check(CUInt(83) Or CDec(0), CLng(83), "cuint(83) or cdec(0)")
            Check(CUInt(0) Or CDec(1), CLng(1), "cuint(0) or culng(1)")
            Check(CUInt(1) Or CDec(50), CLng(51), "cuint(1) or cdec(50)")
            Check(CUInt(42) Or CDec(0), CLng(42), "cuint(42) or v(0)")

            'uint or single
            Check(CUInt(1) Or CSng(1), CLng(1), "cuint(1) or csng(1)")
            Check(CUInt(83) Or CSng(50), CLng(115), "cuint(83) or csng(50)")
            Check(CUInt(1) Or CSng(0), CLng(1), "cuint(1) or csng(0)")
            Check(CUInt(1) Or CSng(0.9), CLng(1), "cuint(1) or csng(0.9)")
            Check(CUInt(83) Or CSng(1.9), CLng(83), "cuint(83) or csng(1.9)")
            Check(CUInt(1) Or CSng(1.5), CLng(3), "cuint(1) or csng(1.5)")
            Check(CUInt(1) Or CSng(1.4999), CLng(1), "cuint(1) or csng(1.4999)")
            Check(CUInt(1) Or CSng(2.4999), CLng(3), "cuint(1) or csng(2.4999)")
            Check(CUInt(83) Or CSng(1.1), CLng(83), "cuint(83) or csng(1.1)")
            Check(CUInt(0) Or CSng(1), CLng(1), "cuint(0) or csng(1)")
            Check(CUInt(0) Or CSng(50), CLng(50), "cuint(0) or csng(50)")
            Check(CUInt(0) Or CSng(0), CLng(0), "cuint(0) or csng(0)")
            Check(CUInt(0) Or CSng(0.6), CLng(1), "cuint(0) or csng(0.6)")

            'uint or double
            Check(CUInt(1) Or CDbl(1), CLng(1), "cuint(1) or cdbl(1)")
            Check(CUInt(83) Or CDbl(50), CLng(115), "cuint(83) or cdbl(50)")
            Check(CUInt(1) Or CDbl(0), CLng(1), "cuint(1) or cdbl(0)")
            Check(CUInt(1) Or CDbl(0.9), CLng(1), "cuint(1) or cdbl(0.9)")
            Check(CUInt(83) Or CDbl(1.9), CLng(83), "cuint(83) or cdbl(1.9)")
            Check(CUInt(1) Or CDbl(1.5), CLng(3), "cuint(1) or cdbl(1.5)")
            Check(CUInt(1) Or CDbl(1.4999), CLng(1), "cuint(1) or cdbl(1.4999)")
            Check(CUInt(1) Or CDbl(2.4999), CLng(3), "cuint(1) or cdbl(2.4999)")
            Check(CUInt(83) Or CDbl(1.1), CLng(83), "cuint(83) or cdbl(1.1)")
            Check(CUInt(0) Or CDbl(1), CLng(1), "cuint(0) or cdbl(1)")
            Check(CUInt(0) Or CDbl(50), CLng(50), "cuint(0) or cdbl(50)")
            Check(CUInt(0) Or CDbl(0), CLng(0), "cuint(0) or cdbl(0)")
            Check(CUInt(0) Or CDbl(0.6), CLng(1), "cuint(0) or cdbl(0.6)")

            'uint or date
            'uint or char

            'uint or string
            Check(CUInt(1) Or "1", 1L, "cuint(1) or '1'")
            Check(CUInt(1) Or "0", 1L, "cuint(1) or '0'")
            Check(CUInt(0) Or "1", 1L, "cuint(0) or '1'")
            Check(CUInt(0) Or "0", 0L, "cuint(0) or '0'")

            'uint or object
            Check(CUInt(1) Or CObj(1), CLng(1), "cuint(1) or cobj(1)")
            Check(CUInt(0) Or CObj(0), CLng(0), "cuint(0) or cobj(0)")

            ''''''

            'long or ulong
            Check(CLng(0) Or CULng(1), CLng(1), "clng(0) or culng(1)")
            Check(CLng(1) Or CULng(50), CLng(51), "clng(1) or culng(50)")
            Check(CLng(83) Or CULng(0), CLng(83), "clng(83) or culng(0)")
            Check(CLng(0) Or CULng(1), CLng(1), "clng(0) or culng(1)")
            Check(CLng(1) Or CULng(50), CLng(51), "clng(1) or culng(50)")
            Check(CLng(42) Or CULng(0), CLng(42), "clng(42) or culng(0)")

            'long or decimal
            Check(CLng(0) Or CDec(1), CLng(1), "clng(0) or cdec(1)")
            Check(CLng(1) Or CDec(50), CLng(51), "clng(1) or cdec(50)")
            Check(CLng(83) Or CDec(0), CLng(83), "clng(83) or cdec(0)")
            Check(CLng(0) Or CDec(1), CLng(1), "clng(0) or culng(1)")
            Check(CLng(1) Or CDec(50), CLng(51), "clng(1) or cdec(50)")
            Check(CLng(42) Or CDec(0), CLng(42), "clng(42) or v(0)")

            'long or single
            Check(CLng(1) Or CSng(1), CLng(1), "clng(1) or csng(1)")
            Check(CLng(83) Or CSng(50), CLng(115), "clng(83) or csng(50)")
            Check(CLng(1) Or CSng(0), CLng(1), "clng(1) or csng(0)")
            Check(CLng(1) Or CSng(0.9), CLng(1), "clng(1) or csng(0.9)")
            Check(CLng(83) Or CSng(1.9), CLng(83), "clng(83) or csng(1.9)")
            Check(CLng(1) Or CSng(1.5), CLng(3), "clng(1) or csng(1.5)")
            Check(CLng(1) Or CSng(1.4999), CLng(1), "clng(1) or csng(1.4999)")
            Check(CLng(1) Or CSng(2.4999), CLng(3), "clng(1) or csng(2.4999)")
            Check(CLng(83) Or CSng(1.1), CLng(83), "clng(83) or csng(1.1)")
            Check(CLng(0) Or CSng(1), CLng(1), "clng(0) or csng(1)")
            Check(CLng(0) Or CSng(50), CLng(50), "clng(0) or csng(50)")
            Check(CLng(0) Or CSng(0), CLng(0), "clng(0) or csng(0)")
            Check(CLng(0) Or CSng(0.6), CLng(1), "clng(0) or csng(0.6)")

            'long or double
            Check(CLng(1) Or CDbl(1), CLng(1), "clng(1) or cdbl(1)")
            Check(CLng(83) Or CDbl(50), CLng(115), "clng(83) or cdbl(50)")
            Check(CLng(1) Or CDbl(0), CLng(1), "clng(1) or cdbl(0)")
            Check(CLng(1) Or CDbl(0.9), CLng(1), "clng(1) or cdbl(0.9)")
            Check(CLng(83) Or CDbl(1.9), CLng(83), "clng(83) or cdbl(1.9)")
            Check(CLng(1) Or CDbl(1.5), CLng(3), "clng(1) or cdbl(1.5)")
            Check(CLng(1) Or CDbl(1.4999), CLng(1), "clng(1) or cdbl(1.4999)")
            Check(CLng(1) Or CDbl(2.4999), CLng(3), "clng(1) or cdbl(2.4999)")
            Check(CLng(83) Or CDbl(1.1), CLng(83), "clng(83) or cdbl(1.1)")
            Check(CLng(0) Or CDbl(1), CLng(1), "clng(0) or cdbl(1)")
            Check(CLng(0) Or CDbl(50), CLng(50), "clng(0) or cdbl(50)")
            Check(CLng(0) Or CDbl(0), CLng(0), "clng(0) or cdbl(0)")
            Check(CLng(0) Or CDbl(0.6), CLng(1), "clng(0) or cdbl(0.6)")

            'long or date
            'long or char

            'long or string
            Check(CLng(1) Or "1", 1L, "clng(1) or '1'")
            Check(CLng(1) Or "0", 1L, "clng(1) or '0'")
            Check(CLng(0) Or "1", 1L, "clng(0) or '1'")
            Check(CLng(0) Or "0", 0L, "clng(0) or '0'")

            'long or object
            Check(CLng(1) Or CObj(1), CLng(1), "clng(1) or cobj(1)")
            Check(CLng(0) Or CObj(0), CLng(0), "clng(0) or cobj(0)")

            ''''''

            'ulong or decimal
            Check(CULng(0) Or CDec(1), CLng(1), "culng(0) or cdec(1)")
            Check(CULng(1) Or CDec(50), CLng(51), "culng(1) or cdec(50)")
            Check(CULng(83) Or CDec(0), CLng(83), "culng(83) or cdec(0)")
            Check(CULng(0) Or CDec(1), CLng(1), "culng(0) or culng(1)")
            Check(CULng(1) Or CDec(50), CLng(51), "culng(1) or cdec(50)")
            Check(CULng(42) Or CDec(0), CLng(42), "culng(42) or v(0)")

            'ulong or single
            Check(CULng(1) Or CSng(1), CLng(1), "culng(1) or csng(1)")
            Check(CULng(83) Or CSng(50), CLng(115), "culng(83) or csng(50)")
            Check(CULng(1) Or CSng(0), CLng(1), "culng(1) or csng(0)")
            Check(CULng(1) Or CSng(0.9), CLng(1), "culng(1) or csng(0.9)")
            Check(CULng(83) Or CSng(1.9), CLng(83), "culng(83) or csng(1.9)")
            Check(CULng(1) Or CSng(1.5), CLng(3), "culng(1) or csng(1.5)")
            Check(CULng(1) Or CSng(1.4999), CLng(1), "culng(1) or csng(1.4999)")
            Check(CULng(1) Or CSng(2.4999), CLng(3), "culng(1) or csng(2.4999)")
            Check(CULng(83) Or CSng(1.1), CLng(83), "culng(83) or csng(1.1)")
            Check(CULng(0) Or CSng(1), CLng(1), "culng(0) or csng(1)")
            Check(CULng(0) Or CSng(50), CLng(50), "culng(0) or csng(50)")
            Check(CULng(0) Or CSng(0), CLng(0), "culng(0) or csng(0)")
            Check(CULng(0) Or CSng(0.6), CLng(1), "culng(0) or csng(0.6)")

            'ulong or double
            Check(CULng(1) Or CDbl(1), CLng(1), "culng(1) or cdbl(1)")
            Check(CULng(83) Or CDbl(50), CLng(115), "culng(83) or cdbl(50)")
            Check(CULng(1) Or CDbl(0), CLng(1), "culng(1) or cdbl(0)")
            Check(CULng(1) Or CDbl(0.9), CLng(1), "culng(1) or cdbl(0.9)")
            Check(CULng(83) Or CDbl(1.9), CLng(83), "culng(83) or cdbl(1.9)")
            Check(CULng(1) Or CDbl(1.5), CLng(3), "culng(1) or cdbl(1.5)")
            Check(CULng(1) Or CDbl(1.4999), CLng(1), "culng(1) or cdbl(1.4999)")
            Check(CULng(1) Or CDbl(2.4999), CLng(3), "culng(1) or cdbl(2.4999)")
            Check(CULng(83) Or CDbl(1.1), CLng(83), "culng(83) or cdbl(1.1)")
            Check(CULng(0) Or CDbl(1), CLng(1), "culng(0) or cdbl(1)")
            Check(CULng(0) Or CDbl(50), CLng(50), "culng(0) or cdbl(50)")
            Check(CULng(0) Or CDbl(0), CLng(0), "culng(0) or cdbl(0)")
            Check(CULng(0) Or CDbl(0.6), CLng(1), "culng(0) or cdbl(0.6)")

            'ulong or date
            'ulong or char

            'ulong or string
            Check(CULng(1) Or "1", 1L, "culng(1) or '1'")
            Check(CULng(1) Or "0", 1L, "culng(1) or '0'")
            Check(CULng(0) Or "1", 1L, "culng(0) or '1'")
            Check(CULng(0) Or "0", 0L, "culng(0) or '0'")

            'ulong or object
            Check(CULng(1) Or CObj(1), CLng(1), "culng(1) or cobj(1)")
            Check(CULng(0) Or CObj(0), CLng(0), "culng(0) or cobj(0)")

            ''''''

            'dec or single
            Check(CDec(1) Or CSng(1), CLng(1), "cdec(1) or csng(1)")
            Check(CDec(83) Or CSng(50), CLng(115), "cdec(83) or csng(50)")
            Check(CDec(1) Or CSng(0), CLng(1), "cdec(1) or csng(0)")
            Check(CDec(1) Or CSng(0.9), CLng(1), "cdec(1) or csng(0.9)")
            Check(CDec(83) Or CSng(1.9), CLng(83), "cdec(83) or csng(1.9)")
            Check(CDec(1) Or CSng(1.5), CLng(3), "cdec(1) or csng(1.5)")
            Check(CDec(1) Or CSng(1.4999), CLng(1), "cdec(1) or csng(1.4999)")
            Check(CDec(1) Or CSng(2.4999), CLng(3), "cdec(1) or csng(2.4999)")
            Check(CDec(83) Or CSng(1.1), CLng(83), "cdec(83) or csng(1.1)")
            Check(CDec(0) Or CSng(1), CLng(1), "cdec(0) or csng(1)")
            Check(CDec(0) Or CSng(50), CLng(50), "cdec(0) or csng(50)")
            Check(CDec(0) Or CSng(0), CLng(0), "cdec(0) or csng(0)")
            Check(CDec(0) Or CSng(0.6), CLng(1), "cdec(0) or csng(0.6)")

            'dec or double
            Check(CDec(1) Or CDbl(1), CLng(1), "cdec(1) or cdbl(1)")
            Check(CDec(83) Or CDbl(50), CLng(115), "cdec(83) or cdbl(50)")
            Check(CDec(1) Or CDbl(0), CLng(1), "cdec(1) or cdbl(0)")
            Check(CDec(1) Or CDbl(0.9), CLng(1), "cdec(1) or cdbl(0.9)")
            Check(CDec(83) Or CDbl(1.9), CLng(83), "cdec(83) or cdbl(1.9)")
            Check(CDec(1) Or CDbl(1.5), CLng(3), "cdec(1) or cdbl(1.5)")
            Check(CDec(1) Or CDbl(1.4999), CLng(1), "cdec(1) or cdbl(1.4999)")
            Check(CDec(1) Or CDbl(2.4999), CLng(3), "cdec(1) or cdbl(2.4999)")
            Check(CDec(83) Or CDbl(1.1), CLng(83), "cdec(83) or cdbl(1.1)")
            Check(CDec(0) Or CDbl(1), CLng(1), "cdec(0) or cdbl(1)")
            Check(CDec(0) Or CDbl(50), CLng(50), "cdec(0) or cdbl(50)")
            Check(CDec(0) Or CDbl(0), CLng(0), "cdec(0) or cdbl(0)")
            Check(CDec(0) Or CDbl(0.6), CLng(1), "cdec(0) or cdbl(0.6)")

            'dec or date
            'dec or char

            'dec or string
            Check(CDec(1) Or "1", 1L, "cdec(1) or '1'")
            Check(CDec(1) Or "0", 1L, "cdec(1) or '0'")
            Check(CDec(0) Or "1", 1L, "cdec(0) or '1'")
            Check(CDec(0) Or "0", 0L, "cdec(0) or '0'")

            'dec or object
            Check(CDec(1) Or CObj(1), CLng(1), "cdec(1) or cobj(1)")
            Check(CDec(0) Or CObj(0), CLng(0), "cdec(0) or cobj(0)")

            ''''''

            'single or double
            Check(CSng(1) Or CDbl(1), CLng(1), "csng(1) or cdbl(1)")
            Check(CSng(83) Or CDbl(50), CLng(115), "csng(83) or cdbl(50)")
            Check(CSng(1) Or CDbl(0), CLng(1), "csng(1) or cdbl(0)")
            Check(CSng(1) Or CDbl(0.9), CLng(1), "csng(1) or cdbl(0.9)")
            Check(CSng(83) Or CDbl(1.9), CLng(83), "csng(83) or cdbl(1.9)")
            Check(CSng(1) Or CDbl(1.5), CLng(3), "csng(1) or cdbl(1.5)")
            Check(CSng(1) Or CDbl(1.4999), CLng(1), "csng(1) or cdbl(1.4999)")
            Check(CSng(1) Or CDbl(2.4999), CLng(3), "csng(1) or cdbl(2.4999)")
            Check(CSng(83) Or CDbl(1.1), CLng(83), "csng(83) or cdbl(1.1)")
            Check(CSng(0) Or CDbl(1), CLng(1), "csng(0) or cdbl(1)")
            Check(CSng(0) Or CDbl(50), CLng(50), "csng(0) or cdbl(50)")
            Check(CSng(0) Or CDbl(0), CLng(0), "csng(0) or cdbl(0)")
            Check(CSng(0) Or CDbl(0.6), CLng(1), "csng(0) or cdbl(0.6)")

            'single or date
            'single or char

            'single or string
            Check(CSng(1) Or "1", 1L, "csng(1) or '1'")
            Check(CSng(1) Or "0", 1L, "csng(1) or '0'")
            Check(CSng(0) Or "1", 1L, "csng(0) or '1'")
            Check(CSng(0) Or "0", 0L, "csng(0) or '0'")

            'single or object
            Check(CSng(1) Or CObj(1), CLng(1), "csng(1) or cobj(1)")
            Check(CSng(0) Or CObj(0), CLng(0), "csng(0) or cobj(0)")

            ''''''

            'double or date
            'double or char

            'double or string
            Check(CDbl(1) Or "1", 1L, "cdbl(1) or '1'")
            Check(CDbl(1) Or "0", 1L, "cdbl(1) or '0'")
            Check(CDbl(0) Or "1", 1L, "cdbl(0) or '1'")
            Check(CDbl(0) Or "0", 0L, "cdbl(0) or '0'")

            'double or object
            Check(CDbl(1) Or CObj(1), CLng(1), "cdbl(1) or cobj(1)")
            Check(CDbl(0) Or CObj(0), CLng(0), "cdbl(0) or cobj(0)")

            ''''''

            'date or char
            'date or string
            'date or object

            ''''''

            'char or string
            'char or object

            ''''''

            'string or string
            Try
                Check("true" Or "true", True, "'true' or 'true")
                Fail("'true' or 'true': Expected InvalidCastException")
            Catch ex As InvalidCastException
                'do nothing
            End Try
            Try
                Check("true" Or "false", False, "'true' or 'false")
                Fail("'true' or 'false': Expected InvalidCastException")
            Catch ex As InvalidCastException
                'do nothing
            End Try
            Try
                Check("false" Or "true", False, "'false' or 'true'")
                Fail("'false' or 'true': Expected InvalidCastException")
            Catch ex As InvalidCastException
                'do nothing
            End Try
            Try
                Check("false" Or "false", False, "'false' or 'false'")
                Fail("'false' or 'false': Expected InvalidCastException")
            Catch ex As InvalidCastException
                'do nothing
            End Try
            Check("1" Or "1", 1L, "'1' or '1'")
            Check("1" Or "0", 1L, "'1' or '0'")
            Check("0" Or "1", 1L, "'0' or '1'")
            Check("0" Or "0", 0L, "'0' or '0'")
            Check("7" Or "89", 95L, "'7' or '89'")
            Check(Long.MaxValue.ToString() Or Long.MinValue.ToString(), -1L, "Long.MaxValue.ToString () or Long.MinValue.ToString ()")


            'string or object
            Check("1" Or CObj(1), CLng(1), "'1' or cobj(1)")
            Check("0" Or CObj(0), CLng(0), "'0' or cobj(0)")

            ''''''

            'object or object
            Check(CObj(1) Or CObj(1), CInt(1), "CObj(1) or cobj(1)")
            Check(CObj(0) Or CObj(0), CInt(0), "CObj(0) or cobj(0)")
        End Sub

        Shared Sub CheckNot()
            ' boolean
            Check(Not True, False, "not true")
            Check(Not False, True, "not false")

            ' sbyte
            Check(Not CSByte(0), CSByte(-1), "not csbyte(0)")
            Check(Not CSByte(1), CSByte(-2), "not csbyte(1)")
            Check(Not CSByte(-1), CSByte(0), "not csbyte(-1)")

            ' byte
            Check(Not CByte(0), Byte.MaxValue, "not cbyte(0)")
            Check(Not CByte(1), CByte(Byte.MaxValue - 1), "not cbyte(1)")

            ' short
            Check(Not CShort(0), CShort(-1), "not cshort(0)")
            Check(Not CShort(1), CShort(-2), "not cshort(1)")
            Check(Not CShort(-1), CShort(0), "not cshort(-1)")

            ' ushort
            Check(Not CUShort(0), UShort.MaxValue, "not cushort(0)")
            Check(Not CUShort(1), CUShort(UShort.MaxValue - 1), "not cushort(1)")

            ' int
            Check(Not CInt(0), CInt(-1), "not cint(0)")
            Check(Not CInt(1), CInt(-2), "not cint(1)")
            Check(Not CInt(-1), CInt(0), "not cint(-1)")

            ' uint
            Check(Not CUInt(0), UInteger.MaxValue, "not cuint(0)")
            Check(Not CUInt(1), CUInt(UInteger.MaxValue - 1), "not cuint(1)")

            ' long
            Check(Not CLng(0), CLng(-1), "not clng(0)")
            Check(Not CLng(1), CLng(-2), "not clng(1)")
            Check(Not CLng(-1), CLng(0), "not clng(-1)")

            ' ulong
            Check(Not CULng(0), ULong.MaxValue, "not culng(0)")
            Check(Not CULng(1), CULng(ULong.MaxValue - 1), "not culng(1)")

            ' decimal
            Check(Not CDec(0), CLng(-1), "not cdec(0)")
            Check(Not CDec(1), CLng(-2), "not cdec(1)")
            Check(Not CDec(-1), CLng(0), "not cdec(-1)")

            ' single
            Check(Not CSng(0), CLng(-1), "not csng(0)")
            Check(Not CSng(1), CLng(-2), "not csng(1)")
            Check(Not CSng(-1), CLng(0), "not csng(-1)")
            Check(Not CSng(1.1), CLng(-2), "not csng(1.1)")
            Check(Not CSng(1.5), CLng(-3), "not csng(1.5)")
            Check(Not CSng(2.5), CLng(-3), "not csng(2.5)")
            Check(Not CSng(-0.5), CLng(-1), "not csng(-0.5)")
            Check(Not CSng(-1.5), CLng(1), "not csng(-1.5)")
            Check(Not CSng(-2.5), CLng(1), "not csng(-2.5)")

            ' double
            Check(Not CDbl(0), CLng(-1), "not cdbl(0)")
            Check(Not CDbl(1), CLng(-2), "not cdbl(1)")
            Check(Not CDbl(-1), CLng(0), "not cdbl(-1)")
            Check(Not CDbl(1.1), CLng(-2), "not cdbl(1.1)")
            Check(Not CDbl(1.5), CLng(-3), "not cdbl(1.5)")
            Check(Not CDbl(2.5), CLng(-3), "not cdbl(2.5)")
            Check(Not CDbl(-0.5), CLng(-1), "not cdbl(-0.5)")
            Check(Not CDbl(-1.5), CLng(1), "not cdbl(-1.5)")
            Check(Not CDbl(-2.5), CLng(1), "not cdbl(-2.5)")

            'date
            'char

            'string
            Check(Not "0", -1L, "not '0'")
            Check(Not "1", -2L, "not '1'")
            Check(Not "123", -124L, "not '123'")
            Check(Not "-123", 122L, "not '-123'")

            'object
            Check(Not CObj(2), -3I, "not cobj(2)")
            Check(Not CObj(True), False, "not cobj(true)")

        End Sub

        Shared Sub CheckXor()
            Dim a As Boolean
            Dim b As Boolean

            ' boolean xor boolean
            Check(True Xor True, False, "true xor true")
            Check(True Xor False, True, "true xor false")
            Check(False Xor True, True, "false xor true")
            Check(False Xor False, False, "false xor false")

            a = False
            b = False
            Check(a Xor b, False, "a[false] xor b[false]")
            a = True
            b = False
            Check(a Xor b, True, "a[true] xor b [false]")
            a = False
            b = True
            Check(a Xor b, True, "a[false] xor b[true]")
            a = True
            b = True
            Check(a Xor b, False, "a[true] xor b[true]")

            'boolean xor sbyte
            Check(True Xor CSByte(1), CSByte(-2), "true xor csbyte(1)")
            Check(True Xor CSByte(50), CSByte(-51), "true xor csbyte(50)")
            Check(True Xor CSByte(0), CSByte(-1), "true xor csbyte(0)")
            Check(False Xor CSByte(1), CSByte(1), "false xor csbyte(1)")
            Check(False Xor CSByte(50), CSByte(50), "false xor csbyte(50)")
            Check(False Xor CSByte(0), CSByte(0), "false xor csbyte(0)")

            'boolean xor byte
            Check(True Xor CByte(1), CShort(-2), "true xor cbyte(1)")
            Check(True Xor CByte(50), CShort(-51), "true xor cbyte(50)")
            Check(True Xor CByte(0), CShort(-1), "true xor cbyte(0)")
            Check(False Xor CByte(1), CShort(1), "false xor cbyte(1)")
            Check(False Xor CByte(50), CShort(50), "false xor cbyte(50)")
            Check(False Xor CByte(0), CShort(0), "false xor cbyte(0)")

            'boolean xor short
            Check(True Xor CShort(1), CShort(-2), "true xor cshort(1)")
            Check(True Xor CShort(50), CShort(-51), "true xor cshort(50)")
            Check(True Xor CShort(0), CShort(-1), "true xor cshort(0)")
            Check(False Xor CShort(1), CShort(1), "false xor cshort(1)")
            Check(False Xor CShort(50), CShort(50), "false xor cshort(50)")
            Check(False Xor CShort(0), CShort(0), "false xor cshort(0)")

            'boolean xor ushort
            Check(True Xor CUShort(1), CInt(-2), "true xor cushort(1)")
            Check(True Xor CUShort(50), CInt(-51), "true xor cushort(50)")
            Check(True Xor CUShort(0), CInt(-1), "true xor cushort(0)")
            Check(False Xor CUShort(1), CInt(1), "false xor cushort(1)")
            Check(False Xor CUShort(50), CInt(50), "false xor cushort(50)")
            Check(False Xor CUShort(0), CInt(0), "false xor cushort(0)")

            'boolean xor int
            Check(True Xor CInt(1), CInt(-2), "true xor cint(1)")
            Check(True Xor CInt(50), CInt(-51), "true xor cint(50)")
            Check(True Xor CInt(0), CInt(-1), "true xor cint(0)")
            Check(False Xor CInt(1), CInt(1), "false xor cint(1)")
            Check(False Xor CInt(50), CInt(50), "false xor cint(50)")
            Check(False Xor CInt(0), CInt(0), "false xor cint(0)")

            'boolean xor uint
            Check(True Xor CUInt(1), CLng(-2), "true xor cuint(1)")
            Check(True Xor CUInt(50), CLng(-51), "true xor cuint(50)")
            Check(True Xor CUInt(0), CLng(-1), "true xor cuint(0)")
            Check(False Xor CUInt(1), CLng(1), "false xor cuint(1)")
            Check(False Xor CUInt(50), CLng(50), "false xor cuint(50)")
            Check(False Xor CUInt(0), CLng(0), "false xor cuint(0)")

            'boolean xor long
            Check(True Xor CLng(1), CLng(-2), "true xor clng(1)")
            Check(True Xor CLng(50), CLng(-51), "true xor clng(50)")
            Check(True Xor CLng(0), CLng(-1), "true xor clng(0)")
            Check(False Xor CLng(1), CLng(1), "false xor clng(1)")
            Check(False Xor CLng(50), CLng(50), "false xor clng(50)")
            Check(False Xor CLng(0), CLng(0), "false xor clng(0)")

            'boolean xor ulong
            Check(True Xor CULng(1), CLng(-2), "true xor culng(1)")
            Check(True Xor CULng(50), CLng(-51), "true xor culng(50)")
            Check(True Xor CULng(0), CLng(-1), "true xor culng(0)")
            Check(False Xor CULng(1), CLng(1), "false xor culng(1)")
            Check(False Xor CULng(50), CLng(50), "false xor culng(50)")
            Check(False Xor CULng(0), CLng(0), "false xor culng(0)")

            'boolean xor decimal
            Check(True Xor CDec(1), CLng(-2), "true xor cdec(1)")
            Check(True Xor CDec(50), CLng(-51), "true xor cdec(50)")
            Check(True Xor CDec(0), CLng(-1), "true xor cdec(0)")
            Check(False Xor CDec(1), CLng(1), "false xor cdec(1)")
            Check(False Xor CDec(50), CLng(50), "false xor cdec(50)")
            Check(False Xor CDec(0), CLng(0), "false xor cdec(0)")

            'boolean xor single
            Check(True Xor CSng(1), CLng(-2), "true xor csng(1)")
            Check(True Xor CSng(50), CLng(-51), "true xor csng(50)")
            Check(True Xor CSng(0), CLng(-1), "true xor csng(0)")
            Check(True Xor CSng(0.9), CLng(-2), "true xor csng(0.9)")
            Check(True Xor CSng(1.9), CLng(-3), "true xor csng(1.9)")
            Check(True Xor CSng(1.5), CLng(-3), "true xor csng(1.5)")
            Check(True Xor CSng(1.1), CLng(-2), "true xor csng(1.1)")
            Check(False Xor CSng(1), CLng(1), "false xor csng(1)")
            Check(False Xor CSng(50), CLng(50), "false xor csng(50)")
            Check(False Xor CSng(0), CLng(0), "false xor csng(0)")
            Check(False Xor CSng(0.6), CLng(1), "false xor csng(0.6)")

            'boolean xor double
            Check(True Xor CDbl(1), CLng(-2), "true xor cdbl(1)")
            Check(True Xor CDbl(50), CLng(-51), "true xor cdbl(50)")
            Check(True Xor CDbl(0), CLng(-1), "true xor cdbl(0)")
            Check(True Xor CDbl(0.9), CLng(-2), "true xor cdbl(0.9)")
            Check(True Xor CDbl(1.9), CLng(-3), "true xor cdbl(1.9)")
            Check(True Xor CDbl(1.5), CLng(-3), "true xor cdbl(1.5)")
            Check(True Xor CDbl(1.4999), CLng(-2), "true xor cdbl(1.4999)")
            Check(True Xor CDbl(2.4999), CLng(-3), "true xor cdbl(2.4999)")
            Check(True Xor CDbl(1.1), CLng(-2), "true xor cdbl(1.1)")
            Check(False Xor CDbl(1), CLng(1), "false xor cdbl(1)")
            Check(False Xor CDbl(50), CLng(50), "false xor cdbl(50)")
            Check(False Xor CDbl(0), CLng(0), "false xor cdbl(0)")
            Check(False Xor CDbl(0.6), CLng(1), "false xor cdbl(0.6)")

            'boolean xor date
            'boolean xor char

            'boolean xor string
            Check(True Xor "true", False, "true xor 'true'")
            Check(True Xor "false", True, "true xor 'false'")
            Check(False Xor "true", True, "false xor 'true'")
            Check(False Xor "false", False, "false xor 'false'")
            Check(True Xor "tRue", False, "true xor 'tRue'")
            Check(True Xor "falsE", True, "true xor 'falsE'")
            Check(False Xor "TRUE", True, "false xor 'TRUE'")
            Check(False Xor "false", False, "false xor 'false'")
            Check(True Xor "1", False, "true xor '1'")
            Check(True Xor "0", True, "true xor '0'")
            Check(False Xor "1", True, "false xor '1'")
            Check(False Xor "0", False, "false xor '0'")

            'string xor boolean
            Check("true" Xor True, False, "'true' xor true")
            Check("false" Xor True, True, "'false' xor true")
            Check("true" Xor False, True, "'true' xor false")
            Check("false" Xor False, False, "'false' xor false")
            Check("tRue" Xor True, False, "'tRue' xor true")
            Check("falsE" Xor True, True, "'falsE' xor true")
            Check("TRUE" Xor False, True, "'TRUE' xor and false")
            Check("false" Xor False, False, "'false' xor false")
            Check("1" Xor True, False, "'1' xor true")
            Check("0" Xor True, True, "'0' xor true")
            Check("1" Xor False, True, "'1' xor false")
            Check("0" Xor False, False, "'0' xor false")

            'boolean xor object
            Check(True Xor CObj(1), CInt(-2), "true xor cobj(1)")
            Check(False Xor CObj(0), CInt(0), "false xor cobj(0)")

            ''''''

            'sbyte xor byte
            Check(CSByte(0) Xor CByte(1), CShort(1), "csbyte(0) xor cbyte(1)")
            Check(CSByte(1) Xor CByte(50), CShort(51), "csbyte(1) xor cbyte(50)")
            Check(CSByte(83) Xor CByte(0), CShort(83), "csbyte(83) xor cbyte(0)")
            Check(CSByte(0) Xor CByte(1), CShort(1), "csbyte(0) xor cbyte(1)")
            Check(CSByte(1) Xor CByte(50), CShort(51), "csbyte(1) xor cbyte(50)")
            Check(CSByte(42) Xor CByte(0), CShort(42), "csbyte(42) xor cbyte(0)")

            'sbyte xor short
            Check(CSByte(0) Xor CShort(1), CShort(1), "csbyte(0) xor cshort(1)")
            Check(CSByte(1) Xor CShort(50), CShort(51), "csbyte(1) xor cshort(50)")
            Check(CSByte(83) Xor CShort(0), CShort(83), "csbyte(83) xor cshort(0)")
            Check(CSByte(0) Xor CShort(1), CShort(1), "csbyte(0) xor cshort(1)")
            Check(CSByte(1) Xor CShort(50), CShort(51), "csbyte(1) xor cshort(50)")
            Check(CSByte(42) Xor CShort(0), CShort(42), "csbyte(42) xor cshort(0)")

            'sbyte xor ushort
            Check(CSByte(0) Xor CUShort(1), CInt(1), "csbyte(0) xor cushort(1)")
            Check(CSByte(1) Xor CUShort(50), CInt(51), "csbyte(1) xor cushort(50)")
            Check(CSByte(83) Xor CUShort(0), CInt(83), "csbyte(83) xor cushort(0)")
            Check(CSByte(0) Xor CUShort(1), CInt(1), "csbyte(0) xor cushort(1)")
            Check(CSByte(1) Xor CUShort(50), CInt(51), "csbyte(1) xor cushort(50)")
            Check(CSByte(42) Xor CUShort(0), CInt(42), "csbyte(42) xor cushort(0)")

            'sbyte xor int
            Check(CSByte(0) Xor CInt(1), CInt(1), "csbyte(0) xor cint(1)")
            Check(CSByte(1) Xor CInt(50), CInt(51), "csbyte(1) xor cint(50)")
            Check(CSByte(83) Xor CInt(0), CInt(83), "csbyte(83) xor cint(0)")
            Check(CSByte(0) Xor CInt(1), CInt(1), "csbyte(0) xor cint(1)")
            Check(CSByte(1) Xor CInt(50), CInt(51), "csbyte(1) xor cint(50)")
            Check(CSByte(42) Xor CInt(0), CInt(42), "csbyte(42) xor cint(0)")

            'sbyte xor uint
            Check(CSByte(0) Xor CUInt(1), CLng(1), "csbyte(0) xor cuint(1)")
            Check(CSByte(1) Xor CUInt(50), CLng(51), "csbyte(1) xor cuint(50)")
            Check(CSByte(83) Xor CUInt(0), CLng(83), "csbyte(83) xor cuint(0)")
            Check(CSByte(0) Xor CUInt(1), CLng(1), "csbyte(0) xor cuint(1)")
            Check(CSByte(1) Xor CUInt(50), CLng(51), "csbyte(1) xor cuint(50)")
            Check(CSByte(42) Xor CUInt(0), CLng(42), "csbyte(42) xor cuint(0)")

            'sbyte xor long
            Check(CSByte(0) Xor CLng(1), CLng(1), "csbyte(0) xor clng(1)")
            Check(CSByte(1) Xor CLng(50), CLng(51), "csbyte(1) xor clng(50)")
            Check(CSByte(83) Xor CLng(0), CLng(83), "csbyte(83) xor clng(0)")
            Check(CSByte(0) Xor CLng(1), CLng(1), "csbyte(0) xor clng(1)")
            Check(CSByte(1) Xor CLng(50), CLng(51), "csbyte(1) xor clng(50)")
            Check(CSByte(42) Xor CLng(0), CLng(42), "csbyte(42) xor clng(0)")

            'sbyte xor ulong
            Check(CSByte(0) Xor CULng(1), CLng(1), "csbyte(0) xor culng(1)")
            Check(CSByte(1) Xor CULng(50), CLng(51), "csbyte(1) xor culng(50)")
            Check(CSByte(83) Xor CULng(0), CLng(83), "csbyte(83) xor culng(0)")
            Check(CSByte(0) Xor CULng(1), CLng(1), "csbyte(0) xor culng(1)")
            Check(CSByte(1) Xor CULng(50), CLng(51), "csbyte(1) xor culng(50)")
            Check(CSByte(42) Xor CULng(0), CLng(42), "csbyte(42) xor culng(0)")

            'sbyte xor decimal
            Check(CSByte(0) Xor CDec(1), CLng(1), "csbyte(0) xor cdec(1)")
            Check(CSByte(1) Xor CDec(50), CLng(51), "csbyte(1) xor cdec(50)")
            Check(CSByte(83) Xor CDec(0), CLng(83), "csbyte(83) xor cdec(0)")
            Check(CSByte(0) Xor CDec(1), CLng(1), "csbyte(0) xor culng(1)")
            Check(CSByte(1) Xor CDec(50), CLng(51), "csbyte(1) xor cdec(50)")
            Check(CSByte(42) Xor CDec(0), CLng(42), "csbyte(42) xor v(0)")

            'sbyte xor single
            Check(CSByte(1) Xor CSng(1), CLng(0), "csbyte(1) xor csng(1)")
            Check(CSByte(83) Xor CSng(50), CLng(97), "csbyte(83) xor csng(50)")
            Check(CSByte(1) Xor CSng(0), CLng(1), "csbyte(1) xor csng(0)")
            Check(CSByte(1) Xor CSng(0.9), CLng(0), "csbyte(1) xor csng(0.9)")
            Check(CSByte(83) Xor CSng(1.9), CLng(81), "csbyte(83) xor csng(1.9)")
            Check(CSByte(1) Xor CSng(1.5), CLng(3), "csbyte(1) xor csng(1.5)")
            Check(CSByte(1) Xor CSng(1.4999), CLng(0), "csbyte(1) xor csng(1.4999)")
            Check(CSByte(1) Xor CSng(2.4999), CLng(3), "csbyte(1) xor csng(2.4999)")
            Check(CSByte(83) Xor CSng(1.1), CLng(82), "csbyte(83) xor csng(1.1)")
            Check(CSByte(0) Xor CSng(1), CLng(1), "csbyte(0) xor csng(1)")
            Check(CSByte(0) Xor CSng(50), CLng(50), "csbyte(0) xor csng(50)")
            Check(CSByte(0) Xor CSng(0), CLng(0), "csbyte(0) xor csng(0)")
            Check(CSByte(0) Xor CSng(0.6), CLng(1), "csbyte(0) xor csng(0.6)")

            'sbyte xor double
            Check(CSByte(1) Xor CDbl(1), CLng(0), "csbyte(1) xor cdbl(1)")
            Check(CSByte(83) Xor CDbl(50), CLng(97), "csbyte(83) xor cdbl(50)")
            Check(CSByte(1) Xor CDbl(0), CLng(1), "csbyte(1) xor cdbl(0)")
            Check(CSByte(1) Xor CDbl(0.9), CLng(0), "csbyte(1) xor cdbl(0.9)")
            Check(CSByte(83) Xor CDbl(1.9), CLng(81), "csbyte(83) xor cdbl(1.9)")
            Check(CSByte(1) Xor CDbl(1.5), CLng(3), "csbyte(1) xor cdbl(1.5)")
            Check(CSByte(1) Xor CDbl(1.4999), CLng(0), "csbyte(1) xor cdbl(1.4999)")
            Check(CSByte(1) Xor CDbl(2.4999), CLng(3), "csbyte(1) xor cdbl(2.4999)")
            Check(CSByte(83) Xor CDbl(1.1), CLng(82), "csbyte(83) xor cdbl(1.1)")
            Check(CSByte(0) Xor CDbl(1), CLng(1), "csbyte(0) xor cdbl(1)")
            Check(CSByte(0) Xor CDbl(50), CLng(50), "csbyte(0) xor cdbl(50)")
            Check(CSByte(0) Xor CDbl(0), CLng(0), "csbyte(0) xor cdbl(0)")
            Check(CSByte(0) Xor CDbl(0.6), CLng(1), "csbyte(0) xor cdbl(0.6)")

            'sbyte xor date
            'sbyte xor char

            'sbyte xor string
            Check(CSByte(1) Xor "1", 0L, "csbyte(1) xor '1'")
            Check(CSByte(1) Xor "0", 1L, "csbyte(1) xor '0'")
            Check(CSByte(0) Xor "1", 1L, "csbyte(0) xor '1'")
            Check(CSByte(0) Xor "0", 0L, "csbyte(0) xor '0'")

            'sbyte xor object
            Check(CSByte(1) Xor CObj(1), CInt(0), "csbyte(1) xor cobj(1)")
            Check(CSByte(0) Xor CObj(0), CInt(0), "csbyte(0) xor cobj(0)")

            ''''''

            'byte xor short
            Check(CByte(0) Xor CShort(1), CShort(1), "cbyte(0) xor cshort(1)")
            Check(CByte(1) Xor CShort(50), CShort(51), "cbyte(1) xor cshort(50)")
            Check(CByte(83) Xor CShort(0), CShort(83), "cbyte(83) xor cshort(0)")
            Check(CByte(0) Xor CShort(1), CShort(1), "cbyte(0) xor cshort(1)")
            Check(CByte(1) Xor CShort(50), CShort(51), "cbyte(1) xor cshort(50)")
            Check(CByte(42) Xor CShort(0), CShort(42), "cbyte(42) xor cshort(0)")

            'byte xor ushort
            Check(CByte(0) Xor CUShort(1), CUShort(1), "cbyte(0) xor cushort(1)")
            Check(CByte(1) Xor CUShort(50), CUShort(51), "cbyte(1) xor cushort(50)")
            Check(CByte(83) Xor CUShort(0), CUShort(83), "cbyte(83) xor cushort(0)")
            Check(CByte(0) Xor CUShort(1), CUShort(1), "cbyte(0) xor cushort(1)")
            Check(CByte(1) Xor CUShort(50), CUShort(51), "cbyte(1) xor cushort(50)")
            Check(CByte(42) Xor CUShort(0), CUShort(42), "cbyte(42) xor cushort(0)")

            'byte xor int
            Check(CByte(0) Xor CInt(1), CInt(1), "cbyte(0) xor cint(1)")
            Check(CByte(1) Xor CInt(50), CInt(51), "cbyte(1) xor cint(50)")
            Check(CByte(83) Xor CInt(0), CInt(83), "cbyte(83) xor cint(0)")
            Check(CByte(0) Xor CInt(1), CInt(1), "cbyte(0) xor cint(1)")
            Check(CByte(1) Xor CInt(50), CInt(51), "cbyte(1) xor cint(50)")
            Check(CByte(42) Xor CInt(0), CInt(42), "cbyte(42) xor cint(0)")

            'byte xor uint
            Check(CByte(0) Xor CUInt(1), CUInt(1), "cbyte(0) xor cuint(1)")
            Check(CByte(1) Xor CUInt(50), CUInt(51), "cbyte(1) xor cuint(50)")
            Check(CByte(83) Xor CUInt(0), CUInt(83), "cbyte(83) xor cuint(0)")
            Check(CByte(0) Xor CUInt(1), CUInt(1), "cbyte(0) xor cuint(1)")
            Check(CByte(1) Xor CUInt(50), CUInt(51), "cbyte(1) xor cuint(50)")
            Check(CByte(42) Xor CUInt(0), CUInt(42), "cbyte(42) xor cuint(0)")

            'byte xor long
            Check(CByte(0) Xor CLng(1), CLng(1), "cbyte(0) xor clng(1)")
            Check(CByte(1) Xor CLng(50), CLng(51), "cbyte(1) xor clng(50)")
            Check(CByte(83) Xor CLng(0), CLng(83), "cbyte(83) xor clng(0)")
            Check(CByte(0) Xor CLng(1), CLng(1), "cbyte(0) xor clng(1)")
            Check(CByte(1) Xor CLng(50), CLng(51), "cbyte(1) xor clng(50)")
            Check(CByte(42) Xor CLng(0), CLng(42), "cbyte(42) xor clng(0)")

            'byte xor ulong
            Check(CByte(0) Xor CULng(1), CULng(1), "cbyte(0) xor culng(1)")
            Check(CByte(1) Xor CULng(50), CULng(51), "cbyte(1) xor culng(50)")
            Check(CByte(83) Xor CULng(0), CULng(83), "cbyte(83) xor culng(0)")
            Check(CByte(0) Xor CULng(1), CULng(1), "cbyte(0) xor culng(1)")
            Check(CByte(1) Xor CULng(50), CULng(51), "cbyte(1) xor culng(50)")
            Check(CByte(42) Xor CULng(0), CULng(42), "cbyte(42) xor culng(0)")

            'byte xor decimal
            Check(CByte(0) Xor CDec(1), CLng(1), "cbyte(0) xor cdec(1)")
            Check(CByte(1) Xor CDec(50), CLng(51), "cbyte(1) xor cdec(50)")
            Check(CByte(83) Xor CDec(0), CLng(83), "cbyte(83) xor cdec(0)")
            Check(CByte(0) Xor CDec(1), CLng(1), "cbyte(0) xor culng(1)")
            Check(CByte(1) Xor CDec(50), CLng(51), "cbyte(1) xor cdec(50)")
            Check(CByte(42) Xor CDec(0), CLng(42), "cbyte(42) xor v(0)")

            'byte xor single
            Check(CByte(1) Xor CSng(1), CLng(0), "cbyte(1) xor csng(1)")
            Check(CByte(83) Xor CSng(50), CLng(97), "cbyte(83) xor csng(50)")
            Check(CByte(1) Xor CSng(0), CLng(1), "cbyte(1) xor csng(0)")
            Check(CByte(1) Xor CSng(0.9), CLng(0), "cbyte(1) xor csng(0.9)")
            Check(CByte(83) Xor CSng(1.9), CLng(81), "cbyte(83) xor csng(1.9)")
            Check(CByte(1) Xor CSng(1.5), CLng(3), "cbyte(1) xor csng(1.5)")
            Check(CByte(1) Xor CSng(1.4999), CLng(0), "cbyte(1) xor csng(1.4999)")
            Check(CByte(1) Xor CSng(2.4999), CLng(3), "cbyte(1) xor csng(2.4999)")
            Check(CByte(83) Xor CSng(1.1), CLng(82), "cbyte(83) xor csng(1.1)")
            Check(CByte(0) Xor CSng(1), CLng(1), "cbyte(0) xor csng(1)")
            Check(CByte(0) Xor CSng(50), CLng(50), "cbyte(0) xor csng(50)")
            Check(CByte(0) Xor CSng(0), CLng(0), "cbyte(0) xor csng(0)")
            Check(CByte(0) Xor CSng(0.6), CLng(1), "cbyte(0) xor csng(0.6)")

            'byte xor double
            Check(CByte(1) Xor CDbl(1), CLng(0), "cbyte(1) xor cdbl(1)")
            Check(CByte(83) Xor CDbl(50), CLng(97), "cbyte(83) xor cdbl(50)")
            Check(CByte(1) Xor CDbl(0), CLng(1), "cbyte(1) xor cdbl(0)")
            Check(CByte(1) Xor CDbl(0.9), CLng(0), "cbyte(1) xor cdbl(0.9)")
            Check(CByte(83) Xor CDbl(1.9), CLng(81), "cbyte(83) xor cdbl(1.9)")
            Check(CByte(1) Xor CDbl(1.5), CLng(3), "cbyte(1) xor cdbl(1.5)")
            Check(CByte(1) Xor CDbl(1.4999), CLng(0), "cbyte(1) xor cdbl(1.4999)")
            Check(CByte(1) Xor CDbl(2.4999), CLng(3), "cbyte(1) xor cdbl(2.4999)")
            Check(CByte(83) Xor CDbl(1.1), CLng(82), "cbyte(83) xor cdbl(1.1)")
            Check(CByte(0) Xor CDbl(1), CLng(1), "cbyte(0) xor cdbl(1)")
            Check(CByte(0) Xor CDbl(50), CLng(50), "cbyte(0) xor cdbl(50)")
            Check(CByte(0) Xor CDbl(0), CLng(0), "cbyte(0) xor cdbl(0)")
            Check(CByte(0) Xor CDbl(0.6), CLng(1), "cbyte(0) xor cdbl(0.6)")

            'byte xor date
            'byte xor char

            'byte xor string
            Check(CByte(1) Xor "1", 0L, "cbyte(1) xor '1'")
            Check(CByte(1) Xor "0", 1L, "cbyte(1) xor '0'")
            Check(CByte(0) Xor "1", 1L, "cbyte(0) xor '1'")
            Check(CByte(0) Xor "0", 0L, "cbyte(0) xor '0'")

            'byte xor object
            Check(CByte(1) Xor CObj(1), CInt(0), "cbyte(1) xor cobj(1)")
            Check(CByte(0) Xor CObj(0), CInt(0), "cbyte(0) xor cobj(0)")

            ''''''

            'short xor ushort
            Check(CShort(0) Xor CUShort(1), CInt(1), "cshort(0) xor cushort(1)")
            Check(CShort(1) Xor CUShort(50), CInt(51), "cshort(1) xor cushort(50)")
            Check(CShort(83) Xor CUShort(0), CInt(83), "cshort(83) xor cushort(0)")
            Check(CShort(0) Xor CUShort(1), CInt(1), "cshort(0) xor cushort(1)")
            Check(CShort(1) Xor CUShort(50), CInt(51), "cshort(1) xor cushort(50)")
            Check(CShort(42) Xor CUShort(0), CInt(42), "cshort(42) xor cushort(0)")

            'short xor int
            Check(CShort(0) Xor CInt(1), CInt(1), "cshort(0) xor cint(1)")
            Check(CShort(1) Xor CInt(50), CInt(51), "cshort(1) xor cint(50)")
            Check(CShort(83) Xor CInt(0), CInt(83), "cshort(83) xor cint(0)")
            Check(CShort(0) Xor CInt(1), CInt(1), "cshort(0) xor cint(1)")
            Check(CShort(1) Xor CInt(50), CInt(51), "cshort(1) xor cint(50)")
            Check(CShort(42) Xor CInt(0), CInt(42), "cshort(42) xor cint(0)")

            'short xor uint
            Check(CShort(0) Xor CUInt(1), CLng(1), "cshort(0) xor cuint(1)")
            Check(CShort(1) Xor CUInt(50), CLng(51), "cshort(1) xor cuint(50)")
            Check(CShort(83) Xor CUInt(0), CLng(83), "cshort(83) xor cuint(0)")
            Check(CShort(0) Xor CUInt(1), CLng(1), "cshort(0) xor cuint(1)")
            Check(CShort(1) Xor CUInt(50), CLng(51), "cshort(1) xor cuint(50)")
            Check(CShort(42) Xor CUInt(0), CLng(42), "cshort(42) xor cuint(0)")

            'short xor long
            Check(CShort(0) Xor CLng(1), CLng(1), "cshort(0) xor clng(1)")
            Check(CShort(1) Xor CLng(50), CLng(51), "cshort(1) xor clng(50)")
            Check(CShort(83) Xor CLng(0), CLng(83), "cshort(83) xor clng(0)")
            Check(CShort(0) Xor CLng(1), CLng(1), "cshort(0) xor clng(1)")
            Check(CShort(1) Xor CLng(50), CLng(51), "cshort(1) xor clng(50)")
            Check(CShort(42) Xor CLng(0), CLng(42), "cshort(42) xor clng(0)")

            'short xor ulong
            Check(CShort(0) Xor CULng(1), CLng(1), "cshort(0) xor culng(1)")
            Check(CShort(1) Xor CULng(50), CLng(51), "cshort(1) xor culng(50)")
            Check(CShort(83) Xor CULng(0), CLng(83), "cshort(83) xor culng(0)")
            Check(CShort(0) Xor CULng(1), CLng(1), "cshort(0) xor culng(1)")
            Check(CShort(1) Xor CULng(50), CLng(51), "cshort(1) xor culng(50)")
            Check(CShort(42) Xor CULng(0), CLng(42), "cshort(42) xor culng(0)")

            'short xor decimal
            Check(CShort(0) Xor CDec(1), CLng(1), "cshort(0) xor cdec(1)")
            Check(CShort(1) Xor CDec(50), CLng(51), "cshort(1) xor cdec(50)")
            Check(CShort(83) Xor CDec(0), CLng(83), "cshort(83) xor cdec(0)")
            Check(CShort(0) Xor CDec(1), CLng(1), "cshort(0) xor culng(1)")
            Check(CShort(1) Xor CDec(50), CLng(51), "cshort(1) xor cdec(50)")
            Check(CShort(42) Xor CDec(0), CLng(42), "cshort(42) xor v(0)")

            'short xor single
            Check(CShort(1) Xor CSng(1), CLng(0), "cshort(1) xor csng(1)")
            Check(CShort(83) Xor CSng(50), CLng(97), "cshort(83) xor csng(50)")
            Check(CShort(1) Xor CSng(0), CLng(1), "cshort(1) xor csng(0)")
            Check(CShort(1) Xor CSng(0.9), CLng(0), "cshort(1) xor csng(0.9)")
            Check(CShort(83) Xor CSng(1.9), CLng(81), "cshort(83) xor csng(1.9)")
            Check(CShort(1) Xor CSng(1.5), CLng(3), "cshort(1) xor csng(1.5)")
            Check(CShort(1) Xor CSng(1.4999), CLng(0), "cshort(1) xor csng(1.4999)")
            Check(CShort(1) Xor CSng(2.4999), CLng(3), "cshort(1) xor csng(2.4999)")
            Check(CShort(83) Xor CSng(1.1), CLng(82), "cshort(83) xor csng(1.1)")
            Check(CShort(0) Xor CSng(1), CLng(1), "cshort(0) xor csng(1)")
            Check(CShort(0) Xor CSng(50), CLng(50), "cshort(0) xor csng(50)")
            Check(CShort(0) Xor CSng(0), CLng(0), "cshort(0) xor csng(0)")
            Check(CShort(0) Xor CSng(0.6), CLng(1), "cshort(0) xor csng(0.6)")

            'short xor double
            Check(CShort(1) Xor CDbl(1), CLng(0), "cshort(1) xor cdbl(1)")
            Check(CShort(83) Xor CDbl(50), CLng(97), "cshort(83) xor cdbl(50)")
            Check(CShort(1) Xor CDbl(0), CLng(1), "cshort(1) xor cdbl(0)")
            Check(CShort(1) Xor CDbl(0.9), CLng(0), "cshort(1) xor cdbl(0.9)")
            Check(CShort(83) Xor CDbl(1.9), CLng(81), "cshort(83) xor cdbl(1.9)")
            Check(CShort(1) Xor CDbl(1.5), CLng(3), "cshort(1) xor cdbl(1.5)")
            Check(CShort(1) Xor CDbl(1.4999), CLng(0), "cshort(1) xor cdbl(1.4999)")
            Check(CShort(1) Xor CDbl(2.4999), CLng(3), "cshort(1) xor cdbl(2.4999)")
            Check(CShort(83) Xor CDbl(1.1), CLng(82), "cshort(83) xor cdbl(1.1)")
            Check(CShort(0) Xor CDbl(1), CLng(1), "cshort(0) xor cdbl(1)")
            Check(CShort(0) Xor CDbl(50), CLng(50), "cshort(0) xor cdbl(50)")
            Check(CShort(0) Xor CDbl(0), CLng(0), "cshort(0) xor cdbl(0)")
            Check(CShort(0) Xor CDbl(0.6), CLng(1), "cshort(0) xor cdbl(0.6)")

            'short xor date
            'short xor char

            'short xor string
            Check(CShort(1) Xor "1", 0L, "cshort(1) xor '1'")
            Check(CShort(1) Xor "0", 1L, "cshort(1) xor '0'")
            Check(CShort(0) Xor "1", 1L, "cshort(0) xor '1'")
            Check(CShort(0) Xor "0", 0L, "cshort(0) xor '0'")

            'short xor object
            Check(CShort(1) Xor CObj(1), CInt(0), "cshort(1) xor cobj(1)")
            Check(CShort(0) Xor CObj(0), CInt(0), "cshort(0) xor cobj(0)")

            ''''''

            'ushort xor int
            Check(CUShort(0) Xor CInt(1), CInt(1), "cushort(0) xor cint(1)")
            Check(CUShort(1) Xor CInt(50), CInt(51), "cushort(1) xor cint(50)")
            Check(CUShort(83) Xor CInt(0), CInt(83), "cushort(83) xor cint(0)")
            Check(CUShort(0) Xor CInt(1), CInt(1), "cushort(0) xor cint(1)")
            Check(CUShort(1) Xor CInt(50), CInt(51), "cushort(1) xor cint(50)")
            Check(CUShort(42) Xor CInt(0), CInt(42), "cushort(42) xor cint(0)")

            'ushort xor uint
            Check(CUShort(0) Xor CUInt(1), CUInt(1), "cushort(0) xor cuint(1)")
            Check(CUShort(1) Xor CUInt(50), CUInt(51), "cushort(1) xor cuint(50)")
            Check(CUShort(83) Xor CUInt(0), CUInt(83), "cushort(83) xor cuint(0)")
            Check(CUShort(0) Xor CUInt(1), CUInt(1), "cushort(0) xor cuint(1)")
            Check(CUShort(1) Xor CUInt(50), CUInt(51), "cushort(1) xor cuint(50)")
            Check(CUShort(42) Xor CUInt(0), CUInt(42), "cushort(42) xor cuint(0)")

            'ushort xor long
            Check(CUShort(0) Xor CLng(1), CLng(1), "cushort(0) xor clng(1)")
            Check(CUShort(1) Xor CLng(50), CLng(51), "cushort(1) xor clng(50)")
            Check(CUShort(83) Xor CLng(0), CLng(83), "cushort(83) xor clng(0)")
            Check(CUShort(0) Xor CLng(1), CLng(1), "cushort(0) xor clng(1)")
            Check(CUShort(1) Xor CLng(50), CLng(51), "cushort(1) xor clng(50)")
            Check(CUShort(42) Xor CLng(0), CLng(42), "cushort(42) xor clng(0)")

            'ushort xor ulong
            Check(CUShort(0) Xor CULng(1), CULng(1), "cushort(0) xor culng(1)")
            Check(CUShort(1) Xor CULng(50), CULng(51), "cushort(1) xor culng(50)")
            Check(CUShort(83) Xor CULng(0), CULng(83), "cushort(83) xor culng(0)")
            Check(CUShort(0) Xor CULng(1), CULng(1), "cushort(0) xor culng(1)")
            Check(CUShort(1) Xor CULng(50), CULng(51), "cushort(1) xor culng(50)")
            Check(CUShort(42) Xor CULng(0), CULng(42), "cushort(42) xor culng(0)")

            'ushort xor decimal
            Check(CUShort(0) Xor CDec(1), CLng(1), "cushort(0) xor cdec(1)")
            Check(CUShort(1) Xor CDec(50), CLng(51), "cushort(1) xor cdec(50)")
            Check(CUShort(83) Xor CDec(0), CLng(83), "cushort(83) xor cdec(0)")
            Check(CUShort(0) Xor CDec(1), CLng(1), "cushort(0) xor culng(1)")
            Check(CUShort(1) Xor CDec(50), CLng(51), "cushort(1) xor cdec(50)")
            Check(CUShort(42) Xor CDec(0), CLng(42), "cushort(42) xor v(0)")

            'ushort xor single
            Check(CUShort(1) Xor CSng(1), CLng(0), "cushort(1) xor csng(1)")
            Check(CUShort(83) Xor CSng(50), CLng(97), "cushort(83) xor csng(50)")
            Check(CUShort(1) Xor CSng(0), CLng(1), "cushort(1) xor csng(0)")
            Check(CUShort(1) Xor CSng(0.9), CLng(0), "cushort(1) xor csng(0.9)")
            Check(CUShort(83) Xor CSng(1.9), CLng(81), "cushort(83) xor csng(1.9)")
            Check(CUShort(1) Xor CSng(1.5), CLng(3), "cushort(1) xor csng(1.5)")
            Check(CUShort(1) Xor CSng(1.4999), CLng(0), "cushort(1) xor csng(1.4999)")
            Check(CUShort(1) Xor CSng(2.4999), CLng(3), "cushort(1) xor csng(2.4999)")
            Check(CUShort(83) Xor CSng(1.1), CLng(82), "cushort(83) xor csng(1.1)")
            Check(CUShort(0) Xor CSng(1), CLng(1), "cushort(0) xor csng(1)")
            Check(CUShort(0) Xor CSng(50), CLng(50), "cushort(0) xor csng(50)")
            Check(CUShort(0) Xor CSng(0), CLng(0), "cushort(0) xor csng(0)")
            Check(CUShort(0) Xor CSng(0.6), CLng(1), "cushort(0) xor csng(0.6)")

            'ushort xor double
            Check(CUShort(1) Xor CDbl(1), CLng(0), "cushort(1) xor cdbl(1)")
            Check(CUShort(83) Xor CDbl(50), CLng(97), "cushort(83) xor cdbl(50)")
            Check(CUShort(1) Xor CDbl(0), CLng(1), "cushort(1) xor cdbl(0)")
            Check(CUShort(1) Xor CDbl(0.9), CLng(0), "cushort(1) xor cdbl(0.9)")
            Check(CUShort(83) Xor CDbl(1.9), CLng(81), "cushort(83) xor cdbl(1.9)")
            Check(CUShort(1) Xor CDbl(1.5), CLng(3), "cushort(1) xor cdbl(1.5)")
            Check(CUShort(1) Xor CDbl(1.4999), CLng(0), "cushort(1) xor cdbl(1.4999)")
            Check(CUShort(1) Xor CDbl(2.4999), CLng(3), "cushort(1) xor cdbl(2.4999)")
            Check(CUShort(83) Xor CDbl(1.1), CLng(82), "cushort(83) xor cdbl(1.1)")
            Check(CUShort(0) Xor CDbl(1), CLng(1), "cushort(0) xor cdbl(1)")
            Check(CUShort(0) Xor CDbl(50), CLng(50), "cushort(0) xor cdbl(50)")
            Check(CUShort(0) Xor CDbl(0), CLng(0), "cushort(0) xor cdbl(0)")
            Check(CUShort(0) Xor CDbl(0.6), CLng(1), "cushort(0) xor cdbl(0.6)")

            'ushort xor date
            'ushort xor char

            'ushort xor string
            Check(CUShort(1) Xor "1", 0L, "cushort(1) xor '1'")
            Check(CUShort(1) Xor "0", 1L, "cushort(1) xor '0'")
            Check(CUShort(0) Xor "1", 1L, "cushort(0) xor '1'")
            Check(CUShort(0) Xor "0", 0L, "cushort(0) xor '0'")

            'ushort xor object
            Check(CUShort(1) Xor CObj(1), CInt(0), "cushort(1) xor cobj(1)")
            Check(CUShort(0) Xor CObj(0), CInt(0), "cushort(0) xor cobj(0)")

            ''''''

            'int xor uint
            Check(CInt(0) Xor CUInt(1), CLng(1), "cint(0) xor cuint(1)")
            Check(CInt(1) Xor CUInt(50), CLng(51), "cint(1) xor cuint(50)")
            Check(CInt(83) Xor CUInt(0), CLng(83), "cint(83) xor cuint(0)")
            Check(CInt(0) Xor CUInt(1), CLng(1), "cint(0) xor cuint(1)")
            Check(CInt(1) Xor CUInt(50), CLng(51), "cint(1) xor cuint(50)")
            Check(CInt(42) Xor CUInt(0), CLng(42), "cint(42) xor cuint(0)")

            'int xor long
            Check(CInt(0) Xor CLng(1), CLng(1), "cint(0) xor clng(1)")
            Check(CInt(1) Xor CLng(50), CLng(51), "cint(1) xor clng(50)")
            Check(CInt(83) Xor CLng(0), CLng(83), "cint(83) xor clng(0)")
            Check(CInt(0) Xor CLng(1), CLng(1), "cint(0) xor clng(1)")
            Check(CInt(1) Xor CLng(50), CLng(51), "cint(1) xor clng(50)")
            Check(CInt(42) Xor CLng(0), CLng(42), "cint(42) xor clng(0)")

            'int xor ulong
            Check(CInt(0) Xor CULng(1), CLng(1), "cint(0) xor culng(1)")
            Check(CInt(1) Xor CULng(50), CLng(51), "cint(1) xor culng(50)")
            Check(CInt(83) Xor CULng(0), CLng(83), "cint(83) xor culng(0)")
            Check(CInt(0) Xor CULng(1), CLng(1), "cint(0) xor culng(1)")
            Check(CInt(1) Xor CULng(50), CLng(51), "cint(1) xor culng(50)")
            Check(CInt(42) Xor CULng(0), CLng(42), "cint(42) xor culng(0)")

            'int xor decimal
            Check(CInt(0) Xor CDec(1), CLng(1), "cint(0) xor cdec(1)")
            Check(CInt(1) Xor CDec(50), CLng(51), "cint(1) xor cdec(50)")
            Check(CInt(83) Xor CDec(0), CLng(83), "cint(83) xor cdec(0)")
            Check(CInt(0) Xor CDec(1), CLng(1), "cint(0) xor culng(1)")
            Check(CInt(1) Xor CDec(50), CLng(51), "cint(1) xor cdec(50)")
            Check(CInt(42) Xor CDec(0), CLng(42), "cint(42) xor v(0)")

            'int xor single
            Check(CInt(1) Xor CSng(1), CLng(0), "cint(1) xor csng(1)")
            Check(CInt(83) Xor CSng(50), CLng(97), "cint(83) xor csng(50)")
            Check(CInt(1) Xor CSng(0), CLng(1), "cint(1) xor csng(0)")
            Check(CInt(1) Xor CSng(0.9), CLng(0), "cint(1) xor csng(0.9)")
            Check(CInt(83) Xor CSng(1.9), CLng(81), "cint(83) xor csng(1.9)")
            Check(CInt(1) Xor CSng(1.5), CLng(3), "cint(1) xor csng(1.5)")
            Check(CInt(1) Xor CSng(1.4999), CLng(0), "cint(1) xor csng(1.4999)")
            Check(CInt(1) Xor CSng(2.4999), CLng(3), "cint(1) xor csng(2.4999)")
            Check(CInt(83) Xor CSng(1.1), CLng(82), "cint(83) xor csng(1.1)")
            Check(CInt(0) Xor CSng(1), CLng(1), "cint(0) xor csng(1)")
            Check(CInt(0) Xor CSng(50), CLng(50), "cint(0) xor csng(50)")
            Check(CInt(0) Xor CSng(0), CLng(0), "cint(0) xor csng(0)")
            Check(CInt(0) Xor CSng(0.6), CLng(1), "cint(0) xor csng(0.6)")

            'int xor double
            Check(CInt(1) Xor CDbl(1), CLng(0), "cint(1) xor cdbl(1)")
            Check(CInt(83) Xor CDbl(50), CLng(97), "cint(83) xor cdbl(50)")
            Check(CInt(1) Xor CDbl(0), CLng(1), "cint(1) xor cdbl(0)")
            Check(CInt(1) Xor CDbl(0.9), CLng(0), "cint(1) xor cdbl(0.9)")
            Check(CInt(83) Xor CDbl(1.9), CLng(81), "cint(83) xor cdbl(1.9)")
            Check(CInt(1) Xor CDbl(1.5), CLng(3), "cint(1) xor cdbl(1.5)")
            Check(CInt(1) Xor CDbl(1.4999), CLng(0), "cint(1) xor cdbl(1.4999)")
            Check(CInt(1) Xor CDbl(2.4999), CLng(3), "cint(1) xor cdbl(2.4999)")
            Check(CInt(83) Xor CDbl(1.1), CLng(82), "cint(83) xor cdbl(1.1)")
            Check(CInt(0) Xor CDbl(1), CLng(1), "cint(0) xor cdbl(1)")
            Check(CInt(0) Xor CDbl(50), CLng(50), "cint(0) xor cdbl(50)")
            Check(CInt(0) Xor CDbl(0), CLng(0), "cint(0) xor cdbl(0)")
            Check(CInt(0) Xor CDbl(0.6), CLng(1), "cint(0) xor cdbl(0.6)")

            'int xor date
            'int xor char

            'int xor string
            Check(CInt(1) Xor "1", 0L, "cint(1) xor '1'")
            Check(CInt(1) Xor "0", 1L, "cint(1) xor '0'")
            Check(CInt(0) Xor "1", 1L, "cint(0) xor '1'")
            Check(CInt(0) Xor "0", 0L, "cint(0) xor '0'")

            'int xor object
            Check(CInt(1) Xor CObj(1), CInt(0), "cint(1) xor cobj(1)")
            Check(CInt(0) Xor CObj(0), CInt(0), "cint(0) xor cobj(0)")

            ''''''

            'uint xor long
            Check(CUInt(0) Xor CLng(1), CLng(1), "cuint(0) xor clng(1)")
            Check(CUInt(1) Xor CLng(50), CLng(51), "cuint(1) xor clng(50)")
            Check(CUInt(83) Xor CLng(0), CLng(83), "cuint(83) xor clng(0)")
            Check(CUInt(0) Xor CLng(1), CLng(1), "cuint(0) xor clng(1)")
            Check(CUInt(1) Xor CLng(50), CLng(51), "cuint(1) xor clng(50)")
            Check(CUInt(42) Xor CLng(0), CLng(42), "cuint(42) xor clng(0)")

            'uint xor ulong
            Check(CUInt(0) Xor CULng(1), CULng(1), "cuint(0) xor culng(1)")
            Check(CUInt(1) Xor CULng(50), CULng(51), "cuint(1) xor culng(50)")
            Check(CUInt(83) Xor CULng(0), CULng(83), "cuint(83) xor culng(0)")
            Check(CUInt(0) Xor CULng(1), CULng(1), "cuint(0) xor culng(1)")
            Check(CUInt(1) Xor CULng(50), CULng(51), "cuint(1) xor culng(50)")
            Check(CUInt(42) Xor CULng(0), CULng(42), "cuint(42) xor culng(0)")

            'uint xor decimal
            Check(CUInt(0) Xor CDec(1), CLng(1), "cuint(0) xor cdec(1)")
            Check(CUInt(1) Xor CDec(50), CLng(51), "cuint(1) xor cdec(50)")
            Check(CUInt(83) Xor CDec(0), CLng(83), "cuint(83) xor cdec(0)")
            Check(CUInt(0) Xor CDec(1), CLng(1), "cuint(0) xor culng(1)")
            Check(CUInt(1) Xor CDec(50), CLng(51), "cuint(1) xor cdec(50)")
            Check(CUInt(42) Xor CDec(0), CLng(42), "cuint(42) xor v(0)")

            'uint xor single
            Check(CUInt(1) Xor CSng(1), CLng(0), "cuint(1) xor csng(1)")
            Check(CUInt(83) Xor CSng(50), CLng(97), "cuint(83) xor csng(50)")
            Check(CUInt(1) Xor CSng(0), CLng(1), "cuint(1) xor csng(0)")
            Check(CUInt(1) Xor CSng(0.9), CLng(0), "cuint(1) xor csng(0.9)")
            Check(CUInt(83) Xor CSng(1.9), CLng(81), "cuint(83) xor csng(1.9)")
            Check(CUInt(1) Xor CSng(1.5), CLng(3), "cuint(1) xor csng(1.5)")
            Check(CUInt(1) Xor CSng(1.4999), CLng(0), "cuint(1) xor csng(1.4999)")
            Check(CUInt(1) Xor CSng(2.4999), CLng(3), "cuint(1) xor csng(2.4999)")
            Check(CUInt(83) Xor CSng(1.1), CLng(82), "cuint(83) xor csng(1.1)")
            Check(CUInt(0) Xor CSng(1), CLng(1), "cuint(0) xor csng(1)")
            Check(CUInt(0) Xor CSng(50), CLng(50), "cuint(0) xor csng(50)")
            Check(CUInt(0) Xor CSng(0), CLng(0), "cuint(0) xor csng(0)")
            Check(CUInt(0) Xor CSng(0.6), CLng(1), "cuint(0) xor csng(0.6)")

            'uint xor double
            Check(CUInt(1) Xor CDbl(1), CLng(0), "cuint(1) xor cdbl(1)")
            Check(CUInt(83) Xor CDbl(50), CLng(97), "cuint(83) xor cdbl(50)")
            Check(CUInt(1) Xor CDbl(0), CLng(1), "cuint(1) xor cdbl(0)")
            Check(CUInt(1) Xor CDbl(0.9), CLng(0), "cuint(1) xor cdbl(0.9)")
            Check(CUInt(83) Xor CDbl(1.9), CLng(81), "cuint(83) xor cdbl(1.9)")
            Check(CUInt(1) Xor CDbl(1.5), CLng(3), "cuint(1) xor cdbl(1.5)")
            Check(CUInt(1) Xor CDbl(1.4999), CLng(0), "cuint(1) xor cdbl(1.4999)")
            Check(CUInt(1) Xor CDbl(2.4999), CLng(3), "cuint(1) xor cdbl(2.4999)")
            Check(CUInt(83) Xor CDbl(1.1), CLng(82), "cuint(83) xor cdbl(1.1)")
            Check(CUInt(0) Xor CDbl(1), CLng(1), "cuint(0) xor cdbl(1)")
            Check(CUInt(0) Xor CDbl(50), CLng(50), "cuint(0) xor cdbl(50)")
            Check(CUInt(0) Xor CDbl(0), CLng(0), "cuint(0) xor cdbl(0)")
            Check(CUInt(0) Xor CDbl(0.6), CLng(1), "cuint(0) xor cdbl(0.6)")

            'uint xor date
            'uint xor char

            'uint xor string
            Check(CUInt(1) Xor "1", 0L, "cuint(1) xor '1'")
            Check(CUInt(1) Xor "0", 1L, "cuint(1) xor '0'")
            Check(CUInt(0) Xor "1", 1L, "cuint(0) xor '1'")
            Check(CUInt(0) Xor "0", 0L, "cuint(0) xor '0'")

            'uint xor object
            Check(CUInt(1) Xor CObj(1), CLng(0), "cuint(1) xor cobj(1)")
            Check(CUInt(0) Xor CObj(0), CLng(0), "cuint(0) xor cobj(0)")

            ''''''

            'long xor ulong
            Check(CLng(0) Xor CULng(1), CLng(1), "clng(0) xor culng(1)")
            Check(CLng(1) Xor CULng(50), CLng(51), "clng(1) xor culng(50)")
            Check(CLng(83) Xor CULng(0), CLng(83), "clng(83) xor culng(0)")
            Check(CLng(0) Xor CULng(1), CLng(1), "clng(0) xor culng(1)")
            Check(CLng(1) Xor CULng(50), CLng(51), "clng(1) xor culng(50)")
            Check(CLng(42) Xor CULng(0), CLng(42), "clng(42) xor culng(0)")

            'long xor decimal
            Check(CLng(0) Xor CDec(1), CLng(1), "clng(0) xor cdec(1)")
            Check(CLng(1) Xor CDec(50), CLng(51), "clng(1) xor cdec(50)")
            Check(CLng(83) Xor CDec(0), CLng(83), "clng(83) xor cdec(0)")
            Check(CLng(0) Xor CDec(1), CLng(1), "clng(0) xor culng(1)")
            Check(CLng(1) Xor CDec(50), CLng(51), "clng(1) xor cdec(50)")
            Check(CLng(42) Xor CDec(0), CLng(42), "clng(42) xor v(0)")

            'long xor single
            Check(CLng(1) Xor CSng(1), CLng(0), "clng(1) xor csng(1)")
            Check(CLng(83) Xor CSng(50), CLng(97), "clng(83) xor csng(50)")
            Check(CLng(1) Xor CSng(0), CLng(1), "clng(1) xor csng(0)")
            Check(CLng(1) Xor CSng(0.9), CLng(0), "clng(1) xor csng(0.9)")
            Check(CLng(83) Xor CSng(1.9), CLng(81), "clng(83) xor csng(1.9)")
            Check(CLng(1) Xor CSng(1.5), CLng(3), "clng(1) xor csng(1.5)")
            Check(CLng(1) Xor CSng(1.4999), CLng(0), "clng(1) xor csng(1.4999)")
            Check(CLng(1) Xor CSng(2.4999), CLng(3), "clng(1) xor csng(2.4999)")
            Check(CLng(83) Xor CSng(1.1), CLng(82), "clng(83) xor csng(1.1)")
            Check(CLng(0) Xor CSng(1), CLng(1), "clng(0) xor csng(1)")
            Check(CLng(0) Xor CSng(50), CLng(50), "clng(0) xor csng(50)")
            Check(CLng(0) Xor CSng(0), CLng(0), "clng(0) xor csng(0)")
            Check(CLng(0) Xor CSng(0.6), CLng(1), "clng(0) xor csng(0.6)")

            'long xor double
            Check(CLng(1) Xor CDbl(1), CLng(0), "clng(1) xor cdbl(1)")
            Check(CLng(83) Xor CDbl(50), CLng(97), "clng(83) xor cdbl(50)")
            Check(CLng(1) Xor CDbl(0), CLng(1), "clng(1) xor cdbl(0)")
            Check(CLng(1) Xor CDbl(0.9), CLng(0), "clng(1) xor cdbl(0.9)")
            Check(CLng(83) Xor CDbl(1.9), CLng(81), "clng(83) xor cdbl(1.9)")
            Check(CLng(1) Xor CDbl(1.5), CLng(3), "clng(1) xor cdbl(1.5)")
            Check(CLng(1) Xor CDbl(1.4999), CLng(0), "clng(1) xor cdbl(1.4999)")
            Check(CLng(1) Xor CDbl(2.4999), CLng(3), "clng(1) xor cdbl(2.4999)")
            Check(CLng(83) Xor CDbl(1.1), CLng(82), "clng(83) xor cdbl(1.1)")
            Check(CLng(0) Xor CDbl(1), CLng(1), "clng(0) xor cdbl(1)")
            Check(CLng(0) Xor CDbl(50), CLng(50), "clng(0) xor cdbl(50)")
            Check(CLng(0) Xor CDbl(0), CLng(0), "clng(0) xor cdbl(0)")
            Check(CLng(0) Xor CDbl(0.6), CLng(1), "clng(0) xor cdbl(0.6)")

            'long xor date
            'long xor char

            'long xor string
            Check(CLng(1) Xor "1", 0L, "clng(1) xor '1'")
            Check(CLng(1) Xor "0", 1L, "clng(1) xor '0'")
            Check(CLng(0) Xor "1", 1L, "clng(0) xor '1'")
            Check(CLng(0) Xor "0", 0L, "clng(0) xor '0'")

            'long xor object
            Check(CLng(1) Xor CObj(1), CLng(0), "clng(1) xor cobj(1)")
            Check(CLng(0) Xor CObj(0), CLng(0), "clng(0) xor cobj(0)")

            ''''''

            'ulong xor decimal
            Check(CULng(0) Xor CDec(1), CLng(1), "culng(0) xor cdec(1)")
            Check(CULng(1) Xor CDec(50), CLng(51), "culng(1) xor cdec(50)")
            Check(CULng(83) Xor CDec(0), CLng(83), "culng(83) xor cdec(0)")
            Check(CULng(0) Xor CDec(1), CLng(1), "culng(0) xor culng(1)")
            Check(CULng(1) Xor CDec(50), CLng(51), "culng(1) xor cdec(50)")
            Check(CULng(42) Xor CDec(0), CLng(42), "culng(42) xor v(0)")

            'ulong xor single
            Check(CULng(1) Xor CSng(1), CLng(0), "culng(1) xor csng(1)")
            Check(CULng(83) Xor CSng(50), CLng(97), "culng(83) xor csng(50)")
            Check(CULng(1) Xor CSng(0), CLng(1), "culng(1) xor csng(0)")
            Check(CULng(1) Xor CSng(0.9), CLng(0), "culng(1) xor csng(0.9)")
            Check(CULng(83) Xor CSng(1.9), CLng(81), "culng(83) xor csng(1.9)")
            Check(CULng(1) Xor CSng(1.5), CLng(3), "culng(1) xor csng(1.5)")
            Check(CULng(1) Xor CSng(1.4999), CLng(0), "culng(1) xor csng(1.4999)")
            Check(CULng(1) Xor CSng(2.4999), CLng(3), "culng(1) xor csng(2.4999)")
            Check(CULng(83) Xor CSng(1.1), CLng(82), "culng(83) xor csng(1.1)")
            Check(CULng(0) Xor CSng(1), CLng(1), "culng(0) xor csng(1)")
            Check(CULng(0) Xor CSng(50), CLng(50), "culng(0) xor csng(50)")
            Check(CULng(0) Xor CSng(0), CLng(0), "culng(0) xor csng(0)")
            Check(CULng(0) Xor CSng(0.6), CLng(1), "culng(0) xor csng(0.6)")

            'ulong xor double
            Check(CULng(1) Xor CDbl(1), CLng(0), "culng(1) xor cdbl(1)")
            Check(CULng(83) Xor CDbl(50), CLng(97), "culng(83) xor cdbl(50)")
            Check(CULng(1) Xor CDbl(0), CLng(1), "culng(1) xor cdbl(0)")
            Check(CULng(1) Xor CDbl(0.9), CLng(0), "culng(1) xor cdbl(0.9)")
            Check(CULng(83) Xor CDbl(1.9), CLng(81), "culng(83) xor cdbl(1.9)")
            Check(CULng(1) Xor CDbl(1.5), CLng(3), "culng(1) xor cdbl(1.5)")
            Check(CULng(1) Xor CDbl(1.4999), CLng(0), "culng(1) xor cdbl(1.4999)")
            Check(CULng(1) Xor CDbl(2.4999), CLng(3), "culng(1) xor cdbl(2.4999)")
            Check(CULng(83) Xor CDbl(1.1), CLng(82), "culng(83) xor cdbl(1.1)")
            Check(CULng(0) Xor CDbl(1), CLng(1), "culng(0) xor cdbl(1)")
            Check(CULng(0) Xor CDbl(50), CLng(50), "culng(0) xor cdbl(50)")
            Check(CULng(0) Xor CDbl(0), CLng(0), "culng(0) xor cdbl(0)")
            Check(CULng(0) Xor CDbl(0.6), CLng(1), "culng(0) xor cdbl(0.6)")

            'ulong xor date
            'ulong xor char

            'ulong xor string
            Check(CULng(1) Xor "1", 0L, "culng(1) xor '1'")
            Check(CULng(1) Xor "0", 1L, "culng(1) xor '0'")
            Check(CULng(0) Xor "1", 1L, "culng(0) xor '1'")
            Check(CULng(0) Xor "0", 0L, "culng(0) xor '0'")

            'ulong xor object
            Check(CULng(1) Xor CObj(1), CLng(0), "culng(1) xor cobj(1)")
            Check(CULng(0) Xor CObj(0), CLng(0), "culng(0) xor cobj(0)")

            ''''''

            'dec xor single
            Check(CDec(1) Xor CSng(1), CLng(0), "cdec(1) xor csng(1)")
            Check(CDec(83) Xor CSng(50), CLng(97), "cdec(83) xor csng(50)")
            Check(CDec(1) Xor CSng(0), CLng(1), "cdec(1) xor csng(0)")
            Check(CDec(1) Xor CSng(0.9), CLng(0), "cdec(1) xor csng(0.9)")
            Check(CDec(83) Xor CSng(1.9), CLng(81), "cdec(83) xor csng(1.9)")
            Check(CDec(1) Xor CSng(1.5), CLng(3), "cdec(1) xor csng(1.5)")
            Check(CDec(1) Xor CSng(1.4999), CLng(0), "cdec(1) xor csng(1.4999)")
            Check(CDec(1) Xor CSng(2.4999), CLng(3), "cdec(1) xor csng(2.4999)")
            Check(CDec(83) Xor CSng(1.1), CLng(82), "cdec(83) xor csng(1.1)")
            Check(CDec(0) Xor CSng(1), CLng(1), "cdec(0) xor csng(1)")
            Check(CDec(0) Xor CSng(50), CLng(50), "cdec(0) xor csng(50)")
            Check(CDec(0) Xor CSng(0), CLng(0), "cdec(0) xor csng(0)")
            Check(CDec(0) Xor CSng(0.6), CLng(1), "cdec(0) xor csng(0.6)")

            'dec xor double
            Check(CDec(1) Xor CDbl(1), CLng(0), "cdec(1) xor cdbl(1)")
            Check(CDec(83) Xor CDbl(50), CLng(97), "cdec(83) xor cdbl(50)")
            Check(CDec(1) Xor CDbl(0), CLng(1), "cdec(1) xor cdbl(0)")
            Check(CDec(1) Xor CDbl(0.9), CLng(0), "cdec(1) xor cdbl(0.9)")
            Check(CDec(83) Xor CDbl(1.9), CLng(81), "cdec(83) xor cdbl(1.9)")
            Check(CDec(1) Xor CDbl(1.5), CLng(3), "cdec(1) xor cdbl(1.5)")
            Check(CDec(1) Xor CDbl(1.4999), CLng(0), "cdec(1) xor cdbl(1.4999)")
            Check(CDec(1) Xor CDbl(2.4999), CLng(3), "cdec(1) xor cdbl(2.4999)")
            Check(CDec(83) Xor CDbl(1.1), CLng(82), "cdec(83) xor cdbl(1.1)")
            Check(CDec(0) Xor CDbl(1), CLng(1), "cdec(0) xor cdbl(1)")
            Check(CDec(0) Xor CDbl(50), CLng(50), "cdec(0) xor cdbl(50)")
            Check(CDec(0) Xor CDbl(0), CLng(0), "cdec(0) xor cdbl(0)")
            Check(CDec(0) Xor CDbl(0.6), CLng(1), "cdec(0) xor cdbl(0.6)")

            'dec xor date
            'dec xor char

            'dec xor string
            Check(CDec(1) Xor "1", 0L, "cdec(1) xor '1'")
            Check(CDec(1) Xor "0", 1L, "cdec(1) xor '0'")
            Check(CDec(0) Xor "1", 1L, "cdec(0) xor '1'")
            Check(CDec(0) Xor "0", 0L, "cdec(0) xor '0'")

            'dec xor object
            Check(CDec(1) Xor CObj(1), CLng(0), "cdec(1) xor cobj(1)")
            Check(CDec(0) Xor CObj(0), CLng(0), "cdec(0) xor cobj(0)")

            ''''''

            'single xor double
            Check(CSng(1) Xor CDbl(1), CLng(0), "csng(1) xor cdbl(1)")
            Check(CSng(83) Xor CDbl(50), CLng(97), "csng(83) xor cdbl(50)")
            Check(CSng(1) Xor CDbl(0), CLng(1), "csng(1) xor cdbl(0)")
            Check(CSng(1) Xor CDbl(0.9), CLng(0), "csng(1) xor cdbl(0.9)")
            Check(CSng(83) Xor CDbl(1.9), CLng(81), "csng(83) xor cdbl(1.9)")
            Check(CSng(1) Xor CDbl(1.5), CLng(3), "csng(1) xor cdbl(1.5)")
            Check(CSng(1) Xor CDbl(1.4999), CLng(0), "csng(1) xor cdbl(1.4999)")
            Check(CSng(1) Xor CDbl(2.4999), CLng(3), "csng(1) xor cdbl(2.4999)")
            Check(CSng(83) Xor CDbl(1.1), CLng(82), "csng(83) xor cdbl(1.1)")
            Check(CSng(0) Xor CDbl(1), CLng(1), "csng(0) xor cdbl(1)")
            Check(CSng(0) Xor CDbl(50), CLng(50), "csng(0) xor cdbl(50)")
            Check(CSng(0) Xor CDbl(0), CLng(0), "csng(0) xor cdbl(0)")
            Check(CSng(0) Xor CDbl(0.6), CLng(1), "csng(0) xor cdbl(0.6)")

            'single xor date
            'single xor char

            'single xor string
            Check(CSng(1) Xor "1", 0L, "csng(1) xor '1'")
            Check(CSng(1) Xor "0", 1L, "csng(1) xor '0'")
            Check(CSng(0) Xor "1", 1L, "csng(0) xor '1'")
            Check(CSng(0) Xor "0", 0L, "csng(0) xor '0'")

            'single xor object
            Check(CSng(1) Xor CObj(1), CLng(0), "csng(1) xor cobj(1)")
            Check(CSng(0) Xor CObj(0), CLng(0), "csng(0) xor cobj(0)")

            ''''''

            'double xor date
            'double xor char

            'double xor string
            Check(CDbl(1) Xor "1", 0L, "cdbl(1) xor '1'")
            Check(CDbl(1) Xor "0", 1L, "cdbl(1) xor '0'")
            Check(CDbl(0) Xor "1", 1L, "cdbl(0) xor '1'")
            Check(CDbl(0) Xor "0", 0L, "cdbl(0) xor '0'")

            'double xor object
            Check(CDbl(1) Xor CObj(1), CLng(0), "cdbl(1) xor cobj(1)")
            Check(CDbl(0) Xor CObj(0), CLng(0), "cdbl(0) xor cobj(0)")

            ''''''

            'date xor char
            'date xor string
            'date xor object

            ''''''

            'char xor string
            'char xor object

            ''''''

            'string xor string
            Try
                Check("true" Xor "true", True, "'true' xor 'true")
                Fail("'true' xor 'true': Expected InvalidCastException")
            Catch ex As InvalidCastException
                'do nothing
            End Try
            Try
                Check("true" Xor "false", False, "'true' xor 'false")
                Fail("'true' xor 'false': Expected InvalidCastException")
            Catch ex As InvalidCastException
                'do nothing
            End Try
            Try
                Check("false" Xor "true", False, "'false' xor 'true'")
                Fail("'false' xor 'true': Expected InvalidCastException")
            Catch ex As InvalidCastException
                'do nothing
            End Try
            Try
                Check("false" Xor "false", False, "'false' xor 'false'")
                Fail("'false' xor 'false': Expected InvalidCastException")
            Catch ex As InvalidCastException
                'do nothing
            End Try
            Check("1" Xor "1", 0L, "'1' xor '1'")
            Check("1" Xor "0", 1L, "'1' xor '0'")
            Check("0" Xor "1", 1L, "'0' xor '1'")
            Check("0" Xor "0", 0L, "'0' xor '0'")
            Check("7" Xor "89", 94L, "'7' xor '89'")
            Check(Long.MaxValue.ToString() Xor Long.MinValue.ToString(), -1L, "Long.MaxValue.ToString () xor Long.MinValue.ToString ()")


            'string xor object
            Check("1" Xor CObj(1), CLng(0), "'1' xor cobj(1)")
            Check("0" Xor CObj(0), CLng(0), "'0' xor cobj(0)")

            ''''''

            'object xor object
            Check(CObj(1) Xor CObj(1), CInt(0), "CObj(1) xor cobj(1)")
            Check(CObj(0) Xor CObj(0), CInt(0), "CObj(0) xor cobj(0)")

        End Sub

        Shared Function Main() As Integer
            CheckAnd()
            CheckOr()
            CheckNot()
            CheckXor()
            Return failures
        End Function
    End Class
End Namespace