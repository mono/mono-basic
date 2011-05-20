Class BinaryOperatorXor
    Inherits IntrinsicOperatorTests
    Shared Function Main As Integer
        Try
#If Boolean_ERRORS
            M(bool Xor bool, "bool Xor bool")
#End If
#If Boolean_ERRORS
            M(bool Xor b, "bool Xor b")
#End If
#If Boolean_ERRORS
            M(bool Xor sb, "bool Xor sb")
#End If
#If Boolean_ERRORS
            M(bool Xor s, "bool Xor s")
#End If
#If Boolean_ERRORS
            M(bool Xor us, "bool Xor us")
#End If
#If Boolean_ERRORS
            M(bool Xor i, "bool Xor i")
#End If
#If Boolean_ERRORS
            M(bool Xor ui, "bool Xor ui")
#End If
#If Boolean_ERRORS
            M(bool Xor l, "bool Xor l")
#End If
#If Boolean_ERRORS
            M(bool Xor ul, "bool Xor ul")
#End If
#If Boolean_ERRORS
            M(bool Xor dec, "bool Xor dec")
#End If
#If Boolean_ERRORS
            M(bool Xor sng, "bool Xor sng")
#End If
#If Boolean_ERRORS
            M(bool Xor dbl, "bool Xor dbl")
#End If
#If Boolean_ERRORS
            M(bool Xor chr, "bool Xor chr")
#End If
#If Boolean_ERRORS
            M(bool Xor str, "bool Xor str")
#End If
#If Boolean_ERRORS
            M(bool Xor dt, "bool Xor dt")
#End If
#If Boolean_ERRORS
            M(bool Xor dbnull, "bool Xor dbnull")
#End If
#If Boolean_ERRORS
            M(bool Xor obj, "bool Xor obj")
#End If
#If Byte_ERRORS
            M(b Xor bool, "b Xor bool")
#End If
#If Byte_ERRORS
            M(b Xor b, "b Xor b")
#End If
#If Byte_ERRORS
            M(b Xor sb, "b Xor sb")
#End If
#If Byte_ERRORS
            M(b Xor s, "b Xor s")
#End If
            expected_tc = TypeCode.UInt16
            M(b Xor us, "b Xor us")
#If Byte_ERRORS
            M(b Xor i, "b Xor i")
#End If
#If Byte_ERRORS
            M(b Xor ui, "b Xor ui")
#End If
#If Byte_ERRORS
            M(b Xor l, "b Xor l")
#End If
#If Byte_ERRORS
            M(b Xor ul, "b Xor ul")
#End If
#If Byte_ERRORS
            M(b Xor dec, "b Xor dec")
#End If
#If Byte_ERRORS
            M(b Xor sng, "b Xor sng")
#End If
#If Byte_ERRORS
            M(b Xor dbl, "b Xor dbl")
#End If
#If Byte_ERRORS
            M(b Xor chr, "b Xor chr")
#End If
#If Byte_ERRORS
            M(b Xor str, "b Xor str")
#End If
#If Byte_ERRORS
            M(b Xor dt, "b Xor dt")
#End If
#If Byte_ERRORS
            M(b Xor dbnull, "b Xor dbnull")
#End If
#If Byte_ERRORS
            M(b Xor obj, "b Xor obj")
#End If
#If SByte_ERRORS
            M(sb Xor bool, "sb Xor bool")
#End If
#If SByte_ERRORS
            M(sb Xor b, "sb Xor b")
#End If
#If SByte_ERRORS
            M(sb Xor sb, "sb Xor sb")
#End If
#If SByte_ERRORS
            M(sb Xor s, "sb Xor s")
#End If
#If SByte_ERRORS
            M(sb Xor us, "sb Xor us")
#End If
#If SByte_ERRORS
            M(sb Xor i, "sb Xor i")
#End If
#If SByte_ERRORS
            M(sb Xor ui, "sb Xor ui")
#End If
#If SByte_ERRORS
            M(sb Xor l, "sb Xor l")
#End If
#If SByte_ERRORS
            M(sb Xor ul, "sb Xor ul")
#End If
#If SByte_ERRORS
            M(sb Xor dec, "sb Xor dec")
#End If
#If SByte_ERRORS
            M(sb Xor sng, "sb Xor sng")
#End If
#If SByte_ERRORS
            M(sb Xor dbl, "sb Xor dbl")
#End If
#If SByte_ERRORS
            M(sb Xor chr, "sb Xor chr")
#End If
#If SByte_ERRORS
            M(sb Xor str, "sb Xor str")
#End If
#If SByte_ERRORS
            M(sb Xor dt, "sb Xor dt")
#End If
#If SByte_ERRORS
            M(sb Xor dbnull, "sb Xor dbnull")
#End If
#If SByte_ERRORS
            M(sb Xor obj, "sb Xor obj")
#End If
#If Int16_ERRORS
            M(s Xor bool, "s Xor bool")
#End If
#If Int16_ERRORS
            M(s Xor b, "s Xor b")
#End If
#If Int16_ERRORS
            M(s Xor sb, "s Xor sb")
#End If
#If Int16_ERRORS
            M(s Xor s, "s Xor s")
#End If
#If Int16_ERRORS
            M(s Xor us, "s Xor us")
#End If
#If Int16_ERRORS
            M(s Xor i, "s Xor i")
#End If
#If Int16_ERRORS
            M(s Xor ui, "s Xor ui")
#End If
#If Int16_ERRORS
            M(s Xor l, "s Xor l")
#End If
#If Int16_ERRORS
            M(s Xor ul, "s Xor ul")
#End If
#If Int16_ERRORS
            M(s Xor dec, "s Xor dec")
#End If
#If Int16_ERRORS
            M(s Xor sng, "s Xor sng")
#End If
#If Int16_ERRORS
            M(s Xor dbl, "s Xor dbl")
#End If
#If Int16_ERRORS
            M(s Xor chr, "s Xor chr")
#End If
#If Int16_ERRORS
            M(s Xor str, "s Xor str")
#End If
#If Int16_ERRORS
            M(s Xor dt, "s Xor dt")
#End If
#If Int16_ERRORS
            M(s Xor dbnull, "s Xor dbnull")
#End If
#If Int16_ERRORS
            M(s Xor obj, "s Xor obj")
#End If
#If UInt16_ERRORS
            M(us Xor bool, "us Xor bool")
#End If
            expected_tc = TypeCode.UInt16
            M(us Xor b, "us Xor b")
#If UInt16_ERRORS
            M(us Xor sb, "us Xor sb")
#End If
#If UInt16_ERRORS
            M(us Xor s, "us Xor s")
#End If
            expected_tc = TypeCode.UInt16
            M(us Xor us, "us Xor us")
#If UInt16_ERRORS
            M(us Xor i, "us Xor i")
#End If
#If UInt16_ERRORS
            M(us Xor ui, "us Xor ui")
#End If
#If UInt16_ERRORS
            M(us Xor l, "us Xor l")
#End If
#If UInt16_ERRORS
            M(us Xor ul, "us Xor ul")
#End If
#If UInt16_ERRORS
            M(us Xor dec, "us Xor dec")
#End If
#If UInt16_ERRORS
            M(us Xor sng, "us Xor sng")
#End If
#If UInt16_ERRORS
            M(us Xor dbl, "us Xor dbl")
#End If
#If UInt16_ERRORS
            M(us Xor chr, "us Xor chr")
#End If
#If UInt16_ERRORS
            M(us Xor str, "us Xor str")
#End If
#If UInt16_ERRORS
            M(us Xor dt, "us Xor dt")
#End If
#If UInt16_ERRORS
            M(us Xor dbnull, "us Xor dbnull")
#End If
#If UInt16_ERRORS
            M(us Xor obj, "us Xor obj")
#End If
#If Int32_ERRORS
            M(i Xor bool, "i Xor bool")
#End If
#If Int32_ERRORS
            M(i Xor b, "i Xor b")
#End If
#If Int32_ERRORS
            M(i Xor sb, "i Xor sb")
#End If
#If Int32_ERRORS
            M(i Xor s, "i Xor s")
#End If
#If Int32_ERRORS
            M(i Xor us, "i Xor us")
#End If
#If Int32_ERRORS
            M(i Xor i, "i Xor i")
#End If
#If Int32_ERRORS
            M(i Xor ui, "i Xor ui")
#End If
#If Int32_ERRORS
            M(i Xor l, "i Xor l")
#End If
#If Int32_ERRORS
            M(i Xor ul, "i Xor ul")
#End If
#If Int32_ERRORS
            M(i Xor dec, "i Xor dec")
#End If
#If Int32_ERRORS
            M(i Xor sng, "i Xor sng")
#End If
#If Int32_ERRORS
            M(i Xor dbl, "i Xor dbl")
#End If
#If Int32_ERRORS
            M(i Xor chr, "i Xor chr")
#End If
#If Int32_ERRORS
            M(i Xor str, "i Xor str")
#End If
#If Int32_ERRORS
            M(i Xor dt, "i Xor dt")
#End If
#If Int32_ERRORS
            M(i Xor dbnull, "i Xor dbnull")
#End If
#If Int32_ERRORS
            M(i Xor obj, "i Xor obj")
#End If
#If UInt32_ERRORS
            M(ui Xor bool, "ui Xor bool")
#End If
#If UInt32_ERRORS
            M(ui Xor b, "ui Xor b")
#End If
#If UInt32_ERRORS
            M(ui Xor sb, "ui Xor sb")
#End If
#If UInt32_ERRORS
            M(ui Xor s, "ui Xor s")
#End If
#If UInt32_ERRORS
            M(ui Xor us, "ui Xor us")
#End If
#If UInt32_ERRORS
            M(ui Xor i, "ui Xor i")
#End If
#If UInt32_ERRORS
            M(ui Xor ui, "ui Xor ui")
#End If
#If UInt32_ERRORS
            M(ui Xor l, "ui Xor l")
#End If
#If UInt32_ERRORS
            M(ui Xor ul, "ui Xor ul")
#End If
#If UInt32_ERRORS
            M(ui Xor dec, "ui Xor dec")
#End If
#If UInt32_ERRORS
            M(ui Xor sng, "ui Xor sng")
#End If
#If UInt32_ERRORS
            M(ui Xor dbl, "ui Xor dbl")
#End If
#If UInt32_ERRORS
            M(ui Xor chr, "ui Xor chr")
#End If
#If UInt32_ERRORS
            M(ui Xor str, "ui Xor str")
#End If
#If UInt32_ERRORS
            M(ui Xor dt, "ui Xor dt")
#End If
#If UInt32_ERRORS
            M(ui Xor dbnull, "ui Xor dbnull")
#End If
#If UInt32_ERRORS
            M(ui Xor obj, "ui Xor obj")
#End If
#If Int64_ERRORS
            M(l Xor bool, "l Xor bool")
#End If
#If Int64_ERRORS
            M(l Xor b, "l Xor b")
#End If
#If Int64_ERRORS
            M(l Xor sb, "l Xor sb")
#End If
#If Int64_ERRORS
            M(l Xor s, "l Xor s")
#End If
#If Int64_ERRORS
            M(l Xor us, "l Xor us")
#End If
#If Int64_ERRORS
            M(l Xor i, "l Xor i")
#End If
#If Int64_ERRORS
            M(l Xor ui, "l Xor ui")
#End If
#If Int64_ERRORS
            M(l Xor l, "l Xor l")
#End If
#If Int64_ERRORS
            M(l Xor ul, "l Xor ul")
#End If
#If Int64_ERRORS
            M(l Xor dec, "l Xor dec")
#End If
#If Int64_ERRORS
            M(l Xor sng, "l Xor sng")
#End If
#If Int64_ERRORS
            M(l Xor dbl, "l Xor dbl")
#End If
#If Int64_ERRORS
            M(l Xor chr, "l Xor chr")
#End If
#If Int64_ERRORS
            M(l Xor str, "l Xor str")
#End If
#If Int64_ERRORS
            M(l Xor dt, "l Xor dt")
#End If
#If Int64_ERRORS
            M(l Xor dbnull, "l Xor dbnull")
#End If
#If Int64_ERRORS
            M(l Xor obj, "l Xor obj")
#End If
#If UInt64_ERRORS
            M(ul Xor bool, "ul Xor bool")
#End If
#If UInt64_ERRORS
            M(ul Xor b, "ul Xor b")
#End If
#If UInt64_ERRORS
            M(ul Xor sb, "ul Xor sb")
#End If
#If UInt64_ERRORS
            M(ul Xor s, "ul Xor s")
#End If
#If UInt64_ERRORS
            M(ul Xor us, "ul Xor us")
#End If
#If UInt64_ERRORS
            M(ul Xor i, "ul Xor i")
#End If
#If UInt64_ERRORS
            M(ul Xor ui, "ul Xor ui")
#End If
#If UInt64_ERRORS
            M(ul Xor l, "ul Xor l")
#End If
#If UInt64_ERRORS
            M(ul Xor ul, "ul Xor ul")
#End If
#If UInt64_ERRORS
            M(ul Xor dec, "ul Xor dec")
#End If
#If UInt64_ERRORS
            M(ul Xor sng, "ul Xor sng")
#End If
#If UInt64_ERRORS
            M(ul Xor dbl, "ul Xor dbl")
#End If
#If UInt64_ERRORS
            M(ul Xor chr, "ul Xor chr")
#End If
#If UInt64_ERRORS
            M(ul Xor str, "ul Xor str")
#End If
#If UInt64_ERRORS
            M(ul Xor dt, "ul Xor dt")
#End If
#If UInt64_ERRORS
            M(ul Xor dbnull, "ul Xor dbnull")
#End If
#If UInt64_ERRORS
            M(ul Xor obj, "ul Xor obj")
#End If
#If Decimal_ERRORS
            M(dec Xor bool, "dec Xor bool")
#End If
#If Decimal_ERRORS
            M(dec Xor b, "dec Xor b")
#End If
#If Decimal_ERRORS
            M(dec Xor sb, "dec Xor sb")
#End If
#If Decimal_ERRORS
            M(dec Xor s, "dec Xor s")
#End If
#If Decimal_ERRORS
            M(dec Xor us, "dec Xor us")
#End If
#If Decimal_ERRORS
            M(dec Xor i, "dec Xor i")
#End If
#If Decimal_ERRORS
            M(dec Xor ui, "dec Xor ui")
#End If
#If Decimal_ERRORS
            M(dec Xor l, "dec Xor l")
#End If
#If Decimal_ERRORS
            M(dec Xor ul, "dec Xor ul")
#End If
#If Decimal_ERRORS
            M(dec Xor dec, "dec Xor dec")
#End If
#If Decimal_ERRORS
            M(dec Xor sng, "dec Xor sng")
#End If
#If Decimal_ERRORS
            M(dec Xor dbl, "dec Xor dbl")
#End If
#If Decimal_ERRORS
            M(dec Xor chr, "dec Xor chr")
#End If
#If Decimal_ERRORS
            M(dec Xor str, "dec Xor str")
#End If
#If Decimal_ERRORS
            M(dec Xor dt, "dec Xor dt")
#End If
#If Decimal_ERRORS
            M(dec Xor dbnull, "dec Xor dbnull")
#End If
#If Decimal_ERRORS
            M(dec Xor obj, "dec Xor obj")
#End If
#If Single_ERRORS
            M(sng Xor bool, "sng Xor bool")
#End If
#If Single_ERRORS
            M(sng Xor b, "sng Xor b")
#End If
#If Single_ERRORS
            M(sng Xor sb, "sng Xor sb")
#End If
#If Single_ERRORS
            M(sng Xor s, "sng Xor s")
#End If
#If Single_ERRORS
            M(sng Xor us, "sng Xor us")
#End If
#If Single_ERRORS
            M(sng Xor i, "sng Xor i")
#End If
#If Single_ERRORS
            M(sng Xor ui, "sng Xor ui")
#End If
#If Single_ERRORS
            M(sng Xor l, "sng Xor l")
#End If
#If Single_ERRORS
            M(sng Xor ul, "sng Xor ul")
#End If
#If Single_ERRORS
            M(sng Xor dec, "sng Xor dec")
#End If
#If Single_ERRORS
            M(sng Xor sng, "sng Xor sng")
#End If
#If Single_ERRORS
            M(sng Xor dbl, "sng Xor dbl")
#End If
#If Single_ERRORS
            M(sng Xor chr, "sng Xor chr")
#End If
#If Single_ERRORS
            M(sng Xor str, "sng Xor str")
#End If
#If Single_ERRORS
            M(sng Xor dt, "sng Xor dt")
#End If
#If Single_ERRORS
            M(sng Xor dbnull, "sng Xor dbnull")
#End If
#If Single_ERRORS
            M(sng Xor obj, "sng Xor obj")
#End If
#If Double_ERRORS
            M(dbl Xor bool, "dbl Xor bool")
#End If
#If Double_ERRORS
            M(dbl Xor b, "dbl Xor b")
#End If
#If Double_ERRORS
            M(dbl Xor sb, "dbl Xor sb")
#End If
#If Double_ERRORS
            M(dbl Xor s, "dbl Xor s")
#End If
#If Double_ERRORS
            M(dbl Xor us, "dbl Xor us")
#End If
#If Double_ERRORS
            M(dbl Xor i, "dbl Xor i")
#End If
#If Double_ERRORS
            M(dbl Xor ui, "dbl Xor ui")
#End If
#If Double_ERRORS
            M(dbl Xor l, "dbl Xor l")
#End If
#If Double_ERRORS
            M(dbl Xor ul, "dbl Xor ul")
#End If
#If Double_ERRORS
            M(dbl Xor dec, "dbl Xor dec")
#End If
#If Double_ERRORS
            M(dbl Xor sng, "dbl Xor sng")
#End If
#If Double_ERRORS
            M(dbl Xor dbl, "dbl Xor dbl")
#End If
#If Double_ERRORS
            M(dbl Xor chr, "dbl Xor chr")
#End If
#If Double_ERRORS
            M(dbl Xor str, "dbl Xor str")
#End If
#If Double_ERRORS
            M(dbl Xor dt, "dbl Xor dt")
#End If
#If Double_ERRORS
            M(dbl Xor dbnull, "dbl Xor dbnull")
#End If
#If Double_ERRORS
            M(dbl Xor obj, "dbl Xor obj")
#End If
#If Char_ERRORS
            M(chr Xor bool, "chr Xor bool")
#End If
#If Char_ERRORS
            M(chr Xor b, "chr Xor b")
#End If
#If Char_ERRORS
            M(chr Xor sb, "chr Xor sb")
#End If
#If Char_ERRORS
            M(chr Xor s, "chr Xor s")
#End If
#If Char_ERRORS
            M(chr Xor us, "chr Xor us")
#End If
#If Char_ERRORS
            M(chr Xor i, "chr Xor i")
#End If
#If Char_ERRORS
            M(chr Xor ui, "chr Xor ui")
#End If
#If Char_ERRORS
            M(chr Xor l, "chr Xor l")
#End If
#If Char_ERRORS
            M(chr Xor ul, "chr Xor ul")
#End If
#If Char_ERRORS
            M(chr Xor dec, "chr Xor dec")
#End If
#If Char_ERRORS
            M(chr Xor sng, "chr Xor sng")
#End If
#If Char_ERRORS
            M(chr Xor dbl, "chr Xor dbl")
#End If
#If Char_ERRORS
            M(chr Xor chr, "chr Xor chr")
#End If
#If Char_ERRORS
            M(chr Xor str, "chr Xor str")
#End If
#If Char_ERRORS
            M(chr Xor dt, "chr Xor dt")
#End If
#If Char_ERRORS
            M(chr Xor dbnull, "chr Xor dbnull")
#End If
#If Char_ERRORS
            M(chr Xor obj, "chr Xor obj")
#End If
#If String_ERRORS
            M(str Xor bool, "str Xor bool")
#End If
#If String_ERRORS
            M(str Xor b, "str Xor b")
#End If
#If String_ERRORS
            M(str Xor sb, "str Xor sb")
#End If
#If String_ERRORS
            M(str Xor s, "str Xor s")
#End If
#If String_ERRORS
            M(str Xor us, "str Xor us")
#End If
#If String_ERRORS
            M(str Xor i, "str Xor i")
#End If
#If String_ERRORS
            M(str Xor ui, "str Xor ui")
#End If
#If String_ERRORS
            M(str Xor l, "str Xor l")
#End If
#If String_ERRORS
            M(str Xor ul, "str Xor ul")
#End If
#If String_ERRORS
            M(str Xor dec, "str Xor dec")
#End If
#If String_ERRORS
            M(str Xor sng, "str Xor sng")
#End If
#If String_ERRORS
            M(str Xor dbl, "str Xor dbl")
#End If
#If String_ERRORS
            M(str Xor chr, "str Xor chr")
#End If
#If String_ERRORS
            M(str Xor str, "str Xor str")
#End If
#If String_ERRORS
            M(str Xor dt, "str Xor dt")
#End If
#If String_ERRORS
            M(str Xor dbnull, "str Xor dbnull")
#End If
#If String_ERRORS
            M(str Xor obj, "str Xor obj")
#End If
#If DateTime_ERRORS
            M(dt Xor bool, "dt Xor bool")
#End If
#If DateTime_ERRORS
            M(dt Xor b, "dt Xor b")
#End If
#If DateTime_ERRORS
            M(dt Xor sb, "dt Xor sb")
#End If
#If DateTime_ERRORS
            M(dt Xor s, "dt Xor s")
#End If
#If DateTime_ERRORS
            M(dt Xor us, "dt Xor us")
#End If
#If DateTime_ERRORS
            M(dt Xor i, "dt Xor i")
#End If
#If DateTime_ERRORS
            M(dt Xor ui, "dt Xor ui")
#End If
#If DateTime_ERRORS
            M(dt Xor l, "dt Xor l")
#End If
#If DateTime_ERRORS
            M(dt Xor ul, "dt Xor ul")
#End If
#If DateTime_ERRORS
            M(dt Xor dec, "dt Xor dec")
#End If
#If DateTime_ERRORS
            M(dt Xor sng, "dt Xor sng")
#End If
#If DateTime_ERRORS
            M(dt Xor dbl, "dt Xor dbl")
#End If
#If DateTime_ERRORS
            M(dt Xor chr, "dt Xor chr")
#End If
#If DateTime_ERRORS
            M(dt Xor str, "dt Xor str")
#End If
#If DateTime_ERRORS
            M(dt Xor dt, "dt Xor dt")
#End If
#If DateTime_ERRORS
            M(dt Xor dbnull, "dt Xor dbnull")
#End If
#If DateTime_ERRORS
            M(dt Xor obj, "dt Xor obj")
#End If
#If DBNull_ERRORS
            M(dbnull Xor bool, "dbnull Xor bool")
#End If
#If DBNull_ERRORS
            M(dbnull Xor b, "dbnull Xor b")
#End If
#If DBNull_ERRORS
            M(dbnull Xor sb, "dbnull Xor sb")
#End If
#If DBNull_ERRORS
            M(dbnull Xor s, "dbnull Xor s")
#End If
#If DBNull_ERRORS
            M(dbnull Xor us, "dbnull Xor us")
#End If
#If DBNull_ERRORS
            M(dbnull Xor i, "dbnull Xor i")
#End If
#If DBNull_ERRORS
            M(dbnull Xor ui, "dbnull Xor ui")
#End If
#If DBNull_ERRORS
            M(dbnull Xor l, "dbnull Xor l")
#End If
#If DBNull_ERRORS
            M(dbnull Xor ul, "dbnull Xor ul")
#End If
#If DBNull_ERRORS
            M(dbnull Xor dec, "dbnull Xor dec")
#End If
#If DBNull_ERRORS
            M(dbnull Xor sng, "dbnull Xor sng")
#End If
#If DBNull_ERRORS
            M(dbnull Xor dbl, "dbnull Xor dbl")
#End If
#If DBNull_ERRORS
            M(dbnull Xor chr, "dbnull Xor chr")
#End If
#If DBNull_ERRORS
            M(dbnull Xor str, "dbnull Xor str")
#End If
#If DBNull_ERRORS
            M(dbnull Xor dt, "dbnull Xor dt")
#End If
#If DBNull_ERRORS
            M(dbnull Xor dbnull, "dbnull Xor dbnull")
#End If
#If DBNull_ERRORS
            M(dbnull Xor obj, "dbnull Xor obj")
#End If
#If Object_ERRORS
            M(obj Xor bool, "obj Xor bool")
#End If
#If Object_ERRORS
            M(obj Xor b, "obj Xor b")
#End If
#If Object_ERRORS
            M(obj Xor sb, "obj Xor sb")
#End If
#If Object_ERRORS
            M(obj Xor s, "obj Xor s")
#End If
#If Object_ERRORS
            M(obj Xor us, "obj Xor us")
#End If
#If Object_ERRORS
            M(obj Xor i, "obj Xor i")
#End If
#If Object_ERRORS
            M(obj Xor ui, "obj Xor ui")
#End If
#If Object_ERRORS
            M(obj Xor l, "obj Xor l")
#End If
#If Object_ERRORS
            M(obj Xor ul, "obj Xor ul")
#End If
#If Object_ERRORS
            M(obj Xor dec, "obj Xor dec")
#End If
#If Object_ERRORS
            M(obj Xor sng, "obj Xor sng")
#End If
#If Object_ERRORS
            M(obj Xor dbl, "obj Xor dbl")
#End If
#If Object_ERRORS
            M(obj Xor chr, "obj Xor chr")
#End If
#If Object_ERRORS
            M(obj Xor str, "obj Xor str")
#End If
#If Object_ERRORS
            M(obj Xor dt, "obj Xor dt")
#End If
#If Object_ERRORS
            M(obj Xor dbnull, "obj Xor dbnull")
#End If
#If Object_ERRORS
            M(obj Xor obj, "obj Xor obj")
#End If
        If failures > 0 Then Return 1
    Catch ex As Exception
        Console.WriteLine ("Exception: {0}", ex)
        Return 2
    End Try
    Return 0
    End Function
End Class
