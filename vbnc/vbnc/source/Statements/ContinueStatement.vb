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
'''ContinueStatement  ::=  "Continue" ContinueKind  StatementTerminator
'''ContinueKind  ::=  "Do" | "For" | "While"
''' </summary>
''' <remarks></remarks>
Public Class ContinueStatement
    Inherits Statement

    Private m_ContinueWhat As KS

    Private m_ContainingStatement As Statement

    Public Overrides Function ResolveTypeReferences() As Boolean
        Return True
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal ContinueWhat As KS)
        m_ContinueWhat = ContinueWhat
    End Sub

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True
        Dim nextIteration As Nullable(Of Label)
        Select Case m_ContinueWhat
            Case KS.While
                Dim whilestmt As WhileStatement = TryCast(m_ContainingStatement, WhileStatement)
                If whilestmt IsNot Nothing Then
                    nextIteration = whilestmt.nextiteration
                Else
                    Throw New InternalException(Me)
                End If
            Case KS.Do
                Dim dostmt As DoStatement = TryCast(m_ContainingStatement, DoStatement)
                If dostmt IsNot Nothing Then
                    nextIteration = dostmt.nextiteration
                Else
                    Throw New InternalException(Me)
                End If
            Case KS.For
                Dim forstmt As ForStatement = TryCast(m_ContainingStatement, ForStatement)
                Dim foreachstmt As ForEachStatement = TryCast(m_ContainingStatement, ForEachStatement)
                If forstmt IsNot Nothing Then
                    nextIteration = forstmt.NextIteration
                ElseIf foreachstmt IsNot Nothing Then
                    nextIteration = foreachstmt.nextiteration
                Else
                    Throw New InternalException(Me)
                End If
            Case Else
                Throw New InternalException(Me)
        End Select

        Helper.Assert(nextIteration.HasValue)
        Emitter.EmitBranchOrLeave(Info, nextIteration.Value, Me, m_ContainingStatement)

        Return result
    End Function

    Public Overrides Function ResolveStatement(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        'If the Continue statement is not contained within the kind of block specified in the statement, a compile-time error occurs.
        Select Case m_ContinueWhat
            Case KS.While
                m_ContainingStatement = Me.FindFirstParent(Of WhileStatement)()
                If m_ContainingStatement Is Nothing Then
                    'Helper.AddCheck("error BC30784: 'Continue While' can only appear inside a 'While' statement.")
                    Compiler.Report.ShowMessage(Messages.VBNC30784, Location)
                End If
            Case KS.For
                m_ContainingStatement = CType(Me.FindFirstParent(Of ForEachStatement, ForStatement)(), Statement)
                If m_ContainingStatement Is Nothing Then
                    'Helper.AddError("error BC30783: 'Continue For' can only appear inside a 'For' statement.")
                    Compiler.Report.ShowMessage(Messages.VBNC30783, Location)
                End If
            Case KS.Do
                m_ContainingStatement = Me.FindFirstParent(Of DoStatement)()
                If m_ContainingStatement Is Nothing Then
                    'Helper.AddCheck("error BC30782: 'Continue Do' can only appear inside a 'Do' statement.")
                    Compiler.Report.ShowMessage(Messages.VBNC30782, Location)
                End If
            Case KS.Else
                Throw New InternalException(Me)
        End Select
        result = m_ContainingStatement IsNot Nothing


        Return result
    End Function

    ReadOnly Property ContinueWhat() As KS
        Get
            Return m_ContinueWhat
        End Get
    End Property
End Class
