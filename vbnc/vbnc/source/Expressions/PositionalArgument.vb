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

Public Class PositionalArgument
    Inherits Argument

    ''' <summary>
    ''' 0 based position of the argument.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_Position As Integer

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Parent"></param>
    ''' <param name="Position">0 based.</param>
    ''' <remarks></remarks>
    Sub New(ByVal Parent As ParsedObject, ByVal Position As Integer, ByVal Expression As Expression)
        MyBase.New(Parent)
        MyBase.Init(Expression)
        m_Position = Position
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Parent"></param>
    ''' <remarks></remarks>
    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Position">0 based.</param>
    ''' <remarks></remarks>
    Shadows Sub Init(ByVal Position As Integer, ByVal Expression As Expression)
        MyBase.Init(Expression)
        m_Position = Position
    End Sub

    ''' <summary>
    ''' The position of the argument.
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property Position() As Integer
        Get
            Return m_position
        End Get
    End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Return MyBase.ResolveTypeReferences
    End Function
End Class
