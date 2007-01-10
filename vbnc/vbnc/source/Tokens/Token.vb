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

Public MustInherit Class Token
    Inherits BaseObject

    Sub New(ByVal Span As Span, ByVal Compiler As Compiler)
        MyBase.New(Compiler, Span)
    End Sub

    Function IdentiferOrKeywordIdentifier() As String
        If IsKeyword() Then
            Return AsKeyword.Identifier
        ElseIf IsIdentifier() Then
            Return AsIdentifier.Identifier
        Else
            Throw New InternalException(Me)
        End If
    End Function

    Function IsSpecial() As Boolean
        Return IsKeyword() OrElse IsSymbol()
    End Function

    ReadOnly Property AsSpecial() As KS
        Get
            If IsKeyword() Then
                Return AsKeyword.Keyword
            ElseIf IsSymbol() Then
                Return AsSymbol.Symbol
            Else
                Throw New InternalException(Me)
            End If
        End Get
    End Property

    Function IsKeyword() As Boolean
        Return TypeOf Me Is KeywordToken
    End Function

    Function AsKeyword() As KeywordToken
        If IsKeyword() = False Then Throw New InternalException(Me) 'Helper.Assert(IsKeyword)
        Return DirectCast(Me, KeywordToken)
    End Function

    Function IsSymbol() As Boolean
        Return TypeOf Me Is SymbolToken
    End Function

    Function AsSymbol() As SymbolToken
        If IsSymbol() = False Then Throw New InternalException(Me) 'Helper.Assert(IsSymbol)
        Return DirectCast(Me, SymbolToken)
    End Function

    Function IsIdentifierOrKeyword() As Boolean
        Return IsIdentifier() OrElse IsKeyword()
    End Function

    Function IsIdentifier() As Boolean
        Return TypeOf Me Is IdentifierToken
    End Function

    Function AsIdentifier() As IdentifierToken
        If IsIdentifier() = False Then Throw New InternalException(Me) 'Helper.Assert(IsIdentifier)
        Return DirectCast(Me, IdentifierToken)
    End Function

    Function IsLiteral() As Boolean
        Return TypeOf Me Is LiteralToken
    End Function

    Function AsLiteral() As LiteralToken
        Return DirectCast(Me, LiteralToken)
    End Function

    Function IsDateLiteral() As Boolean
        Return TypeOf Me Is DateLiteralToken
    End Function

    Function AsDateLiteral() As DateLiteralToken
        If IsDateLiteral() = False Then Throw New InternalException(Me) ' Helper.Assert(IsDateLiteral)
        Return DirectCast(Me, DateLiteralToken)
    End Function

    Function IsIntegerLiteral() As Boolean
        Return TypeOf Me Is IIntegralLiteralToken
    End Function

    Function AsIntegerLiteral() As IIntegralLiteralToken
        If IsIntegerLiteral() = False Then Throw New InternalException(Me) ' Helper.Assert(IsIntegerLiteral)
        Return DirectCast(Me, IIntegralLiteralToken)
    End Function

    Function IsCharLiteral() As Boolean
        Return TypeOf Me Is CharLiteralToken
    End Function

    Function AsCharLiteral() As CharLiteralToken
        If IsCharLiteral() = False Then Throw New InternalException(Me) ' Helper.Assert(IsCharLiteral)
        Return DirectCast(Me, CharLiteralToken)
    End Function

    Function IsStringLiteral() As Boolean
        Return TypeOf Me Is StringLiteralToken
    End Function

    Function AsStringLiteral() As StringLiteralToken
        If IsStringLiteral() = False Then Throw New InternalException(Me) ' Helper.Assert(IsStringLiteral)
        Return DirectCast(Me, StringLiteralToken)
    End Function

    Function IsDecimalLiteral() As Boolean
        Return TypeOf Me Is DecimalLiteralToken
    End Function

    Function AsDecimalLiterl() As DecimalLiteralToken
        If IsDecimalLiteral() = False Then Throw New InternalException(Me) ' Helper.Assert(IsDecimalLiteral)
        Return DirectCast(Me, DecimalLiteralToken)
    End Function

    ''' <summary>
    ''' Compares this token to any of the specified tokens. 
    ''' Returns true if any token matches.
    ''' </summary>
    ''' <param name="AnySpecial"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable Overloads Function Equals(ByVal ParamArray AnySpecial() As KS) As Boolean
        For i As Integer = 0 To VB.UBound(AnySpecial)
            If Equals(AnySpecial(i)) = True Then Return True
        Next
        Return False
    End Function

    Public Overridable Overloads Function Equals(ByVal Special As KS) As Boolean
        'Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "Token.Equals(KS) called on type " & Me.GetType.ToString)
        Return False
    End Function

    Public Overridable Overloads Function Equals(ByVal Identifier As String) As Boolean
        'Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "Token.Equals(String) called on type " & Me.GetType.ToString)
        Return False
    End Function

    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        If TypeOf obj Is Token Then
            Return Equals(DirectCast(obj, Token))
        Else
            Throw New InternalException(Me)
        End If
    End Function

    Overloads Function Equals(ByVal obj As Token) As Boolean
        If Me.IsIdentifier AndAlso obj.IsIdentifier Then
            Return Me.AsIdentifier.Equals(obj.AsIdentifier)
        ElseIf Me.IsLiteral AndAlso obj.IsLiteral Then
            Return Me.AsLiteral.Equals(obj.AsLiteral)
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Returns true if currenttoken = Public,Private,Friend,Protected or Static.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function IsScopeKeyword() As Boolean
        Dim keyword As KeywordToken = TryCast(Me, KeywordToken)
        If keyword IsNot Nothing Then
            Select Case keyword.Keyword
                Case KS.Public, KS.Private, KS.Friend, KS.Protected, KS.Static
                    Return True
                Case KS.Else
                    Return False
            End Select
        Else
            Return False
        End If
    End Function

    Function IsEndOfCode() As Boolean
        Return TypeOf Me Is EndOfCodeToken
    End Function

    Function IsEndOfFile() As Boolean
        Return TypeOf Me Is EndOfFileToken
    End Function

    Function IsEndOfStatement() As Boolean
        Return isEndOfLine(True) OrElse Equals(KS.Colon)
    End Function

    Function IsEndOfLine(Optional ByVal onlyEndOfLine As Boolean = False) As Boolean
        If onlyEndOfLine Then
            Return TypeOf Me Is EndOfLineToken AndAlso TypeOf Me Is EndOfFileToken = False
        Else
            Return TypeOf Me Is EndOfLineToken
        End If
    End Function

#If DEBUG Then
    MustOverride Overloads Sub Dump(ByVal Dumper As IndentedTextWriter)
    MustOverride Overrides Function ToString() As String
#End If

    Shared Operator =(ByVal Token As Token, ByVal Identifier As String) As Boolean
        If Token Is Nothing AndAlso Identifier = "" Then
            Return True
        ElseIf Token Is Nothing Then
            Return False
        ElseIf Not TypeOf Token Is IdentifierToken Then
            Return False
        Else
            Return DirectCast(Token, IdentifierToken) = Identifier
        End If
    End Operator

    Shared Operator <>(ByVal Token As Token, ByVal Identifier As String) As Boolean
        Return Not Token = Identifier
    End Operator

    Shared Operator =(ByVal Token As Token, ByVal Special As KS) As Boolean
        If Token Is Nothing Then Return False
        Return (Token.IsKeyword() AndAlso Token.AsKeyword.Keyword = Special) OrElse _
               (Token.IsSymbol AndAlso Token.AsSymbol.Symbol = Special)
    End Operator

    Shared Operator <>(ByVal Token As Token, ByVal Special As KS) As Boolean
        Return Not Token = Special
    End Operator

    ReadOnly Property FriendlyString() As String
        Get
            Return ToString() & " - " & Location.ToString()
        End Get
    End Property

End Class


