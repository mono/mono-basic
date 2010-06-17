'
' SpecialDirectories.vb
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
#If TARGET_JVM = False Then 'Windows.Forms Not Supported by Grasshopper
Imports System.Windows.Forms

Namespace Microsoft.VisualBasic.FileIO
    Public Class SpecialDirectories
        Public Sub New()
            'Empty constructor
        End Sub

        Private Shared Function RemovePathSeparator(ByVal Path As String) As String
            If Path.Contains("\\") AndAlso Path.StartsWith("\\") = False Then
                Path = Path.Replace("\\", "\")
            End If
            If Path.EndsWith(System.IO.Path.DirectorySeparatorChar.ToString()) Then
                Return Path.Substring(0, Path.Length - 1)
            Else
                Return Path
            End If
        End Function

        Public Shared ReadOnly Property AllUsersApplicationData() As String
            Get
                Return RemovePathSeparator(Application.CommonAppDataPath)
            End Get
        End Property

        Public Shared ReadOnly Property CurrentUserApplicationData() As String
            Get
                Return RemovePathSeparator(Application.UserAppDataPath)
            End Get
        End Property

        Public Shared ReadOnly Property Desktop() As String
            Get
                Return RemovePathSeparator(Environment.GetFolderPath(Environment.SpecialFolder.Desktop))
            End Get
        End Property

        Public Shared ReadOnly Property MyDocuments() As String
            Get
                Return RemovePathSeparator(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments))
            End Get
        End Property

        Public Shared ReadOnly Property MyMusic() As String
            Get
                Return RemovePathSeparator(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic))
            End Get
        End Property

        Public Shared ReadOnly Property MyPictures() As String
            Get
                Return RemovePathSeparator(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures))
            End Get
        End Property

        Public Shared ReadOnly Property ProgramFiles() As String
            Get
                Return RemovePathSeparator(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles))
            End Get
        End Property

        Public Shared ReadOnly Property Programs() As String
            Get
                Return RemovePathSeparator(Environment.GetFolderPath(Environment.SpecialFolder.Programs))
            End Get
        End Property

        Public Shared ReadOnly Property Temp() As String
            Get
                Return RemovePathSeparator(System.IO.Path.GetTempPath)
            End Get
        End Property
    End Class
End Namespace
#End If
