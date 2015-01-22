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

#Const DEBUGEMIT = 0

Public Structure Label
    Public CecilLabel As Mono.Cecil.Cil.Instruction
End Structure

Public Class LocalBuilder
    Public CecilLocal As Mono.Cecil.Cil.VariableDefinition
    ReadOnly Property LocalType() As Mono.Cecil.TypeReference
        Get
            Return CecilLocal.VariableType
        End Get
    End Property
End Class

Public Class EmitLog
    Private m_Compiler As Compiler

    Private m_CilBody As Mono.Cecil.Cil.MethodBody
    Private m_CilWorker As Mono.Cecil.Cil.ILProcessor

    ReadOnly Property CilWorker() As Mono.Cecil.Cil.ILProcessor
        Get
            If m_CilWorker Is Nothing Then
                m_CilWorker = m_CilBody.GetILProcessor
            End If
            Return m_CilWorker
        End Get
    End Property

    Property CilBody() As Mono.Cecil.Cil.MethodBody
        Get
            Return m_CilBody
        End Get
        Set(ByVal value As Mono.Cecil.Cil.MethodBody)
            m_CilBody = value
            m_CilWorker = Nothing
        End Set
    End Property

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

    <Diagnostics.Conditional("DEBUGEMIT")> _
    Private Sub Log(ByVal str As String)
#If DEBUGEMIT Then
        m_Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "Emitted: " & str)
#End If
    End Sub

    <Diagnostics.Conditional("DEBUGEMIT")> _
    Private Sub Log(ByVal str As String, ByVal ParamArray args() As String)
#If DEBUGEMIT Then
        Log(String.Format(str, args))
#End If
    End Sub

    Public Sub BeginExceptFilterBlock()
        Log("BeginExceptFilterBlock")
        BeginExceptFilterBlockCecil()
    End Sub
    Public Function BeginExceptionBlock() As Label
        Log("BeginExceptionBlock")
        Dim result As New Label
        result.CecilLabel = BeginExceptionBlockCecil()
        Return result
    End Function

    Public Sub BeginFinallyBlock()
        Log("BeginFinallyBlock")
        BeginFinallyBlockCecil()
    End Sub

    Public Function DefineLabel() As Label
        Log("DefineLabel")
        Dim result As New Label
        result.CecilLabel = DefineLabelCecil()
        Return result
    End Function

    Public Sub Emit(ByVal opcode As Mono.Cecil.Cil.OpCode, ByVal label As Label)
        Emit(opcode, label.CecilLabel)
    End Sub

    Public Sub Emit(ByVal opcode As Mono.Cecil.Cil.OpCode, ByVal labels() As Label)
        Dim tmpCecil As Mono.Cecil.Cil.Instruction()
        ReDim tmpCecil(labels.Length - 1)
        For i As Integer = 0 To labels.Length - 1
            tmpCecil(i) = labels(i).CecilLabel
        Next
        Emit(opcode, tmpCecil)
    End Sub

    Public Sub EndExceptionBlock()
        Log("EndExceptionBlock")
        EndExceptionBlockCecil()
    End Sub

    Public Sub MarkLabel(ByVal loc As Label)
        MarkLabel(loc.CecilLabel)
    End Sub

    Public Sub MarkSequencePoint(ByVal document As System.Diagnostics.SymbolStore.ISymbolDocumentWriter, ByVal startLine As Integer, ByVal startColumn As Integer, ByVal endLine As Integer, ByVal endColumn As Integer)
        Log("MarkSequencePoint({0},{1},{2},{3},{4}", CObj(document).ToString, startLine.ToString, startColumn.ToString, endLine.ToString, endColumn.ToString)
        'Throw New NotImplementedException
    End Sub

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
            ex.HandlerStart = CreateAndEmitNop()
        Else
            Dim ex As New Mono.Cecil.Cil.ExceptionHandler(Mono.Cecil.Cil.ExceptionHandlerType.Catch)
            Dim block As TryBlock = m_ExceptionBlocks.Peek
            CilWorker.Emit(Mono.Cecil.Cil.OpCodes.Leave, block.EndBlock)
            Dim handlerStart As Integer = CilBody.Instructions.Count
            If block.Handlers.Count = 0 Then
                ex.TryEnd = CreateAndEmitNop()
            Else
                ex.TryEnd = block.Handlers(0).TryEnd
                CilWorker.Emit(Mono.Cecil.Cil.OpCodes.Nop)
            End If
            If block.EndTry Is Nothing Then
                block.EndTry = ex.TryEnd
            End If
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
        If block.EndTry Is Nothing Then
            block.EndTry = CreateAndEmitNop()
        End If
        If block.Handlers.Count > 0 Then
            block.Handlers(block.Handlers.Count - 1).HandlerEnd = CreateAndEmitNop()
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

    Private Sub BeginFinallyBlockCecil()
        Log("BeginFinallyBlock")
        Dim ex As New Mono.Cecil.Cil.ExceptionHandler(Mono.Cecil.Cil.ExceptionHandlerType.Finally)
        Dim block As TryBlock = m_ExceptionBlocks.Peek
        CilWorker.Emit(Mono.Cecil.Cil.OpCodes.Leave, block.EndBlock)
        If block.EndTry Is Nothing Then
            block.EndTry = CreateAndEmitNop()
        End If
        If block.Handlers.Count > 0 Then
            block.Handlers(block.Handlers.Count - 1).HandlerEnd = CreateAndEmitNop()
        End If

        ex.HandlerStart = CilBody.Instructions(CilBody.Instructions.Count - 1)
        ex.TryEnd = ex.HandlerStart
        block.Handlers.Add(ex)
    End Sub

    Public Sub EndExceptionBlockCecil()
        Log("EndExceptionBlock")
        Dim block As TryBlock = m_ExceptionBlocks.Pop
        If block.EndTry Is Nothing Then block.EndTry = CreateAndEmitNop()

        Dim TryStart As Mono.Cecil.Cil.Instruction
        TryStart = CilBody.Instructions(block.Start)
        If block.Handlers(block.Handlers.Count - 1).HandlerType = Mono.Cecil.Cil.ExceptionHandlerType.Finally Then
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
                If handler.HandlerType <> Mono.Cecil.Cil.ExceptionHandlerType.Finally Then
                    CilWorker.Emit(Mono.Cecil.Cil.OpCodes.Leave, block.EndBlock)
                End If
                handler.HandlerEnd = block.EndBlock
            End If
            CilBody.ExceptionHandlers.Add(handler)
        Next
        CilWorker.Append(block.EndBlock)
    End Sub

    Public Function DeclareLocal(ByVal localType As Mono.Cecil.TypeReference) As Mono.Cecil.Cil.VariableDefinition
        Helper.Assert(Helper.IsEmittableMember(Compiler, localType))
        Log("DeclareLocal({0})", localType.ToString)
        Dim local As Mono.Cecil.Cil.VariableDefinition
        local = New Mono.Cecil.Cil.VariableDefinition(localType)
        m_CilBody.Variables.Add(local)
        m_CilBody.InitLocals = True
        Return local
    End Function

    Private Function DefineLabelCecil() As Mono.Cecil.Cil.Instruction
        Log("DefineLabel")
        Dim result As Mono.Cecil.Cil.Instruction
        result = CilWorker.Create(Mono.Cecil.Cil.OpCodes.Nop)
        Return result
    End Function

    Public Function CreateAndEmitNop() As Mono.Cecil.Cil.Instruction
        Dim result As Mono.Cecil.Cil.Instruction
        result = CilWorker.Create(Mono.Cecil.Cil.OpCodes.Nop)
        CilWorker.Append(result)
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
        Helper.Assert(local IsNot Nothing)
        Log("Emit({0},{1})", opcode.ToString, ToString(local))
        CilWorker.Emit(opcode, local)
    End Sub

    Public Sub Emit(ByVal opcode As Mono.Cecil.Cil.OpCode, ByVal field As Mono.Cecil.FieldReference)
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
        'Helper.Assert(Helper.IsEmittableMember(Compiler, methodInfo))
        Log("EmitCall({0},{1},{2})", opcode.ToString, ToString(methodInfo), ToString(optionalParameterTypes))
        'Helper.Assert(Helper.IsEmittableMember(Compiler, methodInfo.ReturnType.ReturnType))
        If optionalParameterTypes Is Nothing Then
            CilWorker.Emit(opcode, methodInfo)
        Else
            Throw New NotImplementedException("CilWorker.Append(CilWorker.Create(opcode, methodInfo, optionalParameterTypes))")
        End If
    End Sub

    Public Sub MarkLabel(ByVal loc As Mono.Cecil.Cil.Instruction)
        Log("MarkLabel({0}", ToString(loc))
        CilWorker.Append(loc)
    End Sub

    Public Sub ThrowException(ByVal excType As Mono.Cecil.TypeReference)
        Helper.Assert(Helper.IsEmittableMember(Compiler, excType))
        Log("ThrowException({0})", ToString(excType))
        CilWorker.Emit(Mono.Cecil.Cil.OpCodes.Throw, excType)
    End Sub
End Class
