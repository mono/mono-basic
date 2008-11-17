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

Imports System.Reflection
Imports System.Reflection.Emit

#If DEBUG Then
#Const DEBUGIMPLICITCONVERSION = 0
#Const EXTENDEDDEBUG = 0
#End If

Partial Public Class Emitter

    Shared Function DefineLabel(ByVal Info As EmitInfo) As Label
        Return Info.ILGen.DefineLabel
    End Function

    Shared Sub MarkSequencePoint(ByVal Info As EmitInfo, ByVal Location As Span)
        'If Location Is Nothing Then Return
        If Location.File(Info.Compiler) Is Nothing Then Return
        If Location.File(Info.Compiler).SymbolDocument Is Nothing Then Return
        If Location.Line <= 0 Then Return

        Info.ILGen.MarkSequencePoint(Location.File(Info.Compiler).SymbolDocument, CInt(Location.Line), Location.Column, CInt(Location.Line), Location.EndColumn)
    End Sub

    Shared Function DeclareLocal(ByVal Info As EmitInfo, ByVal Type As Type, Optional ByVal Name As String = "") As LocalBuilder
        Dim result As LocalBuilder
        Type = Helper.GetTypeOrTypeBuilder(Type)
        result = Info.ILGen.DeclareLocal(Type)
        If Name <> String.Empty AndAlso Info.Compiler.EmittingDebugInfo Then
            result.SetLocalSymInfo(Name)
        End If
        Return result
    End Function

    Shared Sub FreeLocal(ByVal Local As LocalBuilder)
        'local variable reuse not implemented yet.
    End Sub

    Shared Sub MarkLabel(ByVal Info As EmitInfo, ByVal Label As Label)
        Info.ILGen.MarkLabel(Label)
    End Sub

    Shared Sub EmitBeginExceptionFilter(ByVal Info As EmitInfo)
        Helper.Assert(Info.InExceptionFilter = False)
        Info.ILGen.BeginExceptFilterBlock()
        Info.Stack.Push(Info.Compiler.TypeCache.System_Object)
        Info.InExceptionFilter = True
    End Sub

    Shared Sub EmitBeginCatch(ByVal Info As EmitInfo, ByVal ExceptionType As Type)
        If ExceptionType IsNot Nothing Then ExceptionType = Helper.GetTypeOrTypeBuilder(ExceptionType)
        If Info.InExceptionFilter Then
            Info.Stack.Pop(Info.Compiler.TypeCache.System_Boolean)
        End If
        Info.ILGen.BeginCatchBlock(ExceptionType)
        If ExceptionType IsNot Nothing Then
            Info.Stack.Push(ExceptionType)
        Else
            Info.Stack.Push(Info.Compiler.TypeCache.System_Object)
        End If
        Info.InExceptionFilter = False
    End Sub

    Shared Sub EmitEndExceptionBlock(ByVal Info As EmitInfo)
        Helper.Assert(Info.InExceptionFilter = False)
        Info.ILGen.EndExceptionBlock()
    End Sub

    Shared Function EmitBeginExceptionBlock(ByVal Info As EmitInfo) As Label
        Helper.Assert(Info.InExceptionFilter = False)
        Dim result As Label
        result = Info.ILGen.BeginExceptionBlock
        Return result
    End Function

    Shared Sub EmitBeginFinallyBlock(ByVal Info As EmitInfo)
        Info.ILGen.BeginFinallyBlock()
    End Sub

    <Obsolete("Do not use this!")> Shared ReadOnly Property Compiler() As Compiler
        Get
            Throw New InternalException("")
        End Get
    End Property

    Shared Sub EmitPop(ByVal Info As EmitInfo, ByVal Type As Type)
        Type = Helper.GetTypeOrTypeBuilder(Type)
        Info.ILGen.Emit(OpCodes.Pop)
        Info.Stack.Pop(Type)
    End Sub

    Shared Sub EmitBranchIfFalse(ByVal Info As EmitInfo, ByVal Label As Label)
#If DEBUG Then
        If Info.Stack.Peek.IsClass OrElse Info.Stack.Peek.IsInterface Then
            'comparison with Nothing, operand can be a class or interface.
            Info.Stack.Pop(Info.Stack.Peek)
        Else
            Info.Stack.Pop(Info.Compiler.TypeCache.System_Boolean)
        End If
#End If
        Info.ILGen.Emit(OpCodes.Brfalse, Label)
    End Sub

    Shared Sub EmitBranchIfTrue(ByVal Info As EmitInfo, ByVal Label As Label)
        Info.Stack.Pop(Info.Compiler.TypeCache.System_Boolean)
        Info.ILGen.Emit(OpCodes.Brtrue, Label)
    End Sub

    Shared Sub EmitBranchIfTrue(ByVal Info As EmitInfo, ByVal Label As Label, ByVal Type As Type)
        Type = Helper.GetTypeOrTypeBuilder(Type)
        Helper.Assert(Helper.CompareType(Type, Info.Compiler.TypeCache.System_Boolean) OrElse Type.IsClass OrElse Type.IsInterface)
        Info.Stack.Pop(Type)
        Info.ILGen.Emit(OpCodes.Brtrue, Label)
    End Sub

    Shared Sub EmitBranch(ByVal Info As EmitInfo, ByVal Label As Label)
        Info.ILGen.Emit(OpCodes.Br, Label)
    End Sub

    Shared Sub EmitBranchOrLeave(ByVal Info As EmitInfo, ByVal Label As Label, ByVal FromStatement As Statement, ByVal ToStatement As Statement)
        If IsLeaveNecessary(FromStatement, ToStatement) Then
            EmitLeave(Info, Label)
        Else
            EmitBranch(Info, Label)
        End If
    End Sub

    Shared Sub EmitLeave(ByVal Info As EmitInfo, ByVal Label As Label)
        Info.ILGen.Emit(OpCodes.Leave, Label)
        Info.Stack.Clear()
    End Sub

    Shared Sub EmitSub(ByVal Info As EmitInfo, ByVal SubType As Type)
        SubType = Helper.GetTypeOrTypeBuilder(SubType)
        Info.Stack.Pop(SubType)
        Info.Stack.Pop(SubType)
        Info.ILGen.Emit(OpCodes.Sub)
        Info.Stack.Push(SubType)
    End Sub

    Shared Sub EmitSubOvf(ByVal Info As EmitInfo, ByVal SubType As Type)
        SubType = Helper.GetTypeOrTypeBuilder(SubType)
        Info.Stack.Pop(SubType)
        Info.Stack.Pop(SubType)
        Info.ILGen.Emit(OpCodes.Sub_Ovf)
        Info.Stack.Push(SubType)
    End Sub

    Shared Sub EmitSubOvfUn(ByVal Info As EmitInfo, ByVal SubType As Type)
        SubType = Helper.GetTypeOrTypeBuilder(SubType)
        Info.Stack.Pop(SubType)
        Info.Stack.Pop(SubType)
        Info.ILGen.Emit(OpCodes.Sub_Ovf_Un)
        Info.Stack.Push(SubType)
    End Sub

    Shared Sub EmitSubOrSubOvfOrSubOvfUn(ByVal Info As EmitInfo, ByVal SubType As Type)
        If Info.IntegerOverflowChecks Then
            EmitSubOvf(Info, SubType)
        Else
            EmitSub(Info, SubType)
        End If
    End Sub

    Shared Sub EmitOr(ByVal Info As EmitInfo, ByVal OrType As Type)
        OrType = Helper.GetTypeOrTypeBuilder(OrType)
        Info.Stack.Pop(OrType)
        Info.Stack.Pop(OrType)
        Info.ILGen.Emit(OpCodes.Or)
        Info.Stack.Push(OrType)
    End Sub

    Shared Sub EmitAnd(ByVal Info As EmitInfo, ByVal AndType As Type)
        AndType = Helper.GetTypeOrTypeBuilder(AndType)
        Info.Stack.Pop(AndType)
        Info.Stack.Pop(AndType)
        Info.ILGen.Emit(OpCodes.And)
        Info.Stack.Push(AndType)
    End Sub

    Shared Sub EmitXOr(ByVal Info As EmitInfo, ByVal XorType As Type)
        XorType = Helper.GetTypeOrTypeBuilder(XorType)
        Info.Stack.Pop(XorType)
        Info.Stack.Pop(XorType)
        Info.ILGen.Emit(OpCodes.Xor)
        Info.Stack.Push(XorType)
    End Sub

    Shared Sub EmitNot(ByVal Info As EmitInfo, ByVal NotType As Type)
        NotType = Helper.GetTypeOrTypeBuilder(NotType)
        Info.Stack.Pop(NotType)
        Info.ILGen.Emit(OpCodes.Not)
        Info.Stack.Push(NotType)
    End Sub

    Shared Sub EmitMod(ByVal Info As EmitInfo, ByVal ModType As Type)
        ModType = Helper.GetTypeOrTypeBuilder(ModType)
        Info.Stack.Pop(ModType)
        Info.Stack.Pop(ModType)
        Info.ILGen.Emit(OpCodes.[Rem])
        Info.Stack.Push(ModType)
    End Sub

    Shared Sub EmitEquals(ByVal Info As EmitInfo, ByVal CompareType As Type)
        CompareType = Helper.GetTypeOrTypeBuilder(CompareType)
        Info.Stack.Pop(CompareType)
        Info.Stack.Pop(CompareType)
        Info.ILGen.Emit(OpCodes.Ceq)
        Info.Stack.Push(Info.Compiler.TypeCache.System_Boolean)
    End Sub

    Shared Sub EmitNotEquals(ByVal Info As EmitInfo, ByVal CompareType As Type)
        CompareType = Helper.GetTypeOrTypeBuilder(CompareType)
        Info.Stack.Pop(CompareType)
        Info.Stack.Pop(CompareType)
        Info.ILGen.Emit(OpCodes.Ceq)
        Info.ILGen.Emit(OpCodes.Ldc_I4_0)
        Info.ILGen.Emit(OpCodes.Ceq)
        Info.Stack.Push(Info.Compiler.TypeCache.System_Boolean)
    End Sub

    Shared Sub EmitGE(ByVal Info As EmitInfo, ByVal CompareType As Type)
        CompareType = Helper.GetTypeOrTypeBuilder(CompareType)
        Info.Stack.Pop(CompareType)
        Info.Stack.Pop(CompareType)
        Info.ILGen.Emit(OpCodes.Clt)
        Info.ILGen.Emit(OpCodes.Ldc_I4_0)
        Info.ILGen.Emit(OpCodes.Ceq)
        Info.Stack.Push(Info.Compiler.TypeCache.System_Boolean)
    End Sub

    Shared Sub EmitGE_Un(ByVal Info As EmitInfo, ByVal CompareType As Type)
        CompareType = Helper.GetTypeOrTypeBuilder(CompareType)
        Info.Stack.Pop(CompareType)
        Info.Stack.Pop(CompareType)
        Info.ILGen.Emit(OpCodes.Clt_Un)
        Info.ILGen.Emit(OpCodes.Ldc_I4_0)
        Info.ILGen.Emit(OpCodes.Ceq)
        Info.Stack.Push(Info.Compiler.TypeCache.System_Boolean)
    End Sub

    Shared Sub EmitGT(ByVal Info As EmitInfo, ByVal CompareType As Type)
        CompareType = Helper.GetTypeOrTypeBuilder(CompareType)
        Info.Stack.Pop(CompareType)
        Info.Stack.Pop(CompareType)
        Info.ILGen.Emit(OpCodes.Cgt)
        Info.Stack.Push(Info.Compiler.TypeCache.System_Boolean)
    End Sub

    Shared Sub EmitGT_Un(ByVal Info As EmitInfo, ByVal CompareType As Type)
        CompareType = Helper.GetTypeOrTypeBuilder(CompareType)
        Info.Stack.Pop(CompareType)
        Info.Stack.Pop(CompareType)
        Info.ILGen.Emit(OpCodes.Cgt_Un)
        Info.Stack.Push(Info.Compiler.TypeCache.System_Boolean)
    End Sub

    Shared Sub EmitLE(ByVal Info As EmitInfo, ByVal CompareType As Type)
        CompareType = Helper.GetTypeOrTypeBuilder(CompareType)
        Info.Stack.Pop(CompareType)
        Info.Stack.Pop(CompareType)
        Info.ILGen.Emit(OpCodes.Cgt)
        Info.ILGen.Emit(OpCodes.Ldc_I4_0)
        Info.ILGen.Emit(OpCodes.Ceq)
        Info.Stack.Push(Info.Compiler.TypeCache.System_Boolean)
    End Sub

    Shared Sub EmitLE_Un(ByVal Info As EmitInfo, ByVal CompareType As Type)
        CompareType = Helper.GetTypeOrTypeBuilder(CompareType)
        Info.Stack.Pop(CompareType)
        Info.Stack.Pop(CompareType)
        Info.ILGen.Emit(OpCodes.Cgt_Un)
        Info.ILGen.Emit(OpCodes.Ldc_I4_0)
        Info.ILGen.Emit(OpCodes.Ceq)
        Info.Stack.Push(Info.Compiler.TypeCache.System_Boolean)
    End Sub

    Shared Sub EmitLT_Un(ByVal Info As EmitInfo, ByVal CompareType As Type)
        CompareType = Helper.GetTypeOrTypeBuilder(CompareType)
        Info.Stack.Pop(CompareType)
        Info.Stack.Pop(CompareType)
        Info.ILGen.Emit(OpCodes.Clt_Un)
        Info.Stack.Push(Info.Compiler.TypeCache.System_Boolean)
    End Sub

    Shared Sub EmitLT(ByVal Info As EmitInfo, ByVal CompareType As Type)
        CompareType = Helper.GetTypeOrTypeBuilder(CompareType)
        Info.Stack.Pop(CompareType)
        Info.Stack.Pop(CompareType)
        Info.ILGen.Emit(OpCodes.Clt)
        Info.Stack.Push(Info.Compiler.TypeCache.System_Boolean)
    End Sub

    Shared Sub EmitAdd(ByVal Info As EmitInfo, ByVal OperandType As Type)
        OperandType = Helper.GetTypeOrTypeBuilder(OperandType)
        Info.Stack.Pop(OperandType)
        Info.Stack.Pop(OperandType)
        Info.ILGen.Emit(OpCodes.Add)
        Info.Stack.Push(OperandType)
    End Sub

    Shared Sub EmitAddOrAddOvf(ByVal Info As EmitInfo, ByVal OperandType As Type)
        If Info.IntegerOverflowChecks Then
            EmitAddOvf(Info, OperandType)
        Else
            EmitAdd(Info, OperandType)
        End If
    End Sub

    Shared Sub EmitAddOvf(ByVal Info As EmitInfo, ByVal OperandType As Type)
        OperandType = Helper.GetTypeOrTypeBuilder(OperandType)
        Info.Stack.Pop(OperandType)
        Info.Stack.Pop(OperandType)
        Info.ILGen.Emit(OpCodes.Add_Ovf)
        Info.Stack.Push(OperandType)
    End Sub

    Shared Sub EmitMult(ByVal Info As EmitInfo, ByVal OperandType As Type)
        OperandType = Helper.GetTypeOrTypeBuilder(OperandType)
        Info.Stack.Pop(OperandType)
        Info.Stack.Pop(OperandType)
        Info.ILGen.Emit(OpCodes.Mul)
        Info.Stack.Push(OperandType)
    End Sub

    Shared Sub EmitMultOvf(ByVal Info As EmitInfo, ByVal OperandType As Type)
        OperandType = Helper.GetTypeOrTypeBuilder(OperandType)
        Info.Stack.Pop(OperandType)
        Info.Stack.Pop(OperandType)
        Info.ILGen.Emit(OpCodes.Mul_Ovf)
        Info.Stack.Push(OperandType)
    End Sub

    Shared Sub EmitMultOrMultOvf(ByVal Info As EmitInfo, ByVal OperandType As Type)
        If Info.IntegerOverflowChecks Then
            EmitMultOvf(Info, OperandType)
        Else
            EmitMult(Info, OperandType)
        End If
    End Sub

    Shared Sub EmitIs(ByVal Info As EmitInfo)
        Info.Stack.Pop(Info.Stack.Peek)
        Info.Stack.Pop(Info.Stack.Peek)
        Info.ILGen.Emit(OpCodes.Ceq)
        Info.Stack.Push(Info.Compiler.TypeCache.System_Boolean)
    End Sub

    Shared Sub EmitRShift(ByVal Info As EmitInfo, ByVal OpType As Type)
        OpType = Helper.GetTypeOrTypeBuilder(OpType)
        Info.Stack.Pop(Info.Compiler.TypeCache.System_Int32)
        Info.Stack.Pop(OpType)
        Info.ILGen.Emit(OpCodes.Shr)
        Info.Stack.Push(OpType)
    End Sub

    Shared Sub EmitLShift(ByVal Info As EmitInfo, ByVal OpType As Type)
        OpType = Helper.GetTypeOrTypeBuilder(OpType)
        Info.Stack.Pop(Info.Compiler.TypeCache.System_Int32)
        Info.Stack.Pop(OpType)
        Info.ILGen.Emit(OpCodes.Shl)
        Info.Stack.Push(OpType)
    End Sub

    Shared Sub EmitIntDiv(ByVal Info As EmitInfo, ByVal OpType As Type)
        OpType = Helper.GetTypeOrTypeBuilder(OpType)
        Info.Stack.Pop(OpType)
        Info.Stack.Pop(OpType)
        Info.ILGen.Emit(OpCodes.Div)
        Info.Stack.Push(OpType)
    End Sub

    Shared Sub EmitRealDiv(ByVal Info As EmitInfo, ByVal OpType As Type)
        OpType = Helper.GetTypeOrTypeBuilder(OpType)
        Info.Stack.Pop(OpType)
        Info.Stack.Pop(OpType)
        Info.ILGen.Emit(OpCodes.Div)
        Info.Stack.Push(OpType)
    End Sub

    Shared Sub EmitIsNot(ByVal Info As EmitInfo)
        Info.Stack.Pop(Info.Stack.Peek)
        Info.Stack.Pop(Info.Stack.Peek)
        Info.ILGen.Emit(OpCodes.Ceq)
        Info.ILGen.Emit(OpCodes.Ldc_I4_0)
        Info.ILGen.Emit(OpCodes.Ceq)
        Info.Stack.Push(Info.Compiler.TypeCache.System_Boolean)
    End Sub
    ''' <summary>
    ''' Create a local parameter. 
    ''' In the future might return an already created parameter that is no longer beeing used.
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <param name="Type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CreateLocal(ByVal Info As EmitInfo, ByVal Type As Type) As LocalBuilder
        Type = Helper.GetTypeOrTypeBuilder(Type)
        Return Info.ILGen.DeclareLocal(Type)
    End Function


    <Diagnostics.Conditional("DEBUG")> _
    Private Shared Sub PopParameters(ByVal Info As EmitInfo, ByVal Params As ParameterInfo())
        For i As Integer = Params.GetUpperBound(0) To 0 Step -1
            Info.Stack.Pop(Params(i).ParameterType)
        Next
    End Sub

    <Diagnostics.Conditional("DEBUG")> _
    Private Shared Sub PopParameters(ByVal Info As EmitInfo, ByVal Params As Type())
        For i As Integer = Params.GetUpperBound(0) To 0 Step -1
            Info.Stack.Pop(Params(i))
        Next
    End Sub

    Shared Sub EmitDup(ByVal Info As EmitInfo)
        Info.ILGen.Emit(OpCodes.Dup)
        Info.Stack.Push(Info.Stack.Peek)
    End Sub

    ''' <summary>
    ''' Loads a pointer to the specified method onto the stack.
    ''' Loads either a Lftfn or Ldvirtftn, according to the static 
    ''' state of the method.
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <param name="Method"></param>
    ''' <remarks></remarks>
    Shared Sub EmitLoadVftn(ByVal Info As EmitInfo, ByVal Method As MethodBase)
        Dim methodinf As MethodInfo = TryCast(Method, MethodInfo)
        If methodinf IsNot Nothing Then
            methodinf = Helper.GetMethodOrMethodBuilder(methodinf)
            If methodinf.IsStatic Then
                Info.ILGen.Emit(OpCodes.Ldftn, methodinf)
            Else
                Info.Stack.Pop(methodinf.DeclaringType)
                Info.ILGen.Emit(OpCodes.Ldvirtftn, methodinf)
            End If
        Else
            Helper.Stop()
        End If
        Info.Stack.Push(Info.Compiler.TypeCache.System_IntPtr)
    End Sub

    Shared Sub EmitInitObj(ByVal Info As EmitInfo, ByVal Type As Type)
        Type = Helper.GetTypeOrTypeBuilder(Type)
        Info.ILGen.Emit(OpCodes.Initobj, Type)
        Info.Stack.Pop(Info.Compiler.TypeManager.MakeByRefType(CType(Info.Method, ParsedObject), Type))
    End Sub

    Shared Sub EmitNeg(ByVal Info As EmitInfo)
        Info.ILGen.Emit(OpCodes.Neg)
    End Sub

    ''' <summary>
    ''' Emit a newobj.
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <param name="Constructor"></param>
    ''' <remarks></remarks>
    Shared Sub EmitNew(ByVal Info As EmitInfo, ByVal Constructor As ConstructorInfo)
        Dim OriginalConstructor As ConstructorInfo = Constructor
        Helper.Assert(Constructor IsNot Nothing)
        PopParameters(Info, Constructor.GetParameters)
        Constructor = Helper.GetCtorOrCtorBuilder(Constructor)
        Info.ILGen.Emit(OpCodes.Newobj, Constructor)
        Info.Stack.Push(Constructor.DeclaringType)
    End Sub

    ''' <summary>
    ''' Emit a newobj.
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <param name="Constructor"></param>
    ''' <remarks></remarks>
    Shared Sub EmitNew(ByVal Info As EmitInfo, ByVal Constructor As MethodInfo, ByVal ParameterTypes() As Type)
        Dim OriginalConstructor As MethodInfo = Constructor
        Helper.Assert(Constructor IsNot Nothing)
        PopParameters(Info, ParameterTypes)
        Constructor = Helper.GetMethodOrMethodBuilder(Constructor)
        Info.ILGen.Emit(OpCodes.Newobj, Constructor)
        Info.Stack.Push(Constructor.DeclaringType)
    End Sub

    Shared Sub EmitCastClass(ByVal Info As EmitInfo, ByVal FromType As Type, ByVal ToType As Type)
        FromType = Helper.GetTypeOrTypeBuilder(FromType)
        ToType = Helper.GetTypeOrTypeBuilder(ToType)
        Helper.Assert(ToType.IsByRef = False)
        Info.Stack.Pop(FromType)
        Info.ILGen.Emit(OpCodes.Castclass, ToType)
        Info.Stack.Push(ToType)
    End Sub

    Shared Sub EmitIsInst(ByVal Info As EmitInfo, ByVal FromType As Type, ByVal ToType As Type)
        FromType = Helper.GetTypeOrTypeBuilder(FromType)
        ToType = Helper.GetTypeOrTypeBuilder(ToType)
        Info.Stack.Pop(FromType)
        Info.ILGen.Emit(OpCodes.Isinst, ToType)
        Info.Stack.Push(ToType)
    End Sub

    Shared Sub EmitValueTypeToObjectConversion(ByVal Info As EmitInfo, ByVal FromType As Type, ByVal ToType As Type)
        ToType = Helper.GetTypeOrTypeBuilder(ToType)
        'Dim FromType As Type = Info.Stack.Peek
        Dim FromTP, ToTP As TypeCode

        FromTP = Helper.GetTypeCode(Info.Compiler, FromType)
        ToTP = Helper.GetTypeCode(info.Compiler, ToType)

        Helper.Assert(ToTP = TypeCode.Object)

        If ToType.IsByRef AndAlso Helper.CompareType(ToType.GetElementType, FromType) Then
            Dim localvar As LocalBuilder = Info.ILGen.DeclareLocal(FromType)
            Emitter.EmitStoreVariable(Info, localvar)
            Emitter.EmitLoadVariableLocation(Info, localvar)
        ElseIf FromType.BaseType.IsEnum AndAlso Helper.CompareType(ToType, Info.Compiler.TypeCache.System_Enum) Then

        Else
            Info.Compiler.Report.ShowMessage(Messages.VBNC99997, Info.Location)
        End If
    End Sub

    Shared Sub EmitConv_U1(ByVal Info As EmitInfo, ByVal FromType As Type)
        FromType = Helper.GetTypeOrTypeBuilder(FromType)
        Info.Stack.Pop(FromType)
        Info.ILGen.Emit(OpCodes.Conv_U1)
        Info.Stack.Push(Info.Compiler.TypeCache.System_Byte)
    End Sub

    Shared Sub EmitConv_U1_Overflow(ByVal Info As EmitInfo, ByVal FromType As Type)
        FromType = Helper.GetTypeOrTypeBuilder(FromType)
        Info.Stack.Pop(FromType)
        Info.ILGen.Emit(OpCodes.Conv_Ovf_U1)
        Info.Stack.Push(Info.Compiler.TypeCache.System_Byte)
    End Sub

    Shared Sub EmitConv_U1_Overflow_Underflow(ByVal Info As EmitInfo, ByVal FromType As Type)
        FromType = Helper.GetTypeOrTypeBuilder(FromType)
        Info.Stack.Pop(FromType)
        Info.ILGen.Emit(OpCodes.Conv_Ovf_U1_Un)
        Info.Stack.Push(Info.Compiler.TypeCache.System_Byte)
    End Sub

    Shared Sub EmitConv_I1(ByVal Info As EmitInfo, ByVal FromType As Type)
        FromType = Helper.GetTypeOrTypeBuilder(FromType)
        Info.Stack.Pop(FromType)
        Info.ILGen.Emit(OpCodes.Conv_I1)
        Info.Stack.Push(Info.Compiler.TypeCache.System_SByte)
    End Sub

    Shared Sub EmitConv_I1_Overflow(ByVal Info As EmitInfo, ByVal FromType As Type)
        FromType = Helper.GetTypeOrTypeBuilder(FromType)
        Info.Stack.Pop(FromType)
        Info.ILGen.Emit(OpCodes.Conv_Ovf_I1)
        Info.Stack.Push(Info.Compiler.TypeCache.System_SByte)
    End Sub

    Shared Sub EmitConv_I1_Overflow_Underflow(ByVal Info As EmitInfo, ByVal FromType As Type)
        FromType = Helper.GetTypeOrTypeBuilder(FromType)
        Info.Stack.Pop(FromType)
        Info.ILGen.Emit(OpCodes.Conv_Ovf_I1_Un)
        Info.Stack.Push(Info.Compiler.TypeCache.System_SByte)
    End Sub

    Shared Sub EmitConv_U2(ByVal Info As EmitInfo, ByVal FromType As Type)
        FromType = Helper.GetTypeOrTypeBuilder(FromType)
        Info.Stack.Pop(FromType)
        Info.ILGen.Emit(OpCodes.Conv_U2)
        Info.Stack.Push(Info.Compiler.TypeCache.System_UInt16)
    End Sub

    Shared Sub EmitConv_U2_Overflow(ByVal Info As EmitInfo, ByVal FromType As Type)
        FromType = Helper.GetTypeOrTypeBuilder(FromType)
        Info.Stack.Pop(FromType)
        Info.ILGen.Emit(OpCodes.Conv_Ovf_U2)
        Info.Stack.Push(Info.Compiler.TypeCache.System_UInt16)
    End Sub

    Shared Sub EmitConv_U2_Overflow_Underflow(ByVal Info As EmitInfo, ByVal FromType As Type)
        FromType = Helper.GetTypeOrTypeBuilder(FromType)
        Info.Stack.Pop(FromType)
        Info.ILGen.Emit(OpCodes.Conv_Ovf_U2_Un)
        Info.Stack.Push(Info.Compiler.TypeCache.System_UInt16)
    End Sub

    Shared Sub EmitConv_I2(ByVal Info As EmitInfo, ByVal FromType As Type)
        FromType = Helper.GetTypeOrTypeBuilder(FromType)
        Info.Stack.Pop(FromType)
        Info.ILGen.Emit(OpCodes.Conv_I2)
        Info.Stack.Push(Info.Compiler.TypeCache.System_Int16)
    End Sub

    Shared Sub EmitConv_I2_Overflow(ByVal Info As EmitInfo, ByVal FromType As Type)
        FromType = Helper.GetTypeOrTypeBuilder(FromType)
        Info.Stack.Pop(FromType)
        Info.ILGen.Emit(OpCodes.Conv_Ovf_I2)
        Info.Stack.Push(Info.Compiler.TypeCache.System_Int16)
    End Sub

    Shared Sub EmitConv_I2_Overflow_Underflow(ByVal Info As EmitInfo, ByVal FromType As Type)
        FromType = Helper.GetTypeOrTypeBuilder(FromType)
        Info.Stack.Pop(FromType)
        Info.ILGen.Emit(OpCodes.Conv_Ovf_I2_Un)
        Info.Stack.Push(Info.Compiler.TypeCache.System_Int16)
    End Sub

    Shared Sub EmitConv_U4(ByVal Info As EmitInfo, ByVal FromType As Type)
        FromType = Helper.GetTypeOrTypeBuilder(FromType)
        Info.Stack.Pop(FromType)
        Info.ILGen.Emit(OpCodes.Conv_U4)
        Info.Stack.Push(Info.Compiler.TypeCache.System_UInt32)
    End Sub

    Shared Sub EmitConv_U4_Overflow(ByVal Info As EmitInfo, ByVal FromType As Type)
        FromType = Helper.GetTypeOrTypeBuilder(FromType)
        Info.Stack.Pop(FromType)
        Info.ILGen.Emit(OpCodes.Conv_Ovf_U4)
        Info.Stack.Push(Info.Compiler.TypeCache.System_UInt32)
    End Sub

    Shared Sub EmitConv_U4_Overflow_Underflow(ByVal Info As EmitInfo, ByVal FromType As Type)
        FromType = Helper.GetTypeOrTypeBuilder(FromType)
        Info.Stack.Pop(FromType)
        Info.ILGen.Emit(OpCodes.Conv_Ovf_U4_Un)
        Info.Stack.Push(Info.Compiler.TypeCache.System_UInt32)
    End Sub

    Shared Sub EmitConv_I4(ByVal Info As EmitInfo, ByVal FromType As Type)
        FromType = Helper.GetTypeOrTypeBuilder(FromType)
        Info.Stack.Pop(FromType)
        Info.ILGen.Emit(OpCodes.Conv_I4)
        Info.Stack.Push(Info.Compiler.TypeCache.System_Int32)
    End Sub

    Shared Sub EmitConv_I4_Overflow(ByVal Info As EmitInfo, ByVal FromType As Type)
        FromType = Helper.GetTypeOrTypeBuilder(FromType)
        Info.Stack.Pop(FromType)
        Info.ILGen.Emit(OpCodes.Conv_Ovf_I4)
        Info.Stack.Push(Info.Compiler.TypeCache.System_Int32)
    End Sub

    Shared Sub EmitConv_I4_Overflow_Underflow(ByVal Info As EmitInfo, ByVal FromType As Type)
        FromType = Helper.GetTypeOrTypeBuilder(FromType)
        Info.Stack.Pop(FromType)
        Info.ILGen.Emit(OpCodes.Conv_Ovf_I4_Un)
        Info.Stack.Push(Info.Compiler.TypeCache.System_Int32)
    End Sub

    Shared Sub EmitConv_U8(ByVal Info As EmitInfo, ByVal FromType As Type)
        FromType = Helper.GetTypeOrTypeBuilder(FromType)
        Info.Stack.Pop(FromType)
        Info.ILGen.Emit(OpCodes.Conv_U8)
        Info.Stack.Push(Info.Compiler.TypeCache.System_UInt64)
    End Sub

    Shared Sub EmitConv_U8_Overflow(ByVal Info As EmitInfo, ByVal FromType As Type)
        FromType = Helper.GetTypeOrTypeBuilder(FromType)
        Info.Stack.Pop(FromType)
        Info.ILGen.Emit(OpCodes.Conv_Ovf_U8)
        Info.Stack.Push(Info.Compiler.TypeCache.System_UInt64)
    End Sub

    Shared Sub EmitConv_U8_Overflow_Underflow(ByVal Info As EmitInfo, ByVal FromType As Type)
        FromType = Helper.GetTypeOrTypeBuilder(FromType)
        Info.Stack.Pop(FromType)
        Info.ILGen.Emit(OpCodes.Conv_Ovf_U8_Un)
        Info.Stack.Push(Info.Compiler.TypeCache.System_UInt64)
    End Sub

    Shared Sub EmitConv_I8(ByVal Info As EmitInfo, ByVal FromType As Type)
        FromType = Helper.GetTypeOrTypeBuilder(FromType)
        Info.Stack.Pop(FromType)
        Info.ILGen.Emit(OpCodes.Conv_I8)
        Info.Stack.Push(Info.Compiler.TypeCache.System_Int64)
    End Sub

    Shared Sub EmitConv_I8_Overflow(ByVal Info As EmitInfo, ByVal FromType As Type)
        FromType = Helper.GetTypeOrTypeBuilder(FromType)
        Info.Stack.Pop(FromType)
        Info.ILGen.Emit(OpCodes.Conv_Ovf_I8)
        Info.Stack.Push(Info.Compiler.TypeCache.System_Int64)
    End Sub

    Shared Sub EmitConv_I8_Overflow_Underflow(ByVal Info As EmitInfo, ByVal FromType As Type)
        FromType = Helper.GetTypeOrTypeBuilder(FromType)
        Info.Stack.Pop(FromType)
        Info.ILGen.Emit(OpCodes.Conv_Ovf_I8_Un)
        Info.Stack.Push(Info.Compiler.TypeCache.System_Int64)
    End Sub

    Shared Sub EmitConv_R8(ByVal Info As EmitInfo, ByVal FromType As Type)
        FromType = Helper.GetTypeOrTypeBuilder(FromType)
        Info.Stack.Pop(FromType)
        Info.ILGen.Emit(OpCodes.Conv_R8)
        Info.Stack.Push(Info.Compiler.TypeCache.System_Double)
    End Sub

    Shared Sub EmitConv_R4(ByVal Info As EmitInfo, ByVal FromType As Type)
        FromType = Helper.GetTypeOrTypeBuilder(FromType)
        Info.Stack.Pop(FromType)
        Info.ILGen.Emit(OpCodes.Conv_R4)
        Info.Stack.Push(Info.Compiler.TypeCache.System_Single)
    End Sub

    ''' <summary>
    ''' Convert the value on the stack to ToType.
    ''' </summary>
    ''' <param name="ToType"></param>
    ''' <remarks></remarks>
    Shared Sub EmitConversion(ByVal FromType As Type, ByVal ToType As Type, ByVal Info As EmitInfo)
        Dim ToTypeOriginal, FromTypeOriginal As Type

        If Helper.CompareType(FromType, Info.Compiler.TypeCache.Nothing) Then Return

        ToTypeOriginal = ToType
        ToType = Helper.GetTypeOrTypeBuilder(ToType)

        'Dim FromType As Type = Info.Stack.Peek
        Dim FromTP, ToTP As TypeCode
        Dim converted As Boolean = False

        FromTypeOriginal = FromType
        FromType = Helper.GetTypeOrTypeBuilder(FromType)

        FromTP = Helper.GetTypeCode(Info.Compiler, FromType)
        ToTP = Helper.GetTypeCode(info.Compiler, ToType)

#If DEBUGIMPLICITCONVERSION Then
                    		If FromTP <> ToTP OrElse FromTP = TypeCode.Object OrElse ToTP = TypeCode.Object Then
                    			Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, String.Format("Converting from '{0}' to '{1}'.", FromTP.ToString, ToTP.ToString))
                    		End If
#End If

        If ToTP = TypeCode.Object AndAlso FromTP <> TypeCode.Object AndAlso Helper.CompareType(ToType, Info.Compiler.TypeCache.System_Object) = False Then
            EmitValueTypeToObjectConversion(Info, FromType, ToType)
            Return
        End If

        Dim Switch As TypeCombinations = Helper.GetCombination(FromTP, ToTP)

        Select Case Switch
            Case TypeCombinations.Boolean_Boolean, _
             TypeCombinations.Byte_Byte, _
              TypeCombinations.Char_Char, _
               TypeCombinations.DateTime_DateTime, _
                TypeCombinations.Decimal_Decimal, _
                 TypeCombinations.Double_Double, _
                  TypeCombinations.Empty_Empty, _
                   TypeCombinations.Int16_Int16, _
                    TypeCombinations.Int32_Int32, _
                     TypeCombinations.Int64_Int64, _
                       TypeCombinations.SByte_SByte, _
                        TypeCombinations.Single_Single, _
                         TypeCombinations.String_String, _
                          TypeCombinations.UInt16_UInt16, _
                           TypeCombinations.UInt32_UInt32, _
                            TypeCombinations.UInt64_UInt64
                Return 'Nothing to convert, both types are equal
            Case TypeCombinations.Object_Object
                Dim tmpTo, tmpFrom As Type
                If Info.IsExplicitConversion = False Then
                    'If TypeOf FromType Is TypeDescriptor Then
                    '    tmpFrom = DirectCast(FromType, TypeDescriptor).TypeInReflection
                    'Else
                    '    tmpFrom = FromType
                    'End If
                    tmpFrom = FromTypeOriginal
                    'If TypeOf ToTypeIn Is TypeDescriptor Then
                    '    tmpTo = DirectCast(ToType, TypeDescriptor).TypeInReflection
                    'Else
                    tmpTo = ToTypeOriginal
                    '               End If
                    If Helper.CompareType(tmpTo, tmpFrom) Then
                        converted = True
                    ElseIf Helper.IsAssignable(Info.Context, tmpFrom, tmpTo) Then
                        converted = True
                    Else
                        converted = True
                        Info.Compiler.Report.WriteLine(Report.ReportLevels.Debug, "The compiler supposes a conversion from " & tmpFrom.Name & " to " & tmpTo.Name & " is valid.")
                    End If
                Else
                    Info.ILGen.Emit(OpCodes.Castclass, ToType)
                    'Emitter.EmitCastClass(Info, FromType, ToType)
                    converted = True
                End If
                ' ToSByte conversions
            Case TypeCombinations.Byte_SByte
                If Info.IsExplicitConversion Then
                    If Info.IntegerOverflowChecks Then
                        Info.ILGen.Emit(OpCodes.Conv_Ovf_I1_Un) : converted = True
                    Else
                        Info.ILGen.Emit(OpCodes.Conv_I1)
                    End If
                Else
                    Helper.Stop()
                End If
            Case TypeCombinations.Int16_SByte
            Case TypeCombinations.UInt16_SByte
            Case TypeCombinations.Int32_SByte
                If Info.IsExplicitConversion Then
                    If Info.IntegerOverflowChecks Then
                        Info.ILGen.Emit(OpCodes.Conv_Ovf_I1) : converted = True
                    Else
                        Info.ILGen.Emit(OpCodes.Conv_I1) : converted = True
                    End If
                Else
                    Helper.Stop()
                End If
            Case TypeCombinations.UInt32_SByte
            Case TypeCombinations.Int64_SByte
            Case TypeCombinations.UInt64_SByte
            Case TypeCombinations.Single_SByte
            Case TypeCombinations.Double_SByte
            Case TypeCombinations.Decimal_SByte

                'ToByte conversions
            Case TypeCombinations.SByte_Byte, _
              TypeCombinations.Int16_Byte, _
              TypeCombinations.Int32_Byte, _
              TypeCombinations.Int64_Byte
                If Info.IsExplicitConversion Then
                    If Info.IntegerOverflowChecks Then
                        Info.ILGen.Emit(OpCodes.Conv_Ovf_U1) : converted = True
                    Else
                        Info.ILGen.Emit(OpCodes.Conv_U1) : converted = True
                    End If
                Else
                    Helper.Stop()
                End If
            Case TypeCombinations.UInt16_Byte
            Case TypeCombinations.UInt32_Byte
            Case TypeCombinations.UInt64_Byte
            Case TypeCombinations.Single_Byte
            Case TypeCombinations.Double_Byte
            Case TypeCombinations.Decimal_Byte

                'ToUInt64 conversions
            Case TypeCombinations.Byte_UInt16
                Info.ILGen.Emit(OpCodes.Conv_U2)
                converted = True

                'ToInt16 conversions
            Case TypeCombinations.Byte_Int16, _
             TypeCombinations.SByte_Int16
                Info.ILGen.Emit(OpCodes.Conv_I2)
                converted = True


                'ToUInt32 conversions
            Case TypeCombinations.Byte_UInt32, _
               TypeCombinations.UInt16_UInt32
                Info.ILGen.Emit(OpCodes.Conv_U4)
                converted = True

                'ToInt32 conversions
            Case TypeCombinations.SByte_Int32, _
                 TypeCombinations.Byte_Int32, _
                 TypeCombinations.UInt16_Int32, _
                 TypeCombinations.Int16_Int32
                Info.ILGen.Emit(OpCodes.Conv_I4)
                converted = True
            Case TypeCombinations.Object_Int32
                'Narrowing conversion
                If Info.IsExplicitConversion = False Then
                    Helper.AddError(Info.Context)
                Else
                    Emitter.EmitUnbox(Info, Info.Compiler.TypeCache.System_Int32)
                    Emitter.EmitLdobj(Info, Info.Compiler.TypeCache.System_Int32)
                    converted = True
                End If

                'ToInt64 conversions
            Case TypeCombinations.Byte_Int64, _
              TypeCombinations.SByte_Int64, _
               TypeCombinations.UInt16_Int64, _
                TypeCombinations.Int16_Int64, _
                 TypeCombinations.UInt32_Int64, _
                  TypeCombinations.Int32_Int64
                Info.ILGen.Emit(OpCodes.Conv_I8)
                converted = True

                'ToUInt64 conversions
            Case TypeCombinations.Byte_UInt64, _
                  TypeCombinations.UInt16_UInt64, _
                   TypeCombinations.UInt32_UInt64
                Info.ILGen.Emit(OpCodes.Conv_U8)
                converted = True

                'ToSingle conversions
            Case TypeCombinations.SByte_Single, _
                  TypeCombinations.Byte_Single, _
                   TypeCombinations.UInt16_Single, _
                    TypeCombinations.Int16_Single, _
                     TypeCombinations.UInt32_Single, _
                      TypeCombinations.Int32_Single, _
                       TypeCombinations.UInt64_Single, _
                        TypeCombinations.Int64_Single
                Info.ILGen.Emit(OpCodes.Conv_R4)
                converted = True
            Case TypeCombinations.Decimal_Single
                Info.ILGen.Emit(OpCodes.Call, Info.Compiler.TypeCache.System_Convert__ToSingle_Decimal)
                converted = True

                'ToDouble conversions
            Case TypeCombinations.Decimal_Double
                Info.ILGen.Emit(OpCodes.Call, Info.Compiler.TypeCache.System_Convert__ToDouble_Decimal)
                converted = True
            Case TypeCombinations.SByte_Double, _
                  TypeCombinations.Byte_Double, _
                   TypeCombinations.UInt16_Double, _
                    TypeCombinations.Int16_Double, _
                     TypeCombinations.UInt32_Double, _
                      TypeCombinations.Int32_Double, _
                       TypeCombinations.UInt64_Double, _
                        TypeCombinations.Int64_Double, _
                         TypeCombinations.Single_Double
                Info.ILGen.Emit(OpCodes.Conv_R8)
                converted = True

                'ToDecimal conversions
            Case TypeCombinations.Byte_Decimal, _
             TypeCombinations.SByte_Decimal, _
             TypeCombinations.Int16_Decimal
                Info.ILGen.Emit(OpCodes.Conv_I4)
                Info.ILGen.Emit(OpCodes.Newobj, Info.Compiler.TypeCache.System_Decimal__ctor_Int32)
                converted = True
            Case TypeCombinations.UInt16_Decimal, _
               TypeCombinations.UInt32_Decimal
                Info.ILGen.Emit(OpCodes.Conv_I8)
                Info.ILGen.Emit(OpCodes.Newobj, Info.Compiler.TypeCache.System_Decimal__ctor_Int64)
                converted = True
            Case TypeCombinations.Int32_Decimal
                Info.ILGen.Emit(OpCodes.Newobj, Info.Compiler.TypeCache.System_Decimal__ctor_Int32)
                converted = True
            Case TypeCombinations.UInt64_Decimal
                Info.ILGen.Emit(OpCodes.Newobj, Info.Compiler.TypeCache.System_Decimal__ctor_UInt64)
                converted = True
            Case TypeCombinations.Int64_Decimal
                Info.ILGen.Emit(OpCodes.Newobj, Info.Compiler.TypeCache.System_Decimal__ctor_Int64)
                converted = True

                'ToObject conversions
            Case TypeCombinations.SByte_Object
                Info.ILGen.Emit(OpCodes.Box, Info.Compiler.TypeCache.System_SByte)
                converted = True
            Case TypeCombinations.Byte_Object
                Info.ILGen.Emit(OpCodes.Box, Info.Compiler.TypeCache.System_Byte)
                converted = True
            Case TypeCombinations.Int16_Object
                Info.ILGen.Emit(OpCodes.Box, Info.Compiler.TypeCache.System_Int16)
                converted = True
            Case TypeCombinations.UInt16_Object
                Info.ILGen.Emit(OpCodes.Box, Info.Compiler.TypeCache.System_UInt16)
                converted = True
            Case TypeCombinations.Int32_Object
                Info.ILGen.Emit(OpCodes.Box, Info.Compiler.TypeCache.System_Int32)
                converted = True
            Case TypeCombinations.UInt32_Object
                Info.ILGen.Emit(OpCodes.Box, Info.Compiler.TypeCache.System_UInt32)
                converted = True
            Case TypeCombinations.Int64_Object
                Info.ILGen.Emit(OpCodes.Box, Info.Compiler.TypeCache.System_Int64)
                converted = True
            Case TypeCombinations.UInt64_Object
                Info.ILGen.Emit(OpCodes.Box, Info.Compiler.TypeCache.System_UInt64)
                converted = True
            Case TypeCombinations.Single_Object
                Info.ILGen.Emit(OpCodes.Box, Info.Compiler.TypeCache.System_Single)
                converted = True
            Case TypeCombinations.Double_Object
                Info.ILGen.Emit(OpCodes.Box, Info.Compiler.TypeCache.System_Double)
                converted = True
            Case TypeCombinations.String_Object
                converted = True
            Case TypeCombinations.Decimal_Object
                Info.ILGen.Emit(OpCodes.Box, Info.Compiler.TypeCache.System_Decimal)
                converted = True
            Case TypeCombinations.DateTime_Object
                Info.ILGen.Emit(OpCodes.Box, Info.Compiler.TypeCache.System_DateTime)
                converted = True
            Case TypeCombinations.Boolean_Object
                Info.ILGen.Emit(OpCodes.Box, Info.Compiler.TypeCache.System_Boolean)
                converted = True
            Case TypeCombinations.Char_Object
                Info.ILGen.Emit(OpCodes.Box, Info.Compiler.TypeCache.System_Char)
                converted = True
            Case TypeCombinations.DBNull_Object
                converted = True 'Nothing to object
            Case TypeCombinations.Object_String
                Emitter.EmitCastClass(Info, Info.Compiler.TypeCache.System_Object, Info.Compiler.TypeCache.System_String)
                converted = True
            Case Else
                Info.Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "Missed option: " & Switch.ToString)
                Helper.Stop()
        End Select

        If Not converted Then
            Info.Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "Skipped option: " & Switch.ToString)
            Helper.Stop()
        Else
            Info.Stack.Pop(FromType)
            Info.Stack.Push(ToType)
        End If
    End Sub

    ''' <summary>
    ''' Loads Me onto the evaluation stack.
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <remarks></remarks>
    Shared Sub EmitLoadMe(ByVal Info As EmitInfo, ByVal TypeOfMe As Type)
        TypeOfMe = Helper.GetTypeOrTypeBuilder(TypeOfMe)
        Info.ILGen.Emit(OpCodes.Ldarg_0)
        If TypeOfMe.IsValueType Then
            Info.Stack.Push(Info.Compiler.TypeManager.MakeByRefType(CType(Info.Method, ParsedObject), TypeOfMe))
        Else
            Info.Stack.Push(TypeOfMe)
        End If
    End Sub

    Shared Sub EmitCall(ByVal Info As EmitInfo, ByVal Method As MethodBase)
        Dim OriginalMethod As MethodBase = Method

        Helper.Assert(Method IsNot Nothing)

        Dim minfo As MethodInfo = TryCast(Method, MethodInfo)
        Dim cinfo As ConstructorInfo = TryCast(Method, ConstructorInfo)

        If minfo IsNot Nothing Then
            Emitter.EmitCall(Info, minfo)
        ElseIf cinfo IsNot Nothing Then
            Emitter.EmitCall(Info, cinfo)
        Else
            Helper.Stop()
        End If
    End Sub

    Shared Sub EmitCall(ByVal Info As EmitInfo, ByVal Method As MethodInfo)
        Dim OriginalMethod As MethodBase = Method

        Helper.Assert(Method IsNot Nothing, "The method Is Nothing")

        Dim minfo As MethodInfo
        minfo = Helper.GetMethodOrMethodBuilder(Method)

        minfo = SwitchVersionedMethods(Info, minfo)

        PopParameters(Info, Helper.GetParameters(Info.Compiler, Method))
        If Method.IsStatic Then
            Info.ILGen.Emit(OpCodes.Call, minfo)
        ElseIf Method.DeclaringType.IsValueType Then
            Info.Stack.Pop(Info.Compiler.TypeManager.MakeByRefType(CType(Info.Method, ParsedObject), Method.DeclaringType))
            Info.ILGen.Emit(OpCodes.Call, minfo)
        Else
            Info.Stack.Pop(Method.DeclaringType)
            Info.ILGen.Emit(OpCodes.Call, minfo)
        End If

        If minfo.ReturnType IsNot Nothing AndAlso Helper.CompareType(minfo.ReturnType, Info.Compiler.TypeCache.System_Void) = False Then
            Info.Stack.Push(DirectCast(OriginalMethod, MethodInfo).ReturnType)
        End If

    End Sub

    Shared Sub EmitCallOrCallVirt(ByVal Info As EmitInfo, ByVal Method As MethodBase)
        Dim minfo As MethodInfo = TryCast(Method, MethodInfo)
        Dim cinfo As ConstructorInfo = TryCast(Method, ConstructorInfo)

        If minfo IsNot Nothing Then
            EmitCallOrCallVirt(Info, minfo)
        ElseIf cinfo IsNot Nothing Then
            EmitCall(Info, cinfo)
        Else
            Helper.Stop()
        End If
    End Sub

    Shared Sub EmitCallOrCallVirt(ByVal Info As EmitInfo, ByVal Method As MethodInfo)
        Helper.Assert(Method IsNot Nothing)
        If Method.IsStatic OrElse Method.DeclaringType.IsValueType Then
            EmitCall(Info, Method)
        Else
            EmitCallVirt(Info, Method)
        End If
    End Sub

    Shared Sub EmitLoadToken(ByVal Info As EmitInfo, ByVal Type As Type)
        Type = Helper.GetTypeOrTypeBuilder(Type)
        Helper.Assert(Type IsNot Nothing)
        Info.ILGen.Emit(OpCodes.Ldtoken, Type)
        Info.Stack.Push(Info.Compiler.TypeCache.System_RuntimeTypeHandle)
    End Sub


    Shared Sub EmitConstrained(ByVal Info As EmitInfo, ByVal Type As Type)
        Dim OriginalType As Type = Type

        Type = Helper.GetTypeOrTypeBuilder(Type)

        Info.ILGen.Emit(OpCodes.Constrained, Type)
    End Sub

    ''' <summary>
    ''' Emits a constrained callvirt instructions. 
    ''' Throws an exception if the method is a shared method.
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <param name="Method"></param>
    ''' <remarks></remarks>
    Shared Sub EmitConstrainedCallVirt(ByVal Info As EmitInfo, ByVal Method As MethodBase, ByVal ConstrainedType As Type)
        Dim OriginalMethod As MethodBase = Method

        Helper.Assert(Method IsNot Nothing)
        Helper.Assert(ConstrainedType IsNot Nothing)

        EmitConstrained(Info, ConstrainedType)
        EmitCallVirt(Info, Method)
    End Sub

    Shared Sub EmitCallVirt(ByVal Info As EmitInfo, ByVal Method As MethodBase)
        Select Case Method.MemberType
            Case MemberTypes.Method
                EmitCallVirt(Info, DirectCast(Method, MethodInfo))
            Case Else
                Info.Compiler.Report.ShowMessage(Messages.VBNC99997, Info.Location)
        End Select
    End Sub

    ''' <summary>
    ''' Emits a callvirt instructions. 
    ''' Throws an exception if the method is a shared method.
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <param name="Method"></param>
    ''' <remarks></remarks>
    Shared Sub EmitCallVirt(ByVal Info As EmitInfo, ByVal Method As MethodInfo)
        Dim OriginalMethod As MethodInfo = Method

        Helper.Assert(Method IsNot Nothing)

        PopParameters(Info, Helper.GetParameters(Info.Compiler, Method))

        Method = Helper.GetMethodOrMethodBuilder(OriginalMethod)
        Method = SwitchVersionedMethods(Info, Method)

#If DEBUG Then
        If Method.GetType.Name <> "SymbolMethod" Then
            If Method.IsStatic Then
                Throw New InternalException("")
            ElseIf Method.DeclaringType.IsValueType Then
                Throw New InternalException("")
            End If
        End If
#End If
        Info.Stack.Pop(Method.DeclaringType)
        Info.ILGen.EmitCall(OpCodes.Callvirt, Method, Nothing)

        If OriginalMethod.ReturnType IsNot Nothing AndAlso Helper.CompareType(OriginalMethod.ReturnType, Info.Compiler.TypeCache.System_Void) = False Then
            Info.Stack.Push(OriginalMethod.ReturnType)
        End If
    End Sub

    ''' <summary>
    ''' Emits a call to a constructor (not a new expression) 
    ''' Throws an exception if the method is a shared method.
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <param name="Method"></param>
    ''' <remarks></remarks>
    Shared Sub EmitCall(ByVal Info As EmitInfo, ByVal Method As ConstructorInfo)
        Helper.Assert(Method IsNot Nothing)

        PopParameters(Info, Helper.GetParameters(Info.Compiler, Method))
        Method = Helper.GetCtorOrCtorBuilder(Method)

        If Method.IsStatic Then
            Throw New InternalException("")
        Else
            Method = Helper.GetCtorOrCtorBuilder(Method)

            Info.Stack.Pop(Method.DeclaringType)
            Info.ILGen.Emit(OpCodes.Call, Method)
        End If
    End Sub

    Shared Sub EmitNewArr(ByVal Info As EmitInfo, ByVal Type As Type)
        Type = Helper.GetTypeOrTypeBuilder(Type)
        Info.Stack.Pop(Info.Compiler.TypeCache.System_Int32)
        Info.ILGen.Emit(OpCodes.Newarr, Type)
        Info.Stack.Push(Type.MakeArrayType)
    End Sub

    Shared Sub EmitLoadElementAddress(ByVal Info As EmitInfo, ByVal ElementType As Type, ByVal ArrayType As Type)
        ArrayType = Helper.GetTypeOrTypeBuilder(ArrayType)
        ElementType = Helper.GetTypeOrTypeBuilder(ElementType)
        Info.ILGen.Emit(OpCodes.Ldelema, ElementType)
        Info.Stack.Pop(Info.Compiler.TypeCache.System_Int32)
        Info.Stack.Pop(ArrayType)
        Info.Stack.Push(Info.Compiler.TypeManager.MakeByRefType(CType(Info.Method, ParsedObject), ElementType))
    End Sub

    Shared Sub EmitLoadElement(ByVal Info As EmitInfo, ByVal ArrayType As Type)
        ArrayType = Helper.GetTypeOrTypeBuilder(ArrayType)
        Info.Stack.Pop(Info.Compiler.TypeCache.System_Int32)
        Info.Stack.Pop(ArrayType)
        Dim ElementType As Type = ArrayType.GetElementType
        Select Case Helper.GetTypeCode(Info.Compiler, ElementType)
            Case TypeCode.Byte
                Info.ILGen.Emit(OpCodes.Ldelem_U1)
            Case TypeCode.SByte, TypeCode.Boolean
                Info.ILGen.Emit(OpCodes.Ldelem_I1)
            Case TypeCode.UInt16, TypeCode.Char
                Info.ILGen.Emit(OpCodes.Ldelem_U2)
            Case TypeCode.Int16
                Info.ILGen.Emit(OpCodes.Ldelem_I2)
            Case TypeCode.UInt32
                Info.ILGen.Emit(OpCodes.Ldelem_U4)
            Case TypeCode.Int32
                Info.ILGen.Emit(OpCodes.Ldelem_I4)
            Case TypeCode.UInt64, TypeCode.Int64
                Info.ILGen.Emit(OpCodes.Ldelem_I8)
            Case TypeCode.Single
                Info.ILGen.Emit(OpCodes.Ldelem_R4)
            Case TypeCode.Double
                Info.ILGen.Emit(OpCodes.Ldelem_R8)
            Case TypeCode.Object, TypeCode.String, TypeCode.DateTime, TypeCode.Decimal
                If ElementType.IsValueType Then
                    Throw New InternalException("")
                Else
                    Info.ILGen.Emit(OpCodes.Ldelem_Ref)
                End If
            Case Else
                Info.Compiler.Report.ShowMessage(Messages.VBNC99997, Info.Location)
        End Select
        Info.Stack.Push(ElementType)
    End Sub

    Shared Sub LoadElement(ByVal Info As EmitInfo, ByVal ElementType As Type)
        ElementType = Helper.GetTypeOrTypeBuilder(ElementType)
        Select Case Helper.GetTypeCode(Info.Compiler, ElementType)
            Case TypeCode.Byte
                Info.ILGen.Emit(OpCodes.Ldelem_U1)
            Case TypeCode.UInt16, TypeCode.Char
                Info.ILGen.Emit(OpCodes.Ldelem_U2)
            Case TypeCode.UInt32
                Info.ILGen.Emit(OpCodes.Ldelem_U4)
            Case TypeCode.UInt64
                Info.ILGen.Emit(OpCodes.Ldelem_I8)
            Case TypeCode.SByte, TypeCode.Boolean
                Info.ILGen.Emit(OpCodes.Ldelem_I1)
            Case TypeCode.Int16
                Info.ILGen.Emit(OpCodes.Ldelem_I2)
            Case TypeCode.Int32
                Info.ILGen.Emit(OpCodes.Ldelem_I4)
            Case TypeCode.Int64
                Info.ILGen.Emit(OpCodes.Ldelem_I8)
            Case TypeCode.Single
                Info.ILGen.Emit(OpCodes.Ldelem_R4)
            Case TypeCode.Double
                Info.ILGen.Emit(OpCodes.Ldelem_R8)
            Case TypeCode.DateTime, TypeCode.Decimal
                Info.ILGen.Emit(OpCodes.Ldelema, ElementType)
                Info.ILGen.Emit(OpCodes.Ldobj, ElementType)
            Case TypeCode.String
                Info.ILGen.Emit(OpCodes.Ldelem_Ref)
            Case Else
                Info.Compiler.Report.ShowMessage(Messages.VBNC99997, Info.Location)
        End Select
        Info.Stack.Pop(Info.Compiler.TypeCache.System_Int32)
        Info.Stack.Pop(ElementType.MakeArrayType)
        Info.Stack.Push(ElementType)
    End Sub


    ''' <summary>
    ''' Type = the type of the element. (not of the array.)
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <param name="ElementType"></param>
    ''' <remarks></remarks>
    Shared Sub EmitStoreElement(ByVal Info As EmitInfo, ByVal ElementType As Type, ByVal ArrayType As Type)
        ArrayType = Helper.GetTypeOrTypeBuilder(ArrayType)
        ElementType = Helper.GetTypeOrTypeBuilder(ElementType)
        Select Case Helper.GetTypeCode(Info.Compiler, ElementType)
            Case TypeCode.Int32, TypeCode.UInt32
                Info.ILGen.Emit(OpCodes.Stelem_I4)
            Case TypeCode.SByte, TypeCode.Byte, TypeCode.Boolean
                Info.ILGen.Emit(OpCodes.Stelem_I1)
            Case TypeCode.Int16, TypeCode.UInt16, TypeCode.Char
                Info.ILGen.Emit(OpCodes.Stelem_I2)
            Case TypeCode.Int64, TypeCode.UInt64
                Info.ILGen.Emit(OpCodes.Stelem_I8)
            Case TypeCode.Single
                Info.ILGen.Emit(OpCodes.Stelem_R4)
            Case TypeCode.Double
                Info.ILGen.Emit(OpCodes.Stelem_R8)
            Case TypeCode.DateTime, TypeCode.Decimal
                EmitStoreObject(Info, ElementType)
                Return
            Case TypeCode.String
                Info.ILGen.Emit(OpCodes.Stelem_Ref)
            Case TypeCode.Object
                If ElementType.IsValueType Then
                    Info.ILGen.Emit(OpCodes.Stobj, ElementType)
                ElseIf ElementType.IsGenericParameter Then
                    Info.ILGen.Emit(OpCodes.Stelem, ElementType)
                Else
                    Info.ILGen.Emit(OpCodes.Stelem_Ref)
                End If
            Case Else
                Helper.Stop()
        End Select
        Info.Stack.Pop(ElementType)
        Info.Stack.Pop(Info.Compiler.TypeCache.System_Int32)
        Info.Stack.Pop(ArrayType)
    End Sub

    ''' <summary>
    ''' Emits a Stobj instruction.
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <param name="ElementType"></param>
    ''' <remarks></remarks>
    Shared Sub EmitStoreObject(ByVal Info As EmitInfo, ByVal ElementType As Type)
        ElementType = Helper.GetTypeOrTypeBuilder(ElementType)
        Info.ILGen.Emit(OpCodes.Stobj, ElementType)
        Info.Stack.Pop(ElementType)
        Info.Stack.Pop(Info.Compiler.TypeManager.MakeByRefType(CType(Info.Method, ParsedObject), ElementType))
    End Sub

    ''' <summary>
    ''' Emits a Ldobj instruction.
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <param name="ElementType"></param>
    ''' <remarks></remarks>
    Shared Sub EmitLoadObject(ByVal Info As EmitInfo, ByVal ElementType As Type)
        ElementType = Helper.GetTypeOrTypeBuilder(ElementType)
        Info.ILGen.Emit(OpCodes.Ldobj, ElementType)
        Info.Stack.Pop(Info.Compiler.TypeManager.MakeByRefType(CType(Info.Method, ParsedObject), ElementType))
        Info.Stack.Push(ElementType)
    End Sub
    ''' <summary>
    ''' Creates a new array and the new array reference is loaded at the top of the stack.
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <param name="ElementType"></param>
    ''' <param name="Elements"></param>
    ''' <remarks></remarks>
    Shared Sub CreateArray(ByVal Info As EmitInfo, ByVal ElementType As Type, ByVal Elements As Integer)
        ElementType = Helper.GetTypeOrTypeBuilder(ElementType)
        EmitLoadValue(Info.Clone(Info.Context, True, False, Info.Compiler.TypeCache.System_Int32), Elements)
        EmitNewArr(Info, ElementType)
    End Sub

  
    Shared Sub EmitLoadI8Value(ByVal Info As EmitInfo, ByVal I As ULong, ByVal TypeToPushOnStack As Type)
        TypeToPushOnStack = Helper.GetTypeOrTypeBuilder(TypeToPushOnStack)
        EmitLoadI8Value(Info, BitConverter.ToInt64(BitConverter.GetBytes(I), 0), TypeToPushOnStack)
    End Sub

    Shared Sub EmitLoadI8Value(ByVal Info As EmitInfo, ByVal I As ULong)
        EmitLoadI8Value(Info, BitConverter.ToInt64(BitConverter.GetBytes(I), 0), Info.Compiler.TypeCache.System_UInt64)
    End Sub

    Shared Sub EmitLoadI8Value(ByVal Info As EmitInfo, ByVal I As Long)
        Info.ILGen.Emit(OpCodes.Ldc_I8, I)
        Info.Stack.Push(Info.Compiler.TypeCache.System_Int64)
    End Sub

    Shared Sub EmitLoadI8Value(ByVal Info As EmitInfo, ByVal I As Long, ByVal TypeToPushOnStack As Type)
        TypeToPushOnStack = Helper.GetTypeOrTypeBuilder(TypeToPushOnStack)
        Info.ILGen.Emit(OpCodes.Ldc_I8, I)
        Info.Stack.Push(TypeToPushOnStack)
    End Sub

    Shared Sub EmitLoadR8Value(ByVal Info As EmitInfo, ByVal I As Double)
        Info.ILGen.Emit(OpCodes.Ldc_R8, I)
        Info.Stack.Push(Info.Compiler.TypeCache.System_Double)
    End Sub

    Shared Sub EmitLoadR8Value(ByVal Info As EmitInfo, ByVal I As Double, ByVal TypeToPushOnStack As Type)
        TypeToPushOnStack = Helper.GetTypeOrTypeBuilder(TypeToPushOnStack)
        Info.ILGen.Emit(OpCodes.Ldc_R8, I)
        Info.Stack.Push(TypeToPushOnStack)
    End Sub

    Shared Sub EmitLoadR4Value(ByVal Info As EmitInfo, ByVal I As Single)
        Info.ILGen.Emit(OpCodes.Ldc_R4, I)
        Info.Stack.Push(Info.Compiler.TypeCache.System_Single)
    End Sub

    Shared Sub EmitLoadI4Value(ByVal Info As EmitInfo, ByVal I As Integer, ByVal TypeToPushOnStack As Type)
        TypeToPushOnStack = Helper.GetTypeOrTypeBuilder(TypeToPushOnStack)
        Select Case I
            Case -1
                Info.ILGen.Emit(OpCodes.Ldc_I4_M1)
            Case 0
                Info.ILGen.Emit(OpCodes.Ldc_I4_0)
            Case 1
                Info.ILGen.Emit(OpCodes.Ldc_I4_1)
            Case 2
                Info.ILGen.Emit(OpCodes.Ldc_I4_2)
            Case 3
                Info.ILGen.Emit(OpCodes.Ldc_I4_3)
            Case 4
                Info.ILGen.Emit(OpCodes.Ldc_I4_4)
            Case 5
                Info.ILGen.Emit(OpCodes.Ldc_I4_5)
            Case 6
                Info.ILGen.Emit(OpCodes.Ldc_I4_6)
            Case 7
                Info.ILGen.Emit(OpCodes.Ldc_I4_7)
            Case 8
                Info.ILGen.Emit(OpCodes.Ldc_I4_8)
            Case SByte.MinValue To SByte.MaxValue
                Dim sbit As SByte = CSByte(I)
                Info.ILGen.Emit(OpCodes.Ldc_I4_S, sbit)
            Case Else
                Info.ILGen.Emit(OpCodes.Ldc_I4, I)
        End Select
        Info.Stack.Push(TypeToPushOnStack)
    End Sub
    Shared Sub EmitLoadR4Value(ByVal Info As EmitInfo, ByVal I As Single, ByVal TypeToPushOnStack As Type)
        TypeToPushOnStack = Helper.GetTypeOrTypeBuilder(TypeToPushOnStack)
        Info.ILGen.Emit(OpCodes.Ldc_R4, I)
        Info.Stack.Push(TypeToPushOnStack)
    End Sub

    Shared Sub EmitLoadI4Value(ByVal Info As EmitInfo, ByVal I As Boolean)
        If I Then
            EmitLoadI4Value(Info, -1, Info.Compiler.TypeCache.System_Boolean)
        Else
            EmitLoadI4Value(Info, 0, Info.Compiler.TypeCache.System_Boolean)
        End If
    End Sub

    Shared Sub EmitLoadI4Value(ByVal Info As EmitInfo, ByVal I As UInteger)
        EmitLoadI4Value(Info, BitConverter.ToInt32(BitConverter.GetBytes(I), 0), Info.Compiler.TypeCache.System_UInt32)
    End Sub

    Shared Sub EmitLoadI4Value(ByVal Info As EmitInfo, ByVal I As UInteger, ByVal TypeToPushOnStack As Type)
        TypeToPushOnStack = Helper.GetTypeOrTypeBuilder(TypeToPushOnStack)
        EmitLoadI4Value(Info, BitConverter.ToInt32(BitConverter.GetBytes(I), 0), TypeToPushOnStack)
    End Sub

    Shared Sub EmitLoadI4Value(ByVal Info As EmitInfo, ByVal I As Integer)
        EmitLoadI4Value(Info, I, Info.Compiler.TypeCache.System_Int32)
    End Sub

    ''' <summary>
    ''' Loads an value of the desired type onto the evaluation stack.
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <param name="Value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Overloads Shared Function EmitLoadValue(ByVal Info As EmitInfo, ByVal Value As Integer) As Boolean
        Helper.Assert(Info.DesiredType IsNot Nothing)

        Dim tmp As EmitInfo = Info
        Dim DesiredTypeCode As TypeCode = Helper.GetTypeCode(Info.Compiler, Info.DesiredType)

        Select Case DesiredTypeCode
            Case TypeCode.SByte, TypeCode.Int16, TypeCode.Int32, TypeCode.Boolean
                EmitLoadI4Value(Info, Value, Info.DesiredType)
                Return True
            Case TypeCode.Int64
                EmitLoadI8Value(Info, Value, Info.DesiredType)
                Return True
            Case TypeCode.Byte, TypeCode.UInt16
                EmitLoadI4Value(Info, CUInt(Value), Info.DesiredType)
                Return True
            Case TypeCode.UInt32
                If Value <= UInteger.MaxValue AndAlso Value >= UInteger.MinValue Then
                    EmitLoadI4Value(Info, CUInt(Value), Info.DesiredType)
                    Return True
                End If
            Case TypeCode.UInt64
                If Value <= ULong.MaxValue AndAlso Value >= ULong.MinValue Then
                    EmitLoadValue(tmp, CULng(Value))
                    Return True
                End If
            Case TypeCode.Single
                EmitLoadR4Value(tmp, CSng(Value))
                Return True
            Case TypeCode.Double
                EmitLoadR8Value(tmp, CDbl(Value))
                Return True
            Case TypeCode.Decimal
                EmitLoadDecimalValue(tmp, CDec(Value))
                Return True
            Case TypeCode.Object
                EmitLoadI4Value(tmp, Value)
                EmitBox(Info, Info.Compiler.TypeCache.System_Int32)
                Return True
        End Select
        Info.Compiler.Report.ShowMessage(Messages.VBNC99997, Info.Location)
    End Function

    Overloads Shared Function EmitLoadValue(ByVal Info As EmitInfo, ByVal Value As Long) As Boolean
        Helper.Assert(Info.DesiredType IsNot Nothing)
        Dim DesiredTypeCode As TypeCode = Helper.GetTypeCode(Info.Compiler, Info.DesiredType)
        If Value <= Integer.MaxValue AndAlso Value >= Integer.MinValue Then
            Return EmitLoadValue(Info, CInt(Value))
        End If

        Dim tmp As EmitInfo = Info.Clone(Info.Context, CType(Nothing, TypeDescriptor))

        Select Case DesiredTypeCode
            Case TypeCode.Single
                EmitLoadValue(tmp, CSng(Value))
                Return True
            Case TypeCode.Double
                EmitLoadValue(tmp, CDbl(Value))
                Return True
            Case TypeCode.Decimal
                EmitLoadValue(tmp, CDec(Value))
                Return True
        End Select
        Return False
    End Function

    Overloads Shared Function EmitLoadValue(ByVal Info As EmitInfo, ByVal Value As ULong) As Boolean
        Helper.Assert(Info.DesiredType IsNot Nothing)

        Dim DesiredTypeCode As TypeCode = Helper.GetTypeCode(Info.Compiler, Info.DesiredType)
        Dim tmp As EmitInfo = Info

        'If Value <= Integer.MaxValue AndAlso Value >= Integer.MinValue Then
        '    Helper.NotImplemented() 'Return EmitLoadValue(Info, CInt(Value))
        'ElseIf Value <= Long.MaxValue AndAlso Value >= Long.MinValue Then
        '    Return EmitLoadValue(Info, CLng(Value))
        'End If

        Select Case DesiredTypeCode
            Case TypeCode.Single
                EmitLoadValue(tmp, CSng(Value))
                Return True
            Case TypeCode.Double
                EmitLoadValue(tmp, CDbl(Value))
                Return True
            Case TypeCode.Decimal
                EmitLoadValue(tmp, CDec(Value))
                Return True
            Case TypeCode.Int64, TypeCode.UInt64
                EmitLoadI8Value(Info, Value)
                Return True
        End Select
        Info.Compiler.Report.ShowMessage(Messages.VBNC99997, Info.Location)
        Return False
    End Function

    Overloads Shared Function EmitLoadValue(ByVal Info As EmitInfo, ByVal Value As Decimal) As Boolean
        Dim DesiredTypeCode As TypeCode = Helper.GetTypeCode(Info.Compiler, Info.DesiredType)
        Helper.Assert(Info.DesiredType IsNot Nothing)
        If Math.Truncate(Value) = Value Then
            If Value <= Integer.MaxValue AndAlso Value >= Integer.MinValue Then
                Return EmitLoadValue(Info, CInt(Value))
            ElseIf Value <= Long.MaxValue AndAlso Value >= Long.MinValue Then
                Return EmitLoadValue(Info, CLng(Value))
            ElseIf Value <= ULong.MaxValue AndAlso Value >= Long.MinValue Then
                Return EmitLoadValue(Info, CULng(Value))
            End If
        End If

        Dim tmp As EmitInfo = Info.Clone(Info.Context, CType(Nothing, TypeDescriptor))

        Select Case DesiredTypeCode
            Case TypeCode.Single
                EmitLoadValue(tmp, CSng(Value))
                Return True
            Case TypeCode.Double
                EmitLoadValue(tmp, CDbl(Value))
                Return True
        End Select
        Return False
    End Function

    Overloads Shared Function EmitLoadValue(ByVal Info As EmitInfo, ByVal Value As Double) As Boolean
        Helper.Assert(Info.DesiredType IsNot Nothing)
        Dim DesiredTypeCode As TypeCode = Helper.GetTypeCode(Info.Compiler, Info.DesiredType)
        Dim tmp As EmitInfo = Info

        Select Case DesiredTypeCode
            Case TypeCode.Single
                If Value <= Single.MaxValue AndAlso Value >= Single.MinValue Then
                    EmitLoadValue(tmp, CSng(Value))
                    Return True
                End If
        End Select
        Return False
    End Function

    '''' <summary>
    '''' Loads the integer onto the stack.
    '''' </summary>
    '''' <param name="Info"></param>
    '''' <param name="Value"></param>
    '''' <remarks></remarks>
    'Shared Sub EmitLoadValue(ByVal Info As EmitInfo, ByVal Value As Integer)
    '    EmitLoadI4Value(Info, Value)
    '    Info.Stack.Push(Info.Compiler.TypeCache.Integer)
    'End Sub

    ''' <summary>
    ''' Loads the integer onto the stack.
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <param name="Value"></param>
    ''' <remarks></remarks>
    Overloads Shared Sub EmitLoadValue(ByVal Info As EmitInfo, ByVal Value As String)
        Info.ILGen.Emit(OpCodes.Ldstr, Value)
        Info.Stack.Push(Info.Compiler.TypeCache.System_String)
    End Sub

    ''' <summary>
    ''' Loads the boolean value onto the stack.
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <param name="Value"></param>
    ''' <remarks></remarks>
    Overloads Shared Sub EmitLoadValue(ByVal Info As EmitInfo, ByVal Value As Boolean)
        If Value Then
            EmitLoadI4Value(Info, 1, Info.Compiler.TypeCache.System_Boolean)
        Else
            EmitLoadI4Value(Info, 0, Info.Compiler.TypeCache.System_Boolean)
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <param name="Value"></param>
    ''' <remarks></remarks>
    Shared Sub EmitLoadValueAddress(ByVal Info As EmitInfo, ByVal Value As Object)
        Helper.Assert(Info.DesiredType.IsByRef)
        EmitLoadValue(Info.Clone(Info.Context, Info.DesiredType.GetElementType), Value)
        Dim local As LocalBuilder = Info.ILGen.DeclareLocal(Helper.GetTypeOrTypeBuilder(Info.DesiredType.GetElementType))
        EmitStoreVariable(Info, local)
        EmitLoadVariableLocation(Info, local)
    End Sub

    Shared Sub EmitLoadValueConstantOrValueAddress(ByVal Info As EmitInfo, ByVal Value As Object)
        Helper.Assert(Info.DesiredType IsNot Nothing, "EmitInfo.DesiredType must be set!")
        If Info.DesiredType.IsByRef Then
            EmitLoadValueAddress(Info, Value)
        Else
            EmitLoadValue(Info, Value)
        End If
    End Sub

    ''' <summary>
    ''' Loads a constant value onto the stack.
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <param name="Value">The value to emit. Set to DBNull.Value to emit a nothing value.</param>
    ''' <remarks></remarks>
    Overloads Shared Sub EmitLoadValue(ByVal Info As EmitInfo, ByVal Value As Object)
        Helper.Assert(Info.IsRHS, "Not RHS")
        Helper.Assert(Info.DesiredType IsNot Nothing, "No desired type")

        If Value Is Nothing Then Value = DBNull.Value

        Dim ActualType As Type = Value.GetType
        Dim ActualTypeCode As TypeCode = Helper.GetTypeCode(Info.Compiler, ActualType)
        Dim DesiredType As Type = Info.DesiredType
        Dim DesiredTypeCode As TypeCode = Helper.GetTypeCode(info.Compiler, DesiredType)

#If EXTENDEDDEBUG Then
        Info.Compiler.Report.WriteLine(String.Format("Emitter.EmitLoadValue (EmitInfo, Object): ActualType={0}, DesiredType={1}, Value={2}", ActualTypeCode, DesiredTypeCode, Value))
#End If

        Select Case ActualTypeCode
            Case TypeCode.DBNull
                EmitLoadNull(Info)
                Return
            Case TypeCode.SByte, TypeCode.Int16, TypeCode.Int32
                EmitLoadValue(Info, CInt(Value))
                Return
            Case TypeCode.Int64
                Helper.Assert(Helper.CompareType(Info.Compiler.TypeCache.System_Int64, DesiredType), "Expected " & ActualTypeCode.ToString() & ", got " & DesiredType.Name)
                EmitLoadI8Value(Info, CLng(Value))
                Return
            Case TypeCode.Single
                Helper.Assert(Helper.CompareType(Info.Compiler.TypeCache.System_Single, DesiredType), "Expected " & ActualTypeCode.ToString() & ", got " & DesiredType.Name)
                Emitter.EmitLoadR4Value(Info, CSng(Value))
                Return
            Case TypeCode.Double
                Helper.Assert(Helper.CompareType(Info.Compiler.TypeCache.System_Double, DesiredType), "Expected " & ActualTypeCode.ToString() & ", got " & DesiredType.Name)
                Info.ILGen.Emit(OpCodes.Ldc_R8, CDbl(Value))
                Info.Stack.Push(Info.Compiler.TypeCache.System_Double)
                Return
            Case TypeCode.String
                Helper.Assert(Helper.CompareType(Info.Compiler.TypeCache.System_String, DesiredType) OrElse Helper.CompareType(Info.Compiler.TypeCache.System_Object, DesiredType), "Expected " & ActualTypeCode.ToString() & ", got " & DesiredType.Name)
                Info.ILGen.Emit(OpCodes.Ldstr, CStr(Value))
                Info.Stack.Push(Info.Compiler.TypeCache.System_String)
                Return
            Case TypeCode.Byte
                'Helper.Assert(Helper.CompareType(Info.Compiler.TypeCache.System_Byte, DesiredType), "Expected " & ActualTypeCode.ToString() & ", got " & DesiredType.Name)
                EmitLoadI4Value(Info, CInt(Value), Info.Compiler.TypeCache.System_Byte)
                Return
            Case TypeCode.UInt16
                Helper.Assert(Helper.CompareType(Info.Compiler.TypeCache.System_UInt16, DesiredType), "Expected " & ActualTypeCode.ToString() & ", got " & DesiredType.Name)
                EmitLoadI4Value(Info, CInt(Value), Info.Compiler.TypeCache.System_UInt16)
                Return
            Case TypeCode.UInt32
                Helper.Assert(Helper.CompareType(Info.Compiler.TypeCache.System_UInt32, DesiredType), "Expected " & ActualTypeCode.ToString() & ", got " & DesiredType.Name)
                EmitLoadI4Value(Info, CUInt(Value))
                Return
            Case TypeCode.UInt64
                Helper.Assert(Helper.CompareType(Info.Compiler.TypeCache.System_UInt64, DesiredType), "Expected " & ActualTypeCode.ToString() & ", got " & DesiredType.Name)
                EmitLoadI8Value(Info, CULng(Value))
                Return
            Case TypeCode.Decimal
                Helper.Assert(Helper.CompareType(Info.Compiler.TypeCache.System_Decimal, DesiredType), "Expected " & ActualTypeCode.ToString() & ", got " & DesiredType.Name)
                EmitLoadDecimalValue(Info, CDec(Value))
                Return
            Case TypeCode.DateTime
                Helper.Assert(Helper.CompareType(Info.Compiler.TypeCache.System_DateTime, DesiredType), "Expected " & ActualTypeCode.ToString() & ", got " & DesiredType.Name)
                EmitLoadDateValue(Info, CDate(Value))
                Return
            Case TypeCode.Char
                Helper.Assert(Helper.CompareType(Info.Compiler.TypeCache.System_Char, DesiredType) OrElse Helper.CompareType(Info.Compiler.TypeCache.System_String, DesiredType), "Expected " & ActualTypeCode.ToString() & ", got " & DesiredType.Name)
                EmitLoadI4Value(Info, Microsoft.VisualBasic.AscW(CChar(Value)), Info.Compiler.TypeCache.System_Char)
                Return
            Case TypeCode.Boolean
                'Helper.Assert(Helper.CompareType(Info.Compiler.TypeCache.Boolean, DesiredType))
                If CBool(Value) Then
                    EmitLoadI4Value(Info, 1, Info.Compiler.TypeCache.System_Boolean)
                Else
                    EmitLoadI4Value(Info, 0, Info.Compiler.TypeCache.System_Boolean)
                End If
                Return
            Case Else
                Info.Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "Missed case: " & Helper.GetTypeCode(Info.Compiler, Value.GetType).ToString)
                Helper.Stop()
        End Select

        Helper.Stop()
    End Sub

    ''' <summary>
    ''' Loads a nothing constant expression according to the desired type.
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <remarks></remarks>
    Shared Sub EmitLoadNull(ByVal Info As EmitInfo)
        Helper.Assert(Info.DesiredType IsNot Nothing)
        If TypeOf Info.DesiredType Is GenericTypeParameterBuilder OrElse TypeOf Info.DesiredType Is TypeParameterDescriptor Then
            Dim local As LocalBuilder
            local = DeclareLocal(Info, Info.DesiredType)
            EmitLoadVariableLocation(Info, local)
            EmitInitObj(Info, Info.DesiredType)
            EmitLoadVariable(Info, local)
            'Info.Stack.Push(Info.DesiredType)
            FreeLocal(local)
        ElseIf Info.DesiredType.IsByRef Then
            Dim local As LocalBuilder = DeclareLocal(Info, Info.DesiredType.GetElementType)
            Info.ILGen.Emit(OpCodes.Ldnull)
            EmitStoreVariable(Info, local)
            EmitLoadVariableLocation(Info, local)
            Info.Stack.Push(Info.DesiredType)
            FreeLocal(local)
        ElseIf Info.DesiredType.IsClass OrElse Info.DesiredType.IsInterface Then
            Info.ILGen.Emit(OpCodes.Ldnull)
            Info.Stack.Push(Info.DesiredType)
        ElseIf Info.DesiredType.IsValueType Then
            Dim DesiredTypeCode As TypeCode = Helper.GetTypeCode(Info.Compiler, Info.DesiredType)
            Select Case DesiredTypeCode
                Case TypeCode.Boolean
                    EmitLoadI4Value(Info, CInt(False), Info.DesiredType)
                Case TypeCode.SByte, TypeCode.Int16, TypeCode.Int32, TypeCode.Byte, TypeCode.UInt16, TypeCode.UInt32
                    EmitLoadI4Value(Info, 0, Info.DesiredType)
                Case TypeCode.Int64, TypeCode.UInt64
                    EmitLoadI8Value(Info, 0, Info.DesiredType)
                Case TypeCode.Char
                    EmitLoadI4Value(Info, 0, Info.DesiredType)
                Case TypeCode.Single
                    EmitLoadR4Value(Info, CSng(0), Info.DesiredType)
                Case TypeCode.Double
                    EmitLoadR8Value(Info, CDbl(0), Info.DesiredType)
                Case TypeCode.Object, TypeCode.DateTime, TypeCode.Decimal
                    Dim local As LocalBuilder
                    local = DeclareLocal(Info, Info.DesiredType)
                    EmitLoadVariable(Info, local)
                    FreeLocal(local)
                Case TypeCode.String
                    Info.ILGen.Emit(OpCodes.Ldnull)
                    Info.Stack.Push(Info.DesiredType)
                Case Else
                    Info.Compiler.Report.ShowMessage(Messages.VBNC99997, Info.Location)
            End Select
        ElseIf Helper.CompareType(Info.DesiredType, Info.Compiler.TypeCache.System_Enum) Then
            Info.ILGen.Emit(OpCodes.Ldnull)
            Info.Stack.Push(Info.DesiredType)
        Else
            Info.Compiler.Report.ShowMessage(Messages.VBNC99997, Info.Location)
        End If
    End Sub

    ''' <summary>
    ''' Load a constant date value on the evaluation stack.
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <remarks></remarks>
    Shared Sub EmitLoadDateValue(ByVal Info As EmitInfo, ByVal DateValue As Date)
        Dim emitLong As EmitInfo = Info.Clone(Info.Context, Info.Compiler.TypeCache.System_Int64)
        EmitLoadI8Value(emitLong, DateValue.Ticks)
        Info.Stack.Pop(Info.Compiler.TypeCache.System_Int64)
        Info.ILGen.Emit(OpCodes.Newobj, Info.Compiler.TypeCache.System_DateTime__ctor_Int64)
        Info.Stack.Push(Info.Compiler.TypeCache.System_DateTime)
    End Sub

    ''' <summary>
    ''' Load a constant decimal value on to the evaluation stack.
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <param name="decimalValue"></param>
    ''' <remarks></remarks>
    Shared Sub EmitLoadDecimalValue(ByVal Info As EmitInfo, ByVal decimalValue As Decimal)
        EmitLoadDecimalValue(Info, New DecimalFields(decimalValue))
    End Sub

    ''' <summary>
    ''' Load a constant decimal value on to the evaluation stack.
    ''' </summary>
    ''' <param name="decimalValue"></param>
    ''' <remarks></remarks>
    Shared Sub EmitLoadDecimalValue(ByVal Info As EmitInfo, ByVal decimalValue As DecimalFields)
        If decimalValue.Value = -1 Then
            Info.ILGen.Emit(OpCodes.Ldsfld, Info.Compiler.TypeCache.System_Decimal__MinusOne)
            Info.Stack.Push(Info.Compiler.TypeCache.System_Decimal)
        ElseIf decimalValue.Value = 0 Then
            Info.ILGen.Emit(OpCodes.Ldsfld, Info.Compiler.TypeCache.System_Decimal__Zero)
            Info.Stack.Push(Info.Compiler.TypeCache.System_Decimal)
        ElseIf decimalValue.Value = 1 Then
            Info.ILGen.Emit(OpCodes.Ldsfld, Info.Compiler.TypeCache.System_Decimal__One)
            Info.Stack.Push(Info.Compiler.TypeCache.System_Decimal)
        Else
            EmitLoadI4Value(Info, decimalValue.Lo)
            EmitLoadI4Value(Info, decimalValue.Mid)
            EmitLoadI4Value(Info, decimalValue.Hi)
            EmitLoadI4Value(Info, CInt(decimalValue.SignAsBit), Info.Compiler.TypeCache.System_Boolean)
            EmitLoadI4Value(Info, CInt(decimalValue.Scale), Info.Compiler.TypeCache.System_Byte)
            Emitter.EmitNew(Info, Info.Compiler.TypeCache.System_Decimal__ctor_Int32_Int32_Int32_Boolean_Byte)
        End If
    End Sub

    Shared Sub EmitLoadVariableLocation(ByVal Info As EmitInfo, ByVal Variable As VariableClassification)
        If Variable.LocalBuilder IsNot Nothing Then
            Info.ILGen.Emit(OpCodes.Ldloca, Variable.LocalBuilder)
            Info.Stack.Push(Info.Compiler.TypeManager.MakeByRefType(CType(Info.Method, ParsedObject), Variable.LocalBuilder.LocalType))
        ElseIf Variable.FieldInfo IsNot Nothing Then
            Dim emittableField As FieldInfo = Helper.GetFieldOrFieldBuilder(Variable.FieldInfo)
            If Variable.InstanceExpression IsNot Nothing Then
                Dim result As Boolean
                result = Variable.InstanceExpression.GenerateCode(Info)
                Helper.Assert(result)
                Helper.Assert(Variable.FieldInfo.IsStatic = False)
                Info.Stack.Pop(Variable.FieldInfo.DeclaringType)
                Info.ILGen.Emit(OpCodes.Ldflda, emittableField)
            Else
                Helper.Assert(Variable.FieldInfo.IsStatic)
                Info.ILGen.Emit(OpCodes.Ldsflda, emittableField)
            End If
            Info.Stack.Push(Info.Compiler.TypeManager.MakeByRefType(CType(Info.Method, ParsedObject), Variable.FieldInfo.FieldType))
        ElseIf Variable.ParameterInfo IsNot Nothing Then
            Helper.Assert(Variable.InstanceExpression Is Nothing)
            EmitLoadParameterAddress(Info, Variable.ParameterInfo)
        Else
            Info.Compiler.Report.ShowMessage(Messages.VBNC99997, Info.Location)
        End If
    End Sub
    Shared Sub EmitLoadVariable(ByVal Info As EmitInfo, ByVal Variable As VariableClassification)
        If Variable.LocalBuilder IsNot Nothing Then
            EmitLoadVariable(Info, Variable.LocalBuilder)
        ElseIf Variable.FieldInfo IsNot Nothing Then
            EmitLoadVariable(Info, Variable.FieldInfo)
        ElseIf Variable.ParameterInfo IsNot Nothing Then
            EmitLoadVariable(Info, Variable.ParameterInfo)
        Else
            Info.Compiler.Report.ShowMessage(Messages.VBNC99997, Info.Location)
        End If
    End Sub
    Shared Sub EmitLoadVariableLocation(ByVal Info As EmitInfo, ByVal Variable As LocalBuilder)
        Info.ILGen.Emit(OpCodes.Ldloca, Variable)
        Info.Stack.Push(Info.Compiler.TypeManager.MakeByRefType(CType(Info.Method, ParsedObject), Variable.LocalType))
    End Sub

    Shared Sub EmitLoadVariableLocation(ByVal Info As EmitInfo, ByVal Field As FieldInfo)
        Dim emittableField As FieldInfo
        emittableField = Helper.GetFieldOrFieldBuilder(Field)

        If Field.IsLiteral Then
            EmitLoadValueAddress(Info, Field.GetValue(Nothing))
        Else
            If Field.IsStatic Then
                Info.ILGen.Emit(OpCodes.Ldsflda, emittableField)
            Else
                Info.ILGen.Emit(OpCodes.Ldflda, emittableField)
                Info.Stack.Pop(Field.DeclaringType)
            End If
            Info.Stack.Push(Info.Compiler.TypeManager.MakeByRefType(CType(Info.Method, ParsedObject), Field.FieldType))
        End If
    End Sub

    ''' <summary>
    ''' Loads the address of the parameter.
    ''' Just loads the value if it is a byref parameter.
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <param name="Variable"></param>
    ''' <remarks></remarks>
    Shared Sub EmitLoadVariableLocation(ByVal Info As EmitInfo, ByVal Variable As ParameterInfo)
        If Variable.ParameterType.IsByRef Then
            EmitLoadParameter(Info, Variable)
        Else
            Info.ILGen.Emit(OpCodes.Ldarga, GetParameterPosition(Info, Variable))
        End If
    End Sub

    ''' <summary>
    ''' Calculates the actual parameter position of the parameter.
    ''' Ready to send to ILGenerator.Emit(...)
    ''' </summary>
    ''' <param name="Parameter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function GetParameterPosition(ByVal Info As EmitInfo, ByVal Parameter As ParameterInfo) As Integer
        Dim position As Integer = Parameter.Position - 1
        Dim member As MemberInfo = Parameter.Member
        Dim methodmember As MethodInfo = TryCast(member, MethodInfo)
        Dim constructormember As ConstructorInfo = TryCast(member, ConstructorInfo)
        If methodmember IsNot Nothing Then
            If methodmember.IsStatic = False Then
                position += 1
            End If
        ElseIf constructormember IsNot Nothing Then
            If constructormember.IsStatic = False Then
                position += 1
            End If
        Else
            Info.Compiler.Report.ShowMessage(Messages.VBNC99997, Info.Location)
            'do nothing
        End If
        Return position
    End Function

    ''' <summary>
    ''' Loads the value of the specified parameter.
    ''' If it is a byref parameter, the passed-in address is loaded.
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <param name="Parameter"></param>
    ''' <remarks></remarks>
    Shared Sub EmitLoadParameter(ByVal Info As EmitInfo, ByVal Parameter As ParameterInfo)
        Dim position As Integer = GetParameterPosition(Info, Parameter)
        Select Case position
            Case 0
                Info.ILGen.Emit(OpCodes.Ldarg_0)
            Case 1
                Info.ILGen.Emit(OpCodes.Ldarg_1)
            Case 2
                Info.ILGen.Emit(OpCodes.Ldarg_2)
            Case 3
                Info.ILGen.Emit(OpCodes.Ldarg_3)
            Case Is <= 255
                Info.ILGen.Emit(OpCodes.Ldarg_S, CByte(position))
            Case Else
                Info.ILGen.Emit(OpCodes.Ldarg, position)
        End Select
        Info.Stack.Push(Parameter.ParameterType)
    End Sub

    ''' <summary>
    ''' Loads the value of the specified parameter.
    ''' If it is a byref parameter, the passed-in address is loaded.
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <param name="Parameter"></param>
    ''' <remarks></remarks>
    Shared Sub EmitLoadParameterAddress(ByVal Info As EmitInfo, ByVal Parameter As ParameterInfo)
        Dim position As Integer = GetParameterPosition(Info, Parameter)
        If position <= 255 Then
            Info.ILGen.Emit(OpCodes.Ldarga_S, CByte(position))
        Else
            Info.ILGen.Emit(OpCodes.Ldarga, position)
        End If
        Info.Stack.Push(Parameter.ParameterType)
    End Sub

    ''' <summary>
    ''' Loads the value of the parameter.
    ''' Gets the value of byref parameters, not the passed-in address.
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <param name="Variable"></param>
    ''' <remarks></remarks>
    Shared Sub EmitLoadVariable(ByVal Info As EmitInfo, ByVal Variable As ParameterInfo)
        EmitLoadParameter(Info, Variable)
        If Variable.ParameterType.IsByRef AndAlso Info.DesiredType.IsByRef = False Then
            EmitLoadIndirect(Info, Variable.ParameterType)
        End If
    End Sub

    ''' <summary>
    ''' Loads the value of the specified address.
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <param name="ByRefType"></param>
    ''' <remarks></remarks>
    Shared Sub EmitLoadIndirect(ByVal Info As EmitInfo, ByVal ByRefType As Type)
        ByRefType = Helper.GetTypeOrTypeBuilder(ByRefType)
        Info.Stack.Pop(ByRefType)
        Dim elementtype As Type = ByRefType.GetElementType
        Select Case Helper.GetTypeCode(Info.Compiler, elementtype)
            Case TypeCode.Byte
                Info.ILGen.Emit(OpCodes.Ldind_U1)
            Case TypeCode.SByte
                Info.ILGen.Emit(OpCodes.Ldind_I1)
            Case TypeCode.UInt16, TypeCode.Char
                Info.ILGen.Emit(OpCodes.Ldind_U2)
            Case TypeCode.Int16
                Info.ILGen.Emit(OpCodes.Ldind_I2)
            Case TypeCode.UInt32
                Info.ILGen.Emit(OpCodes.Ldind_U4)
            Case TypeCode.Int32
                Info.ILGen.Emit(OpCodes.Ldind_I4)
            Case TypeCode.UInt64
                Info.Compiler.Report.ShowMessage(Messages.VBNC99997, Info.Location)
                Info.ILGen.Emit(OpCodes.Ldind_I8)
            Case TypeCode.Int64
                Info.ILGen.Emit(OpCodes.Ldind_I8)
            Case TypeCode.Single
                Info.ILGen.Emit(OpCodes.Ldind_R4)
            Case TypeCode.Double
                Info.ILGen.Emit(OpCodes.Ldind_R8)
            Case TypeCode.Object, TypeCode.String
                Info.ILGen.Emit(OpCodes.Ldind_Ref)
            Case TypeCode.Boolean
                Info.ILGen.Emit(OpCodes.Ldind_I1)
            Case TypeCode.Decimal
                Info.ILGen.Emit(OpCodes.Ldobj, Info.Compiler.TypeCache.System_Decimal)
            Case Else
                Info.Compiler.Report.ShowMessage(Messages.VBNC99997, Info.Location)
                'Helper.NotImplemented("EmitLoadIndirect of type: " & elementtype.FullName)
        End Select
        Info.Stack.Push(elementtype)
    End Sub

    ''' <summary>
    ''' Returns true if a leave instruction is necessary to jump from one statement to the other.
    ''' If ToStatement is nothing then it is assumed a ret instruction is about to be emitted.
    ''' </summary>
    ''' <param name="FromStatement"></param>
    ''' <param name="ToStatement"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function IsLeaveNecessary(ByVal FromStatement As Statement, ByVal ToStatement As Statement) As Boolean
        Dim result As Boolean

        If ToStatement Is Nothing Then
            result = FromStatement.FindParentCodeBlock.UpmostBlock.HasUnstructuredExceptionHandling
        End If

        If result = False Then
            Dim parentStmt As Statement = FromStatement
            Do Until parentStmt Is ToStatement OrElse parentStmt Is Nothing
                result = result OrElse TypeOf parentStmt Is TryStatement
                result = result OrElse TypeOf parentStmt Is SyncLockStatement
                result = result OrElse TypeOf parentStmt Is ForEachStatement
                result = result OrElse TypeOf parentStmt Is UsingStatement
                result = result OrElse TypeOf parentStmt Is CatchStatement

                If result Then Exit Do

                parentStmt = parentStmt.Parent.FindFirstParent(Of Statement)()
            Loop
        End If
        Return result
    End Function

    Shared Sub EmitRetOrLeave(ByVal Info As EmitInfo, ByVal CurrentStatement As Statement, ByVal HasReturnValue As Boolean)
        Dim emitLeave As Boolean

        emitLeave = IsLeaveNecessary(CurrentStatement, Nothing)

        If emitLeave Then
            If HasReturnValue Then
                Emitter.EmitStoreVariable(Info, Info.Method.DefaultReturnVariable)
            End If
            Emitter.EmitLeave(Info, CurrentStatement.FindParentCodeBlock.UpmostBlock.EndOfMethodLabel)
        Else
            Emitter.EmitRet(Info)
        End If
    End Sub

    Shared Sub EmitRet(ByVal Info As EmitInfo)
        Info.ILGen.Emit(OpCodes.Ret)
        If Info.Method.DefaultReturnVariable IsNot Nothing Then
            Info.Stack.Pop(Info.Method.DefaultReturnVariable.LocalType)
        End If
    End Sub

    Shared Sub EmitLoadVariable(ByVal Info As EmitInfo, ByVal Variable As LocalBuilder)
        Info.ILGen.Emit(OpCodes.Ldloc, Variable)
        Info.Stack.Push(Variable.LocalType)
    End Sub

    Shared Sub EmitNop(ByVal Info As EmitInfo)
        Info.ILGen.Emit(OpCodes.Nop)
    End Sub

    Shared Sub EmitLoadVariable(ByVal Info As EmitInfo, ByVal Variable As FieldInfo)
        Dim Field As FieldInfo

        Field = Helper.GetFieldOrFieldBuilder(Variable)
        If Variable.IsStatic Then
            If Variable.IsLiteral Then
                Emitter.EmitLoadValue(Info.Clone(Info.Context, True, False, Variable.FieldType), Variable.GetValue(Nothing))
            Else
                Info.ILGen.Emit(OpCodes.Ldsfld, Field)
                Info.Stack.Push(Variable.FieldType)
            End If
        Else
            Info.ILGen.Emit(OpCodes.Ldfld, Field)
            Info.Stack.Pop(Variable.DeclaringType)
            Info.Stack.Push(Variable.FieldType)
        End If
    End Sub

    Shared Sub EmitStoreVariable(ByVal Info As EmitInfo, ByVal Variable As LocalBuilder)
        Info.ILGen.Emit(OpCodes.Stloc, Variable)
        Info.Stack.Pop(Variable.LocalType)
    End Sub

    Shared Sub EmitStoreVariable(ByVal Info As EmitInfo, ByVal Variable As VariableClassification)
        If Variable.LocalBuilder IsNot Nothing Then
            EmitStoreVariable(Info, Variable.LocalBuilder)
        ElseIf Variable.FieldInfo IsNot Nothing Then
            EmitStoreField(Info, Variable.FieldInfo)
        ElseIf Variable.ParameterInfo IsNot Nothing Then
            EmitStoreVariable(Info, Variable.ParameterInfo)
        Else
            Info.Compiler.Report.ShowMessage(Messages.VBNC99997, Info.Location)
        End If
    End Sub

    Shared Sub EmitStoreIndirect(ByVal Info As EmitInfo, ByVal ByRefType As Type)
        ByRefType = Helper.GetTypeOrTypeBuilder(ByRefType)
        Dim elementtype As Type = ByRefType.GetElementType
        Info.Stack.Pop(elementtype)
        Info.Stack.Pop(ByRefType)
        Select Case Helper.GetTypeCode(Info.Compiler, elementtype)
            Case TypeCode.SByte, TypeCode.Byte, TypeCode.Boolean
                Info.ILGen.Emit(OpCodes.Stind_I1)
            Case TypeCode.Int16, TypeCode.UInt16, TypeCode.Char
                Info.ILGen.Emit(OpCodes.Stind_I2)
            Case TypeCode.Int32, TypeCode.UInt32
                Info.ILGen.Emit(OpCodes.Stind_I4)
            Case TypeCode.Int64, TypeCode.UInt64
                Info.ILGen.Emit(OpCodes.Stind_I8)
            Case TypeCode.Single
                Info.ILGen.Emit(OpCodes.Stind_R4)
            Case TypeCode.Double
                Info.ILGen.Emit(OpCodes.Stind_R8)
            Case TypeCode.String
                Info.ILGen.Emit(OpCodes.Stind_Ref)
            Case TypeCode.DateTime, TypeCode.Decimal
                Info.ILGen.Emit(OpCodes.Stobj, elementtype)
            Case TypeCode.Object
                If elementtype.IsValueType Then
                    Info.ILGen.Emit(OpCodes.Stobj, elementtype)
                Else
                    Info.ILGen.Emit(OpCodes.Stind_Ref)
                End If
            Case Else
                Info.Compiler.Report.ShowMessage(Messages.VBNC99997, Info.Location)
        End Select
    End Sub

    Shared Sub EmitStoreVariable(ByVal Info As EmitInfo, ByVal Variable As ParameterInfo)
        Dim position As Integer = GetParameterPosition(Info, Variable)
        If Variable.ParameterType.IsByRef Then
            If Variable.ParameterType.GetElementType.IsGenericParameter Then
                EmitStoreObject(Info, Variable.ParameterType.GetElementType)
            Else
                EmitStoreIndirect(Info, Variable.ParameterType)
            End If
        Else
            Info.ILGen.Emit(OpCodes.Starg, position)
            Info.Stack.Pop(Variable.ParameterType)
        End If
    End Sub

    Shared Sub EmitSwitch(ByVal Info As EmitInfo, ByVal Labels() As Label)
        Info.ILGen.Emit(OpCodes.Switch, Labels)
        Info.Stack.Pop(Info.Compiler.TypeCache.System_Int32)
    End Sub

    Shared Sub EmitStoreField(ByVal Info As EmitInfo, ByVal Field As FieldInfo)
        Dim Field2 As FieldInfo
        Field2 = Helper.GetFieldOrFieldBuilder(Field)
        If Field.IsStatic Then
            Info.Stack.Pop(Field.FieldType)
            Info.ILGen.Emit(OpCodes.Stsfld, Field2)
        Else
            Info.Stack.Pop(Field.FieldType)
            Info.Stack.Pop(Field.DeclaringType)
            Info.ILGen.Emit(OpCodes.Stfld, Field2)
        End If
    End Sub

    'Shared Sub EmitBox(ByVal Info As EmitInfo)
    '    Dim tp As Type = Info.Stack.Peek
    '    Helper.Assert(tp.IsValueType OrElse tp.IsGenericParameter)
    '    If tp.IsValueType OrElse tp.IsGenericParameter Then
    '        EmitBox(Info, tp)
    '    End If
    'End Sub


    ''' <summary>
    ''' Emits a box instruction, no checks are done.
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <remarks></remarks>
    Shared Sub EmitBox(ByVal Info As EmitInfo, ByVal SourceType As Type)
        Dim OriginalDestinationType As Type = SourceType
        SourceType = Helper.GetTypeOrTypeBuilder(SourceType)
        Info.ILGen.Emit(OpCodes.Box, SourceType)
        Info.Stack.SwitchHead(Info.Stack.Peek, Info.Compiler.TypeCache.System_Object) 'Push a object reference on the stack.
    End Sub

    Shared Sub EmitUnbox(ByVal Info As EmitInfo, ByVal ToType As Type)
        ToType = Helper.GetTypeOrTypeBuilder(ToType)
        Info.Stack.Pop(Info.Stack.Peek)
        Info.ILGen.Emit(OpCodes.Unbox, ToType)
        Info.Stack.Push(Info.Compiler.TypeManager.MakeByRefType(CType(Info.Method, ParsedObject), ToType))
    End Sub

    Shared Sub EmitUnbox_Any(ByVal Info As EmitInfo, ByVal ToType As Type)
        ToType = Helper.GetTypeOrTypeBuilder(ToType)
        Info.Stack.Pop(Info.Stack.Peek)
        Info.ILGen.Emit(OpCodes.Unbox_Any, ToType)
        Info.Stack.Push(ToType)
    End Sub

    Shared Sub EmitLdobj(ByVal Info As EmitInfo, ByVal SourceType As Type)
        SourceType = Helper.GetTypeOrTypeBuilder(SourceType)
        Info.Stack.Pop(Info.Compiler.TypeManager.MakeByRefType(CType(Info.Method, ParsedObject), SourceType))
        Info.ILGen.Emit(OpCodes.Ldobj, SourceType)
        Info.Stack.Push(SourceType)
    End Sub

    Shared Sub EmitThrow(ByVal Info As EmitInfo)
        Info.ILGen.Emit(OpCodes.Throw)
        Info.Stack.Pop(Info.Compiler.TypeCache.System_Exception)
    End Sub

    Shared Function SwitchVersionedMethods(ByVal Info As EmitInfo, ByVal UnversionedMethod As MethodInfo) As MethodInfo
        If Info.Compiler.CommandLine.VBVersion <> CommandLine.VBVersions.V8 Then Return UnversionedMethod

        If UnversionedMethod Is Info.Compiler.TypeCache.MS_VB_Information__IsNumeric Then Return Info.Compiler.TypeCache.MS_VB_CS_Versioned__IsNumeric
        If UnversionedMethod Is Info.Compiler.TypeCache.MS_VB_Information__SystemTypeName Then Return Info.Compiler.TypeCache.MS_VB_CS_Versioned__SystemTypeName
        If UnversionedMethod Is Info.Compiler.TypeCache.MS_VB_Information__VbTypeName Then Return Info.Compiler.TypeCache.MS_VB_CS_Versioned__VbTypeName
        If UnversionedMethod Is Info.Compiler.TypeCache.MS_VB_Information__TypeName Then Return Info.Compiler.TypeCache.MS_VB_CS_Versioned__TypeName
        If UnversionedMethod Is Info.Compiler.TypeCache.MS_VB_Interaction__CallByName Then Return Info.Compiler.TypeCache.MS_VB_CS_Versioned__CallByName

#If ENABLECECIL Then
        'Check if the object comparison above is true for cecil as well (that is if same method may have multiple object references)
        'This will assert while compiling the compiler if object comparison doesn't hold for cecil.
        Helper.Assert(UnversionedMethod.Name <> "IsNumeric")
#End If

        Return UnversionedMethod
    End Function
End Class
