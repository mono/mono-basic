Module ArgumentTypeInference
    Dim failures As Integer

    Function A(ByVal z As Integer) As Integer
        Return 0
    End Function
    Function A(Of T)(ByVal z As t) As Integer
        Return 1
    End Function

    Function A2(ByVal z As Integer) As Integer
        Return Type.GetTypeCode(GetType(Integer))
    End Function
    Function A2(Of T)(ByVal z As T) As TypeCode
        Return Type.GetTypeCode(z.GetType())
    End Function

    Function B(Of T)(ByVal z As T) As Integer
        Return 2
    End Function
    Function B(Of T)(ByVal z As T, ByVal zz As T) As Integer
        Return 3
    End Function

    Function B2(Of T)(ByVal z As T) As TypeCode
        Return Type.GetTypeCode(z.GetType())
    End Function
    Function B2(Of T)(ByVal z As T, ByVal zz As T) As TypeCode
        Return Type.GetTypeCode(z.GetType())
    End Function

    Function C(Of T As Structure)(ByVal z As Nullable(Of T)) As Integer
        Return 1
    End Function
    Function C(Of T As Structure)(ByVal z As Nullable(Of T), ByVal zz As Nullable(Of T)) As Integer
        Return 2
    End Function

    Function C2(Of T As Structure)(ByVal z As Nullable(Of T)) As TypeCode
        Return Type.GetTypeCode(z.Value.GetType())
    End Function
    Function C2(Of T As Structure)(ByVal z As Nullable(Of T), ByVal zz As Nullable(Of T)) As TypeCode
        Return Type.GetTypeCode(z.Value.GetType())
    End Function

    Public Function Main() As Integer
        Verify("#A1", 0, A(1))
        Verify("#a1", TypeCode.Int32, A2(1))
        Verify("#A2", 1, A(1L))
        Verify("#a2", TypeCode.Int64, A2(1L))

        Verify("#B1", 2, B(1))
        Verify("#b1", TypeCode.Int32, B2(1))
        Verify("#B2", 3, B(1, 1))
        Verify("#b2", TypeCode.Int32, B2(1, 1))
        Verify("#B3", 3, B(1, 1L))
        Verify("#b3", TypeCode.Int64, B2(1, 1L))

        Verify("#C1", 1, C(New Nullable(Of Integer)(2)))
        Verify("#c1", TypeCode.Int32, C2(New Nullable(Of Integer)(2)))
        Verify("#C2", 2, C(New Nullable(Of Long)(1), New Nullable(Of Long)(2)))
        Verify("#c2", TypeCode.Int64, C2(New Nullable(Of Long)(1), New Nullable(Of Long)(2)))

        Return failures
    End Function

    Sub Verify(ByVal msg As Object, ByVal expected As Integer, ByVal actual As Integer)
        If expected = actual Then Return
        Console.WriteLine("FAILED: expected {0} got {1} {2}", expected, actual, msg)
        failures += 1
    End Sub

    Sub Verify(ByVal msg As Object, ByVal expected As TypeCode, ByVal actual As TypeCode)
        If expected = actual Then Return
        Console.WriteLine("FAILED: expected {0} got {1} {2}", expected, actual, msg)
        failures += 1
    End Sub
End Module