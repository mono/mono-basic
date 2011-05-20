Class BinaryOperatorOrElse
    Inherits IntrinsicOperatorTests
    Shared Function Main As Integer
        Try
            expected_tc = TypeCode.Boolean
            M(bool OrElse bool, "bool OrElse bool")
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Boolean
            M(bool OrElse b, "bool OrElse b")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Boolean
            M(bool OrElse sb, "bool OrElse sb")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Boolean
            M(bool OrElse s, "bool OrElse s")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Boolean
            M(bool OrElse us, "bool OrElse us")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Boolean
            M(bool OrElse i, "bool OrElse i")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Boolean
            M(bool OrElse ui, "bool OrElse ui")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Boolean
            M(bool OrElse l, "bool OrElse l")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Boolean
            M(bool OrElse ul, "bool OrElse ul")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Boolean
            M(bool OrElse dec, "bool OrElse dec")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Boolean
            M(bool OrElse sng, "bool OrElse sng")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Boolean
            M(bool OrElse dbl, "bool OrElse dbl")
#End If
#If Boolean_ERRORS
            M(bool OrElse chr, "bool OrElse chr")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Boolean
            M(bool OrElse str, "bool OrElse str")
#End If
#If Boolean_ERRORS
            M(bool OrElse dt, "bool OrElse dt")
#End If
#If Boolean_ERRORS
            M(bool OrElse dbnull, "bool OrElse dbnull")
#End If
#If Not STRICT Or Boolean_ERRORS
            expected_tc = TypeCode.Object
            M(bool OrElse obj, "bool OrElse obj")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b OrElse bool, "b OrElse bool")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b OrElse b, "b OrElse b")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b OrElse sb, "b OrElse sb")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b OrElse s, "b OrElse s")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b OrElse us, "b OrElse us")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b OrElse i, "b OrElse i")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b OrElse ui, "b OrElse ui")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b OrElse l, "b OrElse l")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b OrElse ul, "b OrElse ul")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b OrElse dec, "b OrElse dec")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b OrElse sng, "b OrElse sng")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b OrElse dbl, "b OrElse dbl")
#End If
#If Byte_ERRORS
            M(b OrElse chr, "b OrElse chr")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Boolean
            M(b OrElse str, "b OrElse str")
#End If
#If Byte_ERRORS
            M(b OrElse dt, "b OrElse dt")
#End If
#If Byte_ERRORS
            M(b OrElse dbnull, "b OrElse dbnull")
#End If
#If Not STRICT Or Byte_ERRORS
            expected_tc = TypeCode.Object
            M(b OrElse obj, "b OrElse obj")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb OrElse bool, "sb OrElse bool")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb OrElse b, "sb OrElse b")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb OrElse sb, "sb OrElse sb")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb OrElse s, "sb OrElse s")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb OrElse us, "sb OrElse us")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb OrElse i, "sb OrElse i")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb OrElse ui, "sb OrElse ui")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb OrElse l, "sb OrElse l")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb OrElse ul, "sb OrElse ul")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb OrElse dec, "sb OrElse dec")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb OrElse sng, "sb OrElse sng")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb OrElse dbl, "sb OrElse dbl")
#End If
#If SByte_ERRORS
            M(sb OrElse chr, "sb OrElse chr")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Boolean
            M(sb OrElse str, "sb OrElse str")
#End If
#If SByte_ERRORS
            M(sb OrElse dt, "sb OrElse dt")
#End If
#If SByte_ERRORS
            M(sb OrElse dbnull, "sb OrElse dbnull")
#End If
#If Not STRICT Or SByte_ERRORS
            expected_tc = TypeCode.Object
            M(sb OrElse obj, "sb OrElse obj")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s OrElse bool, "s OrElse bool")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s OrElse b, "s OrElse b")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s OrElse sb, "s OrElse sb")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s OrElse s, "s OrElse s")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s OrElse us, "s OrElse us")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s OrElse i, "s OrElse i")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s OrElse ui, "s OrElse ui")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s OrElse l, "s OrElse l")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s OrElse ul, "s OrElse ul")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s OrElse dec, "s OrElse dec")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s OrElse sng, "s OrElse sng")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s OrElse dbl, "s OrElse dbl")
#End If
#If Int16_ERRORS
            M(s OrElse chr, "s OrElse chr")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Boolean
            M(s OrElse str, "s OrElse str")
#End If
#If Int16_ERRORS
            M(s OrElse dt, "s OrElse dt")
#End If
#If Int16_ERRORS
            M(s OrElse dbnull, "s OrElse dbnull")
#End If
#If Not STRICT Or Int16_ERRORS
            expected_tc = TypeCode.Object
            M(s OrElse obj, "s OrElse obj")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us OrElse bool, "us OrElse bool")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us OrElse b, "us OrElse b")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us OrElse sb, "us OrElse sb")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us OrElse s, "us OrElse s")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us OrElse us, "us OrElse us")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us OrElse i, "us OrElse i")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us OrElse ui, "us OrElse ui")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us OrElse l, "us OrElse l")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us OrElse ul, "us OrElse ul")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us OrElse dec, "us OrElse dec")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us OrElse sng, "us OrElse sng")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us OrElse dbl, "us OrElse dbl")
#End If
#If UInt16_ERRORS
            M(us OrElse chr, "us OrElse chr")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Boolean
            M(us OrElse str, "us OrElse str")
#End If
#If UInt16_ERRORS
            M(us OrElse dt, "us OrElse dt")
#End If
#If UInt16_ERRORS
            M(us OrElse dbnull, "us OrElse dbnull")
#End If
#If Not STRICT Or UInt16_ERRORS
            expected_tc = TypeCode.Object
            M(us OrElse obj, "us OrElse obj")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i OrElse bool, "i OrElse bool")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i OrElse b, "i OrElse b")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i OrElse sb, "i OrElse sb")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i OrElse s, "i OrElse s")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i OrElse us, "i OrElse us")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i OrElse i, "i OrElse i")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i OrElse ui, "i OrElse ui")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i OrElse l, "i OrElse l")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i OrElse ul, "i OrElse ul")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i OrElse dec, "i OrElse dec")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i OrElse sng, "i OrElse sng")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i OrElse dbl, "i OrElse dbl")
#End If
#If Int32_ERRORS
            M(i OrElse chr, "i OrElse chr")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Boolean
            M(i OrElse str, "i OrElse str")
#End If
#If Int32_ERRORS
            M(i OrElse dt, "i OrElse dt")
#End If
#If Int32_ERRORS
            M(i OrElse dbnull, "i OrElse dbnull")
#End If
#If Not STRICT Or Int32_ERRORS
            expected_tc = TypeCode.Object
            M(i OrElse obj, "i OrElse obj")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui OrElse bool, "ui OrElse bool")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui OrElse b, "ui OrElse b")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui OrElse sb, "ui OrElse sb")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui OrElse s, "ui OrElse s")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui OrElse us, "ui OrElse us")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui OrElse i, "ui OrElse i")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui OrElse ui, "ui OrElse ui")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui OrElse l, "ui OrElse l")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui OrElse ul, "ui OrElse ul")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui OrElse dec, "ui OrElse dec")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui OrElse sng, "ui OrElse sng")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui OrElse dbl, "ui OrElse dbl")
#End If
#If UInt32_ERRORS
            M(ui OrElse chr, "ui OrElse chr")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Boolean
            M(ui OrElse str, "ui OrElse str")
#End If
#If UInt32_ERRORS
            M(ui OrElse dt, "ui OrElse dt")
#End If
#If UInt32_ERRORS
            M(ui OrElse dbnull, "ui OrElse dbnull")
#End If
#If Not STRICT Or UInt32_ERRORS
            expected_tc = TypeCode.Object
            M(ui OrElse obj, "ui OrElse obj")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l OrElse bool, "l OrElse bool")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l OrElse b, "l OrElse b")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l OrElse sb, "l OrElse sb")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l OrElse s, "l OrElse s")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l OrElse us, "l OrElse us")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l OrElse i, "l OrElse i")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l OrElse ui, "l OrElse ui")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l OrElse l, "l OrElse l")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l OrElse ul, "l OrElse ul")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l OrElse dec, "l OrElse dec")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l OrElse sng, "l OrElse sng")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l OrElse dbl, "l OrElse dbl")
#End If
#If Int64_ERRORS
            M(l OrElse chr, "l OrElse chr")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Boolean
            M(l OrElse str, "l OrElse str")
#End If
#If Int64_ERRORS
            M(l OrElse dt, "l OrElse dt")
#End If
#If Int64_ERRORS
            M(l OrElse dbnull, "l OrElse dbnull")
#End If
#If Not STRICT Or Int64_ERRORS
            expected_tc = TypeCode.Object
            M(l OrElse obj, "l OrElse obj")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul OrElse bool, "ul OrElse bool")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul OrElse b, "ul OrElse b")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul OrElse sb, "ul OrElse sb")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul OrElse s, "ul OrElse s")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul OrElse us, "ul OrElse us")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul OrElse i, "ul OrElse i")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul OrElse ui, "ul OrElse ui")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul OrElse l, "ul OrElse l")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul OrElse ul, "ul OrElse ul")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul OrElse dec, "ul OrElse dec")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul OrElse sng, "ul OrElse sng")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul OrElse dbl, "ul OrElse dbl")
#End If
#If UInt64_ERRORS
            M(ul OrElse chr, "ul OrElse chr")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Boolean
            M(ul OrElse str, "ul OrElse str")
#End If
#If UInt64_ERRORS
            M(ul OrElse dt, "ul OrElse dt")
#End If
#If UInt64_ERRORS
            M(ul OrElse dbnull, "ul OrElse dbnull")
#End If
#If Not STRICT Or UInt64_ERRORS
            expected_tc = TypeCode.Object
            M(ul OrElse obj, "ul OrElse obj")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec OrElse bool, "dec OrElse bool")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec OrElse b, "dec OrElse b")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec OrElse sb, "dec OrElse sb")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec OrElse s, "dec OrElse s")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec OrElse us, "dec OrElse us")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec OrElse i, "dec OrElse i")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec OrElse ui, "dec OrElse ui")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec OrElse l, "dec OrElse l")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec OrElse ul, "dec OrElse ul")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec OrElse dec, "dec OrElse dec")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec OrElse sng, "dec OrElse sng")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec OrElse dbl, "dec OrElse dbl")
#End If
#If Decimal_ERRORS
            M(dec OrElse chr, "dec OrElse chr")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Boolean
            M(dec OrElse str, "dec OrElse str")
#End If
#If Decimal_ERRORS
            M(dec OrElse dt, "dec OrElse dt")
#End If
#If Decimal_ERRORS
            M(dec OrElse dbnull, "dec OrElse dbnull")
#End If
#If Not STRICT Or Decimal_ERRORS
            expected_tc = TypeCode.Object
            M(dec OrElse obj, "dec OrElse obj")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng OrElse bool, "sng OrElse bool")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng OrElse b, "sng OrElse b")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng OrElse sb, "sng OrElse sb")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng OrElse s, "sng OrElse s")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng OrElse us, "sng OrElse us")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng OrElse i, "sng OrElse i")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng OrElse ui, "sng OrElse ui")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng OrElse l, "sng OrElse l")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng OrElse ul, "sng OrElse ul")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng OrElse dec, "sng OrElse dec")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng OrElse sng, "sng OrElse sng")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng OrElse dbl, "sng OrElse dbl")
#End If
#If Single_ERRORS
            M(sng OrElse chr, "sng OrElse chr")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Boolean
            M(sng OrElse str, "sng OrElse str")
#End If
#If Single_ERRORS
            M(sng OrElse dt, "sng OrElse dt")
#End If
#If Single_ERRORS
            M(sng OrElse dbnull, "sng OrElse dbnull")
#End If
#If Not STRICT Or Single_ERRORS
            expected_tc = TypeCode.Object
            M(sng OrElse obj, "sng OrElse obj")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl OrElse bool, "dbl OrElse bool")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl OrElse b, "dbl OrElse b")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl OrElse sb, "dbl OrElse sb")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl OrElse s, "dbl OrElse s")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl OrElse us, "dbl OrElse us")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl OrElse i, "dbl OrElse i")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl OrElse ui, "dbl OrElse ui")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl OrElse l, "dbl OrElse l")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl OrElse ul, "dbl OrElse ul")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl OrElse dec, "dbl OrElse dec")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl OrElse sng, "dbl OrElse sng")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl OrElse dbl, "dbl OrElse dbl")
#End If
#If Double_ERRORS
            M(dbl OrElse chr, "dbl OrElse chr")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Boolean
            M(dbl OrElse str, "dbl OrElse str")
#End If
#If Double_ERRORS
            M(dbl OrElse dt, "dbl OrElse dt")
#End If
#If Double_ERRORS
            M(dbl OrElse dbnull, "dbl OrElse dbnull")
#End If
#If Not STRICT Or Double_ERRORS
            expected_tc = TypeCode.Object
            M(dbl OrElse obj, "dbl OrElse obj")
#End If
#If Char_ERRORS
            M(chr OrElse bool, "chr OrElse bool")
#End If
#If Char_ERRORS
            M(chr OrElse b, "chr OrElse b")
#End If
#If Char_ERRORS
            M(chr OrElse sb, "chr OrElse sb")
#End If
#If Char_ERRORS
            M(chr OrElse s, "chr OrElse s")
#End If
#If Char_ERRORS
            M(chr OrElse us, "chr OrElse us")
#End If
#If Char_ERRORS
            M(chr OrElse i, "chr OrElse i")
#End If
#If Char_ERRORS
            M(chr OrElse ui, "chr OrElse ui")
#End If
#If Char_ERRORS
            M(chr OrElse l, "chr OrElse l")
#End If
#If Char_ERRORS
            M(chr OrElse ul, "chr OrElse ul")
#End If
#If Char_ERRORS
            M(chr OrElse dec, "chr OrElse dec")
#End If
#If Char_ERRORS
            M(chr OrElse sng, "chr OrElse sng")
#End If
#If Char_ERRORS
            M(chr OrElse dbl, "chr OrElse dbl")
#End If
#If Char_ERRORS
            M(chr OrElse chr, "chr OrElse chr")
#End If
#If Char_ERRORS
            M(chr OrElse str, "chr OrElse str")
#End If
#If Char_ERRORS
            M(chr OrElse dt, "chr OrElse dt")
#End If
#If Char_ERRORS
            M(chr OrElse dbnull, "chr OrElse dbnull")
#End If
#If Char_ERRORS
            M(chr OrElse obj, "chr OrElse obj")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Boolean
            M(str OrElse bool, "str OrElse bool")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Boolean
            M(str OrElse b, "str OrElse b")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Boolean
            M(str OrElse sb, "str OrElse sb")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Boolean
            M(str OrElse s, "str OrElse s")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Boolean
            M(str OrElse us, "str OrElse us")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Boolean
            M(str OrElse i, "str OrElse i")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Boolean
            M(str OrElse ui, "str OrElse ui")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Boolean
            M(str OrElse l, "str OrElse l")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Boolean
            M(str OrElse ul, "str OrElse ul")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Boolean
            M(str OrElse dec, "str OrElse dec")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Boolean
            M(str OrElse sng, "str OrElse sng")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Boolean
            M(str OrElse dbl, "str OrElse dbl")
#End If
#If String_ERRORS
            M(str OrElse chr, "str OrElse chr")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Boolean
            M(str OrElse str, "str OrElse str")
#End If
#If String_ERRORS
            M(str OrElse dt, "str OrElse dt")
#End If
#If String_ERRORS
            M(str OrElse dbnull, "str OrElse dbnull")
#End If
#If Not STRICT Or String_ERRORS
            expected_tc = TypeCode.Object
            M(str OrElse obj, "str OrElse obj")
#End If
#If DateTime_ERRORS
            M(dt OrElse bool, "dt OrElse bool")
#End If
#If DateTime_ERRORS
            M(dt OrElse b, "dt OrElse b")
#End If
#If DateTime_ERRORS
            M(dt OrElse sb, "dt OrElse sb")
#End If
#If DateTime_ERRORS
            M(dt OrElse s, "dt OrElse s")
#End If
#If DateTime_ERRORS
            M(dt OrElse us, "dt OrElse us")
#End If
#If DateTime_ERRORS
            M(dt OrElse i, "dt OrElse i")
#End If
#If DateTime_ERRORS
            M(dt OrElse ui, "dt OrElse ui")
#End If
#If DateTime_ERRORS
            M(dt OrElse l, "dt OrElse l")
#End If
#If DateTime_ERRORS
            M(dt OrElse ul, "dt OrElse ul")
#End If
#If DateTime_ERRORS
            M(dt OrElse dec, "dt OrElse dec")
#End If
#If DateTime_ERRORS
            M(dt OrElse sng, "dt OrElse sng")
#End If
#If DateTime_ERRORS
            M(dt OrElse dbl, "dt OrElse dbl")
#End If
#If DateTime_ERRORS
            M(dt OrElse chr, "dt OrElse chr")
#End If
#If DateTime_ERRORS
            M(dt OrElse str, "dt OrElse str")
#End If
#If DateTime_ERRORS
            M(dt OrElse dt, "dt OrElse dt")
#End If
#If DateTime_ERRORS
            M(dt OrElse dbnull, "dt OrElse dbnull")
#End If
#If DateTime_ERRORS
            M(dt OrElse obj, "dt OrElse obj")
#End If
#If DBNull_ERRORS
            M(dbnull OrElse bool, "dbnull OrElse bool")
#End If
#If DBNull_ERRORS
            M(dbnull OrElse b, "dbnull OrElse b")
#End If
#If DBNull_ERRORS
            M(dbnull OrElse sb, "dbnull OrElse sb")
#End If
#If DBNull_ERRORS
            M(dbnull OrElse s, "dbnull OrElse s")
#End If
#If DBNull_ERRORS
            M(dbnull OrElse us, "dbnull OrElse us")
#End If
#If DBNull_ERRORS
            M(dbnull OrElse i, "dbnull OrElse i")
#End If
#If DBNull_ERRORS
            M(dbnull OrElse ui, "dbnull OrElse ui")
#End If
#If DBNull_ERRORS
            M(dbnull OrElse l, "dbnull OrElse l")
#End If
#If DBNull_ERRORS
            M(dbnull OrElse ul, "dbnull OrElse ul")
#End If
#If DBNull_ERRORS
            M(dbnull OrElse dec, "dbnull OrElse dec")
#End If
#If DBNull_ERRORS
            M(dbnull OrElse sng, "dbnull OrElse sng")
#End If
#If DBNull_ERRORS
            M(dbnull OrElse dbl, "dbnull OrElse dbl")
#End If
#If DBNull_ERRORS
            M(dbnull OrElse chr, "dbnull OrElse chr")
#End If
#If DBNull_ERRORS
            M(dbnull OrElse str, "dbnull OrElse str")
#End If
#If DBNull_ERRORS
            M(dbnull OrElse dt, "dbnull OrElse dt")
#End If
#If DBNull_ERRORS
            M(dbnull OrElse dbnull, "dbnull OrElse dbnull")
#End If
#If DBNull_ERRORS
            M(dbnull OrElse obj, "dbnull OrElse obj")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj OrElse bool, "obj OrElse bool")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj OrElse b, "obj OrElse b")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj OrElse sb, "obj OrElse sb")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj OrElse s, "obj OrElse s")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj OrElse us, "obj OrElse us")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj OrElse i, "obj OrElse i")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj OrElse ui, "obj OrElse ui")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj OrElse l, "obj OrElse l")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj OrElse ul, "obj OrElse ul")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj OrElse dec, "obj OrElse dec")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj OrElse sng, "obj OrElse sng")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj OrElse dbl, "obj OrElse dbl")
#End If
#If Object_ERRORS
            M(obj OrElse chr, "obj OrElse chr")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj OrElse str, "obj OrElse str")
#End If
#If Object_ERRORS
            M(obj OrElse dt, "obj OrElse dt")
#End If
#If Object_ERRORS
            M(obj OrElse dbnull, "obj OrElse dbnull")
#End If
#If Not STRICT Or Object_ERRORS
            expected_tc = TypeCode.Object
            M(obj OrElse obj, "obj OrElse obj")
#End If
        If failures > 0 Then Return 1
    Catch ex As Exception
        Console.WriteLine ("Exception: {0}", ex)
        Return 2
    End Try
    Return 0
    End Function
End Class
