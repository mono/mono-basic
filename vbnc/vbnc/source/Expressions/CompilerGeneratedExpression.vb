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

Public Class CompilerGeneratedExpression
    Inherits Expression

    Delegate Function GenerateCodeDelegate(ByVal Info As EmitInfo) As Boolean

    Protected m_Delegate As GenerateCodeDelegate
    Private m_ExpressionType As Mono.Cecil.TypeReference

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Return m_Delegate(Info)
    End Function

    Sub New(ByVal Parent As ParsedObject, ByVal CodeGenerator As GenerateCodeDelegate, ByVal ExpressionType As Mono.Cecil.TypeReference)
        MyBase.new(Parent)
        m_Delegate = CodeGenerator
        m_ExpressionType = ExpressionType
        MyBase.Classification = New ValueClassification(Me)
    End Sub

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Return True
    End Function

    Overrides ReadOnly Property ExpressionType() As Mono.Cecil.TypeReference
        Get
            Return m_ExpressionType
        End Get
    End Property
End Class

Public Class LoadLocalExpression
    Inherits CompilerGeneratedExpression

    Private m_Local As Mono.Cecil.Cil.VariableDefinition
    
    Sub New(ByVal Parent As ParsedObject, ByVal Local As Mono.Cecil.Cil.VariableDefinition)
        MyBase.New(Parent, Nothing, Local.VariableType)
        MyBase.m_Delegate = New CompilerGeneratedExpression.GenerateCodeDelegate(AddressOf GenerateCodeInternal)
        m_Local = Local
    End Sub

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Helper.Assert(m_Local IsNot Nothing)

        If Info.IsRHS Then
            If CecilHelper.IsByRef(Info.DesiredType) Then
                Emitter.EmitLoadVariableLocation(Info, m_Local)
            Else
                Emitter.EmitLoadVariable(Info, m_Local)
            End If
        Else
            If Info.RHSExpression IsNot Nothing Then
                result = Info.RHSExpression.GenerateCode(Info.Clone(Me, True, , m_Local.VariableType)) AndAlso result
            End If
            Emitter.EmitStoreVariable(Info, m_Local)
        End If

        Return result
    End Function
End Class

Public Class LoadElementExpression
    Inherits CompilerGeneratedExpression

    Private m_Local As Mono.Cecil.Cil.VariableDefinition
    Private m_Index As Integer

    Sub New(ByVal Parent As ParsedObject, ByVal Local As Mono.Cecil.Cil.VariableDefinition, ByVal Index As Integer)
        MyBase.New(Parent, Nothing, Local.VariableType)
        MyBase.m_Delegate = New CompilerGeneratedExpression.GenerateCodeDelegate(AddressOf GenerateCodeInternal)
        m_Local = Local
        m_Index = Index
    End Sub

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Helper.Assert(Info.IsRHS)

        Emitter.EmitLoadVariable(Info, m_Local)
        Emitter.EmitLoadI4Value(Info, m_Index)
        Emitter.EmitLoadElement(Info, m_Local.VariableType)

        Return result
    End Function
End Class

Public Class ValueOnStackExpression
    Inherits CompilerGeneratedExpression

    Sub New(ByVal Parent As ParsedObject, ByVal ExpressionType As Mono.Cecil.TypeReference)
        MyBase.New(Parent, Nothing, ExpressionType)
        MyBase.m_Delegate = New CompilerGeneratedExpression.GenerateCodeDelegate(AddressOf GenerateCodeInternal)
    End Sub

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Return True
    End Function
End Class