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

Public Class VariableExpression
    Inherits CompilerGeneratedExpression

    Private m_Variable As LocalVariableDeclaration

    Sub New(ByVal Parent As ParsedObject, ByVal Variable As LocalVariableDeclaration)
        MyBase.New(Parent, Nothing, Variable.VariableType)

        m_Variable = Variable
        Classification = New VariableClassification(Me, m_Variable)
    End Sub

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Return Emit(Info, m_Variable.LocalBuilder)
    End Function

    Shared Function Emit(ByVal Info As EmitInfo, ByVal LocalBuilder As Mono.Cecil.Cil.VariableDefinition) As Boolean
        Dim result As Boolean = True

        If Info.IsRHS Then
            Emitter.EmitLoadVariable(Info, LocalBuilder)
        Else
            Dim rInfo As EmitInfo = Info.Clone(Info.Context, True, False, LocalBuilder.VariableType)

            Helper.Assert(Info.RHSExpression IsNot Nothing, "RHSExpression Is Nothing!")
            Helper.Assert(Info.RHSExpression.Classification.IsValueClassification OrElse Info.RHSExpression.Classification.CanBeValueClassification)
            result = Info.RHSExpression.Classification.GenerateCode(rInfo) AndAlso result

            Emitter.EmitConversion(Info.RHSExpression.ExpressionType, LocalBuilder.VariableType, Info)

            If Helper.CompareType(LocalBuilder.VariableType, Info.Compiler.TypeCache.System_Object) AndAlso Helper.CompareType(Info.RHSExpression.ExpressionType, Info.Compiler.TypeCache.System_Object) Then
                Emitter.EmitCall(Info, Info.Compiler.TypeCache.System_Runtime_CompilerServices_RuntimeHelpers__GetObjectValue_Object)
            End If

            Emitter.EmitStoreVariable(Info, LocalBuilder)
        End If

        Return True
    End Function
End Class
