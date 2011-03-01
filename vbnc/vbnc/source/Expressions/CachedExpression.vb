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
Public Class CachedExpression
    Inherits Expression

    Private m_Expression As Expression

    Private m_Local As Mono.Cecil.Cil.VariableDefinition

    Sub New(ByVal Parent As Expression, ByVal Expression As Expression)
        MyBase.new(Parent)
        m_Expression = Expression
    End Sub

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        If m_Local Is Nothing Then
            result = m_Expression.GenerateCode(Info.Clone(Me, True, False, m_Expression.ExpressionType)) AndAlso result
            m_Local = Emitter.DeclareLocal(Info, m_Expression.ExpressionType)
            Emitter.EmitStoreVariable(Info, m_Local)
        End If

        Emitter.EmitLoadVariable(Info, m_Local)

        Return result
    End Function

    Overrides ReadOnly Property ExpressionType() As Mono.Cecil.TypeReference
        Get
            Return m_Expression.ExpressionType
        End Get
    End Property

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        If m_Expression.IsResolved = False Then
            result = m_Expression.ResolveExpression(Info)
        Else
            result = True
        End If

        Classification = New ValueClassification(Me, Me.ExpressionType)

        Return result
    End Function
End Class
