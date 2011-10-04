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

Public Structure Token
    Public m_TokenType As TokenType
    Public m_TokenObject As Object
    Public m_Location As Span

    Shared Function IsSomething(ByVal Token As Token) As Boolean
        'Return Token IsNot Nothing AndAlso Token.IsSomething
        Return Token.IsSomething
    End Function

    Function IsSomething() As Boolean
        Return m_TokenType <> TokenType.None
    End Function

    Public Overrides Function ToString() As String
        If Me.IsIdentifier Then
            Return Me.Identifier
        ElseIf Me.IsKeyword Then
            Return Me.Identifier
        Else
            Return "<Token>"
        End If
    End Function

    Shared Function CreateIdentifierToken(ByVal Location As Span, ByVal Identifier As String) As Token
        Dim result As New Token(Location)
        result.m_TokenType = TokenType.Identifier
        result.m_TokenObject = Identifier
        Return result
    End Function

    Shared Function CreateEndOfCodeToken() As Token
        Dim result As New Token(Span.CommandLineSpan)
        result.m_TokenType = TokenType.EndOfCode
        Return result
    End Function

    Shared Function CreateEndOfLineToken(ByVal Location As Span) As Token
        Dim result As New Token(Location)
        result.m_TokenType = TokenType.EndOfLine
        Return result
    End Function

    Shared Function CreateEndOfFileToken(ByVal Location As Span) As Token
        Dim result As New Token(Location)
        result.m_TokenType = TokenType.EndOfFile
        Return result
    End Function

    Shared Function CreateKeywordToken(ByVal Location As Span, ByVal Keyword As KS) As Token
        Dim result As New Token(Location)
        result.m_TokenType = TokenType.Keyword
        result.m_TokenObject = Keyword
        Return result
    End Function

    Shared Function CreateDateToken(ByVal Location As Span, ByVal Value As Date) As Token
        Dim result As New Token(Location)
        result.m_TokenType = TokenType.DateLiteral
        result.m_TokenObject = Value
        Return result
    End Function

    Shared Function CreateCharToken(ByVal Location As Span, ByVal Value As Char) As Token
        Dim result As New Token(Location)
        result.m_TokenType = TokenType.CharLiteral
        result.m_TokenObject = Value
        Return result
    End Function

    Shared Function CreateDecimalToken(ByVal Location As Span, ByVal Value As Decimal) As Token
        Dim result As New Token(Location)
        result.m_TokenType = TokenType.DecimalLiteral
        result.m_TokenObject = Value
        Return result
    End Function

    Shared Function CreateSingleToken(ByVal Location As Span, ByVal Value As Single) As Token
        Dim result As New Token(Location)
        result.m_TokenType = TokenType.SingleLiteral
        result.m_TokenObject = Value
        Return result
    End Function

    Shared Function CreateDoubleToken(ByVal Location As Span, ByVal Value As Double) As Token
        Dim result As New Token(Location)
        result.m_TokenType = TokenType.DoubleLiteral
        result.m_TokenObject = Value
        Return result
    End Function

    Shared Function CreateInt16Token(ByVal Location As Span, ByVal Value As Short) As Token
        Dim result As New Token(Location)
        result.m_TokenType = TokenType.Int16Literal
        result.m_TokenObject = Value
        Return result
    End Function

    Shared Function CreateInt32Token(ByVal Location As Span, ByVal Value As Integer) As Token
        Dim result As New Token(Location)
        result.m_TokenType = TokenType.Int32Literal
        result.m_TokenObject = Value
        Return result
    End Function

    Shared Function CreateInt64Token(ByVal Location As Span, ByVal Value As Long) As Token
        Dim result As New Token(Location)
        result.m_TokenType = TokenType.Int64Literal
        result.m_TokenObject = Value
        Return result
    End Function

    Shared Function CreateUInt16Token(ByVal Location As Span, ByVal Value As UShort) As Token
        Dim result As New Token(Location)
        result.m_TokenType = TokenType.UInt16Literal
        result.m_TokenObject = Value
        Return result
    End Function

    Shared Function CreateUInt32Token(ByVal Location As Span, ByVal Value As UInteger) As Token
        Dim result As New Token(Location)
        result.m_TokenType = TokenType.UInt32Literal
        result.m_TokenObject = Value
        Return result
    End Function

    Shared Function CreateUInt64Token(ByVal Location As Span, ByVal Value As ULong) As Token
        Dim result As New Token(Location)
        result.m_TokenType = TokenType.UInt64Literal
        result.m_TokenObject = Value
        Return result
    End Function

    Shared Function CreateStringLiteral(ByVal Location As Span, ByVal Value As String) As Token
        Dim result As New Token(Location)
        result.m_TokenType = TokenType.StringLiteral
        result.m_TokenObject = Value
        Return result
    End Function

    Shared Function CreateSymbolToken(ByVal Location As Span, ByVal Symbol As KS) As Token
        Dim result As New Token(Location)
        result.m_TokenType = TokenType.Symbol
        result.m_TokenObject = Symbol
        Return result
    End Function

    Sub New(ByVal Span As Span)
        m_Location = Span
    End Sub

    Function IdentiferOrKeywordIdentifier() As String
        If IsKeyword() Then
            Return Identifier
        ElseIf IsIdentifier() Then
            Return Identifier
        Else
            Throw New InternalException()
        End If
    End Function

    Function IsSpecial() As Boolean
        Return IsKeyword() OrElse IsSymbol()
    End Function

    ReadOnly Property AsString() As String
        Get
            Select Case m_TokenType
                Case TokenType.CharLiteral
                    Return """" & CharLiteral & """c"
                Case TokenType.DateLiteral
                    Return "#" & DateLiteral.ToString() & "#"
                Case TokenType.DecimalLiteral
                    Return DecimalLiteral.ToString
                Case TokenType.EndOfCode
                    Return "END OF CODE"
                Case TokenType.EndOfFile
                    Return "END OF FILE"
                Case TokenType.EndOfLine
                    Return "END OF LINE"
                Case TokenType.Identifier
                    Return Identifier
                Case TokenType.Int16Literal, TokenType.Int32Literal, TokenType.Int64Literal, TokenType.UInt16Literal, TokenType.UInt32Literal, TokenType.UInt64Literal
                    Return IntegralLiteral.ToString
                Case TokenType.Keyword
                    Return Keyword.ToString()
                Case TokenType.SingleLiteral, TokenType.DoubleLiteral
                    Return Me.LiteralValue.ToString
                Case TokenType.StringLiteral
                    Return """" & Me.StringLiteral & """"
                Case TokenType.Symbol
                    Return Me.Symbol.ToString
                Case Else
                    Return "EMPTY TOKEN"
            End Select
        End Get
    End Property

    ReadOnly Property AsSpecial() As KS
        Get
            If IsKeyword() Then
                Return Keyword
            ElseIf IsSymbol() Then
                Return Symbol
            Else
                Return KS.None
            End If
        End Get
    End Property

    Function IsKeyword() As Boolean
        Return m_TokenType = TokenType.Keyword
    End Function

    Function IsSymbol() As Boolean
        Return m_TokenType = TokenType.Symbol
    End Function

    ReadOnly Property Symbol() As KS
        Get
            Return CType(m_TokenObject, KS)
        End Get
    End Property

    ReadOnly Property Keyword() As KS
        Get
            Return CType(m_TokenObject, KS)
        End Get
    End Property

    Function IsIdentifierOrKeyword() As Boolean
        Return IsIdentifier() OrElse IsKeyword()
    End Function

    Function IsIdentifier() As Boolean
        Return m_TokenType = TokenType.Identifier
    End Function

    ReadOnly Property LiteralValue() As Object
        Get
            Return m_TokenObject
        End Get
    End Property

    Function IsLiteral() As Boolean
        Select Case m_TokenType
            Case TokenType.DateLiteral, TokenType.CharLiteral, TokenType.DecimalLiteral, TokenType.DoubleLiteral, TokenType.Int16Literal, TokenType.Int32Literal, TokenType.Int64Literal, TokenType.SingleLiteral, TokenType.StringLiteral, TokenType.UInt16Literal, TokenType.UInt32Literal, TokenType.UInt64Literal
                Return True
            Case Else
                Return False
        End Select
    End Function

    ReadOnly Property IntegralLiteral() As ULong
        Get
            Return CULng(m_TokenObject)
        End Get
    End Property

    Function IsDateLiteral() As Boolean
        Return m_TokenType = TokenType.DateLiteral
    End Function

    Function IsIntegerLiteral() As Boolean
        Select Case m_TokenType
            Case TokenType.Int16Literal, TokenType.Int32Literal, TokenType.Int64Literal, TokenType.UInt16Literal, TokenType.UInt32Literal, TokenType.UInt64Literal
                Return True
            Case Else
                Return False
        End Select
    End Function

    Function IsCharLiteral() As Boolean
        Return m_TokenType = TokenType.CharLiteral
    End Function

    ReadOnly Property CharLiteral() As Char
        Get
            Return DirectCast(m_TokenObject, Char)
        End Get
    End Property

    Function IsStringLiteral() As Boolean
        Return m_TokenType = TokenType.StringLiteral
    End Function

    ReadOnly Property StringLiteral() As String
        Get
            Return DirectCast(m_TokenObject, String)
        End Get
    End Property

    ReadOnly Property DateLiteral() As Date
        Get
            Return DirectCast(m_TokenObject, Date)
        End Get
    End Property

    ReadOnly Property DecimalLiteral() As Decimal
        Get
            Return DirectCast(m_TokenObject, Decimal)
        End Get
    End Property

    Function IsDecimalLiteral() As Boolean
        Return m_TokenType = TokenType.DecimalLiteral
    End Function

    ReadOnly Property Identifier() As String
        Get
            If IsKeyword() Then
                Return Enums.strSpecial(Keyword)
            ElseIf IsIdentifier() Then
                Return DirectCast(m_TokenObject, String)
            Else
                Return Nothing
            End If
        End Get
    End Property

    ''' <summary>
    ''' Compares this token to any of the specified tokens. 
    ''' Returns true if any token matches.
    ''' </summary>
    ''' <param name="AnySpecial"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Function Equals(ByVal AnySpecial() As KS) As Boolean
        For i As Integer = 0 To VB.UBound(AnySpecial)
            If Equals(AnySpecial(i)) = True Then Return True
        Next
        Return False
    End Function

    Public Overloads Function Equals(ByVal AnySpecial As ModifierMasks) As Boolean
        Return IsKeyword() AndAlso Modifiers.IsKS(Keyword, AnySpecial)
    End Function

    Public Overloads Function Equals(ByVal a As KS, ByVal b As KS) As Boolean
        Return Equals(a) OrElse Equals(b)
    End Function

    Public Overloads Function Equals(ByVal a As KS, ByVal b As KS, ByVal c As KS) As Boolean
        Return Equals(a) OrElse Equals(b) OrElse Equals(c)
    End Function

    Public Overloads Function Equals(ByVal a As KS, ByVal b As KS, ByVal c As KS, ByVal d As KS) As Boolean
        Return Equals(a) OrElse Equals(b) OrElse Equals(c) OrElse Equals(d)
    End Function

    Public Overloads Function Equals(ByVal a As KS, ByVal b As KS, ByVal c As KS, ByVal d As KS, ByVal e As KS) As Boolean
        Return Equals(a) OrElse Equals(b) OrElse Equals(c) OrElse Equals(d) OrElse Equals(e)
    End Function

    Public Overloads Function Equals(ByVal a As KS, ByVal b As KS, ByVal c As KS, ByVal d As KS, ByVal e As KS, ByVal f As KS) As Boolean
        Return Equals(a) OrElse Equals(b) OrElse Equals(c) OrElse Equals(d) OrElse Equals(e) OrElse Equals(f)
    End Function

    Public Overloads Function Equals(ByVal a As KS, ByVal b As KS, ByVal c As KS, ByVal d As KS, ByVal e As KS, ByVal f As KS, ByVal g As KS) As Boolean
        Return Equals(a) OrElse Equals(b) OrElse Equals(c) OrElse Equals(d) OrElse Equals(e) OrElse Equals(f) OrElse Equals(g)
    End Function

    Public Overloads Function Equals(ByVal a As KS, ByVal b As KS, ByVal c As KS, ByVal d As KS, ByVal e As KS, ByVal f As KS, ByVal g As KS, ByVal h As KS) As Boolean
        Return Equals(a) OrElse Equals(b) OrElse Equals(c) OrElse Equals(d) OrElse Equals(e) OrElse Equals(f) OrElse Equals(g) OrElse Equals(h)
    End Function

    Public Overloads Function Equals(ByVal Special As KS) As Boolean
        If m_TokenType = TokenType.Keyword OrElse m_TokenType = TokenType.Symbol Then
            Return CInt(m_TokenObject) = CInt(Special)
        Else
            Return False
        End If
    End Function

    Shared Function IsKeyword(ByVal str As Char(), ByVal length As Integer, ByRef Keyword As KS) As Boolean
        Dim special As KS
        special = Enums.GetKS(str, length)
        If special <> KS.None Then
            Keyword = special
            Return True
        Else
            Return False
        End If
    End Function

    Public Overloads Function Equals(ByVal Identifier As String) As Boolean
        Return Me.IsIdentifier AndAlso Helper.CompareName(Me.Identifier, Identifier)
    End Function

    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        If TypeOf obj Is Token Then
            Return Equals(DirectCast(obj, Token))
        Else
            Throw New InternalException()
        End If
    End Function

    Overloads Function Equals(ByVal obj As Token) As Boolean
        If Me.IsIdentifier AndAlso obj.IsIdentifier Then
            Return Me.Equals(obj.Identifier)
        ElseIf Me.IsLiteral AndAlso obj.IsLiteral Then
            Return Me.LiteralValue.Equals(obj.LiteralValue)
        ElseIf Me.IsKeyword AndAlso obj.IsKeyword Then
            Return Me.Keyword = obj.Keyword
        ElseIf Me.IsSymbol AndAlso obj.IsSymbol Then
            Return Me.Symbol = obj.Symbol
        Else
            Return False
        End If
    End Function

    ReadOnly Property IsEndOfCode() As Boolean
        Get
            Return m_TokenType = TokenType.EndOfCode
        End Get
    End Property

    ReadOnly Property IsEndOfFile() As Boolean
        Get
            Return m_TokenType = TokenType.EndOfFile OrElse m_TokenType = TokenType.EndOfCode
        End Get
    End Property

    ReadOnly Property IsEndOfLine() As Boolean
        Get
            Return m_TokenType = TokenType.EndOfLine OrElse m_TokenType = TokenType.EndOfFile OrElse m_TokenType = TokenType.EndOfCode
        End Get
    End Property

    ReadOnly Property IsEndOfLineOnly() As Boolean
        Get
            Return m_TokenType = TokenType.EndOfLine
        End Get
    End Property

    Function IsEndOfStatement() As Boolean
        Return IsEndOfLineOnly OrElse Equals(KS.Colon)
    End Function

    Shared Operator =(ByVal Token As Token, ByVal Special As KS) As Boolean
        Return Token.Equals(Special)
    End Operator

    Shared Operator <>(ByVal Token As Token, ByVal Special As KS) As Boolean
        Return Not Token.Equals(Special)
    End Operator

    ReadOnly Property FriendlyString() As String
        Get
            Return ToString()
        End Get
    End Property

    ReadOnly Property SpecialString() As String
        Get
            If TypeOf m_TokenObject Is KS Then Return DirectCast(m_TokenObject, KS).ToString()
            Return "not a symbol"
        End Get
    End Property

    ReadOnly Property Location As Span
        Get
            Return m_Location
        End Get
    End Property

End Structure


