'
' MonoTODOAttribute.vb
'
' Authors:
'   Ravi Pratap (ravi@ximian.com)
'   Eyal Alaluf <eyala@mainsoft.com> 
'
' (C) Ximian, Inc.  http:'www.ximian.com
'

'
' Copyright (C) 2004 Novell, Inc (http:'www.novell.com)
' Copyright (C) 2006 Mainsoft, Inc (http:'www.mainsoft.com)
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
Namespace System

    <AttributeUsage(AttributeTargets.All)> _
    Friend Class MonoTODOAttribute
        Inherits Attribute

        Private _comment As String

        Public Sub New()
        End Sub

        Public Sub New(ByVal comment As String)
            _comment = comment
        End Sub

        Public ReadOnly Property Comment() As String
            Get
                Return _comment
            End Get

        End Property
    End Class

    <AttributeUsage(AttributeTargets.All)> _
    Friend Class MonoDocumentationNoteAttribute
        Inherits MonoTODOAttribute

        Public Sub New(ByVal comment As String)
            MyBase.New(comment)
        End Sub
    End Class

    <AttributeUsage(AttributeTargets.All)> _
    Friend Class MonoExtensionAttribute
        Inherits MonoTODOAttribute

        Public Sub New(ByVal comment As String)
            MyBase.New(comment)
        End Sub
    End Class

    <AttributeUsage(AttributeTargets.All)> _
    Friend Class MonoInternalNoteAttribute
        Inherits MonoTODOAttribute

        Public Sub New(ByVal comment As String)
            MyBase.New(comment)
        End Sub
    End Class

    <AttributeUsage(AttributeTargets.All)> _
    Friend Class MonoLimitationAttribute
        Inherits MonoTODOAttribute

        Public Sub New(ByVal comment As String)
            MyBase.New(comment)
        End Sub
    End Class

    <AttributeUsage(AttributeTargets.All)> _
    Friend Class MonoNotSupportedAttribute
        Inherits MonoTODOAttribute

        Public Sub New(ByVal comment As String)
            MyBase.New(comment)
        End Sub
    End Class
End Namespace
