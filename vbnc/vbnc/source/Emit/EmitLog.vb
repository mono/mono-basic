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
#Const DEBUGEMIT = 0

Public Class EmitLog
    Private m_ILGen As ILGenerator
    Private m_Compiler As Compiler
    ReadOnly Property TheRealILGenerator() As ILGenerator
        Get
            Return m_ILGen
        End Get
    End Property

    Sub New(ByVal ILGen As ILGenerator, ByVal Compiler As Compiler)
        m_ILGen = ILGen
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
    Public Sub BeginCatchBlock(ByVal exceptionType As System.Type)
        Log("BeginCatchBlock")
        Helper.Assert(Helper.IsEmittableMember(exceptionType))
        m_ILGen.BeginCatchBlock(exceptionType)
    End Sub
    Public Sub BeginExceptFilterBlock()
        Log("BeginExceptFilterBlock")
        m_ILGen.BeginExceptFilterBlock()
    End Sub
    Public Function BeginExceptionBlock() As System.Reflection.Emit.Label
        Log("BeginExceptionBlock")
        Return m_ILGen.BeginExceptionBlock()
    End Function
    Public Sub BeginFaultBlock()
        Log("BeginFaultBlock")
        m_ILGen.BeginFaultBlock()
    End Sub
    Public Sub BeginFinallyBlock()
        Log("BeginFinallyBlock")
        m_ILGen.BeginFinallyBlock()
    End Sub
    Public Sub BeginScope()
        Log("BeginScope")
        m_ILGen.BeginScope()
    End Sub
    Public Function DeclareLocal(ByVal localType As System.Type) As System.Reflection.Emit.LocalBuilder
        Helper.Assert(Helper.IsEmittableMember(localType))
        Log("DeclareLocal({0})", localType.ToString)
        Return m_ILGen.DeclareLocal(localType)
    End Function
    Public Function DeclareLocal(ByVal localType As System.Type, ByVal pinned As Boolean) As System.Reflection.Emit.LocalBuilder
        Helper.Assert(Helper.IsEmittableMember(localType))
        Log("DeclareLocal({0}, {1})", localType.ToString, pinned.ToString)
        Return m_ILGen.DeclareLocal(localType, pinned)
    End Function
    Public Function DefineLabel() As System.Reflection.Emit.Label
        Log("DefineLabel")
        Return m_ILGen.DefineLabel()
    End Function
    Public Sub Emit(ByVal opcode As System.Reflection.Emit.OpCode)
        Log("Emit({0})", opcode.ToString)
        m_ILGen.Emit(opcode)
    End Sub
    Public Sub Emit(ByVal opcode As System.Reflection.Emit.OpCode, ByVal arg As Byte)
        Log("Emit({0},{1})", opcode.ToString, ToString(arg))
        m_ILGen.Emit(opcode, arg)
    End Sub
    Public Sub Emit(ByVal opcode As System.Reflection.Emit.OpCode, ByVal arg As Double)
        Log("Emit({0},{1})", opcode.ToString, ToString(arg))
        m_ILGen.Emit(opcode, arg)
    End Sub
    Public Sub Emit(ByVal opcode As System.Reflection.Emit.OpCode, ByVal arg As Integer)
        Log("Emit({0},{1})", opcode.ToString, ToString(arg))
        m_ILGen.Emit(opcode, arg)
    End Sub
    Public Sub Emit(ByVal opcode As System.Reflection.Emit.OpCode, ByVal arg As Long)
        Log("Emit({0},{1})", opcode.ToString, ToString(arg))
        m_ILGen.Emit(opcode, arg)
    End Sub
    Public Sub Emit(ByVal opcode As System.Reflection.Emit.OpCode, ByVal arg As SByte)
        Log("Emit({0},{1})", opcode.ToString, ToString(arg))
        m_ILGen.Emit(opcode, arg)
    End Sub
    Public Sub Emit(ByVal opcode As System.Reflection.Emit.OpCode, ByVal arg As Short)
        Log("Emit({0},{1})", opcode.ToString, ToString(arg))
        m_ILGen.Emit(opcode, arg)
    End Sub
    Public Sub Emit(ByVal opcode As System.Reflection.Emit.OpCode, ByVal arg As Single)
        Log("Emit({0},{1})", opcode.ToString, ToString(arg))
        m_ILGen.Emit(opcode, arg)
    End Sub
    Public Sub Emit(ByVal opcode As System.Reflection.Emit.OpCode, ByVal str As String)
        Log("Emit({0},{1})", opcode.ToString, ToString(str))
        m_ILGen.Emit(opcode, str)
    End Sub
    Public Sub Emit(ByVal opcode As System.Reflection.Emit.OpCode, ByVal con As System.Reflection.ConstructorInfo)
        Helper.Assert(Helper.IsEmittableMember(con))
        Log("Emit({0},{1})", opcode.ToString, ToString(con))
        m_ILGen.Emit(opcode, con)
    End Sub
    Public Sub Emit(ByVal opcode As System.Reflection.Emit.OpCode, ByVal label As System.Reflection.Emit.Label)
        Log("Emit({0},{1})", opcode.ToString, ToString(label))
        m_ILGen.Emit(opcode, label)
    End Sub
    Public Sub Emit(ByVal opcode As System.Reflection.Emit.OpCode, ByVal labels() As System.Reflection.Emit.Label)
        Log("Emit({0},{1})", opcode.ToString, ToString(labels))
        m_ILGen.Emit(opcode, labels)
    End Sub
    Public Sub Emit(ByVal opcode As System.Reflection.Emit.OpCode, ByVal local As System.Reflection.Emit.LocalBuilder)
        Log("Emit({0},{1})", opcode.ToString, ToString(local))
        m_ILGen.Emit(opcode, local)
    End Sub
    Public Sub Emit(ByVal opcode As System.Reflection.Emit.OpCode, ByVal signature As System.Reflection.Emit.SignatureHelper)
        Log("Emit({0},{1})", opcode.ToString, ToString(signature))
        m_ILGen.Emit(opcode, signature)
    End Sub
    Public Sub Emit(ByVal opcode As System.Reflection.Emit.OpCode, ByVal field As System.Reflection.FieldInfo)
        Helper.Assert(Helper.IsEmittableMember(field))
        Log("Emit({0},{1})", opcode.ToString, ToString(field))
        m_ILGen.Emit(opcode, field)
    End Sub
    Public Sub Emit(ByVal opcode As System.Reflection.Emit.OpCode, ByVal meth As System.Reflection.MethodInfo)
        Helper.Assert(Helper.IsEmittableMember(meth))
        Log("Emit({0},{1})", opcode.ToString, ToString(meth))
        m_ILGen.Emit(opcode, meth)
    End Sub
    Public Sub Emit(ByVal opcode As System.Reflection.Emit.OpCode, ByVal cls As System.Type)
        Helper.Assert(Helper.IsEmittableMember(cls))
        Log("Emit({0},{1})", opcode.ToString, ToString(cls))
        m_ILGen.Emit(opcode, cls)
    End Sub
    Public Sub EmitCall(ByVal opcode As System.Reflection.Emit.OpCode, ByVal methodInfo As System.Reflection.MethodInfo, ByVal optionalParameterTypes() As System.Type)
        Helper.Assert(Helper.IsEmittableMember(methodInfo))
        Log("EmitCall({0},{1},{2})", opcode.ToString, ToString(methodInfo), ToString(optionalParameterTypes))
        m_ILGen.EmitCall(opcode, methodInfo, optionalParameterTypes)
    End Sub
    Public Sub EmitCalli(ByVal opcode As System.Reflection.Emit.OpCode, ByVal callingConvention As System.Reflection.CallingConventions, ByVal returnType As System.Type, ByVal parameterTypes() As System.Type, ByVal optionalParameterTypes() As System.Type)
        Helper.Assert(Helper.IsEmittableMember(returnType))
        Helper.Assert(Helper.IsEmittableMember(parameterTypes))
        Helper.Assert(Helper.IsEmittableMember(optionalParameterTypes))
        Log("EmitCalli({0},{1},{2},{3},{4})", opcode.ToString, ToString(callingConvention), ToString(returnType), ToString(parameterTypes), ToString(optionalParameterTypes))
        m_ILGen.EmitCalli(opcode, callingConvention, returnType, parameterTypes, optionalParameterTypes)
    End Sub
    Public Sub EmitCalli(ByVal opcode As System.Reflection.Emit.OpCode, ByVal unmanagedCallConv As System.Runtime.InteropServices.CallingConvention, ByVal returnType As System.Type, ByVal parameterTypes() As System.Type)
        Helper.Assert(Helper.IsEmittableMember(returnType))
        Helper.Assert(Helper.IsEmittableMember(parameterTypes))
        Log("EmitCalli({0},{1},{2},{3})", opcode.ToString, ToString(unmanagedCallConv), ToString(returnType), ToString(parameterTypes))
        m_ILGen.EmitCalli(opcode, unmanagedCallConv, returnType, parameterTypes)
    End Sub
    Public Sub EmitWriteLine(ByVal value As String)
        Log("EmitWriteLine({0})", ToString(value))
        m_ILGen.EmitWriteLine(value)
    End Sub
    Public Sub EmitWriteLine(ByVal localBuilder As System.Reflection.Emit.LocalBuilder)
        Log("EmitWriteLine({0})", ToString(localBuilder))
        m_ILGen.EmitWriteLine(localBuilder)
    End Sub
    Public Sub EmitWriteLine(ByVal fld As System.Reflection.FieldInfo)
        Helper.Assert(Helper.IsEmittableMember(fld))
        Log("EmitWriteLine({0})", ToString(fld))
        m_ILGen.EmitWriteLine(fld)
    End Sub
    Public Sub EndExceptionBlock()
        Log("EndExceptionBlock")
        m_ILGen.EndExceptionBlock()
    End Sub
    Public Sub EndScope()
        Log("EndScope")
        m_ILGen.EndScope()
    End Sub
    Public Sub MarkLabel(ByVal loc As System.Reflection.Emit.Label)
        Log("MarkLabel({0}", ToString(loc))
        m_ILGen.MarkLabel(loc)
    End Sub
    Public Sub MarkSequencePoint(ByVal document As System.Diagnostics.SymbolStore.ISymbolDocumentWriter, ByVal startLine As Integer, ByVal startColumn As Integer, ByVal endLine As Integer, ByVal endColumn As Integer)
        Log("MarkSequencePoint({0},{1},{2},{3},{4}", CObj(document).ToString, startLine.ToString, startColumn.ToString, endLine.ToString, endColumn.ToString)
        m_ILGen.MarkSequencePoint(document, startLine, startColumn, endLine, endColumn)
    End Sub
    Public Sub ThrowException(ByVal excType As System.Type)
        Helper.Assert(Helper.IsEmittableMember(excType))
        Log("ThrowException({0})", ToString(excType))
        m_ILGen.ThrowException(excType)
    End Sub
    Public Sub UsingNamespace(ByVal usingNamespace As String)
        Log("UsingNamespace({0})", usingNamespace)
        m_ILGen.UsingNamespace(usingNamespace)
    End Sub

End Class
#End If
