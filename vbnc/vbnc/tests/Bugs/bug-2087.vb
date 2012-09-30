Imports System

Public Class Base

    Public Sub New(ByVal param As Integer)
    End Sub

End Class

Public Class D
    Inherits Base

    Public Sub New(ByVal newParam As Integer)
        MyBase.New(p)
        Dim p As Integer = newParam
    End Sub
End Class