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
''' WithStatement  ::=
'''	   "With" Expression  StatementTerminator
'''	        [  Block  ]
'''	   "End" "With" StatementTerminator
''' </summary>
''' <remarks></remarks>
Public Class WithStatement
    Inherits BlockStatement

    Private m_WithExpression As Expression

    Private m_WithVariable As Mono.Cecil.Cil.VariableDefinition

    Private m_WithVariableExpression As Expression

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        result = Helper.ResolveTypeReferences(m_WithExpression) AndAlso result
        result = MyBase.ResolveTypeReferences() AndAlso result

        Return result
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Shadows Sub Init(ByVal Code As CodeBlock, ByVal WithExpression As Expression)
        MyBase.Init(Code)
        m_WithExpression = WithExpression
    End Sub

    ReadOnly Property WithVariable() As Mono.Cecil.Cil.VariableDefinition
        Get
            Return m_WithVariable
        End Get
    End Property

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        If m_WithExpression Is m_WithVariableExpression = False Then
            m_WithVariable = Emitter.DeclareLocal(Info, Helper.GetTypeOrTypeBuilder(Compiler, m_WithExpression.ExpressionType), "WithVariable" & Me.ObjectID.ToString)
            result = m_WithExpression.GenerateCode(Info.Clone(Me, True, False, m_WithVariable.VariableType)) AndAlso result
            Emitter.EmitStoreVariable(Info, m_WithVariable)
        End If

        result = CodeBlock.GenerateCode(Info) AndAlso result

        Emitter.FreeLocal(m_WithVariable)

        Return result
    End Function

    ReadOnly Property WithVariableExpression() As Expression
        Get
            Return m_WithVariableExpression
        End Get
    End Property

    Public Overrides Function ResolveStatement(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = m_WithExpression.ResolveExpression(Info) AndAlso result

        If result Then
            If CecilHelper.IsValueType(m_WithExpression.ExpressionType) AndAlso m_WithExpression.Classification.IsVariableClassification Then
                m_WithVariableExpression = m_WithExpression
            Else
                m_WithVariableExpression = New CompilerGeneratedExpression(Me, New CompilerGeneratedExpression.GenerateCodeDelegate(AddressOf GenerateVariableCode), m_WithExpression.ExpressionType)
                result = m_WithVariableExpression.ResolveExpression(Info) AndAlso result
            End If
        End If

        result = CodeBlock.ResolveCode(Info) AndAlso result

        Return result
    End Function

    Function GenerateVariableCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Helper.Assert(Info.IsRHS, "With variables can't be assigned to...")

        If Info.IsRHS Then
            Emitter.EmitLoadVariable(Info, m_WithVariable)
        End If

        Return result
    End Function
End Class
