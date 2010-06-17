'
' SpecialDirectoryProxy.vb
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

#If TARGET_JVM = False Then 'Windows.Forms Not Supported by Grasshopper
Imports System.ComponentModel
Imports System.Windows.Forms

Namespace Microsoft.VisualBasic.MyServices
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Public Class SpecialDirectoriesProxy

        Friend Sub New()
            'Empty constructor
        End Sub

        Public ReadOnly Property AllUsersApplicationData() As String
            Get
                Return FileIO.SpecialDirectories.AllUsersApplicationData
            End Get
        End Property

        Public ReadOnly Property CurrentUserApplicationData() As String
            Get
                Return FileIO.SpecialDirectories.CurrentUserApplicationData
            End Get
        End Property

        Public ReadOnly Property Desktop() As String
            Get
                Return FileIO.SpecialDirectories.Desktop
            End Get
        End Property

        Public ReadOnly Property MyDocuments() As String
            Get
                Return FileIO.SpecialDirectories.MyDocuments
            End Get
        End Property

        Public ReadOnly Property MyMusic() As String
            Get
                Return FileIO.SpecialDirectories.MyMusic
            End Get
        End Property

        Public ReadOnly Property MyPictures() As String
            Get
                Return FileIO.SpecialDirectories.MyPictures
            End Get
        End Property

        Public ReadOnly Property ProgramFiles() As String
            Get
                Return FileIO.SpecialDirectories.ProgramFiles
            End Get
        End Property

        Public ReadOnly Property Programs() As String
            Get
                Return FileIO.SpecialDirectories.Programs
            End Get
        End Property

        Public ReadOnly Property Temp() As String
            Get
                Return FileIO.SpecialDirectories.Temp
            End Get
        End Property
    End Class
End Namespace
#End If
