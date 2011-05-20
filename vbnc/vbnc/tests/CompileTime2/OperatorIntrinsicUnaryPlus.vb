Class UnaryOperatorUnaryPlus
    Inherits IntrinsicOperatorTests
    Shared Function Main As Integer
        Try
#If Not STRICT Or ERRORS
            expected_tc = TypeCode.Int16
        M(+ bool, "+ bool")
#End If
            expected_tc = TypeCode.Byte
        M(+ b, "+ b")
            expected_tc = TypeCode.SByte
        M(+ sb, "+ sb")
            expected_tc = TypeCode.Int16
        M(+ s, "+ s")
            expected_tc = TypeCode.UInt16
        M(+ us, "+ us")
            expected_tc = TypeCode.Int32
        M(+ i, "+ i")
            expected_tc = TypeCode.UInt32
        M(+ ui, "+ ui")
            expected_tc = TypeCode.Int64
        M(+ l, "+ l")
            expected_tc = TypeCode.UInt64
        M(+ ul, "+ ul")
            expected_tc = TypeCode.Decimal
        M(+ dec, "+ dec")
            expected_tc = TypeCode.Single
        M(+ sng, "+ sng")
            expected_tc = TypeCode.Double
        M(+ dbl, "+ dbl")
#If ERRORS
        M(+ chr, "+ chr")
#End If
#If Not STRICT Or ERRORS
            expected_tc = TypeCode.Double
        M(+ str, "+ str")
#End If
#If ERRORS
        M(+ dt, "+ dt")
#End If
#If ERRORS
        M(+ dbnull, "+ dbnull")
#End If
#If Not STRICT Or ERRORS
            expected_tc = TypeCode.Object
        M(+ obj, "+ obj")
#End If
    If failures > 0 Then Return 1
    Catch ex As Exception
        Console.WriteLine ("Exception: {0}", ex)
        Return 2
    End Try
    End Function
End Class
