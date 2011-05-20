Class BinaryOperatorOr
    Inherits IntrinsicOperatorTests
    Shared Function Main As Integer
        Try
#If Boolean_ERRORS
            M(bool Or bool, "bool Or bool")
#End If
#If Boolean_ERRORS
            M(bool Or b, "bool Or b")
#End If
#If Boolean_ERRORS
            M(bool Or sb, "bool Or sb")
#End If
#If Boolean_ERRORS
            M(bool Or s, "bool Or s")
#End If
#If Boolean_ERRORS
            M(bool Or us, "bool Or us")
#End If
#If Boolean_ERRORS
            M(bool Or i, "bool Or i")
#End If
#If Boolean_ERRORS
            M(bool Or ui, "bool Or ui")
#End If
#If Boolean_ERRORS
            M(bool Or l, "bool Or l")
#End If
#If Boolean_ERRORS
            M(bool Or ul, "bool Or ul")
#End If
#If Boolean_ERRORS
            M(bool Or dec, "bool Or dec")
#End If
#If Boolean_ERRORS
            M(bool Or sng, "bool Or sng")
#End If
#If Boolean_ERRORS
            M(bool Or dbl, "bool Or dbl")
#End If
#If Boolean_ERRORS
            M(bool Or chr, "bool Or chr")
#End If
#If Boolean_ERRORS
            M(bool Or str, "bool Or str")
#End If
#If Boolean_ERRORS
            M(bool Or dt, "bool Or dt")
#End If
#If Boolean_ERRORS
            M(bool Or dbnull, "bool Or dbnull")
#End If
#If Boolean_ERRORS
            M(bool Or obj, "bool Or obj")
#End If
#If Byte_ERRORS
            M(b Or bool, "b Or bool")
#End If
#If Byte_ERRORS
            M(b Or b, "b Or b")
#End If
#If Byte_ERRORS
            M(b Or sb, "b Or sb")
#End If
#If Byte_ERRORS
            M(b Or s, "b Or s")
#End If
            expected_tc = TypeCode.UInt16
            M(b Or us, "b Or us")
#If Byte_ERRORS
            M(b Or i, "b Or i")
#End If
#If Byte_ERRORS
            M(b Or ui, "b Or ui")
#End If
#If Byte_ERRORS
            M(b Or l, "b Or l")
#End If
#If Byte_ERRORS
            M(b Or ul, "b Or ul")
#End If
#If Byte_ERRORS
            M(b Or dec, "b Or dec")
#End If
#If Byte_ERRORS
            M(b Or sng, "b Or sng")
#End If
#If Byte_ERRORS
            M(b Or dbl, "b Or dbl")
#End If
#If Byte_ERRORS
            M(b Or chr, "b Or chr")
#End If
#If Byte_ERRORS
            M(b Or str, "b Or str")
#End If
#If Byte_ERRORS
            M(b Or dt, "b Or dt")
#End If
#If Byte_ERRORS
            M(b Or dbnull, "b Or dbnull")
#End If
#If Byte_ERRORS
            M(b Or obj, "b Or obj")
#End If
#If SByte_ERRORS
            M(sb Or bool, "sb Or bool")
#End If
#If SByte_ERRORS
            M(sb Or b, "sb Or b")
#End If
#If SByte_ERRORS
            M(sb Or sb, "sb Or sb")
#End If
#If SByte_ERRORS
            M(sb Or s, "sb Or s")
#End If
#If SByte_ERRORS
            M(sb Or us, "sb Or us")
#End If
#If SByte_ERRORS
            M(sb Or i, "sb Or i")
#End If
#If SByte_ERRORS
            M(sb Or ui, "sb Or ui")
#End If
#If SByte_ERRORS
            M(sb Or l, "sb Or l")
#End If
#If SByte_ERRORS
            M(sb Or ul, "sb Or ul")
#End If
#If SByte_ERRORS
            M(sb Or dec, "sb Or dec")
#End If
#If SByte_ERRORS
            M(sb Or sng, "sb Or sng")
#End If
#If SByte_ERRORS
            M(sb Or dbl, "sb Or dbl")
#End If
#If SByte_ERRORS
            M(sb Or chr, "sb Or chr")
#End If
#If SByte_ERRORS
            M(sb Or str, "sb Or str")
#End If
#If SByte_ERRORS
            M(sb Or dt, "sb Or dt")
#End If
#If SByte_ERRORS
            M(sb Or dbnull, "sb Or dbnull")
#End If
#If SByte_ERRORS
            M(sb Or obj, "sb Or obj")
#End If
#If Int16_ERRORS
            M(s Or bool, "s Or bool")
#End If
#If Int16_ERRORS
            M(s Or b, "s Or b")
#End If
#If Int16_ERRORS
            M(s Or sb, "s Or sb")
#End If
#If Int16_ERRORS
            M(s Or s, "s Or s")
#End If
#If Int16_ERRORS
            M(s Or us, "s Or us")
#End If
#If Int16_ERRORS
            M(s Or i, "s Or i")
#End If
#If Int16_ERRORS
            M(s Or ui, "s Or ui")
#End If
#If Int16_ERRORS
            M(s Or l, "s Or l")
#End If
#If Int16_ERRORS
            M(s Or ul, "s Or ul")
#End If
#If Int16_ERRORS
            M(s Or dec, "s Or dec")
#End If
#If Int16_ERRORS
            M(s Or sng, "s Or sng")
#End If
#If Int16_ERRORS
            M(s Or dbl, "s Or dbl")
#End If
#If Int16_ERRORS
            M(s Or chr, "s Or chr")
#End If
#If Int16_ERRORS
            M(s Or str, "s Or str")
#End If
#If Int16_ERRORS
            M(s Or dt, "s Or dt")
#End If
#If Int16_ERRORS
            M(s Or dbnull, "s Or dbnull")
#End If
#If Int16_ERRORS
            M(s Or obj, "s Or obj")
#End If
#If UInt16_ERRORS
            M(us Or bool, "us Or bool")
#End If
            expected_tc = TypeCode.UInt16
            M(us Or b, "us Or b")
#If UInt16_ERRORS
            M(us Or sb, "us Or sb")
#End If
#If UInt16_ERRORS
            M(us Or s, "us Or s")
#End If
            expected_tc = TypeCode.UInt16
            M(us Or us, "us Or us")
#If UInt16_ERRORS
            M(us Or i, "us Or i")
#End If
#If UInt16_ERRORS
            M(us Or ui, "us Or ui")
#End If
#If UInt16_ERRORS
            M(us Or l, "us Or l")
#End If
#If UInt16_ERRORS
            M(us Or ul, "us Or ul")
#End If
#If UInt16_ERRORS
            M(us Or dec, "us Or dec")
#End If
#If UInt16_ERRORS
            M(us Or sng, "us Or sng")
#End If
#If UInt16_ERRORS
            M(us Or dbl, "us Or dbl")
#End If
#If UInt16_ERRORS
            M(us Or chr, "us Or chr")
#End If
#If UInt16_ERRORS
            M(us Or str, "us Or str")
#End If
#If UInt16_ERRORS
            M(us Or dt, "us Or dt")
#End If
#If UInt16_ERRORS
            M(us Or dbnull, "us Or dbnull")
#End If
#If UInt16_ERRORS
            M(us Or obj, "us Or obj")
#End If
#If Int32_ERRORS
            M(i Or bool, "i Or bool")
#End If
#If Int32_ERRORS
            M(i Or b, "i Or b")
#End If
#If Int32_ERRORS
            M(i Or sb, "i Or sb")
#End If
#If Int32_ERRORS
            M(i Or s, "i Or s")
#End If
#If Int32_ERRORS
            M(i Or us, "i Or us")
#End If
#If Int32_ERRORS
            M(i Or i, "i Or i")
#End If
#If Int32_ERRORS
            M(i Or ui, "i Or ui")
#End If
#If Int32_ERRORS
            M(i Or l, "i Or l")
#End If
#If Int32_ERRORS
            M(i Or ul, "i Or ul")
#End If
#If Int32_ERRORS
            M(i Or dec, "i Or dec")
#End If
#If Int32_ERRORS
            M(i Or sng, "i Or sng")
#End If
#If Int32_ERRORS
            M(i Or dbl, "i Or dbl")
#End If
#If Int32_ERRORS
            M(i Or chr, "i Or chr")
#End If
#If Int32_ERRORS
            M(i Or str, "i Or str")
#End If
#If Int32_ERRORS
            M(i Or dt, "i Or dt")
#End If
#If Int32_ERRORS
            M(i Or dbnull, "i Or dbnull")
#End If
#If Int32_ERRORS
            M(i Or obj, "i Or obj")
#End If
#If UInt32_ERRORS
            M(ui Or bool, "ui Or bool")
#End If
#If UInt32_ERRORS
            M(ui Or b, "ui Or b")
#End If
#If UInt32_ERRORS
            M(ui Or sb, "ui Or sb")
#End If
#If UInt32_ERRORS
            M(ui Or s, "ui Or s")
#End If
#If UInt32_ERRORS
            M(ui Or us, "ui Or us")
#End If
#If UInt32_ERRORS
            M(ui Or i, "ui Or i")
#End If
#If UInt32_ERRORS
            M(ui Or ui, "ui Or ui")
#End If
#If UInt32_ERRORS
            M(ui Or l, "ui Or l")
#End If
#If UInt32_ERRORS
            M(ui Or ul, "ui Or ul")
#End If
#If UInt32_ERRORS
            M(ui Or dec, "ui Or dec")
#End If
#If UInt32_ERRORS
            M(ui Or sng, "ui Or sng")
#End If
#If UInt32_ERRORS
            M(ui Or dbl, "ui Or dbl")
#End If
#If UInt32_ERRORS
            M(ui Or chr, "ui Or chr")
#End If
#If UInt32_ERRORS
            M(ui Or str, "ui Or str")
#End If
#If UInt32_ERRORS
            M(ui Or dt, "ui Or dt")
#End If
#If UInt32_ERRORS
            M(ui Or dbnull, "ui Or dbnull")
#End If
#If UInt32_ERRORS
            M(ui Or obj, "ui Or obj")
#End If
#If Int64_ERRORS
            M(l Or bool, "l Or bool")
#End If
#If Int64_ERRORS
            M(l Or b, "l Or b")
#End If
#If Int64_ERRORS
            M(l Or sb, "l Or sb")
#End If
#If Int64_ERRORS
            M(l Or s, "l Or s")
#End If
#If Int64_ERRORS
            M(l Or us, "l Or us")
#End If
#If Int64_ERRORS
            M(l Or i, "l Or i")
#End If
#If Int64_ERRORS
            M(l Or ui, "l Or ui")
#End If
#If Int64_ERRORS
            M(l Or l, "l Or l")
#End If
#If Int64_ERRORS
            M(l Or ul, "l Or ul")
#End If
#If Int64_ERRORS
            M(l Or dec, "l Or dec")
#End If
#If Int64_ERRORS
            M(l Or sng, "l Or sng")
#End If
#If Int64_ERRORS
            M(l Or dbl, "l Or dbl")
#End If
#If Int64_ERRORS
            M(l Or chr, "l Or chr")
#End If
#If Int64_ERRORS
            M(l Or str, "l Or str")
#End If
#If Int64_ERRORS
            M(l Or dt, "l Or dt")
#End If
#If Int64_ERRORS
            M(l Or dbnull, "l Or dbnull")
#End If
#If Int64_ERRORS
            M(l Or obj, "l Or obj")
#End If
#If UInt64_ERRORS
            M(ul Or bool, "ul Or bool")
#End If
#If UInt64_ERRORS
            M(ul Or b, "ul Or b")
#End If
#If UInt64_ERRORS
            M(ul Or sb, "ul Or sb")
#End If
#If UInt64_ERRORS
            M(ul Or s, "ul Or s")
#End If
#If UInt64_ERRORS
            M(ul Or us, "ul Or us")
#End If
#If UInt64_ERRORS
            M(ul Or i, "ul Or i")
#End If
#If UInt64_ERRORS
            M(ul Or ui, "ul Or ui")
#End If
#If UInt64_ERRORS
            M(ul Or l, "ul Or l")
#End If
#If UInt64_ERRORS
            M(ul Or ul, "ul Or ul")
#End If
#If UInt64_ERRORS
            M(ul Or dec, "ul Or dec")
#End If
#If UInt64_ERRORS
            M(ul Or sng, "ul Or sng")
#End If
#If UInt64_ERRORS
            M(ul Or dbl, "ul Or dbl")
#End If
#If UInt64_ERRORS
            M(ul Or chr, "ul Or chr")
#End If
#If UInt64_ERRORS
            M(ul Or str, "ul Or str")
#End If
#If UInt64_ERRORS
            M(ul Or dt, "ul Or dt")
#End If
#If UInt64_ERRORS
            M(ul Or dbnull, "ul Or dbnull")
#End If
#If UInt64_ERRORS
            M(ul Or obj, "ul Or obj")
#End If
#If Decimal_ERRORS
            M(dec Or bool, "dec Or bool")
#End If
#If Decimal_ERRORS
            M(dec Or b, "dec Or b")
#End If
#If Decimal_ERRORS
            M(dec Or sb, "dec Or sb")
#End If
#If Decimal_ERRORS
            M(dec Or s, "dec Or s")
#End If
#If Decimal_ERRORS
            M(dec Or us, "dec Or us")
#End If
#If Decimal_ERRORS
            M(dec Or i, "dec Or i")
#End If
#If Decimal_ERRORS
            M(dec Or ui, "dec Or ui")
#End If
#If Decimal_ERRORS
            M(dec Or l, "dec Or l")
#End If
#If Decimal_ERRORS
            M(dec Or ul, "dec Or ul")
#End If
#If Decimal_ERRORS
            M(dec Or dec, "dec Or dec")
#End If
#If Decimal_ERRORS
            M(dec Or sng, "dec Or sng")
#End If
#If Decimal_ERRORS
            M(dec Or dbl, "dec Or dbl")
#End If
#If Decimal_ERRORS
            M(dec Or chr, "dec Or chr")
#End If
#If Decimal_ERRORS
            M(dec Or str, "dec Or str")
#End If
#If Decimal_ERRORS
            M(dec Or dt, "dec Or dt")
#End If
#If Decimal_ERRORS
            M(dec Or dbnull, "dec Or dbnull")
#End If
#If Decimal_ERRORS
            M(dec Or obj, "dec Or obj")
#End If
#If Single_ERRORS
            M(sng Or bool, "sng Or bool")
#End If
#If Single_ERRORS
            M(sng Or b, "sng Or b")
#End If
#If Single_ERRORS
            M(sng Or sb, "sng Or sb")
#End If
#If Single_ERRORS
            M(sng Or s, "sng Or s")
#End If
#If Single_ERRORS
            M(sng Or us, "sng Or us")
#End If
#If Single_ERRORS
            M(sng Or i, "sng Or i")
#End If
#If Single_ERRORS
            M(sng Or ui, "sng Or ui")
#End If
#If Single_ERRORS
            M(sng Or l, "sng Or l")
#End If
#If Single_ERRORS
            M(sng Or ul, "sng Or ul")
#End If
#If Single_ERRORS
            M(sng Or dec, "sng Or dec")
#End If
#If Single_ERRORS
            M(sng Or sng, "sng Or sng")
#End If
#If Single_ERRORS
            M(sng Or dbl, "sng Or dbl")
#End If
#If Single_ERRORS
            M(sng Or chr, "sng Or chr")
#End If
#If Single_ERRORS
            M(sng Or str, "sng Or str")
#End If
#If Single_ERRORS
            M(sng Or dt, "sng Or dt")
#End If
#If Single_ERRORS
            M(sng Or dbnull, "sng Or dbnull")
#End If
#If Single_ERRORS
            M(sng Or obj, "sng Or obj")
#End If
#If Double_ERRORS
            M(dbl Or bool, "dbl Or bool")
#End If
#If Double_ERRORS
            M(dbl Or b, "dbl Or b")
#End If
#If Double_ERRORS
            M(dbl Or sb, "dbl Or sb")
#End If
#If Double_ERRORS
            M(dbl Or s, "dbl Or s")
#End If
#If Double_ERRORS
            M(dbl Or us, "dbl Or us")
#End If
#If Double_ERRORS
            M(dbl Or i, "dbl Or i")
#End If
#If Double_ERRORS
            M(dbl Or ui, "dbl Or ui")
#End If
#If Double_ERRORS
            M(dbl Or l, "dbl Or l")
#End If
#If Double_ERRORS
            M(dbl Or ul, "dbl Or ul")
#End If
#If Double_ERRORS
            M(dbl Or dec, "dbl Or dec")
#End If
#If Double_ERRORS
            M(dbl Or sng, "dbl Or sng")
#End If
#If Double_ERRORS
            M(dbl Or dbl, "dbl Or dbl")
#End If
#If Double_ERRORS
            M(dbl Or chr, "dbl Or chr")
#End If
#If Double_ERRORS
            M(dbl Or str, "dbl Or str")
#End If
#If Double_ERRORS
            M(dbl Or dt, "dbl Or dt")
#End If
#If Double_ERRORS
            M(dbl Or dbnull, "dbl Or dbnull")
#End If
#If Double_ERRORS
            M(dbl Or obj, "dbl Or obj")
#End If
#If Char_ERRORS
            M(chr Or bool, "chr Or bool")
#End If
#If Char_ERRORS
            M(chr Or b, "chr Or b")
#End If
#If Char_ERRORS
            M(chr Or sb, "chr Or sb")
#End If
#If Char_ERRORS
            M(chr Or s, "chr Or s")
#End If
#If Char_ERRORS
            M(chr Or us, "chr Or us")
#End If
#If Char_ERRORS
            M(chr Or i, "chr Or i")
#End If
#If Char_ERRORS
            M(chr Or ui, "chr Or ui")
#End If
#If Char_ERRORS
            M(chr Or l, "chr Or l")
#End If
#If Char_ERRORS
            M(chr Or ul, "chr Or ul")
#End If
#If Char_ERRORS
            M(chr Or dec, "chr Or dec")
#End If
#If Char_ERRORS
            M(chr Or sng, "chr Or sng")
#End If
#If Char_ERRORS
            M(chr Or dbl, "chr Or dbl")
#End If
#If Char_ERRORS
            M(chr Or chr, "chr Or chr")
#End If
#If Char_ERRORS
            M(chr Or str, "chr Or str")
#End If
#If Char_ERRORS
            M(chr Or dt, "chr Or dt")
#End If
#If Char_ERRORS
            M(chr Or dbnull, "chr Or dbnull")
#End If
#If Char_ERRORS
            M(chr Or obj, "chr Or obj")
#End If
#If String_ERRORS
            M(str Or bool, "str Or bool")
#End If
#If String_ERRORS
            M(str Or b, "str Or b")
#End If
#If String_ERRORS
            M(str Or sb, "str Or sb")
#End If
#If String_ERRORS
            M(str Or s, "str Or s")
#End If
#If String_ERRORS
            M(str Or us, "str Or us")
#End If
#If String_ERRORS
            M(str Or i, "str Or i")
#End If
#If String_ERRORS
            M(str Or ui, "str Or ui")
#End If
#If String_ERRORS
            M(str Or l, "str Or l")
#End If
#If String_ERRORS
            M(str Or ul, "str Or ul")
#End If
#If String_ERRORS
            M(str Or dec, "str Or dec")
#End If
#If String_ERRORS
            M(str Or sng, "str Or sng")
#End If
#If String_ERRORS
            M(str Or dbl, "str Or dbl")
#End If
#If String_ERRORS
            M(str Or chr, "str Or chr")
#End If
#If String_ERRORS
            M(str Or str, "str Or str")
#End If
#If String_ERRORS
            M(str Or dt, "str Or dt")
#End If
#If String_ERRORS
            M(str Or dbnull, "str Or dbnull")
#End If
#If String_ERRORS
            M(str Or obj, "str Or obj")
#End If
#If DateTime_ERRORS
            M(dt Or bool, "dt Or bool")
#End If
#If DateTime_ERRORS
            M(dt Or b, "dt Or b")
#End If
#If DateTime_ERRORS
            M(dt Or sb, "dt Or sb")
#End If
#If DateTime_ERRORS
            M(dt Or s, "dt Or s")
#End If
#If DateTime_ERRORS
            M(dt Or us, "dt Or us")
#End If
#If DateTime_ERRORS
            M(dt Or i, "dt Or i")
#End If
#If DateTime_ERRORS
            M(dt Or ui, "dt Or ui")
#End If
#If DateTime_ERRORS
            M(dt Or l, "dt Or l")
#End If
#If DateTime_ERRORS
            M(dt Or ul, "dt Or ul")
#End If
#If DateTime_ERRORS
            M(dt Or dec, "dt Or dec")
#End If
#If DateTime_ERRORS
            M(dt Or sng, "dt Or sng")
#End If
#If DateTime_ERRORS
            M(dt Or dbl, "dt Or dbl")
#End If
#If DateTime_ERRORS
            M(dt Or chr, "dt Or chr")
#End If
#If DateTime_ERRORS
            M(dt Or str, "dt Or str")
#End If
#If DateTime_ERRORS
            M(dt Or dt, "dt Or dt")
#End If
#If DateTime_ERRORS
            M(dt Or dbnull, "dt Or dbnull")
#End If
#If DateTime_ERRORS
            M(dt Or obj, "dt Or obj")
#End If
#If DBNull_ERRORS
            M(dbnull Or bool, "dbnull Or bool")
#End If
#If DBNull_ERRORS
            M(dbnull Or b, "dbnull Or b")
#End If
#If DBNull_ERRORS
            M(dbnull Or sb, "dbnull Or sb")
#End If
#If DBNull_ERRORS
            M(dbnull Or s, "dbnull Or s")
#End If
#If DBNull_ERRORS
            M(dbnull Or us, "dbnull Or us")
#End If
#If DBNull_ERRORS
            M(dbnull Or i, "dbnull Or i")
#End If
#If DBNull_ERRORS
            M(dbnull Or ui, "dbnull Or ui")
#End If
#If DBNull_ERRORS
            M(dbnull Or l, "dbnull Or l")
#End If
#If DBNull_ERRORS
            M(dbnull Or ul, "dbnull Or ul")
#End If
#If DBNull_ERRORS
            M(dbnull Or dec, "dbnull Or dec")
#End If
#If DBNull_ERRORS
            M(dbnull Or sng, "dbnull Or sng")
#End If
#If DBNull_ERRORS
            M(dbnull Or dbl, "dbnull Or dbl")
#End If
#If DBNull_ERRORS
            M(dbnull Or chr, "dbnull Or chr")
#End If
#If DBNull_ERRORS
            M(dbnull Or str, "dbnull Or str")
#End If
#If DBNull_ERRORS
            M(dbnull Or dt, "dbnull Or dt")
#End If
#If DBNull_ERRORS
            M(dbnull Or dbnull, "dbnull Or dbnull")
#End If
#If DBNull_ERRORS
            M(dbnull Or obj, "dbnull Or obj")
#End If
#If Object_ERRORS
            M(obj Or bool, "obj Or bool")
#End If
#If Object_ERRORS
            M(obj Or b, "obj Or b")
#End If
#If Object_ERRORS
            M(obj Or sb, "obj Or sb")
#End If
#If Object_ERRORS
            M(obj Or s, "obj Or s")
#End If
#If Object_ERRORS
            M(obj Or us, "obj Or us")
#End If
#If Object_ERRORS
            M(obj Or i, "obj Or i")
#End If
#If Object_ERRORS
            M(obj Or ui, "obj Or ui")
#End If
#If Object_ERRORS
            M(obj Or l, "obj Or l")
#End If
#If Object_ERRORS
            M(obj Or ul, "obj Or ul")
#End If
#If Object_ERRORS
            M(obj Or dec, "obj Or dec")
#End If
#If Object_ERRORS
            M(obj Or sng, "obj Or sng")
#End If
#If Object_ERRORS
            M(obj Or dbl, "obj Or dbl")
#End If
#If Object_ERRORS
            M(obj Or chr, "obj Or chr")
#End If
#If Object_ERRORS
            M(obj Or str, "obj Or str")
#End If
#If Object_ERRORS
            M(obj Or dt, "obj Or dt")
#End If
#If Object_ERRORS
            M(obj Or dbnull, "obj Or dbnull")
#End If
#If Object_ERRORS
            M(obj Or obj, "obj Or obj")
#End If
        If failures > 0 Then Return 1
    Catch ex As Exception
        Console.WriteLine ("Exception: {0}", ex)
        Return 2
    End Try
    Return 0
    End Function
End Class
