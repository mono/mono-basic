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
''' AttributeArguments  ::=	
'''     AttributePositionalArgumentList  |
''' 	AttributePositionalArgumentList  ,  VariablePropertyInitializerList  |
'''	    VariablePropertyInitializerList
'''
''' </summary>
''' <remarks></remarks>
Public Class AttributeArguments
    Inherits ParsedObject

    Private m_AttributePositionalArgumentList As AttributePositionalArgumentList
    Private m_VariablePropertyInitializerList As VariablePropertyInitializerList

    Sub New(ByVal Parent As ParsedObject)
        MyBase.new(Parent)
    End Sub

    Sub Init(ByVal PositionalArgumentList As AttributePositionalArgumentList, ByVal VariablePropertyInitializerList As VariablePropertyInitializerList)
        m_AttributePositionalArgumentList = PositionalArgumentList
        m_VariablePropertyInitializerList = VariablePropertyInitializerList
    End Sub

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        If m_AttributePositionalArgumentList IsNot Nothing Then result = m_AttributePositionalArgumentList.ResolveCode(info) AndAlso result
        If m_VariablePropertyInitializerList IsNot Nothing Then result = m_VariablePropertyInitializerList.ResolveCode(Info) AndAlso result

        Return result
    End Function

    ReadOnly Property PositionalArgumentList() As AttributePositionalArgumentList
        Get
            If m_AttributePositionalArgumentList Is Nothing Then
                m_AttributePositionalArgumentList = New AttributePositionalArgumentList(Me)
            End If
            Return m_AttributePositionalArgumentList
        End Get
    End Property

    ReadOnly Property VariablePropertyInitializerList() As VariablePropertyInitializerList
        Get
            If m_VariablePropertyInitializerList Is Nothing Then
                m_VariablePropertyInitializerList = New VariablePropertyInitializerList(Me)
            End If
            Return m_VariablePropertyInitializerList
        End Get
    End Property
End Class
