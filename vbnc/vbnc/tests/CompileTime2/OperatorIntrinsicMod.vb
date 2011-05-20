Class BinaryOperatorMod
    Inherits IntrinsicOperatorTests
    Shared Function Main As Integer
        Try
            obj = 1
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Int16
            M(bool Mod bool, "bool Mod bool")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Int16
            M(bool Mod b, "bool Mod b")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.SByte
            M(bool Mod sb, "bool Mod sb")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Int16
            M(bool Mod s, "bool Mod s")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Int32
            M(bool Mod us, "bool Mod us")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Int32
            M(bool Mod i, "bool Mod i")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Int64
            M(bool Mod ui, "bool Mod ui")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Int64
            M(bool Mod l, "bool Mod l")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Decimal
            M(bool Mod ul, "bool Mod ul")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Decimal
            M(bool Mod dec, "bool Mod dec")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Single
            M(bool Mod sng, "bool Mod sng")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Double
            M(bool Mod dbl, "bool Mod dbl")
#End If
#If Boolean_ERRORS
            M(bool Mod chr, "bool Mod chr")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Double
            M(bool Mod str, "bool Mod str")
#End If
#If Boolean_ERRORS
            M(bool Mod dt, "bool Mod dt")
#End If
#If Boolean_ERRORS
            M(bool Mod dbnull, "bool Mod dbnull")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Object
            M(bool Mod obj, "bool Mod obj")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Int16
            M(b Mod bool, "b Mod bool")
#End If
            expected_tc = TypeCode.Byte
            M(b Mod b, "b Mod b")
            expected_tc = TypeCode.Int16
            M(b Mod sb, "b Mod sb")
            expected_tc = TypeCode.Int16
            M(b Mod s, "b Mod s")
            expected_tc = TypeCode.UInt16
            M(b Mod us, "b Mod us")
            expected_tc = TypeCode.Int32
            M(b Mod i, "b Mod i")
            expected_tc = TypeCode.UInt32
            M(b Mod ui, "b Mod ui")
            expected_tc = TypeCode.Int64
            M(b Mod l, "b Mod l")
            expected_tc = TypeCode.UInt64
            M(b Mod ul, "b Mod ul")
            expected_tc = TypeCode.Decimal
            M(b Mod dec, "b Mod dec")
            expected_tc = TypeCode.Single
            M(b Mod sng, "b Mod sng")
            expected_tc = TypeCode.Double
            M(b Mod dbl, "b Mod dbl")
#If Byte_ERRORS
            M(b Mod chr, "b Mod chr")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Double
            M(b Mod str, "b Mod str")
#End If
#If Byte_ERRORS
            M(b Mod dt, "b Mod dt")
#End If
#If Byte_ERRORS
            M(b Mod dbnull, "b Mod dbnull")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Object
            M(b Mod obj, "b Mod obj")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.SByte
            M(sb Mod bool, "sb Mod bool")
#End If
            expected_tc = TypeCode.Int16
            M(sb Mod b, "sb Mod b")
            expected_tc = TypeCode.SByte
            M(sb Mod sb, "sb Mod sb")
            expected_tc = TypeCode.Int16
            M(sb Mod s, "sb Mod s")
            expected_tc = TypeCode.Int32
            M(sb Mod us, "sb Mod us")
            expected_tc = TypeCode.Int32
            M(sb Mod i, "sb Mod i")
            expected_tc = TypeCode.Int64
            M(sb Mod ui, "sb Mod ui")
            expected_tc = TypeCode.Int64
            M(sb Mod l, "sb Mod l")
            expected_tc = TypeCode.Decimal
            M(sb Mod ul, "sb Mod ul")
            expected_tc = TypeCode.Decimal
            M(sb Mod dec, "sb Mod dec")
            expected_tc = TypeCode.Single
            M(sb Mod sng, "sb Mod sng")
            expected_tc = TypeCode.Double
            M(sb Mod dbl, "sb Mod dbl")
#If SByte_ERRORS
            M(sb Mod chr, "sb Mod chr")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Double
            M(sb Mod str, "sb Mod str")
#End If
#If SByte_ERRORS
            M(sb Mod dt, "sb Mod dt")
#End If
#If SByte_ERRORS
            M(sb Mod dbnull, "sb Mod dbnull")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Object
            M(sb Mod obj, "sb Mod obj")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Int16
            M(s Mod bool, "s Mod bool")
#End If
            expected_tc = TypeCode.Int16
            M(s Mod b, "s Mod b")
            expected_tc = TypeCode.Int16
            M(s Mod sb, "s Mod sb")
            expected_tc = TypeCode.Int16
            M(s Mod s, "s Mod s")
            expected_tc = TypeCode.Int32
            M(s Mod us, "s Mod us")
            expected_tc = TypeCode.Int32
            M(s Mod i, "s Mod i")
            expected_tc = TypeCode.Int64
            M(s Mod ui, "s Mod ui")
            expected_tc = TypeCode.Int64
            M(s Mod l, "s Mod l")
            expected_tc = TypeCode.Decimal
            M(s Mod ul, "s Mod ul")
            expected_tc = TypeCode.Decimal
            M(s Mod dec, "s Mod dec")
            expected_tc = TypeCode.Single
            M(s Mod sng, "s Mod sng")
            expected_tc = TypeCode.Double
            M(s Mod dbl, "s Mod dbl")
#If Int16_ERRORS
            M(s Mod chr, "s Mod chr")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Double
            M(s Mod str, "s Mod str")
#End If
#If Int16_ERRORS
            M(s Mod dt, "s Mod dt")
#End If
#If Int16_ERRORS
            M(s Mod dbnull, "s Mod dbnull")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Object
            M(s Mod obj, "s Mod obj")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Int32
            M(us Mod bool, "us Mod bool")
#End If
            expected_tc = TypeCode.UInt16
            M(us Mod b, "us Mod b")
            expected_tc = TypeCode.Int32
            M(us Mod sb, "us Mod sb")
            expected_tc = TypeCode.Int32
            M(us Mod s, "us Mod s")
            expected_tc = TypeCode.UInt16
            M(us Mod us, "us Mod us")
            expected_tc = TypeCode.Int32
            M(us Mod i, "us Mod i")
            expected_tc = TypeCode.UInt32
            M(us Mod ui, "us Mod ui")
            expected_tc = TypeCode.Int64
            M(us Mod l, "us Mod l")
            expected_tc = TypeCode.UInt64
            M(us Mod ul, "us Mod ul")
            expected_tc = TypeCode.Decimal
            M(us Mod dec, "us Mod dec")
            expected_tc = TypeCode.Single
            M(us Mod sng, "us Mod sng")
            expected_tc = TypeCode.Double
            M(us Mod dbl, "us Mod dbl")
#If UInt16_ERRORS
            M(us Mod chr, "us Mod chr")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Double
            M(us Mod str, "us Mod str")
#End If
#If UInt16_ERRORS
            M(us Mod dt, "us Mod dt")
#End If
#If UInt16_ERRORS
            M(us Mod dbnull, "us Mod dbnull")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Object
            M(us Mod obj, "us Mod obj")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Int32
            M(i Mod bool, "i Mod bool")
#End If
            expected_tc = TypeCode.Int32
            M(i Mod b, "i Mod b")
            expected_tc = TypeCode.Int32
            M(i Mod sb, "i Mod sb")
            expected_tc = TypeCode.Int32
            M(i Mod s, "i Mod s")
            expected_tc = TypeCode.Int32
            M(i Mod us, "i Mod us")
            expected_tc = TypeCode.Int32
            M(i Mod i, "i Mod i")
            expected_tc = TypeCode.Int64
            M(i Mod ui, "i Mod ui")
            expected_tc = TypeCode.Int64
            M(i Mod l, "i Mod l")
            expected_tc = TypeCode.Decimal
            M(i Mod ul, "i Mod ul")
            expected_tc = TypeCode.Decimal
            M(i Mod dec, "i Mod dec")
            expected_tc = TypeCode.Single
            M(i Mod sng, "i Mod sng")
            expected_tc = TypeCode.Double
            M(i Mod dbl, "i Mod dbl")
#If Int32_ERRORS
            M(i Mod chr, "i Mod chr")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Double
            M(i Mod str, "i Mod str")
#End If
#If Int32_ERRORS
            M(i Mod dt, "i Mod dt")
#End If
#If Int32_ERRORS
            M(i Mod dbnull, "i Mod dbnull")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Object
            M(i Mod obj, "i Mod obj")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Int64
            M(ui Mod bool, "ui Mod bool")
#End If
            expected_tc = TypeCode.UInt32
            M(ui Mod b, "ui Mod b")
            expected_tc = TypeCode.Int64
            M(ui Mod sb, "ui Mod sb")
            expected_tc = TypeCode.Int64
            M(ui Mod s, "ui Mod s")
            expected_tc = TypeCode.UInt32
            M(ui Mod us, "ui Mod us")
            expected_tc = TypeCode.Int64
            M(ui Mod i, "ui Mod i")
            expected_tc = TypeCode.UInt32
            M(ui Mod ui, "ui Mod ui")
            expected_tc = TypeCode.Int64
            M(ui Mod l, "ui Mod l")
            expected_tc = TypeCode.UInt64
            M(ui Mod ul, "ui Mod ul")
            expected_tc = TypeCode.Decimal
            M(ui Mod dec, "ui Mod dec")
            expected_tc = TypeCode.Single
            M(ui Mod sng, "ui Mod sng")
            expected_tc = TypeCode.Double
            M(ui Mod dbl, "ui Mod dbl")
#If UInt32_ERRORS
            M(ui Mod chr, "ui Mod chr")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Double
            M(ui Mod str, "ui Mod str")
#End If
#If UInt32_ERRORS
            M(ui Mod dt, "ui Mod dt")
#End If
#If UInt32_ERRORS
            M(ui Mod dbnull, "ui Mod dbnull")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Object
            M(ui Mod obj, "ui Mod obj")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Int64
            M(l Mod bool, "l Mod bool")
#End If
            expected_tc = TypeCode.Int64
            M(l Mod b, "l Mod b")
            expected_tc = TypeCode.Int64
            M(l Mod sb, "l Mod sb")
            expected_tc = TypeCode.Int64
            M(l Mod s, "l Mod s")
            expected_tc = TypeCode.Int64
            M(l Mod us, "l Mod us")
            expected_tc = TypeCode.Int64
            M(l Mod i, "l Mod i")
            expected_tc = TypeCode.Int64
            M(l Mod ui, "l Mod ui")
            expected_tc = TypeCode.Int64
            M(l Mod l, "l Mod l")
            expected_tc = TypeCode.Decimal
            M(l Mod ul, "l Mod ul")
            expected_tc = TypeCode.Decimal
            M(l Mod dec, "l Mod dec")
            expected_tc = TypeCode.Single
            M(l Mod sng, "l Mod sng")
            expected_tc = TypeCode.Double
            M(l Mod dbl, "l Mod dbl")
#If Int64_ERRORS
            M(l Mod chr, "l Mod chr")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Double
            M(l Mod str, "l Mod str")
#End If
#If Int64_ERRORS
            M(l Mod dt, "l Mod dt")
#End If
#If Int64_ERRORS
            M(l Mod dbnull, "l Mod dbnull")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Object
            M(l Mod obj, "l Mod obj")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Decimal
            M(ul Mod bool, "ul Mod bool")
#End If
            expected_tc = TypeCode.UInt64
            M(ul Mod b, "ul Mod b")
            expected_tc = TypeCode.Decimal
            M(ul Mod sb, "ul Mod sb")
            expected_tc = TypeCode.Decimal
            M(ul Mod s, "ul Mod s")
            expected_tc = TypeCode.UInt64
            M(ul Mod us, "ul Mod us")
            expected_tc = TypeCode.Decimal
            M(ul Mod i, "ul Mod i")
            expected_tc = TypeCode.UInt64
            M(ul Mod ui, "ul Mod ui")
            expected_tc = TypeCode.Decimal
            M(ul Mod l, "ul Mod l")
            expected_tc = TypeCode.UInt64
            M(ul Mod ul, "ul Mod ul")
            expected_tc = TypeCode.Decimal
            M(ul Mod dec, "ul Mod dec")
            expected_tc = TypeCode.Single
            M(ul Mod sng, "ul Mod sng")
            expected_tc = TypeCode.Double
            M(ul Mod dbl, "ul Mod dbl")
#If UInt64_ERRORS
            M(ul Mod chr, "ul Mod chr")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Double
            M(ul Mod str, "ul Mod str")
#End If
#If UInt64_ERRORS
            M(ul Mod dt, "ul Mod dt")
#End If
#If UInt64_ERRORS
            M(ul Mod dbnull, "ul Mod dbnull")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Object
            M(ul Mod obj, "ul Mod obj")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Decimal
            M(dec Mod bool, "dec Mod bool")
#End If
            expected_tc = TypeCode.Decimal
            M(dec Mod b, "dec Mod b")
            expected_tc = TypeCode.Decimal
            M(dec Mod sb, "dec Mod sb")
            expected_tc = TypeCode.Decimal
            M(dec Mod s, "dec Mod s")
            expected_tc = TypeCode.Decimal
            M(dec Mod us, "dec Mod us")
            expected_tc = TypeCode.Decimal
            M(dec Mod i, "dec Mod i")
            expected_tc = TypeCode.Decimal
            M(dec Mod ui, "dec Mod ui")
            expected_tc = TypeCode.Decimal
            M(dec Mod l, "dec Mod l")
            expected_tc = TypeCode.Decimal
            M(dec Mod ul, "dec Mod ul")
            expected_tc = TypeCode.Decimal
            M(dec Mod dec, "dec Mod dec")
            expected_tc = TypeCode.Single
            M(dec Mod sng, "dec Mod sng")
            expected_tc = TypeCode.Double
            M(dec Mod dbl, "dec Mod dbl")
#If Decimal_ERRORS
            M(dec Mod chr, "dec Mod chr")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Double
            M(dec Mod str, "dec Mod str")
#End If
#If Decimal_ERRORS
            M(dec Mod dt, "dec Mod dt")
#End If
#If Decimal_ERRORS
            M(dec Mod dbnull, "dec Mod dbnull")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Object
            M(dec Mod obj, "dec Mod obj")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Single
            M(sng Mod bool, "sng Mod bool")
#End If
            expected_tc = TypeCode.Single
            M(sng Mod b, "sng Mod b")
            expected_tc = TypeCode.Single
            M(sng Mod sb, "sng Mod sb")
            expected_tc = TypeCode.Single
            M(sng Mod s, "sng Mod s")
            expected_tc = TypeCode.Single
            M(sng Mod us, "sng Mod us")
            expected_tc = TypeCode.Single
            M(sng Mod i, "sng Mod i")
            expected_tc = TypeCode.Single
            M(sng Mod ui, "sng Mod ui")
            expected_tc = TypeCode.Single
            M(sng Mod l, "sng Mod l")
            expected_tc = TypeCode.Single
            M(sng Mod ul, "sng Mod ul")
            expected_tc = TypeCode.Single
            M(sng Mod dec, "sng Mod dec")
            expected_tc = TypeCode.Single
            M(sng Mod sng, "sng Mod sng")
            expected_tc = TypeCode.Double
            M(sng Mod dbl, "sng Mod dbl")
#If Single_ERRORS
            M(sng Mod chr, "sng Mod chr")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Double
            M(sng Mod str, "sng Mod str")
#End If
#If Single_ERRORS
            M(sng Mod dt, "sng Mod dt")
#End If
#If Single_ERRORS
            M(sng Mod dbnull, "sng Mod dbnull")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Object
            M(sng Mod obj, "sng Mod obj")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Double
            M(dbl Mod bool, "dbl Mod bool")
#End If
            expected_tc = TypeCode.Double
            M(dbl Mod b, "dbl Mod b")
            expected_tc = TypeCode.Double
            M(dbl Mod sb, "dbl Mod sb")
            expected_tc = TypeCode.Double
            M(dbl Mod s, "dbl Mod s")
            expected_tc = TypeCode.Double
            M(dbl Mod us, "dbl Mod us")
            expected_tc = TypeCode.Double
            M(dbl Mod i, "dbl Mod i")
            expected_tc = TypeCode.Double
            M(dbl Mod ui, "dbl Mod ui")
            expected_tc = TypeCode.Double
            M(dbl Mod l, "dbl Mod l")
            expected_tc = TypeCode.Double
            M(dbl Mod ul, "dbl Mod ul")
            expected_tc = TypeCode.Double
            M(dbl Mod dec, "dbl Mod dec")
            expected_tc = TypeCode.Double
            M(dbl Mod sng, "dbl Mod sng")
            expected_tc = TypeCode.Double
            M(dbl Mod dbl, "dbl Mod dbl")
#If Double_ERRORS
            M(dbl Mod chr, "dbl Mod chr")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Double
            M(dbl Mod str, "dbl Mod str")
#End If
#If Double_ERRORS
            M(dbl Mod dt, "dbl Mod dt")
#End If
#If Double_ERRORS
            M(dbl Mod dbnull, "dbl Mod dbnull")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Object
            M(dbl Mod obj, "dbl Mod obj")
#End If
#If Char_ERRORS
            M(chr Mod bool, "chr Mod bool")
#End If
#If Char_ERRORS
            M(chr Mod b, "chr Mod b")
#End If
#If Char_ERRORS
            M(chr Mod sb, "chr Mod sb")
#End If
#If Char_ERRORS
            M(chr Mod s, "chr Mod s")
#End If
#If Char_ERRORS
            M(chr Mod us, "chr Mod us")
#End If
#If Char_ERRORS
            M(chr Mod i, "chr Mod i")
#End If
#If Char_ERRORS
            M(chr Mod ui, "chr Mod ui")
#End If
#If Char_ERRORS
            M(chr Mod l, "chr Mod l")
#End If
#If Char_ERRORS
            M(chr Mod ul, "chr Mod ul")
#End If
#If Char_ERRORS
            M(chr Mod dec, "chr Mod dec")
#End If
#If Char_ERRORS
            M(chr Mod sng, "chr Mod sng")
#End If
#If Char_ERRORS
            M(chr Mod dbl, "chr Mod dbl")
#End If
#If Char_ERRORS
            M(chr Mod chr, "chr Mod chr")
#End If
#If Char_ERRORS
            M(chr Mod str, "chr Mod str")
#End If
#If Char_ERRORS
            M(chr Mod dt, "chr Mod dt")
#End If
#If Char_ERRORS
            M(chr Mod dbnull, "chr Mod dbnull")
#End If
#If Char_ERRORS
            M(chr Mod obj, "chr Mod obj")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Double
            M(str Mod bool, "str Mod bool")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Double
            M(str Mod b, "str Mod b")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Double
            M(str Mod sb, "str Mod sb")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Double
            M(str Mod s, "str Mod s")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Double
            M(str Mod us, "str Mod us")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Double
            M(str Mod i, "str Mod i")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Double
            M(str Mod ui, "str Mod ui")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Double
            M(str Mod l, "str Mod l")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Double
            M(str Mod ul, "str Mod ul")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Double
            M(str Mod dec, "str Mod dec")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Double
            M(str Mod sng, "str Mod sng")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Double
            M(str Mod dbl, "str Mod dbl")
#End If
#If String_ERRORS
            M(str Mod chr, "str Mod chr")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Double
            M(str Mod str, "str Mod str")
#End If
#If String_ERRORS
            M(str Mod dt, "str Mod dt")
#End If
#If String_ERRORS
            M(str Mod dbnull, "str Mod dbnull")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Object
            M(str Mod obj, "str Mod obj")
#End If
#If DateTime_ERRORS
            M(dt Mod bool, "dt Mod bool")
#End If
#If DateTime_ERRORS
            M(dt Mod b, "dt Mod b")
#End If
#If DateTime_ERRORS
            M(dt Mod sb, "dt Mod sb")
#End If
#If DateTime_ERRORS
            M(dt Mod s, "dt Mod s")
#End If
#If DateTime_ERRORS
            M(dt Mod us, "dt Mod us")
#End If
#If DateTime_ERRORS
            M(dt Mod i, "dt Mod i")
#End If
#If DateTime_ERRORS
            M(dt Mod ui, "dt Mod ui")
#End If
#If DateTime_ERRORS
            M(dt Mod l, "dt Mod l")
#End If
#If DateTime_ERRORS
            M(dt Mod ul, "dt Mod ul")
#End If
#If DateTime_ERRORS
            M(dt Mod dec, "dt Mod dec")
#End If
#If DateTime_ERRORS
            M(dt Mod sng, "dt Mod sng")
#End If
#If DateTime_ERRORS
            M(dt Mod dbl, "dt Mod dbl")
#End If
#If DateTime_ERRORS
            M(dt Mod chr, "dt Mod chr")
#End If
#If DateTime_ERRORS
            M(dt Mod str, "dt Mod str")
#End If
#If DateTime_ERRORS
            M(dt Mod dt, "dt Mod dt")
#End If
#If DateTime_ERRORS
            M(dt Mod dbnull, "dt Mod dbnull")
#End If
#If DateTime_ERRORS
            M(dt Mod obj, "dt Mod obj")
#End If
#If DBNull_ERRORS
            M(dbnull Mod bool, "dbnull Mod bool")
#End If
#If DBNull_ERRORS
            M(dbnull Mod b, "dbnull Mod b")
#End If
#If DBNull_ERRORS
            M(dbnull Mod sb, "dbnull Mod sb")
#End If
#If DBNull_ERRORS
            M(dbnull Mod s, "dbnull Mod s")
#End If
#If DBNull_ERRORS
            M(dbnull Mod us, "dbnull Mod us")
#End If
#If DBNull_ERRORS
            M(dbnull Mod i, "dbnull Mod i")
#End If
#If DBNull_ERRORS
            M(dbnull Mod ui, "dbnull Mod ui")
#End If
#If DBNull_ERRORS
            M(dbnull Mod l, "dbnull Mod l")
#End If
#If DBNull_ERRORS
            M(dbnull Mod ul, "dbnull Mod ul")
#End If
#If DBNull_ERRORS
            M(dbnull Mod dec, "dbnull Mod dec")
#End If
#If DBNull_ERRORS
            M(dbnull Mod sng, "dbnull Mod sng")
#End If
#If DBNull_ERRORS
            M(dbnull Mod dbl, "dbnull Mod dbl")
#End If
#If DBNull_ERRORS
            M(dbnull Mod chr, "dbnull Mod chr")
#End If
#If DBNull_ERRORS
            M(dbnull Mod str, "dbnull Mod str")
#End If
#If DBNull_ERRORS
            M(dbnull Mod dt, "dbnull Mod dt")
#End If
#If DBNull_ERRORS
            M(dbnull Mod dbnull, "dbnull Mod dbnull")
#End If
#If DBNull_ERRORS
            M(dbnull Mod obj, "dbnull Mod obj")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj Mod bool, "obj Mod bool")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj Mod b, "obj Mod b")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj Mod sb, "obj Mod sb")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj Mod s, "obj Mod s")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj Mod us, "obj Mod us")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj Mod i, "obj Mod i")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj Mod ui, "obj Mod ui")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj Mod l, "obj Mod l")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj Mod ul, "obj Mod ul")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj Mod dec, "obj Mod dec")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj Mod sng, "obj Mod sng")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj Mod dbl, "obj Mod dbl")
#End If
#If Object_ERRORS
            M(obj Mod chr, "obj Mod chr")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj Mod str, "obj Mod str")
#End If
#If Object_ERRORS
            M(obj Mod dt, "obj Mod dt")
#End If
#If Object_ERRORS
            M(obj Mod dbnull, "obj Mod dbnull")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj Mod obj, "obj Mod obj")
#End If
        If failures > 0 Then Return 1
    Catch ex As Exception
        Console.WriteLine ("Exception: {0}", ex)
        Return 2
    End Try
    Return 0
    End Function
End Class
