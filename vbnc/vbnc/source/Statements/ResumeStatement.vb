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
Public Class ResumeStatement
    Inherits Statement

    Private m_IsResumeNext As Boolean
    Private m_TargetLabel As Token?
    Private m_TargetLocation As Span?
    Private m_TargetLabelDeclaration As LabelDeclarationStatement

    Sub New(ByVal Parent As ParsedObject, ByVal IsResumeNext As Boolean, TargetLabel As Token?, TargetLocation As Span?)
        MyBase.New(Parent)
        m_IsResumeNext = IsResumeNext
        m_TargetLabel = TargetLabel
        m_TargetLocation = TargetLocation
    End Sub

    Public Overrides Function ResolveTypeReferences() As Boolean
        Return True
    End Function

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Dim ResumeOK As Label = Emitter.DefineLabel(Info)

        Dim block As CodeBlock = Me.FindFirstParent(Of CodeBlock)()
        Dim lastblock As CodeBlock = block
        Do Until lastblock Is Nothing
            block = lastblock
            lastblock = block.FindFirstParent(Of CodeBlock)()
        Loop

        'Clear the error.
        Emitter.EmitCall(Info, Compiler.TypeCache.MS_VB_CS_ProjectData__ClearProjectError)

        'Test if the code is in an exception handler
        Emitter.EmitLoadVariable(Info, block.VB_ResumeTarget)
        Emitter.EmitBranchIfTrue(Info, ResumeOK)

        'If code is not in an exception handler raise an error
        Emitter.EmitLoadI4Value(Info, -2146828268)
        Emitter.EmitCall(Info, Compiler.TypeCache.MS_VB_CS_ProjectData__CreateProjectError_Int32)
        Emitter.EmitThrow(Info)

        Emitter.MarkLabel(Info, ResumeOK)
        If m_TargetLabelDeclaration IsNot Nothing Then
            Emitter.EmitBranchOrLeave(Info, m_TargetLabelDeclaration.GetLabel(Info), Me, m_TargetLabelDeclaration)
        Else
            'Load the instruction switch index
            Emitter.EmitLoadVariable(Info, block.VB_CurrentInstruction)
            'Increment the instruction pointer if it is a Resume Next statement
            If m_IsResumeNext Then
                Emitter.EmitLoadI4Value(Info, 1)
                Emitter.EmitAdd(Info, Compiler.TypeCache.System_Int32)
            End If
            'If everything is ok, jump to the instruction switch (adding one to the instruction if necessary)
            Emitter.EmitLeave(Info, block.UnstructuredResumeHandler)
        End If

        Return result
    End Function

    Public Overrides Function ResolveStatement(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        Compiler.Helper.AddCheck("Resume statement can only occur in methods with no structured exception handling.")

        Dim block As CodeBlock = Me.FindFirstParent(Of CodeBlock)()
        block.HasUnstructuredExceptionHandling = True
        block.HasResume = True

        If m_TargetLabel.HasValue Then
            m_TargetLabelDeclaration = FindFirstParent(Of CodeBlock)().FindLabel(m_TargetLabel.Value)
            If m_TargetLabelDeclaration Is Nothing Then
                result = Report.ShowMessage(Messages.VBNC30132, m_TargetLocation.Value, m_TargetLabel.Value.Identifier) AndAlso result
            End If
        End If

        Return True
    End Function
End Class

