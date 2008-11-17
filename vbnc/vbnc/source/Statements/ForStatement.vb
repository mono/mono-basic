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

    Private m_IsLateBound As Boolean
    Private m_IsDecimal As Boolean

    Private Class LoopCounterData
        Public Type As LoopCounterTypes
        Public Variable As Object
        Public InstanceExpression As Expression

        ReadOnly Property FieldInfo() As FieldInfo
            Get
                Return DirectCast(Variable, FieldInfo)
            End Get
        End Property

        ReadOnly Property LocalBuilder() As LocalBuilder
            Get
                Return DirectCast(Variable, LocalBuilder)
            End Get
        End Property

        Sub New(ByVal Variable As Object, ByVal Type As LoopCounterTypes, Optional ByVal InstanceExpression As Expression = Nothing)
            Me.Type = Type
            Me.Variable = Variable
            Me.InstanceExpression = InstanceExpression
        End Sub
    End Class

    Private Enum LoopCounterTypes
        Local
        Field
        Array
    End Enum

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

    Private Function EmitLoadAddressCounter(ByVal Info As EmitInfo, ByVal Data As LoopCounterData) As Boolean
        Dim result As Boolean = True
        Select Case Data.Type
            Case LoopCounterTypes.Array
                Return Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
            Case LoopCounterTypes.Field
                If Data.InstanceExpression IsNot Nothing Then
                    result = Data.InstanceExpression.GenerateCode(Info.Clone(Me, Data.InstanceExpression.ExpressionType)) AndAlso result
                End If
                Emitter.EmitLoadVariableLocation(Info, Data.FieldInfo)
            Case LoopCounterTypes.Local
                Emitter.EmitLoadVariableLocation(Info, Data.LocalBuilder)
            Case Else
                Throw New InternalException("Unknown LoopCounterType: " & Data.Type.ToString())
        End Select
        Return result
    End Function

    Private Function EmitLoadCounter(ByVal Info As EmitInfo, ByVal Data As LoopCounterData) As Boolean
        Dim result As Boolean = True
        Select Case Data.Type
            Case LoopCounterTypes.Array
                Return Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
            Case LoopCounterTypes.Field
                If Data.InstanceExpression IsNot Nothing Then
                    result = Data.InstanceExpression.GenerateCode(Info.Clone(Me, Data.InstanceExpression.ExpressionType)) AndAlso result
                End If
                Emitter.EmitLoadVariable(Info, Data.FieldInfo)
            Case LoopCounterTypes.Local
                Emitter.EmitLoadVariable(Info, Data.LocalBuilder)
            Case Else
                Throw New InternalException("Unknown LoopCounterType: " & Data.Type.ToString())
        End Select
        Return result
    End Function

    Private Function EmitStoreCounterInstanceExpression(ByVal Info As EmitInfo, ByVal Data As LoopCounterData) As Boolean
        Dim result As Boolean = True

        If Data.InstanceExpression IsNot Nothing Then
            result = Data.InstanceExpression.GenerateCode(Info.Clone(Me, Data.InstanceExpression.ExpressionType)) AndAlso result
        End If

        Return result
    End Function

    Private Function EmitStoreCounter(ByVal Info As EmitInfo, ByVal Data As LoopCounterData) As Boolean
        Dim result As Boolean = True
        Select Case Data.Type
            Case LoopCounterTypes.Array
                Return Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
            Case LoopCounterTypes.Field
                Emitter.EmitStoreField(Info, Data.FieldInfo)
            Case LoopCounterTypes.Local
                Emitter.EmitStoreVariable(Info, Data.LocalBuilder)
            Case Else
                Throw New InternalException("Unknown LoopCounterType: " & Data.Type.ToString())
        End Select
        Return result
    End Function

    ''' <summary>
    ''' Emit code for
    ''' For i As Integer = 0 ...
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Dim conditionLabel As Label
        Dim startlabel As Label
        Dim loopCounter As LoopCounterData
        Dim loopMax As LocalBuilder
        Dim loopStart As LocalBuilder
        Dim loopStep As LocalBuilder
        Dim loopLateBoundObject As LocalBuilder = Nothing
        Dim loadInfo As EmitInfo

        conditionLabel = Info.ILGen.DefineLabel
        startlabel = Info.ILGen.DefineLabel
        EndLabel = Info.ILGen.DefineLabel
        m_NextIteration = Info.ILGen.DefineLabel

        loadInfo = Info.Clone(Me, True, False, m_LoopType)

        'Create the localbuilder
        If m_LoopControlVariable.IsVariableDeclaration Then
            result = m_LoopControlVariable.GenerateCode(Info) AndAlso result
            loopCounter = New LoopCounterData(m_LoopControlVariable.GetVariableDeclaration.LocalBuilder, LoopCounterTypes.Local)
        Else
            Dim varClass As VariableClassification
            varClass = m_LoopControlVariable.Expression.Classification.AsVariableClassification
            If varClass.LocalBuilder IsNot Nothing Then
                loopCounter = New LoopCounterData(varClass.LocalBuilder, LoopCounterTypes.Local)
            ElseIf varClass.FieldInfo IsNot Nothing Then
                loopCounter = New LoopCounterData(varClass.FieldInfo, LoopCounterTypes.Field, varClass.InstanceExpression)
            Else
                Throw New InternalException()
            End If
        End If

        loopMax = Emitter.DeclareLocal(Info, m_LoopType, "maxvar$" & Me.ObjectID.ToString)
        loopStep = Emitter.DeclareLocal(Info, m_LoopType, "stepvar$" & Me.ObjectID.ToString)
        loopStart = Emitter.DeclareLocal(Info, m_LoopType, "startvar$" & Me.ObjectID.ToString)
        If m_IsLateBound Then
            loopLateBoundObject = Emitter.DeclareLocal(Info, Compiler.TypeCache.System_Object, "loopobj$" & Me.ObjectID.ToString)
        End If

        'Load the initial expression
        result = m_LoopStartExpression.GenerateCode(loadInfo) AndAlso result
        Emitter.EmitStoreVariable(Info, loopStart)

        'Store the initial expression in the counter
        EmitStoreCounterInstanceExpression(Info, loopCounter)
        Emitter.EmitLoadVariable(Info, loopStart)
        EmitStoreCounter(Info, loopCounter)

        'Load the max expression
        result = m_LoopEndExpression.GenerateCode(loadInfo) AndAlso result
        Emitter.EmitStoreVariable(Info, loopMax)

        'Load the step expression
        result = m_LoopStepExpression.GenerateCode(loadInfo) AndAlso result
        Emitter.EmitStoreVariable(Info, loopStep)

        'Jump to the comparison
        If m_IsLateBound Then
            EmitLoadCounter(Info, loopCounter)
            'Emitter.EmitLoadVariable(Info, loopCounter)
            Emitter.EmitLoadVariable(Info, loopStart)
            Emitter.EmitLoadVariable(Info, loopMax)
            Emitter.EmitLoadVariable(Info, loopStep)
            Emitter.EmitLoadVariableLocation(Info, loopLateBoundObject)
            EmitLoadAddressCounter(Info, loopCounter)
            'Emitter.EmitLoadVariableLocation(Info, loopCounter)
            Emitter.EmitCall(Info, Compiler.TypeCache.MS_VB_CS_ObjectFlowControl_ForLoopControl__ForLoopInitObj_Object_Object_Object_Object_Object_Object)
            Emitter.EmitBranchIfFalse(Info, EndLabel)
        Else
            Emitter.EmitBranch(Info, conditionLabel)
        End If

        'Emit the contained code.
        Info.ILGen.MarkLabel(startlabel)
        result = CodeBlock.GenerateCode(Info) AndAlso result

        'This is the start of the next iteration
        Info.ILGen.MarkLabel(m_NextIteration)

        If m_IsLateBound Then
            EmitLoadCounter(Info, loopCounter)
            'Emitter.EmitLoadVariable(Info, loopCounter)
            Emitter.EmitLoadVariable(Info, loopLateBoundObject)
            EmitLoadAddressCounter(Info, loopCounter)
            'Emitter.EmitLoadVariableLocation(Info, loopCounter)
            Emitter.EmitCall(Info, Info.Compiler.TypeCache.MS_VB_CS_ObjectFlowControl_ForLoopControl__ForNextCheckObj_Object_Object_Object)
            Emitter.EmitBranchIfTrue(Info, startlabel)
        ElseIf m_IsDecimal Then
            EmitStoreCounterInstanceExpression(Info, loopCounter)
            EmitLoadCounter(Info, loopCounter) 'Emitter.EmitLoadVariable(Info, loopCounter)
            Emitter.EmitLoadVariable(Info, loopStep)
            Emitter.EmitCall(Info, Info.Compiler.TypeCache.System_Decimal__Add_Decimal_Decimal)
            EmitStoreCounter(Info, loopCounter)

            'Do the comparison
            Info.ILGen.MarkLabel(conditionLabel)

            'Load the current value
            EmitLoadCounter(Info, loopCounter)
            'Emitter.EmitLoadVariable(Info, loopCounter)

            'Load the max value
            Emitter.EmitLoadVariable(Info, loopMax)

            'Load the step value
            Emitter.EmitLoadVariable(Info, loopStep)

            'Compare the values
            Emitter.EmitCall(Info, Info.Compiler.TypeCache.MS_VB_CS_ObjectFlowControl_ForLoopControl__ForNextCheckDec_Decimal_Decimal_Decimal)
            Emitter.EmitBranchIfTrue(Info, startlabel)
        Else
            EmitStoreCounterInstanceExpression(Info, loopCounter)
            EmitLoadCounter(Info, loopCounter) 'Emitter.EmitLoadVariable(Info, loopCounter)
            Emitter.EmitLoadVariable(Info, loopStep)
            Emitter.EmitAdd(Info, m_LoopType)
            EmitStoreCounter(Info, loopCounter) 'Emitter.EmitStoreVariable(Info, loopCounter)

            'Do the comparison
            Info.ILGen.MarkLabel(conditionLabel)

            'Load the current value
            EmitLoadCounter(Info, loopCounter) 'Emitter.EmitLoadVariable(Info, loopCounter)

            'Load the max value
            Emitter.EmitLoadVariable(Info, loopMax)

            'Compare the values
            If IsKnownStep() Then
                If IsPositiveStep() Then
                    Emitter.EmitLE(Info, m_LoopType)
                    Emitter.EmitBranchIfTrue(Info, startlabel)
                ElseIf IsNegativeStep() Then
                    Emitter.EmitGE(Info, m_LoopType)
                    Emitter.EmitBranchIfTrue(Info, startlabel)
                Else
                    Helper.AddError(Me, "Infinite loop")
                End If
            Else
                Dim negativeLabel As Label
                Dim endCheck As Label

                negativeLabel = Emitter.DefineLabel(Info)
                endCheck = Emitter.DefineLabel(Info)

                Emitter.EmitLoadVariable(Info, loopStep)
                Dim tmp As Object = Nothing
                If TypeConverter.ConvertTo(Me, 0, m_LoopType, tmp) Then
                    Emitter.EmitLoadValue(Info.Clone(Me, True, False, m_LoopType), tmp)
                Else
                    Throw New InternalException
                End If
                Emitter.EmitGE(Info, m_LoopType) 'stepvar >= 0?
                Info.ILGen.Emit(OpCodes.Brfalse_S, negativeLabel)
                Info.Stack.Pop(Compiler.TypeCache.System_Boolean)
                Emitter.EmitLE(Info, m_LoopType) 'Positive check
                Emitter.EmitBranch(Info, endCheck)
                Emitter.MarkLabel(Info, negativeLabel)
                Emitter.EmitGE(Info, m_LoopType) 'Negative check
                Emitter.MarkLabel(Info, endCheck)
                Emitter.EmitBranchIfTrue(Info, startlabel)
            End If
        End If

        Info.ILGen.MarkLabel(EndLabel)

        Return result
    End Function

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        result = GenerateCodeInternal(Info) AndAlso result

        Return result
    End Function

    Public Overrides Function ResolveStatement(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = m_LoopControlVariable.ResolveCode(Info) AndAlso result
        result = m_LoopStartExpression.ResolveExpression(Info) AndAlso result
        result = m_LoopEndExpression.ResolveExpression(Info) AndAlso result

        If result = False Then Return result

        If m_LoopControlVariable.Expression IsNot Nothing Then
            If m_LoopControlVariable.Expression.Classification.IsVariableClassification = False Then
                Select Case m_LoopControlVariable.Expression.Classification.Classification
                    Case ExpressionClassification.Classifications.PropertyAccess, ExpressionClassification.Classifications.PropertyGroup, ExpressionClassification.Classifications.LateBoundAccess
                        Return Compiler.Report.ShowMessage(Messages.VBNC30039, Me.Location) AndAlso result
                    Case ExpressionClassification.Classifications.Value
                        Return Helper.ShowClassificationError(Compiler, Me.Location, m_LoopControlVariable.Expression.Classification, "Variable") AndAlso result
                    Case Else
                        Return Helper.ShowClassificationError(Compiler, Me.Location, m_LoopControlVariable.Expression.Classification, "Variable") AndAlso result
                End Select
            End If
        End If

        m_LoopType = m_LoopControlVariable.VariableType

        If m_LoopStepExpression IsNot Nothing Then
            result = m_LoopStepExpression.ResolveExpression(Info) AndAlso result
        Else
            m_LoopStepExpression = New ConstantExpression(Me, 1, Compiler.TypeCache.System_Int32)
        End If

        'If m_NextExpressionList IsNot Nothing Then result = m_NextExpressionList.ResolveCode(info) AndAlso result
        m_LoopStepExpression = Helper.CreateTypeConversion(Me, m_LoopStepExpression, m_LoopType, result)
        m_LoopStartExpression = Helper.CreateTypeConversion(Me, m_LoopStartExpression, m_LoopType, result)
        m_LoopEndExpression = Helper.CreateTypeConversion(Me, m_LoopEndExpression, m_LoopType, result)

        If m_LoopControlVariable.IsVariableDeclaration Then
            CodeBlock.Variables.Add(m_LoopControlVariable.GetVariableDeclaration)
        End If

        result = CodeBlock.ResolveCode(Info) AndAlso result

        If result = False Then Return result

        Select Case Type.GetTypeCode(m_LoopType)
            Case TypeCode.Boolean, TypeCode.Char, TypeCode.DBNull, TypeCode.Empty, TypeCode.String
                result = Compiler.Report.ShowMessage(Messages.VBNC30337, m_LoopType.Name) AndAlso result
            Case TypeCode.Decimal
                m_IsLateBound = False
                m_IsDecimal = True
            Case TypeCode.Byte, TypeCode.Double, TypeCode.Int16, TypeCode.Int32, TypeCode.Int64, TypeCode.SByte, TypeCode.Single, TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64
                m_IsLateBound = False
            Case TypeCode.Object
                m_IsLateBound = True

                Compiler.Helper.AddCheck("The loop control variable of a For statement must be of a primitive numeric type (...), Object, or a type T that has the following operators: (...)")

            Case Else
                result = Compiler.Report.ShowMessage(Messages.VBNC30337, m_LoopType.Name) AndAlso result
        End Select


        Compiler.Helper.AddCheck("Check that loop variable has not been used in another for statement.")
        Compiler.Helper.AddCheck("The bound and step expressions must be implicitly convertible to the type of the loop control. ")
        Compiler.Helper.AddCheck("If a variable matches a For loop that is not the most nested loop at that point, a compile-time error results")
        Compiler.Helper.AddCheck("It is not valid to branch into a For loop from outside the loop.")

        Return result
    End Function
End Class
