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
    Inherits A

    ' call base class ctor explicitly
    Public Sub New()
        MyBase.New()
    End Sub
End Class

Class C
    Inherits A

    ' call base class ctor with parameter
    Public Sub New()
        MyBase.NEw("abc")
    End Sub
End Class

Class D

    ' call another ctor in the same class
    ' either of the methods mentioned below should give same result
    Public Sub New()
        MyClass.NEw("aaa")
        'Me.NEw("aaa")
    End Sub

    Public Sub New(ByVal name As String)
        If name <> "aaa" Then
            Throw New exception("#A2, Unexpected result")
        End If
    End Sub
End Class

Module M
    Function Main() As Integer
        Dim x As B = New B()
        Dim y As C = New C()
        Dim z As D = New D()
    End Function
End Module
