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

Public MustInherit Class CompoundAssignmentStatement
    Inherits AssignmentStatement

    Private m_CompoundExpression As Expression

    Protected MustOverride Overloads Function ResolveStatement(ByVal LSide As Expression, ByVal RSide As Expression) As Expression

    Sub New(ByVal Parent As ParsedObject)
        MyBase.new(Parent)
    End Sub

    Private Function CheckIndexedStatement(ByVal Info As ResolveInfo, ByVal InvocationExpression As InvocationOrIndexExpression) As Boolean
        Dim result As Boolean = True

        If InvocationExpression Is Nothing Then Return result

        If InvocationExpression.Classification.IsVariableClassification AndAlso CecilHelper.IsArray(InvocationExpression.Expression.ExpressionType) Then
            result = ResolveIndexedStatement(Info, InvocationExpression) AndAlso result
        End If

        Return result
    End Function

    Private Function ResolveIndexedStatement(ByVal Info As ResolveInfo, ByVal InvocationExpression As InvocationOrIndexExpression) As Boolean
        Dim result As Boolean = True
        Dim block As CodeBlock = Me.FindFirstParent(Of CodeBlock)()

        For i As Integer = 0 To InvocationExpression.ArgumentList.Count - 1
            Dim arg As Argument = InvocationExpression.ArgumentList(i)
            Dim exp As Expression = arg.Expression
            Dim newExp As VariableExpression
            Dim varDecl As LocalVariableDeclaration
            Dim stmt As AssignmentStatement

            varDecl = New LocalVariableDeclaration(arg)
            varDecl.Init(Nothing, "VB$tmp", exp.ExpressionType)
            block.AddVariable(varDecl)

            newExp = New VariableExpression(arg, varDecl)

            stmt = New AssignmentStatement(Me.Parent)
            stmt.Init(newExp, exp)
            block.AddStatementBefore(stmt, Me)

            arg.Expression = newExp
        Next

        If InvocationExpression.Classification.IsVariableClassification Then
            result = CheckIndexedStatement(Info, TryCast(InvocationExpression.Classification.AsVariableClassification.ArrayVariable, InvocationOrIndexExpression)) AndAlso result
        End If

        Return result
    End Function

    Public NotOverridable Overrides Function ResolveStatement(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveStatement(Info) AndAlso result

        If result = False Then Return result

        result = CheckIndexedStatement(Info, TryCast(LSide, InvocationOrIndexExpression)) AndAlso result

        m_CompoundExpression = ResolveStatement(LSide, RSide)

        result = m_CompoundExpression.ResolveExpression(Info) AndAlso result

        m_CompoundExpression = Helper.CreateTypeConversion(Me, m_CompoundExpression, LSide.ExpressionType, result)

        Return result
    End Function

    Friend NotOverridable Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        'result = m_CompoundExpression.GenerateCode(Info.Clone(True, False, LSide.ExpressionType)) AndAlso result

        Dim lInfo As EmitInfo = Info.Clone(Me, m_CompoundExpression)
        result = LSide.Classification.GenerateCode(lInfo) AndAlso result

        Return result
    End Function

    Public Overrides Function CreateTypeConversion() As Boolean
        Dim result As Boolean = True

        Return result
    End Function
End Class
