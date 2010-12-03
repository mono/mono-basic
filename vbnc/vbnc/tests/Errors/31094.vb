Imports System.Runtime.InteropServices

Class IClass
    Implements I

    Public Sub New(ByVal foo As Integer)

    End Sub
End Class

Interface II

End Interface

<CoClass(GetType(II))> _
Interface I

End Interface

Class C
    Shared Sub Main()
        Dim o As Object
        o = New I(1)
    End Sub
End Class