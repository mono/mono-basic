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
''' ElseIfStatement  ::=
'''	   "ElseIf" BooleanExpression  [  Then  ]  StatementTerminator
'''	        [  Block  ]
''' </summary>
''' <remarks></remarks>
Public Class ElseIfStatement
    Inherits BlockStatement

    Private m_Condition As Expression

    ReadOnly Property Condition() As Expression
        Get
            Return m_condition
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Shadows Sub Init(ByVal Code As CodeBlock, ByVal Condition As Expression)
        MyBase.Init(Code)
        m_Condition = Condition
    End Sub

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Dim falseLabel As Label = Info.ILGen.DefineLabel

        result = m_Condition.GenerateCode(Info.Clone(Me, True, False, Compiler.TypeCache.System_Boolean)) AndAlso result
        Emitter.EmitBranchIfFalse(Info, falseLabel)

        result = CodeBlock.GenerateCode(Info) AndAlso result
        Emitter.EmitBranch(Info, ParentAsIfStatement.EndLabel)

        Info.ILGen.MarkLabel(falseLabel)

        Return result
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        result = m_Condition.ResolveTypeReferences AndAlso result

        result = MyBase.ResolveTypeReferences AndAlso result

        Return result
    End Function

    Public Overrides Function ResolveStatement(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = m_Condition.ResolveExpression(Info) AndAlso result
        result = CodeBlock.ResolveCode(Info) AndAlso result

        If m_Condition.Classification.IsValueClassification Then
            'nothing to do
        ElseIf m_Condition.Classification.CanBeValueClassification Then
            m_Condition = m_Condition.ReclassifyToValueExpression
            result = m_Condition.ResolveExpression(ResolveInfo.Default(Compiler)) AndAlso result

            If result = False Then
                result = Helper.AddError(Me) AndAlso result
                Return result
            End If
            m_Condition = Helper.CreateTypeConversion(Me, m_Condition, Compiler.TypeCache.System_Boolean, result)

            If result = False Then
                result = Helper.AddError(Me) AndAlso result
                Return result
            End If
        Else
            result = Helper.AddError(Me, "Each expression in an If...Then...Else statement must be classified as a value and be implicitly convertible to Boolean") AndAlso result
        End If

        Return result
    End Function

    Shadows ReadOnly Property Parent() As IfStatement
        Get
            Return DirectCast(MyBase.Parent, IfStatement)
        End Get
    End Property

    Function ParentAsIfStatement() As IfStatement
        Helper.Assert(TypeOf Me.Parent Is IfStatement)
        Return DirectCast(Me.Parent, IfStatement)
    End Function
End Class
