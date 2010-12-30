Module ArgumentTypeInference
    Dim failures As Integer

    Function A(ByVal z As Integer) As Integer
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
        Verify("#C3", 2, C(New Nullable(Of Integer)(1), New Nullable(Of Long)(2)))

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