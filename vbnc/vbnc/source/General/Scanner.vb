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

#If DEBUG Then
#Const DOEOFCHECK = 0
#Const EXTENDEDDEBUG = 0
#End If
#Const EXTENDED = False

Public Class Scanner
    Inherits BaseObject

    'Useful constants.
    Private Const nl27 As Char = """"c
    Private Const nl201C As Char = Microsoft.VisualBasic.ChrW(&H201C)
    Private Const nl201D As Char = Microsoft.VisualBasic.ChrW(&H201D)
    Private Const nl0 As Char = Microsoft.VisualBasic.ChrW(0)
    Private Const nlA As Char = Microsoft.VisualBasic.ChrW(&HA)
    Private Const nlD As Char = Microsoft.VisualBasic.ChrW(&HD)
    Private Const nl2028 As Char = Microsoft.VisualBasic.ChrW(&H2028)
    Private Const nl2029 As Char = Microsoft.VisualBasic.ChrW(&H2029)
    Private Const nlTab As Char = Microsoft.VisualBasic.ChrW(9)

    Private Const COMMENTCHAR1 As Char = "'"c
    Private Const COMMENTCHAR2 As Char = Microsoft.VisualBasic.ChrW(&H2018)
    Private Const COMMENTCHAR3 As Char = Microsoft.VisualBasic.ChrW(&H2019)

    ''' <summary>
    ''' The total number of lines scanned.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_TotalLineCount As UInteger

    ''' <summary>
    ''' The total number of characters scanned.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_TotalCharCount As Integer

    ''' <summary>
    ''' The current line.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_CurrentLine As UInteger

    ''' <summary>
    ''' The current column.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_CurrentColumn As Byte

    ''' <summary>
    ''' The current code file.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_CodeFile As CodeFile
    Private m_CodeFileIndex As UShort

    Private m_PreviousChar As Char
    Private m_CurrentChar As Char
    Private m_EndOfFile As Boolean
    Private m_PeekedChars As New Generic.Queue(Of Char)
    Private m_Reader As System.IO.StreamReader

    Private m_StringBuilder(127) As Char
    Private m_StringBuilderLength As Integer

    ''' <summary>
    ''' If any tokens has been found on this line.
    ''' Reset by IncLine, set by NewToken
    ''' </summary>
    ''' <remarks></remarks>
    Private m_TokensSeenOnLine As Integer

    Private m_Files As Generic.Queue(Of CodeFile)

    'Data about the current token
    Private m_LastWasNewline As Boolean
    Private m_Current As Token
    Private m_CurrentTypeCharacter As TypeCharacters.Characters
    Private m_CurrentData As Object
    Private m_Peeked As Token?

#Region "Conditional Compilation"
    'Data related to conditional compilation
    Private m_ProjectConstants As New ConditionalConstants
    Private m_CurrentConstants As ConditionalConstants
    Private m_Evaluator As New ConditionalExpression(Me)

    ''' <summary>
    ''' 0 if condition is false and has never been true
    ''' 1 if condition is true
    ''' -1 if condition has been true
    ''' </summary>
    ''' <remarks></remarks>
    Private m_ConditionStack As New Generic.List(Of Integer)

    Private m_Methods As New Generic.Dictionary(Of Mono.Cecil.MethodReference, Mono.Collections.Generic.Collection(Of Mono.Cecil.CustomAttribute))

    Function IsConditionallyExcluded(ByVal CalledMethod As Mono.Cecil.MethodReference, ByVal AtLocation As Span) As Boolean
        Dim attribs As Mono.Collections.Generic.Collection(Of Mono.Cecil.CustomAttribute)

        If m_Methods.ContainsKey(CalledMethod) Then
            attribs = m_Methods(CalledMethod)
        Else
            attribs = CecilHelper.FindDefinition(CalledMethod).CustomAttributes
            m_Methods.Add(CalledMethod, attribs)
        End If

        If attribs Is Nothing OrElse attribs.Count = 0 Then Return False

        For i As Integer = 0 To attribs.Count - 1
            Dim attrib As Mono.Cecil.CustomAttribute = attribs(i)
            Dim identifier As String
            If Helper.CompareType(Compiler.TypeCache.System_Diagnostics_ConditionalAttribute, attrib.AttributeType) = False Then
                Continue For
            End If
            If attrib.ConstructorArguments.Count <> 1 Then
                Continue For
            End If
            identifier = TryCast(attrib.ConstructorArguments(0).Value, String)
            If identifier = String.Empty Then Continue For

            If Not IsDefinedAtLocation(identifier, AtLocation) Then Return True
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

    ReadOnly Property IfdOut() As Boolean
        Get
            For i As Integer = 0 To m_ConditionStack.Count - 1
                If Not m_ConditionStack(i) > 0 Then Return True
            Next
            Return False
        End Get
    End Property

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
        Dim name As String
        Dim value As Object = Nothing

        If m_Current <> KS.Const Then
            Helper.AddError(Me.Compiler, Me.GetCurrentLocation, "Expected 'Const'")
            Me.EatLine(False)
            Return
        End If

        Me.NextUnconditionally()

        If m_Current.IsIdentifier = False Then
            Compiler.Report.ShowMessage(Messages.VBNC30203, m_Current.Location)
            Me.EatLine(False)
            Return
        End If
        name = m_Current.Identifier
        Me.NextUnconditionally()

        If m_Current <> KS.Equals Then
            Compiler.Report.ShowMessage(Messages.VBNC30249, m_Current.Location)
            Return
        End If
        Me.NextUnconditionally()

        m_Evaluator.Parse(value)

        If Me.IfdOut = False Then
            m_CurrentConstants.Add(New ConditionalConstant(name, value))
            GetLocation.File(Compiler).AddConditionalConstants(GetLocation.Line, m_CurrentConstants)
        End If

        ParseEndOfLine()
    End Sub
#End Region

#Region "If"
    Private Sub ParseIf()
        Dim theExpression As ConditionalExpression
        Dim expression As Object = Nothing

        If Not m_Current = KS.If Then
            Helper.AddError(Me.Compiler, Me.GetCurrentLocation, "Expected 'If'")
            Me.EatLine(False)
            Return
        End If
        Me.NextUnconditionally()

        theExpression = New ConditionalExpression(Me)
        If Not theExpression.Parse(expression) Then
            EatLine(False)
            Return
        End If

        If m_Current = KS.Then Then
            Me.NextUnconditionally()
        End If

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

        If m_Current <> KS.ElseIf Then
            Helper.AddError(Me.Compiler, Me.GetCurrentLocation, "Expected 'ElseIf'")
            Me.EatLine(False)
            Return
        End If
        Me.NextUnconditionally()

        If theExpression.Parse(expression) = False Then
            EatLine(False)
            Return
        End If

        If m_Current = KS.Then Then
            Me.NextUnconditionally()
        End If

        ParseEndOfLine()

        If m_ConditionStack(m_ConditionStack.Count - 1) = 1 Then
            m_ConditionStack(m_ConditionStack.Count - 1) = -1
        ElseIf m_ConditionStack(m_ConditionStack.Count - 1) = 0 AndAlso CBool(expression) Then
            m_ConditionStack(m_ConditionStack.Count - 1) = 1
        End If
    End Sub

    Private Sub ParseElse()
        If m_Current <> KS.Else Then
            Helper.AddError(Me.Compiler, Me.GetCurrentLocation, "Expected 'Else'")
            Me.EatLine(False)
            Return
        End If
        Me.NextUnconditionally()

        If Not CheckEmtpyStack() Then Return

        If m_ConditionStack(m_ConditionStack.Count - 1) = 0 Then
            m_ConditionStack(m_ConditionStack.Count - 1) = 1
        ElseIf m_ConditionStack(m_ConditionStack.Count - 1) = 1 Then
            m_ConditionStack(m_ConditionStack.Count - 1) = -1
        End If
        ParseEndOfLine()
    End Sub

    Private Sub ParseEndIf()
        If m_Current <> KS.If Then
            Helper.AddError(Me.Compiler, Me.GetCurrentLocation, "Expected 'If'")
            Me.EatLine(False)
            Return
        End If
        Me.NextUnconditionally()

        If Not CheckEmtpyStack() Then Return

        m_ConditionStack.RemoveAt(m_ConditionStack.Count - 1)
        ParseEndOfLine()
    End Sub

    Private Function CheckEmtpyStack(Optional ByVal Msg As Messages = Messages.VBNC30013) As Boolean
        If m_ConditionStack.Count > 0 Then Return True

        Compiler.Report.ShowMessage(Msg, GetCurrentLocation)
        EatLine(False)

        Return False
    End Function
#End Region

#Region "Region"
    Private Sub ParseRegion()
        If m_Current.Equals("Region") = False Then
            Helper.AddError(Me.Compiler, Me.GetCurrentLocation, "Expected 'Region'")
            Me.EatLine(False)
            Return
        End If

        'Save the location of the #Region token to use as the location of any missing string literal
        Dim regionLoc As Span = GetCurrentLocation()

        Me.NextUnconditionally()

        If Not m_Current.IsStringLiteral Then
            Compiler.Report.ShowMessage(Messages.VBNC30217, regionLoc)
            EatLine(False)
            Return
        End If
        Me.NextUnconditionally()

        ParseEndOfLine()
    End Sub

    Private Sub ParseEndRegion()
        If m_Current.Equals("Region") = False Then
            Helper.AddError(Me.Compiler, Me.GetCurrentLocation, "Expected 'Region'")
            Me.EatLine(False)
            Return
        End If
        Me.NextUnconditionally()

        ParseEndOfLine()
    End Sub
#End Region

#Region "External Source"
    Private Sub ParseExternalSource()
        If m_Current.Equals("ExternalSource") = False Then
            Helper.AddError(Me.Compiler, Me.GetCurrentLocation, "Expected 'ExternalSource'")
            Me.EatLine(False)
            Return
        End If
        Me.NextUnconditionally()

        If m_Current <> KS.LParenthesis Then
            Helper.AddError(Compiler, GetCurrentLocation, "Expected '('")
            Me.EatLine(False)
            Return
        End If
        Me.NextUnconditionally()

        If m_Current.IsStringLiteral = False Then
            Helper.AddError(Compiler, GetCurrentLocation, "Expected string literal")
            Me.EatLine(False)
            Return
        End If
        Me.NextUnconditionally()

        If m_Current <> KS.Comma Then
            Helper.AddError(Compiler, GetCurrentLocation, "Expected ','")
            Me.EatLine(False)
            Return
        End If
        Me.NextUnconditionally()

        If m_Current.IsIntegerLiteral = False Then
            Helper.AddError(Compiler, GetCurrentLocation, "Expected integer literal")
            Me.EatLine(False)
            Return
        End If
        Me.NextUnconditionally()

        If m_Current <> KS.RParenthesis Then
            Helper.AddError(Compiler, GetCurrentLocation, "Expected ')'")
            Me.EatLine(False)
            Return
        End If
        Me.NextUnconditionally()

        ParseEndOfLine()
    End Sub

    Private Sub ParseEndExternalSource()
        If m_Current.Equals("ExternalSource") = False Then
            Helper.AddError(Me.Compiler, Me.GetCurrentLocation, "Expected 'ExternalSource'")
            Me.EatLine(False)
            Return
        End If
        Me.NextUnconditionally()

        ParseEndOfLine()
    End Sub
#End Region

    Private Sub ParseEndOfLine()
        If m_Current.IsEndOfLine = False Then
            Helper.AddError(Me.Compiler, GetCurrentLocation, "Expected end of line")
            EatLine(False)
            Return
        End If
        'Me.NextUnconditionally()
    End Sub

    Private Sub ParseEnd()
        If m_Current <> KS.End Then
            Helper.AddError(Me.Compiler, Me.GetCurrentLocation, "Expected 'End'")
            Me.EatLine(False)
            Return
        End If
        Me.NextUnconditionally()

        If m_Current = KS.If Then
            ParseEndIf()
        ElseIf m_Current.Equals("ExternalSource") Then
            ParseEndExternalSource()
        ElseIf m_Current.Equals("Region") Then
            ParseEndRegion()
        Else
            Helper.AddError(Me, "'End' what?")
            Me.EatLine(False)
            Return
        End If
    End Sub

    Public Function [Next]() As Token
        Do
            NextUnconditionally()

            If m_Current.IsEndOfCode Then
                Return m_Current
            End If

            If m_Current.IsEndOfFile Then
                ResetCurrentConstants()
                Return m_Current
            End If

            If TokensSeenOnLine = 1 AndAlso m_Current = KS.Numeral Then

                Me.NextUnconditionally()

                If m_Current.IsEndOfFile Then
                    ResetCurrentConstants()
                    Return m_Current
                ElseIf m_Current.IsEndOfLine Then
                    EatLine(True)
                    Return Me.Next()
                End If

                If m_Current = KS.If Then
                    ParseIf()
                ElseIf m_Current = KS.Else Then
                    ParseElse()
                ElseIf m_Current = KS.ElseIf Then
                    ParseElseIf()
                ElseIf m_Current = KS.Const Then
                    ParseConst()
                ElseIf m_Current.Equals("ExternalSource") Then
                    ParseExternalSource()
                ElseIf m_Current.Equals("Region") Then
                    ParseRegion()
                ElseIf m_Current = KS.End Then
                    ParseEnd()
                Else
                    Compiler.Report.ShowMessage(Messages.VBNC30248, GetCurrentLocation())
                    EatLine(False)
                End If
            ElseIf IfdOut Then
                If m_Current.IsEndOfLine = False Then EatLine(False)
                Continue Do
            Else
                If m_Current.IsEndOfLineOnly AndAlso m_LastWasNewline Then
                    Continue Do
                End If
                m_LastWasNewline = m_Current.IsEndOfLineOnly
                Return m_Current
            End If
        Loop While m_Current.IsEndOfCode = False AndAlso m_Current.IsEndOfFile = False
        Return m_Current
    End Function
#End Region

#Region "StringBuilder"
    Private Property StringBuilderLength As Integer
        Get
            Return m_StringBuilderLength
        End Get
        Set(ByVal value As Integer)
            m_StringBuilderLength = value
        End Set
    End Property

    Private Sub StringBuilderAppend(ByVal c As Char)
        m_StringBuilderLength += 1
        If m_StringBuilder Is Nothing Then
            ReDim m_StringBuilder(31)
        End If
        If m_StringBuilder.Length < m_StringBuilderLength Then
            Dim tmp(Math.Max(m_StringBuilder.Length * 2 - 1, m_StringBuilderLength)) As Char
            m_StringBuilder.CopyTo(tmp, 0)
            m_StringBuilder = tmp
        End If
        m_StringBuilder(m_StringBuilderLength - 1) = c
    End Sub

    Private Function StringBuilderToString() As String
        Dim result As String
        result = New String(m_StringBuilder, 0, m_StringBuilderLength)
        m_StringBuilderLength = 0
        Return result
    End Function
#End Region

    Private Structure Data
        Public Type As TokenType
        Public Symbol As KS
        Public Data As Object
        Public TypeCharacter As TypeCharacters.Characters

        Public Sub Clear()
            Type = vbnc.TokenType.None
            Symbol = KS.None
            Data = Nothing
            TypeCharacter = TypeCharacters.Characters.None
        End Sub
    End Structure

    ReadOnly Property TokensSeenOnLine() As Integer
        Get
            Return m_TokensSeenOnLine
        End Get
    End Property

    ReadOnly Property TotalLineCount() As UInteger
        Get
            Return m_TotalLineCount
        End Get
    End Property

    ReadOnly Property TotalCharCount() As Integer
        Get
            Return m_TotalCharCount
        End Get
    End Property

    Private Function IsNewLine() As Boolean
        Return IsNewLine(CurrentChar)
    End Function

    Public Shared Function IsNewLine(ByVal chr As Char) As Boolean
        Return chr = nlA OrElse chr = nlD OrElse chr = nl2028 OrElse chr = nl2029 OrElse chr = nl0
    End Function

    Private Function IsUnderscoreCharacter(ByVal chr As Char) As Boolean
        'UnderscoreCharacter ::= < Unicode connection character (class Pc) >
        Return Char.GetUnicodeCategory(chr) = Globalization.UnicodeCategory.ConnectorPunctuation
    End Function

    Private Function IsIdentifierCharacter(ByVal chr As Char) As Boolean
        'IdentifierCharacter ::=
        '   UnderscoreCharacter |
        '   AlphaCharacter |
        '   NumericCharacter |
        '   CombiningCharacter |
        '   FormattingCharacter
        Return IsUnderscoreCharacter(chr) OrElse _
                IsAlphaCharacter(chr) OrElse _
                IsNumericCharacter(chr) OrElse _
                IsCombiningCharacter(chr) OrElse _
                IsFormattingCharacter(chr)
    End Function

    Private Function IsNumericCharacter(ByVal chr As Char) As Boolean
        'NumericCharacter ::= < Unicode decimal digit character (class Nd) >
        Return Char.GetUnicodeCategory(chr) = Globalization.UnicodeCategory.DecimalDigitNumber 'Nd
    End Function

    Private Function IsOperatorCharacter(ByVal chr As Char) As Boolean
        'Operator ::= & * +  -  /  \  ^ <  =  >
        Return chr = "&"c OrElse _
                chr = "*"c OrElse _
                chr = "+"c OrElse _
                chr = "-"c OrElse _
                chr = "/"c OrElse _
                chr = "\"c OrElse _
                chr = "^"c OrElse _
                chr = "<"c OrElse _
                chr = "="c OrElse _
                chr = ">"c
        chr = ":"c
    End Function

    Private Function IsSeparatorCharacter(ByVal chr As Char) As Boolean
        'Separator ::= (  )  {  }  !  #  ,  .  :
        Return chr = "("c OrElse _
                chr = ")"c OrElse _
                chr = "{"c OrElse _
                chr = "}"c OrElse _
                chr = "!"c OrElse _
                chr = "#"c OrElse _
                chr = ","c OrElse _
                chr = "."c OrElse _
                chr = ":"c
    End Function

    Private Function IsCombiningCharacter(ByVal chr As Char) As Boolean
        'CombiningCharacter ::= < Unicode combining character (classes Mn, Mc) >
        Select Case Char.GetUnicodeCategory(chr)
            Case Globalization.UnicodeCategory.NonSpacingMark 'Mn
                Return True
            Case Globalization.UnicodeCategory.SpacingCombiningMark 'Mc
                Return True
            Case Else
                Return False
        End Select
    End Function

    Private Function IsFormattingCharacter(ByVal chr As Char) As Boolean
        'FormattingCharacter ::= < Unicode formatting character (class Cf) >
        Return Char.GetUnicodeCategory(chr) = Globalization.UnicodeCategory.Format 'Cf
    End Function

    Private Function IsAlphaCharacter(ByVal chr As Char) As Boolean
        'AlphaCharacter ::= < Unicode alphabetic character (classes Lu, Ll, Lt, Lm, Lo, Nl) >
        Select Case Char.GetUnicodeCategory(chr) 'Alpha Character
            Case Globalization.UnicodeCategory.UppercaseLetter 'Lu
                Return True
            Case Globalization.UnicodeCategory.LowercaseLetter 'Ll
                Return True
            Case Globalization.UnicodeCategory.TitlecaseLetter  'Lt
                Return True
            Case Globalization.UnicodeCategory.ModifierLetter 'Lm
                Return True
            Case Globalization.UnicodeCategory.OtherLetter 'Lo
                Return True
            Case Globalization.UnicodeCategory.LetterNumber 'Nl
                Return True
            Case Else
                Return False
        End Select
    End Function

    Private Function IsLineContinuation() As Boolean
        If Not (CurrentChar() = " "c AndAlso PeekChar() = "_"c) Then Return False

        Dim i As Integer = 2
        Do Until IsNewLine(PeekChars(i))
            If IsWhiteSpace(PeekChars(i)) = False Then Return False
            i += 1
        Loop

        Return True
    End Function

    Shared Function IsWhiteSpace(ByVal chr As Char) As Boolean
        Return chr = nlTab OrElse Char.GetUnicodeCategory(chr) = Globalization.UnicodeCategory.SpaceSeparator
    End Function

    Private Function IsWhiteSpace() As Boolean
        Return IsWhiteSpace(CurrentChar())
    End Function

    Private Function IsComment(ByVal chr As Char) As Boolean
        Return chr = COMMENTCHAR1 OrElse chr = COMMENTCHAR2 OrElse chr = COMMENTCHAR3
    End Function

    Private Function IsComment() As Boolean
        Return IsComment(CurrentChar)
    End Function

    ''' <summary>
    ''' Eat all characters until the newline character(s). Optionally eat the newline character(s) as well
    ''' </summary>
    ''' <param name="NewLineCharAlso"></param>
    ''' <remarks></remarks>
    Private Sub EatLine(ByVal NewLineCharAlso As Boolean)
        'LineTerminator ::=
        '  < Unicode carriage return character (0x000D) > |
        '  < Unicode line feed character (0x000A) > |
        '  < Unicode carriage return character > < Unicode line feed character > |
        '  < Unicode line separator character (0x2028) > |
        '  < Unicode paragraph separator character (0x2029) >

        Dim ch As Char = m_CurrentChar
        Do Until IsNewLine(ch)
            ch = NextChar()
        Loop

        If NewLineCharAlso Then
            EatNewLine()
        End If
    End Sub

    Private Sub EatNewLine()
        Select Case CurrentChar()
            Case nlD
                NextChar()
                If CurrentChar() = nlA Then
                    NextChar()
                End If
                IncLine()
            Case nlA, nl2029, nl2028
                NextChar()
                IncLine()
            Case nl0
                IncLine()
            Case Else
                Throw New InternalException("Current character is not a new line.")
        End Select
    End Sub

    Public Shared Function IsSingleNewLine(ByVal chr1 As Char, ByVal chr2 As Char) As Boolean
        Return Not (chr1 = nlD AndAlso chr2 = nlA)
    End Function

    Private Sub EatComment()
        Select Case CurrentChar()
            Case COMMENTCHAR1, COMMENTCHAR2, COMMENTCHAR3 'Traditional VB comment
                EatLine(False) 'do not eat newline, it needs to be added as a token
                Return
            Case Else
                REM is taken care of some other place.
                'Function should never be called if not a comment
                Throw New InternalException("EatComment called with no comment.")
        End Select
    End Sub

    Private Sub EatWhiteSpace()
        While IsWhiteSpace()
            If IsLineContinuation() Then
                EatLine(True)
            Else
                NextChar()
            End If
        End While
    End Sub

    Private Function GetDate() As Token
        Helper.Assert(CurrentChar() = "#"c, "GetDate called without a date!")

        EatWhiteSpace()

        Dim Count As Integer
        'Date value
        Dim bCont As Boolean = True
        StringBuilderLength = 0
        Do
            Count += 1
            Dim ch As Char = NextChar()
            If (IsNewLine()) Then
                Compiler.Report.ShowMessage(Messages.VBNC90000, GetCurrentLocation())
                bCont = False
            Else
                Select Case ch
                    Case nl0
                        Compiler.Report.ShowMessage(Messages.VBNC90001, GetCurrentLocation())
                        bCont = False
                    Case "#"c
                        NextChar() 'The ending #
                        bCont = False
                End Select
            End If
            If bCont Then StringBuilderAppend(ch)
        Loop While bCont

        Return Token.CreateDateToken(GetCurrentLocation, CDate(StringBuilderToString))
    End Function

    Private Function CanStartIdentifier() As Boolean
        Return CanStartIdentifier(CurrentChar)
    End Function

    Private Function CanStartIdentifier(ByVal chr As Char) As Boolean
        Return IsAlphaCharacter(chr) OrElse IsUnderscoreCharacter(chr)
    End Function

    Private Function GetEscapedIdentifier() As Token
        'EscapedIdentifier  ::=  [  IdentifierName  ] 
        Helper.Assert(CurrentChar() = "["c)
        NextChar()
        Dim id As Token
        id = GetIdentifier(True)
        If CurrentChar() = "]"c = False Then
            Compiler.Report.ShowMessage(Messages.VBNC30034, GetCurrentLocation)
        Else
            NextChar()
        End If
        Return id
    End Function

    Private Function GetIdentifier(Optional ByVal Escaped As Boolean = False) As Token
        Dim bValid As Boolean = False
        Dim ch As Char

        'Identifier  ::=
        '	NonEscapedIdentifier  [  TypeCharacter  ]  |
        '	Keyword  TypeCharacter  |
        '	EscapedIdentifier
        '
        'NonEscapedIdentifier  ::=  < IdentifierName but not Keyword >
        'EscapedIdentifier  ::=  [  IdentifierName  ] 
        '
        'IdentifierName ::= IdentifierStart [ IdentifierCharacter+ ]

        'IdentifierStart ::=
        '   AlphaCharacter |
        '   UnderscoreCharacter IdentifierCharacter 

        'IdentifierCharacter ::=
        '   UnderscoreCharacter |
        '   AlphaCharacter |
        '   NumericCharacter |
        '   CombiningCharacter |
        '   FormattingCharacter
        StringBuilderLength = 0

        ch = CurrentChar()
        StringBuilderAppend(ch)
        If IsAlphaCharacter(ch) Then
            bValid = True
        ElseIf IsUnderscoreCharacter(ch) Then
            ch = NextChar()
            StringBuilderAppend(ch)
            bValid = IsIdentifierCharacter(ch)
        End If

        If Not bValid Then
            Compiler.Report.ShowMessage(Messages.VBNC30203, Me.GetCurrentLocation(), CStr(ch))
            Return Nothing
        Else
            Do While IsIdentifierCharacter(NextChar)
                StringBuilderAppend(CurrentChar)
            Loop
        End If

        'The type character ! presents a special problem in that it can be used both as a type character and 
        'as a separator in the language. To remove ambiguity, a ! character is a type character as long as 
        'the character that follows it cannot start an identifier. If it can, then the ! character is a separator, 
        'not a type character.
        Dim typecharacter As TypeCharacters.Characters
        Dim canstartidentifier As Boolean = Me.IsLastChar = False AndAlso (IsAlphaCharacter(PeekChar) OrElse IsUnderscoreCharacter(PeekChar))
        If TypeCharacters.IsTypeCharacter(CurrentChar, typecharacter) AndAlso (canstartidentifier = False OrElse typecharacter <> TypeCharacters.Characters.SingleTypeCharacter) Then
            NextChar()
            m_CurrentTypeCharacter = typecharacter
            Return Token.CreateIdentifierToken(GetCurrentLocation, StringBuilderToString())
        Else
            Dim keyword As KS
            If Escaped = False AndAlso Token.IsKeyword(m_StringBuilder, m_StringBuilderLength, keyword) Then
                Return Token.CreateKeywordToken(GetCurrentLocation, keyword)
            Else
                m_CurrentTypeCharacter = typecharacter
                Return Token.CreateIdentifierToken(GetCurrentLocation, StringBuilderToString())
            End If
        End If
    End Function

    Private Function GetString() As Token
        Dim bEndOfString As Boolean = False
        StringBuilderLength = 0
        Do
            Select Case NextChar()
                Case nl27, nl201C, nl201D
                    'If " followed by a ", output one "
                    Dim nc As Char = NextChar()
                    If nc = nl27 OrElse nc = nl201C OrElse nc = nl201D Then
                        StringBuilderAppend(nc)
                    Else
                        bEndOfString = True
                    End If
                Case nlA, nlD, nl2028, nl2029
                    Compiler.Report.ShowMessage(Messages.VBNC30648, GetCurrentLocation)
                    bEndOfString = True
                Case Else
                    If m_EndOfFile Then
                        Compiler.Report.ShowMessage(Messages.VBNC30648, GetCurrentLocation)
                        'PreviousChar() 'Step back
                        bEndOfString = True
                    Else
                        StringBuilderAppend(CurrentChar())
                    End If

            End Select
        Loop While bEndOfString = False
        If CurrentChar() = "C"c OrElse CurrentChar() = "c"c Then
            'Is a char type character
            NextChar()
            If StringBuilderLength <> 1 Then
                Compiler.Report.ShowMessage(Messages.VBNC30004, GetCurrentLocation())
                Return Token.CreateStringLiteral(GetCurrentLocation, StringBuilderToString)
            Else
                Return Token.CreateCharToken(GetCurrentLocation, m_StringBuilder(0))
            End If
        Else
            Return Token.CreateStringLiteral(GetCurrentLocation, StringBuilderToString)
        End If
    End Function

    Private Function GetNumber() As Token
        Dim Base As IntegerBase
        Dim bReal As Boolean
        Dim bE As Boolean
        Static Builder As New Text.StringBuilder
        Builder.Length = 0

        'First find the type of the number
        Select Case CurrentChar()
            Case "."c, "0"c To "9"c 'Decimal
                Base = IntegerBase.Decimal
            Case "&"c
                Select Case NextChar()
#If EXTENDED Then
                    Case "b"c, "B"c 'Binary
                        Base = IntegerBase.Binary
#End If
                    Case "d"c, "D"c 'Decimal
                        Base = IntegerBase.Decimal
                    Case "h"c, "H"c 'Hex
                        Base = IntegerBase.Hex
                    Case "o"c, "O"c 'Octal
                        Base = IntegerBase.Octal
                    Case Else
                        Throw New InternalException(GetCurrentLocation.ToString(Compiler)) 'Should never get here, this function should only be called with the correct specifiers.
                End Select
                NextChar()
            Case Else
                Throw New InternalException("Invalid character: " & CurrentChar.ToString & ", Location: " & GetCurrentLocation.ToString(Compiler))
        End Select

        Dim ch As Char = CurrentChar()
        ' Then start the parsing
        Select Case Base
            Case IntegerBase.Decimal
                While Me.IsNumericCharacter(ch)
                    Builder.Append(ch)
                    ch = NextChar()
                End While
                If ch = "."c Then
                    If Me.IsNumericCharacter(Me.PeekChar) Then
                        Builder.Append(ch)
                        bReal = True
                        ch = NextChar()
                        While Me.IsNumericCharacter(ch)
                            Builder.Append(ch)
                            ch = NextChar()
                        End While
                    End If
                End If
                If ch = "E"c OrElse ch = "e"c Then
                    bE = True
                    bReal = True
                    Builder.Append(ch)
                    ch = NextChar()
                    If ch = "+"c OrElse ch = "-"c Then
                        Builder.Append(ch)
                        ch = NextChar()
                    End If
                    While Me.IsNumericCharacter(ch)
                        Builder.Append(ch)
                        ch = NextChar()
                    End While
                End If
#If EXTENDED Then
            Case IntegerBase.Binary
                While ((ch >= "0"c) AndAlso (ch <= "1"c))
                    Builder.Append(ch)
                    ch = NextChar()
                End While
#End If
            Case IntegerBase.Hex
                While (((ch >= "0"c) AndAlso (ch <= "9"c)) OrElse _
                  ((ch >= "a"c) AndAlso (ch <= "f"c)) OrElse _
                  ((ch >= "A"c) AndAlso (ch <= "F"c)))
                    Builder.Append(ch)
                    ch = NextChar()
                End While
            Case IntegerBase.Octal
                While ((ch >= "0"c) AndAlso (ch <= "7"c))
                    Builder.Append(ch)
                    ch = NextChar()
                End While
            Case Else
                Throw New InternalException(GetCurrentLocation.ToString(Compiler))
        End Select

        'Find the type character, if any
        Dim strType As String = ""
        Dim typeOfNumber As BuiltInDataTypes
        Dim typeCharacter As LiteralTypeCharacters_Characters = LiteralTypeCharacters_Characters.None

        Dim test As String
        test = CurrentChar()
        If test = "U" OrElse test = "u" Then test &= PeekChar()
        typeCharacter = LiteralTypeCharacters.GetTypeCharacter(test)
        If typeCharacter <> LiteralTypeCharacters_Characters.None Then
            NextChar()
            If test.Length = 2 Then NextChar()
            typeOfNumber = LiteralTypeCharacters.GetBuiltInType(typeCharacter)
        End If

        If typeCharacter <> LiteralTypeCharacters_Characters.None AndAlso LiteralTypeCharacters.IsIntegral(typeCharacter) = False AndAlso Base <> IntegerBase.Decimal Then
            Compiler.Report.ShowMessage(Messages.VBNC90002, Me.GetCurrentLocation(), KS.Decimal.ToString)
        End If

        ' Found the string of the number
        Dim strResult As String = Builder.ToString
        Dim IntegerValue As ULong

        Select Case Base
            Case IntegerBase.Decimal
                Try
                    Dim tp As BuiltInDataTypes
                    If typeCharacter = LiteralTypeCharacters_Characters.None Then
                        If bReal Then
                            tp = BuiltInDataTypes.Double
                        Else
                            tp = BuiltInDataTypes.Integer
                        End If
                    Else
                        tp = LiteralTypeCharacters.GetBuiltInType(typeCharacter)
                    End If
                    Select Case tp
                        Case BuiltInDataTypes.Decimal
                            GetNumber = Token.CreateDecimalToken(GetCurrentLocation, Decimal.Parse(strResult, Helper.USCulture))
                        Case BuiltInDataTypes.Double
                            GetNumber = Token.CreateDoubleToken(GetCurrentLocation, Double.Parse(strResult, Helper.USCulture))
                        Case BuiltInDataTypes.Single
                            GetNumber = Token.CreateSingleToken(GetCurrentLocation, Single.Parse(strResult, Helper.USCulture))
                        Case BuiltInDataTypes.Integer, BuiltInDataTypes.Long, BuiltInDataTypes.Short, BuiltInDataTypes.UInteger, BuiltInDataTypes.ULong, BuiltInDataTypes.UShort
                            If bReal Then
                                Compiler.Report.ShowMessage(Messages.VBNC90002, GetCurrentLocation(), typeCharacter.ToString)
                                IntegerValue = 0
                            Else
                                'Try to parse the result
                                IntegerValue = ULong.Parse(strResult, Helper.USCulture)
                            End If
                            'Check if value is out of range for data type.
                            Dim bOutOfRange As Boolean
                            'TODO: Make error code number.
                            Select Case tp
                                Case BuiltInDataTypes.Integer
                                    If IntegerValue > Integer.MaxValue Then bOutOfRange = True
                                Case BuiltInDataTypes.Long
                                    If IntegerValue > Long.MaxValue Then bOutOfRange = True
                                Case BuiltInDataTypes.Short
                                    If IntegerValue > Short.MaxValue Then bOutOfRange = True
                                Case BuiltInDataTypes.UInteger
                                    If IntegerValue > UInteger.MaxValue Then bOutOfRange = True
                                Case BuiltInDataTypes.ULong 'Not necessary
                                    '    If IntegerValue > Integer.MaxValue Then bOutOfRange = True
                                Case BuiltInDataTypes.UShort
                                    If IntegerValue > UShort.MaxValue Then bOutOfRange = True
                                Case Else
                                    Throw New InternalException("")
                            End Select
                            If bOutOfRange AndAlso typeCharacter <> LiteralTypeCharacters_Characters.None Then
                                Compiler.Report.ShowMessage(Messages.VBNC30439, GetCurrentLocation(), typeCharacter.ToString)
                            End If
                            GetNumber = GetIntegralToken(ULong.Parse(strResult, Helper.USCulture), Base, typeCharacter)
                        Case Else
                            Compiler.Report.ShowMessage(Messages.VBNC90002, GetCurrentLocation(), typeCharacter.ToString)
                            GetNumber = Token.CreateDoubleToken(GetCurrentLocation, 0)
                    End Select
                Catch ex As System.OverflowException
                    Compiler.Report.ShowMessage(Messages.VBNC30036, GetCurrentLocation())
                    GetNumber = Token.CreateDoubleToken(GetCurrentLocation, 0)
                Catch ex As Exception
                    Compiler.Report.ShowMessage(Messages.VBNC90005, GetCurrentLocation())
                    GetNumber = Token.CreateDoubleToken(GetCurrentLocation, 0)
                End Try
#If EXTENDED Then
            Case IntegerBase.Binary
                Try
                    IntegerValue = Helper.BinToInt(strResult)
                    IntegerValue = ConvertNonDecimalBits(IntegerValue, typeCharacter)
                Catch ex As Exception
                    Compiler.Report.ShowMessage(Messages.VBNC90006, "binary")
                End Try
                GetNumber = GetIntegralToken(IntegerValue, Base, typeCharacter)
#End If
            Case IntegerBase.Hex
                Try
                    'Console.WriteLine("Hex: " & strResult)
                    IntegerValue = Helper.HexToInt(strResult)
                Catch ex As Exception
                    Compiler.Report.ShowMessage(Messages.VBNC90006, Me.GetCurrentLocation, "hexadecimal")
                End Try
                GetNumber = GetIntegralToken(IntegerValue, Base, typeCharacter)
            Case IntegerBase.Octal
                Try
                    IntegerValue = Helper.OctToInt(strResult)
                Catch ex As Exception
                    Compiler.Report.ShowMessage(Messages.VBNC90006, GetCurrentLocation(), "octal")
                End Try
                GetNumber = GetIntegralToken(IntegerValue, Base, typeCharacter)
            Case Else
                Throw New InternalException(GetCurrentLocation.ToString(Compiler))
        End Select
    End Function

    Private Function GetIntegralToken(ByVal Value As ULong, ByVal Base As IntegerBase, ByVal TypeCharacter As LiteralTypeCharacters_Characters) As Token
        Dim case_type As BuiltInDataTypes
        'TODO: Check bounds of value 
        If TypeCharacter = LiteralTypeCharacters_Characters.None Then
            If Value > Integer.MaxValue Then
                case_type = BuiltInDataTypes.Long
            Else
                case_type = BuiltInDataTypes.Integer
            End If
        Else
            case_type = LiteralTypeCharacters.GetBuiltInType(TypeCharacter)
        End If

        Select Case case_type
            Case BuiltInDataTypes.Integer
                Return Token.CreateInt32Token(GetCurrentLocation, ExtractInt(Value, Base))
            Case BuiltInDataTypes.UInteger
                Return Token.CreateUInt32Token(GetCurrentLocation, ExtractUInt(Value, Base))
            Case BuiltInDataTypes.Long
                Return Token.CreateInt64Token(GetCurrentLocation, ExtractLong(Value, Base))
            Case BuiltInDataTypes.ULong
                Return Token.CreateUInt64Token(GetCurrentLocation, ExtractULong(Value, Base))
            Case BuiltInDataTypes.Short
                Return Token.CreateInt16Token(GetCurrentLocation, ExtractShort(Value, Base))
            Case BuiltInDataTypes.UShort
                Return Token.CreateUInt16Token(GetCurrentLocation, ExtractUShort(Value, Base))
            Case Else
                Throw New InternalException("")
        End Select
    End Function

    Private Function ExtractInt(ByVal Value As ULong, ByVal Base As IntegerBase) As Integer
        Select Case Base
            Case IntegerBase.Decimal
                Return CInt(Value)
            Case IntegerBase.Hex, IntegerBase.Octal
                If Value > Integer.MaxValue Then
                    Return CInt(Integer.MinValue + (CUInt(Value) - Integer.MaxValue - 1))
                Else
                    Return CInt(Value)
                End If
            Case Else
                Throw New InternalException("Unknown base: " & Base.ToString())
        End Select
    End Function

    Private Function ExtractUInt(ByVal Value As ULong, ByVal Base As IntegerBase) As UInteger
        Select Case Base
            Case IntegerBase.Decimal
                Return CUInt(Value)
            Case IntegerBase.Hex, IntegerBase.Octal
                Return CUInt(Value)
            Case Else
                Throw New InternalException("Unknown base: " & Base.ToString())
        End Select
    End Function

    Private Function ExtractShort(ByVal Value As ULong, ByVal Base As IntegerBase) As Short
        Select Case Base
            Case IntegerBase.Decimal
                Return CShort(Value)
            Case IntegerBase.Hex, IntegerBase.Octal
                If Value > Short.MaxValue Then
                    Return CShort(Short.MinValue + (CUShort(Value) - Short.MaxValue - 1))
                Else
                    Return CShort(Value)
                End If
            Case Else
                Throw New InternalException("Unknown base: " & Base.ToString())
        End Select
    End Function

    Private Function ExtractUShort(ByVal Value As ULong, ByVal Base As IntegerBase) As UShort
        Select Case Base
            Case IntegerBase.Decimal
                Return CUShort(Value)
            Case IntegerBase.Hex, IntegerBase.Octal
                Return CUShort(Value)
            Case Else
                Throw New InternalException("Unknown base: " & Base.ToString())
        End Select
    End Function

    Private Function ExtractLong(ByVal Value As ULong, ByVal Base As IntegerBase) As Long
        Select Case Base
            Case IntegerBase.Decimal
                Return CLng(Value)
            Case IntegerBase.Hex, IntegerBase.Octal
                If Value > Long.MaxValue Then
                    Return CLng(Long.MinValue + (Value - Long.MaxValue - 1))
                Else
                    Return CLng(Value)
                End If
            Case Else
                Throw New InternalException("Unknown base: " & Base.ToString())
        End Select
    End Function

    Private Function ExtractULong(ByVal Value As ULong, ByVal Base As IntegerBase) As ULong
        Select Case Base
            Case IntegerBase.Decimal
                Return CULng(Value)
            Case IntegerBase.Hex, IntegerBase.Octal
                Return CULng(Value)
            Case Else
                Throw New InternalException("Unknown base: " & Base.ToString())
        End Select
    End Function

    Function GetCurrentLocation() As Span
        Return New Span(m_CodeFileIndex, m_CurrentLine, m_CurrentColumn)
    End Function

    ReadOnly Property CurrentLocation() As Span
        Get
            Return GetCurrentLocation()
        End Get
    End Property

    Private ReadOnly Property CurrentChar() As Char
        Get
            Return m_CurrentChar
        End Get
    End Property

    Private Function NextChar() As Char
        If m_CurrentColumn < 255 Then m_CurrentColumn += CByte(1)
        m_TotalCharCount += 1

        m_PreviousChar = m_CurrentChar
        If m_PeekedChars.Count > 0 Then
            m_CurrentChar = m_PeekedChars.Dequeue
        Else
            If m_Reader.EndOfStream Then
                m_CurrentChar = nl0
            Else
                If m_Reader.EndOfStream Then
                    m_EndOfFile = True
                    m_CurrentChar = nl0
                Else
                    m_CurrentChar = Convert.ToChar(m_Reader.Read())
                End If
            End If
        End If

        Return m_CurrentChar
    End Function

    Private ReadOnly Property PreviousChar() As Char
        Get
            Return m_PreviousChar
        End Get
    End Property

    Private Function PeekChar() As Char
        If m_PeekedChars.Count = 0 Then
            If m_Reader.EndOfStream Then Return nl0
            m_PeekedChars.Enqueue(Convert.ToChar(m_Reader.Read))
        End If
        Return m_PeekedChars.Peek()
    End Function

    Private Function PeekChars(ByVal Chars As Integer) As Char
        Do Until m_PeekedChars.Count >= Chars
            If m_Reader.EndOfStream Then Return nlA
            m_PeekedChars.Enqueue(Convert.ToChar(m_Reader.Read))
        Loop
        Return m_PeekedChars.ToArray()(Chars - 1)
    End Function

    ''' <summary>
    ''' Returns true if the current character is the last character in the scanner.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsLastChar() As Boolean
        Return m_Reader.EndOfStream
    End Function

    ''' <summary>
    ''' Next line!
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub IncLine()
        m_CurrentLine += 1UI
        m_CurrentColumn = 1
        m_TokensSeenOnLine = 0
    End Sub

    ''' <summary>
    ''' Creates a new symbol token of the specified symbol.
    ''' </summary>
    ''' <param name="Symbol"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NewToken(ByVal Symbol As KS) As Token
        Return Token.CreateSymbolToken(GetCurrentLocation, Symbol)
    End Function

    Private Function GetNextToken() As Token
        Dim Result As Token = Nothing
        Do
            Select Case CurrentChar()
                Case nl27, nl201C, nl201D 'String Literal
                    Result = GetString()
                Case COMMENTCHAR1, COMMENTCHAR2, COMMENTCHAR3 'VB Comment
                    EatComment()
                Case nlD, nlA, nl2028, nl2029 'New line

                    'Keep the current line of the end of line token to the current line so we get better
                    'location info for errors and warnings
                    Result = Token.CreateEndOfLineToken(GetCurrentLocation)
                    EatNewLine()

                Case nl0 'End of file
                    Result = Token.CreateEndOfFileToken(GetCurrentLocation)
                Case ":"c ':
                    NextChar()
                    Result = NewToken(KS.Colon)
                Case ","c ',
                    NextChar()
                    Result = NewToken(KS.Comma)
                Case "."c
                    If PeekChar() >= "0"c AndAlso PeekChar() <= "9"c Then
                        Result = GetNumber()
                    Else
                        NextChar()
                        Result = NewToken(KS.Dot)
                    End If
                Case "0"c To "9"c
                    Result = GetNumber()
                Case "("c
                    NextChar()
                    Result = NewToken(KS.LParenthesis)
                Case ")"c
                    NextChar()
                    Result = NewToken(KS.RParenthesis)
                Case "["c
                    Result = GetEscapedIdentifier()
                Case "{"c
                    NextChar()
                    Result = NewToken(KS.LBrace)
                Case "}"c
                    NextChar()
                    Result = NewToken(KS.RBrace)
                Case ">"c
                    NextChar()
                    EatWhiteSpace()
                    'If CurrentChar() = "<"c Then
                    '    NextChar()
                    '    Result = NewToken(KS.NotEqual)
                    'Else
                    If CurrentChar() = "="c Then
                        NextChar()
                        Result = NewToken(KS.GE)
                    ElseIf CurrentChar() = ">"c Then
                        NextChar()
                        EatWhiteSpace()
                        If CurrentChar() = "="c Then
                            NextChar()
                            Result = NewToken(KS.ShiftRightAssign)
                        Else
                            Result = NewToken(KS.ShiftRight)
                        End If
                    Else
                        Result = NewToken(KS.GT)
                    End If
                Case "<"c
                    NextChar()
                    EatWhiteSpace()
                    If (CurrentChar() = ">"c) Then
                        NextChar()
                        Result = NewToken(KS.NotEqual)
                    ElseIf CurrentChar() = "="c Then
                        NextChar()
                        Result = NewToken(KS.LE)
                    ElseIf CurrentChar() = "<"c Then
                        NextChar()
                        EatWhiteSpace()
                        If CurrentChar() = "="c Then
                            NextChar()
                            Result = NewToken(KS.ShiftLeftAssign)
                        Else
                            Result = NewToken(KS.ShiftLeft)
                        End If
                    Else
                        Result = NewToken(KS.LT)
                    End If
                Case "="c
                    NextChar()
                    Result = NewToken(KS.Equals)
                Case "!"c
                    NextChar()
                    Result = NewToken(KS.Exclamation)
                Case "?"c
                    NextChar()
                    Result = NewToken(KS.Interrogation)
                Case "&"c
                    Select Case PeekChar()
#If EXTENDED Then
                        Case "b"c, "B"c, "h"c, "H"c, "o"c, "O"c, "d"c, "D"c
#Else
                        Case "h"c, "H"c, "o"c, "O"c, "d"c, "D"c
#End If
                            Result = GetNumber()
                        Case Else 'Not a number, but operator
                            NextChar()
                            EatWhiteSpace()
                            If CurrentChar() = "="c Then
                                Result = NewToken(KS.ConcatAssign)
                                NextChar()
                            Else
                                Result = NewToken(KS.Concat)
                            End If
                    End Select
                Case "*"c
                    NextChar()
                    EatWhiteSpace()
                    If (CurrentChar() = "="c) Then
                        NextChar()
                        Result = NewToken(KS.MultAssign)
                    Else
                        Result = NewToken(KS.Mult)
                    End If
                Case "+"c
                    NextChar()
                    EatWhiteSpace()
                    If (CurrentChar() = "="c) Then
                        NextChar()
                        Result = NewToken(KS.AddAssign)
                    Else
                        Result = NewToken(KS.Add)
                    End If
                Case "-"c
                    NextChar()
                    EatWhiteSpace()
                    If (CurrentChar() = "="c) Then
                        NextChar()
                        Result = NewToken(KS.MinusAssign)
                    Else
                        Result = NewToken(KS.Minus)
                    End If
                Case "^"c
                    NextChar()
                    EatWhiteSpace()
                    If (CurrentChar() = "="c) Then
                        NextChar()
                        Result = NewToken(KS.PowerAssign)
                    Else
                        Result = NewToken(KS.Power)
                    End If
                Case "\"c
                    NextChar()
                    EatWhiteSpace()
                    If (CurrentChar() = "="c) Then
                        NextChar()
                        Result = NewToken(KS.IntDivAssign)
                    Else
                        Result = NewToken(KS.IntDivision)
                    End If
                Case "#"c
                    'Type characters are already scanned when they appear after a literal. 
                    'If scanning gets here, it is not a type character.
                    If m_TokensSeenOnLine = 0 Then
                        Result = NewToken(KS.Numeral)
                        NextChar()
                    Else
                        Result = GetDate()
                    End If
                Case "/"c
                    NextChar()
                    EatWhiteSpace()
                    If (CurrentChar() = "="c) Then
                        NextChar()
                        Result = NewToken(KS.RealDivAssign)
                    Else
                        Result = NewToken(KS.RealDivision)
                    End If
                Case " "c 'Space
                    NextChar()
                    If (CurrentChar() = "_"c) Then '
                        Dim i As Integer = 1
                        Do While IsWhiteSpace(PeekChars(i))
                            i += 1
                        Loop
                        If IsNewLine(PeekChars(i)) Then
                            NextChar()
                            EatWhiteSpace()
                            EatNewLine()
                        End If
                    End If
                Case nlTab ' Tab character
                    NextChar()
                Case Else
                    If IsWhiteSpace() Then
                        NextChar()
                    ElseIf CanStartIdentifier() Then
                        Result = GetIdentifier()
                        If Result.IsKeyword AndAlso Result.Equals(KS.[REM]) Then
                            EatLine(False)
                            Result = Nothing
                        End If
                    Else
                        Compiler.Report.ShowMessage(Messages.VBNC30037, GetCurrentLocation())
                        EatLine(False)
                    End If
            End Select
        Loop While Token.IsSomething(Result) = False

        If Result.IsEndOfLine = False Then
            m_TokensSeenOnLine += 1
        Else
            m_TokensSeenOnLine = 0
        End If

        Return Result
    End Function

    Public Sub New(ByVal Compiler As Compiler)
        MyBase.New(Compiler)
        m_Files = New Generic.Queue(Of CodeFile)(Compiler.CommandLine.Files)
        NextFile()
        LoadProjectConstants()
    End Sub

    Public Sub New(ByVal Compiler As Compiler, ByVal Code As String)
        MyBase.New(Compiler)
        m_Files = New Generic.Queue(Of CodeFile)()
        Dim cf As New CodeFile("<Internal>", "", Compiler, Code)
        m_Files.Enqueue(cf)
        Compiler.CommandLine.Files.Add(cf)
        NextFile()
        LoadProjectConstants()
    End Sub

    Private Sub NextFile()
        m_TotalLineCount += m_CurrentLine
        'm_TotalCharCount += m_Code.Length

        m_CurrentLine = 1
        m_CurrentColumn = 1
        m_TokensSeenOnLine = 0
        m_CurrentChar = Nothing
        m_PreviousChar = Nothing
        m_EndOfFile = False
        m_PeekedChars.Clear()

        If m_Files.Count > 0 Then
            m_CodeFile = m_Files.Dequeue()
            m_CodeFileIndex = CUShort(Compiler.CommandLine.Files.IndexOf(m_CodeFile))
            m_Reader = m_CodeFile.CodeStream
            NextChar()
        Else
            m_CodeFile = Nothing
            'm_Code = Nothing
            m_Reader = Nothing
        End If
    End Sub

    Public Sub NextUnconditionally()
        Dim lastTokenType As TokenType
        Dim lastKS As KS

        If m_Peeked.HasValue Then
            m_Current = m_Peeked.Value
            m_CurrentData = m_Current.m_TokenType
            m_Peeked = Nothing
            Return
        End If

        If Token.IsSomething(m_Current) AndAlso m_Current.IsEndOfFile Then
            NextFile()
        End If

        If m_CodeFile Is Nothing Then
            m_Current = Token.CreateEndOfCodeToken
            Return
        End If

        lastTokenType = m_Current.m_TokenType
        If lastTokenType = TokenType.Symbol Then
            lastKS = DirectCast(m_CurrentData, KS)
        End If

        m_CurrentTypeCharacter = TypeCharacters.Characters.None
        m_Current = GetNextToken()

        m_CurrentData = m_Current.m_TokenObject

        If m_Current.m_TokenType = TokenType.EndOfLine Then
            If lastTokenType = TokenType.Symbol AndAlso (lastKS = KS.Comma OrElse lastKS = KS.LParenthesis OrElse lastKS = KS.LBrace) Then
                m_CurrentTypeCharacter = TypeCharacters.Characters.None
                m_Current = GetNextToken()
                m_CurrentData = m_Current.m_TokenObject
            Else
                m_Peeked = GetNextToken()

                If m_Peeked.Value.IsSymbol AndAlso (m_Peeked.Value.Symbol = KS.RParenthesis OrElse m_Peeked.Value.Symbol = KS.RBrace) Then
                    NextUnconditionally()
                End If
            End If
        End If
    End Sub

    Public ReadOnly Property Current() As Token
        Get
            Return m_Current
        End Get
    End Property

    Public ReadOnly Property CurrentTypeCharacter() As TypeCharacters.Characters
        Get
            Return m_CurrentTypeCharacter
        End Get
    End Property

    Public ReadOnly Property GetLocation() As Span
        Get
            Return Me.GetCurrentLocation
        End Get
    End Property

    Public ReadOnly Property TokenData() As Object
        Get
            Return m_CurrentData
        End Get
    End Property
End Class

