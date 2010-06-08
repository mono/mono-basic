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
''' Type | QualifiedIdentifier ( Of [TypeArityList] )
''' </summary>
''' <remarks></remarks>
Public Class GetTypeTypeName
    Inherits ParsedObject

    Private m_Name As ParsedObject
    Private m_TypeArity As Integer

    Private m_ResolvedType As Mono.Cecil.TypeReference

    ReadOnly Property Name() As ParsedObject
        Get
            Return m_Name
        End Get
    End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        If m_ResolvedType IsNot Nothing Then Return True

        If m_TypeArity > 0 Then
            Dim qi As QualifiedIdentifier = DirectCast(m_Name, QualifiedIdentifier)
            result = qi.ResolveAsTypeName(False, m_TypeArity) AndAlso result

            If result Then m_ResolvedType = qi.ResolvedType
        Else
            result = m_Name.ResolveTypeReferences AndAlso result
            If result Then m_ResolvedType = DirectCast(m_Name, TypeName).ResolvedType
        End If

        Return result
    End Function

    Sub New(ByVal Parent As GetTypeExpression)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal Name As TypeName)
        m_Name = Name
    End Sub

    Sub Init(ByVal Name As QualifiedIdentifier, ByVal TypeArity As Integer)
        m_Name = Name
        m_TypeArity = TypeArity
    End Sub

    ReadOnly Property ResolvedType() As Mono.Cecil.TypeReference
        Get
            Helper.Assert(m_ResolvedType IsNot Nothing)
            Return m_ResolvedType
        End Get
    End Property

    Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Return ResolveTypeReferences()
    End Function

End Class
