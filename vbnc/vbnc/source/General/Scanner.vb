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

#Const SUPPORT_CSTYLE_COMMENTS = 1
#If DEBUG Then
#Const DOEOFCHECK = 0
#Const EXTENDEDDEBUG = 0
#End If
#Const EXTENDED = False

Public Class Scanner
    Implements ITokenReader

    ''' <summary>
    ''' The total number of lines scanned.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_TotalLineCount As Integer

    ''' <summary>
    ''' The total number of characters scanned.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_TotalCharCount As Integer

    ''' <summary>
    ''' The current line.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_CurrentLine As Integer

    ''' <summary>
    ''' The current column.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_CurrentColumn As Integer

    '''' <summary>
    '''' The code.
    '''' </summary>
    '''' <remarks></remarks>
    'Private m_Code As String = ""

    ''' <summary>
    ''' The current code file.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_CodeFile As CodeFile

    Private m_PreviousChar As Char
    Private m_CurrentChar As Char
    Private m_PeekedChars As New Generic.Queue(Of Char)
    Private m_Reader As System.IO.StreamReader
    Private m_Builder As New System.Text.StringBuilder

    ''' <summary>
    ''' If any tokens has been found on this line.
    ''' Reset by IncLine, set by NewToken
    ''' </summary>
    ''' <remarks></remarks>
    Private m_TokensSeenOnLine As Boolean

    Private m_Files As Generic.Queue(Of CodeFile)

    Private m_Compiler As Compiler

    Private m_Peeked As Token
    Private m_PeekedExact As Token
    Private m_Current As Token

    'Useful constants.
    Private Const nl0 As Char = Microsoft.VisualBasic.ChrW(0)
    Private Const nlA As Char = Microsoft.VisualBasic.ChrW(&HA)
    Private Const nlD As Char = Microsoft.VisualBasic.ChrW(&HD)
    Private Const nl2028 As Char = Microsoft.VisualBasic.ChrW(&H2028)
    Private Const nl2029 As Char = Microsoft.VisualBasic.ChrW(&H2029)
    Private Const nlTab As Char = Microsoft.VisualBasic.ChrW(9)

    Private Const COMMENTCHAR1 As Char = "'"c
    Private Const COMMENTCHAR2 As Char = Microsoft.VisualBasic.ChrW(&H2018)
    Private Const COMMENTCHAR3 As Char = Microsoft.VisualBasic.ChrW(&H2019)

    ReadOnly Property TotalLineCount() As Integer
        Get
            Return m_TotalLineCount
        End Get
    End Property

    ReadOnly Property TotalCharCount() As Integer
        Get
            Return m_TotalCharCount
        End Get
    End Property

    ReadOnly Property Compiler() As Compiler
        Get
            Return m_Compiler
        End Get
    End Property

    Private Function IsNewLine() As Boolean
        Return IsNewLine(CurrentChar)
    End Function

    Private Function IsNewLine(ByVal chr As Char) As Boolean
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

    Private Function IsWhiteSpace(ByVal chr As Char) As Boolean
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

        Dim ch As Char
        Do
            ch = NextChar()
        Loop Until IsNewLine(ch)

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
            Case Else
                Throw New InternalException("Current character is not a new line.")
        End Select
    End Sub

    Private Sub EatComment()
        Select Case CurrentChar()
            Case COMMENTCHAR1, COMMENTCHAR2, COMMENTCHAR3 'Traditional VB comment
                EatLine(False) 'do not eat newline, it needs to be added as a token
                Return
#If SUPPORT_CSTYLE_COMMENTS Then
            Case "/"c 'C-style comment
                NextChar()
                Select Case CurrentChar()
                    Case "/"c 'Single line comment
                        EatLine(False) 'do not eat newline, it needs to be added as a token
                        Return
                    Case "*"c 'Nestable, multiline comment.
                        Dim iNesting As Integer = 1
                        NextChar()
                        Do
                            Select Case CurrentChar()
                                Case "*"c
                                    If PeekChar() = "/"c Then
                                        'End of comment found (if iNesting is 0)
                                        NextChar()
                                        NextChar()
                                        iNesting -= 1
                                    Else
                                        NextChar()
                                    End If
                                Case "/"c
                                    If PeekChar() = "*"c Then
                                        'a nested comment was found
                                        NextChar()
                                        iNesting += 1
                                    ElseIf PeekChar() = "/"c Then
                                        EatLine(True)
                                    Else
                                        NextChar()
                                    End If
                                Case nl0
                                    Compiler.Report.ShowMessage(Messages.VBNC90022)
                                    Return
                                Case Else
                                    If IsNewLine() Then
                                        EatNewLine() 'To update the line variable
                                    Else
                                        NextChar()
                                    End If
                            End Select
                        Loop While (iNesting <> 0)
                    Case Else
                        'Function should never be called if not a comment
                        Throw New InternalException("EatComment called with no comment.")
                End Select
#End If
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

    Private Function GetDate() As DateLiteralToken
        Helper.Assert(CurrentChar() = "#"c, "GetDate called without a date!")

        EatWhiteSpace()

        Dim Count As Integer
        'Date value
        Dim bCont As Boolean = True
        m_Builder.Length = 0
        Do
            Count += 1
            Dim ch As Char = NextChar()
            If (IsNewLine()) Then
                Compiler.Report.ShowMessage(Messages.VBNC90000)
                bCont = False
            Else
                Select Case ch
                    Case nl0
                        Compiler.Report.ShowMessage(Messages.VBNC90001)
                        bCont = False
                    Case "#"c
                        NextChar() 'The ending #
                        bCont = False
                End Select
            End If
            If bCont Then m_Builder.Append(ch)
        Loop While bCont

        Return New DateLiteralToken(GetCurrentLocation, CDate(m_Builder.ToString), Compiler)
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
            Helper.AddError()
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
        m_Builder.Length = 0

        ch = CurrentChar()
        m_Builder.Append(ch)
        If IsAlphaCharacter(ch) Then
            bValid = True
        ElseIf IsUnderscoreCharacter(ch) Then
            ch = NextChar()
            m_Builder.Append(ch)
            bValid = IsIdentifierCharacter(ch)
        End If

        If Not bValid Then
            Compiler.Report.ShowMessage(Messages.VBNC30203, Me.GetCurrentLocation(), CStr(ch))
            Return Nothing
        Else
            Do While IsIdentifierCharacter(NextChar)
                m_Builder.Append(CurrentChar)
            Loop
        End If

        Dim strIdent As String = m_Builder.ToString()

        'The type character ! presents a special problem in that it can be used both as a type character and 
        'as a separator in the language. To remove ambiguity, a ! character is a type character as long as 
        'the character that follows it cannot start an identifier. If it can, then the ! character is a separator, 
        'not a type character.
        Dim typecharacter As TypeCharacters.Characters
        Dim canstartidentifier As Boolean = Me.IsLastChar = False AndAlso (IsAlphaCharacter(PeekChar) OrElse IsUnderscoreCharacter(PeekChar))
        If TypeCharacters.IsTypeCharacter(CurrentChar, typecharacter) AndAlso (canstartidentifier = False OrElse typecharacter <> TypeCharacters.Characters.SingleTypeCharacter) Then
            NextChar()
            Return New IdentifierToken(GetCurrentLocation, strIdent, typecharacter, Escaped, Compiler)
        Else
            Dim keyword As KS
            If Escaped = False AndAlso KeywordToken.IsKeyword(strIdent, keyword) Then
                Return New KeywordToken(GetCurrentLocation, keyword, Compiler)
            Else
                Return New IdentifierToken(GetCurrentLocation, strIdent, typecharacter, Escaped, Compiler)
            End If
        End If
    End Function

    Private Function GetString() As Token
        Dim bEndOfString As Boolean = False
        m_Builder.Length = 0
        Do
            Select Case NextChar()
                Case """"c '
                    'If " followed by a ", output one "
                    If NextChar() = """" Then
                        m_Builder.Append("""")
                    Else
                        bEndOfString = True
                    End If
                Case nlA, nlD, nl2028, nl2029
                    'vbc accepts this...
                    Compiler.Report.ShowMessage(Messages.VBNC90003)
                    EatNewLine()
                    bEndOfString = True
                Case nl0
                    ' End of file
                    Compiler.Report.ShowMessage(Messages.VBNC90004)
                    'PreviousChar() 'Step back
                    bEndOfString = True
                Case Else
                    m_Builder.Append(CurrentChar())
            End Select
        Loop While bEndOfString = False
        If CurrentChar() = "C"c OrElse CurrentChar() = "c"c Then
            'Is a char type character
            NextChar()
            If m_Builder.Length <> 1 Then
                Compiler.Report.ShowMessage(Messages.VBNC30004)
                Return New StringLiteralToken(GetCurrentLocation, m_Builder.ToString, Compiler)
            Else
                Return New CharLiteralToken(GetCurrentLocation, CChar(m_Builder.ToString), Compiler)
            End If
        Else
            Return New StringLiteralToken(GetCurrentLocation, m_Builder.ToString, Compiler)
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
                        Throw New InternalException(GetCurrentLocation.ToString) 'Should never get here, this function should only be called with the correct specifiers.
                End Select
                NextChar()
            Case Else
                Throw New InternalException("Invalid character: " & CurrentChar.ToString & ", Location: " & GetCurrentLocation.ToString)
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
                Throw New InternalException(GetCurrentLocation.ToString)
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
                            GetNumber = New DecimalLiteralToken(GetCurrentLocation, Decimal.Parse(strResult, Globalization.NumberStyles.AllowDecimalPoint Or Globalization.NumberStyles.AllowExponent), Compiler, typeCharacter)
                        Case BuiltInDataTypes.Double
                            GetNumber = New FloatingPointLiteralToken(Of Double)(GetCurrentLocation, Double.Parse(strResult), BuiltInDataTypes.Double, Compiler, typeCharacter)
                        Case BuiltInDataTypes.Single
                            GetNumber = New FloatingPointLiteralToken(Of Single)(GetCurrentLocation, Single.Parse(strResult), BuiltInDataTypes.Single, Compiler, typeCharacter)
                        Case BuiltInDataTypes.Integer, BuiltInDataTypes.Long, BuiltInDataTypes.Short, BuiltInDataTypes.UInteger, BuiltInDataTypes.ULong, BuiltInDataTypes.UShort
                            If bReal Then
                                Compiler.Report.ShowMessage(Messages.VBNC90002, typeCharacter.ToString)
                                IntegerValue = 0
                            Else
                                'Try to parse the result
                                IntegerValue = ULong.Parse(strResult)
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
                            If bOutOfRange Then
                                Compiler.Report.ShowMessage(Messages.VBNC30439, typeCharacter.ToString)
                            End If
                            GetNumber = GetIntegralToken(ULong.Parse(strResult), Base, typeCharacter)
                        Case Else
                            Compiler.Report.ShowMessage(Messages.VBNC90002, typeCharacter.ToString)
                            GetNumber = New FloatingPointLiteralToken(Of Double)(GetCurrentLocation, 0, BuiltInDataTypes.Double, Compiler, LiteralTypeCharacters_Characters.None)
                    End Select
                Catch ex As System.OverflowException
                    Compiler.Report.ShowMessage(Messages.VBNC30036)
                    GetNumber = New FloatingPointLiteralToken(Of Double)(GetCurrentLocation, 0, BuiltInDataTypes.Double, Compiler, LiteralTypeCharacters_Characters.None)
                Catch ex As Exception
                    Compiler.Report.ShowMessage(Messages.VBNC90005)
                    GetNumber = New FloatingPointLiteralToken(Of Double)(GetCurrentLocation, 0, BuiltInDataTypes.Double, Compiler, LiteralTypeCharacters_Characters.None)
                End Try
#If EXTENDED Then
            Case IntegerBase.Binary
                Try
                    IntegerValue = Helper.BinToInt(strResult)
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
                    Compiler.Report.ShowMessage(Messages.VBNC90006, "octal")
                End Try
                GetNumber = GetIntegralToken(IntegerValue, Base, typeCharacter)
            Case Else
                Throw New InternalException(GetCurrentLocation.ToString)
        End Select
    End Function

    Private Function GetIntegralToken(ByVal Value As ULong, ByVal Base As IntegerBase, ByVal TypeCharacter As LiteralTypeCharacters_Characters) As Token
        'TODO: Check bounds of value 
        If TypeCharacter = LiteralTypeCharacters_Characters.None Then
            GoTo integertype
        Else
            Select Case LiteralTypeCharacters.GetBuiltInType(TypeCharacter)
                Case BuiltInDataTypes.Integer
integertype:
                    Return New IntegralLiteralToken(Of Integer)(GetCurrentLocation, CInt(Value), Base, TypeCharacter, Compiler)
                Case BuiltInDataTypes.UInteger
                    Return New IntegralLiteralToken(Of UInteger)(GetCurrentLocation, CUInt(Value), Base, TypeCharacter, Compiler)
                Case BuiltInDataTypes.Long
                    Return New IntegralLiteralToken(Of Long)(GetCurrentLocation, CLng(Value), Base, TypeCharacter, Compiler)
                Case BuiltInDataTypes.ULong
                    Return New IntegralLiteralToken(Of ULong)(GetCurrentLocation, CULng(Value), Base, TypeCharacter, Compiler)
                Case BuiltInDataTypes.Short
                    Return New IntegralLiteralToken(Of Short)(GetCurrentLocation, CShort(Value), Base, TypeCharacter, Compiler)
                Case BuiltInDataTypes.UShort
                    Return New IntegralLiteralToken(Of UShort)(GetCurrentLocation, CUShort(Value), Base, TypeCharacter, Compiler)
                Case Else
                    Throw New InternalException("")
            End Select
        End If
    End Function
    Private Function GetCurrentLocation() As Span
        Return New Span(m_CodeFile, m_CurrentLine, m_CurrentColumn)
    End Function

    ''' <summary>
    ''' Converts the specified string to the specified typecode
    ''' </summary>
    ''' <param name="Value"></param>
    ''' <param name="Type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ConvertTo(ByVal Value As String, ByVal Type As TypeCode) As Object
        Select Case Type
            Case TypeCode.Boolean
                Return CBool(Value)
            Case TypeCode.Byte
                Return CByte(Value)
            Case TypeCode.Char
                Return CChar(Value)
            Case TypeCode.DateTime
                Return CDate(Value)
            Case TypeCode.DBNull
                Throw New InternalException("")
            Case TypeCode.Decimal
                Return Convert.ToDecimal(Value, vbnc.Helper.USCulture)
            Case TypeCode.Double
                Return CDbl(Value)
            Case TypeCode.Empty
                Throw New InternalException("")
            Case TypeCode.Int16
                Return CShort(Value)
            Case TypeCode.Int32
                Return CInt(Value)
            Case TypeCode.Int64
                Return CLng(Value)
            Case TypeCode.Object
                Throw New InternalException("")
            Case TypeCode.[SByte]
                Return CSByte(Value)
            Case TypeCode.Single
                Return CSng(Value)
            Case TypeCode.String
                Return CStr(Value)
            Case TypeCode.UInt16
                Dim result As UInt16
                If UInt16.TryParse(Value, result) Then
                    Return result
                Else
                    Compiler.Report.ShowMessage(Messages.VBNC30748, Value, KS.UShort.ToString)
                    Return 0
                End If
            Case TypeCode.UInt32
                Dim result As UInt32
                If UInt32.TryParse(Value, result) Then
                    Return result
                Else
                    Compiler.Report.ShowMessage(Messages.VBNC30748, Value, KS.UInteger.ToString)
                    Return 0
                End If
            Case TypeCode.UInt64
                Dim result As UInt64
                If UInt64.TryParse(Value, result) Then
                    Return result
                Else
                    Compiler.Report.ShowMessage(Messages.VBNC30748, Value, KS.ULong.ToString)
                    Return 0
                End If
            Case Else
                Throw New InternalException("")
        End Select
    End Function

    Private ReadOnly Property CurrentChar() As Char
        Get
            Return m_CurrentChar
        End Get
    End Property

    Private Function NextChar() As Char
        m_CurrentColumn += 1
        m_TotalCharCount += 1

        m_PreviousChar = m_CurrentChar
        If m_PeekedChars.Count > 0 Then
            m_CurrentChar = m_PeekedChars.Dequeue
        Else
            If m_Reader.EndOfStream Then
                m_CurrentChar = nl0
            Else
                m_CurrentChar = Convert.ToChar(m_Reader.Read())
            End If
        End If

        Return m_CurrentChar
    End Function

    Private ReadOnly Property PreviousChar() As Char
        Get
            Return m_PreviousChar
        End Get
    End Property

    'Private Function PreviousChar(Optional ByVal Retrocede As Boolean = True) As Char
    '    m_CurrentPosition -= 1
    '    m_CurrentColumn -= 1
    '    PreviousChar = CurrentChar()
    '    If Not Retrocede Then
    '        m_CurrentPosition += 1
    '    End If
    'End Function

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
        m_CurrentLine += 1
        m_CurrentColumn = 1
        m_TokensSeenOnLine = False
    End Sub

    ''' <summary>
    ''' Creates a new symbol token of the specified symbol.
    ''' </summary>
    ''' <param name="Symbol"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NewToken(ByVal Symbol As KS) As Token
        Return New SymbolToken(GetCurrentLocation, Symbol, Compiler)
    End Function

    '''' <summary>
    '''' Add the endofcode token to the token stream.
    '''' </summary>
    '''' <param name="Tokens"></param>
    '''' <remarks></remarks>
    'Public Sub SetCodeEnd(ByVal Tokens As Tokens)
    '    m_Tokens = Tokens

    '    '#If DEBUG Then
    '    '        CheckEndOfFiles()
    '    '#End If
    '    '        SetMultiKeywords()
    '    '#If DEBUG Then
    '    '        CheckEndOfFiles()
    '    '#End If
    '    m_Tokens.StartAgain()

    '    m_Tokens.Add(New EndOfCodeToken(Compiler))
    '    m_Tokens = Nothing
    'End Sub

    Private Function GetNextToken() As Token
        Dim Result As Token = Nothing
        Do
            Select Case CurrentChar()
                Case """"c 'String Literal
                    Result = GetString()
                Case COMMENTCHAR1, COMMENTCHAR2, COMMENTCHAR3 'VB Comment
                    EatComment()
                Case nlD, nlA, nl2028, nl2029 'New line
                    EatNewLine()
                    Result = New EndOfLineToken(GetCurrentLocation, Compiler)
                Case nl0 'End of file
                    Result = New EndOfFileToken(GetCurrentLocation, Compiler)
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
                    If IsNewLine(PreviousChar) Then
                        Result = NewToken(KS.Numeral)
                        NextChar()
                    Else
                        Result = GetDate()
                    End If
                Case "/"c
#If SUPPORT_CSTYLE_COMMENTS Then
                    If (PeekChar() = "/"c OrElse PeekChar() = "*"c) Then 'Comment
                        EatComment()
                    Else 'Division
#End If
                        NextChar()
                        EatWhiteSpace()
                        If (CurrentChar() = "="c) Then
                            NextChar()
                            Result = NewToken(KS.RealDivAssign)
                        Else
                            Result = NewToken(KS.RealDivision)
                        End If
#If SUPPORT_CSTYLE_COMMENTS Then
                    End If
#End If
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
                        If Result.IsKeyword AndAlso DirectCast(Result, KeywordToken).Equals(KS.[REM]) Then
                            EatLine(False)
                            Result = Nothing
                        End If
                    Else
                        Compiler.Report.ShowMessage(Messages.VBNC30037)
                        NextChar()
                    End If
            End Select
        Loop While Result Is Nothing
        Return Result
    End Function

    Function SetMultiKeywords(ByVal current As Token) As Token
        Dim peeked As Token

        If current.Equals(KS.ConditionalEnd) Then
            peeked = Me.PeekExactToken()

            If peeked.Equals(KS.If) Then
                peeked = Me.NextExactToken
                Return New KeywordToken(New Span(current.Location, peeked.Location), KS.ConditionalEndIf, Compiler)
            End If

            If Not peeked.IsIdentifier Then Return current

            If peeked.AsIdentifier.Equals("Region") Then
                peeked = Me.NextExactToken
                Return New KeywordToken(New Span(current.Location, peeked.Location), KS.ConditionalEndRegion, Compiler)
            ElseIf peeked.AsIdentifier.Equals("ExternalSource") Then
                peeked = Me.NextExactToken
                Return New KeywordToken(New Span(current.Location, peeked.Location), KS.ConditionalEndExternalSource, Compiler)
            Else
                Return current
            End If
        End If

        If current.Equals(KS.End) Then
            peeked = Me.PeekExactToken()
            If Not peeked.IsKeyword Then Return current

            Dim attrib As KSEnumStringAttribute
            attrib = Enums.GetKSStringAttribute(peeked.AsKeyword.Keyword)
            If Not attrib.IsMultiKeyword Then Return current

            peeked = Me.NextExactToken
            Return New KeywordToken(New Span(current.Location, peeked.Location), attrib.MultiKeyword, Compiler)
        End If

        If current.Equals(KS.Numeral) AndAlso Not Me.m_TokensSeenOnLine Then
            peeked = Me.PeekExactToken
            If peeked.IsKeyword Then
                Select Case peeked.AsKeyword.Keyword
                    Case KS.If
                        peeked = Me.NextExactToken
                        Return New KeywordToken(New Span(current.Location, peeked.Location), KS.ConditionalIf, Compiler)
                    Case KS.Else
                        peeked = Me.NextExactToken
                        Return New KeywordToken(New Span(current.Location, peeked.Location), KS.ConditionalElse, Compiler)
                    Case KS.ElseIf
                        peeked = Me.NextExactToken
                        Return New KeywordToken(New Span(current.Location, peeked.Location), KS.ConditionalElseIf, Compiler)
                    Case KS.Const
                        peeked = Me.NextExactToken
                        Return SetMultiKeywords(New KeywordToken(New Span(current.Location, peeked.Location), KS.ConditionalConst, Compiler))
                    Case KS.End
                        peeked = Me.NextExactToken
                        Return SetMultiKeywords(New KeywordToken(New Span(current.Location, peeked.Location), KS.ConditionalEnd, Compiler))
                    Case KS.End_If
                        peeked = Me.NextExactToken
                        Return New KeywordToken(New Span(current.Location, peeked.Location), KS.ConditionalEndIf, Compiler)
                    Case Else
                        Return current
                End Select
            ElseIf peeked.IsIdentifier Then
                If peeked.AsIdentifier.Equals("Region") Then
                    peeked = Me.NextExactToken
                    Return New KeywordToken(New Span(current.Location, peeked.Location), KS.ConditionalRegion, Compiler)
                ElseIf peeked.AsIdentifier.Equals("ExternalSource") Then
                    peeked = Me.NextExactToken
                    Return New KeywordToken(New Span(current.Location, peeked.Location), KS.ConditionalExternalSource, Compiler)
                Else
                    Return current
                End If
            End If
        End If

        If current.IsIdentifier AndAlso current.AsIdentifier.Equals("Custom") Then
            If Not Me.PeekExactToken.Equals(KS.Event) Then Return current
            peeked = Me.Next()
            Return New KeywordToken(New Span(current.Location, peeked.Location), KS.CustomEvent, Compiler)
        End If

        Return current
    End Function

    Public Sub New(ByVal Compiler As Compiler)
        m_Compiler = Compiler
        m_Files = New Generic.Queue(Of CodeFile)(m_Compiler.CommandLine.Files)
        NextFile()
    End Sub

    Private Sub NextFile()
        m_TotalLineCount += m_CurrentLine
        'm_TotalCharCount += m_Code.Length

        m_CurrentLine = 1
        m_CurrentColumn = 1
        m_TokensSeenOnLine = False
        m_CurrentChar = Nothing
        m_PreviousChar = Nothing
        m_PeekedChars.Clear()

        If m_Files.Count > 0 Then
            m_CodeFile = m_Files.Dequeue()
            'm_Code = m_CodeFile.Code
            m_Reader = m_CodeFile.CodeStream
            NextChar()
        Else
            m_CodeFile = Nothing
            'm_Code = Nothing
            m_Reader = Nothing
        End If
    End Sub

    Private Function NextExactToken() As Token
        Dim result As Token

        If m_PeekedExact IsNot Nothing Then
            result = m_PeekedExact
            m_PeekedExact = Nothing
            Return result
        End If

        Return GetNextToken()
    End Function

    Private Function PeekExactToken() As Token
        If m_PeekedExact Is Nothing Then
            m_PeekedExact = NextExactToken()
        End If

        Return m_PeekedExact
    End Function

    Public Function [Next]() As Token Implements ITokenReader.Next
        Dim result As Token

        If m_Peeked IsNot Nothing Then
            m_Current = m_Peeked
            m_Peeked = Nothing
            Return m_Current
        End If

        If m_CodeFile Is Nothing Then
            result = New EndOfCodeToken(m_Compiler)
            m_Current = result
            Return result
        End If

        result = NextExactToken()

        'Console.WriteLine("Scanned token: " & result.FriendlyString())

        result = SetMultiKeywords(result)

        If result.IsEndOfLine(True) Then
            Do While PeekExactToken().IsEndOfLine(True)
                result = Me.NextExactToken() 'Eat all posterior newlines
            Loop
        End If

        If result.IsEndOfFile() Then
            If m_Current IsNot Nothing AndAlso Not m_Current.IsEndOfLine(True) Then
                m_Peeked = result
                result = New EndOfLineToken(m_Peeked.Location, Compiler)
            End If
            NextFile()
        End If

        m_Current = result

        'Console.WriteLine("Returning token: " & result.FriendlyString)

        Return result
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

