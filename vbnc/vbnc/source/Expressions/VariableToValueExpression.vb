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

Public Class VariableToValueExpression
    Inherits Expression

    Private m_Variable As VariableClassification
    Private m_ExpressionType As Type

    Sub New(ByVal Parent As ParsedObject, ByVal VariableClassification As VariableClassification)
        MyBase.new(Parent)
        m_Variable = VariableClassification
    End Sub

    Public Overrides ReadOnly Property IsConstant() As Boolean
        Get
            Return m_Variable.IsConstant
        End Get
    End Property

    Public Overrides ReadOnly Property ConstantValue() As Object
        Get
            Return m_Variable.ConstantValue
        End Get
    End Property

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        m_ExpressionType = m_Variable.Type

        If m_ExpressionType IsNot Nothing AndAlso m_ExpressionType.IsByRef Then
            m_ExpressionType = m_ExpressionType.GetElementType
        End If

        result = m_ExpressionType IsNot Nothing AndAlso result

        Me.Classification = New ValueClassification(Me)

        Return result
    End Function

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True
        Dim isByRef As Boolean = Info.DesiredType.IsByRef

        If m_Variable.InstanceExpression IsNot Nothing Then
            Dim exp As Type = m_Variable.InstanceExpression.ExpressionType
            If exp.IsValueType AndAlso exp.IsByRef = False Then
                exp = exp.MakeByRefType
            End If
            result = m_Variable.InstanceExpression.GenerateCode(Info.Clone(Me, exp)) AndAlso result
        End If

        If m_Variable.FieldInfo IsNot Nothing Then
            If Info.IsRHS Then
                Emitter.EmitLoadVariable(Info, m_Variable.FieldInfo)
            Else
                Return Compiler.Report.ShowMessage(Messages.VBNC99997, Location)
            End If
        ElseIf m_Variable.LocalBuilder IsNot Nothing Then
            If Info.IsRHS Then
                Emitter.EmitLoadVariable(Info, m_Variable.LocalBuilder)
            Else
                Return Compiler.Report.ShowMessage(Messages.VBNC99997, Location)
            End If
        ElseIf m_Variable.ParameterInfo IsNot Nothing Then
            Helper.Assert(m_Variable.InstanceExpression Is Nothing)
            If Info.IsRHS Then
                Emitter.EmitLoadParameter(Info, m_Variable.ParameterInfo)
                If m_Variable.ParameterInfo.ParameterType.IsByRef Then
                    Emitter.EmitLoadIndirect(Info, m_Variable.ParameterInfo.ParameterType)
                End If
            Else
                Return Compiler.Report.ShowMessage(Messages.VBNC99997, Location)
        End If
        ElseIf m_Variable.ArrayVariable IsNot Nothing Then
            result = Helper.EmitLoadArrayElement(Info, m_Variable.ArrayVariable, m_Variable.Arguments) AndAlso result
        ElseIf m_Variable.Expression IsNot Nothing Then
            result = m_Variable.Expression.GenerateCode(Info) AndAlso result
        ElseIf m_Variable.Method IsNot Nothing Then
            Emitter.EmitLoadVariable(Info, m_Variable.Method.DefaultReturnVariable)
        Else
            Throw New InternalException(Me)
        End If

        If Info.DesiredType.IsByRef Then
            Dim elementType As Type = Helper.GetTypeOrTypeBuilder(Info.DesiredType.GetElementType)
            Dim local As LocalBuilder
            local = Info.ILGen.DeclareLocal(elementType)

            Emitter.EmitStoreVariable(Info, local)
            Emitter.EmitLoadVariableLocation(Info, local)
        End If

        Return result
    End Function

    Overrides ReadOnly Property ExpressionType() As Type
        Get
            Return m_ExpressionType
        End Get
    End Property
End Class
