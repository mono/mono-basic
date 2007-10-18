Option Strict Off
Imports System
Class A
    Public Shared y As Integer = 20
    Public z As Integer = 30

    Shared Sub New()
    End Sub

    Public Sub New()
    End Sub

    Shared Function f() As Integer
        Return 50
    End Function
End Class

Module M
    Function Main() As Integer
        If (A.y <> 20) Then
            Throw New Exception("#A1, Unexpected result")
        End If

        Dim c As Object = New A()
        Dim d As Object = New A()

        If (c.y <> 20) Then
            Throw New Exception("#A2, Unexpected result")
        End If

        If (d.y <> 20) Then
            Throw New Exception("#A3, Unexpected result")
        End If

        A.y = 25

        If (c.y <> 25) Then
            Throw New Exception("#A4, Unexpected result")
        End If

        c.y = 35

        If (A.y <> 35) Then
            Throw New Exception("#A5, Unexpected result")
        End If

        If (d.y <> 35) Then
            Throw New Exception("#A6, Unexpected result")
        End If


        If (c.z <> 30) Then
            Throw New Exception("#A7, Unexpected result")
        End If

        If (A.f() <> 50) Then
            Throw New Exception("#A8, Unexpected result")
        End If
    End Function
End Module
