Class BinaryOperatorAnd
    Inherits IntrinsicOperatorTests
    Shared Function Main As Integer
        Try
            expected_tc = TypeCode.Boolean
            M(bool And bool, "bool And bool")
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Int16
            M(bool And b, "bool And b")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.SByte
            M(bool And sb, "bool And sb")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Int16
            M(bool And s, "bool And s")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Int32
            M(bool And us, "bool And us")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Int32
            M(bool And i, "bool And i")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Int64
            M(bool And ui, "bool And ui")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Int64
            M(bool And l, "bool And l")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Int64
            M(bool And ul, "bool And ul")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Int64
            M(bool And dec, "bool And dec")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Int64
            M(bool And sng, "bool And sng")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Int64
            M(bool And dbl, "bool And dbl")
#End If
#If Boolean_ERRORS
            M(bool And chr, "bool And chr")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Boolean
            M(bool And str, "bool And str")
#End If
#If Boolean_ERRORS
            M(bool And dt, "bool And dt")
#End If
#If Boolean_ERRORS
            M(bool And dbnull, "bool And dbnull")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Object
            M(bool And obj, "bool And obj")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Int16
            M(b And bool, "b And bool")
#End If
            expected_tc = TypeCode.Byte
            M(b And b, "b And b")
            expected_tc = TypeCode.Int16
            M(b And sb, "b And sb")
            expected_tc = TypeCode.Int16
            M(b And s, "b And s")
            expected_tc = TypeCode.UInt16
            M(b And us, "b And us")
            expected_tc = TypeCode.Int32
            M(b And i, "b And i")
            expected_tc = TypeCode.UInt32
            M(b And ui, "b And ui")
            expected_tc = TypeCode.Int64
            M(b And l, "b And l")
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.UInt64
            M(b And ul, "b And ul")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Int64
            M(b And dec, "b And dec")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Int64
            M(b And sng, "b And sng")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Int64
            M(b And dbl, "b And dbl")
#End If
#If Byte_ERRORS
            M(b And chr, "b And chr")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Int64
            M(b And str, "b And str")
#End If
#If Byte_ERRORS
            M(b And dt, "b And dt")
#End If
#If Byte_ERRORS
            M(b And dbnull, "b And dbnull")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Object
            M(b And obj, "b And obj")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.SByte
            M(sb And bool, "sb And bool")
#End If
            expected_tc = TypeCode.Int16
            M(sb And b, "sb And b")
            expected_tc = TypeCode.SByte
            M(sb And sb, "sb And sb")
            expected_tc = TypeCode.Int16
            M(sb And s, "sb And s")
            expected_tc = TypeCode.Int32
            M(sb And us, "sb And us")
            expected_tc = TypeCode.Int32
            M(sb And i, "sb And i")
            expected_tc = TypeCode.Int64
            M(sb And ui, "sb And ui")
            expected_tc = TypeCode.Int64
            M(sb And l, "sb And l")
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Int64
            M(sb And ul, "sb And ul")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Int64
            M(sb And dec, "sb And dec")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Int64
            M(sb And sng, "sb And sng")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Int64
            M(sb And dbl, "sb And dbl")
#End If
#If SByte_ERRORS
            M(sb And chr, "sb And chr")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Int64
            M(sb And str, "sb And str")
#End If
#If SByte_ERRORS
            M(sb And dt, "sb And dt")
#End If
#If SByte_ERRORS
            M(sb And dbnull, "sb And dbnull")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Object
            M(sb And obj, "sb And obj")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Int16
            M(s And bool, "s And bool")
#End If
            expected_tc = TypeCode.Int16
            M(s And b, "s And b")
            expected_tc = TypeCode.Int16
            M(s And sb, "s And sb")
            expected_tc = TypeCode.Int16
            M(s And s, "s And s")
            expected_tc = TypeCode.Int32
            M(s And us, "s And us")
            expected_tc = TypeCode.Int32
            M(s And i, "s And i")
            expected_tc = TypeCode.Int64
            M(s And ui, "s And ui")
            expected_tc = TypeCode.Int64
            M(s And l, "s And l")
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Int64
            M(s And ul, "s And ul")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Int64
            M(s And dec, "s And dec")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Int64
            M(s And sng, "s And sng")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Int64
            M(s And dbl, "s And dbl")
#End If
#If Int16_ERRORS
            M(s And chr, "s And chr")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Int64
            M(s And str, "s And str")
#End If
#If Int16_ERRORS
            M(s And dt, "s And dt")
#End If
#If Int16_ERRORS
            M(s And dbnull, "s And dbnull")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Object
            M(s And obj, "s And obj")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Int32
            M(us And bool, "us And bool")
#End If
            expected_tc = TypeCode.UInt16
            M(us And b, "us And b")
            expected_tc = TypeCode.Int32
            M(us And sb, "us And sb")
            expected_tc = TypeCode.Int32
            M(us And s, "us And s")
            expected_tc = TypeCode.UInt16
            M(us And us, "us And us")
            expected_tc = TypeCode.Int32
            M(us And i, "us And i")
            expected_tc = TypeCode.UInt32
            M(us And ui, "us And ui")
            expected_tc = TypeCode.Int64
            M(us And l, "us And l")
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.UInt64
            M(us And ul, "us And ul")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Int64
            M(us And dec, "us And dec")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Int64
            M(us And sng, "us And sng")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Int64
            M(us And dbl, "us And dbl")
#End If
#If UInt16_ERRORS
            M(us And chr, "us And chr")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Int64
            M(us And str, "us And str")
#End If
#If UInt16_ERRORS
            M(us And dt, "us And dt")
#End If
#If UInt16_ERRORS
            M(us And dbnull, "us And dbnull")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Object
            M(us And obj, "us And obj")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Int32
            M(i And bool, "i And bool")
#End If
            expected_tc = TypeCode.Int32
            M(i And b, "i And b")
            expected_tc = TypeCode.Int32
            M(i And sb, "i And sb")
            expected_tc = TypeCode.Int32
            M(i And s, "i And s")
            expected_tc = TypeCode.Int32
            M(i And us, "i And us")
            expected_tc = TypeCode.Int32
            M(i And i, "i And i")
            expected_tc = TypeCode.Int64
            M(i And ui, "i And ui")
            expected_tc = TypeCode.Int64
            M(i And l, "i And l")
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Int64
            M(i And ul, "i And ul")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Int64
            M(i And dec, "i And dec")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Int64
            M(i And sng, "i And sng")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Int64
            M(i And dbl, "i And dbl")
#End If
#If Int32_ERRORS
            M(i And chr, "i And chr")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Int64
            M(i And str, "i And str")
#End If
#If Int32_ERRORS
            M(i And dt, "i And dt")
#End If
#If Int32_ERRORS
            M(i And dbnull, "i And dbnull")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Object
            M(i And obj, "i And obj")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Int64
            M(ui And bool, "ui And bool")
#End If
            expected_tc = TypeCode.UInt32
            M(ui And b, "ui And b")
            expected_tc = TypeCode.Int64
            M(ui And sb, "ui And sb")
            expected_tc = TypeCode.Int64
            M(ui And s, "ui And s")
            expected_tc = TypeCode.UInt32
            M(ui And us, "ui And us")
            expected_tc = TypeCode.Int64
            M(ui And i, "ui And i")
            expected_tc = TypeCode.UInt32
            M(ui And ui, "ui And ui")
            expected_tc = TypeCode.Int64
            M(ui And l, "ui And l")
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.UInt64
            M(ui And ul, "ui And ul")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Int64
            M(ui And dec, "ui And dec")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Int64
            M(ui And sng, "ui And sng")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Int64
            M(ui And dbl, "ui And dbl")
#End If
#If UInt32_ERRORS
            M(ui And chr, "ui And chr")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Int64
            M(ui And str, "ui And str")
#End If
#If UInt32_ERRORS
            M(ui And dt, "ui And dt")
#End If
#If UInt32_ERRORS
            M(ui And dbnull, "ui And dbnull")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Object
            M(ui And obj, "ui And obj")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Int64
            M(l And bool, "l And bool")
#End If
            expected_tc = TypeCode.Int64
            M(l And b, "l And b")
            expected_tc = TypeCode.Int64
            M(l And sb, "l And sb")
            expected_tc = TypeCode.Int64
            M(l And s, "l And s")
            expected_tc = TypeCode.Int64
            M(l And us, "l And us")
            expected_tc = TypeCode.Int64
            M(l And i, "l And i")
            expected_tc = TypeCode.Int64
            M(l And ui, "l And ui")
            expected_tc = TypeCode.Int64
            M(l And l, "l And l")
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Int64
            M(l And ul, "l And ul")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Int64
            M(l And dec, "l And dec")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Int64
            M(l And sng, "l And sng")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Int64
            M(l And dbl, "l And dbl")
#End If
#If Int64_ERRORS
            M(l And chr, "l And chr")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Int64
            M(l And str, "l And str")
#End If
#If Int64_ERRORS
            M(l And dt, "l And dt")
#End If
#If Int64_ERRORS
            M(l And dbnull, "l And dbnull")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Object
            M(l And obj, "l And obj")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Int64
            M(ul And bool, "ul And bool")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.UInt64
            M(ul And b, "ul And b")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Int64
            M(ul And sb, "ul And sb")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Int64
            M(ul And s, "ul And s")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.UInt64
            M(ul And us, "ul And us")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Int64
            M(ul And i, "ul And i")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.UInt64
            M(ul And ui, "ul And ui")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Int64
            M(ul And l, "ul And l")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.UInt64
            M(ul And ul, "ul And ul")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Int64
            M(ul And dec, "ul And dec")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Int64
            M(ul And sng, "ul And sng")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Int64
            M(ul And dbl, "ul And dbl")
#End If
#If UInt64_ERRORS
            M(ul And chr, "ul And chr")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Int64
            M(ul And str, "ul And str")
#End If
#If UInt64_ERRORS
            M(ul And dt, "ul And dt")
#End If
#If UInt64_ERRORS
            M(ul And dbnull, "ul And dbnull")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Object
            M(ul And obj, "ul And obj")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Int64
            M(dec And bool, "dec And bool")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Int64
            M(dec And b, "dec And b")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Int64
            M(dec And sb, "dec And sb")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Int64
            M(dec And s, "dec And s")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Int64
            M(dec And us, "dec And us")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Int64
            M(dec And i, "dec And i")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Int64
            M(dec And ui, "dec And ui")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Int64
            M(dec And l, "dec And l")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Int64
            M(dec And ul, "dec And ul")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Int64
            M(dec And dec, "dec And dec")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Int64
            M(dec And sng, "dec And sng")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Int64
            M(dec And dbl, "dec And dbl")
#End If
#If Decimal_ERRORS
            M(dec And chr, "dec And chr")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Int64
            M(dec And str, "dec And str")
#End If
#If Decimal_ERRORS
            M(dec And dt, "dec And dt")
#End If
#If Decimal_ERRORS
            M(dec And dbnull, "dec And dbnull")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Object
            M(dec And obj, "dec And obj")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Int64
            M(sng And bool, "sng And bool")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Int64
            M(sng And b, "sng And b")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Int64
            M(sng And sb, "sng And sb")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Int64
            M(sng And s, "sng And s")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Int64
            M(sng And us, "sng And us")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Int64
            M(sng And i, "sng And i")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Int64
            M(sng And ui, "sng And ui")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Int64
            M(sng And l, "sng And l")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Int64
            M(sng And ul, "sng And ul")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Int64
            M(sng And dec, "sng And dec")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Int64
            M(sng And sng, "sng And sng")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Int64
            M(sng And dbl, "sng And dbl")
#End If
#If Single_ERRORS
            M(sng And chr, "sng And chr")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Int64
            M(sng And str, "sng And str")
#End If
#If Single_ERRORS
            M(sng And dt, "sng And dt")
#End If
#If Single_ERRORS
            M(sng And dbnull, "sng And dbnull")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Object
            M(sng And obj, "sng And obj")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Int64
            M(dbl And bool, "dbl And bool")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Int64
            M(dbl And b, "dbl And b")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Int64
            M(dbl And sb, "dbl And sb")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Int64
            M(dbl And s, "dbl And s")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Int64
            M(dbl And us, "dbl And us")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Int64
            M(dbl And i, "dbl And i")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Int64
            M(dbl And ui, "dbl And ui")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Int64
            M(dbl And l, "dbl And l")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Int64
            M(dbl And ul, "dbl And ul")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Int64
            M(dbl And dec, "dbl And dec")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Int64
            M(dbl And sng, "dbl And sng")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Int64
            M(dbl And dbl, "dbl And dbl")
#End If
#If Double_ERRORS
            M(dbl And chr, "dbl And chr")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Int64
            M(dbl And str, "dbl And str")
#End If
#If Double_ERRORS
            M(dbl And dt, "dbl And dt")
#End If
#If Double_ERRORS
            M(dbl And dbnull, "dbl And dbnull")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Object
            M(dbl And obj, "dbl And obj")
#End If
#If Char_ERRORS
            M(chr And bool, "chr And bool")
#End If
#If Char_ERRORS
            M(chr And b, "chr And b")
#End If
#If Char_ERRORS
            M(chr And sb, "chr And sb")
#End If
#If Char_ERRORS
            M(chr And s, "chr And s")
#End If
#If Char_ERRORS
            M(chr And us, "chr And us")
#End If
#If Char_ERRORS
            M(chr And i, "chr And i")
#End If
#If Char_ERRORS
            M(chr And ui, "chr And ui")
#End If
#If Char_ERRORS
            M(chr And l, "chr And l")
#End If
#If Char_ERRORS
            M(chr And ul, "chr And ul")
#End If
#If Char_ERRORS
            M(chr And dec, "chr And dec")
#End If
#If Char_ERRORS
            M(chr And sng, "chr And sng")
#End If
#If Char_ERRORS
            M(chr And dbl, "chr And dbl")
#End If
#If Char_ERRORS
            M(chr And chr, "chr And chr")
#End If
#If Char_ERRORS
            M(chr And str, "chr And str")
#End If
#If Char_ERRORS
            M(chr And dt, "chr And dt")
#End If
#If Char_ERRORS
            M(chr And dbnull, "chr And dbnull")
#End If
#If Char_ERRORS
            M(chr And obj, "chr And obj")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Boolean
            M(str And bool, "str And bool")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Int64
            M(str And b, "str And b")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Int64
            M(str And sb, "str And sb")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Int64
            M(str And s, "str And s")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Int64
            M(str And us, "str And us")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Int64
            M(str And i, "str And i")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Int64
            M(str And ui, "str And ui")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Int64
            M(str And l, "str And l")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Int64
            M(str And ul, "str And ul")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Int64
            M(str And dec, "str And dec")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Int64
            M(str And sng, "str And sng")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Int64
            M(str And dbl, "str And dbl")
#End If
#If String_ERRORS
            M(str And chr, "str And chr")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Int64
            M(str And str, "str And str")
#End If
#If String_ERRORS
            M(str And dt, "str And dt")
#End If
#If String_ERRORS
            M(str And dbnull, "str And dbnull")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Object
            M(str And obj, "str And obj")
#End If
#If DateTime_ERRORS
            M(dt And bool, "dt And bool")
#End If
#If DateTime_ERRORS
            M(dt And b, "dt And b")
#End If
#If DateTime_ERRORS
            M(dt And sb, "dt And sb")
#End If
#If DateTime_ERRORS
            M(dt And s, "dt And s")
#End If
#If DateTime_ERRORS
            M(dt And us, "dt And us")
#End If
#If DateTime_ERRORS
            M(dt And i, "dt And i")
#End If
#If DateTime_ERRORS
            M(dt And ui, "dt And ui")
#End If
#If DateTime_ERRORS
            M(dt And l, "dt And l")
#End If
#If DateTime_ERRORS
            M(dt And ul, "dt And ul")
#End If
#If DateTime_ERRORS
            M(dt And dec, "dt And dec")
#End If
#If DateTime_ERRORS
            M(dt And sng, "dt And sng")
#End If
#If DateTime_ERRORS
            M(dt And dbl, "dt And dbl")
#End If
#If DateTime_ERRORS
            M(dt And chr, "dt And chr")
#End If
#If DateTime_ERRORS
            M(dt And str, "dt And str")
#End If
#If DateTime_ERRORS
            M(dt And dt, "dt And dt")
#End If
#If DateTime_ERRORS
            M(dt And dbnull, "dt And dbnull")
#End If
#If DateTime_ERRORS
            M(dt And obj, "dt And obj")
#End If
#If DBNull_ERRORS
            M(dbnull And bool, "dbnull And bool")
#End If
#If DBNull_ERRORS
            M(dbnull And b, "dbnull And b")
#End If
#If DBNull_ERRORS
            M(dbnull And sb, "dbnull And sb")
#End If
#If DBNull_ERRORS
            M(dbnull And s, "dbnull And s")
#End If
#If DBNull_ERRORS
            M(dbnull And us, "dbnull And us")
#End If
#If DBNull_ERRORS
            M(dbnull And i, "dbnull And i")
#End If
#If DBNull_ERRORS
            M(dbnull And ui, "dbnull And ui")
#End If
#If DBNull_ERRORS
            M(dbnull And l, "dbnull And l")
#End If
#If DBNull_ERRORS
            M(dbnull And ul, "dbnull And ul")
#End If
#If DBNull_ERRORS
            M(dbnull And dec, "dbnull And dec")
#End If
#If DBNull_ERRORS
            M(dbnull And sng, "dbnull And sng")
#End If
#If DBNull_ERRORS
            M(dbnull And dbl, "dbnull And dbl")
#End If
#If DBNull_ERRORS
            M(dbnull And chr, "dbnull And chr")
#End If
#If DBNull_ERRORS
            M(dbnull And str, "dbnull And str")
#End If
#If DBNull_ERRORS
            M(dbnull And dt, "dbnull And dt")
#End If
#If DBNull_ERRORS
            M(dbnull And dbnull, "dbnull And dbnull")
#End If
#If DBNull_ERRORS
            M(dbnull And obj, "dbnull And obj")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj And bool, "obj And bool")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj And b, "obj And b")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj And sb, "obj And sb")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj And s, "obj And s")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj And us, "obj And us")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj And i, "obj And i")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj And ui, "obj And ui")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj And l, "obj And l")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj And ul, "obj And ul")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj And dec, "obj And dec")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj And sng, "obj And sng")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj And dbl, "obj And dbl")
#End If
#If Object_ERRORS
            M(obj And chr, "obj And chr")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj And str, "obj And str")
#End If
#If Object_ERRORS
            M(obj And dt, "obj And dt")
#End If
#If Object_ERRORS
            M(obj And dbnull, "obj And dbnull")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj And obj, "obj And obj")
#End If
        If failures > 0 Then Return 1
    Catch ex As Exception
        Console.WriteLine ("Exception: {0}", ex)
        Return 2
    End Try
    Return 0
    End Function
End Class
