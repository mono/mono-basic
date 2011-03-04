' 
' Visual Basic.Net Compiler
' Copyright (C) 2010 Rolf Bjarne Kvinge, RKvinge@novell.com
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
''' </summary>
''' <remarks></remarks>
Public Class IfExpression
    Inherits Expression

    Public Condition As Expression
    Public SecondPart As Expression
    Public ThirdPart As Expression

    Private m_ExpressionType As TypeReference

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Public Overrides Function GetConstant(ByRef result As Object, ByVal ShowError As Boolean) As Boolean
        Dim first As Object = Nothing
        Dim second As Object = Nothing
        Dim third As Object = Nothing

        If ThirdPart IsNot Nothing Then
            If Not Condition.GetConstant(first, ShowError) Then Return False
            If Not SecondPart.GetConstant(second, ShowError) Then Return False
            If Not ThirdPart.GetConstant(third, ShowError) Then Return False

            result = If(CBool(first), second, third)
            Return TypeConverter.ConvertTo(Me, result, m_ExpressionType, result, ShowError)
        End If

        If TypeOf SecondPart Is NothingConstantExpression Then
            If TypeOf Condition Is NothingConstantExpression Then
                result = Nothing
                Return True
            Else
                If ShowError Then Show30059()
                Return False
            End If
        ElseIf TypeOf Condition Is NothingConstantExpression Then
            If ShowError Then Show30059()
            Return False
        Else
            If Not Condition.GetConstant(first, ShowError) Then Return False
            If Not SecondPart.GetConstant(second, ShowError) Then Return False

            Return TypeConverter.ConvertTo(Me, result, m_ExpressionType, result, ShowError)
        End If

        If ShowError Then Show30059()
        Return False
    End Function

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = Condition.ResolveExpression(Info) AndAlso result
        result = SecondPart.ResolveExpression(Info) AndAlso result

        If ThirdPart IsNot Nothing Then
            result = ThirdPart.ResolveExpression(Info) AndAlso result
        End If

        If Not result Then Return result

        If ThirdPart IsNot Nothing Then
            If Compiler.TypeResolution.IsImplicitlyConvertible(Me, Condition.ExpressionType, Compiler.TypeCache.System_Boolean) = False AndAlso Location.File(Compiler).IsOptionStrictOn Then
                result = Compiler.Report.ShowMessage(Messages.VBNC30512, Condition.Location, Condition.ExpressionType.Name, Compiler.TypeCache.System_Boolean.Name) AndAlso result
            Else
                Condition = Helper.CreateTypeConversion(Me, Condition, Compiler.TypeCache.System_Boolean, result)
            End If

            If Helper.CompareType(SecondPart.ExpressionType, ThirdPart.ExpressionType) = False Then
                If Compiler.TypeResolution.IsImplicitlyConvertible(Me, SecondPart.ExpressionType, ThirdPart.ExpressionType) Then
                    m_ExpressionType = ThirdPart.ExpressionType
                    SecondPart = Helper.CreateTypeConversion(Me, SecondPart, m_ExpressionType, result)
                ElseIf Compiler.TypeResolution.IsImplicitlyConvertible(Me, ThirdPart.ExpressionType, SecondPart.ExpressionType) Then
                    m_ExpressionType = SecondPart.ExpressionType
                    ThirdPart = Helper.CreateTypeConversion(Me, ThirdPart, m_ExpressionType, result)
                Else
                    result = Compiler.Report.ShowMessage(Messages.VBNC33106, Me.Location) AndAlso result
                End If
            Else
                m_ExpressionType = SecondPart.ExpressionType
            End If
        Else
            If Condition.ExpressionType.IsValueType AndAlso CecilHelper.IsNullable(Condition.ExpressionType) = False Then
                Return Compiler.Report.ShowMessage(Messages.VBNC33107, Condition.Location) AndAlso result
            End If

            If TypeOf SecondPart Is NothingConstantExpression Then
                If TypeOf Condition Is NothingConstantExpression Then
                    m_ExpressionType = Compiler.TypeCache.System_Object
                Else
                    Return Compiler.Report.ShowMessage(Messages.VBNC30512, Condition.Location, Condition.ExpressionType.Name, "Integer")
                End If
            ElseIf TypeOf Condition Is NothingConstantExpression Then
                If Helper.CompareType(SecondPart.ExpressionType, Compiler.TypeCache.System_Int32) Then
                    Return Compiler.Report.ShowMessage(Messages.VBNC33110, Me.Location)
                Else
                    Return Compiler.Report.ShowMessage(Messages.VBNC30512, Condition.Location, SecondPart.ExpressionType.Name, "Integer")
                End If
            Else
                If Helper.CompareType(SecondPart.ExpressionType, Condition.ExpressionType) = False Then
                    Dim condType As TypeReference = Condition.ExpressionType

                    If CecilHelper.IsNullable(Condition.ExpressionType) AndAlso Not CecilHelper.IsNullable(SecondPart.ExpressionType) Then
                        condType = CecilHelper.GetNulledType(condType)
                    End If

                    If Compiler.TypeResolution.IsImplicitlyConvertible(Me, SecondPart.ExpressionType, condType) Then
                        m_ExpressionType = condType
                        SecondPart = Helper.CreateTypeConversion(Me, SecondPart, m_ExpressionType, result)
                    ElseIf Compiler.TypeResolution.IsImplicitlyConvertible(Me, condType, SecondPart.ExpressionType) Then
                        m_ExpressionType = SecondPart.ExpressionType
                    Else
                        result = Compiler.Report.ShowMessage(Messages.VBNC33110, Me.Location) AndAlso result
                    End If
                Else
                    m_ExpressionType = SecondPart.ExpressionType
                End If
            End If
        End If

        If result Then
            Me.Classification = New ValueClassification(Me, Me)
        End If

        Return result
    End Function

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True
        Dim falseLabel As Label = Emitter.DefineLabel(Info)
        Dim endLabel As Label = Emitter.DefineLabel(Info)
        Dim local As Mono.Cecil.Cil.VariableDefinition
        Dim vose As ValueOnStackExpression
        Dim type_conversion As Expression
        Dim nullable_type As GenericInstanceType = Nothing
        Dim has_value As MethodReference
        Dim get_value As MethodReference

        If ThirdPart IsNot Nothing Then
            result = Condition.GenerateCode(Info) AndAlso result
            Emitter.EmitBranchIfFalse(Info, falseLabel)
            result = SecondPart.GenerateCode(Info) AndAlso result
            Emitter.EmitBranch(Info, endLabel)
            Emitter.MarkLabel(Info, falseLabel)
            result = ThirdPart.GenerateCode(Info) AndAlso result
        Else
            'Emit condition, and store the result in a local variable
            local = Emitter.DeclareLocal(Info, Condition.ExpressionType)
            result = Condition.GenerateCode(Info) AndAlso result
            Emitter.EmitStoreVariable(Info, local)

            'If the first operand (condition) is nullable and the second is not, the result is the dominant type 
            'between the nulled type of the first operand and the type of the second operand.
            'This means that for this case:
            ' - True condition: denullify the first operand + convert to proper type
            ' - False condition: just convert to proper type
            'For the normal case:
            ' - Both conditions: convert to proper type

            'conditions
            If CecilHelper.IsNullable(Condition.ExpressionType) Then
                nullable_type = New GenericInstanceType(Helper.GetTypeOrTypeReference(Compiler, Compiler.TypeCache.System_Nullable1))
                nullable_type.GenericArguments.Add(Helper.GetTypeOrTypeReference(Compiler, CecilHelper.GetNulledType(Condition.ExpressionType)))

                'Call Nullable`1.HasValue to check the condition
                has_value = New MethodReference("get_HasValue", Helper.GetTypeOrTypeReference(Compiler, Compiler.TypeCache.System_Boolean))
                has_value.DeclaringType = nullable_type
                has_value.HasThis = True
                Emitter.EmitLoadVariableLocation(Info, local)
                Emitter.EmitCall(Info, has_value)
                Emitter.EmitBranchIfFalse(Info, falseLabel)
            Else
                Emitter.EmitLoadVariable(Info, local)
                Emitter.EmitBranchIfFalse(Info, falseLabel)
            End If

            'true branch
            If CecilHelper.IsNullable(Condition.ExpressionType) AndAlso CecilHelper.IsNullable(m_ExpressionType) = False Then
                'denullify
                get_value = New MethodReference("GetValueOrDefault", Compiler.TypeCache.System_Nullable1.GenericParameters(0))
                get_value.DeclaringType = nullable_type
                get_value.HasThis = True
                Emitter.EmitLoadVariableLocation(Info, local)
                Emitter.EmitCall(Info, get_value)
                'convert to proper type
                vose = New ValueOnStackExpression(Me, CecilHelper.GetNulledType(Condition.ExpressionType))
            Else
                Emitter.EmitLoadVariable(Info, local)
                vose = New ValueOnStackExpression(Me, Condition.ExpressionType)
            End If
            type_conversion = Helper.CreateTypeConversion(Me, vose, m_ExpressionType, result)
            result = type_conversion.GenerateCode(Info) AndAlso result
            Emitter.EmitBranch(Info, endLabel)

            'false branch
            Emitter.MarkLabel(Info, falseLabel)
            result = SecondPart.GenerateCode(Info) AndAlso result
            If CecilHelper.IsNullable(SecondPart.ExpressionType) AndAlso CecilHelper.IsNullable(m_ExpressionType) = False Then
                nullable_type = New GenericInstanceType(Helper.GetTypeOrTypeReference(Compiler, Compiler.TypeCache.System_Nullable1))
                nullable_type.GenericArguments.Add(Helper.GetTypeOrTypeReference(Compiler, CecilHelper.GetNulledType(SecondPart.ExpressionType)))

                'denullify
                get_value = New MethodReference("GetValueOrDefault", Compiler.TypeCache.System_Nullable1.GenericParameters(0))
                get_value.DeclaringType = nullable_type
                get_value.HasThis = True
                Emitter.EmitLoadVariableLocation(Info, local)
                Emitter.EmitCall(Info, get_value)
                'convert to proper type
                vose = New ValueOnStackExpression(Me, CecilHelper.GetNulledType(SecondPart.ExpressionType))
            Else
                vose = New ValueOnStackExpression(Me, SecondPart.ExpressionType)
            End If
            type_conversion = Helper.CreateTypeConversion(Me, vose, m_ExpressionType, result)
            result = type_conversion.GenerateCode(Info) AndAlso result
        End If
        Emitter.MarkLabel(Info, endLabel)

        Return result
    End Function

    Public Overrides ReadOnly Property ExpressionType() As Mono.Cecil.TypeReference
        Get
            Return m_ExpressionType
        End Get
    End Property
End Class
