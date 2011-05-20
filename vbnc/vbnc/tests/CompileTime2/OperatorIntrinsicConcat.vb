Class BinaryOperatorConcat
    Inherits IntrinsicOperatorTests
    Shared Function Main As Integer
        Try
            expected_tc = TypeCode.String
            M(bool & bool, "bool & bool")
            expected_tc = TypeCode.String
            M(bool & b, "bool & b")
            expected_tc = TypeCode.String
            M(bool & sb, "bool & sb")
            expected_tc = TypeCode.String
            M(bool & s, "bool & s")
            expected_tc = TypeCode.String
            M(bool & us, "bool & us")
            expected_tc = TypeCode.String
            M(bool & i, "bool & i")
            expected_tc = TypeCode.String
            M(bool & ui, "bool & ui")
            expected_tc = TypeCode.String
            M(bool & l, "bool & l")
            expected_tc = TypeCode.String
            M(bool & ul, "bool & ul")
            expected_tc = TypeCode.String
            M(bool & dec, "bool & dec")
            expected_tc = TypeCode.String
            M(bool & sng, "bool & sng")
            expected_tc = TypeCode.String
            M(bool & dbl, "bool & dbl")
            expected_tc = TypeCode.String
            M(bool & chr, "bool & chr")
            expected_tc = TypeCode.String
            M(bool & str, "bool & str")
            expected_tc = TypeCode.String
            M(bool & dt, "bool & dt")
#If Boolean_ERRORS
            M(bool & dbnull, "bool & dbnull")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Object
            M(bool & obj, "bool & obj")
#End If
            expected_tc = TypeCode.String
            M(b & bool, "b & bool")
            expected_tc = TypeCode.String
            M(b & b, "b & b")
            expected_tc = TypeCode.String
            M(b & sb, "b & sb")
            expected_tc = TypeCode.String
            M(b & s, "b & s")
            expected_tc = TypeCode.String
            M(b & us, "b & us")
            expected_tc = TypeCode.String
            M(b & i, "b & i")
            expected_tc = TypeCode.String
            M(b & ui, "b & ui")
            expected_tc = TypeCode.String
            M(b & l, "b & l")
            expected_tc = TypeCode.String
            M(b & ul, "b & ul")
            expected_tc = TypeCode.String
            M(b & dec, "b & dec")
            expected_tc = TypeCode.String
            M(b & sng, "b & sng")
            expected_tc = TypeCode.String
            M(b & dbl, "b & dbl")
            expected_tc = TypeCode.String
            M(b & chr, "b & chr")
            expected_tc = TypeCode.String
            M(b & str, "b & str")
            expected_tc = TypeCode.String
            M(b & dt, "b & dt")
#If Byte_ERRORS
            M(b & dbnull, "b & dbnull")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Object
            M(b & obj, "b & obj")
#End If
            expected_tc = TypeCode.String
            M(sb & bool, "sb & bool")
            expected_tc = TypeCode.String
            M(sb & b, "sb & b")
            expected_tc = TypeCode.String
            M(sb & sb, "sb & sb")
            expected_tc = TypeCode.String
            M(sb & s, "sb & s")
            expected_tc = TypeCode.String
            M(sb & us, "sb & us")
            expected_tc = TypeCode.String
            M(sb & i, "sb & i")
            expected_tc = TypeCode.String
            M(sb & ui, "sb & ui")
            expected_tc = TypeCode.String
            M(sb & l, "sb & l")
            expected_tc = TypeCode.String
            M(sb & ul, "sb & ul")
            expected_tc = TypeCode.String
            M(sb & dec, "sb & dec")
            expected_tc = TypeCode.String
            M(sb & sng, "sb & sng")
            expected_tc = TypeCode.String
            M(sb & dbl, "sb & dbl")
            expected_tc = TypeCode.String
            M(sb & chr, "sb & chr")
            expected_tc = TypeCode.String
            M(sb & str, "sb & str")
            expected_tc = TypeCode.String
            M(sb & dt, "sb & dt")
#If SByte_ERRORS
            M(sb & dbnull, "sb & dbnull")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Object
            M(sb & obj, "sb & obj")
#End If
            expected_tc = TypeCode.String
            M(s & bool, "s & bool")
            expected_tc = TypeCode.String
            M(s & b, "s & b")
            expected_tc = TypeCode.String
            M(s & sb, "s & sb")
            expected_tc = TypeCode.String
            M(s & s, "s & s")
            expected_tc = TypeCode.String
            M(s & us, "s & us")
            expected_tc = TypeCode.String
            M(s & i, "s & i")
            expected_tc = TypeCode.String
            M(s & ui, "s & ui")
            expected_tc = TypeCode.String
            M(s & l, "s & l")
            expected_tc = TypeCode.String
            M(s & ul, "s & ul")
            expected_tc = TypeCode.String
            M(s & dec, "s & dec")
            expected_tc = TypeCode.String
            M(s & sng, "s & sng")
            expected_tc = TypeCode.String
            M(s & dbl, "s & dbl")
            expected_tc = TypeCode.String
            M(s & chr, "s & chr")
            expected_tc = TypeCode.String
            M(s & str, "s & str")
            expected_tc = TypeCode.String
            M(s & dt, "s & dt")
#If Int16_ERRORS
            M(s & dbnull, "s & dbnull")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Object
            M(s & obj, "s & obj")
#End If
            expected_tc = TypeCode.String
            M(us & bool, "us & bool")
            expected_tc = TypeCode.String
            M(us & b, "us & b")
            expected_tc = TypeCode.String
            M(us & sb, "us & sb")
            expected_tc = TypeCode.String
            M(us & s, "us & s")
            expected_tc = TypeCode.String
            M(us & us, "us & us")
            expected_tc = TypeCode.String
            M(us & i, "us & i")
            expected_tc = TypeCode.String
            M(us & ui, "us & ui")
            expected_tc = TypeCode.String
            M(us & l, "us & l")
            expected_tc = TypeCode.String
            M(us & ul, "us & ul")
            expected_tc = TypeCode.String
            M(us & dec, "us & dec")
            expected_tc = TypeCode.String
            M(us & sng, "us & sng")
            expected_tc = TypeCode.String
            M(us & dbl, "us & dbl")
            expected_tc = TypeCode.String
            M(us & chr, "us & chr")
            expected_tc = TypeCode.String
            M(us & str, "us & str")
            expected_tc = TypeCode.String
            M(us & dt, "us & dt")
#If UInt16_ERRORS
            M(us & dbnull, "us & dbnull")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Object
            M(us & obj, "us & obj")
#End If
            expected_tc = TypeCode.String
            M(i & bool, "i & bool")
            expected_tc = TypeCode.String
            M(i & b, "i & b")
            expected_tc = TypeCode.String
            M(i & sb, "i & sb")
            expected_tc = TypeCode.String
            M(i & s, "i & s")
            expected_tc = TypeCode.String
            M(i & us, "i & us")
            expected_tc = TypeCode.String
            M(i & i, "i & i")
            expected_tc = TypeCode.String
            M(i & ui, "i & ui")
            expected_tc = TypeCode.String
            M(i & l, "i & l")
            expected_tc = TypeCode.String
            M(i & ul, "i & ul")
            expected_tc = TypeCode.String
            M(i & dec, "i & dec")
            expected_tc = TypeCode.String
            M(i & sng, "i & sng")
            expected_tc = TypeCode.String
            M(i & dbl, "i & dbl")
            expected_tc = TypeCode.String
            M(i & chr, "i & chr")
            expected_tc = TypeCode.String
            M(i & str, "i & str")
            expected_tc = TypeCode.String
            M(i & dt, "i & dt")
#If Int32_ERRORS
            M(i & dbnull, "i & dbnull")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Object
            M(i & obj, "i & obj")
#End If
            expected_tc = TypeCode.String
            M(ui & bool, "ui & bool")
            expected_tc = TypeCode.String
            M(ui & b, "ui & b")
            expected_tc = TypeCode.String
            M(ui & sb, "ui & sb")
            expected_tc = TypeCode.String
            M(ui & s, "ui & s")
            expected_tc = TypeCode.String
            M(ui & us, "ui & us")
            expected_tc = TypeCode.String
            M(ui & i, "ui & i")
            expected_tc = TypeCode.String
            M(ui & ui, "ui & ui")
            expected_tc = TypeCode.String
            M(ui & l, "ui & l")
            expected_tc = TypeCode.String
            M(ui & ul, "ui & ul")
            expected_tc = TypeCode.String
            M(ui & dec, "ui & dec")
            expected_tc = TypeCode.String
            M(ui & sng, "ui & sng")
            expected_tc = TypeCode.String
            M(ui & dbl, "ui & dbl")
            expected_tc = TypeCode.String
            M(ui & chr, "ui & chr")
            expected_tc = TypeCode.String
            M(ui & str, "ui & str")
            expected_tc = TypeCode.String
            M(ui & dt, "ui & dt")
#If UInt32_ERRORS
            M(ui & dbnull, "ui & dbnull")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Object
            M(ui & obj, "ui & obj")
#End If
            expected_tc = TypeCode.String
            M(l & bool, "l & bool")
            expected_tc = TypeCode.String
            M(l & b, "l & b")
            expected_tc = TypeCode.String
            M(l & sb, "l & sb")
            expected_tc = TypeCode.String
            M(l & s, "l & s")
            expected_tc = TypeCode.String
            M(l & us, "l & us")
            expected_tc = TypeCode.String
            M(l & i, "l & i")
            expected_tc = TypeCode.String
            M(l & ui, "l & ui")
            expected_tc = TypeCode.String
            M(l & l, "l & l")
            expected_tc = TypeCode.String
            M(l & ul, "l & ul")
            expected_tc = TypeCode.String
            M(l & dec, "l & dec")
            expected_tc = TypeCode.String
            M(l & sng, "l & sng")
            expected_tc = TypeCode.String
            M(l & dbl, "l & dbl")
            expected_tc = TypeCode.String
            M(l & chr, "l & chr")
            expected_tc = TypeCode.String
            M(l & str, "l & str")
            expected_tc = TypeCode.String
            M(l & dt, "l & dt")
#If Int64_ERRORS
            M(l & dbnull, "l & dbnull")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Object
            M(l & obj, "l & obj")
#End If
            expected_tc = TypeCode.String
            M(ul & bool, "ul & bool")
            expected_tc = TypeCode.String
            M(ul & b, "ul & b")
            expected_tc = TypeCode.String
            M(ul & sb, "ul & sb")
            expected_tc = TypeCode.String
            M(ul & s, "ul & s")
            expected_tc = TypeCode.String
            M(ul & us, "ul & us")
            expected_tc = TypeCode.String
            M(ul & i, "ul & i")
            expected_tc = TypeCode.String
            M(ul & ui, "ul & ui")
            expected_tc = TypeCode.String
            M(ul & l, "ul & l")
            expected_tc = TypeCode.String
            M(ul & ul, "ul & ul")
            expected_tc = TypeCode.String
            M(ul & dec, "ul & dec")
            expected_tc = TypeCode.String
            M(ul & sng, "ul & sng")
            expected_tc = TypeCode.String
            M(ul & dbl, "ul & dbl")
            expected_tc = TypeCode.String
            M(ul & chr, "ul & chr")
            expected_tc = TypeCode.String
            M(ul & str, "ul & str")
            expected_tc = TypeCode.String
            M(ul & dt, "ul & dt")
#If UInt64_ERRORS
            M(ul & dbnull, "ul & dbnull")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Object
            M(ul & obj, "ul & obj")
#End If
            expected_tc = TypeCode.String
            M(dec & bool, "dec & bool")
            expected_tc = TypeCode.String
            M(dec & b, "dec & b")
            expected_tc = TypeCode.String
            M(dec & sb, "dec & sb")
            expected_tc = TypeCode.String
            M(dec & s, "dec & s")
            expected_tc = TypeCode.String
            M(dec & us, "dec & us")
            expected_tc = TypeCode.String
            M(dec & i, "dec & i")
            expected_tc = TypeCode.String
            M(dec & ui, "dec & ui")
            expected_tc = TypeCode.String
            M(dec & l, "dec & l")
            expected_tc = TypeCode.String
            M(dec & ul, "dec & ul")
            expected_tc = TypeCode.String
            M(dec & dec, "dec & dec")
            expected_tc = TypeCode.String
            M(dec & sng, "dec & sng")
            expected_tc = TypeCode.String
            M(dec & dbl, "dec & dbl")
            expected_tc = TypeCode.String
            M(dec & chr, "dec & chr")
            expected_tc = TypeCode.String
            M(dec & str, "dec & str")
            expected_tc = TypeCode.String
            M(dec & dt, "dec & dt")
#If Decimal_ERRORS
            M(dec & dbnull, "dec & dbnull")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Object
            M(dec & obj, "dec & obj")
#End If
            expected_tc = TypeCode.String
            M(sng & bool, "sng & bool")
            expected_tc = TypeCode.String
            M(sng & b, "sng & b")
            expected_tc = TypeCode.String
            M(sng & sb, "sng & sb")
            expected_tc = TypeCode.String
            M(sng & s, "sng & s")
            expected_tc = TypeCode.String
            M(sng & us, "sng & us")
            expected_tc = TypeCode.String
            M(sng & i, "sng & i")
            expected_tc = TypeCode.String
            M(sng & ui, "sng & ui")
            expected_tc = TypeCode.String
            M(sng & l, "sng & l")
            expected_tc = TypeCode.String
            M(sng & ul, "sng & ul")
            expected_tc = TypeCode.String
            M(sng & dec, "sng & dec")
            expected_tc = TypeCode.String
            M(sng & sng, "sng & sng")
            expected_tc = TypeCode.String
            M(sng & dbl, "sng & dbl")
            expected_tc = TypeCode.String
            M(sng & chr, "sng & chr")
            expected_tc = TypeCode.String
            M(sng & str, "sng & str")
            expected_tc = TypeCode.String
            M(sng & dt, "sng & dt")
#If Single_ERRORS
            M(sng & dbnull, "sng & dbnull")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Object
            M(sng & obj, "sng & obj")
#End If
            expected_tc = TypeCode.String
            M(dbl & bool, "dbl & bool")
            expected_tc = TypeCode.String
            M(dbl & b, "dbl & b")
            expected_tc = TypeCode.String
            M(dbl & sb, "dbl & sb")
            expected_tc = TypeCode.String
            M(dbl & s, "dbl & s")
            expected_tc = TypeCode.String
            M(dbl & us, "dbl & us")
            expected_tc = TypeCode.String
            M(dbl & i, "dbl & i")
            expected_tc = TypeCode.String
            M(dbl & ui, "dbl & ui")
            expected_tc = TypeCode.String
            M(dbl & l, "dbl & l")
            expected_tc = TypeCode.String
            M(dbl & ul, "dbl & ul")
            expected_tc = TypeCode.String
            M(dbl & dec, "dbl & dec")
            expected_tc = TypeCode.String
            M(dbl & sng, "dbl & sng")
            expected_tc = TypeCode.String
            M(dbl & dbl, "dbl & dbl")
            expected_tc = TypeCode.String
            M(dbl & chr, "dbl & chr")
            expected_tc = TypeCode.String
            M(dbl & str, "dbl & str")
            expected_tc = TypeCode.String
            M(dbl & dt, "dbl & dt")
#If Double_ERRORS
            M(dbl & dbnull, "dbl & dbnull")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Object
            M(dbl & obj, "dbl & obj")
#End If
            expected_tc = TypeCode.String
            M(chr & bool, "chr & bool")
            expected_tc = TypeCode.String
            M(chr & b, "chr & b")
            expected_tc = TypeCode.String
            M(chr & sb, "chr & sb")
            expected_tc = TypeCode.String
            M(chr & s, "chr & s")
            expected_tc = TypeCode.String
            M(chr & us, "chr & us")
            expected_tc = TypeCode.String
            M(chr & i, "chr & i")
            expected_tc = TypeCode.String
            M(chr & ui, "chr & ui")
            expected_tc = TypeCode.String
            M(chr & l, "chr & l")
            expected_tc = TypeCode.String
            M(chr & ul, "chr & ul")
            expected_tc = TypeCode.String
            M(chr & dec, "chr & dec")
            expected_tc = TypeCode.String
            M(chr & sng, "chr & sng")
            expected_tc = TypeCode.String
            M(chr & dbl, "chr & dbl")
            expected_tc = TypeCode.String
            M(chr & chr, "chr & chr")
            expected_tc = TypeCode.String
            M(chr & str, "chr & str")
            expected_tc = TypeCode.String
            M(chr & dt, "chr & dt")
#If Char_ERRORS
            M(chr & dbnull, "chr & dbnull")
#End If
#If Not STRICT Or Char_ERRORS
            expected_tc = TypeCode.Object
            M(chr & obj, "chr & obj")
#End If
            expected_tc = TypeCode.String
            M(str & bool, "str & bool")
            expected_tc = TypeCode.String
            M(str & b, "str & b")
            expected_tc = TypeCode.String
            M(str & sb, "str & sb")
            expected_tc = TypeCode.String
            M(str & s, "str & s")
            expected_tc = TypeCode.String
            M(str & us, "str & us")
            expected_tc = TypeCode.String
            M(str & i, "str & i")
            expected_tc = TypeCode.String
            M(str & ui, "str & ui")
            expected_tc = TypeCode.String
            M(str & l, "str & l")
            expected_tc = TypeCode.String
            M(str & ul, "str & ul")
            expected_tc = TypeCode.String
            M(str & dec, "str & dec")
            expected_tc = TypeCode.String
            M(str & sng, "str & sng")
            expected_tc = TypeCode.String
            M(str & dbl, "str & dbl")
            expected_tc = TypeCode.String
            M(str & chr, "str & chr")
            expected_tc = TypeCode.String
            M(str & str, "str & str")
            expected_tc = TypeCode.String
            M(str & dt, "str & dt")
#If String_ERRORS
            M(str & dbnull, "str & dbnull")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Object
            M(str & obj, "str & obj")
#End If
            expected_tc = TypeCode.String
            M(dt & bool, "dt & bool")
            expected_tc = TypeCode.String
            M(dt & b, "dt & b")
            expected_tc = TypeCode.String
            M(dt & sb, "dt & sb")
            expected_tc = TypeCode.String
            M(dt & s, "dt & s")
            expected_tc = TypeCode.String
            M(dt & us, "dt & us")
            expected_tc = TypeCode.String
            M(dt & i, "dt & i")
            expected_tc = TypeCode.String
            M(dt & ui, "dt & ui")
            expected_tc = TypeCode.String
            M(dt & l, "dt & l")
            expected_tc = TypeCode.String
            M(dt & ul, "dt & ul")
            expected_tc = TypeCode.String
            M(dt & dec, "dt & dec")
            expected_tc = TypeCode.String
            M(dt & sng, "dt & sng")
            expected_tc = TypeCode.String
            M(dt & dbl, "dt & dbl")
            expected_tc = TypeCode.String
            M(dt & chr, "dt & chr")
            expected_tc = TypeCode.String
            M(dt & str, "dt & str")
            expected_tc = TypeCode.String
            M(dt & dt, "dt & dt")
#If DateTime_ERRORS
            M(dt & dbnull, "dt & dbnull")
#End If
#If Not STRICT Or DateTime_ERRORS
            expected_tc = TypeCode.Object
            M(dt & obj, "dt & obj")
#End If
#If DBNull_ERRORS
            M(dbnull & bool, "dbnull & bool")
#End If
#If DBNull_ERRORS
            M(dbnull & b, "dbnull & b")
#End If
#If DBNull_ERRORS
            M(dbnull & sb, "dbnull & sb")
#End If
#If DBNull_ERRORS
            M(dbnull & s, "dbnull & s")
#End If
#If DBNull_ERRORS
            M(dbnull & us, "dbnull & us")
#End If
#If DBNull_ERRORS
            M(dbnull & i, "dbnull & i")
#End If
#If DBNull_ERRORS
            M(dbnull & ui, "dbnull & ui")
#End If
#If DBNull_ERRORS
            M(dbnull & l, "dbnull & l")
#End If
#If DBNull_ERRORS
            M(dbnull & ul, "dbnull & ul")
#End If
#If DBNull_ERRORS
            M(dbnull & dec, "dbnull & dec")
#End If
#If DBNull_ERRORS
            M(dbnull & sng, "dbnull & sng")
#End If
#If DBNull_ERRORS
            M(dbnull & dbl, "dbnull & dbl")
#End If
#If DBNull_ERRORS
            M(dbnull & chr, "dbnull & chr")
#End If
#If DBNull_ERRORS
            M(dbnull & str, "dbnull & str")
#End If
#If DBNull_ERRORS
            M(dbnull & dt, "dbnull & dt")
#End If
#If DBNull_ERRORS
            M(dbnull & dbnull, "dbnull & dbnull")
#End If
#If DBNull_ERRORS
            M(dbnull & obj, "dbnull & obj")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj & bool, "obj & bool")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj & b, "obj & b")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj & sb, "obj & sb")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj & s, "obj & s")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj & us, "obj & us")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj & i, "obj & i")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj & ui, "obj & ui")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj & l, "obj & l")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj & ul, "obj & ul")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj & dec, "obj & dec")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj & sng, "obj & sng")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj & dbl, "obj & dbl")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj & chr, "obj & chr")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj & str, "obj & str")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj & dt, "obj & dt")
#End If
#If Object_ERRORS
            M(obj & dbnull, "obj & dbnull")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj & obj, "obj & obj")
#End If
        If failures > 0 Then Return 1
    Catch ex As Exception
        Console.WriteLine ("Exception: {0}", ex)
        Return 2
    End Try
    Return 0
    End Function
End Class
