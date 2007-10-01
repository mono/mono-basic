Public Class A
    Public ReadOnly Property Header As String
        Get
            Return "OK"
        End Get
    End Property
End Class

Public Class B
    Inherits A
    Protected Header As String

    Public Shared Sub Main
        Dim b as B = new B ()
        System.Console.WriteLine (b.header)
    End Sub
End Class

