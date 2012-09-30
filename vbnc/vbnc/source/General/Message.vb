' 
' Visual Basic.Net Compiler
' Copyright (C) 2004 - 2010 Rolf Bjarne Kvinge, RKvinge@novell.com
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
''' Represents a compiler message (might be a warning, an error or an information).
''' A compiler message might have several lines.
''' </summary>
Public Class Message
    Private m_Compiler As Compiler

    ''' <summary>
    ''' The actual message itself.
    ''' Might be several messages if this is a multiline message.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_Message As Messages()

    ''' <summary>
    ''' The location of the code corresponding to the the message.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_Location As Nullable(Of Span)

    ''' <summary>
    ''' The parameters of the message(s)
    ''' </summary>
    Private m_Parameters()() As String

    ''' <summary>
    ''' The format of the message
    ''' </summary>
    ''' <remarks></remarks>
    Private Const MESSAGEFORMATWITHLOCATION As String = "%LOCATION% : %MESSAGELEVEL% %MESSAGE%"

    ''' <summary>
    ''' Format of messages without a location.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const MESSAGEFORMATNOLOCATION As String = "vbnc : %MESSAGELEVEL% %MESSAGE%"

    ''' <summary>
    ''' The format of the message
    ''' </summary>
    ''' <remarks></remarks>
    Private Const MESSAGEFORMAT As String = "vbnc : %MESSAGELEVEL% : %MESSAGE%"

    ''' <summary>
    ''' Get the severity level of this message.
    ''' </summary>
    Function GetLevel() As MessageLevel
        If IsWarningAsError() Then Return MessageLevel.Error
        Return Level
    End Function

    ReadOnly Property Level As MessageLevel
        Get
            Return DirectCast(System.Attribute.GetCustomAttribute(GetType(Messages).GetField(m_Message(0).ToString), GetType(MessageAttribute)), MessageAttribute).Level
        End Get
    End Property

    Function IsWarningAsError() As Boolean
        If Level <> MessageLevel.Warning Then Return False

        If Compiler.CommandLine.WarnAsError.HasValue AndAlso Compiler.CommandLine.WarnAsError.Value Then
            Return True
        ElseIf Compiler.CommandLine.WarningsAsError IsNot Nothing AndAlso Compiler.CommandLine.WarningsAsError.Contains(CInt(m_Message(0))) Then
            Return True
        Else
            Return False
        End If
    End Function

    Function IsSuppressedWarning() As Boolean
        If Level <> MessageLevel.Warning Then Return False

        If IsWarningAsError() Then Return False

        If Compiler.CommandLine.NoWarn Then Return True

        If Compiler.CommandLine.NoWarnings Is Nothing Then Return False

        Return Compiler.CommandLine.NoWarnings.Contains((CInt(m_Message(0))))
    End Function

    ''' <summary>
    ''' The actual message itself.
    ''' Might be several messages if this is a multiline message.
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property Message() As Messages()
        Get
            Return m_Message
        End Get
    End Property

    ''' <summary>
    ''' The location of the code corresponding to the the message.
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property Location() As Span
        Get
            Return m_Location.GetValueOrDefault()
        End Get
    End Property

    ''' <summary>
    ''' The parameters of the message(s)
    ''' </summary>
    ReadOnly Property Parameters() As String()()
        Get
            Return m_Parameters
        End Get
    End Property

    ''' <summary>
    ''' Create a new message with the specified data.
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Sub New(ByVal Compiler As Compiler, ByVal Message As Messages, ByVal Location As Span)
        Me.m_Compiler = Compiler
        Me.m_Message = New Messages() {Message}
        Me.m_Location = Location
        Me.m_Parameters = New String()() {}
    End Sub

    ''' <summary>
    ''' Create a new message with the specified data.
    ''' </summary>
    Sub New(ByVal Compiler As Compiler, ByVal Message As Messages(), ByVal Parameters()() As String, ByVal Location As Span)
        Me.m_Compiler = Compiler
        Me.m_Message = Message
        Me.m_Location = Location
        Me.m_Parameters = Parameters
    End Sub

    ''' <summary>
    ''' Create a new message with the specified data.
    ''' </summary>
    Sub New(ByVal Compiler As Compiler, ByVal Message As Messages, ByVal Parameters() As String, ByVal Location As Span)
        Me.m_Compiler = Compiler
        Me.m_Message = New Messages() {Message}
        Me.m_Location = Location
        If Parameters Is Nothing Then
            Me.m_Parameters = New String()() {New String() {}}
        Else
            Me.m_Parameters = New String()() {Parameters}
        End If
    End Sub

    ''' <summary>
    ''' Create a new message with the specified data.
    ''' </summary>
    Sub New(ByVal Compiler As Compiler, ByVal Message As Messages, ByVal Parameters() As String)
        Me.m_Compiler = Compiler
        Me.m_Message = New Messages() {Message}
        Me.m_Location = Nothing
        If Parameters Is Nothing Then
            Me.m_Parameters = New String()() {New String() {}}
        Else
            Me.m_Parameters = New String()() {Parameters}
        End If
    End Sub

    ReadOnly Property Compiler() As Compiler
        Get
            Return m_Compiler
        End Get
    End Property

    Function GetText(IncludeNumber As Boolean) As String
        Dim strMessages() As String

        Helper.Assert(m_Message IsNot Nothing, "m_Message Is Nothing")
        Helper.Assert(m_Parameters IsNot Nothing, "m_Parameters Is Nothing")

        'Get the message string and format it with the message parameters.

        ReDim strMessages(m_Message.GetUpperBound(0))
        For i As Integer = 0 To m_Message.GetUpperBound(0)
            strMessages(i) = Report.LookupErrorCode(m_Message(i))
            Helper.Assert(m_Parameters(i) IsNot Nothing, "m_Parameters(" & i.ToString & ") Is Nothing")
            If m_Parameters IsNot Nothing AndAlso m_Parameters.Length > i Then
                strMessages(i) = String.Format(strMessages(i), m_Parameters(i))
            End If
            If IncludeNumber AndAlso i = 0 Then strMessages(i) = m_Message(i).ToString & ": " & strMessages(i)
        Next
        Return Microsoft.VisualBasic.Join(strMessages, Microsoft.VisualBasic.vbNewLine)
    End Function

    ''' <summary>
    ''' Formats the message to a string.
    ''' The returned string might have several lines.
    ''' </summary>
    Overrides Function ToString() As String
        Dim strMessage, strLocation As String
        Dim result As String

        strMessage = GetText(True)

        'Get the location string
        If Location.HasFile Then
            strLocation = Location.ToString(Compiler)
            result = MESSAGEFORMATWITHLOCATION
        ElseIf m_Location.HasValue Then
            strLocation = "vbnc : Command line"
            result = MESSAGEFORMATWITHLOCATION
        Else
            strLocation = String.Empty
            result = MESSAGEFORMATNOLOCATION
        End If

        'Format the entire message.
        result = result.Replace("%LOCATION%", strLocation)
        result = result.Replace("%MESSAGE%", strMessage)
        result = result.Replace("%MESSAGELEVEL%", GetLevel().ToString.ToLower())

        Return result
    End Function
End Class