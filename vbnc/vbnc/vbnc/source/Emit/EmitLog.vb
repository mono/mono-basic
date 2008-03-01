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

#If DEBUG Or ENABLECECIL Then
#Const DEBUGEMIT = 0

Public Structure Label
    'Public SRELabel As System.Reflection.Emit.Label
#If ENABLECECIL Then
    Public CecilLabel As Mono.Cecil.Cil.Instruction
#End If
End Structure

Public Class LocalBuilder
    'Public SRELocal As System.Reflection.Emit.LocalBuilder
#If ENABLECECIL Then
    Public CecilLocal As Mono.Cecil.Cil.VariableDefinition
#End If
    ReadOnly Property LocalType() As Mono.Cecil.TypeReference
        Get
            Return CecilLocal.VariableType
        End Get
    End Property
End Class

Public Class EmitLog
    'Private m_ILGen As ILGenerator
    Private m_Compiler As Compiler

    '#If ENABLECECIL Then
    Private m_CilBody As Mono.Cecil.Cil.MethodBody

    ReadOnly Property CilWorker() As Mono.Cecil.Cil.CilWorker
        Get
            Return m_CilBody.CilWorker
        End Get
    End Property

    Property CilBody() As Mono.Cecil.Cil.MethodBody
        Get
            Return m_CilBody
        End Get
        Set(ByVal value As Mono.Cecil.Cil.MethodBody)
            m_CilBody = value
        End Set
    End Property

    'Private Shared m_Hashed As Generic.Dictionary(Of String, Mono.Cecil.Cil.OpCode)

    'Function ConvertOpCode(ByVal SRE As OpCode) As Mono.Cecil.Cil.OpCode

    '    If m_Hashed Is Nothing Then
    '        m_Hashed = New Generic.Dictionary(Of String, Mono.Cecil.Cil.OpCode)
    '        Dim cecilCodes As FieldInfo() = GetType(Mono.Cecil.Cil.OpCodes).GetFields()
    '        For i As Integer = 0 To cecilCodes.Length - 1
    '            Dim fi As FieldInfo = cecilCodes(i)
    '            If Not fi.IsStatic Then Continue For
    '            If Not fi.IsInitOnly Then Continue For
    '            If Not fi.IsPublic Then Continue For
    '            Dim tmp As Object
    '            tmp = fi.GetValue(Nothing)
    '            If TypeOf tmp Is Mono.Cecil.Cil.OpCode = False Then Continue For
    '            Dim value As Mono.Cecil.Cil.OpCode
    '            value = DirectCast(tmp, Mono.Cecil.Cil.OpCode)
    '            m_Hashed.Add(value.Name, value)
    '        Next
    '    End If

    '    If m_Hashed.ContainsKey(SRE.Name) = False Then
    '        If SRE.Name = "stelem" Then Return Mono.Cecil.Cil.OpCodes.Stelem_Any
    '        Stop
    '    End If

    '    Return m_Hashed(SRE.Name)
    'End Function
    '#End If

    ReadOnly Property Compiler() As Compiler
        Get
            Return m_Compiler
        End Get
    End Property

    Sub New(ByVal Compiler As Compiler)
        m_Compiler = Compiler
    End Sub

    Shared ReadOnly Property IsLogging() As Boolean
        Get
            Return True
        End Get
    End Property

    Private Overloads Shared Function ToString(ByVal obj As Object) As String
        If obj Is Nothing Then
            Return "Nothing"
        Else
            Return "Type: " & obj.GetType.ToString & ";Value: " & obj.ToString
        End If
    End Function

    <Diagnostics.Conditional("DEBUGEMIT")> Private Sub Log(ByVal str As String)
#If DEBUGEMIT Then
        m_Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "Emitted: " & str)
#End If
    End Sub

    <Diagnostics.Conditional("DEBUGEMIT")> Private Sub Log(ByVal str As String, ByVal ParamArray args() As String)
#If DEBUGEMIT Then
        Log(String.Format(str, args))
#End If
    End Sub

    '    Public Sub BeginCatchBlock(ByVal exceptionType As System.Type)
    '        Log("BeginCatchBlock")
    '        Helper.Assert(Helper.IsEmittableMember(exceptionType))
    '        m_ILGen.BeginCatchBlock(exceptionType)
    '#If ENABLECECIL Then
    '        BeginCatchBlock(Helper.GetTypeOrTypeReference(Compiler, exceptionType))
    '#End If
    '    End Sub
    Public Sub BeginExceptFilterBlock()
        Log("BeginExceptFilterBlock")
        'm_ILGen.BeginExceptFilterBlock()
#If ENABLECECIL Then
        BeginExceptFilterBlockCecil()
#End If
    End Sub
    Public Function BeginExceptionBlock() As Label
        Log("BeginExceptionBlock")
        Dim result As New Label
        'result.SRELabel = m_ILGen.BeginExceptionBlock
#If ENABLECECIL Then
        result.CecilLabel = BeginExceptionBlockCecil()
#End If
        Return result
    End Function
    Public Sub BeginFaultBlock()
        Log("BeginFaultBlock")
        'm_ILGen.BeginFaultBlock()
#If ENABLECECIL Then
        BeginFaultBlockCecil()
#End If
    End Sub
    Public Sub BeginFinallyBlock()
        Log("BeginFinallyBlock")
        'm_ILGen.BeginFinallyBlock()
#If ENABLECECIL Then
        BeginFinallyBlockCecil()
#End If
    End Sub
    '    Public Sub BeginScope()
    '        Log("BeginScope")
    '        'm_ILGen.BeginScope()
    '#If ENABLECECIL Then
    '        BeginScopeCecil()
    '#End If
    '    End Sub

    '    Public Function DeclareLocal(ByVal localType As System.Type) As LocalBuilder
    '        Dim result As New LocalBuilder

    '        result.SRELocal = DeclareLocalSRE(localType)

    '#If ENABLECECIL Then
    '        result.CecilLocal = DeclareLocal(Helper.GetTypeOrTypeReference(Compiler, localType))
    '#End If

    '        Return result
    '    End Function

    'Private Function DeclareLocalSRE(ByVal localType As System.Type) As System.Reflection.Emit.LocalBuilder
    '    Helper.Assert(Helper.IsEmittableMember(localType))
    '    Log("DeclareLocal({0})", localType.ToString)
    '    Return m_ILGen.DeclareLocal(localType)
    'End Function
    'Public Function DeclareLocal(ByVal localType As System.Type, ByVal pinned As Boolean) As System.Reflection.Emit.LocalBuilder
    '    Helper.Assert(Helper.IsEmittableMember(localType))
    '    Log("DeclareLocal({0}, {1})", localType.ToString, pinned.ToString)
    '    Return m_ILGen.DeclareLocal(localType, pinned)
    'End Function
    'Public Function DefineLabel() As System.Reflection.Emit.Label
    '    Log("DefineLabel")
    '    Return m_ILGen.DefineLabel()
    'End Function
    Public Function DefineLabel() As Label
        Log("DefineLabel")
        Dim result As New Label
        'result.SRELabel = m_ILGen.DefineLabel
#If ENABLECECIL Then
        result.CecilLabel = DefineLabelCecil()
#End If
        Return result
    End Function
    '    Public Sub Emit(ByVal opcode As System.Reflection.Emit.OpCode)
    '        Log("Emit({0})", opcode.ToString)
    '        'm_ILGen.Emit(opcode)
    '#If ENABLECECIL Then
    '        Emit(ConvertOpCode(opcode))
    '#End If
    '    End Sub
    '    Public Sub Emit(ByVal opcode As System.Reflection.Emit.OpCode, ByVal arg As Byte)
    '        Log("Emit({0},{1})", opcode.ToString, ToString(arg))
    '        'm_ILGen.Emit(opcode, arg)
    '#If ENABLECECIL Then
    '        Emit(ConvertOpCode(opcode), arg)
    '#End If
    '    End Sub
    '    Public Sub Emit(ByVal opcode As System.Reflection.Emit.OpCode, ByVal arg As Double)
    '        Log("Emit({0},{1})", opcode.ToString, ToString(arg))
    '        'm_ILGen.Emit(opcode, arg)
    '#If ENABLECECIL Then
    '        Emit(ConvertOpCode(opcode), arg)
    '#End If
    '    End Sub
    '    Public Sub Emit(ByVal opcode As System.Reflection.Emit.OpCode, ByVal arg As Integer)
    '        Log("Emit({0},{1})", opcode.ToString, ToString(arg))
    '        'm_ILGen.Emit(opcode, arg)
    '#If ENABLECECIL Then
    '        Emit(ConvertOpCode(opcode), arg)
    '#End If
    '    End Sub
    '    Public Sub Emit(ByVal opcode As System.Reflection.Emit.OpCode, ByVal arg As Long)
    '        Log("Emit({0},{1})", opcode.ToString, ToString(arg))
    '        'm_ILGen.Emit(opcode, arg)
    '#If ENABLECECIL Then
    '        Emit(ConvertOpCode(opcode), arg)
    '#End If
    '    End Sub
    '    Public Sub Emit(ByVal opcode As System.Reflection.Emit.OpCode, ByVal arg As SByte)
    '        Log("Emit({0},{1})", opcode.ToString, ToString(arg))
    '        'm_ILGen.Emit(opcode, arg)
    '#If ENABLECECIL Then
    '        Emit(ConvertOpCode(opcode), arg)
    '#End If
    '    End Sub
    '    Public Sub Emit(ByVal opcode As System.Reflection.Emit.OpCode, ByVal arg As Short)
    '        Log("Emit({0},{1})", opcode.ToString, ToString(arg))
    '        'm_ILGen.Emit(opcode, arg)
    '#If ENABLECECIL Then
    '        Emit(ConvertOpCode(opcode), arg)
    '#End If
    '    End Sub
    '    Public Sub Emit(ByVal opcode As System.Reflection.Emit.OpCode, ByVal arg As Single)
    '        Log("Emit({0},{1})", opcode.ToString, ToString(arg))
    '        'm_ILGen.Emit(opcode, arg)
    '#If ENABLECECIL Then
    '        Emit(ConvertOpCode(opcode), arg)
    '#End If
    '    End Sub
    '    Public Sub Emit(ByVal opcode As System.Reflection.Emit.OpCode, ByVal str As String)
    '        Log("Emit({0},{1})", opcode.ToString, ToString(str))
    '        'm_ILGen.Emit(opcode, str)
    '#If ENABLECECIL Then
    '        Emit(ConvertOpCode(opcode), str)
    '#End If
    '    End Sub
    '    Public Sub Emit(ByVal opcode As System.Reflection.Emit.OpCode, ByVal con As System.Reflection.ConstructorInfo)
    '        Helper.Assert(Helper.IsEmittableMember(con))
    '        Log("Emit({0},{1})", opcode.ToString, ToString(con))
    '        m_ILGen.Emit(opcode, con)
    '#If ENABLECECIL Then
    '        Emit(ConvertOpCode(opcode), Helper.GetMethodOrMethodReference(Compiler, con))
    '#End If
    '    End Sub
    '    Public Sub Emit(ByVal opcode As System.Reflection.Emit.OpCode, ByVal label As Label)
    '        'Emit(opcode, label.SRELabel)
    '#If ENABLECECIL Then
    '        Emit(ConvertOpCode(opcode), label.CecilLabel)
    '#End If
    '    End Sub
    Public Sub Emit(ByVal opcode As Mono.Cecil.Cil.OpCode, ByVal label As Label)
        Emit(opcode, label.CecilLabel)
    End Sub

    'Private Sub Emit(ByVal opcode As System.Reflection.Emit.OpCode, ByVal label As System.Reflection.Emit.Label)
    '    Log("Emit({0},{1})", opcode.ToString, ToString(label))
    '    m_ILGen.Emit(opcode, label)
    'End Sub
    'Private Sub Emit(ByVal opcode As System.Reflection.Emit.OpCode, ByVal labels() As System.Reflection.Emit.Label)
    '    Log("Emit({0},{1})", opcode.ToString, ToString(labels))
    '    m_ILGen.Emit(opcode, labels)
    'End Sub

    '    Public Sub Emit(ByVal opcode As System.Reflection.Emit.OpCode, ByVal labels() As Label)
    '        Log("Emit({0},{1})", opcode.ToString, ToString(labels))
    '        'Dim tmpSRE As System.Reflection.Emit.Label()
    '        'ReDim tmpSRE(labels.Length - 1)
    '        'For i As Integer = 0 To labels.Length - 1
    '        '    tmpSRE(i) = labels(i).SRELabel
    '        'Next
    '        'm_ILGen.Emit(opcode, tmpSRE)
    '#If ENABLECECIL Then
    '        Dim tmpCecil As Mono.Cecil.Cil.Instruction()
    '        ReDim tmpCecil(labels.Length - 1)
    '        For i As Integer = 0 To labels.Length - 1
    '            tmpCecil(i) = labels(i).CecilLabel
    '        Next
    '        Emit(ConvertOpCode(opcode), tmpCecil)
    '#End If
    '    End Sub

    Public Sub Emit(ByVal opcode As Mono.Cecil.Cil.OpCode, ByVal labels() As Label)
        'Log("Emit({0},{1})", opcode.ToString, ToString(labels))
        'Dim tmpSRE As System.Reflection.Emit.Label()
        'ReDim tmpSRE(labels.Length - 1)
        'For i As Integer = 0 To labels.Length - 1
        '    tmpSRE(i) = labels(i).SRELabel
        'Next
        'm_ILGen.Emit(opcode, tmpSRE)
#If ENABLECECIL Then
        Dim tmpCecil As Mono.Cecil.Cil.Instruction()
        ReDim tmpCecil(labels.Length - 1)
        For i As Integer = 0 To labels.Length - 1
            tmpCecil(i) = labels(i).CecilLabel
        Next
        Emit(opcode, tmpCecil)
#End If
    End Sub

    'Private Sub Emit(ByVal opcode As System.Reflection.Emit.OpCode, ByVal local As System.Reflection.Emit.LocalBuilder)
    '    Log("Emit({0},{1})", opcode.ToString, ToString(local))
    '    m_ILGen.Emit(opcode, local)
    'End Sub

    '    Public Sub Emit(ByVal opcode As System.Reflection.Emit.OpCode, ByVal local As LocalBuilder)
    '        Log("Emit({0},{1})", opcode.ToString, ToString(local))
    '        'Emit(opcode, local.SRELocal)
    '#If ENABLECECIL Then
    '        Emit(ConvertOpCode(opcode), local.CecilLocal)
    '#End If
    '    End Sub

    '    Public Sub Emit(ByVal opcode As System.Reflection.Emit.OpCode, ByVal signature As System.Reflection.Emit.SignatureHelper)
    '        Log("Emit({0},{1})", opcode.ToString, ToString(signature))
    '        'm_ILGen.Emit(opcode, signature)
    '#If ENABLECECIL Then
    '        Throw New NotImplementedException()
    '#End If
    '    End Sub
    '    Public Sub Emit(ByVal opcode As System.Reflection.Emit.OpCode, ByVal field As System.Reflection.FieldInfo)
    '        Helper.Assert(Helper.IsEmittableMember(field))
    '        Log("Emit({0},{1})", opcode.ToString, ToString(field))
    '        m_ILGen.Emit(opcode, field)

    '#If ENABLECECIL Then
    '        Emit(ConvertOpCode(opcode), Helper.getfieldorfieldreference(Compiler, field))
    '#End If
    '    End Sub
    '    Public Sub Emit(ByVal opcode As System.Reflection.Emit.OpCode, ByVal meth As System.Reflection.MethodInfo)
    '        Helper.Assert(Helper.IsEmittableMember(meth))
    '        Log("Emit({0},{1})", opcode.ToString, ToString(meth))
    '        m_ILGen.Emit(opcode, meth)

    '#If ENABLECECIL Then
    '        Emit(ConvertOpCode(opcode), Helper.GetMethodOrMethodReference(Compiler, meth))
    '#End If
    '    End Sub
    '    Public Sub Emit(ByVal opcode As System.Reflection.Emit.OpCode, ByVal cls As System.Type)
    '        'Helper.Assert(Helper.IsEmittableMember(cls))
    '        Log("Emit({0},{1})", opcode.ToString, ToString(cls))
    '        m_ILGen.Emit(opcode, Helper.GetTypeOrTypeBuilder(cls))

    '#If ENABLECECIL Then
    '        Emit(ConvertOpCode(opcode), Helper.GetTypeOrTypeReference(Compiler, cls))
    '#End If
    '    End Sub
    '    Public Sub EmitCall(ByVal opcode As System.Reflection.Emit.OpCode, ByVal methodInfo As System.Reflection.MethodInfo, ByVal optionalParameterTypes() As System.Type)
    '        Helper.Assert(Helper.IsEmittableMember(methodInfo))
    '        Log("EmitCall({0},{1},{2})", opcode.ToString, ToString(methodInfo), ToString(optionalParameterTypes))
    '        m_ILGen.EmitCall(opcode, methodInfo, optionalParameterTypes)
    '#If ENABLECECIL Then
    '        If optionalParameterTypes Is Nothing Then
    '            EmitCall(ConvertOpCode(opcode), Helper.GetMethodOrMethodReference(Compiler, methodInfo), Nothing)
    '        Else
    '            Throw New NotImplementedException("Emitcall(ConvertOpCode(opcode), Helper.GetMethodOrMethodReference(Compiler, methodInfo), ?)")
    '        End If
    '#End If
    '    End Sub
    '    Public Sub EmitCalli(ByVal opcode As System.Reflection.Emit.OpCode, ByVal callingConvention As System.Reflection.CallingConventions, ByVal returnType As System.Type, ByVal parameterTypes() As System.Type, ByVal optionalParameterTypes() As System.Type)
    '        Helper.Assert(Helper.IsEmittableMember(returnType))
    '        Helper.Assert(Helper.IsEmittableMember(parameterTypes))
    '        Helper.Assert(Helper.IsEmittableMember(optionalParameterTypes))
    '        Log("EmitCalli({0},{1},{2},{3},{4})", opcode.ToString, ToString(callingConvention), ToString(returnType), ToString(parameterTypes), ToString(optionalParameterTypes))
    '        m_ILGen.EmitCalli(opcode, callingConvention, returnType, parameterTypes, optionalParameterTypes)
    '#If ENABLECECIL Then
    '        Throw New NotImplementedException()
    '#End If
    '    End Sub
    '    Public Sub EmitCalli(ByVal opcode As System.Reflection.Emit.OpCode, ByVal unmanagedCallConv As System.Runtime.InteropServices.CallingConvention, ByVal returnType As System.Type, ByVal parameterTypes() As System.Type)
    '        Helper.Assert(Helper.IsEmittableMember(returnType))
    '        Helper.Assert(Helper.IsEmittableMember(parameterTypes))
    '        Log("EmitCalli({0},{1},{2},{3})", opcode.ToString, ToString(unmanagedCallConv), ToString(returnType), ToString(parameterTypes))
    '        m_ILGen.EmitCalli(opcode, unmanagedCallConv, returnType, parameterTypes)
    '#If ENABLECECIL Then
    '        Throw New NotImplementedException
    '#End If
    '    End Sub
    '    Public Sub EmitWriteLine(ByVal value As String)
    '        Log("EmitWriteLine({0})", ToString(value))
    '        'm_ILGen.EmitWriteLine(value)
    '#If ENABLECECIL Then
    '        Throw New NotImplementedException
    '#End If
    '    End Sub
    '    Public Sub EmitWriteLine(ByVal localBuilder As System.Reflection.Emit.LocalBuilder)
    '        Log("EmitWriteLine({0})", ToString(localBuilder))
    '        m_ILGen.EmitWriteLine(localBuilder)
    '#If ENABLECECIL Then
    '        Throw New NotImplementedException
    '#End If
    '    End Sub
    '    Public Sub EmitWriteLine(ByVal fld As System.Reflection.FieldInfo)
    '        Helper.Assert(Helper.IsEmittableMember(fld))
    '        Log("EmitWriteLine({0})", ToString(fld))
    '        m_ILGen.EmitWriteLine(fld)
    '#If ENABLECECIL Then
    '        Throw New NotImplementedException
    '#End If
    '    End Sub
    Public Sub EndExceptionBlock()
        Log("EndExceptionBlock")
        'm_ILGen.EndExceptionBlock()
#If ENABLECECIL Then
        EndExceptionBlockCecil()
#End If
    End Sub
    '    Public Sub EndScope()
    '        Log("EndScope")
    '        'm_ILGen.EndScope()
    '#If ENABLECECIL Then
    '        EndScopeCecil()
    '#End If
    '    End Sub
    'Private Sub MarkLabel(ByVal loc As System.Reflection.Emit.Label)
    '    Log("MarkLabel({0}", ToString(loc))
    '    'm_ILGen.MarkLabel(loc)
    'End Sub

    Public Sub MarkLabel(ByVal loc As Label)
        'MarkLabel(loc.SRELabel)
#If ENABLECECIL Then
        MarkLabel(loc.CecilLabel)
#End If
    End Sub
    Public Sub MarkSequencePoint(ByVal document As System.Diagnostics.SymbolStore.ISymbolDocumentWriter, ByVal startLine As Integer, ByVal startColumn As Integer, ByVal endLine As Integer, ByVal endColumn As Integer)
        Log("MarkSequencePoint({0},{1},{2},{3},{4}", CObj(document).ToString, startLine.ToString, startColumn.ToString, endLine.ToString, endColumn.ToString)
        'm_ILGen.MarkSequencePoint(document, startLine, startColumn, endLine, endColumn)
#If ENABLECECIL Then
        '        Throw New NotImplementedException
#End If
    End Sub
    '    Public Sub ThrowException(ByVal excType As System.Type)
    '        Helper.Assert(Helper.IsEmittableMember(excType))
    '        Log("ThrowException({0})", ToString(excType))
    '        m_ILGen.ThrowException(excType)
    '#If ENABLECECIL Then
    '        ThrowException(Helper.GetTypeOrTypeReference(Compiler, excType))
    '#End If
    '    End Sub
    '    Public Sub UsingNamespace(ByVal usingNamespace As String)
    '        Log("UsingNamespace({0})", usingNamespace)
    '        'm_ILGen.UsingNamespace(usingNamespace)
    '#If ENABLECECIL Then
    '        Throw New NotImplementedException
    '#End If
    '    End Sub

#If ENABLECECIL Then
    Private Class TryBlock
        Public Start As Integer
        Public EndBlock As Mono.Cecil.Cil.Instruction
        Public EndTry As Mono.Cecil.Cil.Instruction
        Public Handlers As New Generic.List(Of Mono.Cecil.Cil.ExceptionHandler)

        ReadOnly Property CurrentHandler() As Mono.Cecil.Cil.ExceptionHandler
            Get
                If Handlers.Count = 0 Then
                    Return Nothing
                Else
                    Return Handlers(Handlers.Count - 1)
                End If
            End Get
        End Property
    End Class

    Private m_ExceptionBlocks As Generic.Stack(Of TryBlock)

    Public Sub BeginCatchBlock(ByVal exceptionType As Mono.Cecil.TypeReference)
        Log("BeginCatchBlock")
        Helper.Assert(Helper.IsEmittableMember(m_Compiler, exceptionType))

        If exceptionType Is Nothing Then
            Dim block As TryBlock = m_ExceptionBlocks.Peek
            Dim ex As Mono.Cecil.Cil.ExceptionHandler = block.Handlers(block.Handlers.Count - 1)
            CilWorker.Emit(Mono.Cecil.Cil.OpCodes.Endfilter)
            ex.FilterEnd = CilWorker.Emit(Mono.Cecil.Cil.OpCodes.Nop)
            ex.HandlerStart = ex.FilterEnd
        Else
            Dim ex As New Mono.Cecil.Cil.ExceptionHandler(Mono.Cecil.Cil.ExceptionHandlerType.Catch)
            Dim block As TryBlock = m_ExceptionBlocks.Peek
            CilWorker.Emit(Mono.Cecil.Cil.OpCodes.Leave, block.EndBlock)
            Dim handlerStart As Integer = CilBody.Instructions.Count
            If block.Handlers.Count = 0 Then
                ex.TryEnd = CilWorker.Emit(Mono.Cecil.Cil.OpCodes.Nop)
            Else
                ex.TryEnd = block.Handlers(0).TryEnd
                CilWorker.Emit(Mono.Cecil.Cil.OpCodes.Nop)
            End If
            block.EndTry = ex.TryEnd
            If block.Handlers.Count > 0 Then
                block.Handlers(block.Handlers.Count - 1).HandlerEnd = CilBody.Instructions(CilBody.Instructions.Count - 1) 'CilWorker.Emit(Mono.Cecil.Cil.OpCodes.Nop)
            End If
            ex.HandlerStart = CilBody.Instructions(handlerStart)
            ex.CatchType = exceptionType
            block.Handlers.Add(ex)
        End If
    End Sub

    Private Sub BeginExceptFilterBlockCecil()
        Log("BeginExceptFilterBlock")
        Dim ex As New Mono.Cecil.Cil.ExceptionHandler(Mono.Cecil.Cil.ExceptionHandlerType.Filter)
        Dim block As TryBlock = m_ExceptionBlocks.Peek

        CilWorker.Emit(Mono.Cecil.Cil.OpCodes.Leave, block.EndBlock)
        If block.EndTry Is Nothing Then block.EndTry = CilWorker.Emit(Mono.Cecil.Cil.OpCodes.Nop)
        If block.Handlers.Count > 0 Then
            block.Handlers(block.Handlers.Count - 1).HandlerEnd = CilWorker.Emit(Mono.Cecil.Cil.OpCodes.Nop) ' cilBody.Instructions(CilBody.Instructions.Count - 1) ' CilWorker.Emit(Mono.Cecil.Cil.OpCodes.Nop)
        End If

        ex.FilterStart = CilBody.Instructions(CilBody.Instructions.Count - 1)
        ex.HandlerStart = ex.FilterStart
        block.Handlers.Add(ex)
    End Sub

    Private Function BeginExceptionBlockCecil() As Mono.Cecil.Cil.Instruction
        Log("BeginExceptionBlock")
        Dim block As New TryBlock
        CilWorker.Emit(Mono.Cecil.Cil.OpCodes.Nop)
        block.Start = CilBody.Instructions.Count
        block.EndBlock = CilWorker.Create(Mono.Cecil.Cil.OpCodes.Nop)
        If m_ExceptionBlocks Is Nothing Then m_ExceptionBlocks = New Generic.Stack(Of TryBlock)
        m_ExceptionBlocks.Push(block)
        CilWorker.Emit(Mono.Cecil.Cil.OpCodes.Nop)
        Return block.EndBlock
    End Function

    Private Sub BeginFaultBlockCecil()
        Log("BeginFaultBlock")
        'VB doesn't support fault blocks.
        Throw New NotImplementedException("m_ILGen.BeginFaultBlock()")
    End Sub

    Private Sub BeginFinallyBlockCecil()
        Log("BeginFinallyBlock")
        Dim ex As New Mono.Cecil.Cil.ExceptionHandler(Mono.Cecil.Cil.ExceptionHandlerType.Finally)
        Dim block As TryBlock = m_ExceptionBlocks.Peek
        CilWorker.Emit(Mono.Cecil.Cil.OpCodes.Leave, block.EndBlock)
        If block.EndTry Is Nothing Then
            block.EndTry = CilWorker.Emit(Mono.Cecil.Cil.OpCodes.Nop)
        End If
        If block.Handlers.Count > 0 Then
            block.Handlers(block.Handlers.Count - 1).HandlerEnd = CilWorker.Emit(Mono.Cecil.Cil.OpCodes.Nop) ' CilBody.Instructions(CilBody.Instructions.Count - 1)
        End If

        ex.HandlerStart = CilBody.Instructions(CilBody.Instructions.Count - 1)
        ex.TryEnd = ex.HandlerStart
        block.Handlers.Add(ex)
    End Sub

    Public Sub EndExceptionBlockCecil()
        Log("EndExceptionBlock")
        Dim block As TryBlock = m_ExceptionBlocks.Pop
        If block.EndTry Is Nothing Then block.EndTry = CilWorker.Emit(Mono.Cecil.Cil.OpCodes.Nop)

        Dim TryStart As Mono.Cecil.Cil.Instruction
        TryStart = CilBody.Instructions(block.Start)
        If block.Handlers(block.Handlers.Count - 1).Type = Mono.Cecil.Cil.ExceptionHandlerType.Finally Then
            CilWorker.Emit(Mono.Cecil.Cil.OpCodes.Endfinally)
        End If
        For i As Integer = 0 To block.Handlers.Count - 1
            Dim handler As Mono.Cecil.Cil.ExceptionHandler
            handler = block.Handlers(i)
            handler.TryStart = TryStart
            If handler.TryEnd Is Nothing Then
                handler.TryEnd = block.EndTry
            End If
            If handler.HandlerEnd Is Nothing Then
                If handler.Type <> Mono.Cecil.Cil.ExceptionHandlerType.Finally Then
                    CilWorker.Emit(Mono.Cecil.Cil.OpCodes.Leave, block.EndBlock)
                End If
                handler.HandlerEnd = block.EndBlock
            End If
            CilBody.ExceptionHandlers.Add(handler)
        Next
        CilWorker.Append(block.EndBlock)
    End Sub

    'Private Sub BeginScopeCecil()
    '    Log("BeginScope")
    '    Throw New NotImplementedException("m_ILGen.BeginScope()")
    'End Sub

    Public Function DeclareLocal(ByVal localType As Mono.Cecil.TypeReference) As Mono.Cecil.Cil.VariableDefinition
        Helper.Assert(Helper.IsEmittableMember(Compiler, localType))
        Log("DeclareLocal({0})", localType.ToString)
        Dim local As Mono.Cecil.Cil.VariableDefinition
        local = New Mono.Cecil.Cil.VariableDefinition(localType)
        m_CilBody.Variables.Add(local)
        m_CilBody.InitLocals = True
        Return local
    End Function

    Public Function DeclareLocal(ByVal localType As Mono.Cecil.TypeReference, ByVal pinned As Boolean) As Mono.Cecil.Cil.VariableDefinition
        Helper.Assert(Helper.IsEmittableMember(Compiler, localType))
        Log("DeclareLocal({0})", localType.ToString)
        Throw New NotImplementedException("DeclareLocal (pinned")
        'Dim local As Mono.Cecil.Cil.VariableDefinition
        'local = New Mono.Cecil.Cil.VariableDefinition(localType)
        'm_CilBody.Variables.Add(local)
        'Return local
    End Function

    Private Function DefineLabelCecil() As Mono.Cecil.Cil.Instruction
        Log("DefineLabel")
        Dim result As Mono.Cecil.Cil.Instruction
        result = CilWorker.Create(Mono.Cecil.Cil.OpCodes.Nop)
        Return result
    End Function

    Public Sub Emit(ByVal opcode As Mono.Cecil.Cil.OpCode)
        Log("Emit({0})", opcode.ToString)
        CilWorker.Emit(opcode)
    End Sub

    Public Sub Emit(ByVal opcode As Mono.Cecil.Cil.OpCode, ByVal arg As Byte)
        Log("Emit({0},{1})", opcode.ToString, ToString(arg))
        CilWorker.Emit(opcode, arg)
    End Sub

    Public Sub Emit(ByVal opcode As Mono.Cecil.Cil.OpCode, ByVal arg As Double)
        Log("Emit({0},{1})", opcode.ToString, ToString(arg))
        CilWorker.Emit(opcode, arg)
    End Sub

    Public Sub Emit(ByVal opcode As Mono.Cecil.Cil.OpCode, ByVal arg As Integer)
        Log("Emit({0},{1})", opcode.ToString, ToString(arg))
        CilWorker.Emit(opcode, arg)
    End Sub

    Public Sub Emit(ByVal opcode As Mono.Cecil.Cil.OpCode, ByVal arg As Long)
        Log("Emit({0},{1})", opcode.ToString, ToString(arg))
        CilWorker.Emit(opcode, arg)
    End Sub

    Public Sub Emit(ByVal opcode As Mono.Cecil.Cil.OpCode, ByVal arg As SByte)
        Log("Emit({0},{1})", opcode.ToString, ToString(arg))
        CilWorker.Emit(opcode, arg)
    End Sub

    Public Sub Emit(ByVal opcode As Mono.Cecil.Cil.OpCode, ByVal arg As Short)
        Log("Emit({0},{1})", opcode.ToString, ToString(arg))
        CilWorker.Emit(opcode, arg)
    End Sub

    Public Sub Emit(ByVal opcode As Mono.Cecil.Cil.OpCode, ByVal arg As Single)
        Log("Emit({0},{1})", opcode.ToString, ToString(arg))
        CilWorker.Emit(opcode, arg)
    End Sub

    Public Sub Emit(ByVal opcode As Mono.Cecil.Cil.OpCode, ByVal str As String)
        Log("Emit({0},{1})", opcode.ToString, ToString(str))
        CilWorker.Emit(opcode, str)
    End Sub

    Public Sub Emit(ByVal opcode As Mono.Cecil.Cil.OpCode, ByVal con As Mono.Cecil.MethodReference)
        Helper.Assert(Helper.IsEmittableMember(Compiler, con))
        Log("Emit({0},{1})", opcode.ToString, ToString(con))
        CilWorker.Emit(opcode, con)
    End Sub

    Public Sub Emit(ByVal opcode As Mono.Cecil.Cil.OpCode, ByVal label As Mono.Cecil.Cil.Instruction)
        Log("Emit({0},{1})", opcode.ToString, ToString(label))
        CilWorker.Emit(opcode, label)
    End Sub

    Public Sub Emit(ByVal opcode As Mono.Cecil.Cil.OpCode, ByVal labels() As Mono.Cecil.Cil.Instruction)
        Log("Emit({0},{1})", opcode.ToString, ToString(labels))
        CilWorker.Emit(opcode, labels)
    End Sub

    Public Sub Emit(ByVal opcode As Mono.Cecil.Cil.OpCode, ByVal local As Mono.Cecil.Cil.VariableDefinition)
        Log("Emit({0},{1})", opcode.ToString, ToString(local))
        CilWorker.Emit(opcode, local)
    End Sub

    'Public Sub Emit(ByVal opcode As Mono.Cecil.Cil.OpCode, ByVal signature As System.Reflection.Emit.SignatureHelper)
    '    Log("Emit({0},{1})", opcode.ToString, ToString(signature))
    '    Throw New NotImplementedException("m_ILGen.Emit(opcode, signature)")
    'End Sub

    Public Sub Emit(ByVal opcode As Mono.Cecil.Cil.OpCode, ByVal field As Mono.Cecil.FieldReference)
        If TypeOf field Is Mono.Cecil.FieldDefinition AndAlso field.DeclaringType.GenericParameters.Count > 0 Then
            Dim dT As Mono.Cecil.GenericInstanceType
            dT = New Mono.Cecil.GenericInstanceType(field.DeclaringType)
            dT.GenericArguments.Add(field.DeclaringType.GenericParameters(0))
            field = New Mono.Cecil.FieldReference(field.Name, dT, field.FieldType)
        End If

        Helper.Assert(Helper.IsEmittableMember(Compiler, field))
        Log("Emit({0},{1})", opcode.ToString, ToString(field))
        CilWorker.Emit(opcode, field)
    End Sub

    Public Sub Emit(ByVal opcode As Mono.Cecil.Cil.OpCode, ByVal cls As Mono.Cecil.TypeReference)
        Helper.Assert(Helper.IsEmittableMember(Compiler, cls))
        Log("Emit({0},{1})", opcode.ToString, ToString(cls))
        CilWorker.Emit(opcode, cls)
    End Sub

    Public Sub EmitCall(ByVal opcode As Mono.Cecil.Cil.OpCode, ByVal methodInfo As Mono.Cecil.MethodReference, ByVal optionalParameterTypes() As Mono.Cecil.TypeReference)
        Helper.Assert(Helper.IsEmittableMember(Compiler, methodInfo))
        Log("EmitCall({0},{1},{2})", opcode.ToString, ToString(methodInfo), ToString(optionalParameterTypes))
        If optionalParameterTypes Is Nothing Then
            CilWorker.Emit(opcode, methodInfo)
        Else
            Throw New NotImplementedException("CilWorker.Append(CilWorker.Create(opcode, methodInfo, optionalParameterTypes))")
        End If
    End Sub

    'Public Sub EmitCalli(ByVal opcode As Mono.Cecil.Cil.OpCode, ByVal callingConvention As System.Reflection.CallingConventions, ByVal returnType As System.Type, ByVal parameterTypes() As System.Type, ByVal optionalParameterTypes() As System.Type)
    '    Helper.Assert(Helper.IsEmittableMember(returnType))
    '    Helper.Assert(Helper.IsEmittableMember(parameterTypes))
    '    Helper.Assert(Helper.IsEmittableMember(optionalParameterTypes))
    '    Log("EmitCalli({0},{1},{2},{3},{4})", opcode.ToString, ToString(callingConvention), ToString(returnType), ToString(parameterTypes), ToString(optionalParameterTypes))
    '    Throw New NotImplementedException("m_ILGen.EmitCalli(opcode, callingConvention, returnType, parameterTypes, optionalParameterTypes)")
    'End Sub

    'Public Sub EmitCalli(ByVal opcode As Mono.Cecil.Cil.OpCode, ByVal unmanagedCallConv As System.Runtime.InteropServices.CallingConvention, ByVal returnType As System.Type, ByVal parameterTypes() As System.Type)
    '    Helper.Assert(Helper.IsEmittableMember(returnType))
    '    Helper.Assert(Helper.IsEmittableMember(parameterTypes))
    '    Log("EmitCalli({0},{1},{2},{3})", opcode.ToString, ToString(unmanagedCallConv), ToString(returnType), ToString(parameterTypes))
    '    Throw New NotImplementedException("m_ILGen.EmitCalli(opcode, unmanagedCallConv, returnType, parameterTypes)")
    'End Sub

    'Public Sub EmitWriteLine(ByVal value As String)
    '    Log("EmitWriteLine({0})", ToString(value))
    '    Throw New NotImplementedException("m_ILGen.EmitWriteLine(value)")
    'End Sub

    'Public Sub EmitWriteLine(ByVal localBuilder As Mono.Cecil.Cil.Instruction)
    '    Log("EmitWriteLine({0})", ToString(localBuilder))
    '    Throw New NotImplementedException("m_ILGen.EmitWriteLine(localBuilder)")
    'End Sub

    'Public Sub EmitWriteLine(ByVal fld As Mono.Cecil.FieldReference)
    '    Helper.Assert(Helper.IsEmittableMember(Compiler, fld))
    '    Log("EmitWriteLine({0})", ToString(fld))
    '    Throw New NotImplementedException("m_ILGen.EmitWriteLine(fld)")
    'End Sub

    'Public Sub EndScopeCecil()
    '    Log("EndScope")
    '    'm_ILGen.EndScope()
    'End Sub

    Public Sub MarkLabel(ByVal loc As Mono.Cecil.Cil.Instruction)
        Log("MarkLabel({0}", ToString(loc))
        CilWorker.Append(loc)
        'Throw New NotImplementedException("m_ILGen.MarkLabel(loc)")
    End Sub

    'Private Sub MarkSequencePoint(ByVal document As System.Diagnostics.SymbolStore.ISymbolDocumentWriter, ByVal startLine As Integer, ByVal startColumn As Integer, ByVal endLine As Integer, ByVal endColumn As Integer)
    '    Log("MarkSequencePoint({0},{1},{2},{3},{4}", CObj(document).ToString, startLine.ToString, startColumn.ToString, endLine.ToString, endColumn.ToString)
    '    Throw New NotImplementedException("m_ILGen.MarkSequencePoint(document, startLine, startColumn, endLine, endColumn)")
    'End Sub

    Public Sub ThrowException(ByVal excType As Mono.Cecil.TypeReference)
        Helper.Assert(Helper.IsEmittableMember(Compiler, excType))
        Log("ThrowException({0})", ToString(excType))
        CilWorker.Emit(Mono.Cecil.Cil.OpCodes.Throw, excType)
    End Sub

    'Public Sub UsingNamespaceCecil(ByVal usingNamespace As String)
    '    Log("UsingNamespace({0})", usingNamespace)
    '    Throw New NotImplementedException("m_ILGen.UsingNamespace(usingNamespace)")
    'End Sub
#End If
End Class
#End If
