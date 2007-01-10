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
''' An expression with this classification can only appear as the left side
''' of a member access. In any other context an expression classified as
''' a type causes a compile-time error.
''' </summary>
''' <remarks></remarks>
Public Class TypeClassification
    Inherits ExpressionClassification

    Private m_Type As Type 'Descriptor
    Private m_TypeParameter As TypeParameter

    ReadOnly Property IsTypeParameter() As Boolean
        Get
            Return m_TypeParameter IsNot Nothing
        End Get
    End Property

    Property Type() As Type 'Descriptor
        Get
            Return m_Type
        End Get
        Set(ByVal value As Type) 'Descriptor)
            m_Type = value
        End Set
    End Property

    Sub New(ByVal Parent As ParsedObject, ByVal Type As TypeDeclaration)
        MyBase.new(Classifications.Type, Parent)
        m_Type = Type.TypeDescriptor
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal TypeParameter As TypeParameter)
        MyBase.New(Classifications.Type, Parent)
        m_TypeParameter = TypeParameter
    End Sub

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Classifications.Type, Parent)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Type As Object)
        Me.new(Parent)
        If TypeOf Type Is TypeDescriptor Then
            m_Type = DirectCast(Type, TypeDescriptor)
        ElseIf TypeOf Type Is Type Then
            m_Type = DirectCast(Type, Type) ' New TypeDescriptor(DirectCast(Type, Type))
        ElseIf TypeOf Type Is TypeDeclaration Then
            m_Type = DirectCast(Type, TypeDeclaration).TypeDescriptor
        ElseIf TypeOf Type Is TypeParameter Then
            m_TypeParameter = DirectCast(Type, TypeParameter)
        Else
            Throw New InternalException(Me)
        End If
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Type As TypeDescriptor)
        Me.New(Parent)
        m_Type = Type
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Type As Type)
        MyBase.New(Classifications.Type, Parent)
        m_Type = Type ' New TypeDescriptor(Type)
    End Sub
End Class
