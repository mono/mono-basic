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
''' ParametersOrType  ::= [  "(" [  ParameterList  ]  ")"  ]  | "As"  NonArrayTypeName
''' </summary>
''' <remarks></remarks>
Public Class ParametersOrType
    Inherits ParsedObject

    Private m_ParameterList As ParameterList
    Private m_NonArrayTypeName As NonArrayTypeName

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal ParameterList As ParameterList)
        m_ParameterList = ParameterList
    End Sub

    Sub Init(ByVal NonArrayTypeName As NonArrayTypeName)
        m_NonArrayTypeName = NonArrayTypeName
    End Sub

    ReadOnly Property Parameters() As ParameterList
        Get
            Return m_ParameterList
        End Get
    End Property

    ReadOnly Property Type() As NonArrayTypeName
        Get
            Return m_NonArrayTypeName
        End Get
    End Property

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        If m_ParameterList IsNot Nothing Then result = m_ParameterList.ResolveCode(info) AndAlso result

        Return result
    End Function

    Public Overrides Function CreateDefinition() As Boolean
        Dim result As Boolean = True

        result = MyBase.CreateDefinition AndAlso result
        If m_ParameterList IsNot Nothing Then result = m_ParameterList.CreateDefinition AndAlso result

        Return result
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        If m_ParameterList IsNot Nothing Then
            result = m_ParameterList.ResolveTypeReferences AndAlso result
        End If
        If m_NonArrayTypeName IsNot Nothing Then
            result = m_NonArrayTypeName.ResolveTypeReferences AndAlso result
        End If

        Return result
    End Function
End Class
