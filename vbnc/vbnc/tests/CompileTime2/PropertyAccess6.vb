Option Strict On
Class A
    Shared Sub Main()
        Dim o As Date = now()
        o = test()
        Dim obj As Object = i()
    End Sub
    Shared ReadOnly Property Test As Date
        Get
            Return Date.now
        End Get
    End Property
    Shared ReadOnly Property Test2 As Decimal
        Get
            Return 0
        End Get
    End Property
    Shared ReadOnly Property I As Integer
        Get
            Return 0
        End Get
    End Property
End Class