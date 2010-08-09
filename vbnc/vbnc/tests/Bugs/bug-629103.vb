Imports System.Runtime.InteropServices

Public Class TestCase

    Private Shared Sub M1(ByRef HidGuid As Guid)
    End Sub

    Public Shared Function M2() As System.Guid
        M1(M2)
    End Function

End Class
