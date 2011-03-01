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

Public MustInherit Class GenericTypeDeclaration
    Inherits TypeDeclaration
    Implements IConstructable

    Private m_TypeParameters As TypeParameters

    Sub New(ByVal Parent As ParsedObject, ByVal [Namespace] As String, ByVal Name As Identifier, ByVal TypeParameters As TypeParameters)
        MyBase.new(Parent, [Namespace], Name)
        m_TypeParameters = TypeParameters
    End Sub

    ReadOnly Property TypeParameters() As TypeParameters Implements IConstructable.TypeParameters
        Get
            Return m_TypeParameters
        End Get
    End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        If m_TypeParameters IsNot Nothing Then
            result = m_TypeParameters.ResolveTypeReferences AndAlso result
        End If
        result = MyBase.ResolveTypeReferences AndAlso result

        Return result
    End Function

    Public Overrides Function DefineTypeHierarchy() As Boolean
        Dim result As Boolean = True

        result = MyBase.DefineTypeHierarchy AndAlso result

        Return result
    End Function

    Public Overrides Function CreateDefinition() As Boolean
        Dim result As Boolean = True

        result = MyBase.CreateDefinition() AndAlso result

        If m_TypeParameters IsNot Nothing Then result = m_TypeParameters.CreateDefinition AndAlso result

        Return result
    End Function
End Class
