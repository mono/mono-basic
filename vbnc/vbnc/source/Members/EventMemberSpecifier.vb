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
''' EventMemberSpecifier  ::=
'''  QualifiedIdentifier  "."  IdentifierOrKeyword  |
'''  MyBase  "."  IdentifierOrKeyword  |
'''	 Me  "."  IdentifierOrKeyword
''' </summary>
''' <remarks></remarks>
Public Class EventMemberSpecifier
    Inherits ParsedObject

    Private m_First As BaseObject
    Private m_Second As IdentifierOrKeyword

    Private m_ResolvedType As Type

    ReadOnly Property First() As BaseObject
        Get
            Return m_First
        End Get
    End Property

    ReadOnly Property Second() As IdentifierOrKeyword
        Get
            Return m_second
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal First As BaseObject, ByVal Second As IdentifierOrKeyword)
        m_First = First
        m_Second = Second
    End Sub

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        Dim qi As QualifiedIdentifier = TryCast(m_First, QualifiedIdentifier)
        If qi IsNot Nothing Then
            result = qi.ResolveAsTypeName AndAlso result
            m_ResolvedType = qi.ResolvedType
        ElseIf TypeOf m_First Is MyBaseExpression Then
            m_ResolvedType = Me.FindFirstParent(Of TypeDeclaration).BaseType
        ElseIf TypeOf m_First Is MeExpression Then
            m_ResolvedType = Me.FindFirstParent(Of TypeDeclaration).TypeDescriptor
        Else
            Throw New InternalException(Me)
        End If

        Return result
    End Function

End Class
