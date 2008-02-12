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

Option Compare Text

Public Class ConditionalCompiler
    Inherits BaseObject
    Implements ITokenReader

    Private m_ProjectConstants As New ConditionalConstants
    Private m_CurrentConstants As ConditionalConstants
    Private m_Evaluator As New ConditionalExpression(Me)

    Private m_Reader As ITokenReader
    Private m_Peeked As Token
    Private m_Current As Token

    ''' <summary>
    ''' 0 if condition is false and has never been true
    ''' 1 if condition is true
    ''' -1 if condition has been true
    ''' </summary>
    ''' <remarks></remarks>
    Private m_ConditionStack As New Generic.List(Of Integer)

    Private m_Methods As New Generic.Dictionary(Of MethodInfo, Object())

    Function IsConditionallyExcluded(ByVal CalledMethod As MethodInfo, ByVal AtLocation As Span) As Boolean
        Dim attribs() As Object

        If m_Methods.ContainsKey(CalledMethod) Then
            attribs = m_Methods(CalledMethod)
        Else
            attribs = CalledMethod.GetCustomAttributes(Compiler.TypeCache.System_Diagnostics_ConditionalAttribute, False)
            m_Methods.Add(CalledMethod, attribs)
        End If

        If attribs Is Nothing Then Return False

        For Each attrib As Object In attribs
            Dim conditionalAttrib As System.Diagnostics.ConditionalAttribute

            conditionalAttrib = TryCast(attrib, System.Diagnostics.ConditionalAttribute)
            If conditionalAttrib Is Nothing Then Continue For

            If Not IsDefinedAtLocation(conditionalAttrib.ConditionString, AtLocation) Then Return True
        Next

        Return False
    End Function

    ''' <summary>
    ''' Checks if the specified symbol is defined at the specified location.
    ''' </summary>
    ''' <param name="Symbol"></param>
    ''' <param name="Location"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function IsDefinedAtLocation(ByVal Symbol As String, ByVal Location As Span) As Boolean
        Dim constants As ConditionalConstants

        constants = Location.File(Compiler).GetConditionalConstants(Location.Line)

        If constants IsNot Nothing AndAlso constants.ContainsKey(Symbol) Then
            Return constants(Symbol).IsDefined
        End If

        If m_ProjectConstants.ContainsKey(Symbol) Then
            Return m_ProjectConstants(Symbol).IsDefined
        End If

        Return False
    End Function

    ReadOnly Property Reader() As ITokenReader
        Get
            Return m_Reader
        End Get
    End Property

    ReadOnly Property IfdOut() As Boolean
        Get
            For i As Integer = 0 To m_ConditionStack.Count - 1
                If Not m_ConditionStack(i) > 0 Then Return True
            Next
            Return False
        End Get
    End Property

    Sub New(ByVal Parent As BaseObject, ByVal Reader As ITokenReader)
        MyBase.New(Parent)
        m_Reader = Reader
        LoadProjectConstants()
    End Sub

    ReadOnly Property CurrentConstants() As ConditionalConstants
        Get
            Return m_CurrentConstants
        End Get
    End Property

    Private Sub LoadProjectConstants()
        'Set the project level defines
        Dim Constant As ConditionalConstant
        For Each def As Define In Compiler.CommandLine.Define
            Constant = New ConditionalConstant(def.Symbol, def.ObjectValue)
            m_ProjectConstants.Add(Constant)
        Next

        ResetCurrentConstants()
    End Sub

    Private Sub ResetCurrentConstants()
        m_CurrentConstants = New ConditionalConstants(m_ProjectConstants)
    End Sub

#Region "Const"
    Private Sub ParseConst()
        Dim current As Token
        Dim name As String
        Dim value As Object = Nothing

        current = m_Reader.Next
        If Not current.IsIdentifier Then
            Compiler.Report.ShowMessage(Messages.VBNC30203)
            vbnc.tm.GotoNewline(m_Reader, True)
            Return
        End If
        name = current.Identifier

        If Not vbnc.tm.Accept(m_Reader, KS.Equals, True) Then
            Return
        End If

        m_Evaluator.Parse(value)

        If Not Me.IfdOut Then
            m_CurrentConstants.Add(New ConditionalConstant(name, value))
            current.Location.File(Compiler).AddConditionalConstants(current.Location.Line, m_CurrentConstants)
        End If

        ParseEndOfLine()
    End Sub
#End Region

#Region "If"
    Private Sub ParseIf()
        Dim theExpression As New ConditionalExpression(Me)
        Dim expression As Object = Nothing

        If Not theExpression.Parse(expression) Then
            vbnc.tm.GotoNewline(m_Reader, True)
            Return
        End If

        If m_Reader.Peek.Equals(KS.Then) Then m_Reader.Next()

        ParseEndOfLine()

        If CBool(expression) Then
            m_ConditionStack.Add(1)
        Else
            m_ConditionStack.Add(0)
        End If
    End Sub

    Private Sub ParseElseIf()
        If Not CheckEmtpyStack(Messages.VBNC30014) Then Return

        Dim theExpression As New ConditionalExpression(Me)
        Dim expression As Object = Nothing

        If Not theExpression.Parse(expression) Then
            vbnc.tm.GotoNewline(m_Reader, True)
            Return
        End If

        If m_Reader.Peek.Equals(KS.Then) Then m_Reader.Next()

        ParseEndOfLine()

        If m_ConditionStack(m_ConditionStack.Count - 1) = 1 Then
            m_ConditionStack(m_ConditionStack.Count - 1) = -1
        ElseIf m_ConditionStack(m_ConditionStack.Count - 1) = 0 AndAlso CBool(expression) Then
            m_ConditionStack(m_ConditionStack.Count - 1) = 1
        End If
    End Sub

    Private Sub ParseElse()
        If Not CheckEmtpyStack() Then Return

        If m_ConditionStack(m_ConditionStack.Count - 1) = 0 Then
            m_ConditionStack(m_ConditionStack.Count - 1) = 1
        ElseIf m_ConditionStack(m_ConditionStack.Count - 1) = 1 Then
            m_ConditionStack(m_ConditionStack.Count - 1) = -1
        End If
        ParseEndOfLine()
    End Sub

    Private Sub ParseEndIf()
        If Not CheckEmtpyStack() Then Return

        m_ConditionStack.RemoveAt(m_ConditionStack.Count - 1)
        ParseEndOfLine()
    End Sub

    Private Function CheckEmtpyStack(Optional ByVal Msg As Messages = Messages.VBNC30013) As Boolean
        If m_ConditionStack.Count > 0 Then Return True

        Compiler.Report.ShowMessage(Msg, m_Reader.Current.Location)
        vbnc.tm.GotoNewline(m_Reader, True)

        Return False
    End Function
#End Region

#Region "Region"
    Private Sub ParseRegion()
        Dim current As Token

        current = m_Reader.Next
        If Not current.IsStringLiteral Then
            Helper.AddError(Me, "Expected string literal")
            vbnc.tm.GotoNewline(m_Reader, True)
            Return
        End If

        ParseEndOfLine()
    End Sub

    Private Sub ParseEndRegion()
        ParseEndOfLine()
    End Sub
#End Region

#Region "External Source"
    Private Sub ParseExternalSource()
        If Not vbnc.tm.Accept(m_Reader, KS.LParenthesis, True) Then
            Return
        End If

        If Not vbnc.tm.AcceptStringLiteral(m_Reader, True) Then
            Return
        End If

        If Not vbnc.tm.Accept(m_Reader, KS.Comma, True) Then
            Return
        End If

        If Not vbnc.tm.AcceptIntegerLiteral(m_Reader, True) Then
            Return
        End If

        If Not vbnc.tm.Accept(m_Reader, KS.RParenthesis, True) Then
            Return
        End If

        ParseEndOfLine()
    End Sub

    Private Sub ParseEndExternalSource()
        ParseEndOfLine()
    End Sub
#End Region

    Private Sub ParseEndOfLine()
        Dim current As Token
        current = m_Reader.Next
        If Not current.IsEndOfLine() Then
            Helper.AddError(Me, "Expected end of line")
            vbnc.tm.GotoNewline(m_Reader, True)
        End If
    End Sub

    <Obsolete()> Friend Overrides ReadOnly Property tm() As tm
        Get
            Return MyBase.tm 'Throw New InternalException("Don't use tm.")
        End Get
    End Property

    Public Function [Next]() As Token Implements ITokenReader.Next
        Dim result As Token

        If Token.IsSomething(m_Peeked) Then
            m_Current = m_Peeked
            m_Peeked = Nothing
            Return m_Current
        End If

        Do
            result = m_Reader.Next
            If result.IsEndOfCode Then
                m_Current = result
                Return result
            End If

            If result.IsEndOfFile Then
                ResetCurrentConstants()
                m_Current = result
                Return result
            End If

            '#If DEBUG Then
            '            If result IsNot Nothing AndAlso result.Location.Column <= 40 AndAlso IfdOut AndAlso Helper.ShowDebugFor("CONDITIONALCOMPILER") Then
            '                Compiler.Report.WriteLine("EXCLUDED: " & result.Location.ToString(Compiler))
            '            End If
            '#End If

            If result.IsKeyword Then
                Select Case result.Keyword
                    Case KS.ConditionalIf
                        ParseIf()
                    Case KS.ConditionalElse
                        ParseElse()
                    Case KS.ConditionalElseIf
                        ParseElseIf()
                    Case KS.ConditionalEndIf
                        ParseEndIf()
                    Case KS.ConditionalConst
                        ParseConst()
                    Case KS.ConditionalExternalSource
                        ParseExternalSource()
                    Case KS.ConditionalEndExternalSource
                        ParseEndExternalSource()
                    Case KS.ConditionalRegion
                        ParseRegion()
                    Case KS.ConditionalEndRegion
                        ParseEndRegion()
                    Case KS.ConditionalEnd
                        Helper.AddError(Me, "'End' what?")
                        Continue Do
                    Case Else
                        If IfdOut Then
                            Continue Do
                        Else
                            m_Current = result
                            Return result
                        End If
                End Select
            ElseIf IfdOut Then
                Continue Do
            Else
                m_Current = result
                Return result
            End If
        Loop While True
        Return Nothing
    End Function

    Public Function Peek() As Token Implements ITokenReader.Peek
        If Token.IsSomething(m_Peeked) Then Return m_Peeked
        m_Peeked = [Next]()
        Return m_Peeked
    End Function

    Public Function Current() As Token Implements ITokenReader.Current
        Return m_Current
    End Function

    Public Function CurrentTypeCharacter() As TypeCharacters.Characters Implements ITokenReader.CurrentTypeCharacter
        Return m_Reader.CurrentTypeCharacter
    End Function
End Class

