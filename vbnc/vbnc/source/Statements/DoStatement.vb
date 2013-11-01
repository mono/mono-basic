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
''' DoLoopStatement  ::=  DoTopLoopStatement  |  DoBottomLoopStatement
''' DoTopLoopStatement  ::=
'''	   "Do" [  WhileOrUntil  BooleanExpression  ]  StatementTerminator
'''	       [  Block  ]
'''	   "Loop" StatementTerminator
''' DoBottomLoopStatement  ::=
'''	   "Do" StatementTerminator
'''	       [  Block  ]
'''	   "Loop" WhileOrUntil  BooleanExpression  StatementTerminator
'''WhileOrUntil  ::= "While" | "Until"
''' </summary>
''' <remarks></remarks>
Public Class DoStatement
    Inherits BlockStatement

    Private m_PreCondition As Expression
    Private m_PostCondition As Expression
    Private m_IsWhile As Boolean

    Private m_NextIteration As Label

    ReadOnly Property PreCondition() As Expression
        Get
            Return m_precondition
        End Get
    End Property

    ReadOnly Property PostCondition() As Expression
        Get
            Return m_postcondition
        End Get
    End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        result = Helper.ResolveTypeReferences(m_PreCondition, m_PostCondition) AndAlso result
        result = MyBase.ResolveTypeReferences AndAlso result

        Return result
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Shadows Sub Init(ByVal PreCondition As Expression, ByVal PostCondition As Expression, ByVal IsWhile As Boolean, ByVal Code As CodeBlock)
        MyBase.Init(Code)

        m_PreCondition = PreCondition
        m_PostCondition = PostCondition
        m_IsWhile = IsWhile
    End Sub

    ReadOnly Property NextIteration() As Label
        Get
            Return m_NextIteration
        End Get
    End Property

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Dim startLabel As Label = Emitter.DefineLabel(Info)
        EndLabel = Emitter.DefineLabel(Info)
        m_NextIteration = Emitter.DefineLabel(Info)

        Emitter.MarkLabel(Info, startLabel)
        If m_PreCondition IsNot Nothing Then
            Emitter.MarkLabel(Info, m_NextIteration)
            result = m_PreCondition.GenerateCode(Info.Clone(Me, True, False, Compiler.TypeCache.System_Boolean)) AndAlso result
            Emitter.EmitConversion(m_PreCondition.ExpressionType, Compiler.TypeCache.System_Boolean, Info)
            If m_IsWhile Then
                Emitter.EmitBranchIfFalse(Info, EndLabel)
            Else
                Emitter.EmitBranchIfTrue(Info, EndLabel)
            End If
        End If

        result = CodeBlock.GenerateCode(Info) AndAlso result

        If m_PostCondition IsNot Nothing Then
            Emitter.MarkLabel(Info, m_NextIteration)
            result = m_PostCondition.GenerateCode(Info.Clone(Me, True, False, Compiler.TypeCache.System_Boolean)) AndAlso result
            Emitter.EmitConversion(m_PostCondition.ExpressionType, Compiler.TypeCache.System_Boolean, Info)
            If m_IsWhile Then
                Emitter.EmitBranchIfFalse(Info, EndLabel)
            Else
                Emitter.EmitBranchIfTrue(Info, EndLabel)
            End If
        End If

        If m_PreCondition Is Nothing AndAlso m_PostCondition Is Nothing Then
            Emitter.MarkLabel(Info, m_NextIteration)
        End If

        Emitter.EmitBranch(Info, startLabel)

        Emitter.MarkLabel(Info, EndLabel)

        Return result
    End Function

    Public Overrides Function ResolveStatement(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        If m_PreCondition IsNot Nothing Then
            result = m_PreCondition.ResolveExpression(Info) AndAlso result
            result = Helper.VerifyValueClassification(m_PreCondition, Info) AndAlso result

            If Not result Then Return result

            m_PreCondition = Helper.CreateTypeConversion(Me, m_PreCondition, Compiler.TypeCache.System_Boolean, result)
        End If

        If m_PostCondition IsNot Nothing Then
            result = m_PostCondition.ResolveExpression(info) AndAlso result
            result = Helper.VerifyValueClassification(m_PostCondition, Info) AndAlso result

            If Not result Then Return result

            m_PostCondition = Helper.CreateTypeConversion(Me, m_PostCondition, Compiler.TypeCache.System_Boolean, result)
        End If
        result = CodeBlock.ResolveCode(info) AndAlso result

        Return result
    End Function

    ReadOnly Property IsUntil() As Boolean
        Get
            Return Not m_IsWhile
        End Get
    End Property

    ReadOnly Property IsWhile() As Boolean
        Get
            Return m_IsWhile
        End Get
    End Property
End Class

