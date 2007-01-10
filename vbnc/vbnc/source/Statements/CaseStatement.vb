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
''' CaseStatement  ::=
'''	   "Case" CaseClauses  StatementTerminator
'''	        [  Block  ]
''' CaseElseStatement  ::=
'''	   "Case" "Else" StatementTerminator
'''	   [  Block  ]
''' </summary>
''' <remarks></remarks>
Public Class CaseStatement
    Inherits BlockStatement

    Private m_IsElse As Boolean
    Private m_Clauses As CaseClauses
    Private m_StartCode As Label

    ReadOnly Property StartCode() As Label
        Get
            Return m_StartCode
        End Get
    End Property

    ReadOnly Property IsElse() As Boolean
        Get
            Return m_iselse
        End Get
    End Property

    ReadOnly Property Clauses() As CaseClauses
        Get
            Return m_Clauses
        End Get
    End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        If m_Clauses IsNot Nothing Then result = m_Clauses.ResolveTypeReferences AndAlso result

        result = MyBase.ResolveTypeReferences AndAlso result

        Return result
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Shadows Sub Init(ByVal IsElse As Boolean, ByVal Clauses As CaseClauses, ByVal Block As CodeBlock)
        MyBase.Init(Block)
        m_IsElse = IsElse
        m_Clauses = Clauses
    End Sub

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Dim selectparent As SelectStatement = Me.FindFirstParent(Of SelectStatement)()

        EndLabel = Info.ILGen.DefineLabel
        m_StartCode = Info.ILGen.DefineLabel

        If m_IsElse = False Then
            For i As Integer = 0 To m_Clauses.Count - 1
                Dim clause As CaseClause = m_Clauses.Item(i)
                result = clause.GenerateCode(Info) AndAlso result
            Next
            Emitter.EmitBranch(Info, EndLabel)
        End If
        Info.ILGen.MarkLabel(m_StartCode)
        result = CodeBlock.GenerateCode(Info) AndAlso result
        Emitter.EmitBranch(Info, selectparent.EndLabel)
        Info.ILGen.MarkLabel(EndLabel)

        Return result
    End Function

    Public Overrides Function ResolveStatement(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        If m_Clauses IsNot Nothing Then
            result = m_Clauses.ResolveStatements(Info) AndAlso result
        End If
        result = CodeBlock.ResolveCode(Info) AndAlso result

        Return result
    End Function

    Function ParentAsSelectStatement() As SelectStatement
        Helper.Assert(TypeOf Me.Parent Is SelectStatement)
        Return DirectCast(Me.Parent, SelectStatement)
    End Function
End Class
