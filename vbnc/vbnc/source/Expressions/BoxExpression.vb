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

Public Class BoxExpression
    Inherits Expression

    Private m_DestinationType As Mono.Cecil.TypeReference
    Private m_Expression As Expression

    ''' <summary>
    ''' Automatically resolved.
    ''' </summary>
    ''' <param name="Parent"></param>
    ''' <param name="Expression"></param>
    ''' <param name="DestinationType"></param>
    ''' <remarks></remarks>
    Sub New(ByVal Parent As ParsedObject, ByVal Expression As Expression, ByVal DestinationType As Mono.Cecil.TypeReference)
        MyBase.new(Parent)
        m_DestinationType = DestinationType
        m_Expression = Expression

        Helper.Assert(m_DestinationType IsNot Nothing)
        Helper.Assert(m_Expression IsNot Nothing)
        Helper.Assert(m_Expression.IsResolved)
        Helper.Assert(TypeOf Expression Is BoxExpression = False)

        Classification = New ValueClassification(Me, m_DestinationType)

        If MyBase.ResolveExpression(ResolveInfo.Default(Compiler)) = False Then
            Helper.ErrorRecoveryNotImplemented(Me.Location)
        End If

    End Sub

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Return True
    End Function

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True
        Dim tp As TypeReference

        result = m_Expression.GenerateCode(Info) AndAlso result

        tp = m_Expression.ExpressionType
        If CecilHelper.IsByRef(tp) Then
            tp = CecilHelper.GetElementType(tp)
        End If
        Emitter.EmitBox(Info, tp)

        Return result
    End Function

    Overrides ReadOnly Property ExpressionType() As Mono.Cecil.TypeReference
        Get
            Return m_DestinationType
        End Get
    End Property

End Class
