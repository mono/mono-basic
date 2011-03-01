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

Public Class MethodGroupToValueExpression
    Inherits Expression

    Private m_MethodGroup As MethodGroupClassification
    Private m_ExpressionType As Mono.Cecil.TypeReference

    Sub New(ByVal Parent As ParsedObject, ByVal MethodGroupClassification As MethodGroupClassification)
        MyBase.new(Parent)
        m_MethodGroup = MethodGroupClassification
    End Sub

    Public Overrides Function GetConstant(ByRef result As Object, ByVal ShowError As Boolean) As Boolean
        Return m_MethodGroup.GetConstant(result, ShowError)
    End Function

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True
        Dim arguments As ArgumentList = New ArgumentList(Me.Parent)

        If m_MethodGroup.Resolved = False Then
            result = m_MethodGroup.ResolveGroup(arguments) AndAlso result
        Else
            'm_FinalArguments = m_MethodGroup.
        End If

        If result = False Then
            result = m_MethodGroup.ResolveGroup(arguments, True) AndAlso result
            Return False
        End If

        Helper.Assert(m_MethodGroup.ResolvedMethod IsNot Nothing)

        If m_MethodGroup.ResolvedMethodInfo IsNot Nothing Then
            m_ExpressionType = m_MethodGroup.ResolvedMethodInfo.ReturnType
        ElseIf m_MethodGroup.ResolvedConstructor IsNot Nothing Then
            m_ExpressionType = m_MethodGroup.ResolvedConstructor.DeclaringType
        Else
            Throw New InternalException(Me)
        End If

        result = m_ExpressionType IsNot Nothing AndAlso result

        If Helper.CompareType(Compiler.TypeCache.System_Void, m_ExpressionType) Then
            Me.Classification = New VoidClassification(Me)
            result = ReportReclassifyToValueErrorMessage()
        Else
            Me.Classification = New ValueClassification(Me)
        End If

        Return result
    End Function

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Helper.Assert(m_MethodGroup.Resolved)

        Helper.EmitArgumentsAndCallOrCallVirt(Info, m_MethodGroup.InstanceExpression, m_MethodGroup.FinalArguments, Helper.GetMethodOrMethodReference(Compiler, m_MethodGroup.ResolvedMethod))

        Return result
    End Function

    Overrides ReadOnly Property ExpressionType() As Mono.Cecil.TypeReference
        Get
            Return m_ExpressionType
        End Get
    End Property
End Class
