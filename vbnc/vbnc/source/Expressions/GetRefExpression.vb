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
''' 
''' </summary>
''' <remarks></remarks>
Public Class GetRefExpression
    Inherits Expression

    Private m_Expression As Expression
    Private m_ExpressionType As Mono.Cecil.TypeReference

    ReadOnly Property Expression() As Expression
        Get
            Return m_Expression
        End Get
    End Property

    ''' <summary>
    ''' Automatically resolved.
    ''' </summary>
    ''' <param name="Parent"></param>
    ''' <param name="Expression"></param>
    ''' <remarks></remarks>
    Sub New(ByVal Parent As ParsedObject, ByVal Expression As Expression)
        MyBase.new(Parent)
        m_Expression = Expression
        m_ExpressionType = Parent.Compiler.TypeManager.MakeByRefType(Parent, Expression.ExpressionType)

        If MyBase.ResolveExpression(ResolveInfo.Default(Parent.Compiler)) = False Then
            Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
            Throw New InternalException(Me)
        End If
    End Sub

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        Classification = New ValueClassification(Me, m_ExpressionType)

        Return result
    End Function

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Dim refInfo As EmitInfo = Info.Clone(Me, Me.ExpressionType)

        Select Case m_Expression.Classification.Classification
            Case ExpressionClassification.Classifications.Variable
                Dim varC As VariableClassification = m_Expression.Classification.AsVariableClassification

                If varC.InstanceExpression IsNot Nothing Then
                    Dim desiredType As Mono.Cecil.TypeReference
                    desiredType = varC.InstanceExpression.ExpressionType
                    If CecilHelper.IsValueType(desiredType) AndAlso CecilHelper.IsByRef(desiredType) = False Then
                        desiredType = CecilHelper.MakeByRefType(desiredType)
                    End If
                    result = varC.InstanceExpression.GenerateCode(Info.Clone(Me, desiredType)) AndAlso result
                    'result = varC.InstanceExpression.GenerateCode(refInfo) AndAlso result
                End If

                If varC.LocalBuilder IsNot Nothing Then
                    Emitter.EmitLoadVariableLocation(refInfo, varC.LocalBuilder)
                ElseIf varC.ParameterInfo IsNot Nothing Then
                    Emitter.EmitLoadVariableLocation(refInfo, varC.ParameterInfo)
                ElseIf varC.FieldInfo IsNot Nothing Then
                    If varC.FieldDefinition.IsLiteral Then
                        Dim local As Mono.Cecil.Cil.VariableDefinition
                        local = Emitter.DeclareLocal(Info, varC.FieldInfo.FieldType)
                        Emitter.EmitLoadVariable(Info, varC.FieldInfo)
                        Emitter.EmitStoreVariable(Info, local)
                        Emitter.EmitLoadVariableLocation(refInfo, local)
                    Else
                        Emitter.EmitLoadVariableLocation(refInfo, varC.FieldInfo)
                    End If
                ElseIf varC.ArrayVariable IsNot Nothing Then
                    Dim arrtype As Mono.Cecil.TypeReference = varC.ArrayVariable.ExpressionType
                    Dim elementtype As Mono.Cecil.TypeReference = CecilHelper.GetElementType(arrtype)
                    Dim isnonprimitivevaluetype As Boolean = CecilHelper.IsPrimitive(Compiler, elementtype) = False AndAlso CecilHelper.IsValueType(elementtype)

                    result = varC.ArrayVariable.GenerateCode(Info.Clone(Me, True, False, arrtype)) AndAlso result

                    Dim methodtypes As New Generic.List(Of Mono.Cecil.TypeReference)

                    Dim elementInfo As EmitInfo = Info.Clone(Me, True, False, Compiler.TypeCache.System_Int32)
                    For i As Integer = 0 To varC.Arguments.Count - 1
                        result = varC.Arguments(i).GenerateCode(elementInfo) AndAlso result
                        Emitter.EmitConversion(varC.Arguments(i).Expression.ExpressionType, Compiler.TypeCache.System_Int32, Info)
                        methodtypes.Add(Compiler.TypeCache.System_Int32)
                    Next

                    Dim rInfo As EmitInfo = Info.Clone(Me, True, False, elementtype)
                    methodtypes.Add(elementtype)

                    If CecilHelper.GetArrayRank(arrtype) = 1 Then
                        If isnonprimitivevaluetype Then
                            Emitter.EmitLoadElementAddress(Info, elementtype, arrtype)
                            'result = Info.RHSExpression.Classification.GenerateCode(rInfo) AndAlso result
                            'Emitter.EmitStoreObject(Info, elementtype)
                        Else
                            Emitter.EmitLoadElementAddress(Info, elementtype, arrtype)
                            'result = Info.RHSExpression.Classification.GenerateCode(rInfo) AndAlso result
                            'Emitter.EmitStoreElement(Info, elementtype, arrtype)
                        End If
                    Else
                        Dim method As Mono.Cecil.MethodReference = ArrayElementInitializer.GetAddressMethod(Compiler, arrtype)
                        Emitter.EmitCallVirt(Info, method)
                    End If
                ElseIf varC.Expression IsNot Nothing Then
                    If TypeOf varC.Expression Is MeExpression Then
                        Dim local As Mono.Cecil.Cil.VariableDefinition
                        local = Emitter.DeclareLocal(Info, varC.Expression.ExpressionType)
                        Emitter.EmitLoadMe(Info, varC.Expression.ExpressionType)
                        Emitter.EmitStoreVariable(Info, local)
                        Emitter.EmitLoadVariableLocation(refInfo, local)
                    ElseIf TypeOf varC.Expression Is GetRefExpression AndAlso varC.Expression IsNot Me Then
                        result = varC.Expression.GenerateCode(Info) AndAlso result
                    Else
                        Return Compiler.Report.ShowMessage(Messages.VBNC99997, Parent.Location)
                    End If
                ElseIf varC.Method IsNot Nothing Then
                    If varC.Method.DefaultReturnVariable Is Nothing Then
                        Return Compiler.Report.ShowMessage(Messages.VBNC99997, Parent.Location)
                    Else
                        Emitter.EmitLoadVariableLocation(refInfo, varC.Method.DefaultReturnVariable)
                    End If
                Else
                    Return Compiler.Report.ShowMessage(Messages.VBNC99997, Parent.Location)
                End If
            Case ExpressionClassification.Classifications.Value
                result = m_Expression.GenerateCode(Info.Clone(Me, m_Expression.ExpressionType)) AndAlso result

                Dim local As Mono.Cecil.Cil.VariableDefinition
                local = Emitter.DeclareLocal(Info, m_Expression.ExpressionType)
                Emitter.EmitStoreVariable(Info, local)
                Emitter.EmitLoadVariableLocation(Info, local)
            Case ExpressionClassification.Classifications.PropertyAccess
                Return Compiler.Report.ShowMessage(Messages.VBNC99997, Parent.Location)
            Case ExpressionClassification.Classifications.MethodPointer
                Return Compiler.Report.ShowMessage(Messages.VBNC99997, Parent.Location)
            Case Else
                Return Compiler.Report.ShowMessage(Messages.VBNC99997, Parent.Location)
        End Select

        Return result
    End Function

    Overrides ReadOnly Property ExpressionType() As Mono.Cecil.TypeReference
        Get
            Return m_ExpressionType
        End Get
    End Property
End Class
