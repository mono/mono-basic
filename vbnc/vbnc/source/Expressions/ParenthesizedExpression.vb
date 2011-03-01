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
''' Classification: Value.
''' ParenthesizedExpression can only contain one expression.
''' 
''' ParenthesizedExpression  ::=  "("  Expression  ")"
''' </summary>
''' <remarks></remarks>
Public Class ParenthesizedExpression
    Inherits Expression

    Private m_Expression As Expression

    Public Overrides Function ResolveTypeReferences() As Boolean
        Return m_Expression.ResolveTypeReferences
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Expression As Expression)
        MyBase.New(Parent)
        m_Expression = Expression
    End Sub

    Sub Init(ByVal Expression As Expression)
        m_Expression = Expression
    End Sub

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        If Info.IsRHS Then
            If Me.Classification.IsValueClassification Then
                result = m_Expression.GenerateCode(Info) AndAlso result
            Else
                Throw New InternalException(Me)
            End If
        Else
            Throw New InternalException(Me)
        End If

        Return result
    End Function

    Shared Function IsMe(ByVal tm As tm) As Boolean
        Return tm.CurrentToken.Equals(KS.LParenthesis)
    End Function

    ReadOnly Property Expression() As Expression
        Get
            Return m_Expression
        End Get
    End Property

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = m_Expression.ResolveExpression(Info) AndAlso result

        If result = False Then Return result

        result = Helper.VerifyValueClassification(m_Expression, Info) AndAlso result

        Classification = New ValueClassification(Me, Me.ExpressionType)

        Return result
    End Function

    Overrides ReadOnly Property ExpressionType() As Mono.Cecil.TypeReference
        Get
            Return m_Expression.ExpressionType
        End Get
    End Property

    Public Overrides Function GetConstant(ByRef result As Object, ByVal ShowError As Boolean) As Boolean
        Return m_Expression.GetConstant(result, ShowError)
    End Function
End Class
