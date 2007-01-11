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

    Sub New(ByVal Parent As IBaseObject, ByVal Reader As ITokenReader)
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
            Constant = New ConditionalConstant(def.Symbol, def.Value)
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
        name = current.AsIdentifier.Identifier

        If Not vbnc.tm.Accept(m_Reader, KS.Equals, True) Then
            Return
        End If

        m_Evaluator.Parse(value)

        m_CurrentConstants.Add(New ConditionalConstant(name, value))

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
        If m_ConditionStack(m_ConditionStack.Count - 1) = 0 Then
            m_ConditionStack(m_ConditionStack.Count - 1) = 1
        ElseIf m_ConditionStack(m_ConditionStack.Count - 1) = 1 Then
            m_ConditionStack(m_ConditionStack.Count - 1) = -1
        End If
        ParseEndOfLine()
    End Sub

    Private Sub ParseEndIf()
        m_ConditionStack.RemoveAt(m_ConditionStack.Count - 1)
        ParseEndOfLine()
    End Sub
#End Region

#Region "Region"
    Private Sub ParseRegion()
        Dim current As Token

        current = m_Reader.Next
        If Not current.IsStringLiteral Then
            Helper.AddError("Expected string literal")
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
            Helper.AddError("Expected end of line")
            vbnc.tm.GotoNewline(m_Reader, True)
        End If
    End Sub

    <Obsolete()> Friend Overrides ReadOnly Property tm() As tm
        Get
            Return MyBase.tm 'Throw New InternalException("Don't use tm.")
        End Get
    End Property

#If DEBUG Then
    Public Sub Dump(ByVal Dumper As IndentedTextWriter)
        Dumper.WriteLine("Dump of conditional compiler: ")
        Dumper.Indent()
        m_ProjectConstants.Dump(Dumper)
        m_CurrentConstants.Dump(Dumper)
        Dumper.Unindent()
        Dumper.WriteLine("End of dump of conditional compiler.")
    End Sub
#End If

    Public Function [Next]() As Token Implements ITokenReader.Next
        Dim result As Token

        If m_Peeked IsNot Nothing Then
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

            If result.IsKeyword Then
                Select Case result.AsKeyword.Keyword
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
                        Helper.AddError("'End' what?")
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
        If m_Peeked IsNot Nothing Then Return m_Peeked
        m_Peeked = [Next]()
        Return m_Peeked
    End Function

    Public Function Current() As Token Implements ITokenReader.Current
        Return m_Current
    End Function
End Class

