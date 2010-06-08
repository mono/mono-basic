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
''' OnErrorStatement  ::=  "On" "Error" ErrorClause  StatementTerminator
''' ErrorClause  ::=
'''	   "GoTo"  "-"  "1" |
'''	   "GoTo"  "0"  |
'''	   GotoStatement  |
'''	   "Resume" "Next"
''' </summary>
''' <remarks></remarks>
Public Class OnErrorStatement
    Inherits Statement

    Private m_IsResumeNext As Boolean
    Private m_Label As Token
    Private m_IsGotoMinusOne As Boolean
    Private m_IsGotoZero As Boolean

    Private m_ResolvedLabel As LabelDeclarationStatement

    Sub New(ByVal Parent As ParsedObject, ByVal IsResumeNext As Boolean, ByVal Label As Token, ByVal IsGotoMinusOne As Boolean, ByVal IsGotoZero As Boolean)
        MyBase.New(Parent)
        m_IsResumeNext = IsResumeNext
        m_Label = Label
        m_IsGotoMinusOne = IsGotoMinusOne
        m_IsGotoZero = IsGotoZero
    End Sub

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        If m_ResolvedLabel IsNot Nothing Then result = m_ResolvedLabel.ResolveTypeReferences() AndAlso result

        Return result
    End Function

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Dim parent As CodeBlock = Me.FindFirstParent(Of CodeBlock)()
        Dim lastparent As CodeBlock = parent.FindFirstParent(Of CodeBlock)()
        Do Until lastparent Is Nothing
            parent = lastparent
            lastparent = parent.FindFirstParent(Of CodeBlock)()
        Loop

        If m_IsGotoMinusOne Then
            '•	On Error GoTo -1 resets the most recent exception to Nothing.
            Emitter.EmitCall(Info, Compiler.TypeCache.MS_VB_CS_ProjectData__ClearProjectError)
        ElseIf m_IsGotoZero Then
            '•	On Error GoTo 0 resets the most recent exception-handler location to Nothing.
            Emitter.EmitCall(Info, Compiler.TypeCache.MS_VB_CS_ProjectData__ClearProjectError)
            Emitter.EmitLoadI4Value(Info, 0)
            Emitter.EmitStoreVariable(Info, parent.VB_ActiveHandler)
        ElseIf m_IsResumeNext Then
            Emitter.EmitCall(Info, Compiler.TypeCache.MS_VB_CS_ProjectData__ClearProjectError)
            '•	On Error Resume Next, establishes the Resume Next behavior as the most recent exception-handler location.
            Emitter.EmitLoadI4Value(Info, parent.UnstructuredExceptionHandlers.IndexOf(parent.UnstructuredResumeNextHandler)) 'Load the index of the switch table, 1 = resume next handler.
            Emitter.EmitStoreVariable(Info, parent.VB_ActiveHandler)
        Else
            Emitter.EmitCall(Info, Compiler.TypeCache.MS_VB_CS_ProjectData__ClearProjectError)
            Dim index As Integer
            If parent.UnstructuredExceptionHandlers.Contains(m_ResolvedLabel.GetLabel(Info)) = False Then
                parent.UnstructuredExceptionHandlers.Add(m_ResolvedLabel.GetLabel(Info))
            End If
            index = parent.UnstructuredExceptionHandlers.IndexOf(m_ResolvedLabel.GetLabel(Info))
            Emitter.EmitLoadI4Value(Info, index)
            Emitter.EmitStoreVariable(Info, parent.VB_ActiveHandler)
        End If

        Return result
    End Function

    Public Overrides Function ResolveStatement(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        Dim block As CodeBlock = Me.FindFirstParent(Of CodeBlock)()
        block.HasUnstructuredExceptionHandling = True
        If m_IsResumeNext Then block.HasResume = True
        If block.HasStructuredExceptionHandling Then
            Helper.AddError(Me, "No structured exception handling in the same method.")
        End If

        If Token.IsSomething(m_Label) Then
            block = Me.FindFirstParent(Of CodeBlock)()
            m_ResolvedLabel = block.FindLabel(m_Label)
            Compiler.Helper.AddCheck("Label must exist.")
        End If

        Return result
    End Function
End Class
