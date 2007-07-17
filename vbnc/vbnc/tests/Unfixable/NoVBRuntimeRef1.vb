Option Strict On

Namespace NoVBRuntimeRef1
    Public Class novbruntimeref
        Public Const x As Integer = AscW("x"c, "x")

        Public Shared Function AscW(ByVal x As Char, ByVal y As String) As Integer
            Return AscW(x, "x"c)
        End Function

        Public Shared Function AscW(Optional ByVal x As Char = "x"c) As Integer
            Return AscW()
        End Function

        Public Shared Function Asc1(ByVal x As Char) As Integer
            Return AscW(x, x)
        End Function

        Public Shared Function Asc2(ByVal x As Char) As Integer
            Return AscW("x"c, x)
        End Function
    End Class

    Class novbruntimeref1
        Public Shared Function Ascw(ByVal x As Char, ByVal y As Char) As Integer
            Return Ascw(x, y)
        End Function

        Public Shared Sub Ascw(ByVal x As Char)
        End Sub

        Public Shared Function Asc1(ByVal x As Char) As Integer
            Return Ascw(x)
        End Function
    End Class

    Class novbruntimeref2
        Public Shared Function Asc1(ByVal x As Char) As Integer
            Return novbruntimeref.AscW(x, x)
        End Function
    End Class
End Namespace
