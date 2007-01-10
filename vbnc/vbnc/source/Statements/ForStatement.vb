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

''' <summary>
''' ForStatement  ::=
'''	   "For" LoopControlVariable  "="  Expression  "To"  Expression  [  "Step"  Expression  ]  StatementTerminator
'''	      [ Block  ]
'''	   "Next" [  NextExpressionList  ]  StatementTerminator
''' LoopControlVariable  ::=
'''	   Identifier  [  ArrayNameModifier  ] "As" TypeName  |
'''	   Expression
''' NextExpressionList  ::=
'''	   Expression  |
'''	   NextExpressionList "," Expression
''' </summary>
''' <remarks></remarks>
Public Class ForStatement
    Inherits BlockStatement

    ''' <summary>
    ''' The loop control variable
    ''' </summary>
    ''' <remarks></remarks>
    Private m_LoopControlVariable As LoopControlVariable

    Private m_LoopStartExpression As Expression
    Private m_LoopEndExpression As Expression
    Private m_LoopStepExpression As Expression
    Private m_NextExpressionList As ExpressionList

    Private m_NextIteration As Label
    Private m_LoopType As Type

    ReadOnly Property LoopStepExpression() As Expression
        Get
            Return m_LoopStepExpression
        End Get
    End Property

    ReadOnly Property NextExpressionList() As ExpressionList
        Get
            Return m_NextExpressionList
        End Get
    End Property

    ReadOnly Property LoopEndExpression() As Expression
        Get
            Return m_LoopEndExpression
        End Get
    End Property

    ReadOnly Property LoopStartExpression() As Expression
        Get
            Return m_LoopStartExpression
        End Get
    End Property

    ReadOnly Property LoopControlVariable() As LoopControlVariable
        Get
            Return m_LoopControlVariable
        End Get
    End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        If m_LoopControlVariable IsNot Nothing Then result = m_LoopControlVariable.ResolveTypeReferences AndAlso result
        If m_LoopStartExpression IsNot Nothing Then result = m_LoopStartExpression.ResolveTypeReferences AndAlso result
        If m_LoopEndExpression IsNot Nothing Then result = m_LoopEndExpression.ResolveTypeReferences AndAlso result
        If m_LoopStepExpression IsNot Nothing Then result = m_LoopStepExpression.ResolveTypeReferences AndAlso result
        If m_NextExpressionList IsNot Nothing Then result = m_NextExpressionList.ResolveTypeReferences AndAlso result

        result = MyBase.ResolveTypeReferences AndAlso result

        Return result
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Shadows Sub Init(ByVal LoopControlVariable As LoopControlVariable, ByVal LoopStartExpression As Expression, ByVal LoopEndExpression As Expression, ByVal LoopStepExpression As Expression, ByVal NextExpressionList As ExpressionList, ByVal Block As CodeBlock)
        MyBase.Init(Block)

        m_LoopControlVariable = LoopControlVariable
        m_LoopStartExpression = LoopStartExpression
        m_LoopEndExpression = LoopEndExpression
        m_LoopStepExpression = LoopStepExpression
        m_NextExpressionList = NextExpressionList
    End Sub

    ReadOnly Property NextIteration() As Label
        Get
            Return m_NextIteration
        End Get
    End Property

    ''' <summary>
    ''' Emit code for 
    ''' Dim i As Integer
    ''' For i = 0 ...
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateOtherVariableCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Dim conditionLabel As Label
        Dim startlabel As Label
        Dim loopexp As Expression
        Dim loopClass As VariableClassification
        Dim loadInfo, storeInfo As EmitInfo
        Dim maxvar As LocalBuilder
        Dim stepvar As LocalBuilder = Nothing

        startlabel = Info.ILGen.DefineLabel
        conditionLabel = Info.ILGen.DefineLabel
        EndLabel = Info.ILGen.DefineLabel
        m_NextIteration = Info.ILGen.DefineLabel

        loopexp = m_LoopControlVariable.Expression
        loopClass = loopexp.Classification.AsVariableClassification

        loadInfo = Info.Clone(True, False, m_LoopType)
        storeInfo = Info.Clone(False, False, m_LoopType)

        maxvar = Emitter.DeclareLocal(Info, m_LoopType, "maxvar$" & Me.ObjectID.ToString)
        stepvar = Emitter.DeclareLocal(Info, m_LoopType, "stepvar$" & Me.ObjectID.ToString)

        'Load the initial expression
        result = m_LoopStartExpression.GenerateCode(loadInfo) AndAlso result
        Emitter.EmitStoreVariable(Info, loopClass)

        'Load the max expression
        result = m_LoopEndExpression.GenerateCode(loadInfo) AndAlso result
        Emitter.EmitStoreVariable(Info, maxvar)

        result = m_LoopStepExpression.GenerateCode(loadInfo) AndAlso result
        Emitter.EmitStoreVariable(Info, stepvar)

        'Jump to the comparison
        Emitter.EmitBranch(Info, conditionLabel)

        Info.ILGen.MarkLabel(startlabel)

        result = CodeBlock.GenerateCode(Info) AndAlso result

        'This is the start of the next iteration
        Info.ILGen.MarkLabel(m_NextIteration)

        'Load the current loop value
        result = loopClass.GenerateCodeAsValue(loadInfo) AndAlso result

        'Load the value to add
        Emitter.EmitLoadVariable(Info, stepvar)

        'Add them up
        Emitter.EmitAdd(Info, m_LoopType)

        'Store the result into the loop var
        Emitter.EmitStoreVariable(Info, loopClass)

        Info.ILGen.MarkLabel(conditionLabel)
        'Load the current value
        result = loopexp.GenerateCode(loadInfo) AndAlso result

        'Load the max value
        Emitter.EmitLoadVariable(Info, maxvar)

        'Compare the values
        EmitComparison(Info, startlabel, stepvar)

        Info.ILGen.MarkLabel(EndLabel)

        Return result
    End Function

    Private Function IsNegativeStep() As Boolean
        Dim constant As Object

        constant = m_LoopStepExpression.ConstantValue

        If TypeOf constant Is Decimal Then
            Return CDec(constant) < 0
        Else
            Return CDbl(constant) < 0
        End If
    End Function

    Private Function IsPositiveStep() As Boolean
        Dim constant As Object

        constant = m_LoopStepExpression.ConstantValue

        If TypeOf constant Is Decimal Then
            Return CDec(constant) > 0
        Else
            Return CDbl(constant) > 0
        End If
    End Function

    Private Function IsKnownStep() As Boolean
        Return m_LoopStepExpression.IsConstant
    End Function

    ''' <summary>
    ''' Emit code for
    ''' For i As Integer = 0 ...
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateDeclaredVariableCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Dim conditionLabel As Label
        Dim startlabel As Label
        Dim maxvar As LocalBuilder
        Dim loopvar As LocalBuilder
        Dim stepvar As LocalBuilder = Nothing
        Dim loadInfo As EmitInfo

        conditionLabel = Info.ILGen.DefineLabel
        startlabel = Info.ILGen.DefineLabel
        EndLabel = Info.ILGen.DefineLabel
        m_NextIteration = Info.ILGen.DefineLabel

        loadInfo = Info.Clone(True, False, m_LoopType)

        'Create the localbuilder
        result = m_LoopControlVariable.GenerateCode(Info) AndAlso result

        loopvar = m_LoopControlVariable.GetVariableDeclaration.LocalBuilder
        maxvar = Emitter.DeclareLocal(Info, m_LoopType, "maxvar$" & Me.ObjectID.ToString)
        stepvar = Emitter.DeclareLocal(Info, m_LoopType, "stepvar$" & Me.ObjectID.ToString)

        'Load the initial expression
        result = m_LoopStartExpression.GenerateCode(loadInfo) AndAlso result
        Emitter.EmitStoreVariable(Info, loopvar)

        'Load the max expression
        result = m_LoopEndExpression.GenerateCode(loadInfo) AndAlso result
        Emitter.EmitStoreVariable(Info, maxvar)

        result = m_LoopStepExpression.GenerateCode(loadInfo) AndAlso result
        Emitter.EmitStoreVariable(Info, stepvar)

        'Jump to the comparison
        Emitter.EmitBranch(Info, conditionLabel)

        'Emit the contained code.
        Info.ILGen.MarkLabel(startlabel)
        result = CodeBlock.GenerateCode(Info) AndAlso result

        'This is the start of the next iteration
        Info.ILGen.MarkLabel(m_NextIteration)

        'Load the current loop value
        Emitter.EmitLoadVariable(Info, loopvar)

        'Load the value to add
        Emitter.EmitLoadVariable(Info, stepvar)

        'Add them up
        Emitter.EmitAdd(Info, m_LoopType)

        'Store the result into the loop var
        Emitter.EmitStoreVariable(Info, loopvar)

        'Do the comparison
        Info.ILGen.MarkLabel(conditionLabel)

        'Load the current value
        Emitter.EmitLoadVariable(Info, loopvar)

        'Load the max value
        Emitter.EmitLoadVariable(Info, maxvar)

        'Compare the values
        EmitComparison(Info, startlabel, stepvar)

        Info.ILGen.MarkLabel(EndLabel)

        Return result
    End Function

    Private Sub EmitComparison(ByVal Info As EmitInfo, ByVal startLabel As Label, ByVal stepvar As LocalBuilder)
        If IsKnownStep() Then
            If IsPositiveStep() Then
                Emitter.EmitLE(Info, m_LoopType)
                Emitter.EmitBranchIfTrue(Info, startlabel)
            ElseIf IsNegativeStep() Then
                Emitter.EmitGE(Info, m_LoopType)
                Emitter.EmitBranchIfTrue(Info, startlabel)
            Else
                Helper.AddError("Infinite loop")
            End If
        Else
            Dim negativeLabel As Label
            Dim endCheck As Label

            negativeLabel = Emitter.DefineLabel(info)
            endCheck = Emitter.DefineLabel(Info)

            Emitter.EmitLoadVariable(Info, stepvar)
            Emitter.EmitLoadValue(Info.Clone(True, False, m_LoopType), TypeConverter.ConvertTo(0, m_LoopType))
            Emitter.EmitGE(Info, m_LoopType) 'stepvar >= 0?
            Info.ILGen.Emit(OpCodes.Brfalse_S, negativeLabel)
            Info.Stack.Pop(Compiler.TypeCache.Boolean)
            Emitter.EmitLE(Info, m_LoopType) 'Positive check
            Emitter.EmitBranch(Info, endCheck)
            Emitter.MarkLabel(Info, negativeLabel)
            Emitter.EmitGE(Info, m_LoopType) 'Negative check
            Emitter.MarkLabel(info, endCheck)
            Emitter.EmitBranchIfTrue(Info, startlabel)
        End If
    End Sub

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        If m_LoopControlVariable.IsVariableDeclaration Then
            result = GenerateDeclaredVariableCode(Info) AndAlso result
        Else
            result = GenerateOtherVariableCode(Info) AndAlso result
        End If

        Return result
    End Function

    Public Overrides Function ResolveStatement(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = m_LoopControlVariable.ResolveCode(Info) AndAlso result
        result = m_LoopStartExpression.ResolveExpression(Info) AndAlso result
        result = m_LoopEndExpression.ResolveExpression(Info) AndAlso result

        If m_LoopControlVariable.Expression IsNot Nothing Then
            If m_LoopControlVariable.Expression.Classification.IsVariableClassification = False Then
                Helper.AddError("In the case of an expression, the expression must be classified as a variable.")
            End If
        End If

        m_LoopType = m_LoopControlVariable.VariableType

        If m_LoopStepExpression IsNot Nothing Then
            result = m_LoopStepExpression.ResolveExpression(Info) AndAlso result
        Else
            m_LoopStepExpression = New ConstantExpression(Me, 1, Compiler.TypeCache.Integer)
        End If

        'If m_NextExpressionList IsNot Nothing Then result = m_NextExpressionList.ResolveCode(info) AndAlso result
        m_LoopStepExpression = Helper.CreateTypeConversion(Me, m_LoopStepExpression, m_LoopType, result)
        m_LoopStartExpression = Helper.CreateTypeConversion(Me, m_LoopStartExpression, m_LoopType, result)
        m_LoopEndExpression = Helper.CreateTypeConversion(Me, m_LoopEndExpression, m_LoopType, result)

        If m_LoopControlVariable.IsVariableDeclaration Then
            CodeBlock.Variables.Add(m_LoopControlVariable.GetVariableDeclaration)
        End If

        result = CodeBlock.ResolveCode(Info) AndAlso result

        Compiler.Helper.AddCheck("Check that loop variable has not been used in another for statement.")
        Compiler.Helper.AddCheck("The loop control variable of a For statement must be of a primitive numeric type (Byte, SByte, UShort, Short, UInteger, Integer, ULong, Long, Decimal, Single, Double), Object, or a type T that has the following operators: (...)")
        Compiler.Helper.AddCheck("The bound and step expressions must be implicitly convertible to the type of the loop control. ")
        Compiler.Helper.AddCheck("If a variable matches a For loop that is not the most nested loop at that point, a compile-time error results")
        Compiler.Helper.AddCheck("It is not valid to branch into a For loop from outside the loop.")

        Return result
    End Function
End Class
