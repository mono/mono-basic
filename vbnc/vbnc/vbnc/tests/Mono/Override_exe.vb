REM CompilerOptions: /r:Override_dll.dll

Option Strict Off

Imports System
Imports NSOverride


Class D
    Inherits B2

    Public Overrides Function S1(ByVal i As Integer)
    End Function

    Public Overrides Function S2(ByVal i As Integer, ByVal j As String)
    End Function

    Public Overrides ReadOnly Property SS(ByVal i As Integer, ByVal y As String) As Integer
        Get
        End Get
    End Property

    Public Overrides WriteOnly Property SS1(ByVal i As Integer, ByVal y As String) As Integer
        Set(ByVal Value As Integer)
        End Set
    End Property

    Public Overrides Property SS2(ByVal i As Integer, ByVal y As String) As Integer
        Get
        End Get
        Set(ByVal Value As Integer)
        End Set
    End Property
End Class

Module M
    Function main() As Integer
    End Function
End Module
