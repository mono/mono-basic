Option Strict Off
Imports System

Structure S
    Dim a As String
    Const b As Integer = 25

    Sub New(ByVal l As Long)
    End Sub
End Structure


Module M
    Function Main() As Integer
        Dim x As S = New S(100)
    End Function
End Module
