Option Strict On
Class C1
    Sub F()
        Math.Round(2.2, 0)
    End Sub

    Sub E()
        Dim s() As String
        Dim e As String
        e = Microsoft.VisualBasic.Join(s, Microsoft.VisualBasic.vbNewLine)
    End Sub

    Sub D()
        Dim D() As Derived
        Dim I1 As System.Collections.Generic.IList(Of Base) = D
        Dim E1 As System.Collections.Generic.IEnumerable(Of Base) = D
        Dim C1 As System.Collections.Generic.ICollection(Of Base) = D
    End Sub

    Sub C(ByVal lst As system.collections.generic.Ilist(Of Base))
        Dim l As system.collections.generic.list(Of derived)
        C(l)
    End Sub
    Sub C(ByVal lst As system.collections.generic.Ilist(Of Derived))
    End Sub

    Sub B()
        Console.WriteLine(1)
    End Sub

    Sub A()
        Dim d1? As Decimal
        Dim d2? As Decimal
        Dim b As Boolean = Decimal.Equals(d1, d2)
    End Sub

End Class

Class Base
End Class
Class Derived
    Inherits Base
End Class