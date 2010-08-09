Option Explicit On
Option Strict On

Imports System



Public Class TestCase2

    Public Sub New()
        MyBase.New()
    End Sub

    Private _aField() As Integer

    Public ReadOnly Property AField() As Integer()
        Get
            AField = _aField
            REM	      Dim Ret() As Integer
            REM	      Ret = _aField
            REM	      Return Ret
        End Get
    End Property

End Class
