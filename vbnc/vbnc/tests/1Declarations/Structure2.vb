Structure Structure2
    Public var1 As String
    Private var2 As Integer
    Friend var3 As Long
    Sub a()
    End Sub
    Function b() As Short
    End Function
    Delegate Sub c()
    Delegate Function d() As Byte
    Event e()
    Event f As c
    ReadOnly Property g() As Decimal
        Get
        End Get
    End Property
    WriteOnly Property h() As Single
        Set(ByVal value As Single)
        End Set
    End Property
    Property i() As Double
        Get
        End Get
        Set(ByVal value As Double)
        End Set
    End Property
End Structure