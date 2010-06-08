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

Public Class LoadFieldExpression
    Inherits Expression

    Private m_Field As Mono.Cecil.FieldReference
    Private m_InstanceExpression As Expression

    ReadOnly Property Field() As Mono.Cecil.FieldReference
        Get
            Return m_Field
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject, ByVal Field As Mono.Cecil.FieldReference, Optional ByVal InstanceExpression As Expression = Nothing)
        MyBase.New(Parent)

        m_Field = Field
        m_InstanceExpression = InstanceExpression
        Me.Classification = New ValueClassification(Me, m_Field.FieldType)
    End Sub

    Overrides ReadOnly Property ExpressionType() As Mono.Cecil.TypeReference
        Get
            Return m_Field.FieldType
        End Get
    End Property

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        If Me.Classification IsNot Nothing Then
            Me.Classification = New ValueClassification(Me, m_Field.FieldType)
        End If

        Return result
    End Function

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        If m_InstanceExpression IsNot Nothing Then
            result = m_InstanceExpression.GenerateCode(Info) AndAlso result
        End If

        Emitter.EmitLoadVariable(Info, m_Field)

        Return result
    End Function

End Class
