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
''' GotoStatement  ::=  "GoTo" LabelName  StatementTerminator
''' LabelName ::= Identifier | IntLiteral
''' </summary>
''' <remarks></remarks>
Public Class GotoStatement
    Inherits Statement

    Private m_GotoWhere As Token

    Private m_Destination As LabelDeclarationStatement

    Public Overrides Function ResolveTypeReferences() As Boolean
        Return True
    End Function

    Sub New(ByVal Parent As ParsedObject, ByVal GotoWhere As Token)
        MyBase.New(Parent)
        m_GotoWhere = GotoWhere
    End Sub

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Emitter.EmitBranchOrLeave(Info, m_Destination.GetLabel(Info), Me, m_Destination)

        Return result
    End Function

    Public Overrides Function ResolveStatement(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        m_Destination = Me.FindFirstParent(Of CodeBlock).FindLabel(m_GotoWhere)
        If m_Destination Is Nothing Then
            result = false
            Report.ShowMessage(Messages.VBNC30132, Me.Location, m_GotoWhere.Identifier)
        End If

        Return result
    End Function

    ReadOnly Property GotoWhere() As Token
        Get
            Return m_GotoWhere
        End Get
    End Property
End Class

