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
''' Identifier ::= IdentifierToken
''' </summary>
''' <remarks></remarks>
''' 
Public Class Identifier
    Inherits ParsedObject

    'Private m_Identifier As Token
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

    Sub Init(ByVal Identifier As String, ByVal Location As Span, ByVal TypeCharacter As TypeCharacters.Characters)
        Me.Location = Location
        m_Name = Identifier
        m_TypeCharacter = TypeCharacter
    End Sub

    ReadOnly Property Identifier() As String
        Get
            Return m_Name
        End Get
    End Property

    ReadOnly Property TypeCharacter() As TypeCharacters.Characters
        Get
            Return m_TypeCharacter
        End Get
    End Property

    ReadOnly Property HasTypeCharacter() As Boolean
        Get
            Return m_TypeCharacter <> TypeCharacters.Characters.None
        End Get
    End Property

    Function Clone(Optional ByVal NewParent As ParsedObject = Nothing) As Identifier
        Dim result As Identifier
        If NewParent Is Nothing Then NewParent = Me.Parent
        result = New Identifier(NewParent)
        result.Init(m_Name, Location, m_TypeCharacter)
        Return result
    End Function

    'ReadOnly Property Token() As Token
    '    Get
    '        Return m_Identifier
    '    End Get
    'End Property

    ReadOnly Property Name() As String
        Get
            Return m_Name
        End Get
    End Property

End Class
