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

Public Class PropertyAccessToValueExpression
    Inherits Expression

    Private m_PropertyAccess As PropertyAccessClassification
    Private m_ExpressionType As Type

    Public Overrides ReadOnly Property AsString() As String
        Get
            Dim result As String = ""
            If m_PropertyAccess.InstanceExpression IsNot Nothing Then
                result = m_PropertyAccess.InstanceExpression.AsString & "."
            End If
            result &= m_PropertyAccess.ResolvedProperty.Name
            result &= "(" & m_PropertyAccess.Parameters.AsString & ")"
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property IsConstant() As Boolean
        Get
            Return m_PropertyAccess.IsConstant
        End Get
    End Property

    Public Overrides ReadOnly Property ConstantValue() As Object
        Get
            Return m_PropertyAccess.ConstantValue
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject, ByVal PropertyAccess As PropertyAccessClassification)
        MyBase.new(Parent)
        m_PropertyAccess = PropertyAccess
    End Sub

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        m_ExpressionType = m_PropertyAccess.Type

        result = m_ExpressionType IsNot Nothing AndAlso result

        Me.Classification = New ValueClassification(Me)

        Return result
    End Function

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True
        Dim method As MethodInfo

        method = m_PropertyAccess.ResolvedProperty.GetGetMethod(True)

        result = Helper.EmitArgumentsAndCallOrCallVirt(Info, m_PropertyAccess.InstanceExpression, m_PropertyAccess.Parameters, method) AndAlso result

        Return result
    End Function

    Overrides ReadOnly Property ExpressionType() As Type
        Get
            Return m_ExpressionType
        End Get
    End Property
End Class
