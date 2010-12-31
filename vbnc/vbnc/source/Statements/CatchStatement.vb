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
''' CatchStatement  ::=
'''	   "Catch" [  Identifier "As" NonArrayTypeName  ]  [ "When" BooleanExpression  ]  StatementTerminator
'''	      [  Block  ]
''' </summary>
''' <remarks></remarks>
Public Class CatchStatement
    Inherits BlockStatement

    Private m_Variable As Identifier
    Private m_TypeName As NonArrayTypeName
    Private m_When As Expression

    Private m_ExceptionType As Mono.Cecil.TypeReference

    Private m_VariableDeclaration As LocalVariableDeclaration

    ReadOnly Property Variable() As Identifier
        Get
            Return m_Variable
        End Get
    End Property

    ReadOnly Property TypeName() As NonArrayTypeName
        Get
            Return m_TypeName
        End Get
    End Property

    ReadOnly Property [When]() As Expression
        Get
            Return m_When
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Shadows Sub Init(ByVal Variable As Identifier, ByVal TypeName As NonArrayTypeName, ByVal [When] As Expression, ByVal Block As CodeBlock)
        MyBase.Init(Block)

        m_Variable = Variable
        m_TypeName = TypeName
        m_When = [When]
    End Sub

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        If m_When IsNot Nothing Then
            Dim EndWhen, DoWhenComparison As Label
            EndWhen = Emitter.DefineLabel(Info)
            DoWhenComparison = Emitter.DefineLabel(Info)

            Emitter.EmitBeginExceptionFilter(Info)
            'Check if the exception object is of type System.Exception.
            Emitter.EmitIsInst(Info, Compiler.TypeCache.System_Object, Compiler.TypeCache.System_Exception)
            Emitter.EmitDup(Info)
            'If True, do the comparison
            Emitter.EmitBranchIfTrue(Info, DoWhenComparison, Compiler.TypeCache.System_Exception)
            'Otherwise load a false value and go to the end of the filter.
            Emitter.EmitPop(Info, Compiler.TypeCache.System_Exception)
            Emitter.EmitLoadValue(Info, False)
            Emitter.EmitBranch(Info, EndWhen)

            'Do the when clause.
            Emitter.MarkLabel(Info, DoWhenComparison)
            Emitter.EmitPop(Info, Compiler.TypeCache.System_Exception)
            result = m_When.GenerateCode(Info.Clone(Me, True, False, Compiler.TypeCache.System_Boolean)) AndAlso result
            Emitter.MarkLabel(Info, EndWhen)
            Emitter.EmitBeginCatch(Info, Nothing)
        Else
            Helper.Assert(m_ExceptionType IsNot Nothing)
            Emitter.EmitBeginCatch(Info, m_ExceptionType)
        End If

        If m_VariableDeclaration Is Nothing Then
            If m_ExceptionType Is Nothing Then
                Emitter.EmitPop(Info, Compiler.TypeCache.System_Object)
            Else
                Emitter.EmitPop(Info, m_ExceptionType)
            End If
        Else
            result = m_VariableDeclaration.GenerateCode(Info) AndAlso result
            Emitter.EmitIsInst(Info, Compiler.TypeCache.System_Object, m_ExceptionType)
            Emitter.EmitStoreVariable(Info, m_VariableDeclaration.LocalBuilder)
        End If
        result = CodeBlock.GenerateCode(Info) AndAlso result


        Return result
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        If m_TypeName IsNot Nothing Then
            result = m_TypeName.ResolveTypeReferences AndAlso result
        End If

        If m_When IsNot Nothing Then
            result = m_When.ResolveTypeReferences AndAlso result
        End If

        result = MyBase.ResolveTypeReferences AndAlso result

        Return result
    End Function

    Public Overrides Function ResolveStatement(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        If m_TypeName IsNot Nothing Then
            m_ExceptionType = m_TypeName.ResolvedType
            Helper.Assert(m_ExceptionType IsNot Nothing)
            If Helper.CompareType(Compiler.TypeCache.System_Exception, m_ExceptionType) = False AndAlso Helper.IsSubclassOf(Compiler.TypeCache.System_Exception, m_ExceptionType) = False Then
                Helper.AddError(Me, "Exception type does not inherit from System.Exception")
                result = True
            End If
        ElseIf m_When Is Nothing Then
            m_ExceptionType = Compiler.TypeCache.System_Exception
        End If
        If m_Variable IsNot Nothing Then 'Token.IsSomething(m_Variable) Then
            m_VariableDeclaration = New LocalVariableDeclaration(Me, m_Variable, False, m_TypeName, Nothing, Nothing)
            result = m_VariableDeclaration.ResolveTypeReferences AndAlso result
            CodeBlock.Variables.Add(m_VariableDeclaration)
        End If
        If m_When IsNot Nothing Then result = m_When.ResolveExpression(Info) AndAlso result
        result = CodeBlock.ResolveCode(Info) AndAlso result

        If m_When IsNot Nothing Then
            m_When = Helper.CreateTypeConversion(Me, m_When, Compiler.TypeCache.System_Boolean, result)
            If result = False Then
                Helper.AddError(Me)
                Return result
            End If
        End If

        Return result
    End Function

    Function ParentAsTryStatement() As TryStatement
        Helper.Assert(TypeOf Me.Parent Is TryStatement)
        Return DirectCast(Me.Parent, TryStatement)
    End Function
End Class
