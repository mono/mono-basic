
Class A
    Public Sub New()

    End Sub
End Class
Class C
End Class
Interface I
End Interface
Interface II
    Inherits I
End Interface
Class CI
    Implements I
End Class
Class BC
    Inherits C
End Class

Class B
    Sub M1(Of T As New)()
        M1(Of A)()
        M1(Of T)()
    End Sub
    Sub M2(Of T As Structure)()
        M2(Of VT)()
        M2(Of Integer)()
        M2(Of TypeCode)()
        M2(Of T)()
    End Sub
    Sub M3(Of T As Class)()
        M3(Of A)()
        M3(Of String)()
        M3(Of Object)()
        M3(Of T)()
    End Sub
    Sub M4(Of T As I)()
        M4(Of CI)()
        M4(Of T)()
    End Sub
    Sub M5(Of T As C)()
        M5(Of BC)()
        M5(Of T)()
    End Sub
    Sub M6(Of T As II)()
        M6(Of T)()
        M4(Of T)()
    End Sub


End Class

Structure VT
    Dim i As Integer
End Structure