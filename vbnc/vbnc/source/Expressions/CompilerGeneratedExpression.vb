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

Public Class CompilerGeneratedExpression
    Inherits Expression

    Delegate Function GenerateCodeDelegate(ByVal Info As EmitInfo) As Boolean

    Private m_Delegate As GenerateCodeDelegate
    Private m_ExpressionType As Type

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Return m_Delegate(Info)
    End Function

    Sub New(ByVal Parent As ParsedObject, ByVal CodeGenerator As GenerateCodeDelegate, ByVal ExpressionType As Type)
        MyBase.new(Parent)
        m_Delegate = CodeGenerator
        m_ExpressionType = ExpressionType
        MyBase.Classification = New ValueClassification(Me)
    End Sub

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Return True
    End Function

    Overrides ReadOnly Property ExpressionType() As Type
        Get

            Return m_ExpressionType
        End Get
    End Property
End Class
