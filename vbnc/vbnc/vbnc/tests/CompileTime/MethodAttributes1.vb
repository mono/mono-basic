MustInherit Class MethodAttributes1

    Public Sub Main()

    End Sub
    Friend Function Test1() As String

    End Function
    Private Sub Main2(ByVal a As String)

    End Sub
    Protected Sub main3(ByRef b As Integer)

    End Sub
    Shared Sub sub1()

    End Sub
    Overridable Sub sub2()

    End Sub
    MustOverride Sub sub4()
    Overridable Sub sub6()

    End Sub
    Overloads Sub subo(ByVal test1 As String)

    End Sub
    Overloads Sub subvb()

    End Sub
End Class
Class MethodAttributes1_Inheriter
    Inherits MethodAttributes1
    NotOverridable Overrides Sub sub4()

    End Sub
    Overrides Sub sub6()

    End Sub
End Class