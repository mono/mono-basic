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

Public Class IdentifierOrKeyword
    Inherits ParsedObject

    Private m_Identifier As String
    Private m_Keyword As KS

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

    Sub New(ByVal Parent As ParsedObject, ByVal Identifier As String, ByVal Keyword As KS)
        MyBase.New(Parent)
        m_Identifier = Identifier
        m_Keyword = Keyword
    End Sub

    Sub Init(ByVal Identifier As String, ByVal Keyword As KS)
        m_Identifier = Identifier
        m_Keyword = Keyword
    End Sub

    Sub Init(ByVal Token As Token)
        Helper.Assert(Token.IsIdentifierOrKeyword)
        m_Identifier = Token.Identifier
        If Token.IsKeyword Then m_Keyword = Token.Keyword
    End Sub

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

    ReadOnly Property Keyword() As KS
        Get
            Return m_Keyword
        End Get
    End Property

    ReadOnly Property IsIdentifier() As Boolean
        Get
            Return Not IsKeyword
        End Get
    End Property

    ReadOnly Property IsKeyword() As Boolean
        Get
            Return m_Keyword <> KS.None
        End Get
    End Property

End Class
