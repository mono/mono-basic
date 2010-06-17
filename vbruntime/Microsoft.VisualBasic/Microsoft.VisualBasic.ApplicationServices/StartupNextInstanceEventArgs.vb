'
' Microsoft.VisualBasic.ApplicationServices.StartupNextInstanceEventArgs.vb
'
' Authors:
'   Rolf Bjarne Kvinge  (RKvinge@novell.com)
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

Imports System.ComponentModel
Imports System.Collections.ObjectModel
Imports System.Runtime
Imports System.Runtime.InteropServices

Namespace Microsoft.VisualBasic.ApplicationServices
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public Class StartupNextInstanceEventArgs
        Inherits EventArgs

        Private m_BringToForeground As Boolean
        Private m_CommandLine As ReadOnlyCollection(Of String)

        Public Sub New(ByVal args As ReadOnlyCollection(Of String), ByVal bringToForegroundFlag As Boolean)
            m_BringToForeground = bringToForegroundFlag
            m_CommandLine = args
        End Sub

        Public Property BringToForeground() As Boolean
            Get
                Return m_BringToForeground
            End Get
            Set(ByVal value As Boolean)
                m_BringToForeground = value
            End Set
        End Property

        Public ReadOnly Property CommandLine() As ReadOnlyCollection(Of String)
            Get
                Return m_CommandLine
            End Get
        End Property
    End Class
End Namespace
