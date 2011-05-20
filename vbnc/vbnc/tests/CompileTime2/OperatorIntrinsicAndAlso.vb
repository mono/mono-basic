Class BinaryOperatorAndAlso
    Inherits IntrinsicOperatorTests
    Shared Function Main As Integer
        Try
            expected_tc = TypeCode.Boolean
            M(bool AndAlso bool, "bool AndAlso bool")
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Boolean
            M(bool AndAlso b, "bool AndAlso b")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Boolean
            M(bool AndAlso sb, "bool AndAlso sb")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Boolean
            M(bool AndAlso s, "bool AndAlso s")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Boolean
            M(bool AndAlso us, "bool AndAlso us")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Boolean
            M(bool AndAlso i, "bool AndAlso i")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Boolean
            M(bool AndAlso ui, "bool AndAlso ui")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Boolean
            M(bool AndAlso l, "bool AndAlso l")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Boolean
            M(bool AndAlso ul, "bool AndAlso ul")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Boolean
            M(bool AndAlso dec, "bool AndAlso dec")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Boolean
            M(bool AndAlso sng, "bool AndAlso sng")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Boolean
            M(bool AndAlso dbl, "bool AndAlso dbl")
#End If
#If Boolean_ERRORS
            M(bool AndAlso chr, "bool AndAlso chr")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Boolean
            M(bool AndAlso str, "bool AndAlso str")
#End If
#If Boolean_ERRORS
            M(bool AndAlso dt, "bool AndAlso dt")
#End If
#If Boolean_ERRORS
            M(bool AndAlso dbnull, "bool AndAlso dbnull")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Object
            M(bool AndAlso obj, "bool AndAlso obj")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b AndAlso bool, "b AndAlso bool")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b AndAlso b, "b AndAlso b")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b AndAlso sb, "b AndAlso sb")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b AndAlso s, "b AndAlso s")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b AndAlso us, "b AndAlso us")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b AndAlso i, "b AndAlso i")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b AndAlso ui, "b AndAlso ui")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b AndAlso l, "b AndAlso l")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b AndAlso ul, "b AndAlso ul")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b AndAlso dec, "b AndAlso dec")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b AndAlso sng, "b AndAlso sng")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b AndAlso dbl, "b AndAlso dbl")
#End If
#If Byte_ERRORS
            M(b AndAlso chr, "b AndAlso chr")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b AndAlso str, "b AndAlso str")
#End If
#If Byte_ERRORS
            M(b AndAlso dt, "b AndAlso dt")
#End If
#If Byte_ERRORS
            M(b AndAlso dbnull, "b AndAlso dbnull")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Object
            M(b AndAlso obj, "b AndAlso obj")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb AndAlso bool, "sb AndAlso bool")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb AndAlso b, "sb AndAlso b")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb AndAlso sb, "sb AndAlso sb")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb AndAlso s, "sb AndAlso s")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb AndAlso us, "sb AndAlso us")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb AndAlso i, "sb AndAlso i")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb AndAlso ui, "sb AndAlso ui")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb AndAlso l, "sb AndAlso l")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb AndAlso ul, "sb AndAlso ul")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb AndAlso dec, "sb AndAlso dec")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb AndAlso sng, "sb AndAlso sng")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb AndAlso dbl, "sb AndAlso dbl")
#End If
#If SByte_ERRORS
            M(sb AndAlso chr, "sb AndAlso chr")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb AndAlso str, "sb AndAlso str")
#End If
#If SByte_ERRORS
            M(sb AndAlso dt, "sb AndAlso dt")
#End If
#If SByte_ERRORS
            M(sb AndAlso dbnull, "sb AndAlso dbnull")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Object
            M(sb AndAlso obj, "sb AndAlso obj")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s AndAlso bool, "s AndAlso bool")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s AndAlso b, "s AndAlso b")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s AndAlso sb, "s AndAlso sb")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s AndAlso s, "s AndAlso s")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s AndAlso us, "s AndAlso us")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s AndAlso i, "s AndAlso i")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s AndAlso ui, "s AndAlso ui")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s AndAlso l, "s AndAlso l")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s AndAlso ul, "s AndAlso ul")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s AndAlso dec, "s AndAlso dec")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s AndAlso sng, "s AndAlso sng")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s AndAlso dbl, "s AndAlso dbl")
#End If
#If Int16_ERRORS
            M(s AndAlso chr, "s AndAlso chr")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s AndAlso str, "s AndAlso str")
#End If
#If Int16_ERRORS
            M(s AndAlso dt, "s AndAlso dt")
#End If
#If Int16_ERRORS
            M(s AndAlso dbnull, "s AndAlso dbnull")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Object
            M(s AndAlso obj, "s AndAlso obj")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us AndAlso bool, "us AndAlso bool")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us AndAlso b, "us AndAlso b")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us AndAlso sb, "us AndAlso sb")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us AndAlso s, "us AndAlso s")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us AndAlso us, "us AndAlso us")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us AndAlso i, "us AndAlso i")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us AndAlso ui, "us AndAlso ui")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us AndAlso l, "us AndAlso l")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us AndAlso ul, "us AndAlso ul")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us AndAlso dec, "us AndAlso dec")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us AndAlso sng, "us AndAlso sng")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us AndAlso dbl, "us AndAlso dbl")
#End If
#If UInt16_ERRORS
            M(us AndAlso chr, "us AndAlso chr")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us AndAlso str, "us AndAlso str")
#End If
#If UInt16_ERRORS
            M(us AndAlso dt, "us AndAlso dt")
#End If
#If UInt16_ERRORS
            M(us AndAlso dbnull, "us AndAlso dbnull")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Object
            M(us AndAlso obj, "us AndAlso obj")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i AndAlso bool, "i AndAlso bool")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i AndAlso b, "i AndAlso b")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i AndAlso sb, "i AndAlso sb")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i AndAlso s, "i AndAlso s")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i AndAlso us, "i AndAlso us")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i AndAlso i, "i AndAlso i")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i AndAlso ui, "i AndAlso ui")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i AndAlso l, "i AndAlso l")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i AndAlso ul, "i AndAlso ul")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i AndAlso dec, "i AndAlso dec")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i AndAlso sng, "i AndAlso sng")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i AndAlso dbl, "i AndAlso dbl")
#End If
#If Int32_ERRORS
            M(i AndAlso chr, "i AndAlso chr")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i AndAlso str, "i AndAlso str")
#End If
#If Int32_ERRORS
            M(i AndAlso dt, "i AndAlso dt")
#End If
#If Int32_ERRORS
            M(i AndAlso dbnull, "i AndAlso dbnull")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Object
            M(i AndAlso obj, "i AndAlso obj")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui AndAlso bool, "ui AndAlso bool")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui AndAlso b, "ui AndAlso b")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui AndAlso sb, "ui AndAlso sb")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui AndAlso s, "ui AndAlso s")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui AndAlso us, "ui AndAlso us")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui AndAlso i, "ui AndAlso i")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui AndAlso ui, "ui AndAlso ui")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui AndAlso l, "ui AndAlso l")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui AndAlso ul, "ui AndAlso ul")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui AndAlso dec, "ui AndAlso dec")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui AndAlso sng, "ui AndAlso sng")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui AndAlso dbl, "ui AndAlso dbl")
#End If
#If UInt32_ERRORS
            M(ui AndAlso chr, "ui AndAlso chr")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui AndAlso str, "ui AndAlso str")
#End If
#If UInt32_ERRORS
            M(ui AndAlso dt, "ui AndAlso dt")
#End If
#If UInt32_ERRORS
            M(ui AndAlso dbnull, "ui AndAlso dbnull")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Object
            M(ui AndAlso obj, "ui AndAlso obj")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l AndAlso bool, "l AndAlso bool")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l AndAlso b, "l AndAlso b")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l AndAlso sb, "l AndAlso sb")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l AndAlso s, "l AndAlso s")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l AndAlso us, "l AndAlso us")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l AndAlso i, "l AndAlso i")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l AndAlso ui, "l AndAlso ui")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l AndAlso l, "l AndAlso l")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l AndAlso ul, "l AndAlso ul")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l AndAlso dec, "l AndAlso dec")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l AndAlso sng, "l AndAlso sng")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l AndAlso dbl, "l AndAlso dbl")
#End If
#If Int64_ERRORS
            M(l AndAlso chr, "l AndAlso chr")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l AndAlso str, "l AndAlso str")
#End If
#If Int64_ERRORS
            M(l AndAlso dt, "l AndAlso dt")
#End If
#If Int64_ERRORS
            M(l AndAlso dbnull, "l AndAlso dbnull")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Object
            M(l AndAlso obj, "l AndAlso obj")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul AndAlso bool, "ul AndAlso bool")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul AndAlso b, "ul AndAlso b")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul AndAlso sb, "ul AndAlso sb")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul AndAlso s, "ul AndAlso s")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul AndAlso us, "ul AndAlso us")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul AndAlso i, "ul AndAlso i")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul AndAlso ui, "ul AndAlso ui")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul AndAlso l, "ul AndAlso l")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul AndAlso ul, "ul AndAlso ul")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul AndAlso dec, "ul AndAlso dec")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul AndAlso sng, "ul AndAlso sng")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul AndAlso dbl, "ul AndAlso dbl")
#End If
#If UInt64_ERRORS
            M(ul AndAlso chr, "ul AndAlso chr")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul AndAlso str, "ul AndAlso str")
#End If
#If UInt64_ERRORS
            M(ul AndAlso dt, "ul AndAlso dt")
#End If
#If UInt64_ERRORS
            M(ul AndAlso dbnull, "ul AndAlso dbnull")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Object
            M(ul AndAlso obj, "ul AndAlso obj")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec AndAlso bool, "dec AndAlso bool")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec AndAlso b, "dec AndAlso b")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec AndAlso sb, "dec AndAlso sb")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec AndAlso s, "dec AndAlso s")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec AndAlso us, "dec AndAlso us")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec AndAlso i, "dec AndAlso i")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec AndAlso ui, "dec AndAlso ui")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec AndAlso l, "dec AndAlso l")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec AndAlso ul, "dec AndAlso ul")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec AndAlso dec, "dec AndAlso dec")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec AndAlso sng, "dec AndAlso sng")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec AndAlso dbl, "dec AndAlso dbl")
#End If
#If Decimal_ERRORS
            M(dec AndAlso chr, "dec AndAlso chr")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec AndAlso str, "dec AndAlso str")
#End If
#If Decimal_ERRORS
            M(dec AndAlso dt, "dec AndAlso dt")
#End If
#If Decimal_ERRORS
            M(dec AndAlso dbnull, "dec AndAlso dbnull")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Object
            M(dec AndAlso obj, "dec AndAlso obj")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng AndAlso bool, "sng AndAlso bool")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng AndAlso b, "sng AndAlso b")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng AndAlso sb, "sng AndAlso sb")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng AndAlso s, "sng AndAlso s")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng AndAlso us, "sng AndAlso us")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng AndAlso i, "sng AndAlso i")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng AndAlso ui, "sng AndAlso ui")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng AndAlso l, "sng AndAlso l")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng AndAlso ul, "sng AndAlso ul")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng AndAlso dec, "sng AndAlso dec")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng AndAlso sng, "sng AndAlso sng")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng AndAlso dbl, "sng AndAlso dbl")
#End If
#If Single_ERRORS
            M(sng AndAlso chr, "sng AndAlso chr")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng AndAlso str, "sng AndAlso str")
#End If
#If Single_ERRORS
            M(sng AndAlso dt, "sng AndAlso dt")
#End If
#If Single_ERRORS
            M(sng AndAlso dbnull, "sng AndAlso dbnull")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Object
            M(sng AndAlso obj, "sng AndAlso obj")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl AndAlso bool, "dbl AndAlso bool")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl AndAlso b, "dbl AndAlso b")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl AndAlso sb, "dbl AndAlso sb")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl AndAlso s, "dbl AndAlso s")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl AndAlso us, "dbl AndAlso us")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl AndAlso i, "dbl AndAlso i")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl AndAlso ui, "dbl AndAlso ui")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl AndAlso l, "dbl AndAlso l")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl AndAlso ul, "dbl AndAlso ul")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl AndAlso dec, "dbl AndAlso dec")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl AndAlso sng, "dbl AndAlso sng")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl AndAlso dbl, "dbl AndAlso dbl")
#End If
#If Double_ERRORS
            M(dbl AndAlso chr, "dbl AndAlso chr")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl AndAlso str, "dbl AndAlso str")
#End If
#If Double_ERRORS
            M(dbl AndAlso dt, "dbl AndAlso dt")
#End If
#If Double_ERRORS
            M(dbl AndAlso dbnull, "dbl AndAlso dbnull")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Object
            M(dbl AndAlso obj, "dbl AndAlso obj")
#End If
#If Char_ERRORS
            M(chr AndAlso bool, "chr AndAlso bool")
#End If
#If Char_ERRORS
            M(chr AndAlso b, "chr AndAlso b")
#End If
#If Char_ERRORS
            M(chr AndAlso sb, "chr AndAlso sb")
#End If
#If Char_ERRORS
            M(chr AndAlso s, "chr AndAlso s")
#End If
#If Char_ERRORS
            M(chr AndAlso us, "chr AndAlso us")
#End If
#If Char_ERRORS
            M(chr AndAlso i, "chr AndAlso i")
#End If
#If Char_ERRORS
            M(chr AndAlso ui, "chr AndAlso ui")
#End If
#If Char_ERRORS
            M(chr AndAlso l, "chr AndAlso l")
#End If
#If Char_ERRORS
            M(chr AndAlso ul, "chr AndAlso ul")
#End If
#If Char_ERRORS
            M(chr AndAlso dec, "chr AndAlso dec")
#End If
#If Char_ERRORS
            M(chr AndAlso sng, "chr AndAlso sng")
#End If
#If Char_ERRORS
            M(chr AndAlso dbl, "chr AndAlso dbl")
#End If
#If Char_ERRORS
            M(chr AndAlso chr, "chr AndAlso chr")
#End If
#If Char_ERRORS
            M(chr AndAlso str, "chr AndAlso str")
#End If
#If Char_ERRORS
            M(chr AndAlso dt, "chr AndAlso dt")
#End If
#If Char_ERRORS
            M(chr AndAlso dbnull, "chr AndAlso dbnull")
#End If
#If Char_ERRORS
            M(chr AndAlso obj, "chr AndAlso obj")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Boolean
            M(str AndAlso bool, "str AndAlso bool")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Boolean
            M(str AndAlso b, "str AndAlso b")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Boolean
            M(str AndAlso sb, "str AndAlso sb")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Boolean
            M(str AndAlso s, "str AndAlso s")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Boolean
            M(str AndAlso us, "str AndAlso us")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Boolean
            M(str AndAlso i, "str AndAlso i")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Boolean
            M(str AndAlso ui, "str AndAlso ui")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Boolean
            M(str AndAlso l, "str AndAlso l")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Boolean
            M(str AndAlso ul, "str AndAlso ul")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Boolean
            M(str AndAlso dec, "str AndAlso dec")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Boolean
            M(str AndAlso sng, "str AndAlso sng")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Boolean
            M(str AndAlso dbl, "str AndAlso dbl")
#End If
#If String_ERRORS
            M(str AndAlso chr, "str AndAlso chr")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Boolean
            M(str AndAlso str, "str AndAlso str")
#End If
#If String_ERRORS
            M(str AndAlso dt, "str AndAlso dt")
#End If
#If String_ERRORS
            M(str AndAlso dbnull, "str AndAlso dbnull")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Object
            M(str AndAlso obj, "str AndAlso obj")
#End If
#If DateTime_ERRORS
            M(dt AndAlso bool, "dt AndAlso bool")
#End If
#If DateTime_ERRORS
            M(dt AndAlso b, "dt AndAlso b")
#End If
#If DateTime_ERRORS
            M(dt AndAlso sb, "dt AndAlso sb")
#End If
#If DateTime_ERRORS
            M(dt AndAlso s, "dt AndAlso s")
#End If
#If DateTime_ERRORS
            M(dt AndAlso us, "dt AndAlso us")
#End If
#If DateTime_ERRORS
            M(dt AndAlso i, "dt AndAlso i")
#End If
#If DateTime_ERRORS
            M(dt AndAlso ui, "dt AndAlso ui")
#End If
#If DateTime_ERRORS
            M(dt AndAlso l, "dt AndAlso l")
#End If
#If DateTime_ERRORS
            M(dt AndAlso ul, "dt AndAlso ul")
#End If
#If DateTime_ERRORS
            M(dt AndAlso dec, "dt AndAlso dec")
#End If
#If DateTime_ERRORS
            M(dt AndAlso sng, "dt AndAlso sng")
#End If
#If DateTime_ERRORS
            M(dt AndAlso dbl, "dt AndAlso dbl")
#End If
#If DateTime_ERRORS
            M(dt AndAlso chr, "dt AndAlso chr")
#End If
#If DateTime_ERRORS
            M(dt AndAlso str, "dt AndAlso str")
#End If
#If DateTime_ERRORS
            M(dt AndAlso dt, "dt AndAlso dt")
#End If
#If DateTime_ERRORS
            M(dt AndAlso dbnull, "dt AndAlso dbnull")
#End If
#If DateTime_ERRORS
            M(dt AndAlso obj, "dt AndAlso obj")
#End If
#If DBNull_ERRORS
            M(dbnull AndAlso bool, "dbnull AndAlso bool")
#End If
#If DBNull_ERRORS
            M(dbnull AndAlso b, "dbnull AndAlso b")
#End If
#If DBNull_ERRORS
            M(dbnull AndAlso sb, "dbnull AndAlso sb")
#End If
#If DBNull_ERRORS
            M(dbnull AndAlso s, "dbnull AndAlso s")
#End If
#If DBNull_ERRORS
            M(dbnull AndAlso us, "dbnull AndAlso us")
#End If
#If DBNull_ERRORS
            M(dbnull AndAlso i, "dbnull AndAlso i")
#End If
#If DBNull_ERRORS
            M(dbnull AndAlso ui, "dbnull AndAlso ui")
#End If
#If DBNull_ERRORS
            M(dbnull AndAlso l, "dbnull AndAlso l")
#End If
#If DBNull_ERRORS
            M(dbnull AndAlso ul, "dbnull AndAlso ul")
#End If
#If DBNull_ERRORS
            M(dbnull AndAlso dec, "dbnull AndAlso dec")
#End If
#If DBNull_ERRORS
            M(dbnull AndAlso sng, "dbnull AndAlso sng")
#End If
#If DBNull_ERRORS
            M(dbnull AndAlso dbl, "dbnull AndAlso dbl")
#End If
#If DBNull_ERRORS
            M(dbnull AndAlso chr, "dbnull AndAlso chr")
#End If
#If DBNull_ERRORS
            M(dbnull AndAlso str, "dbnull AndAlso str")
#End If
#If DBNull_ERRORS
            M(dbnull AndAlso dt, "dbnull AndAlso dt")
#End If
#If DBNull_ERRORS
            M(dbnull AndAlso dbnull, "dbnull AndAlso dbnull")
#End If
#If DBNull_ERRORS
            M(dbnull AndAlso obj, "dbnull AndAlso obj")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj AndAlso bool, "obj AndAlso bool")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj AndAlso b, "obj AndAlso b")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj AndAlso sb, "obj AndAlso sb")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj AndAlso s, "obj AndAlso s")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj AndAlso us, "obj AndAlso us")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj AndAlso i, "obj AndAlso i")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj AndAlso ui, "obj AndAlso ui")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj AndAlso l, "obj AndAlso l")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj AndAlso ul, "obj AndAlso ul")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj AndAlso dec, "obj AndAlso dec")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj AndAlso sng, "obj AndAlso sng")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj AndAlso dbl, "obj AndAlso dbl")
#End If
#If Object_ERRORS
            M(obj AndAlso chr, "obj AndAlso chr")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj AndAlso str, "obj AndAlso str")
#End If
#If Object_ERRORS
            M(obj AndAlso dt, "obj AndAlso dt")
#End If
#If Object_ERRORS
            M(obj AndAlso dbnull, "obj AndAlso dbnull")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj AndAlso obj, "obj AndAlso obj")
#End If
        If failures > 0 Then Return 1
    Catch ex As Exception
        Console.WriteLine ("Exception: {0}", ex)
        Return 2
    End Try
    Return 0
    End Function
End Class
