Class Delegate2
    Delegate Sub a()
    Delegate Function b() As String
    Delegate Sub c(ByVal b As String)
    Delegate Function d(ByVal b As String) As String
    Delegate Sub e(ByRef r As String)
    Delegate Function f(ByRef r As String) As String
    Delegate Sub g(ByRef r As String, ByVal b As String)
    Delegate Function h(ByRef r As String, ByVal b As String) As String
    Delegate Function i(ByRef r1 As Integer, ByVal b1 As Short, ByRef r2 As Long, ByVal b2 As Byte) As Object
End Class

