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

    Private m_Identifier As Token

    Sub New(ByVal Parent As ParsedObject)
        MyBase.new(Parent)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Identifier As Token)
        MyBase.new(Parent)
        m_Identifier = Identifier
    End Sub

    Sub Init(ByVal Identifier As Token)
        m_Identifier = Identifier
    End Sub

    Function Clone(Optional ByVal NewParent As ParsedObject = Nothing) As Identifier
        If NewParent Is Nothing Then NewParent = Me.Parent
        Return New Identifier(NewParent, m_Identifier)
    End Function

    ReadOnly Property Token() As Token
        Get
            Return m_Identifier
        End Get
    End Property

    ReadOnly Property Name() As String
        Get
            Return m_Identifier.Identifier
        End Get
    End Property

End Class
