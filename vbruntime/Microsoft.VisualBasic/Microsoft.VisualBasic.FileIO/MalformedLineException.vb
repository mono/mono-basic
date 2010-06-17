'
' MalformedLineException.vb
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
Imports System.ComponentModel
Imports System.Security.Permissions
Imports system.Runtime.Serialization

Namespace Microsoft.VisualBasic.FileIO
    <Serializable()> _
    Public Class MalformedLineException
        Inherits Exception

        Private m_LineNumber As Long
        Private m_AnyMessage As Boolean

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal message As String)
            MyBase.New(message)
            m_AnyMessage = Not String.IsNullOrEmpty(message)
        End Sub

        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
            MyBase.New(info, context)
            If info IsNot Nothing Then m_LineNumber = info.GetInt64("LineNumber")
        End Sub

        Public Sub New(ByVal message As String, ByVal innerException As Exception)
            MyBase.New(message, innerException)
            m_AnyMessage = Not String.IsNullOrEmpty(message)
        End Sub

        Public Sub New(ByVal message As String, ByVal lineNumber As Long)
            MyBase.New(message)
            m_LineNumber = lineNumber
            m_AnyMessage = Not String.IsNullOrEmpty(message)
        End Sub

        Public Sub New(ByVal message As String, ByVal lineNumber As Long, ByVal innerException As Exception)
            MyBase.New(message, innerException)
            m_LineNumber = lineNumber
            m_AnyMessage = Not String.IsNullOrEmpty(message)
        End Sub

        <EditorBrowsable(EditorBrowsableState.Advanced), SecurityPermission(SecurityAction.Demand, SerializationFormatter:=True)> _
        Public Overrides Sub GetObjectData(ByVal info As SerializationInfo, ByVal context As StreamingContext)
            MyBase.GetObjectData(info, context)
            If info IsNot Nothing Then info.AddValue("LineNumber", m_LineNumber)
        End Sub

        Public Overrides Function ToString() As String
            Dim msg As String

            msg = "Microsoft.VisualBasic.FileIO.MalformedLineException: "

            If m_AnyMessage Then
                msg &= MyBase.Message
            Else
                msg &= "Exception of type 'Microsoft.VisualBasic.FileIO.MalformedLineException' was thrown."
            End If

            If MyBase.InnerException IsNot Nothing Then
                msg &= " ---> " & MyBase.InnerException.ToString & Environment.NewLine
                msg &= MyBase.InnerException.StackTrace & "   --- End of inner exception stack trace ---"
            End If

            msg = msg & " Line Number:" & m_LineNumber.ToString

            Return msg
        End Function

        <EditorBrowsable(EditorBrowsableState.Always)> _
        Public Property LineNumber() As Long
            Get
                Return m_LineNumber
            End Get
            Set(ByVal value As Long)
                m_LineNumber = value
            End Set
        End Property
    End Class

End Namespace
