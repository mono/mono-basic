Class Delegate1
    Delegate Sub a()
    Delegate Function b() As String
    Delegate Sub c(ByVal byvalParam As Integer)
    Delegate Function d(ByRef byrefParam As Single) As Double
    Delegate Function complex(ByRef byref1 As Integer, ByVal byval2 As String, ByRef byref3 As Decimal, ByVal byval4 As Object) As String
    Class nestedclass
        Delegate Sub nested()
    End Class
    Private Shared Sub Main()

    End Sub
End Class

