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
''' A define as specified on the command line.
''' </summary>
Public Class Define
    ''' <summary>
    ''' The symbol of this define.
    ''' </summary>
    Private m_Symbol As String

    ''' <summary>
    ''' The value of this define.
    ''' </summary>
    Private m_Value As String

    ''' <summary>
    ''' The symbol of this define.
    ''' </summary>
    ReadOnly Property Symbol() As String
        Get
            Return m_Symbol
        End Get
    End Property

    ''' <summary>
    ''' The value of this define.
    ''' </summary>
    ReadOnly Property Value() As String
        Get
            Return m_Value
        End Get
    End Property

    ''' <summary>
    ''' Create a new define with the specified values.
    ''' </summary>
    Sub New(ByVal Symbol As String, ByVal Value As String)
        Me.m_Symbol = Symbol
        Me.m_Value = Value
    End Sub
End Class