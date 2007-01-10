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
Public MustInherit Class LiteralTokenBase(Of T)
    Inherits LiteralToken

    Sub New(ByVal Span As Span, ByVal Type As BuiltInDataTypes, ByVal Compiler As Compiler, ByVal LiteralTypeCharacter As LiteralTypeCharacters_Characters, ByVal Literal As T)
        MyBase.New(Span, Type, Compiler, LiteralTypeCharacter, Literal)
    End Sub
End Class

Public MustInherit Class LiteralToken '(Of Type)
    Inherits Token
    'Implements ILiteralToken

    Private m_Value As Object
    Private m_Type As BuiltInDataTypes
    Private m_LiteralTypeCharacter As LiteralTypeCharacters_Characters

    Public Overloads Function Equals(ByVal Token As LiteralToken) As Boolean 'Implements ILiteralToken.Equals
        If Me.LiteralType = Token.LiteralType Then
            Return CBool(Microsoft.VisualBasic.CompilerServices.Operators.CompareObjectEqual(Token.LiteralValue, Me.LiteralValue, False))
        Else
            Return False
        End If
    End Function

#If DEBUG Then
    Public Overrides Sub Dump(ByVal Dumper As IndentedTextWriter)
        Dumper.Write(ToString)
    End Sub
    Sub DumpLiteralTypeCharacter(ByVal Dumper As IndentedTextWriter)
        If m_LiteralTypeCharacter <> LiteralTypeCharacters_Characters.None Then
            Dumper.Write(LiteralTypeCharacters.GetTypeCharacter(m_LiteralTypeCharacter))
        End If
    End Sub
#End If
    Public Overrides Function ToString() As String
        Return m_Value.ToString
    End Function

    ReadOnly Property Literal() As Object
        Get
            Return m_Value
        End Get
    End Property

    ReadOnly Property LiteralValue() As Object 'Implements ILiteralToken.LiteralValue
        Get
            Return m_Value
        End Get
    End Property

    Public ReadOnly Property LiteralTypeCharacterCache() As LiteralTypeCharacters_Characters
        Get
            Return m_LiteralTypeCharacter
        End Get
    End Property

    Sub New(ByVal Span As Span, ByVal Type As BuiltInDataTypes, ByVal Compiler As Compiler, ByVal LiteralTypeCharacter As LiteralTypeCharacters_Characters, ByVal Literal As Object)
        MyBase.New(Span, Compiler)
        m_Type = Type
        m_LiteralTypeCharacter = LiteralTypeCharacter
        m_Value = Literal
        Helper.Assert(m_Type <> BuiltInDataTypes.Object)
    End Sub

    ReadOnly Property LiteralType() As TypeCode 'Implements ILiteralToken.LiteralType
        Get
            Return TypeResolution.BuiltInTypeToTypeCode(m_Type)
        End Get
    End Property

End Class

