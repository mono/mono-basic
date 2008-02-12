' 
' Visual Basic.Net Compiler
' Copyright (C) 2004 - 2007 Rolf Bjarne Kvinge, RKvinge@novell.com
' 
' This library is free software; you can redistribute it and/or
' modify it under the terms of the GNU Lesser General Public
' License as published by the Free Software Foundation; either
' version 2.1 of the License, or (at your option) any later version.
' 
' This library is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
' Lesser General Public License for more details.
' 
' You should have received a copy of the GNU Lesser General Public
' License along with this library; if not, write to the Free Software
' Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
' 

''' <summary>
''' This exception is thrown when an unexpected condition occurs in the compiler.
''' </summary>
Public Class InternalException
    Inherits vbncException

    Private Shared recursive As Boolean
    Private m_Message As String

    Public Overrides ReadOnly Property Message() As String
        Get
            Return m_Message
        End Get
    End Property

    Sub New()
        m_Message = "There has been an internal error in the compiler."
        StopOnInternalException()
    End Sub

    <Diagnostics.DebuggerHidden()> _
    Sub New(ByVal Location As Span)
        MyBase.new()
        'If Location IsNot Nothing Then
        m_Message = "There has been an internal error in the compiler caused by the line: " & Location.AsString
        'Else
        'm_Message = "There has been an internal error in the compiler."
        'End If
        StopOnInternalException()
    End Sub

    <Diagnostics.DebuggerHidden()> _
    Sub New(ByVal Obj As IBaseObject)
        MyBase.new()
        If Obj IsNot Nothing Then
            m_Message = "There has been an internal error in the compiler caused by the line: " & Obj.Location.AsString
        Else
            m_Message = "There has been an internal error in the compiler."
        End If
        StopOnInternalException()
    End Sub

    <Diagnostics.DebuggerHidden()> _
    Sub New(ByVal Obj As ExpressionClassification)
        MyBase.new()
        If Obj IsNot Nothing Then
            m_Message = "There has been an internal error in the compiler caused by the line: " & Obj.Parent.Location.ToString(Obj.Parent.Compiler)
        Else
            m_Message = "There has been an internal error in the compiler."
        End If
        StopOnInternalException()
    End Sub

    <Diagnostics.DebuggerHidden()> _
    Sub New(ByVal Obj As MemberInfo)
        MyBase.new()
        If False Then 'Obj IsNot Nothing AndAlso Obj.Location IsNot Nothing Then
            'm_Message = "There has been an internal error in the compiler caused by the line: " & Obj.Location.AsString
        Else
            m_Message = "There has been an internal error in the compiler."
        End If
        StopOnInternalException()
    End Sub
    <Diagnostics.DebuggerHidden()> _
    Sub New(ByVal InnerException As Exception)
        MyBase.New("", InnerException)
        m_Message = "There has been an internal error in the compiler: " & InnerException.Message
StopOnInternalException()
    End Sub

    <Diagnostics.DebuggerHidden()> _
    Sub New(ByVal Obj As BaseObject, ByVal strMsg As String)
        MyBase.new()
        m_Message = "There has been an internal error in the compiler: '" & strMsg & "'"
        If Obj IsNot Nothing Then
            m_Message &= " caused by the line: " & Obj.Location.AsString
        End If
StopOnInternalException()
    End Sub

    <Diagnostics.DebuggerHidden()> _
    Sub New(ByVal strMsg As String)
        MyBase.new()
        m_Message = "There has been an internal error in the compiler: " & strMsg
        StopOnInternalException()
    End Sub

    <Diagnostics.DebuggerHidden()> _
    Sub StopOnInternalException()
#If DEBUG Then
        If recursive Then Return
        recursive = True
        Helper.StopIfDebugging(True)
        recursive = False
#End If
    End Sub

#If DEBUG Then
    Shared Sub Dump(ByVal xml As Xml.XmlWriter, ByVal ex As Exception, Optional ByVal ElementWritten As Boolean = False)
        If ElementWritten = False Then xml.WriteStartElement("Exception")
        xml.WriteAttributeString("Type", ex.GetType.ToString)
        xml.WriteAttributeString("HelpLink", ex.HelpLink)
        xml.WriteAttributeString("Source", ex.Source)
        If ex.TargetSite IsNot Nothing Then xml.WriteAttributeString("TargetSite", ex.TargetSite.ToString)

        xml.WriteStartElement("Message")
        xml.WriteString(ex.Message)
        xml.WriteEndElement()

        xml.WriteStartElement("StackTrace")
        xml.WriteString(ex.StackTrace)
        xml.WriteEndElement()

        If ex.InnerException IsNot Nothing Then
            xml.WriteStartElement("InnerException")
            Dump(xml, ex.InnerException, True)
            xml.WriteEndElement()
        End If
        If ElementWritten = False Then xml.WriteEndElement()
    End Sub
#End If
End Class
