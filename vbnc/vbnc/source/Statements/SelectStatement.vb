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
''' SelectStatement  ::=
'''	   "Select" [ "Case" ]  Expression  StatementTerminator
'''	        [  CaseStatement+  ]
'''	        [  CaseElseStatement  ]
'''	   "End" "Select" StatementTerminator
''' </summary>
''' <remarks></remarks>
Public Class SelectStatement
    Inherits Statement

    Private m_Test As Expression
    Private m_CachedTest As CachedExpression
    Private m_Cases As BaseObjects(Of CaseStatement)

    Public EndLabel As Label

    ReadOnly Property CachedTest() As Expression
        Get
            Return m_cachedtest
        End Get
    End Property

    ReadOnly Property Test() As Expression
        Get
            Return m_Test
        End Get
    End Property

    ReadOnly Property Cases() As BaseObjects(Of CaseStatement)
        Get
            Return m_Cases
        End Get
    End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        If m_Test IsNot Nothing Then result = m_Test.ResolveTypeReferences AndAlso result
        If m_Cases IsNot Nothing Then result = m_Cases.ResolveTypeReferences AndAlso result

        Return result
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal Test As Expression, ByVal Cases As BaseObjects(Of CaseStatement))
        m_Test = Test
        m_Cases = Cases
    End Sub

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        EndLabel = Emitter.DefineLabel(Info)

        For i As Integer = 0 To m_Cases.Count - 1
            Dim stmt As CaseStatement = m_Cases(i)
            result = stmt.GenerateCode(Info) AndAlso result
        Next

        Emitter.MarkLabel(Info, EndLabel)

        Return result
    End Function

    Public Overrides Function ResolveStatement(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = m_Test.ResolveExpression(Info) AndAlso result
        If result = False Then Return False

        result = Helper.VerifyValueClassification(m_Test, Info) AndAlso result
        If result = False Then Return False

        m_CachedTest = New CachedExpression(m_Test, m_Test)

        result = m_Cases.ResolveCode(Info) AndAlso result

        For i As Integer = 0 To m_Cases.Count - 1
            Dim aCase As CaseStatement = m_Cases(i)
            If aCase.IsElse AndAlso i < m_Cases.Count - 1 Then
                result = Report.ShowMessage(Messages.VBNC30321, m_Cases(i + 1).Location) AndAlso result
                Exit For
            End If
        Next

        Return result
    End Function

End Class

