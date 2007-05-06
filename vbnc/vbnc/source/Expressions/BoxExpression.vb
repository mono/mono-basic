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

Public Class BoxExpression
    Inherits Expression

    Private m_DestinationType As Type
    Private m_Expression As Expression

    ''' <summary>
    ''' Automatically resolved.
    ''' </summary>
    ''' <param name="Parent"></param>
    ''' <param name="Expression"></param>
    ''' <param name="DestinationType"></param>
    ''' <remarks></remarks>
    Sub New(ByVal Parent As ParsedObject, ByVal Expression As Expression, ByVal DestinationType As Type)
        MyBase.new(Parent)
        m_DestinationType = DestinationType
        m_Expression = Expression

        Helper.Assert(m_DestinationType IsNot Nothing)
        Helper.Assert(m_Expression IsNot Nothing)
        Helper.Assert(m_Expression.IsResolved)

        Classification = New ValueClassification(Me, m_DestinationType)

        If MyBase.ResolveExpression(ResolveInfo.Default(Compiler)) = False Then
            Helper.ErrorRecoveryNotImplemented()
        End If

    End Sub

    Public Overrides ReadOnly Property IsConstant() As Boolean
        Get
            Return False
        End Get
    End Property

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Return True
    End Function

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        result = m_Expression.GenerateCode(Info) AndAlso result
        Emitter.EmitBox(Info, m_Expression.ExpressionType)

        Return result
    End Function

    Overrides ReadOnly Property ExpressionType() As Type
        Get
            Return m_DestinationType
        End Get
    End Property

End Class
