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

Public Class ExpressionParseInfo
    Private m_Parent As ParsedObject
    Private m_IsLeftSide As Boolean
    Private m_IsInTypeOf As Boolean

    Property IsLeftSide() As Boolean
        Get
            Return m_IsLeftSide
        End Get
        Set(ByVal value As Boolean)
            m_IsLeftSide = value
        End Set
    End Property

    Property Parent() As ParsedObject
        Get
            Return m_Parent
        End Get
        Set(ByVal value As ParsedObject)
            m_Parent = value
        End Set
    End Property

    Public Property IsInTypeOf() As Boolean
        Get
            Return m_IsInTypeOf
        End Get
        Set(ByVal value As Boolean)
            m_IsInTypeOf = value
        End Set
    End Property

    ''' <summary>
    ''' Initializing constructor.
    ''' </summary>
    ''' <param name="LeftSide"></param>
    ''' <remarks></remarks>
    Sub New(ByVal Parent As ParsedObject, Optional ByVal LeftSide As Boolean = False, Optional ByVal IsInTypeOf As Boolean = False)
        m_IsLeftSide = LeftSide
        m_IsInTypeOf = IsInTypeOf
        m_Parent = Parent
    End Sub
End Class
