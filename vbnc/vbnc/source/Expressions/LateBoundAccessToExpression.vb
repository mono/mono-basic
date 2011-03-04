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

Public MustInherit Class LateBoundAccessToExpression
    Inherits Expression

    Private m_LateBoundAccess As LateBoundAccessClassification

    ReadOnly Property LateBoundAccess() As LateBoundAccessClassification
        Get
            Return m_LateBoundAccess
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject, ByVal LateBoundAccessClassification As LateBoundAccessClassification)
        MyBase.new(Parent)
        m_LateBoundAccess = LateBoundAccessClassification
    End Sub

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Return Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)

        Return result
    End Function

    Overrides ReadOnly Property ExpressionType() As Mono.Cecil.TypeReference
        Get
            Return Compiler.TypeCache.System_Object
        End Get
    End Property

    Protected Function EmitLateIndexGet(ByVal Info As EmitInfo) As Boolean
        Return EmitLateIndexGet(Info, LateBoundAccess)
    End Function

    Protected Function EmitLateGet(ByVal Info As EmitInfo) As Boolean
        Return EmitLateGet(Info, LateBoundAccess)
    End Function

    Protected Function EmitLateIndexSet(ByVal Info As EmitInfo) As Boolean
        Return EmitLateIndexSet(Info, LateBoundAccess)
    End Function

    Protected Function EmitLateSet(ByVal Info As EmitInfo) As Boolean
        Return EmitLateSet(Info, LateBoundAccess)
    End Function

    ''' <summary>
    ''' Creates an object array (always).
    ''' - initializes it with the arguments (if any). 
    ''' - adds the rhs expression (if supplied).
    ''' Leaves a reference to the object array on the stack.
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <param name="LateBoundAccess"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function EmitArguments(ByVal Info As EmitInfo, ByVal LateBoundAccess As LateBoundAccessClassification, ByRef arguments As Mono.Cecil.Cil.VariableDefinition) As Boolean
        Dim result As Boolean = True

        Dim argCount As Integer
        Dim elementCount As Integer
        Dim args As ArgumentList

        Dim namedCount As Integer

        args = LateBoundAccess.Arguments
        If args IsNot Nothing Then argCount = args.Count

        elementCount = argCount
        If Info.RHSExpression IsNot Nothing Then elementCount += 1

        arguments = Emitter.DeclareLocal(Info, Info.Compiler.TypeCache.System_Object_Array)

        Emitter.EmitLoadI4Value(Info, elementCount)
        Emitter.EmitNewArr(Info, Info.Compiler.TypeCache.System_Object)

        Emitter.EmitStoreVariable(Info, arguments)

        For i As Integer = 0 To argCount - 1
            Dim arg As Argument = args.Arguments(i)
            If TypeOf arg Is NamedArgument Then namedCount += 1
            Emitter.EmitLoadVariable(Info, arguments)
            Emitter.EmitLoadI4Value(Info, i)
            If arg.Expression Is Nothing Then
                Emitter.EmitLoadVariable(Info, Info.Compiler.TypeCache.System_Reflection_Missing__Value)
            Else
                result = arg.GenerateCode(Info.Clone(Info.Context, True, False, arg.Expression.ExpressionType)) AndAlso result
                If CecilHelper.IsValueType(arg.Expression.ExpressionType) Then
                    Emitter.EmitBox(Info, arg.Expression.ExpressionType)
                End If
            End If
            Emitter.EmitStoreElement(Info, Info.Compiler.TypeCache.System_Object, Info.Compiler.TypeCache.System_Object_Array)
        Next

        If elementCount <> argCount Then
            Emitter.EmitLoadVariable(Info, arguments)
            Emitter.EmitLoadI4Value(Info, elementCount - 1)
            result = Info.RHSExpression.GenerateCode(Info.Clone(Info.Context, True, False, Info.RHSExpression.ExpressionType)) AndAlso result
            If CecilHelper.IsValueType(Info.RHSExpression.ExpressionType) Then
                Emitter.EmitBox(Info, Info.RHSExpression.ExpressionType)
            End If
            Emitter.EmitStoreElement(Info, Info.Compiler.TypeCache.System_Object, Info.Compiler.TypeCache.System_Object_Array)
        End If

        Emitter.EmitLoadVariable(Info, arguments)

        If namedCount > 0 Then
            Dim namedArguments As Mono.Cecil.Cil.VariableDefinition
            namedArguments = Emitter.DeclareLocal(Info, Info.Compiler.TypeCache.System_String_Array)
            Emitter.EmitLoadI4Value(Info, namedCount)
            Emitter.EmitNewArr(Info, Info.Compiler.TypeCache.System_String)
            Emitter.EmitStoreVariable(Info, namedArguments)

            Dim iNamed As Integer
            For i As Integer = 0 To argCount - 1
                Dim arg As NamedArgument = TryCast(args.Arguments(i), NamedArgument)
                If arg Is Nothing Then Continue For

                Emitter.EmitLoadVariable(Info, namedArguments)
                Emitter.EmitLoadI4Value(Info, iNamed)
                Emitter.EmitLoadValue(Info, arg.Name)
                Emitter.EmitStoreElement(Info, Info.Compiler.TypeCache.System_String, Info.Compiler.TypeCache.System_String_Array)

                iNamed += 1
            Next

            Emitter.EmitLoadVariable(Info, namedArguments)
        Else
            Emitter.EmitLoadNull(Info.Clone(Info.Context, Info.Compiler.TypeCache.System_String_Array))
        End If

        Return result
    End Function

    ''' <summary>
    ''' If there's anything to copy back, creates a boolean array with:
    ''' - true if the value can be copied back (the argument is assignable, that is: field, variable, rw property), otherwise false (function, ro property, constant)
    ''' Otherwise just loads a null value.
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <param name="LateBoundAccess"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function EmitCopyBacks(ByVal Info As EmitInfo, ByVal LateBoundAccess As LateBoundAccessClassification, ByRef copyBackHints As Boolean(), ByRef copyBacks As Mono.Cecil.Cil.VariableDefinition) As Boolean
        Dim result As Boolean = True
        Dim args As ArgumentList

        args = LateBoundAccess.Arguments

        If args Is Nothing OrElse args.Count = 0 Then
            Emitter.EmitLoadNull(Info.Clone(Info.Context, Info.Compiler.TypeCache.System_Boolean_Array))
        Else
            copyBackHints = New Boolean(args.Count - 1) {}
            copyBacks = Emitter.DeclareLocal(Info, Info.Compiler.TypeCache.System_Boolean_Array)

            Emitter.EmitLoadI4Value(Info, args.Count)
            Emitter.EmitNewArr(Info, Info.Compiler.TypeCache.System_Boolean)
            Emitter.EmitStoreVariable(Info, copyBacks)
            For i As Integer = 0 To args.Count - 1
                Dim arg As Argument
                Dim exp As Expression
                Dim copyBack As Boolean

                arg = args.Arguments(i)
                exp = arg.Expression

                Emitter.EmitLoadVariable(Info, copyBacks) ' Emitter.EmitDup(Info)
                Emitter.EmitLoadI4Value(Info, i)
                If exp Is Nothing Then
                    copyBack = False
                Else
                    Select Case exp.Classification.Classification
                        Case ExpressionClassification.Classifications.Variable
                            Dim varC As VariableClassification = exp.Classification.AsVariableClassification
                            If varC.LocalBuilder IsNot Nothing Then
                                copyBack = True
                            ElseIf varC.FieldInfo IsNot Nothing Then
                                Dim fD As IFieldMember = TryCast(varC.FieldInfo.Annotations(Info.Compiler), IFieldMember)
                                If fD IsNot Nothing Then
                                    'TODO: Is the copyback done for readonly fields inside constructors?
                                    copyBack = varC.FieldDefinition.IsLiteral = False AndAlso fD.Modifiers.Is(ModifierMasks.ReadOnly) = False
                                Else
                                    copyBack = varC.FieldDefinition.IsLiteral = False
                                End If
                            Else
                                copyBack = False
                            End If
                        Case ExpressionClassification.Classifications.Value
                            copyBack = False
                        Case ExpressionClassification.Classifications.PropertyAccess
                            copyBack = CecilHelper.FindDefinition(exp.Classification.AsPropertyAccess.ResolvedProperty).SetMethod IsNot Nothing '.CanWrite
                        Case Else
                            Return Info.Compiler.Report.ShowMessage(Messages.VBNC99997, LateBoundAccess.Parent.Location)
                    End Select
                End If
                copyBackHints(i) = copyBack
                If copyBack Then
                    Emitter.EmitLoadI4Value(Info, 1)
                Else
                    Emitter.EmitLoadI4Value(Info, 0)
                End If
                Emitter.EmitStoreElement(Info, Info.Compiler.TypeCache.System_Boolean, Info.Compiler.TypeCache.System_Boolean_Array)
            Next
            Emitter.EmitLoadVariable(Info, copyBacks)
        End If

        Return result
    End Function

    Private Shared Function EmitStoreBacks(ByVal Info As EmitInfo, ByVal LateBoundAccess As LateBoundAccessClassification, ByVal CopyBacks As Boolean(), ByVal array As Mono.Cecil.Cil.VariableDefinition, ByVal arguments As Mono.Cecil.Cil.VariableDefinition) As Boolean
        Dim result As Boolean = True
        Dim args As ArgumentList

        If CopyBacks Is Nothing OrElse CopyBacks.Length = 0 Then Return result

        args = LateBoundAccess.Arguments

        For i As Integer = 0 To CopyBacks.Length - 1
            Dim branch As Label

            If CopyBacks(i) = False Then Continue For

            Dim arg As Argument
            Dim exp As Expression

            arg = args.Arguments(i)
            exp = arg.Expression

            branch = Emitter.DefineLabel(Info)
            Emitter.EmitLoadVariable(Info, array)
            Emitter.EmitLoadI4Value(Info, i)
            Emitter.EmitLoadElement(Info, Info.Compiler.TypeCache.System_Boolean_Array)
            Emitter.EmitBranchIfFalse(Info, branch)

            Dim tmpVar As Mono.Cecil.Cil.VariableDefinition
            tmpVar = Emitter.DeclareLocal(Info, exp.ExpressionType)

            Emitter.EmitLoadVariable(Info, arguments)
            Emitter.EmitLoadI4Value(Info, i)
            Emitter.EmitLoadElement(Info, Info.Compiler.TypeCache.System_Object_Array)
            Emitter.EmitCall(Info, Info.Compiler.TypeCache.System_Runtime_CompilerServices_RuntimeHelpers__GetObjectValue_Object)
            Emitter.EmitLoadToken(Info, exp.ExpressionType)
            Emitter.EmitCall(Info, Info.Compiler.TypeCache.System_Type__GetTypeFromHandle_RuntimeTypeHandle)
            Emitter.EmitCall(Info, Info.Compiler.TypeCache.MS_VB_CS_Conversions__ChangeType_Object_Type)

            Dim vosExp As New ValueOnStackExpression(exp, Info.Compiler.TypeCache.System_Object)
            Dim convExp As DirectCastExpression
            convExp = New DirectCastExpression(exp)
            convExp.Init(vosExp, exp.ExpressionType)
            result = convExp.GenerateCode(Info) AndAlso result
            Emitter.EmitStoreVariable(Info, tmpVar)
            result = exp.GenerateCode(Info.Clone(Info.Context, New LoadLocalExpression(exp, tmpVar))) AndAlso result

            Emitter.MarkLabel(Info, branch)
        Next

        Return result
    End Function

    Public Shared Function EmitLateGet(ByVal Info As EmitInfo, ByVal LateBoundAccess As LateBoundAccessClassification) As Boolean
        Dim result As Boolean = True
        Dim copyBacks As Mono.Cecil.Cil.VariableDefinition = Nothing, arguments As Mono.Cecil.Cil.VariableDefinition = Nothing
        Dim copyBackHints As Boolean() = Nothing

        'We need to emit a call to LateGet

        If LateBoundAccess.InstanceExpression Is Nothing Then
            '1 - the instance expression (none in this case)
            Emitter.EmitLoadNull(Info.Clone(Info.Context, Info.Compiler.TypeCache.System_Type))

            '2 - Type 
            Emitter.EmitLoadToken(Info, LateBoundAccess.LateBoundType)
            Emitter.EmitCall(Info, Info.Compiler.TypeCache.System_Type__GetTypeFromHandle_RuntimeTypeHandle)
        Else
            '1 - the instance expression
            result = LateBoundAccess.InstanceExpression.GenerateCode(Info) AndAlso result

            '2 - Type  - we have the instance, so no need to pass the type here.
            Emitter.EmitLoadNull(Info.Clone(Info.Context, Info.Compiler.TypeCache.System_Type))
        End If

        '3 - The member name
        Emitter.EmitLoadValue(Info, LateBoundAccess.Name)

        '4 - The arguments
        '5 - ArgumentNames
        EmitArguments(Info, LateBoundAccess, arguments)

        '6 - TypeArguments
        If LateBoundAccess.TypeArguments IsNot Nothing Then
            Return Info.Compiler.Report.ShowMessage(Messages.VBNC99997, LateBoundAccess.Parent.Location)
        Else
            Emitter.EmitLoadNull(Info.Clone(Info.Context, Info.Compiler.TypeCache.System_Type_Array))
        End If

        '7 - CopyBack
        EmitCopyBacks(Info, LateBoundAccess, copyBackHints, copyBacks)

        Emitter.EmitCall(Info, Info.Compiler.TypeCache.MS_VB_CS_NewLateBinding__LateGet_Object_Type_String_Array_Array_Array_Array)
        Emitter.EmitCall(Info, Info.Compiler.TypeCache.System_Runtime_CompilerServices_RuntimeHelpers__GetObjectValue_Object)

        EmitStoreBacks(Info, LateBoundAccess, copyBackHints, copyBacks, arguments)

        Return result
    End Function

    Public Shared Function EmitLateIndexGet(ByVal Info As EmitInfo, ByVal LateBoundAccess As LateBoundAccessClassification) As Boolean
        Dim result As Boolean = True
        Dim arguments As Mono.Cecil.Cil.VariableDefinition = Nothing

        'We need to emit a call to LateIndexGet

        '1 - the instance expression
        result = LateBoundAccess.InstanceExpression.GenerateCode(Info) AndAlso result

        '2 - The arguments
        '5 - ArgumentNames
        EmitArguments(Info, LateBoundAccess, arguments)

        Emitter.EmitCall(Info, Info.Compiler.TypeCache.MS_VB_CS_NewLateBinding__LateIndexGet_Object_Array_Array)
        Emitter.EmitCall(Info, Info.Compiler.TypeCache.System_Runtime_CompilerServices_RuntimeHelpers__GetObjectValue_Object)

        Return result
    End Function

    Public Shared Function EmitLateSet(ByVal Info As EmitInfo, ByVal LateBoundAccess As LateBoundAccessClassification) As Boolean
        Dim result As Boolean = True
        Dim arguments As Mono.Cecil.Cil.VariableDefinition = Nothing

        'We need to emit a call to LateSet

        '1 - the instance expression
        result = LateBoundAccess.InstanceExpression.GenerateCode(Info.Clone(Info.Context, True, False, LateBoundAccess.InstanceExpression.ExpressionType)) AndAlso result

        '2 - Type ??? - haven't found an example where this isn't nothing yet
        Emitter.EmitLoadNull(Info.Clone(Info.Context, Info.Compiler.TypeCache.System_Type))

        '3 - The member name
        Emitter.EmitLoadValue(Info, LateBoundAccess.Name)

        '4 - The arguments
        '5 - ArgumentNames
        EmitArguments(Info, LateBoundAccess, arguments)

        '6 - TypeArguments
        If LateBoundAccess.TypeArguments IsNot Nothing Then
            Return Info.Compiler.Report.ShowMessage(Messages.VBNC99997, LateBoundAccess.Parent.Location)
        Else
            Emitter.EmitLoadNull(Info.Clone(Info.Context, Info.Compiler.TypeCache.System_Type_Array))
        End If

        Emitter.EmitCall(Info, Info.Compiler.TypeCache.MS_VB_CS_NewLateBinding__LateSet_Object_Type_String_Array_Array_Array)

        Return result
    End Function

    Public Shared Function EmitLateIndexSet(ByVal Info As EmitInfo, ByVal LateBoundAccess As LateBoundAccessClassification) As Boolean
        Dim result As Boolean = True
        Dim arguments As Mono.Cecil.Cil.VariableDefinition = Nothing

        'We need to emit a call to LateIndexSet

        '1 - the instance expression
        result = LateBoundAccess.InstanceExpression.GenerateCode(Info.Clone(Info.Context, True, False, LateBoundAccess.InstanceExpression.ExpressionType)) AndAlso result

        '2 - The arguments
        '3 - ArgumentNames
        EmitArguments(Info, LateBoundAccess, arguments)

        Emitter.EmitCall(Info, Info.Compiler.TypeCache.MS_VB_CS_NewLateBinding__LateIndexSet_Object_Array_Array)

        Return result
    End Function

    Public Shared Function EmitLateCall(ByVal Info As EmitInfo, ByVal LateBoundAccess As LateBoundAccessClassification) As Boolean
        Dim result As Boolean = True
        Dim copyBacks As Mono.Cecil.Cil.VariableDefinition = Nothing, arguments As Mono.Cecil.Cil.VariableDefinition = Nothing
        Dim copyBackHints As Boolean() = Nothing

        'We need to emit a call to LateCall

        '1 - the instance expression
        If LateBoundAccess.InstanceExpression IsNot Nothing Then
            result = LateBoundAccess.InstanceExpression.GenerateCode(Info.Clone(Info.Context, True, False, LateBoundAccess.InstanceExpression.ExpressionType)) AndAlso result
        Else
            Emitter.EmitLoadNull(Info.Clone(Info.Context, Info.Compiler.TypeCache.System_Object))
        End If

        '2 - Type
        If LateBoundAccess.LateBoundType Is Nothing Then
            Emitter.EmitLoadNull(Info.Clone(Info.Context, Info.Compiler.TypeCache.System_Type))
        Else
            Emitter.EmitLoadToken(Info, LateBoundAccess.LateBoundType)
            Emitter.EmitCall(Info, Info.Compiler.TypeCache.System_Type__GetTypeFromHandle_RuntimeTypeHandle)
        End If

        '3 - The member name
        Emitter.EmitLoadValue(Info, LateBoundAccess.Name)

        '4 - The arguments
        '5 - ArgumentNames
        EmitArguments(Info, LateBoundAccess, arguments)

        '6 - TypeArguments
        If LateBoundAccess.TypeArguments IsNot Nothing Then
            Return Info.Compiler.Report.ShowMessage(Messages.VBNC99997, LateBoundAccess.Parent.Location)
        Else
            Emitter.EmitLoadNull(Info.Clone(Info.Context, Info.Compiler.TypeCache.System_Type_Array))
        End If

        '7 - CopyBack
        EmitCopyBacks(Info, LateBoundAccess, copyBackHints, copyBacks)

        '8 - Ignore return
        Emitter.EmitLoadI4Value(Info, 1)
        Emitter.EmitCall(Info, Info.Compiler.TypeCache.MS_VB_CS_NewLateBinding__LateCall_Object_Type_String_Array_Array_Array_Array_Boolean)

        Emitter.EmitPop(Info, Info.Compiler.TypeCache.System_Object)

        EmitStoreBacks(Info, LateBoundAccess, copyBackHints, copyBacks, arguments)

        Return result
    End Function
End Class
