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

Public Class DeRefExpression
    Inherits Expression

    Private m_Expression As Expression
    Private m_ExpressionType As Type

    ''' <summary>
    ''' Automatically resolved.
    ''' </summary>
    ''' <param name="Parent"></param>
    ''' <param name="Expression"></param>
    ''' <remarks></remarks>
    Sub New(ByVal Parent As ParsedObject, ByVal Expression As Expression)
        MyBase.new(Parent)
        m_Expression = Expression
        m_ExpressionType = Expression.ExpressionType.GetElementType

        Classification = New VariableClassification(Me, Expression, m_ExpressionType)

        If MyBase.ResolveExpression(ResolveInfo.Default(Parent.Compiler)) = False Then Helper.ErrorRecoveryNotImplemented()
    End Sub

    ReadOnly Property Expression() As Expression
        Get
            Return m_Expression
        End Get
    End Property

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        result = m_Expression.GenerateCode(Info.Clone(Me, True, False, m_Expression.ExpressionType)) AndAlso result

        Emitter.EmitLoadIndirect(Info, m_Expression.ExpressionType)

        Return result
    End Function

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        Return result
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        Me.CheckTypeReferencesNotResolved()

        Return result
    End Function

    Overrides ReadOnly Property ExpressionType() As Type
        Get
            Return m_ExpressionType
        End Get
    End Property
End Class
