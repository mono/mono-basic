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

'Public Class MethodPointerToValueExpression
'    Inherits Expression

'    Private m_MethodPointer As MethodPointerClassification
'    Private m_ExpressionType As Type

'    Sub New(ByVal Parent As ParsedObject, ByVal MethodPointerClassification As MethodPointerClassification)
'        MyBase.new(Parent)
'        m_MethodPointer = MethodPointerClassification

'        If m_MethodPointer.Resolved Then
'            m_ExpressionType = m_MethodPointer.Type
'            Classification = New ValueClassification(Me, Me.ExpressionType)

'            If ResolveExpression(ResolveInfo.Default(Parent.Compiler)) = False Then Helper.ErrorRecoveryNotImplemented()
'        End If
'    End Sub

'    Public Overrides ReadOnly Property IsConstant() As Boolean
'        Get
'            Return False
'        End Get
'    End Property

'    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
'        Dim result As Boolean = True

'        result = m_ExpressionType IsNot Nothing AndAlso result

'        Me.Classification = New ValueClassification(Me)

'        Return result
'    End Function

'    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
'        Dim result As Boolean = True

'        Helper.Assert(m_MethodPointer.Method IsNot Nothing)
'        Helper.Assert(m_MethodPointer.DelegateType IsNot Nothing)

'        If m_MethodPointer.MethodGroup.InstanceExpression IsNot Nothing Then
'            result = m_MethodPointer.MethodGroup.InstanceExpression.GenerateCode(Info.Clone(True, False, m_MethodPointer.MethodGroup.InstanceExpression.ExpressionType)) AndAlso result
'            Emitter.EmitDup(Info)
'        Else
'            Emitter.EmitLoadNull(Info.Clone(True, False, Compiler.TypeCache.Object))
'        End If

'        Emitter.EmitLoadVftn(Info, m_MethodPointer.Method)

'        Dim ctor As ConstructorInfo
'        ctor = m_MethodPointer.DelegateType.GetConstructor(New Type() {Compiler.TypeCache.Object, Compiler.TypeCache.IntPtr})
'        Emitter.EmitNew(Info, ctor)


'        Return result
'    End Function

'    Overrides ReadOnly Property ExpressionType() As Type
'        Get
'            Return m_ExpressionType
'        End Get
'    End Property
'End Class
