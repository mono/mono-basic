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
''' Classification: Value
''' 
''' If value is nothing and type is nothing it is a nothing literal expression.
''' </summary>
''' <remarks></remarks>
Public Class ConstantExpression
    Inherits Expression

    Private m_Value As Object
    Private m_ExpressionType As Mono.Cecil.TypeReference

    ''' <summary>
    ''' Only sets the classification for this expression (Value).
    ''' Always returns True.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        If m_Value IsNot Nothing Then
            Classification = New ValueClassification(Me, m_ExpressionType, m_Value)
        Else
            Classification = New ValueClassification(Me, m_ExpressionType, DBNull.Value)
        End If
        Return True
    End Function

    Protected Sub Init(ByVal Value As Object, ByVal ExpressionType As Mono.Cecil.TypeReference)
        m_Value = Value
        m_ExpressionType = ExpressionType
    End Sub

    Overrides ReadOnly Property ExpressionType() As Mono.Cecil.TypeReference
        Get
            Return m_ExpressionType
        End Get
    End Property

    Protected Sub New(ByVal Parent As ParsedObject)
        MyBase.new(Parent)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Constant As Object, ByVal ExpressionType As Mono.Cecil.TypeReference)
        MyBase.New(Parent)
        m_Value = Constant
        m_ExpressionType = ExpressionType
    End Sub

    Public Overrides Function GetConstant(ByRef result As Object, ByVal ShowError As Boolean) As Boolean
        result = m_Value
        Return True
    End Function
End Class