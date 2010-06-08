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
''' Operand  ::=  [  "ByVal"  ]  Identifier  [  "As"  TypeName  ]
''' </summary>
''' <remarks></remarks>
Public Class Operand
    Inherits ParsedObject
    Implements INameable

    Private m_Identifier As Identifier
    Private m_TypeName As TypeName

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal Identifier As Identifier, ByVal TypeName As TypeName)
        m_Identifier = Identifier
        m_TypeName = TypeName
    End Sub

    ReadOnly Property Identifier() As Identifier
        Get
            Return m_Identifier
        End Get
    End Property

    ReadOnly Property TypeName() As TypeName
        Get
            Return m_TypeName
        End Get
    End Property

    <Obsolete("No code to resolve here.")> Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Return True
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        If m_TypeName IsNot Nothing Then result = m_TypeName.ResolveTypeReferences AndAlso result

        Return result
    End Function

    Public Property Name() As String Implements INameable.Name
        Get
            Return m_Identifier.Name
        End Get
        Set(ByVal value As String)
            m_Identifier.Name = value
        End Set
    End Property
End Class
