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
''' A resource file specified on the command line.
''' </summary>
Public Class Resource
    ''' <summary>
    ''' The filename of the resource.
    ''' </summary>
    Private m_Filename As String

    ''' <summary>
    ''' The name of the resource.
    ''' </summary>
    Private m_Identifier As String

    ''' <summary>
    ''' Is this resource public?
    ''' </summary>
    Private m_Public As Boolean

    ''' <summary>
    ''' The filename of the resource.
    ''' </summary>
    ReadOnly Property Filename() As String
        Get
            Return m_Filename
        End Get
    End Property

    ''' <summary>
    ''' The name of the resource.
    ''' </summary>
    ReadOnly Property Identifier() As String
        Get
            Return m_Identifier
        End Get
    End Property

    ''' <summary>
    ''' Is this resource public?
    ''' </summary>
    ReadOnly Property [Public]() As Boolean
        Get
            Return m_Public
        End Get
    End Property

    ''' <summary>
    ''' Creates a new resource with the specified values.
    ''' </summary>
    Sub New(ByVal Filename As String, ByVal Identifier As String, Optional ByVal [Public] As Boolean = True)
        Me.m_Filename = Filename
        Me.m_Identifier = Identifier
        Me.m_Public = [Public]
    End Sub

End Class