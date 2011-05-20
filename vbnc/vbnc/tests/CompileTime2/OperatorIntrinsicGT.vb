Class BinaryOperatorGT
    Inherits IntrinsicOperatorTests
    Shared Function Main As Integer
        Try
#If Boolean_ERRORS
            M(bool > bool, "bool > bool")
#End If
#If Boolean_ERRORS
            M(bool > b, "bool > b")
#End If
#If Boolean_ERRORS
            M(bool > sb, "bool > sb")
#End If
#If Boolean_ERRORS
            M(bool > s, "bool > s")
#End If
#If Boolean_ERRORS
            M(bool > us, "bool > us")
#End If
#If Boolean_ERRORS
            M(bool > i, "bool > i")
#End If
#If Boolean_ERRORS
            M(bool > ui, "bool > ui")
#End If
#If Boolean_ERRORS
            M(bool > l, "bool > l")
#End If
#If Boolean_ERRORS
            M(bool > ul, "bool > ul")
#End If
#If Boolean_ERRORS
            M(bool > dec, "bool > dec")
#End If
#If Boolean_ERRORS
            M(bool > sng, "bool > sng")
#End If
#If Boolean_ERRORS
            M(bool > dbl, "bool > dbl")
#End If
#If Boolean_ERRORS
            M(bool > chr, "bool > chr")
#End If
#If Boolean_ERRORS
            M(bool > str, "bool > str")
#End If
#If Boolean_ERRORS
            M(bool > dt, "bool > dt")
#End If
#If Boolean_ERRORS
            M(bool > dbnull, "bool > dbnull")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Object
            M(bool > obj, "bool > obj")
#End If
#If Byte_ERRORS
            M(b > bool, "b > bool")
#End If
            expected_tc = TypeCode.Boolean
            M(b > b, "b > b")
            expected_tc = TypeCode.Boolean
            M(b > sb, "b > sb")
            expected_tc = TypeCode.Boolean
            M(b > s, "b > s")
            expected_tc = TypeCode.Boolean
            M(b > us, "b > us")
            expected_tc = TypeCode.Boolean
            M(b > i, "b > i")
            expected_tc = TypeCode.Boolean
            M(b > ui, "b > ui")
            expected_tc = TypeCode.Boolean
            M(b > l, "b > l")
            expected_tc = TypeCode.Boolean
            M(b > ul, "b > ul")
            expected_tc = TypeCode.Boolean
            M(b > dec, "b > dec")
            expected_tc = TypeCode.Boolean
            M(b > sng, "b > sng")
            expected_tc = TypeCode.Boolean
            M(b > dbl, "b > dbl")
#If Byte_ERRORS
            M(b > chr, "b > chr")
#End If
#If Byte_ERRORS
            M(b > str, "b > str")
#End If
#If Byte_ERRORS
            M(b > dt, "b > dt")
#End If
#If Byte_ERRORS
            M(b > dbnull, "b > dbnull")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Object
            M(b > obj, "b > obj")
#End If
#If SByte_ERRORS
            M(sb > bool, "sb > bool")
#End If
            expected_tc = TypeCode.Boolean
            M(sb > b, "sb > b")
            expected_tc = TypeCode.Boolean
            M(sb > sb, "sb > sb")
            expected_tc = TypeCode.Boolean
            M(sb > s, "sb > s")
            expected_tc = TypeCode.Boolean
            M(sb > us, "sb > us")
            expected_tc = TypeCode.Boolean
            M(sb > i, "sb > i")
            expected_tc = TypeCode.Boolean
            M(sb > ui, "sb > ui")
            expected_tc = TypeCode.Boolean
            M(sb > l, "sb > l")
            expected_tc = TypeCode.Boolean
            M(sb > ul, "sb > ul")
            expected_tc = TypeCode.Boolean
            M(sb > dec, "sb > dec")
            expected_tc = TypeCode.Boolean
            M(sb > sng, "sb > sng")
            expected_tc = TypeCode.Boolean
            M(sb > dbl, "sb > dbl")
#If SByte_ERRORS
            M(sb > chr, "sb > chr")
#End If
#If SByte_ERRORS
            M(sb > str, "sb > str")
#End If
#If SByte_ERRORS
            M(sb > dt, "sb > dt")
#End If
#If SByte_ERRORS
            M(sb > dbnull, "sb > dbnull")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Object
            M(sb > obj, "sb > obj")
#End If
#If Int16_ERRORS
            M(s > bool, "s > bool")
#End If
            expected_tc = TypeCode.Boolean
            M(s > b, "s > b")
            expected_tc = TypeCode.Boolean
            M(s > sb, "s > sb")
            expected_tc = TypeCode.Boolean
            M(s > s, "s > s")
            expected_tc = TypeCode.Boolean
            M(s > us, "s > us")
            expected_tc = TypeCode.Boolean
            M(s > i, "s > i")
            expected_tc = TypeCode.Boolean
            M(s > ui, "s > ui")
            expected_tc = TypeCode.Boolean
            M(s > l, "s > l")
            expected_tc = TypeCode.Boolean
            M(s > ul, "s > ul")
            expected_tc = TypeCode.Boolean
            M(s > dec, "s > dec")
            expected_tc = TypeCode.Boolean
            M(s > sng, "s > sng")
            expected_tc = TypeCode.Boolean
            M(s > dbl, "s > dbl")
#If Int16_ERRORS
            M(s > chr, "s > chr")
#End If
#If Int16_ERRORS
            M(s > str, "s > str")
#End If
#If Int16_ERRORS
            M(s > dt, "s > dt")
#End If
#If Int16_ERRORS
            M(s > dbnull, "s > dbnull")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Object
            M(s > obj, "s > obj")
#End If
#If UInt16_ERRORS
            M(us > bool, "us > bool")
#End If
            expected_tc = TypeCode.Boolean
            M(us > b, "us > b")
            expected_tc = TypeCode.Boolean
            M(us > sb, "us > sb")
            expected_tc = TypeCode.Boolean
            M(us > s, "us > s")
            expected_tc = TypeCode.Boolean
            M(us > us, "us > us")
            expected_tc = TypeCode.Boolean
            M(us > i, "us > i")
            expected_tc = TypeCode.Boolean
            M(us > ui, "us > ui")
            expected_tc = TypeCode.Boolean
            M(us > l, "us > l")
            expected_tc = TypeCode.Boolean
            M(us > ul, "us > ul")
            expected_tc = TypeCode.Boolean
            M(us > dec, "us > dec")
            expected_tc = TypeCode.Boolean
            M(us > sng, "us > sng")
            expected_tc = TypeCode.Boolean
            M(us > dbl, "us > dbl")
#If UInt16_ERRORS
            M(us > chr, "us > chr")
#End If
#If UInt16_ERRORS
            M(us > str, "us > str")
#End If
#If UInt16_ERRORS
            M(us > dt, "us > dt")
#End If
#If UInt16_ERRORS
            M(us > dbnull, "us > dbnull")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Object
            M(us > obj, "us > obj")
#End If
#If Int32_ERRORS
            M(i > bool, "i > bool")
#End If
            expected_tc = TypeCode.Boolean
            M(i > b, "i > b")
            expected_tc = TypeCode.Boolean
            M(i > sb, "i > sb")
            expected_tc = TypeCode.Boolean
            M(i > s, "i > s")
            expected_tc = TypeCode.Boolean
            M(i > us, "i > us")
            expected_tc = TypeCode.Boolean
            M(i > i, "i > i")
            expected_tc = TypeCode.Boolean
            M(i > ui, "i > ui")
            expected_tc = TypeCode.Boolean
            M(i > l, "i > l")
            expected_tc = TypeCode.Boolean
            M(i > ul, "i > ul")
            expected_tc = TypeCode.Boolean
            M(i > dec, "i > dec")
            expected_tc = TypeCode.Boolean
            M(i > sng, "i > sng")
            expected_tc = TypeCode.Boolean
            M(i > dbl, "i > dbl")
#If Int32_ERRORS
            M(i > chr, "i > chr")
#End If
#If Int32_ERRORS
            M(i > str, "i > str")
#End If
#If Int32_ERRORS
            M(i > dt, "i > dt")
#End If
#If Int32_ERRORS
            M(i > dbnull, "i > dbnull")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Object
            M(i > obj, "i > obj")
#End If
#If UInt32_ERRORS
            M(ui > bool, "ui > bool")
#End If
            expected_tc = TypeCode.Boolean
            M(ui > b, "ui > b")
            expected_tc = TypeCode.Boolean
            M(ui > sb, "ui > sb")
            expected_tc = TypeCode.Boolean
            M(ui > s, "ui > s")
            expected_tc = TypeCode.Boolean
            M(ui > us, "ui > us")
            expected_tc = TypeCode.Boolean
            M(ui > i, "ui > i")
            expected_tc = TypeCode.Boolean
            M(ui > ui, "ui > ui")
            expected_tc = TypeCode.Boolean
            M(ui > l, "ui > l")
            expected_tc = TypeCode.Boolean
            M(ui > ul, "ui > ul")
            expected_tc = TypeCode.Boolean
            M(ui > dec, "ui > dec")
            expected_tc = TypeCode.Boolean
            M(ui > sng, "ui > sng")
            expected_tc = TypeCode.Boolean
            M(ui > dbl, "ui > dbl")
#If UInt32_ERRORS
            M(ui > chr, "ui > chr")
#End If
#If UInt32_ERRORS
            M(ui > str, "ui > str")
#End If
#If UInt32_ERRORS
            M(ui > dt, "ui > dt")
#End If
#If UInt32_ERRORS
            M(ui > dbnull, "ui > dbnull")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Object
            M(ui > obj, "ui > obj")
#End If
#If Int64_ERRORS
            M(l > bool, "l > bool")
#End If
            expected_tc = TypeCode.Boolean
            M(l > b, "l > b")
            expected_tc = TypeCode.Boolean
            M(l > sb, "l > sb")
            expected_tc = TypeCode.Boolean
            M(l > s, "l > s")
            expected_tc = TypeCode.Boolean
            M(l > us, "l > us")
            expected_tc = TypeCode.Boolean
            M(l > i, "l > i")
            expected_tc = TypeCode.Boolean
            M(l > ui, "l > ui")
            expected_tc = TypeCode.Boolean
            M(l > l, "l > l")
            expected_tc = TypeCode.Boolean
            M(l > ul, "l > ul")
            expected_tc = TypeCode.Boolean
            M(l > dec, "l > dec")
            expected_tc = TypeCode.Boolean
            M(l > sng, "l > sng")
            expected_tc = TypeCode.Boolean
            M(l > dbl, "l > dbl")
#If Int64_ERRORS
            M(l > chr, "l > chr")
#End If
#If Int64_ERRORS
            M(l > str, "l > str")
#End If
#If Int64_ERRORS
            M(l > dt, "l > dt")
#End If
#If Int64_ERRORS
            M(l > dbnull, "l > dbnull")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Object
            M(l > obj, "l > obj")
#End If
#If UInt64_ERRORS
            M(ul > bool, "ul > bool")
#End If
            expected_tc = TypeCode.Boolean
            M(ul > b, "ul > b")
            expected_tc = TypeCode.Boolean
            M(ul > sb, "ul > sb")
            expected_tc = TypeCode.Boolean
            M(ul > s, "ul > s")
            expected_tc = TypeCode.Boolean
            M(ul > us, "ul > us")
            expected_tc = TypeCode.Boolean
            M(ul > i, "ul > i")
            expected_tc = TypeCode.Boolean
            M(ul > ui, "ul > ui")
            expected_tc = TypeCode.Boolean
            M(ul > l, "ul > l")
            expected_tc = TypeCode.Boolean
            M(ul > ul, "ul > ul")
            expected_tc = TypeCode.Boolean
            M(ul > dec, "ul > dec")
            expected_tc = TypeCode.Boolean
            M(ul > sng, "ul > sng")
            expected_tc = TypeCode.Boolean
            M(ul > dbl, "ul > dbl")
#If UInt64_ERRORS
            M(ul > chr, "ul > chr")
#End If
#If UInt64_ERRORS
            M(ul > str, "ul > str")
#End If
#If UInt64_ERRORS
            M(ul > dt, "ul > dt")
#End If
#If UInt64_ERRORS
            M(ul > dbnull, "ul > dbnull")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Object
            M(ul > obj, "ul > obj")
#End If
#If Decimal_ERRORS
            M(dec > bool, "dec > bool")
#End If
            expected_tc = TypeCode.Boolean
            M(dec > b, "dec > b")
            expected_tc = TypeCode.Boolean
            M(dec > sb, "dec > sb")
            expected_tc = TypeCode.Boolean
            M(dec > s, "dec > s")
            expected_tc = TypeCode.Boolean
            M(dec > us, "dec > us")
            expected_tc = TypeCode.Boolean
            M(dec > i, "dec > i")
            expected_tc = TypeCode.Boolean
            M(dec > ui, "dec > ui")
            expected_tc = TypeCode.Boolean
            M(dec > l, "dec > l")
            expected_tc = TypeCode.Boolean
            M(dec > ul, "dec > ul")
            expected_tc = TypeCode.Boolean
            M(dec > dec, "dec > dec")
            expected_tc = TypeCode.Boolean
            M(dec > sng, "dec > sng")
            expected_tc = TypeCode.Boolean
            M(dec > dbl, "dec > dbl")
#If Decimal_ERRORS
            M(dec > chr, "dec > chr")
#End If
#If Decimal_ERRORS
            M(dec > str, "dec > str")
#End If
#If Decimal_ERRORS
            M(dec > dt, "dec > dt")
#End If
#If Decimal_ERRORS
            M(dec > dbnull, "dec > dbnull")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Object
            M(dec > obj, "dec > obj")
#End If
#If Single_ERRORS
            M(sng > bool, "sng > bool")
#End If
            expected_tc = TypeCode.Boolean
            M(sng > b, "sng > b")
            expected_tc = TypeCode.Boolean
            M(sng > sb, "sng > sb")
            expected_tc = TypeCode.Boolean
            M(sng > s, "sng > s")
            expected_tc = TypeCode.Boolean
            M(sng > us, "sng > us")
            expected_tc = TypeCode.Boolean
            M(sng > i, "sng > i")
            expected_tc = TypeCode.Boolean
            M(sng > ui, "sng > ui")
            expected_tc = TypeCode.Boolean
            M(sng > l, "sng > l")
            expected_tc = TypeCode.Boolean
            M(sng > ul, "sng > ul")
            expected_tc = TypeCode.Boolean
            M(sng > dec, "sng > dec")
            expected_tc = TypeCode.Boolean
            M(sng > sng, "sng > sng")
            expected_tc = TypeCode.Boolean
            M(sng > dbl, "sng > dbl")
#If Single_ERRORS
            M(sng > chr, "sng > chr")
#End If
#If Single_ERRORS
            M(sng > str, "sng > str")
#End If
#If Single_ERRORS
            M(sng > dt, "sng > dt")
#End If
#If Single_ERRORS
            M(sng > dbnull, "sng > dbnull")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Object
            M(sng > obj, "sng > obj")
#End If
#If Double_ERRORS
            M(dbl > bool, "dbl > bool")
#End If
            expected_tc = TypeCode.Boolean
            M(dbl > b, "dbl > b")
            expected_tc = TypeCode.Boolean
            M(dbl > sb, "dbl > sb")
            expected_tc = TypeCode.Boolean
            M(dbl > s, "dbl > s")
            expected_tc = TypeCode.Boolean
            M(dbl > us, "dbl > us")
            expected_tc = TypeCode.Boolean
            M(dbl > i, "dbl > i")
            expected_tc = TypeCode.Boolean
            M(dbl > ui, "dbl > ui")
            expected_tc = TypeCode.Boolean
            M(dbl > l, "dbl > l")
            expected_tc = TypeCode.Boolean
            M(dbl > ul, "dbl > ul")
            expected_tc = TypeCode.Boolean
            M(dbl > dec, "dbl > dec")
            expected_tc = TypeCode.Boolean
            M(dbl > sng, "dbl > sng")
            expected_tc = TypeCode.Boolean
            M(dbl > dbl, "dbl > dbl")
#If Double_ERRORS
            M(dbl > chr, "dbl > chr")
#End If
#If Double_ERRORS
            M(dbl > str, "dbl > str")
#End If
#If Double_ERRORS
            M(dbl > dt, "dbl > dt")
#End If
#If Double_ERRORS
            M(dbl > dbnull, "dbl > dbnull")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Object
            M(dbl > obj, "dbl > obj")
#End If
#If Char_ERRORS
            M(chr > bool, "chr > bool")
#End If
#If Char_ERRORS
            M(chr > b, "chr > b")
#End If
#If Char_ERRORS
            M(chr > sb, "chr > sb")
#End If
#If Char_ERRORS
            M(chr > s, "chr > s")
#End If
#If Char_ERRORS
            M(chr > us, "chr > us")
#End If
#If Char_ERRORS
            M(chr > i, "chr > i")
#End If
#If Char_ERRORS
            M(chr > ui, "chr > ui")
#End If
#If Char_ERRORS
            M(chr > l, "chr > l")
#End If
#If Char_ERRORS
            M(chr > ul, "chr > ul")
#End If
#If Char_ERRORS
            M(chr > dec, "chr > dec")
#End If
#If Char_ERRORS
            M(chr > sng, "chr > sng")
#End If
#If Char_ERRORS
            M(chr > dbl, "chr > dbl")
#End If
#If Char_ERRORS
            M(chr > chr, "chr > chr")
#End If
#If Char_ERRORS
            M(chr > str, "chr > str")
#End If
#If Char_ERRORS
            M(chr > dt, "chr > dt")
#End If
#If Char_ERRORS
            M(chr > dbnull, "chr > dbnull")
#End If
#If Char_ERRORS
            M(chr > obj, "chr > obj")
#End If
#If String_ERRORS
            M(str > bool, "str > bool")
#End If
#If String_ERRORS
            M(str > b, "str > b")
#End If
#If String_ERRORS
            M(str > sb, "str > sb")
#End If
#If String_ERRORS
            M(str > s, "str > s")
#End If
#If String_ERRORS
            M(str > us, "str > us")
#End If
#If String_ERRORS
            M(str > i, "str > i")
#End If
#If String_ERRORS
            M(str > ui, "str > ui")
#End If
#If String_ERRORS
            M(str > l, "str > l")
#End If
#If String_ERRORS
            M(str > ul, "str > ul")
#End If
#If String_ERRORS
            M(str > dec, "str > dec")
#End If
#If String_ERRORS
            M(str > sng, "str > sng")
#End If
#If String_ERRORS
            M(str > dbl, "str > dbl")
#End If
#If String_ERRORS
            M(str > chr, "str > chr")
#End If
            expected_tc = TypeCode.Boolean
            M(str > str, "str > str")
#If String_ERRORS
            M(str > dt, "str > dt")
#End If
#If String_ERRORS
            M(str > dbnull, "str > dbnull")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Object
            M(str > obj, "str > obj")
#End If
#If DateTime_ERRORS
            M(dt > bool, "dt > bool")
#End If
#If DateTime_ERRORS
            M(dt > b, "dt > b")
#End If
#If DateTime_ERRORS
            M(dt > sb, "dt > sb")
#End If
#If DateTime_ERRORS
            M(dt > s, "dt > s")
#End If
#If DateTime_ERRORS
            M(dt > us, "dt > us")
#End If
#If DateTime_ERRORS
            M(dt > i, "dt > i")
#End If
#If DateTime_ERRORS
            M(dt > ui, "dt > ui")
#End If
#If DateTime_ERRORS
            M(dt > l, "dt > l")
#End If
#If DateTime_ERRORS
            M(dt > ul, "dt > ul")
#End If
#If DateTime_ERRORS
            M(dt > dec, "dt > dec")
#End If
#If DateTime_ERRORS
            M(dt > sng, "dt > sng")
#End If
#If DateTime_ERRORS
            M(dt > dbl, "dt > dbl")
#End If
#If DateTime_ERRORS
            M(dt > chr, "dt > chr")
#End If
#If DateTime_ERRORS
            M(dt > str, "dt > str")
#End If
            expected_tc = TypeCode.Boolean
            M(dt > dt, "dt > dt")
#If DateTime_ERRORS
            M(dt > dbnull, "dt > dbnull")
#End If
#If Not STRICT Or DateTime_ERRORS
            expected_tc = TypeCode.Object
            M(dt > obj, "dt > obj")
#End If
#If DBNull_ERRORS
            M(dbnull > bool, "dbnull > bool")
#End If
#If DBNull_ERRORS
            M(dbnull > b, "dbnull > b")
#End If
#If DBNull_ERRORS
            M(dbnull > sb, "dbnull > sb")
#End If
#If DBNull_ERRORS
            M(dbnull > s, "dbnull > s")
#End If
#If DBNull_ERRORS
            M(dbnull > us, "dbnull > us")
#End If
#If DBNull_ERRORS
            M(dbnull > i, "dbnull > i")
#End If
#If DBNull_ERRORS
            M(dbnull > ui, "dbnull > ui")
#End If
#If DBNull_ERRORS
            M(dbnull > l, "dbnull > l")
#End If
#If DBNull_ERRORS
            M(dbnull > ul, "dbnull > ul")
#End If
#If DBNull_ERRORS
            M(dbnull > dec, "dbnull > dec")
#End If
#If DBNull_ERRORS
            M(dbnull > sng, "dbnull > sng")
#End If
#If DBNull_ERRORS
            M(dbnull > dbl, "dbnull > dbl")
#End If
#If DBNull_ERRORS
            M(dbnull > chr, "dbnull > chr")
#End If
#If DBNull_ERRORS
            M(dbnull > str, "dbnull > str")
#End If
#If DBNull_ERRORS
            M(dbnull > dt, "dbnull > dt")
#End If
#If DBNull_ERRORS
            M(dbnull > dbnull, "dbnull > dbnull")
#End If
#If DBNull_ERRORS
            M(dbnull > obj, "dbnull > obj")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj > bool, "obj > bool")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj > b, "obj > b")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj > sb, "obj > sb")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj > s, "obj > s")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj > us, "obj > us")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj > i, "obj > i")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj > ui, "obj > ui")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj > l, "obj > l")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj > ul, "obj > ul")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj > dec, "obj > dec")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj > sng, "obj > sng")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj > dbl, "obj > dbl")
#End If
#If Object_ERRORS
            M(obj > chr, "obj > chr")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj > str, "obj > str")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj > dt, "obj > dt")
#End If
#If Object_ERRORS
            M(obj > dbnull, "obj > dbnull")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj > obj, "obj > obj")
#End If
        If failures > 0 Then Return 1
    Catch ex As Exception
        Console.WriteLine ("Exception: {0}", ex)
        Return 2
    End Try
    Return 0
    End Function
End Class
