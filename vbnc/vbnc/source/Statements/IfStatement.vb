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
''' IfStatement  ::=  BlockIfStatement  |  LineIfThenStatement
''' BlockIfStatement  ::=
'''	   "If" BooleanExpression  [ "Then" ]  StatementTerminator
'''	        [  Block  ]
'''	   [  ElseIfStatement+  ]
'''	   [  ElseStatement  ]
'''	   "End" "If" StatementTerminator
''' ElseIfStatement  ::=
'''	   "ElseIf" BooleanExpression  [ "Then" ]  StatementTerminator
'''	        [  Block  ]
''' ElseStatement  ::=
'''	   "Else" StatementTerminator
'''	        [  Block  ]
''' LineIfThenStatement  ::=
'''	   "If" BooleanExpression "Then" Statements  [ "Else" Statements  ]  StatementTerminator
''' </summary>
''' <remarks></remarks>
Public Class IfStatement
    Inherits BlockStatement

    Private m_Condition As Expression
    'Private m_TrueCode As CodeBlock 'Provided by base class' m_Code
    Private m_FalseCode As CodeBlock
    Private m_ElseIfs As BaseObjects(Of ElseIfStatement)

    ''' <summary>
    ''' Is this a one line statement?
    ''' </summary>
    ''' <remarks></remarks>
    Private m_OneLiner As Boolean

    ReadOnly Property Condition() As Expression
        Get
            Return m_Condition
        End Get
    End Property

    ReadOnly Property FalseCode() As CodeBlock
        Get
            Return m_FalseCode
        End Get
    End Property

    ReadOnly Property ElseIfs() As BaseObjects(Of ElseIfStatement)
        Get
            Return m_ElseIfs
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Shadows Sub Init(ByVal Condition As Expression, ByVal FalseCode As CodeBlock, ByVal TrueCode As CodeBlock, ByVal OneLiner As Boolean, ByVal ElseIfs As BaseObjects(Of ElseIfStatement))
        MyBase.Init(TrueCode)

        m_Condition = Condition
        m_FalseCode = FalseCode
        m_ElseIfs = ElseIfs
        m_OneLiner = OneLiner
    End Sub

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Dim startFalse As Label = Emitter.DefineLabel(Info)
        EndLabel = Emitter.DefineLabel(Info)

        result = m_Condition.GenerateCode(Info.Clone(Me, True, False, Compiler.TypeCache.System_Boolean)) AndAlso result

        Emitter.EmitBranchIfFalse(Info, startFalse)
        'True code
        result = CodeBlock.GenerateCode(Info) AndAlso result
        Emitter.EmitBranch(Info, EndLabel)

        'False code
        Emitter.MarkLabel(Info, startFalse)
        If m_ElseIfs IsNot Nothing Then
            For Each eif As ElseIfStatement In m_ElseIfs
                result = eif.GenerateCode(Info) AndAlso result
            Next
        End If

        If m_FalseCode IsNot Nothing Then
            result = m_FalseCode.GenerateCode(Info) AndAlso result
        End If
        Emitter.MarkLabel(Info, EndLabel)


        Return result
    End Function

    Public Overrides Function ResolveStatement(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = m_Condition.ResolveExpression(Info) AndAlso result
        result = CodeBlock.ResolveCode(Info) AndAlso result
        If m_FalseCode IsNot Nothing Then result = m_FalseCode.ResolveCode(Info) AndAlso result
        If m_ElseIfs IsNot Nothing Then result = m_ElseIfs.ResolveCode(Info) AndAlso result

        If result = False Then Return result

        If m_Condition.Classification.IsValueClassification Then
            'nothing to do
        ElseIf m_Condition.Classification.CanBeValueClassification Then
            m_Condition = m_Condition.ReclassifyToValueExpression
            result = m_Condition.ResolveExpression(ResolveInfo.Default(Compiler)) AndAlso result

            If result = False Then
                Helper.AddError(Me)
                Return result
            End If
        Else
            Helper.AddError(Me, "Each expression in an If...Then...Else statement must be classified as a value and be implicitly convertible to Boolean")
        End If

        m_Condition = Helper.CreateTypeConversion(Me, m_Condition, Compiler.TypeCache.System_Boolean, result)

        If result = False Then
            Helper.AddError(Me)
            Return result
        End If

        Return result
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveTypeReferences AndAlso result

        If m_Condition IsNot Nothing Then result = m_Condition.ResolveTypeReferences AndAlso result
        If m_ElseIfs IsNot Nothing Then result = m_ElseIfs.ResolveTypeReferences AndAlso result
        If m_FalseCode IsNot Nothing Then result = m_FalseCode.ResolveTypeReferences AndAlso result

        Return result
    End Function

    Overrides ReadOnly Property IsOneLiner() As Boolean
        Get
            Return m_OneLiner OrElse MyBase.IsOneLiner
        End Get
    End Property
End Class
