'
' Log.vb
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

Namespace Microsoft.VisualBasic.Logging
    Public Class Log

        Private m_Source As TraceSource

        Private Shared m_IDs() As Integer = New Integer() {3, 2, 1, 0, 8, -1, -1, -1, 4, 5, 6, 7, 9}

        Public Sub New()
            'Empty constructor

            m_Source = New TraceSource("DefaultSource")
            InitializeWithDefaultsSinceNoConfigExists()

        End Sub

        Public Sub New(ByVal name As String)
            m_Source = New TraceSource(name)
            InitializeWithDefaultsSinceNoConfigExists()
        End Sub

        Protected Friend Overridable Sub InitializeWithDefaultsSinceNoConfigExists()
            m_Source.Listeners.Add(New FileLogTraceListener("FileLog"))
            m_Source.Switch.Level = SourceLevels.Information
        End Sub

        Private Shared Function GetIDOfType(ByVal severity As TraceEventType) As Integer
            Return m_IDs(CInt(System.Math.Log(CInt(severity), 2)))
        End Function

        Public Sub WriteEntry(ByVal message As String)
            WriteEntry(message, TraceEventType.Information, GetIDOfType(TraceEventType.Information))
        End Sub


        Public Sub WriteEntry(ByVal message As String, ByVal severity As TraceEventType)
            WriteEntry(message, severity, GetIDOfType(severity))
        End Sub

        Public Sub WriteEntry(ByVal message As String, ByVal severity As TraceEventType, ByVal id As Integer)
            If message Is Nothing Then message = String.Empty

            'This is necessary, because if TRACE = false the call to TraceEvent is removed.
#Const TRACEDEFAULT = Trace
#Const TRACE = True
            m_Source.TraceEvent(severity, id, message)
#Const TRACE = TRACEDEFAULT

        End Sub

        Public Sub WriteException(ByVal ex As Exception)
            WriteException(ex, TraceEventType.Error, String.Empty, GetIDOfType(TraceEventType.Error))
        End Sub

        Public Sub WriteException(ByVal ex As Exception, ByVal severity As TraceEventType, ByVal additionalInfo As String)
            WriteException(ex, severity, additionalInfo, GetIDOfType(severity))
        End Sub

        Public Sub WriteException(ByVal ex As Exception, ByVal severity As TraceEventType, ByVal additionalInfo As String, ByVal id As Integer)
            Dim msg As String

            If ex Is Nothing Then Throw New ArgumentNullException("ex")
            msg = ex.Message
            If Not String.IsNullOrEmpty(additionalInfo) Then
                msg &= " " & additionalInfo
            End If
            WriteEntry(msg, severity, id)
        End Sub

        Public ReadOnly Property DefaultFileLogWriter() As FileLogTraceListener
            Get
                Return TryCast(m_Source.Listeners("FileLog"), FileLogTraceListener)
            End Get
        End Property

        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public ReadOnly Property TraceSource() As TraceSource
            Get
                Return m_Source
            End Get
        End Property
    End Class
End Namespace
