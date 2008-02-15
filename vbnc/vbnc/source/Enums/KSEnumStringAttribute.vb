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

Public Class KSEnumStringAttribute
    Inherits EnumStringAttribute
    Protected m_FriendlyValue As String
    Protected m_IsKeyword As Boolean
    Protected m_IsSymbol As Boolean

    ReadOnly Property IsKeyword() As Boolean
        Get
            Return m_IsKeyword
        End Get
    End Property

    ReadOnly Property IsSymbol() As Boolean
        Get
            Return m_IsSymbol
        End Get
    End Property

    Public ReadOnly Property FriendlyValue() As String
        Get
            If m_FriendlyValue Is Nothing Then
                Return m_Value
            Else
                Return m_FriendlyValue
            End If
        End Get
    End Property

    ''' <summary>
    ''' Constructor.
    ''' </summary>
    ''' <param name="Value"></param>
    ''' <param name="FriendlyValue"></param>
    ''' <param name="IsKeyword">Is this a real keyword?</param>
    ''' <param name="IsSymbol">Is this a real symbol?</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal Value As String, ByVal FriendlyValue As String, ByVal IsKeyword As Boolean, ByVal IsSymbol As Boolean)
        MyBase.New(Value, Nothing)
        m_FriendlyValue = FriendlyValue
        m_IsKeyword = IsKeyword
        m_IsSymbol = IsSymbol
    End Sub

    Public Sub New(ByVal Value As String, Optional ByVal FriendlyValue As String = Nothing, Optional ByVal Tag As String = Nothing)
        Me.New(Value, FriendlyValue, False, False)
        m_Tag = Tag
    End Sub
End Class