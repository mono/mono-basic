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
''' <summary>
''' Identifier  ::=
'''	   NonEscapedIdentifier  [  TypeCharacter  ]  |
'''	   Keyword  TypeCharacter  |
'''	   EscapedIdentifier
''' 
''' NonEscapedIdentifier  ::=  &lt; IdentifierName but not Keyword &gt;
''' EscapedIdentifier  ::=  [  IdentifierName  ] 
''' </summary>
''' <remarks></remarks>
Public Class IdentifierToken
    Inherits Token

    Private m_Identifier As String
    Private m_TypeCharacter As TypeCharacters.Characters
    Private m_Escaped As Boolean

    ReadOnly Property HasTypeCharacter() As Boolean
        Get
            Return m_TypeCharacter <> TypeCharacters.Characters.None
        End Get
    End Property

    Sub New(ByVal Range As Span, ByVal Identifier As String, ByVal TypeCharacter As TypeCharacters.Characters, ByVal Escaped As Boolean, ByVal Compiler As Compiler)
        MyBase.New(Range, Compiler)
        m_Identifier = Identifier
        m_TypeCharacter = TypeCharacter
        m_Escaped = Escaped
    End Sub

    Overloads Shared Operator =(ByVal o1 As IdentifierToken, ByVal o2 As String) As Boolean
        Return String.Compare(o1.m_Identifier, o2, True) = 0
    End Operator

    Overloads Shared Operator <>(ByVal o1 As IdentifierToken, ByVal o2 As String) As Boolean
        Return Not o1 = o2
    End Operator

    Public Overrides Function Equals(ByVal Identifier As String) As Boolean
        Return NameResolution.CompareName(m_Identifier, Identifier)
    End Function

    Public Overloads Function Equals(ByVal Identifier As IdentifierToken) As Boolean
        Return Equals(Identifier.Identifier)
    End Function

#If DEBUG Then
    Public Overrides Sub Dump(ByVal Dumper As IndentedTextWriter)
        If m_Escaped Then
            Dumper.Write("[" & m_Identifier & "]")
        Else
            Dumper.Write(m_Identifier)
            Dumper.Write(TypeCharacters.GetTypeCharacter(m_TypeCharacter))
        End If
    End Sub
#End If

    ReadOnly Property Name() As String
        Get
            Return m_Identifier
        End Get
    End Property

    ReadOnly Property Identifier() As String
        Get
            Return m_Identifier
        End Get
    End Property

    Public Overrides Function ToString() As String
        Return m_Identifier
    End Function
End Class
