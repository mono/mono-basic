'
' ComClassAttribute.vb
'
' Authors:
'   Chris J Breisch (cjbreisch@altavista.net) 
'   Rafael Teixeira (rafaelteixeirabr@hotmail.com)
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

Namespace Microsoft.VisualBasic
    <AttributeUsage(AttributeTargets.Class, Inherited:=False, AllowMultiple:=False)> _
    Public NotInheritable Class ComClassAttribute
        Inherits Attribute
        ' Declarations
        Private m_classID As String
        Private m_interfaceID As String
        Private m_eventID As String
        Private m_interfaceShadows As Boolean

        ' Constructors

        Public Sub New()
        End Sub

        Public Sub New(ByVal _ClassID As String)
            m_classID = _ClassID
        End Sub

        Public Sub New(ByVal _ClassID As String, ByVal _InterfaceID As String)
            m_classID = _ClassID
            m_interfaceID = _InterfaceID
        End Sub

        Public Sub New(ByVal _ClassID As String, ByVal _InterfaceID As String, ByVal _EventID As String)
            m_classID = _ClassID
            m_interfaceID = _InterfaceID
            m_eventID = _EventID
        End Sub

        ' Properties
        Public ReadOnly Property EventID() As String
            Get
                Return m_eventID
            End Get
        End Property

        Public Property InterfaceShadows() As Boolean
            Get
                Return m_interfaceShadows
            End Get
            Set(ByVal Value As Boolean)
                m_interfaceShadows = Value
            End Set
        End Property

        Public ReadOnly Property ClassID() As String
            Get
                Return m_classID
            End Get
        End Property

        Public ReadOnly Property InterfaceID() As String
            Get
                Return m_interfaceID
            End Get
        End Property
    End Class
End Namespace
