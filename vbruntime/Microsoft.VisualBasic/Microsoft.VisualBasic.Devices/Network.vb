'
' Network.vb
'
' Authors:
'   Rolf Bjarne Kvinge (RKvinge@novell.com>
'
' Copyright (C) 2007 Novell (http://www.novell.com)
'
' Permission is hereby granted, free of charge, to any person obtaining
' a copy of this software and associated documentation files (the
' "Software"), to deal in the Software without restriction, including
' without limitation the rights to use, copy, modify, merge, publish,
' distribute, sublicense, and/or sell copies of the Software, and to
' permit persons to whom the Software is furnished to do so, subject to
' the following conditions:
' 
' The above copyright notice and this permission notice shall be
' included in all copies or substantial portions of the Software.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
' EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
' MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
' NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
' LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
' OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
' WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
'

Imports System.Net
Imports Microsoft.VisualBasic.FileIO
Imports System.Net.NetworkInformation

Namespace Microsoft.VisualBasic.Devices
    Public Class Network

        'MonoTODO
        Public Event NetworkAvailabilityChanged As NetworkAvailableEventHandler

        Private Const DEFAULTTIMEOUT As Integer = 100000

        Public Sub New()
            'Empty
        End Sub

        Public Sub DownloadFile(ByVal address As String, ByVal destinationFileName As String)
            DownloadFile(address, destinationFileName, String.Empty, String.Empty, False, DEFAULTTIMEOUT, False, UICancelOption.ThrowException)
        End Sub

        Public Sub DownloadFile(ByVal address As Uri, ByVal destinationFileName As String)
            DownloadFile(address, destinationFileName, String.Empty, String.Empty, False, DEFAULTTIMEOUT, False, UICancelOption.ThrowException)
        End Sub

        Public Sub DownloadFile(ByVal address As String, ByVal destinationFileName As String, ByVal userName As String, ByVal password As String)
            DownloadFile(address, destinationFileName, userName, password, False, DEFAULTTIMEOUT, False, UICancelOption.ThrowException)
        End Sub

        Public Sub DownloadFile(ByVal address As Uri, ByVal destinationFileName As String, ByVal userName As String, ByVal password As String)
            DownloadFile(address, destinationFileName, userName, password, False, DEFAULTTIMEOUT, False, UICancelOption.ThrowException)
        End Sub

        Public Sub DownloadFile(ByVal address As Uri, ByVal destinationFileName As String, ByVal networkCredentials As ICredentials, ByVal showUI As Boolean, ByVal connectionTimeout As Integer, ByVal overwrite As Boolean)
            DownloadFile(address, destinationFileName, networkCredentials, showUI, connectionTimeout, overwrite, UICancelOption.ThrowException)
        End Sub

        Public Sub DownloadFile(ByVal address As String, ByVal destinationFileName As String, ByVal userName As String, ByVal password As String, ByVal showUI As Boolean, ByVal connectionTimeout As Integer, ByVal overwrite As Boolean)
            DownloadFile(address, destinationFileName, userName, password, showUI, connectionTimeout, overwrite, UICancelOption.ThrowException)
        End Sub

        Public Sub DownloadFile(ByVal address As Uri, ByVal destinationFileName As String, ByVal networkCredentials As ICredentials, ByVal showUI As Boolean, ByVal connectionTimeout As Integer, ByVal overwrite As Boolean, ByVal onUserCancel As UICancelOption)
            Dim client As WebClient

            If connectionTimeout < 0 Then
                Throw New ArgumentOutOfRangeException("connectionTimeout")
            End If

            If IO.File.Exists(destinationFileName) Then
                If overwrite Then
                    IO.File.Delete(destinationFileName)
                Else
                    Throw New System.IO.IOException(String.Format("The file '{0}' already exists.", destinationFileName))
                End If
            End If

            client = New MyWebClient(connectionTimeout)

            If networkCredentials IsNot Nothing Then
                client.Credentials = networkCredentials
            End If

            If showUI Then
#If TARGET_JVM = False Then 'Windows.Forms Not Supported by Grasshopper
                Dim progress As MyProgressDialog
                progress = New MyProgressDialog(client, String.Format("Downloading '{0}'...", address.ToString))

                client.DownloadFileAsync(address, destinationFileName)

                If progress.ShowDialog() = Windows.Forms.DialogResult.Cancel AndAlso onUserCancel = UICancelOption.ThrowException Then
                    Throw New OperationCanceledException("The operation was canceled.")
                End If
#End If
            Else
                client.DownloadFile(address, destinationFileName)
            End If
        End Sub

        Public Sub DownloadFile(ByVal address As Uri, ByVal destinationFileName As String, ByVal userName As String, ByVal password As String, ByVal showUI As Boolean, ByVal connectionTimeout As Integer, ByVal overwrite As Boolean)
            DownloadFile(address, destinationFileName, userName, password, showUI, connectionTimeout, overwrite, UICancelOption.ThrowException)
        End Sub

        Public Sub DownloadFile(ByVal address As String, ByVal destinationFileName As String, ByVal userName As String, ByVal password As String, ByVal showUI As Boolean, ByVal connectionTimeout As Integer, ByVal overwrite As Boolean, ByVal onUserCancel As UICancelOption)
            Dim credentials As NetworkCredential
            Dim uri As Uri

            credentials = New NetworkCredential(userName, password)
            uri = New Uri(address)

            DownloadFile(uri, destinationFileName, credentials, showUI, connectionTimeout, overwrite, onUserCancel)
        End Sub

        Public Sub DownloadFile(ByVal address As Uri, ByVal destinationFileName As String, ByVal userName As String, ByVal password As String, ByVal showUI As Boolean, ByVal connectionTimeout As Integer, ByVal overwrite As Boolean, ByVal onUserCancel As UICancelOption)
            Dim credentials As NetworkCredential
            credentials = New NetworkCredential(userName, password)
            DownloadFile(address, destinationFileName, credentials, showUI, connectionTimeout, overwrite, onUserCancel)
        End Sub

        <MonoNotSupported("")> _
        Public Function Ping(ByVal hostNameOrAddress As String) As Boolean
            Return Ping(hostNameOrAddress, 500)
        End Function

        <MonoNotSupported("")> _
        Public Function Ping(ByVal address As Uri) As Boolean
            Return Ping(address.Host, 500)
        End Function

        ' Not supported, since uses unsupported System.Net.NetworkInformation.Ping.Send()
        <MonoNotSupported("")> _
        Public Function Ping(ByVal hostNameOrAddress As String, ByVal timeout As Integer) As Boolean
#If TARGET_JVM = True Then
			Throw New NotSupportedException()
#Else
            If IsAvailable = False Then
                Throw New InvalidOperationException("Network not available.")
            End If

            Dim worker As Ping
            Dim result As PingReply

            worker = New Ping()
            result = worker.Send(hostNameOrAddress, timeout)

            Return result.Status = IPStatus.Success
#End If
        End Function

        <MonoNotSupported("")> _
        Public Function Ping(ByVal address As Uri, ByVal timeout As Integer) As Boolean
            Return Ping(address.Host, timeout)
        End Function

        Public Sub UploadFile(ByVal sourceFileName As String, ByVal address As String)
            UploadFile(sourceFileName, address, String.Empty, String.Empty, False, DEFAULTTIMEOUT, UICancelOption.ThrowException)
        End Sub

        Public Sub UploadFile(ByVal sourceFileName As String, ByVal address As Uri)
            UploadFile(sourceFileName, address, String.Empty, String.Empty, False, DEFAULTTIMEOUT, UICancelOption.ThrowException)
        End Sub

        Public Sub UploadFile(ByVal sourceFileName As String, ByVal address As String, ByVal userName As String, ByVal password As String)
            UploadFile(sourceFileName, address, userName, password, False, DEFAULTTIMEOUT, UICancelOption.ThrowException)
        End Sub

        Public Sub UploadFile(ByVal sourceFileName As String, ByVal address As Uri, ByVal userName As String, ByVal password As String)
            UploadFile(sourceFileName, address, userName, password)
        End Sub

        Public Sub UploadFile(ByVal sourceFileName As String, ByVal address As Uri, ByVal networkCredentials As ICredentials, ByVal showUI As Boolean, ByVal connectionTimeout As Integer)
            UploadFile(sourceFileName, address, networkCredentials, showUI, connectionTimeout, UICancelOption.ThrowException)
        End Sub

        Public Sub UploadFile(ByVal sourceFileName As String, ByVal address As String, ByVal userName As String, ByVal password As String, ByVal showUI As Boolean, ByVal connectionTimeout As Integer)
            UploadFile(sourceFileName, address, userName, password, showUI, connectionTimeout, UICancelOption.ThrowException)
        End Sub

        Public Sub UploadFile(ByVal sourceFileName As String, ByVal address As Uri, ByVal networkCredentials As ICredentials, ByVal showUI As Boolean, ByVal connectionTimeout As Integer, ByVal onUserCancel As UICancelOption)
            Dim client As WebClient

            If connectionTimeout < 0 Then
                Throw New ArgumentOutOfRangeException("connectionTimeout")
            End If

            client = New MyWebClient(connectionTimeout)

            If networkCredentials IsNot Nothing Then
                client.Credentials = networkCredentials
            End If

            If showUI Then
#If TARGET_JVM = False Then 'Windows.Forms Not Supported by Grasshopper
                Dim progress As MyProgressDialog
                progress = New MyProgressDialog(client, String.Format("Uploading '{0}'...", address.ToString))

                client.UploadFileAsync(address, sourceFileName)

                If progress.ShowDialog() = Windows.Forms.DialogResult.Cancel AndAlso onUserCancel = UICancelOption.ThrowException Then
                    Throw New OperationCanceledException("The operation was canceled.")
                End If
#End If
            Else
                client.UploadFile(address, sourceFileName)
            End If
        End Sub

        Public Sub UploadFile(ByVal sourceFileName As String, ByVal address As Uri, ByVal userName As String, ByVal password As String, ByVal showUI As Boolean, ByVal connectionTimeout As Integer)
            UploadFile(sourceFileName, address, userName, password, showUI, connectionTimeout, UICancelOption.ThrowException)
        End Sub

        Public Sub UploadFile(ByVal sourceFileName As String, ByVal address As String, ByVal userName As String, ByVal password As String, ByVal showUI As Boolean, ByVal connectionTimeout As Integer, ByVal onUserCancel As UICancelOption)
            Dim credentials As NetworkCredential
            Dim uri As Uri

            credentials = New NetworkCredential(userName, password)
            uri = New Uri(address)

            UploadFile(sourceFileName, uri, credentials, showUI, connectionTimeout, onUserCancel)
        End Sub

        Public Sub UploadFile(ByVal sourceFileName As String, ByVal address As Uri, ByVal userName As String, ByVal password As String, ByVal showUI As Boolean, ByVal connectionTimeout As Integer, ByVal onUserCancel As UICancelOption)
            Dim credentials As NetworkCredential

            credentials = New NetworkCredential(userName, password)

            UploadFile(sourceFileName, address, credentials, showUI, connectionTimeout, onUserCancel)
        End Sub

        Public ReadOnly Property IsAvailable() As Boolean
            Get
#If TARGET_JVM = True Then
				Throw New NotSupportedException()
#Else
                Return NetworkInterface.GetIsNetworkAvailable()
#End If
            End Get
        End Property
    End Class
End Namespace
