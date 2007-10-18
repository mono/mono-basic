Imports System.Net


Class UseOtherOverload

    Shared Sub Main
    End Sub

    Sub Bbbbb
        Dim handler As New EventHandler(AddressOf FillDevices)
    End Sub

    Sub Ccccc
        Dim handler As EventHandler = New EventHandler(AddressOf FillDevices)
    End Sub

    Sub Ddddd
        Me.BeginInvoke(AddressOf FillDevices)
    End Sub

    Sub Aaaaa
        Dim handler As EventHandler = AddressOf FillDevices
        'Me.BeginInvoke(handler)
    End Sub


    Private Sub FillDevices(ByVal sender As Object, ByVal e As EventArgs)
        Dim devices() As WebRequest = Nothing
            '... ...
        OriginallyThoughThisOverloadWasTheIssue_FillDevicesFill(devices)
        'DumpDeviceInfo(devices, startTime, endTime)
    End Sub

    Private Sub OriginallyThoughThisOverloadWasTheIssue_FillDevicesFill(ByVal devices() As WebRequest)
        '... ...
    End Sub

    '
    Public Function BeginInvoke(method As EventHandler) As IAsyncResult
    '' OTHER BUG Public Function BeginInvoke(method As Delegate) As IAsyncResult
        'fake
    End Function
End Class

