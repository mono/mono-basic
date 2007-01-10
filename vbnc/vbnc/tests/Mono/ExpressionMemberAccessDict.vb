Option Strict Off
Imports System

Class Keys
    Default Public ReadOnly Property Item(ByVal s As String) As Integer
        Get
            Return 10
        End Get
    End Property
End Class

Module Test
    Function Main() As Integer
        Dim x As Object = New Keys()
        Dim y As Integer
        Dim z As Integer
        y = x!zzz
        z = x("abc")
        If y <> 10 Or Z <> 10 Then
            Throw New Exception("Unexpected Behavior . As y should be equal to 10 but got y =" & y)
        End If
    End Function
End Module
