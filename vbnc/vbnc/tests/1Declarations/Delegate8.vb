Imports System.Threading
Imports System.IO

Public MustInherit Class Listener

    Public Delegate Sub processClientDelegate(ByVal client As Stream)

    Protected processClient As processClientDelegate

    Protected Sub invokeDelegate(ByRef client As Stream)
        Dim t As Object = New ParameterizedThreadStart(AddressOf processClient.Invoke)
    End Sub

End Class
