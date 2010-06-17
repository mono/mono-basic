'
' ClipboardProxy.vb
'
' Authors:
'   Rolf Bjarne Kvinge (RKvinge@novell.com)
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

#If TARGET_JVM = False Then
Imports System.ComponentModel
Imports System.Collections.Specialized
Imports System.IO
Imports System.Drawing
Imports System.Windows.Forms

Namespace Microsoft.VisualBasic.MyServices

    <EditorBrowsable(EditorBrowsableState.Never)> _
    Public Class ClipboardProxy
        Friend Sub New()
            'Empty constructor
        End Sub

        Public Sub Clear()
            Clipboard.Clear()
        End Sub

        Public Function ContainsAudio() As Boolean
            Return Clipboard.ContainsAudio
        End Function

        Public Function ContainsData(ByVal format As String) As Boolean
            Return Clipboard.ContainsData(format)
        End Function

        Public Function ContainsFileDropList() As Boolean
            Return Clipboard.ContainsFileDropList()
        End Function

        Public Function ContainsImage() As Boolean
            Return Clipboard.ContainsImage
        End Function

        Public Function ContainsText() As Boolean
            Return Clipboard.ContainsText
        End Function

        Public Function ContainsText(ByVal format As TextDataFormat) As Boolean
            Return Clipboard.ContainsText(format)
        End Function

        Public Function GetAudioStream() As Stream
            Return Clipboard.GetAudioStream
        End Function

        Public Function GetData(ByVal format As String) As Object
            Return Clipboard.GetData(format)
        End Function

        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Function GetDataObject() As IDataObject
            Return Clipboard.GetDataObject()
        End Function

        Public Function GetFileDropList() As StringCollection
            Return Clipboard.GetFileDropList
        End Function

        Public Function GetImage() As Image
            Return Clipboard.GetImage
        End Function

        Public Function GetText() As String
            Return Clipboard.GetText
        End Function

        Public Function GetText(ByVal format As TextDataFormat) As String
            Return Clipboard.GetText(format)
        End Function

        Public Sub SetAudio(ByVal audioStream As Stream)
            Clipboard.SetAudio(audioStream)
        End Sub

        Public Sub SetAudio(ByVal audioBytes As Byte())
            Clipboard.SetAudio(audioBytes)
        End Sub

        Public Sub SetData(ByVal format As String, ByVal data As Object)
            Clipboard.SetData(format, data)
        End Sub

        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Sub SetDataObject(ByVal data As DataObject)
            Clipboard.SetDataObject(data)
        End Sub

        Public Sub SetFileDropList(ByVal filePaths As StringCollection)
            Clipboard.SetFileDropList(filePaths)
        End Sub

        Public Sub SetImage(ByVal image As Image)
            Clipboard.SetImage(image)
        End Sub

        Public Sub SetText(ByVal text As String)
            Clipboard.SetText(text)
        End Sub

        Public Sub SetText(ByVal text As String, ByVal format As TextDataFormat)
            Clipboard.SetText(text, format)
        End Sub
    End Class
End Namespace
#End If
