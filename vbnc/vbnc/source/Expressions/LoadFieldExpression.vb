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

Public Class LoadFieldExpression
    Inherits Expression

    Private m_Field As FieldInfo

    ReadOnly Property Field() As fieldinfo
        Get
            Return m_Field
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject, ByVal Field As FieldInfo)
        MyBase.New(Parent)

        m_Field = Field
        Me.Classification = New ValueClassification(Me, m_Field.FieldType)
    End Sub

    Overrides ReadOnly Property ExpressionType() As Type
        Get
            Return m_Field.FieldType
        End Get
    End Property

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean

        If Me.Classification IsNot Nothing Then
            Me.Classification = New ValueClassification(Me, m_Field.FieldType)
        End If

        Return result
    End Function

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Emitter.EmitLoadVariable(Info, m_Field)

        Return result
    End Function

End Class
