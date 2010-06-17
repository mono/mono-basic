' AudioTests.vb - NUnit Test Cases for Microsoft.VisualBasic.Devices.Audio
'
' Rolf Bjarne Kvinge  (RKvinge@novell.com)
'
' 
' Copyright (C) 2007 Novell, Inc (http://www.novell.com)
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

#If Not TARGET_JVM Then

Imports Microsoft.VisualBasic.Devices

Namespace Devices
    <TestFixture()> _
    Public Class AudioTests
        <Category("TargetJvmNotSupported")> _
        <Category("UI")> _
        <Test()> _
        Public Sub Test()
            Dim testname As String = "Test"
            Dim sc As New Audio
            Dim result As MsgBoxResult

            If MsgBox("Do you have audio on your system (and is it turned on)?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                Assert.Ignore("Ignored since the user cannot hear anything.")
                Return
            End If

            MsgBox("You should now here a beep.")
            sc.PlaySystemSound(Media.SystemSounds.Beep)
            result = MsgBox("Did you hear the sound?", MsgBoxStyle.Question Or MsgBoxStyle.YesNoCancel)
            If result = MsgBoxResult.Cancel Then
                Assert.Ignore("Cancelled by the user")
                Return
            End If
            Assert.AreEqual(MsgBoxResult.Yes, result, testname & "-1")


            Dim soundFile As String
            Dim bytes() As Byte
            Dim i As Integer


            soundFile = "sound.wav"

            If IO.File.Exists(soundFile) = False Then
                Assert.Ignore("Could not find an audio file.")
                Return
            End If

            bytes = Microsoft.VisualBasic.FileIO.FileSystem.ReadAllBytes(soundFile)

            i += 1
            MsgBox("You should now here the test sound (once).")
            sc.Play(soundFile)
            result = MsgBox("Did you hear the sound?", MsgBoxStyle.Question Or MsgBoxStyle.YesNoCancel)
            If result = MsgBoxResult.Cancel Then
                Assert.Ignore("Cancelled by the user")
                Return
            End If
            Assert.AreEqual(MsgBoxResult.Yes, result, testname & "-" & i)

            i += 1
            MsgBox("You should now here the test sound (once).")
            sc.Play(soundFile, AudioPlayMode.WaitToComplete)
            result = MsgBox("Did you hear the sound?", MsgBoxStyle.Question Or MsgBoxStyle.YesNoCancel)
            If result = MsgBoxResult.Cancel Then
                Assert.Ignore("Cancelled by the user")
                Return
            End If
            Assert.AreEqual(MsgBoxResult.Yes, result, testname & "-" & i)


            i += 1
            MsgBox("You should now here the test sound (once).")
            sc.Play(soundFile, AudioPlayMode.Background)
            result = MsgBox("Did you hear the sound and did this messagebox appear before the sound stopped?", MsgBoxStyle.Question Or MsgBoxStyle.YesNoCancel)
            If result = MsgBoxResult.Cancel Then
                Assert.Ignore("Cancelled by the user")
                Return
            End If
            Assert.AreEqual(MsgBoxResult.Yes, result, testname & "-" & i)

            i += 1
            MsgBox("You should now here the test sound (looping).")
            sc.Play(soundFile, AudioPlayMode.BackgroundLoop)
            result = MsgBox("Do you hear the sound?", MsgBoxStyle.Question Or MsgBoxStyle.YesNoCancel)
            If result = MsgBoxResult.Cancel Then
                Assert.Ignore("Cancelled by the user")
                Return
            End If
            Assert.AreEqual(MsgBoxResult.Yes, result, testname & "-" & i)

            i += 1
            MsgBox("You should now here the test sound (once).")
            sc.Play(bytes, AudioPlayMode.WaitToComplete)
            result = MsgBox("Did you hear the sound?", MsgBoxStyle.Question Or MsgBoxStyle.YesNoCancel)
            If result = MsgBoxResult.Cancel Then
                Assert.Ignore("Cancelled by the user")
                Return
            End If
            Assert.AreEqual(MsgBoxResult.Yes, result, testname & "-" & i)


            i += 1
            MsgBox("You should now here the test sound (once).")
            sc.Play(bytes, AudioPlayMode.Background)
            result = MsgBox("Did you hear the sound and did this messagebox appear before the sound stopped?", MsgBoxStyle.Question Or MsgBoxStyle.YesNoCancel)
            If result = MsgBoxResult.Cancel Then
                Assert.Ignore("Cancelled by the user")
                Return
            End If
            Assert.AreEqual(MsgBoxResult.Yes, result, testname & "-" & i)

            i += 1
            MsgBox("You should now here the test sound (looping).")
            sc.Play(bytes, AudioPlayMode.BackgroundLoop)
            result = MsgBox("Do you hear the sound?", MsgBoxStyle.Question Or MsgBoxStyle.YesNoCancel)
            If result = MsgBoxResult.Cancel Then
                Assert.Ignore("Cancelled by the user")
                Return
            End If

            i += 1
            Using stream As New IO.MemoryStream(bytes)
                Assert.AreEqual(MsgBoxResult.Yes, result, testname & "-" & i)
                MsgBox("You should now here the test sound (once).")
                sc.Play(stream, AudioPlayMode.WaitToComplete)
                result = MsgBox("Did you hear the sound?", MsgBoxStyle.Question Or MsgBoxStyle.YesNoCancel)
                If result = MsgBoxResult.Cancel Then
                    Assert.Ignore("Cancelled by the user")
                    Return
                End If
                Assert.AreEqual(MsgBoxResult.Yes, result, testname & "-" & i)
            End Using

            i += 1
            Using stream As New IO.MemoryStream(bytes)
                MsgBox("You should now here the test sound (once).")
                sc.Play(stream, AudioPlayMode.Background)
                result = MsgBox("Did you hear the sound and did this messagebox appear before the sound stopped?", MsgBoxStyle.Question Or MsgBoxStyle.YesNoCancel)
                If result = MsgBoxResult.Cancel Then
                    Assert.Ignore("Cancelled by the user")
                    Return
                End If
                Assert.AreEqual(MsgBoxResult.Yes, result, testname & "-" & i)
            End Using

            i += 1
            Using stream As New IO.MemoryStream(bytes)
                MsgBox("You should now here the test sound (looping).")
                sc.Play(stream, AudioPlayMode.BackgroundLoop)
                result = MsgBox("Do you hear the sound?", MsgBoxStyle.Question Or MsgBoxStyle.YesNoCancel)
                If result = MsgBoxResult.Cancel Then
                    Assert.Ignore("Cancelled by the user")
                    Return
                End If
                Assert.AreEqual(MsgBoxResult.Yes, result, testname & "-" & i)
            End Using





            MsgBox("Audio test finished")
        End Sub
    End Class
End Namespace
#End If
