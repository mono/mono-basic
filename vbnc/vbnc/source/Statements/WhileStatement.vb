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
''' WhileStatement  ::=
'''	   "While" BooleanExpression  StatementTerminator
'''	         [  Block  ]
'''	   "End" "While" StatementTerminator
''' </summary>
''' <remarks></remarks>
Public Class WhileStatement
    Inherits BlockStatement

    Private m_Condition As Expression

    Private m_NextIteration As Label

    ReadOnly Property Condition() As Expression
        Get
            Return m_condition
        End Get
    End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        result = Helper.ResolveTypeReferences(m_Condition) AndAlso result
        result = MyBase.ResolveTypeReferences() AndAlso result

        Return result
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Shadows Sub Init(ByVal Condition As Expression, ByVal Code As CodeBlock)
        MyBase.Init(Code)
        m_Condition = Condition
    End Sub

    ReadOnly Property NextIteration() As Label
        Get
            Return m_NextIteration
        End Get
    End Property

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        EndLabel = Emitter.DefineLabel(Info)
        m_NextIteration = Emitter.DefineLabel(Info)

        Emitter.MarkLabel(Info, m_NextIteration)
        result = m_Condition.GenerateCode(Info.Clone(Me, True, , Compiler.TypeCache.System_Boolean)) AndAlso result
        Emitter.EmitBranchIfFalse(Info, EndLabel)
        result = CodeBlock.GenerateCode(Info) AndAlso result
        Emitter.EmitBranch(Info, m_NextIteration)
        Emitter.MarkLabel(Info, EndLabel)

        Return result
    End Function

    Public Overrides Function ResolveStatement(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = m_Condition.ResolveExpression(Info) AndAlso result
        result = Helper.VerifyValueClassification(m_Condition, Info) AndAlso result
        result = CodeBlock.ResolveCode(Info) AndAlso result

        Return result
    End Function

End Class
