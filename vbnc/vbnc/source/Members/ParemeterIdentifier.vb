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
''' ParameterIdentifier  ::=  Identifier  [  ArrayNameModifier  ]
''' </summary>
''' <remarks></remarks>
Public Class ParameterIdentifier
    Inherits ParsedObject
    Implements INameable

    Private m_Identifier As Identifier
    Private m_ArrayNameModifier As ArrayNameModifier

    Property Identifier() As Identifier
        Get
            Return m_Identifier
        End Get
        Set(ByVal value As Identifier)
            m_Identifier = value
        End Set
    End Property

    Sub New(ByVal Parent As Parameter)
        MyBase.New(Parent)
    End Sub

    Sub New(ByVal Parent As Parameter, ByVal Name As String)
        MyBase.New(Parent)
        m_Identifier = New Identifier(Me, Name, Nothing, TypeCharacters.Characters.None)
    End Sub

    Sub Init(ByVal Identifier As Identifier, ByVal ArrayNameModifier As ArrayNameModifier)
        m_Identifier = Identifier
        m_ArrayNameModifier = ArrayNameModifier
    End Sub

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        If m_ArrayNameModifier IsNot Nothing Then result = m_ArrayNameModifier.ResolveTypeReferences AndAlso result

        Return result
    End Function

    Shadows ReadOnly Property Parent() As Parameter
        Get
            Return DirectCast(MyBase.Parent, Parameter)
        End Get
    End Property

    ReadOnly Property ArrayNameModifier() As ArrayNameModifier
        Get
            Return m_ArrayNameModifier
        End Get
    End Property

    Public Property Name() As String Implements INameable.Name
        Get
            Return m_Identifier.Name
        End Get
        Set(ByVal value As String)
            m_Identifier.Name = value
        End Set
    End Property
End Class
