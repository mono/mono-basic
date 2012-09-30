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

Imports System.Resources
#If DEBUG Then
#Const STOPONERROR = True
#Const STOPONWARNING = False
#End If

''' <summary>
''' The report class for the compiler. Is used to show all the
''' messages from the compiler.
''' </summary>
Public Class Report
    ''' <summary>
    ''' The count of each message shown (by message level).
    ''' </summary>
    ''' <remarks>Depends on the fact that the first message level is 0 </remarks>
    Private m_MessageCount(MessageLevel.Error) As Integer

    ''' <summary>
    ''' The max number of errors before quit compiling.
    ''' </summary>
    ''' <remarks></remarks>
    Const MAXERRORS As Integer = 100

    ''' <summary>
    ''' The resource manager for this report.
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared m_Resources As ResourceManager

    ''' <summary>
    ''' A list of all the errors / warnings shown.
    ''' Messages are not added until they are shown
    ''' (if they are saved).
    ''' </summary>
    ''' <remarks></remarks>
    Private m_Messages As New ArrayList

    ''' <summary>
    ''' A list of all the saved errors / warnings to show.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_SavedMessages As New ArrayList

    ''' <summary>
    ''' The executing compiler.
    ''' </summary>
    Private m_Compiler As Compiler


    Enum ReportLevels
        ''' <summary>
        ''' Always show the message.
        ''' </summary>
        ''' <remarks></remarks>
        Always
        ''' <summary>
        ''' Only show if verbose
        ''' </summary>
        ''' <remarks></remarks>
        Verbose
        ''' <summary>
        ''' Only show in debug builds
        ''' </summary>
        ''' <remarks></remarks>
        Debug
    End Enum

    Private m_ReportLevel As ReportLevels = ReportLevels.Debug
    Private m_Listeners As New Generic.List(Of Diagnostics.TraceListener)
    Private m_WarningAsErrorShown As Boolean

    ''' <summary>
    ''' The listeners who will receive text output.
    ''' A console writer is here by default.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Listeners() As Generic.List(Of Diagnostics.TraceListener)
        Get
            Return m_Listeners
        End Get
    End Property

    Sub Write(ByVal Level As ReportLevels, Optional ByVal Value As String = "")
        If Level <= m_ReportLevel Then
            Write(Value)
        End If
    End Sub

    Sub Write(Optional ByVal Value As String = "")
        For Each d As Diagnostics.TraceListener In m_Listeners
            d.Write(Value)
        Next
        Console.Write(Value)
    End Sub

    Sub Indent()
        For Each d As Diagnostics.TraceListener In m_Listeners
            d.IndentLevel += 1
        Next
    End Sub

    Sub Unindent()
        For Each d As Diagnostics.TraceListener In m_Listeners
            d.IndentLevel -= 1
        Next
    End Sub

    Sub WriteLine(ByVal Level As ReportLevels, Optional ByVal Value As String = "")
        Write(Level, Value & VB.vbNewLine)
    End Sub

    Sub WriteLine(Optional ByVal Value As String = "")
        Write(Value & VB.vbNewLine)
    End Sub

    ''' <summary>
    ''' The executing compiler.
    ''' </summary>
    ReadOnly Property Compiler() As Compiler
        Get
            Return m_Compiler
        End Get
    End Property

    ''' <summary>
    ''' Creates a new default report.
    ''' </summary>
    ''' <remarks></remarks>
    Sub New(ByVal Compiler As Compiler)
        m_Compiler = Compiler
        'm_Listeners.Add(New System.Diagnostics.TextWriterTraceListener(Console.Out))
#If DEBUG Then
        For Each i As Diagnostics.TraceListener In System.Diagnostics.Debug.Listeners
            m_Listeners.Add(i)
        Next
#End If
    End Sub

    ''' <summary>
    ''' Looks up the specified error code and returns the string
    ''' </summary>
    ''' <param name="ErrorCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function LookupErrorCode(ByVal ErrorCode As Integer) As String
        Dim result As Object

        If m_Resources Is Nothing Then
            m_Resources = New ResourceManager("vbnc.Errors", System.Reflection.Assembly.GetExecutingAssembly())
        End If

        result = m_Resources.GetObject(ErrorCode.ToString)

        If result Is Nothing Then
            Console.WriteLine("Could not find the error message corresponding with the error code: " & ErrorCode.ToString)
            Return ErrorCode.ToString
        Else
            Return result.ToString
        End If

    End Function

    ''' <summary>
    ''' The number of messages shown for the specified message level
    ''' </summary>
    ReadOnly Property MessageCount(ByVal Level As MessageLevel) As Integer
        Get
            Return m_MessageCount(Level)
        End Get
    End Property

    ''' <summary>
    ''' The number of errors so far.
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    ReadOnly Property Errors() As Integer
        Get
            Return m_MessageCount(MessageLevel.Error)
        End Get
    End Property

    ''' <summary>
    ''' The number of warnings so far.
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    ReadOnly Property Warnings() As Integer
        Get
            Return m_MessageCount(MessageLevel.Warning)
        End Get
    End Property

    ''' <summary>
    ''' Show the saved messages. Returns true if any error messages have been shown.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ShowSavedMessages() As Boolean
        Dim result As Boolean = False

        For Each msg As Message In m_SavedMessages
            ShowMessage(False, msg) 'Compiler.Report.WriteLine(str)
            If msg.GetLevel() >= MessageLevel.Error Then result = True
        Next
        m_SavedMessages.Clear()
        Return result
    End Function

    ''' <summary>
    ''' Helper to construct a message for a multiline message when every line after the first one
    ''' is the same message. Message() must be an array with two elements, FirstParameters() is applied
    ''' to the first one, then the second element is multiplied by the number of SubsequentParameters()
    ''' and then a message is created with the corresponding SubsequentParameters() for every line after the 
    ''' first one.
    ''' </summary>
    <Diagnostics.DebuggerHidden()> _
    Sub ShowMessageHelper(ByVal Message() As Messages, ByVal Location As Span, ByVal FirstParameters() As String, ByVal SubsequentParameters() As String)
        Dim msg() As Messages
        Dim params()() As String

        ReDim msg(SubsequentParameters.Length)
        ReDim params(SubsequentParameters.Length)
        msg(0) = Message(0)
        params(0) = FirstParameters
        For i As Integer = 1 To msg.GetUpperBound(0)
            msg(i) = Message(1)
            params(i) = New String() {SubsequentParameters(i - 1)}
        Next

        ShowMessage(msg, Location, params)
    End Sub

    ''' <summary>
    ''' Shows the message with the specified location and parameters
    ''' </summary>
    <Diagnostics.DebuggerHidden()> _
    Function ShowMessage(ByVal Message As Messages, ByVal Location As Span, ByVal ParamArray Parameters() As String) As Boolean
        Return ShowMessage(False, New Message(Compiler, Message, Parameters, Location))
    End Function

    ''' <summary>
    ''' Shows the message with the specified location and parameters
    ''' </summary>
    <Diagnostics.DebuggerHidden()> _
    Function ShowMessageNoLocation(ByVal Message As Messages, ByVal ParamArray Parameters() As String) As Boolean
        Return ShowMessage(False, New Message(Compiler, Message, Parameters))
    End Function

    ''' <summary>
    ''' Shows the multiline message with the specified location and parameters.
    ''' </summary>
    <Diagnostics.DebuggerHidden()> _
    Function ShowMessage(ByVal Message() As Messages, ByVal Location As Span, ByVal ParamArray Parameters()() As String) As Boolean
        Return ShowMessage(False, New Message(Compiler, Message, Parameters, Location))
    End Function

    ''' <summary>
    ''' Saves the message with the specified location and parameters.
    ''' </summary>
    <Diagnostics.DebuggerHidden()> _
    Function SaveMessage(ByVal Message As Messages, ByVal Location As Span, ByVal ParamArray Parameters() As String) As Boolean
        Return ShowMessage(True, New Message(Compiler, Message, Parameters, Location))
    End Function

    ''' <summary>
    ''' Shows the specified message. Can optionally save it (not show it)
    ''' to show it later with ShowSavedMessages()
    ''' </summary>
    <Diagnostics.DebuggerHidden()> _
    Private Function ShowMessage(ByVal SaveIt As Boolean, ByVal Message As Message) As Boolean
        Dim isOnlyWarning As Boolean = False

        If Message.IsSuppressedWarning Then Return True

        isOnlyWarning = Message.GetLevel() <= MessageLevel.Warning

        If SaveIt Then
            m_SavedMessages.Add(Message)
        Else
            m_Messages.Add(Message)
            Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Always, Message.ToString())
            m_MessageCount(Message.GetLevel()) += 1

            If m_WarningAsErrorShown = False AndAlso Message.IsWarningAsError() Then
                m_WarningAsErrorShown = True
                ShowMessage(Messages.VBNC31072, Message.Location, Message.GetText(False))
            End If

            If m_MessageCount(MessageLevel.Error) > MAXERRORS Then
                Throw New TooManyErrorsException()
            End If
        End If

#If TRACEMESSAGES Then
        Console.WriteLine(Environment.StackTrace)
#End If

#If STOPONERROR Then
        If Helper.IsDebugging AndAlso Message.GetLevel() = MessageLevel.Error Then
            Helper.Stop()
        ElseIf Helper.IsBootstrapping Then
            Throw New InternalException(Message.ToString)
        End If
#ElseIf STOPONWARNING Then
        If Debugger.IsAttached AndAlso Message.Level = MessageLevel.Warning Then
            Helper.Stop()
        End If
#End If

        Return isOnlyWarning
    End Function

    Public Sub Trace(ByVal format As String, ByVal ParamArray args() As Object)
        If Compiler.CommandLine.Trace = False Then Return
        If args IsNot Nothing Then
            For i As Integer = 0 To args.Length - 1
                If TypeOf args(i) Is Span Then
                    args(i) = DirectCast(args(i), Span).ToString(Compiler)
                End If
            Next
        End If
        Console.WriteLine(format, args)
    End Sub
End Class
