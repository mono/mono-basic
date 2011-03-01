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
''' "TypeOf" Expression "Is" TypeName
''' Classification: Value (Type=Boolean)
''' </summary>
''' <remarks></remarks>
Public Class TypeOfExpression
    Inherits Expression

    Private m_Expression As Expression
    Private m_Type As TypeName
    ''' <summary>
    ''' Is or IsNot?
    ''' </summary>
    ''' <remarks></remarks>
    Private m_Is As Boolean

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        result = m_Expression.ResolveTypeReferences AndAlso result
        result = m_Type.ResolveTypeReferences AndAlso result

        Return result
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.new(parent)
    End Sub

    Sub Init(ByVal Expression As Expression, ByVal [Is] As Boolean, ByVal Type As TypeName)
        m_Expression = Expression
        m_Is = [Is]
        m_Type = Type
    End Sub

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        result = m_Expression.GenerateCode(Info.Clone(Me, True, False, m_Expression.ExpressionType)) AndAlso result
        If CecilHelper.IsGenericParameter(m_Expression.ExpressionType) Then
            Emitter.EmitBox(Info, m_Expression.ExpressionType)
        End If
        Emitter.EmitIsInst(Info, m_Expression.ExpressionType, m_Type.ResolvedType)

        Emitter.EmitLoadNull(Info.Clone(Me, True, False, Compiler.TypeCache.System_Object))
        If m_Is Then
            Emitter.EmitNotEquals(Info, m_Type.ResolvedType)
        Else
            Emitter.EmitEquals(Info, m_Type.ResolvedType)
        End If

        Return result
    End Function

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = m_Expression.ResolveExpression(Info) AndAlso result
        result = m_Type.ResolveTypeReferences AndAlso result

        Classification = New ValueClassification(Me)

        If m_Expression.Classification.IsValueClassification Then
        ElseIf m_Expression.Classification.CanBeValueClassification Then
            m_Expression = m_Expression.ReclassifyToValueExpression()
            result = m_Expression.ResolveExpression(Info) AndAlso result
        Else
            result = Helper.AddError(Me) AndAlso result
        End If

        Return result
    End Function

    Overrides ReadOnly Property ExpressionType() As Mono.Cecil.TypeReference
        Get
            Return Compiler.TypeCache.System_Boolean
        End Get
    End Property
End Class
