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

Public Class IdentifierOrKeyword
    Inherits ParsedObject

    Private m_Token As Token

    Public Overrides Function ResolveTypeReferences() As Boolean
        Return True
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.new(Parent)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Token As Token)
        MyBase.New(Parent)
        Me.Init(Token)
    End Sub

    Sub Init(ByVal Token As Token)
        m_Token = Token
        Helper.Assert(m_Token.IsIdentifierOrKeyword)
    End Sub

    Function Clone(Optional ByVal NewParent As ParsedObject = Nothing) As IdentifierOrKeyword
        If NewParent Is Nothing Then NewParent = DirectCast(Me.Parent, ParsedObject)
        Return New IdentifierOrKeyword(NewParent, m_Token)
    End Function

    ReadOnly Property Name() As String
        Get
            If IsIdentifier Then
                Return Token.Identifier
            ElseIf IsKeyword Then
                Return Token.Identifier
            Else
                Throw New InternalException(Me)
            End If
        End Get
    End Property

    ReadOnly Property Identifier() As String
        Get
            Return m_Token.IdentiferOrKeywordIdentifier
        End Get
    End Property

    ReadOnly Property IsIdentifier() As Boolean
        Get
            Return m_Token.IsIdentifier
        End Get
    End Property

    ReadOnly Property IsKeyword() As Boolean
        Get
            Return m_Token.IsKeyword
        End Get
    End Property

    ReadOnly Property Token() As Token
        Get
            Return m_Token
        End Get
    End Property

End Class
