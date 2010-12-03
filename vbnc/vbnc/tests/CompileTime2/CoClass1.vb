Imports System.Runtime.InteropServices

Class IClass
    Implements IC

    Public Sub New(ByVal foo As Integer)

    End Sub
End Class

Structure IStructure
    Implements [IS]

    Dim i As Integer

    Public Sub New(ByVal foo As Integer)

    End Sub

End Structure

<CoClass(GetType(IStructure))> _
Interface [IS]

End Interface

<CoClass(GetType(IClass))> _
Interface IC

End Interface

Class C
    Shared Sub Main()
        Dim o As Object
        o = New [IS](1)
        o = New IC(1)

    End Sub
End Class