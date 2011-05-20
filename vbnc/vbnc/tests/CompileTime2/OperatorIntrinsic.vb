Class IntrinsicOperatorTests
    Public Shared expected_tc As System.TypeCode
    Public Shared bool As Boolean = True
    Public Shared b As Byte = 1
    Public Shared sb As SByte = 1
    Public Shared s As Short = 1
    Public Shared us As UShort = 1
    Public Shared i As Integer = 1
    Public Shared ui As UInteger = 1
    Public Shared l As Long = 1
    Public Shared ul As ULong = 1
    Public Shared dec As Decimal = 1
    Public Shared sng As Single = 1
    Public Shared dbl As Double = 1
    Public Shared chr As Char = "1"c
    Public Shared str As String = "1"
    Public Shared dt As Date
    Public Shared dbnull As System.DBNull
    Public Shared obj As Object

    Public Shared Failures As Integer

    Shared Sub Verify(ByVal real_tc As System.TypeCode, ByVal msg As String)
        If real_tc = expected_tc Then Return

        failures += 1
        Console.WriteLine("{2}: Expected {0} got {1}", expected_tc, real_tc, msg)
    End Sub

    Shared Sub M(ByVal v As Boolean, ByVal msg As String)
        Verify(TypeCode.Boolean, msg)
    End Sub
    Shared Sub M(ByVal v As Byte, ByVal msg As String)
        Verify(TypeCode.Byte, msg)
    End Sub
    Shared Sub M(ByVal v As SByte, ByVal msg As String)
        Verify(TypeCode.SByte, msg)
    End Sub
    Shared Sub M(ByVal v As Short, ByVal msg As String)
        Verify(TypeCode.Int16, msg)
    End Sub
    Shared Sub M(ByVal v As UShort, ByVal msg As String)
        Verify(TypeCode.UInt16, msg)
    End Sub
    Shared Sub M(ByVal v As Integer, ByVal msg As String)
        Verify(TypeCode.Int32, msg)
    End Sub
    Shared Sub M(ByVal v As UInteger, ByVal msg As String)
        Verify(TypeCode.UInt32, msg)
    End Sub
    Shared Sub M(ByVal v As Long, ByVal msg As String)
        Verify(TypeCode.Int64, msg)
    End Sub
    Shared Sub M(ByVal v As ULong, ByVal msg As String)
        Verify(TypeCode.UInt64, msg)
    End Sub
    Shared Sub M(ByVal v As Decimal, ByVal msg As String)
        Verify(TypeCode.Decimal, msg)
    End Sub
    Shared Sub M(ByVal v As Single, ByVal msg As String)
        Verify(TypeCode.Single, msg)
    End Sub
    Shared Sub M(ByVal v As Double, ByVal msg As String)
        Verify(TypeCode.Double, msg)
    End Sub
    Shared Sub M(ByVal v As Char, ByVal msg As String)
        Verify(TypeCode.Char, msg)
    End Sub
    Shared Sub M(ByVal v As String, ByVal msg As String)
        Verify(TypeCode.String, msg)
    End Sub
    Shared Sub M(ByVal v As Date, ByVal msg As String)
        Verify(TypeCode.DateTime, msg)
    End Sub
    Shared Sub M(ByVal v As System.DBNull, ByVal msg As String)
        Verify(TypeCode.DBNull, msg)
    End Sub
    Shared Sub M(ByVal v As Object, ByVal msg As String)
        Verify(TypeCode.Object, msg)
    End Sub
End Class