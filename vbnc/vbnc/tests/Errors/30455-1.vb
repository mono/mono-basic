Imports System.Runtime.InteropServices

Class IClass
    Implements I

    Public Sub New(ByVal foo As Integer)

    End Sub
End Class

<CoClass(GetType(IClass))> _
Interface I

End Interface

Class C
    Shared Sub Main()
        Dim o As Object
        o = New I(1)
        o = New I()
    End Sub
End Class