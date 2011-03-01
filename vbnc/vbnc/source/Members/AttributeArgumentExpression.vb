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
''' AttributeArgumentExpression  ::=
'''   ConstantExpression  |
'''   GetTypeExpression  |
'''   ArrayCreationExpression
''' </summary>
''' <remarks></remarks>
Public Class AttributeArgumentExpression
    Inherits ParsedObject

    Private m_Expression As Expression

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal Expression As Expression)
        m_Expression = Expression
    End Sub

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = m_Expression.ResolveExpression(info) AndAlso result

        Return result
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        result = m_Expression.ResolveTypeReferences AndAlso result

        Return result
    End Function

    ReadOnly Property Expression() As Expression
        Get
            Return m_Expression
        End Get
    End Property

    Shared Function CanBeMe(ByVal tm As tm) As Boolean
        Return Not VariablePropertyInitializer.IsMe(tm)
    End Function

End Class
