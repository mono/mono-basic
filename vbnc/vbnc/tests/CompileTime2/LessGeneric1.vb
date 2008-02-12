Imports System.IO

Class LessGeneric1
    Shared Function Main() As Integer
        Return a(CUShort(1))
    End Function
    Shared Function A(ByVal i As Integer) As Integer
        Return 0
    End Function
    Shared Function A(ByVal i As ULong) As Integer
        Return 1
    End Function
End Class
