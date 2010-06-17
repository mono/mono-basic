'
' Audio.vb
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
#If TARGET_JVM = False Then 'System.Media Not Supported by Grasshopper
Imports System
Imports System.IO
Imports System.Media

Namespace Microsoft.VisualBasic.Devices
    Public Class Audio
        Private m_Player As SoundPlayer

        Public Sub New()
            'Empty
        End Sub

        Public Sub Play(ByVal location As String)
            Play(New SoundPlayer(location), AudioPlayMode.Background)
        End Sub

        Public Sub Play(ByVal location As String, ByVal playMode As AudioPlayMode)
            Play(New SoundPlayer(location), playMode)
        End Sub

        Public Sub Play(ByVal stream As Stream, ByVal playMode As AudioPlayMode)
            Play(New SoundPlayer(stream), playMode)
        End Sub

        Public Sub Play(ByVal data As Byte(), ByVal playMode As AudioPlayMode)
            Using stream As New MemoryStream(data)
                Play(New SoundPlayer(stream), playMode)
            End Using
        End Sub

        Public Sub PlaySystemSound(ByVal systemSound As SystemSound)
            systemSound.Play()
        End Sub

        Public Sub [Stop]()
            If m_Player IsNot Nothing Then m_Player.Stop()
        End Sub

        Private Sub Play(ByVal player As SoundPlayer, ByVal mode As AudioPlayMode)
            If m_Player IsNot Nothing Then
                m_Player.Stop()
            End If
            m_Player = player
            Select Case mode
                Case AudioPlayMode.Background
                    player.Play()
                Case AudioPlayMode.BackgroundLoop
                    player.PlayLooping()
                Case AudioPlayMode.WaitToComplete
                    player.PlaySync()
                Case Else
                    player.PlaySync()
            End Select
        End Sub
    End Class
End Namespace
#End If
