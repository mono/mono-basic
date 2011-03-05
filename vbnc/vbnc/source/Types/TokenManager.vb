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
''' Token stream handler
''' </summary>
''' <remarks></remarks>
Public Class tm
    Inherits Helper

    Private m_Reader As Scanner
    Private m_TokenList As New Generic.List(Of Token)
    Private m_RestorePoints As Integer
    Private m_CurrentIndex As Integer
    Private m_Current As Token
    Private m_Previous As Token

    Private Property Current() As Token
        Get
            Return m_Current
        End Get
        Set(ByVal value As Token)
            m_Previous = m_Current
            m_Current = value

            'Console.WriteLine("Setting currenttoken to: " & m_Current.FriendlyString)
        End Set
    End Property

    ReadOnly Property CurrentTypeCharacter() As TypeCharacters.Characters
        Get
            Return m_Reader.currenttypecharacter
        End Get
    End Property

    ReadOnly Property Reader() As Scanner
        Get
            Return m_Reader
        End Get
    End Property

    Function CurrentToken() As Token
        Return Current
    End Function

    ReadOnly Property CurrentLocation() As Span
        Get
            Return m_Reader.GetCurrentLocation
        End Get
    End Property

    Function PeekToken(Optional ByVal Jump As Integer = 1) As Token
        Helper.Assert(Jump >= -1)
        If Jump = -1 Then Return m_Previous
        If Jump = 0 Then Return CurrentToken()
        If m_TokenList.Count = 0 Then
            m_TokenList.Add(CurrentToken)
        End If
        For i As Integer = m_TokenList.Count To m_CurrentIndex + Jump
            m_TokenList.Add(m_Reader.Next())
        Next
        Return m_TokenList(m_CurrentIndex + Jump)
    End Function

    Sub NextToken()
        If m_RestorePoints > 0 Then
            m_CurrentIndex += 1
            Helper.Assert(m_CurrentIndex <= m_TokenList.Count)
            If m_CurrentIndex = m_TokenList.Count Then
                Current = m_Reader.Next
                m_TokenList.Add(Current)
            Else
                Current = m_TokenList(m_CurrentIndex)
            End If
        ElseIf m_TokenList.Count > 0 Then
            'Peeked items.
            m_CurrentIndex += 1
            Helper.Assert(m_CurrentIndex <= m_TokenList.Count)
            If m_CurrentIndex = m_TokenList.Count Then
#If DEBUG Then
                'Console.WriteLine("Reached a total of " & m_TokenList.Count.ToString & " tokens in restorable list")
#End If
                Current = m_Reader.Next
                m_TokenList.Clear()
                m_CurrentIndex = 0
            Else
                Current = m_TokenList(m_CurrentIndex)
            End If
        Else
            Current = m_Reader.Next()
        End If
    End Sub

    Sub NextToken(ByVal Jump As Integer)
        Helper.Assert(Jump >= 0)
        For i As Integer = 1 To Jump
            NextToken()
        Next
    End Sub

    Function GetRestorablePoint() As RestorablePoint
        m_RestorePoints += 1
        If m_TokenList.Count = 0 Then
            m_TokenList.Add(CurrentToken)
        End If
        'Console.WriteLine(" Creating restore point: " & CurrentToken.FriendlyString)
        Return New RestorablePoint(m_CurrentIndex)
    End Function

    Sub RestoreToPoint(ByVal Point As RestorablePoint)
        m_CurrentIndex = Point.Index
        Current = m_TokenList(m_CurrentIndex)
        'Console.WriteLine(" Restored to: " & CurrentToken.FriendlyString)
        IgnoreRestoredPoint()
    End Sub

    Sub IgnoreRestoredPoint()
        m_RestorePoints -= 1
    End Sub

#If DEBUG Then
    ReadOnly Property TokenSequence() As String
        Get
            Const Range As Integer = 3
            Dim result As String = ""
            For i As Integer = -Range To Range
                'If Me.IsTokenValid(Me.iCurrentToken + i) Then
                'result &= "(" & i.ToString & "): '" & Me.PeekToken(i).ToString & "' "
                result &= Me.PeekToken(i).ToString & " "
                'End If
            Next
            Return result
        End Get
    End Property
#End If

    Function AcceptSequence(ByVal ParamArray ks As KS()) As Boolean
        For i As Integer = 0 To ks.GetUpperBound(0)
            If Me.PeekToken(i).Equals(ks(i)) = False Then Return False
        Next
        Me.NextToken(ks.GetUpperBound(0) + 1)
        Return True
    End Function

    Sub New(ByVal Compiler As Compiler, ByVal Reader As Scanner)
        MyBase.New(Compiler)
        m_Reader = Reader
    End Sub

    ReadOnly Property IsCurrentTokenValid() As Boolean
        Get
            Return Token.IsSomething(Current)
        End Get
    End Property

    ''' <summary>
    ''' Skips tokens until a newline is found.
    ''' Returns if CodeEnd of EndOfFile found.
    ''' </summary>
    ''' <param name="EatNewLine">Eat the newline character?</param>
    ''' <param name="ReportError">Report the error "End of line expected."? (Always shown if this paramter is true.</param>
    ''' <remarks></remarks>
    Sub GotoNewline(ByVal EatNewLine As Boolean, Optional ByVal ReportError As Boolean = False)
        If ReportError Then Compiler.Report.ShowMessage(Messages.VBNC90018, CurrentLocation)

        Do Until CurrentToken.IsEndOfLine
            NextToken()
        Loop
        If EatNewLine AndAlso CurrentToken.IsEndOfFile = False Then NextToken()
    End Sub
    ''' <summary>
    ''' If not the current token is a newline, then shows a message ("End of line expected")
    ''' and eats the newline. After this sub the current token will be the first after the newline
    ''' Returns false if first symbol isn't a newline
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function FindNewLineAndShowError() As Boolean
        Return FindNewLineAndShowError(Messages.VBNC90018)
    End Function

    ''' <summary>
    ''' If not the current token is a newline, then shows the specified message
    ''' and eats the newline. After this sub the current token will be the first after the newline
    ''' Returns false if first symbol isn't a newline
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function FindNewLineAndShowError(ByVal Message As Messages) As Boolean
        If Not AcceptNewLine() Then
            Compiler.Report.ShowMessage(Message, CurrentLocation)
            GotoNewline(True, False)
            Return False
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' Skips token until it finds any of the specified tokens or KS.EndOfFile or KS.CodeEnd
    ''' </summary>
    ''' <param name="Specials"></param>
    ''' <remarks></remarks>
    Sub GotoAny(ByVal ParamArray Specials As KS())
        GotoAny(False, Specials)
    End Sub

    ''' <summary>
    ''' Skips tokens until it finds any of the specified tokens.
    ''' If StopOnNewLine = True then stops also when a NewLine is found 
    ''' (the newline token is not eaten).
    ''' </summary>
    ''' <param name="StopOnNewline"></param>
    ''' <param name="Specials"></param>
    ''' <remarks></remarks>
    Sub GotoAny(ByVal StopOnNewline As Boolean, ByVal ParamArray Specials() As KS)
        Do Until (CurrentToken.Equals(Specials))
            If StopOnNewline AndAlso CurrentToken.IsEndOfLine Then
                Return
            ElseIf CurrentToken.IsEndOfFile Then
                Return
            End If
            NextToken()
        Loop
    End Sub

    Sub EatNewLines()
        Do While AcceptNewLine()
        Loop
    End Sub

    ''' <summary>
    ''' Eats the current token if if coincides, if not shows a 
    ''' message "Expected: " and the keyword.
    ''' If GotoNewline = true then calls GotoNewline(True) - next token is the first one after the newline.
    ''' </summary>
    ''' <param name="Special"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Diagnostics.DebuggerHidden()> Function AcceptIfNotError(ByVal Special As KS, Optional ByVal GotoNewline As Boolean = False) As Boolean
        Return AcceptIfNotError(Special, Messages.VBNC90019, GotoNewline, Enums.strSpecial(Special))
    End Function

    ''' <summary>
    ''' Eats the current tokens if if coincides, if not shows a 
    ''' message "Expected: " and the keyword.
    ''' If GotoNewline = true then calls GotoNewline(True) - next token is the first one after the newline.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Diagnostics.DebuggerHidden()> Function AcceptIfNotError(ByVal Special1 As KS, ByVal Special2 As KS, Optional ByVal GotoNewline As Boolean = False) As Boolean
        Return AcceptIfNotError(Special1, Messages.VBNC90019, GotoNewline, Enums.strSpecial(Special1)) AndAlso AcceptIfNotError(Special2, Messages.VBNC90019, GotoNewline, Enums.strSpecial(Special2))
    End Function

    ''' <summary>
    ''' If the current token is not the specified keyword / symbol, then a
    ''' InternalException is thrown. (In which case it doesn't return,
    ''' so this is not a function).
    ''' </summary>
    ''' <param name="Special"></param>
    ''' <remarks></remarks>
    Sub AcceptIfNotInternalError(ByVal Special As KS)
        If Not Accept(Special) Then Throw New InternalException("Location: " & CurrentLocation.ToString(Compiler))
    End Sub

    Sub AcceptIfNotInternalError(ByVal Identifier As String)
        If Not Accept(Identifier) Then Throw New InternalException("")
    End Sub

    ''' <summary>
    ''' If GotoNewline = true then calls GotoNewline(True) - next token is the first one after the newline.
    ''' </summary>
    ''' <param name="Special"></param>
    ''' <param name="Message"></param>
    ''' <param name="GotoNewline"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function AcceptIfNotError(ByVal Special As KS, ByVal Message As Messages, Optional ByVal GotoNewline As Boolean = False) As Boolean
        If Accept(Special) Then
            Return True
        Else
            If GotoNewline Then Me.GotoNewline(True)
            Compiler.Report.ShowMessage(Message, CurrentLocation)
            Return False
        End If
    End Function

    ''' <summary>
    ''' If GotoNewline = true then calls GotoNewline(True) - next token is the first one after the newline.
    ''' </summary>
    ''' <param name="Special"></param>
    ''' <param name="Message"></param>
    ''' <param name="GotoNewline"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Diagnostics.DebuggerHidden()> Function AcceptIfNotError(ByVal Special As KS, ByVal Message As Messages, ByVal GotoNewline As Boolean, ByVal MessageParameters() As String) As Boolean
        If Accept(Special) Then
            Return True
        Else
            If GotoNewline Then Me.GotoNewline(True)
            Compiler.Report.ShowMessage(Message, CurrentLocation, MessageParameters)
            Return False
        End If
    End Function

    <Diagnostics.DebuggerHidden()> Function AcceptIfNotError(ByVal Special As KS, ByVal Message As Messages, ByVal GotoNewline As Boolean, ByVal MessageParameter As String) As Boolean
        If Accept(Special) Then
            Return True
        Else
            If GotoNewline Then Me.GotoNewline(True)
            Compiler.Report.ShowMessage(Message, CurrentLocation, MessageParameter)
            Return False
        End If
    End Function
    ''' <summary>
    ''' GotoNewline defaults to false for this overload.
    ''' </summary>
    ''' <param name="Special"></param>
    ''' <param name="Message"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function AcceptIfNotError(ByVal Special As KS, ByVal Message As Messages, ByVal ParamArray MessageParameters() As String) As Boolean
        Return AcceptIfNotError(Special, Message, False, MessageParameters)
    End Function

    ''' <summary>
    ''' If GotoNewline = true then calls GotoNewline(True) - next token is the first one after the newline.
    ''' Accepts only newline, not endofcode, nor endoffile. 
    ''' </summary>
    ''' <param name="GotoNewline"></param>
    ''' <param name="EOFIsError">Specifies whether to return false if the current token is EOF or not.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function AcceptNewLine(Optional ByVal GotoNewline As Boolean = False, Optional ByVal EOFIsError As Boolean = True, Optional ByVal ReportError As Boolean = False) As Boolean
        If CurrentToken.IsEndOfLine Then
            If CurrentToken.IsEndOfLineOnly Then
                NextToken()
                Return True
            ElseIf EOFIsError = False Then
                Return True
            Else
                Return False
            End If
        Else
            If GotoNewline Then Me.GotoNewline(True, ReportError)
            Return False
        End If
    End Function

    ''' <summary>
    ''' Accepts Newline or : (not endoffile, nor endofcode)
    ''' If ReportError = True then:
    '''  - reports an error if currenttoken != ks.colon AND currenttoken != newline
    '''    doesn't matter if OnlyColon is true or not.
    ''' </summary>
    ''' <param name="OnlyColon">Set to true to only accept colon, not even NewLine</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function AcceptEndOfStatement(Optional ByVal OnlyColon As Boolean = False, Optional ByVal ReportError As Boolean = False) As Boolean
        Dim result As Boolean = True
        If OnlyColon Then
            result = Accept(KS.Colon)
            If ReportError AndAlso result = False AndAlso CurrentToken.IsEndOfLineOnly = False Then
                Compiler.Report.ShowMessage(Messages.VBNC30205, CurrentLocation)
            End If
            Return result
        Else
            If CurrentToken.IsEndOfLineOnly OrElse CurrentToken() = KS.Colon Then
                Do
                    NextToken()
                Loop While CurrentToken.IsEndOfLineOnly OrElse CurrentToken.Equals(KS.Colon)
                Return True
            ElseIf CurrentToken.IsEndOfFile Then
                Return True
            Else
                If ReportError Then
                    Compiler.Report.ShowMessage(Messages.VBNC30205, CurrentLocation)
                End If
                Return False
            End If
        End If
    End Function

    Function AcceptEndOfFile() As Boolean
        If CurrentToken.IsEndOfFile Then
            NextToken()
            Return True
        Else
            Return False
        End If
    End Function

    <Obsolete()> _
    Function AcceptIdentifier(ByRef result As Token) As Boolean
        Dim tmp As Token = CurrentToken()
        If CurrentToken.IsIdentifier Then
            result = CurrentToken()
            If tmp.IsIdentifier = False Then Throw New InternalException("Not an identifier?????")
            If CurrentToken.IsIdentifier = False Then Throw New InternalException("Not an identifier???")
            If result.IsIdentifier = False Then Throw New InternalException("Not an identifier?")
            NextToken()
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Returns true if the current token is an identifier
    ''' and advances to the next token.
    ''' If GotoNewline = true then calls GotoNewline(True) - next token is the first one after the newline.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function AcceptIdentifier(Optional ByVal GotoNewline As Boolean = False) As Boolean
        If CurrentToken.IsIdentifier Then
            NextToken()
            Return True
        Else
            If GotoNewline Then Me.GotoNewline(True)
            Return False
        End If
    End Function

    Function AcceptStringLiteral(Optional ByVal GotoNewline As Boolean = False) As Boolean
        If CurrentToken.IsStringLiteral Then
            NextToken()
            Return True
        Else
            If GotoNewline Then Me.GotoNewline(True)
            Return False
        End If
    End Function

    ''' <summary>
    ''' If GotoNewline = true then calls GotoNewline(True) - next token is the first one after the newline.
    ''' </summary>
    ''' <param name="GotoNewline"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function AcceptIntegerLiteral(Optional ByVal GotoNewline As Boolean = False) As Boolean
        If CurrentToken.IsIntegerLiteral Then
            NextToken()
            Return True
        Else
            If GotoNewline Then Me.GotoNewline(True)
            Return False
        End If
    End Function

    ''' <summary>
    ''' If GotoNewline = true then calls GotoNewline(True) - next token is the first one after the newline.
    ''' </summary>
    ''' <param name="Special"></param>
    ''' <param name="GotoNewline"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Accept(ByVal Special As KS, Optional ByVal GotoNewline As Boolean = False) As Boolean
        If CurrentToken.Equals(Special) Then
            Accept = True
            NextToken()
        Else
            If GotoNewline Then Me.GotoNewline(True)
            Accept = False
        End If
    End Function

    ''' <summary>
    ''' If GotoNewline = true then calls GotoNewline(True) - next token is the first one after the newline.
    ''' </summary>
    ''' <param name="GotoNewline"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Accept(ByVal Special1 As KS, ByVal Special2 As KS, Optional ByVal GotoNewline As Boolean = False) As Boolean
        If CurrentToken.Equals(Special1) AndAlso PeekToken.Equals(Special2) Then
            NextToken()
            NextToken()
        Else
            If GotoNewline Then Me.GotoNewline(True)
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' If GotoNewline = true then calls GotoNewline(True) - next token is the first one after the newline.
    ''' </summary>
    Function Accept(ByVal Identifier As String, Optional ByVal GotoNewline As Boolean = False) As Boolean
        If CurrentToken.Equals(Identifier) Then
            Accept = True
            NextToken()
        Else
            If GotoNewline Then Me.GotoNewline(True)
            Accept = False
        End If
    End Function

    Function AcceptAny(ByVal ParamArray Keywords() As KS) As Boolean
        Dim i As Integer
        For i = 0 To Keywords.Length - 1
            If Accept(Keywords(i)) Then
                Return True
            End If
        Next
        Return False
    End Function
    Function AcceptAll(ByVal ParamArray Specials() As KS) As Boolean
        Dim i As Integer
        AcceptAll = True
        For i = 0 To Specials.Length - 1
            AcceptAll = PeekToken(i).Equals(Specials(i)) AndAlso AcceptAll
        Next
        If AcceptAll Then NextToken(Specials.Length)
    End Function
End Class
