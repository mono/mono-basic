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
''' Identifier ::= IdentifierToken
''' </summary>
''' <remarks></remarks>
''' 
Public Class Identifier
    Inherits ParsedObject

    Private m_Name As String
    Private m_TypeCharacter As TypeCharacters.Characters = TypeCharacters.Characters.None

    Sub New(ByVal Parent As ParsedObject)
        MyBase.new(Parent)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Identifier As String, ByVal Location As Span, ByVal TypeCharacter As TypeCharacters.Characters)
        MyBase.new(Parent, Location)
        m_Name = Identifier
        m_TypeCharacter = TypeCharacter
    End Sub

    Sub New(ByVal Identifier As String)
        m_Name = Identifier
    End Sub

    Sub Init(ByVal Identifier As String, ByVal Location As Span, ByVal TypeCharacter As TypeCharacters.Characters)
        Me.Location = Location
        m_Name = Identifier
        m_TypeCharacter = TypeCharacter
    End Sub

    Property Identifier() As String
        Get
            Return m_Name
        End Get
        Set
            m_Name = Value
        End Set
    End Property

    Property TypeCharacter() As TypeCharacters.Characters
        Get
            Return m_TypeCharacter
        End Get
        Set
            m_TypeCharacter = Value
        End Set
    End Property

    ReadOnly Property HasTypeCharacter() As Boolean
        Get
            Return m_TypeCharacter <> TypeCharacters.Characters.None
        End Get
    End Property

    Property Name() As String
        Get
            Return m_Name
        End Get
        Set(ByVal value As String)
            m_Name = value
        End Set
    End Property

End Class
