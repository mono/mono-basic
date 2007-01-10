Imports System

Module M
    Enum E
        A = B
        B = C
        C = 100
    End Enum

    Function Main() As Integer
        Dim e1 As E

        If E.A <> 100 Then
            Throw New Exception("#D1: Unexpected value for E.A")
        End If

        If E.B <> 100 Then
            Throw New Exception("#D2: Unexpected value for E.B")
        End If

        If E.C <> 100 Then
            Throw New Exception("#D3: Unexpected value for E.C")
        End If
    End Function
End Module
