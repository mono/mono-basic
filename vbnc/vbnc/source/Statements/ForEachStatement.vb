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
''' ForEachStatement  ::=
'''	   "For" "Each" LoopControlVariable "In" Expression  StatementTerminator
'''	         [  Block  ]
'''	   "Next" [Expression  ]  StatementTerminator
''' </summary>
''' <remarks></remarks>
Public Class ForEachStatement
    Inherits BlockStatement

    Private m_LoopControlVariable As LoopControlVariable
    Private m_InExpression As Expression
    Private m_NextExpression As Expression

    Private m_NextIteration As Label

    Private m_Enumerator As Mono.Cecil.Cil.VariableDefinition

    ReadOnly Property NextExpression() As Expression
        Get
            Return m_NextExpression
        End Get
    End Property

    ReadOnly Property InExpression() As Expression
        Get
            Return m_InExpression
        End Get
    End Property

    ReadOnly Property LoopControlVariable() As LoopControlVariable
        Get
            Return m_loopcontrolvariable
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Shadows Sub Init(ByVal LoopControlVariable As LoopControlVariable, ByVal InExpression As Expression, ByVal NextExpression As Expression, ByVal Block As CodeBlock)
        MyBase.Init(Block)

        m_LoopControlVariable = LoopControlVariable
        m_InExpression = InExpression
        m_NextExpression = NextExpression
    End Sub

    ReadOnly Property Enumerator() As Mono.Cecil.Cil.VariableDefinition
        Get
            Return m_Enumerator
        End Get
    End Property

    ReadOnly Property NextIteration() As Label
        Get
            Return m_NextIteration
        End Get
    End Property

    Function GenerateCode_LoadCurrentLoopVariable(ByVal Info As EmitInfo) As Boolean
        Dim varType As Mono.Cecil.TypeReference = m_LoopControlVariable.VariableType
        Dim isGenericParameter As Boolean = CecilHelper.IsGenericParameter(varType)
        Dim isValueType As Boolean = isGenericParameter = False AndAlso CecilHelper.IsValueType(varType)
        Dim isClass As Boolean = isGenericParameter = False AndAlso CecilHelper.IsClass(varType)

        Emitter.EmitLoadVariable(Info, m_Enumerator)
        Emitter.EmitCallVirt(Info, Compiler.TypeCache.System_Collections_IEnumerator__get_Current)

        Dim valueTPLoad As Label = Nothing
        Dim valueTPLoaded As Label = Nothing

        If isValueType Then
            Dim tmpStructureVariable As Mono.Cecil.Cil.VariableDefinition

            valueTPLoad = Emitter.DefineLabel(Info)
            valueTPLoaded = Emitter.DefineLabel(Info)
            tmpStructureVariable = Emitter.DeclareLocal(Info, varType)
            Emitter.EmitDup(Info)
            Emitter.EmitBranchIfTrue(Info, valueTPLoad)
            Emitter.EmitPop(Info, Compiler.TypeCache.System_Object)
            Emitter.EmitLoadVariable(Info, tmpStructureVariable)
            Emitter.EmitBranch(Info, valueTPLoaded)
            Emitter.FreeLocal(tmpStructureVariable)
        Else
            Emitter.EmitCallOrCallVirt(Info, Compiler.TypeCache.System_Runtime_CompilerServices_RuntimeHelpers__GetObjectValue_Object)
        End If

        If isGenericParameter Then
            Emitter.EmitUnbox_Any(Info, varType)
        ElseIf isClass Then
            Emitter.EmitCastClass(Info, varType)
        ElseIf isValueType Then
            Emitter.MarkLabel(Info, valueTPLoad)
            Emitter.EmitUnbox(Info, varType)
            Emitter.EmitLoadObject(Info, varType)
            Emitter.MarkLabel(Info, valueTPLoaded)
        Else
            Emitter.EmitConversion(Compiler.TypeCache.System_Object, varType, Info.Clone(Me, True, True, varType))
        End If
        Return True
    End Function

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True
        Dim beginEx As Label

        Dim startIteration As Label

        result = m_LoopControlVariable.GenerateCode(Info) AndAlso result 'Creates the localbuilder if necessary

        m_Enumerator = Emitter.DeclareLocal(Info, Helper.GetTypeOrTypeReference(Compiler, Compiler.TypeCache.System_Collections_IEnumerator))
        EndLabel = Emitter.DefineLabel(Info)
        m_NextIteration = Emitter.DefineLabel(Info)
        startIteration = Emitter.DefineLabel(Info)

        beginEx = Emitter.EmitBeginExceptionBlock(Info)

        Compiler.Helper.AddCheck("Check correct type of foreach loop container.")
        Helper.Assert(Helper.CompareType(Compiler.TypeCache.System_Object, m_InExpression.ExpressionType) OrElse Helper.IsAssignable(Me, m_InExpression.ExpressionType, Compiler.TypeCache.System_Collections_IEnumerable))

        'Load the container variable and get the enumerator
        result = m_InExpression.GenerateCode(Info.Clone(Me, True, False, m_InExpression.ExpressionType)) AndAlso result
        Emitter.EmitCastClass(Info, Helper.GetTypeOrTypeReference(Compiler, Compiler.TypeCache.System_Collections_IEnumerable))
        Emitter.EmitCallVirt(Info, Helper.GetMethodOrMethodReference(Compiler, Compiler.TypeCache.System_Collections_IEnumerable__GetEnumerator))
        Emitter.EmitStoreVariable(Info, m_Enumerator)

        'Jump to the next iteration
        Emitter.EmitBranch(Info, m_NextIteration)

        'Mark the beginning of the code
        Emitter.MarkLabel(Info, startIteration)

        Emitter.EmitNop(Info)
        Dim cge As New CompilerGeneratedExpression(Me, New CompilerGeneratedExpression.GenerateCodeDelegate(AddressOf GenerateCode_LoadCurrentLoopVariable), m_LoopControlVariable.VariableType)
        result = m_LoopControlVariable.EmitStoreVariable(Info.Clone(Me, cge)) AndAlso result

        result = CodeBlock.GenerateCode(Info) AndAlso result

        'Move to the next element
        Emitter.MarkLabel(Info, m_NextIteration)
        Emitter.EmitNop(Info)
        Emitter.EmitLoadVariable(Info, m_Enumerator)
        Emitter.EmitCallVirt(Info, Helper.GetMethodOrMethodReference(Compiler, Compiler.TypeCache.System_Collections_IEnumerator__MoveNext))
        'Jump to the code for the next element
        Emitter.EmitBranchIfTrue(Info, startIteration)
        'End of try code.
        'Emitter.EmitLeave(Info, beginEx)

        'Dispose of the enumerator if it is disposable.
        Emitter.EmitBeginFinallyBlock(Info)
        Dim EndFinally As Label = Emitter.DefineLabel(info)
        Emitter.EmitLoadVariable(Info, m_Enumerator)
        Emitter.EmitIsInst(Info, Helper.GetTypeOrTypeReference(Compiler, Compiler.TypeCache.System_Collections_IEnumerator), Helper.GetTypeOrTypeReference(Compiler, Compiler.TypeCache.System_IDisposable))
        Emitter.EmitBranchIfFalse(Info, EndFinally)
        Emitter.EmitLoadVariable(Info, m_Enumerator)
        Emitter.EmitIsInst(Info, Helper.GetTypeOrTypeReference(Compiler, Compiler.TypeCache.System_Collections_IEnumerator), Helper.GetTypeOrTypeReference(Compiler, Compiler.TypeCache.System_IDisposable))
        Emitter.EmitCallVirt(Info, Compiler.TypeCache.System_IDisposable__Dispose)
        Emitter.MarkLabel(info, EndFinally)
        Emitter.EmitEndExceptionBlock(Info)

        Emitter.MarkLabel(Info, EndLabel)

        Return result
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        If m_LoopControlVariable IsNot Nothing Then result = m_LoopControlVariable.ResolveTypeReferences AndAlso result
        If m_InExpression IsNot Nothing Then result = m_InExpression.ResolveTypeReferences AndAlso result
        If m_NextExpression IsNot Nothing Then result = m_NextExpression.ResolveTypeReferences AndAlso result

        result = MyBase.ResolveTypeReferences AndAlso result

        Return result
    End Function

    Public Overrides Function ResolveStatement(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = m_LoopControlVariable.ResolveCode(Info) AndAlso result
        If m_LoopControlVariable.GetVariableDeclaration IsNot Nothing Then
            MyBase.CodeBlock.AddVariable(m_LoopControlVariable.GetVariableDeclaration)
        End If

        result = m_InExpression.ResolveExpression(Info) AndAlso result
        If result = False Then Return False
        result = Helper.VerifyValueClassification(m_InExpression, Info) AndAlso result

        result = CodeBlock.ResolveCode(Info) AndAlso result

        If m_NextExpression IsNot Nothing Then
            result = m_NextExpression.ResolveExpression(Info) AndAlso result
            If result = False Then Return False

            If m_NextExpression.Classification.IsVariableClassification Then
                If m_LoopControlVariable.Expression IsNot Nothing Then
                    Dim lcvVar As VariableClassification = Nothing
                    Dim nextExpVar As VariableClassification = m_NextExpression.Classification.AsVariableClassification

                    If m_LoopControlVariable.Expression.Classification.IsVariableClassification Then
                        lcvVar = m_LoopControlVariable.Expression.Classification.AsVariableClassification
                    End If

                    If lcvVar IsNot Nothing AndAlso lcvVar.LocalVariable Is nextExpVar.LocalVariable Then
                        'OK
                    ElseIf lcvVar IsNot Nothing AndAlso lcvVar.ArrayVariable IsNot Nothing AndAlso lcvVar.ArrayVariable.Classification.IsVariableClassification AndAlso lcvVar.ArrayVariable.Classification.AsVariableClassification.LocalVariable Is nextExpVar.LocalVariable Then
                        'OK
                    Else
                        result = Compiler.Report.ShowMessage(Messages.VBNC30070, m_NextExpression.Location, m_LoopControlVariable.Identifier.Name)
                    End If
                ElseIf m_NextExpression.Classification.AsVariableClassification.LocalVariable IsNot m_LoopControlVariable.GetVariableDeclaration Then
                    result = Compiler.Report.ShowMessage(Messages.VBNC30070, m_NextExpression.Location, m_LoopControlVariable.Identifier.Name)
                End If
                Else
                    result = Compiler.Report.ShowMessage(Messages.VBNC30070, m_NextExpression.Location, m_LoopControlVariable.Identifier.Name)
                End If
            End If

            Compiler.Helper.AddCheck("It is not valid to branch into a For Each statement block from outside the block.")
            Compiler.Helper.AddCheck("The loop control variable is specified either through an identifier followed by an As clause or an expression. (...) In the case of an expression, the expression must be classified as a variable. ")
            Compiler.Helper.AddCheck("The enumerator expression must be classified as a value and its type must be a collection type or Object. ")
            Compiler.Helper.AddCheck("If the type of the enumerator expression is Object, then all processing is deferred until run-time. Otherwise, a conversion must exist from the element type of the collection to the type of the loop control variable")
            Compiler.Helper.AddCheck("The loop control variable cannot be used by another enclosing For Each statement. ")

            Return result
    End Function
End Class

