Class UnaryOperatorUnaryNot
    Inherits IntrinsicOperatorTests
    Shared Function Main As Integer
        Try
            expected_tc = TypeCode.Boolean
        M(Not bool, "Not bool")
            expected_tc = TypeCode.Byte
        M(Not b, "Not b")
            expected_tc = TypeCode.SByte
        M(Not sb, "Not sb")
            expected_tc = TypeCode.Int16
        M(Not s, "Not s")
            expected_tc = TypeCode.UInt16
        M(Not us, "Not us")
            expected_tc = TypeCode.Int32
        M(Not i, "Not i")
            expected_tc = TypeCode.UInt32
        M(Not ui, "Not ui")
            expected_tc = TypeCode.Int64
        M(Not l, "Not l")
            expected_tc = TypeCode.UInt64
        M(Not ul, "Not ul")
#If Not STRICT Or ERRORS
            expected_tc = TypeCode.Int64
        M(Not dec, "Not dec")
#End If
#If Not STRICT Or ERRORS
            expected_tc = TypeCode.Int64
        M(Not sng, "Not sng")
#End If
#If Not STRICT Or ERRORS
            expected_tc = TypeCode.Int64
        M(Not dbl, "Not dbl")
#End If
#If ERRORS
        M(Not chr, "Not chr")
#End If
#If Not STRICT Or ERRORS
            expected_tc = TypeCode.Int64
        M(Not str, "Not str")
#End If
#If ERRORS
        M(Not dt, "Not dt")
#End If
#If ERRORS
        M(Not dbnull, "Not dbnull")
#End If
#If Not STRICT Or ERRORS
            expected_tc = TypeCode.Object
        M(Not obj, "Not obj")
#End If
    If failures > 0 Then Return 1
    Catch ex As Exception
        Console.WriteLine ("Exception: {0}", ex)
        Return 2
    End Try
    End Function
End Class
