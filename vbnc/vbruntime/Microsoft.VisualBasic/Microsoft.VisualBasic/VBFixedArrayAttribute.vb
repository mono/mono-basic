'
' VBFixedArrayAttribute.vb
'
' Author:
'   Mizrahi Rafael (rafim@mainsoft.com)
'

'
' Copyright (C) 2002-2006 Mainsoft Corporation.
' Copyright (C) 2004-2006 Novell, Inc (http://www.novell.com)
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
Imports System
Imports System.ComponentModel
Imports System.Runtime.InteropServices

Namespace Microsoft.VisualBasic
    <AttributeUsage(AttributeTargets.Field, Inherited:=False, AllowMultiple:=False)> _
    Public NotInheritable Class VBFixedArrayAttribute
        Inherits Attribute

        Private m_UpperBound1 As Integer
        Private m_UpperBound2 As Integer

        Public Sub New(ByVal UpperBound1 As Integer)
            If UpperBound1 < 0 Then Throw New ArgumentException("Arguments to 'VBFixedArrayAttribute' are not valid.")
            m_UpperBound1 = UpperBound1
            m_UpperBound2 = Integer.MinValue
        End Sub

        Public Sub New(ByVal UpperBound1 As Integer, ByVal UpperBound2 As Integer)
            If UpperBound1 < 0 Then Throw New ArgumentException("Arguments to 'VBFixedArrayAttribute' are not valid.")
            If UpperBound2 < 0 Then Throw New ArgumentException("Arguments to 'VBFixedArrayAttribute' are not valid.")
            m_UpperBound1 = UpperBound1
            m_UpperBound2 = UpperBound2
        End Sub

        Public ReadOnly Property Bounds() As Integer()
            Get
                If m_UpperBound2 = Integer.MinValue Then
                    Return New Integer() {m_UpperBound1}
                Else
                    Return New Integer() {m_UpperBound1, m_UpperBound2}
                End If
            End Get
        End Property

        Public ReadOnly Property Length() As Integer
            Get
                If m_UpperBound2 = Integer.MinValue Then
                    Return m_UpperBound1 + 1
                Else
                    Return (m_UpperBound1 + 1) * (m_UpperBound2 + 1)
                End If
            End Get
        End Property
    End Class
End Namespace
