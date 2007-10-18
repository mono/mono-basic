Imports System

Class A
    Public Sub New()
    End Sub

    Public Sub New(ByVal name As String)
        If name <> "abc" Then
            Throw New exception("#A1, Unexpected result")
        End If
    End Sub
End Class

Class B
    ' implicitly call base class default ctor
    Public Sub New()
    End Sub
End Class

Module M
    Function Main() As Integer
        Dim x As A = New A()
        Dim y As A = New A("abc")
        Dim z As B = New B()
    End Function
End Module
