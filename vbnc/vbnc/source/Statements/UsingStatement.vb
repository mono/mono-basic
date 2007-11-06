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
''' UsingStatement  ::=
'''	"Using" UsingResources  StatementTerminator
'''		[  Block  ]
'''	"End" "Using" StatementTerminator
''' 
''' UsingResources  ::=  VariableDeclarators  |  Expression
''' 
''' LAMESPEC!?
''' I'm using this:
''' UsingResources ::= UsingDeclarators | Expression
''' </summary>
''' <remarks></remarks>
Public Class UsingStatement
    Inherits BlockStatement

    Private m_UsingResources As ParsedObject

    ReadOnly Property UsingResources() As ParsedObject
        Get
            Return m_UsingResources
        End Get
    End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        ' result = Helper.ResolveTypeReferences(m_UsingResources) AndAlso result
        result = MyBase.ResolveTypeReferences() AndAlso result

        Return result
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Shadows Sub Init(ByVal UsingResources As ParsedObject, ByVal Code As CodeBlock)
        MyBase.Init(Code)
        m_UsingResources = UsingResources
    End Sub

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Dim usingDecls As UsingDeclarators = TryCast(m_UsingResources, UsingDeclarators)
        Dim usingExp As Expression = TryCast(m_UsingResources, Expression)
        Dim usingVars As New Generic.Stack(Of LocalBuilder)
        Dim exceptionEnds As New Generic.Stack(Of Label)
        Dim exceptionEnds2 As New Generic.Stack(Of Label)

        If usingDecls IsNot Nothing Then
            For i As Integer = 0 To usingDecls.Count - 1
                Dim tmpDecl As UsingDeclarator = usingDecls(i)
                result = usingDecls(i).GenerateCode(Info) AndAlso result
                usingVars.Push(tmpDecl.UsingVariable)
                exceptionEnds.Push(Info.ILGen.BeginExceptionBlock())
                exceptionEnds2.Push(Info.ILGen.DefineLabel)
            Next
        ElseIf usingExp IsNot Nothing Then
            Dim local As LocalBuilder = Emitter.DeclareLocal(Info, usingExp.ExpressionType)
            result = usingExp.GenerateCode(Info.Clone(Me, True, False, usingExp.ExpressionType)) AndAlso result
            Emitter.EmitStoreVariable(Info, local)
            usingVars.Push(local)
            exceptionEnds.Push(Info.ILGen.BeginExceptionBlock())
            exceptionEnds2.Push(Info.ILGen.DefineLabel)
        Else
            Throw New InternalException(Me)
        End If

        result = CodeBlock.GenerateCode(Info) AndAlso result

        Do Until usingVars.Count = 0
            Dim tmpvar As LocalBuilder = usingVars.Pop
            Dim endblock As Label = exceptionEnds.Pop
            Dim endblock2 As Label = exceptionEnds2.Pop

            Info.ILGen.BeginFinallyBlock()
            Emitter.EmitLoadVariable(Info, tmpvar)
            Emitter.EmitBranchIfFalse(Info, endblock2)
            Emitter.EmitLoadVariable(Info, tmpvar)
            Emitter.EmitCallVirt(Info, Compiler.TypeCache.System_IDisposable__Dispose)
            Info.ILGen.MarkLabel(endblock2)
            Info.ILGen.EndExceptionBlock()
        Loop

        Return result
    End Function

    Public Overrides Function ResolveStatement(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True


        result = m_UsingResources.ResolveCode(Info) AndAlso result

        result = CodeBlock.ResolveCode(Info) AndAlso result

        Return result
    End Function
End Class
