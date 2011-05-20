Class BinaryOperatorLike
    Inherits IntrinsicOperatorTests
    Shared Function Main As Integer
        Try
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Boolean
            M(bool Like bool, "bool Like bool")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Boolean
            M(bool Like b, "bool Like b")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Boolean
            M(bool Like sb, "bool Like sb")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Boolean
            M(bool Like s, "bool Like s")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Boolean
            M(bool Like us, "bool Like us")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Boolean
            M(bool Like i, "bool Like i")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Boolean
            M(bool Like ui, "bool Like ui")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Boolean
            M(bool Like l, "bool Like l")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Boolean
            M(bool Like ul, "bool Like ul")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Boolean
            M(bool Like dec, "bool Like dec")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Boolean
            M(bool Like sng, "bool Like sng")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Boolean
            M(bool Like dbl, "bool Like dbl")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Boolean
            M(bool Like chr, "bool Like chr")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Boolean
            M(bool Like str, "bool Like str")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Boolean
            M(bool Like dt, "bool Like dt")
#End If
#If Boolean_ERRORS
            M(bool Like dbnull, "bool Like dbnull")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Object
            M(bool Like obj, "bool Like obj")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b Like bool, "b Like bool")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b Like b, "b Like b")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b Like sb, "b Like sb")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b Like s, "b Like s")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b Like us, "b Like us")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b Like i, "b Like i")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b Like ui, "b Like ui")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b Like l, "b Like l")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b Like ul, "b Like ul")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b Like dec, "b Like dec")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b Like sng, "b Like sng")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b Like dbl, "b Like dbl")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b Like chr, "b Like chr")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b Like str, "b Like str")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b Like dt, "b Like dt")
#End If
#If Byte_ERRORS
            M(b Like dbnull, "b Like dbnull")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Object
            M(b Like obj, "b Like obj")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb Like bool, "sb Like bool")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb Like b, "sb Like b")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb Like sb, "sb Like sb")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb Like s, "sb Like s")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb Like us, "sb Like us")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb Like i, "sb Like i")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb Like ui, "sb Like ui")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb Like l, "sb Like l")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb Like ul, "sb Like ul")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb Like dec, "sb Like dec")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb Like sng, "sb Like sng")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb Like dbl, "sb Like dbl")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb Like chr, "sb Like chr")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb Like str, "sb Like str")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb Like dt, "sb Like dt")
#End If
#If SByte_ERRORS
            M(sb Like dbnull, "sb Like dbnull")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Object
            M(sb Like obj, "sb Like obj")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s Like bool, "s Like bool")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s Like b, "s Like b")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s Like sb, "s Like sb")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s Like s, "s Like s")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s Like us, "s Like us")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s Like i, "s Like i")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s Like ui, "s Like ui")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s Like l, "s Like l")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s Like ul, "s Like ul")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s Like dec, "s Like dec")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s Like sng, "s Like sng")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s Like dbl, "s Like dbl")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s Like chr, "s Like chr")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s Like str, "s Like str")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s Like dt, "s Like dt")
#End If
#If Int16_ERRORS
            M(s Like dbnull, "s Like dbnull")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Object
            M(s Like obj, "s Like obj")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us Like bool, "us Like bool")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us Like b, "us Like b")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us Like sb, "us Like sb")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us Like s, "us Like s")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us Like us, "us Like us")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us Like i, "us Like i")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us Like ui, "us Like ui")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us Like l, "us Like l")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us Like ul, "us Like ul")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us Like dec, "us Like dec")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us Like sng, "us Like sng")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us Like dbl, "us Like dbl")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us Like chr, "us Like chr")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us Like str, "us Like str")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us Like dt, "us Like dt")
#End If
#If UInt16_ERRORS
            M(us Like dbnull, "us Like dbnull")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Object
            M(us Like obj, "us Like obj")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i Like bool, "i Like bool")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i Like b, "i Like b")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i Like sb, "i Like sb")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i Like s, "i Like s")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i Like us, "i Like us")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i Like i, "i Like i")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i Like ui, "i Like ui")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i Like l, "i Like l")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i Like ul, "i Like ul")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i Like dec, "i Like dec")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i Like sng, "i Like sng")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i Like dbl, "i Like dbl")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i Like chr, "i Like chr")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i Like str, "i Like str")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i Like dt, "i Like dt")
#End If
#If Int32_ERRORS
            M(i Like dbnull, "i Like dbnull")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Object
            M(i Like obj, "i Like obj")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui Like bool, "ui Like bool")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui Like b, "ui Like b")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui Like sb, "ui Like sb")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui Like s, "ui Like s")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui Like us, "ui Like us")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui Like i, "ui Like i")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui Like ui, "ui Like ui")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui Like l, "ui Like l")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui Like ul, "ui Like ul")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui Like dec, "ui Like dec")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui Like sng, "ui Like sng")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui Like dbl, "ui Like dbl")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui Like chr, "ui Like chr")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui Like str, "ui Like str")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui Like dt, "ui Like dt")
#End If
#If UInt32_ERRORS
            M(ui Like dbnull, "ui Like dbnull")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Object
            M(ui Like obj, "ui Like obj")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l Like bool, "l Like bool")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l Like b, "l Like b")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l Like sb, "l Like sb")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l Like s, "l Like s")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l Like us, "l Like us")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l Like i, "l Like i")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l Like ui, "l Like ui")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l Like l, "l Like l")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l Like ul, "l Like ul")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l Like dec, "l Like dec")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l Like sng, "l Like sng")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l Like dbl, "l Like dbl")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l Like chr, "l Like chr")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l Like str, "l Like str")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l Like dt, "l Like dt")
#End If
#If Int64_ERRORS
            M(l Like dbnull, "l Like dbnull")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Object
            M(l Like obj, "l Like obj")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul Like bool, "ul Like bool")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul Like b, "ul Like b")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul Like sb, "ul Like sb")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul Like s, "ul Like s")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul Like us, "ul Like us")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul Like i, "ul Like i")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul Like ui, "ul Like ui")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul Like l, "ul Like l")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul Like ul, "ul Like ul")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul Like dec, "ul Like dec")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul Like sng, "ul Like sng")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul Like dbl, "ul Like dbl")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul Like chr, "ul Like chr")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul Like str, "ul Like str")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul Like dt, "ul Like dt")
#End If
#If UInt64_ERRORS
            M(ul Like dbnull, "ul Like dbnull")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Object
            M(ul Like obj, "ul Like obj")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec Like bool, "dec Like bool")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec Like b, "dec Like b")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec Like sb, "dec Like sb")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec Like s, "dec Like s")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec Like us, "dec Like us")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec Like i, "dec Like i")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec Like ui, "dec Like ui")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec Like l, "dec Like l")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec Like ul, "dec Like ul")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec Like dec, "dec Like dec")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec Like sng, "dec Like sng")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec Like dbl, "dec Like dbl")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec Like chr, "dec Like chr")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec Like str, "dec Like str")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec Like dt, "dec Like dt")
#End If
#If Decimal_ERRORS
            M(dec Like dbnull, "dec Like dbnull")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Object
            M(dec Like obj, "dec Like obj")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng Like bool, "sng Like bool")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng Like b, "sng Like b")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng Like sb, "sng Like sb")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng Like s, "sng Like s")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng Like us, "sng Like us")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng Like i, "sng Like i")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng Like ui, "sng Like ui")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng Like l, "sng Like l")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng Like ul, "sng Like ul")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng Like dec, "sng Like dec")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng Like sng, "sng Like sng")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng Like dbl, "sng Like dbl")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng Like chr, "sng Like chr")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng Like str, "sng Like str")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng Like dt, "sng Like dt")
#End If
#If Single_ERRORS
            M(sng Like dbnull, "sng Like dbnull")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Object
            M(sng Like obj, "sng Like obj")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl Like bool, "dbl Like bool")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl Like b, "dbl Like b")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl Like sb, "dbl Like sb")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl Like s, "dbl Like s")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl Like us, "dbl Like us")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl Like i, "dbl Like i")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl Like ui, "dbl Like ui")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl Like l, "dbl Like l")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl Like ul, "dbl Like ul")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl Like dec, "dbl Like dec")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl Like sng, "dbl Like sng")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl Like dbl, "dbl Like dbl")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl Like chr, "dbl Like chr")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl Like str, "dbl Like str")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl Like dt, "dbl Like dt")
#End If
#If Double_ERRORS
            M(dbl Like dbnull, "dbl Like dbnull")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Object
            M(dbl Like obj, "dbl Like obj")
#End If
#If Not STRICT Or Char_ERRORS
            expected_tc = TypeCode.Boolean
            M(chr Like bool, "chr Like bool")
#End If
#If Not STRICT Or Char_ERRORS
            expected_tc = TypeCode.Boolean
            M(chr Like b, "chr Like b")
#End If
#If Not STRICT Or Char_ERRORS
            expected_tc = TypeCode.Boolean
            M(chr Like sb, "chr Like sb")
#End If
#If Not STRICT Or Char_ERRORS
            expected_tc = TypeCode.Boolean
            M(chr Like s, "chr Like s")
#End If
#If Not STRICT Or Char_ERRORS
            expected_tc = TypeCode.Boolean
            M(chr Like us, "chr Like us")
#End If
#If Not STRICT Or Char_ERRORS
            expected_tc = TypeCode.Boolean
            M(chr Like i, "chr Like i")
#End If
#If Not STRICT Or Char_ERRORS
            expected_tc = TypeCode.Boolean
            M(chr Like ui, "chr Like ui")
#End If
#If Not STRICT Or Char_ERRORS
            expected_tc = TypeCode.Boolean
            M(chr Like l, "chr Like l")
#End If
#If Not STRICT Or Char_ERRORS
            expected_tc = TypeCode.Boolean
            M(chr Like ul, "chr Like ul")
#End If
#If Not STRICT Or Char_ERRORS
            expected_tc = TypeCode.Boolean
            M(chr Like dec, "chr Like dec")
#End If
#If Not STRICT Or Char_ERRORS
            expected_tc = TypeCode.Boolean
            M(chr Like sng, "chr Like sng")
#End If
#If Not STRICT Or Char_ERRORS
            expected_tc = TypeCode.Boolean
            M(chr Like dbl, "chr Like dbl")
#End If
            expected_tc = TypeCode.Boolean
            M(chr Like chr, "chr Like chr")
            expected_tc = TypeCode.Boolean
            M(chr Like str, "chr Like str")
#If Not STRICT Or Char_ERRORS
            expected_tc = TypeCode.Boolean
            M(chr Like dt, "chr Like dt")
#End If
#If Char_ERRORS
            M(chr Like dbnull, "chr Like dbnull")
#End If
#If Not STRICT Or Char_ERRORS
            expected_tc = TypeCode.Object
            M(chr Like obj, "chr Like obj")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Boolean
            M(str Like bool, "str Like bool")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Boolean
            M(str Like b, "str Like b")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Boolean
            M(str Like sb, "str Like sb")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Boolean
            M(str Like s, "str Like s")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Boolean
            M(str Like us, "str Like us")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Boolean
            M(str Like i, "str Like i")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Boolean
            M(str Like ui, "str Like ui")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Boolean
            M(str Like l, "str Like l")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Boolean
            M(str Like ul, "str Like ul")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Boolean
            M(str Like dec, "str Like dec")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Boolean
            M(str Like sng, "str Like sng")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Boolean
            M(str Like dbl, "str Like dbl")
#End If
            expected_tc = TypeCode.Boolean
            M(str Like chr, "str Like chr")
            expected_tc = TypeCode.Boolean
            M(str Like str, "str Like str")
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Boolean
            M(str Like dt, "str Like dt")
#End If
#If String_ERRORS
            M(str Like dbnull, "str Like dbnull")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Object
            M(str Like obj, "str Like obj")
#End If
#If Not STRICT Or DateTime_ERRORS
            expected_tc = TypeCode.Boolean
            M(dt Like bool, "dt Like bool")
#End If
#If Not STRICT Or DateTime_ERRORS
            expected_tc = TypeCode.Boolean
            M(dt Like b, "dt Like b")
#End If
#If Not STRICT Or DateTime_ERRORS
            expected_tc = TypeCode.Boolean
            M(dt Like sb, "dt Like sb")
#End If
#If Not STRICT Or DateTime_ERRORS
            expected_tc = TypeCode.Boolean
            M(dt Like s, "dt Like s")
#End If
#If Not STRICT Or DateTime_ERRORS
            expected_tc = TypeCode.Boolean
            M(dt Like us, "dt Like us")
#End If
#If Not STRICT Or DateTime_ERRORS
            expected_tc = TypeCode.Boolean
            M(dt Like i, "dt Like i")
#End If
#If Not STRICT Or DateTime_ERRORS
            expected_tc = TypeCode.Boolean
            M(dt Like ui, "dt Like ui")
#End If
#If Not STRICT Or DateTime_ERRORS
            expected_tc = TypeCode.Boolean
            M(dt Like l, "dt Like l")
#End If
#If Not STRICT Or DateTime_ERRORS
            expected_tc = TypeCode.Boolean
            M(dt Like ul, "dt Like ul")
#End If
#If Not STRICT Or DateTime_ERRORS
            expected_tc = TypeCode.Boolean
            M(dt Like dec, "dt Like dec")
#End If
#If Not STRICT Or DateTime_ERRORS
            expected_tc = TypeCode.Boolean
            M(dt Like sng, "dt Like sng")
#End If
#If Not STRICT Or DateTime_ERRORS
            expected_tc = TypeCode.Boolean
            M(dt Like dbl, "dt Like dbl")
#End If
#If Not STRICT Or DateTime_ERRORS
            expected_tc = TypeCode.Boolean
            M(dt Like chr, "dt Like chr")
#End If
#If Not STRICT Or DateTime_ERRORS
            expected_tc = TypeCode.Boolean
            M(dt Like str, "dt Like str")
#End If
#If Not STRICT Or DateTime_ERRORS
            expected_tc = TypeCode.Boolean
            M(dt Like dt, "dt Like dt")
#End If
#If DateTime_ERRORS
            M(dt Like dbnull, "dt Like dbnull")
#End If
#If Not STRICT Or DateTime_ERRORS
            expected_tc = TypeCode.Object
            M(dt Like obj, "dt Like obj")
#End If
#If DBNull_ERRORS
            M(dbnull Like bool, "dbnull Like bool")
#End If
#If DBNull_ERRORS
            M(dbnull Like b, "dbnull Like b")
#End If
#If DBNull_ERRORS
            M(dbnull Like sb, "dbnull Like sb")
#End If
#If DBNull_ERRORS
            M(dbnull Like s, "dbnull Like s")
#End If
#If DBNull_ERRORS
            M(dbnull Like us, "dbnull Like us")
#End If
#If DBNull_ERRORS
            M(dbnull Like i, "dbnull Like i")
#End If
#If DBNull_ERRORS
            M(dbnull Like ui, "dbnull Like ui")
#End If
#If DBNull_ERRORS
            M(dbnull Like l, "dbnull Like l")
#End If
#If DBNull_ERRORS
            M(dbnull Like ul, "dbnull Like ul")
#End If
#If DBNull_ERRORS
            M(dbnull Like dec, "dbnull Like dec")
#End If
#If DBNull_ERRORS
            M(dbnull Like sng, "dbnull Like sng")
#End If
#If DBNull_ERRORS
            M(dbnull Like dbl, "dbnull Like dbl")
#End If
#If DBNull_ERRORS
            M(dbnull Like chr, "dbnull Like chr")
#End If
#If DBNull_ERRORS
            M(dbnull Like str, "dbnull Like str")
#End If
#If DBNull_ERRORS
            M(dbnull Like dt, "dbnull Like dt")
#End If
#If DBNull_ERRORS
            M(dbnull Like dbnull, "dbnull Like dbnull")
#End If
#If DBNull_ERRORS
            M(dbnull Like obj, "dbnull Like obj")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj Like bool, "obj Like bool")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj Like b, "obj Like b")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj Like sb, "obj Like sb")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj Like s, "obj Like s")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj Like us, "obj Like us")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj Like i, "obj Like i")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj Like ui, "obj Like ui")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj Like l, "obj Like l")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj Like ul, "obj Like ul")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj Like dec, "obj Like dec")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj Like sng, "obj Like sng")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj Like dbl, "obj Like dbl")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj Like chr, "obj Like chr")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj Like str, "obj Like str")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj Like dt, "obj Like dt")
#End If
#If Object_ERRORS
            M(obj Like dbnull, "obj Like dbnull")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj Like obj, "obj Like obj")
#End If
        If failures > 0 Then Return 1
    Catch ex As Exception
        Console.WriteLine ("Exception: {0}", ex)
        Return 2
    End Try
    Return 0
    End Function
End Class
