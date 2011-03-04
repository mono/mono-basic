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
''' VariableIdentifier  ::=  Identifier  [  ArrayNameModifier  ]
''' </summary>
''' <remarks></remarks>
Public Class VariableIdentifier
    Inherits ParsedObject
    Implements INameable

    Private m_Identifier As Identifier
    Private m_ArrayNameModifier As ArrayNameModifier
    Public IsNullable As Boolean

    ReadOnly Property Identifier() As Identifier
        Get
            Return m_Identifier
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Identifier As Identifier)
        MyBase.New(Parent)
        m_Identifier = Identifier
        If Identifier.Identifier Is Nothing Then Throw New InternalException("No identifier: " & Identifier.Location.ToString(Compiler))
    End Sub

    Sub Init(ByVal Identifier As Identifier, ByVal ArrayNameModifier As ArrayNameModifier)
        m_Identifier = Identifier
        m_ArrayNameModifier = ArrayNameModifier

        If Identifier.Identifier Is Nothing Then Throw New InternalException("No identifier: " & Identifier.Location.ToString(Compiler))
    End Sub

    ReadOnly Property ArrayNameModifier() As ArrayNameModifier
        Get
            Return m_ArrayNameModifier
        End Get
    End Property

    ReadOnly Property HasArrayNameModifier() As Boolean
        Get
            Return m_ArrayNameModifier IsNot Nothing
        End Get
    End Property

    Public Property Name() As String Implements INameable.Name
        Get
            Return m_Identifier.Identifier
        End Get
        Set(ByVal value As String)
            m_Identifier.Identifier = value
        End Set
    End Property
End Class

