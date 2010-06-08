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
'''SyncLockStatement  ::=
'''	"SyncLock" Expression  StatementTerminator
'''	   [  Block  ]
'''	"End" "SyncLock" StatementTerminator
''' </summary>
''' <remarks></remarks>
Public Class SyncLockStatement
    Inherits BlockStatement

    Private m_Lock As Expression

    ReadOnly Property Lock() As Expression
        Get
            Return m_Lock
        End Get
    End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        result = Helper.ResolveTypeReferences(m_Lock) AndAlso result
        result = MyBase.ResolveTypeReferences() AndAlso result

        Return result
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Shadows Sub Init(ByVal Lock As Expression, ByVal Code As CodeBlock)
        MyBase.Init(Code)
        m_Lock = Lock
    End Sub

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Dim lockType As Mono.Cecil.TypeReference = Helper.GetTypeOrTypeBuilder(Compiler, m_Lock.ExpressionType)

        Dim lockVariable As Mono.Cecil.Cil.VariableDefinition
        lockVariable = Emitter.DeclareLocal(Info, lockType)

        result = m_Lock.GenerateCode(Info.Clone(Me, True, False, lockType)) AndAlso result
        Emitter.EmitStoreVariable(Info, lockVariable)

        If Helper.CompareType(Compiler.TypeCache.System_Object, lockVariable.VariableType) Then
            Emitter.EmitLoadVariable(Info, lockVariable)
            Emitter.EmitCall(Info, Compiler.TypeCache.MS_VB_CS_ObjectFlowControl__CheckForSyncLockOnValueType_Object)
        End If

        Dim endException As Label
        endException = Emitter.EmitBeginExceptionBlock(Info)
        'Enter the lock
        Emitter.EmitLoadVariable(Info, lockVariable)
        Emitter.EmitCall(Info, Compiler.TypeCache.System_Threading_Monitor__Enter_Object)

        'Emit the code
        result = CodeBlock.GenerateCode(Info) AndAlso result
        Info.ILGen.BeginFinallyBlock()

        'Exit the lock
        Emitter.EmitLoadVariable(Info, lockVariable)
        Emitter.EmitCall(Info, Compiler.TypeCache.System_Threading_Monitor__Exit_Object)

        Info.ILGen.EndExceptionBlock()

        Return result
    End Function

    Public Overrides Function ResolveStatement(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = m_Lock.ResolveExpression(INfo) AndAlso result
        result = CodeBlock.ResolveCode(info) AndAlso result

        Return result
    End Function
End Class
