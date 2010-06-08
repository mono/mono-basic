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
''' LabelDeclarationStatement  ::=  LabelName  ":"
''' LabelName  ::=  Identifier  |  IntLiteral
''' </summary>
''' <remarks></remarks>
Public Class LabelDeclarationStatement
    Inherits Statement

    ''' <summary>
    ''' LabelName ::= Identifier | IntLiteral
    ''' </summary>
    ''' <remarks></remarks>
    Private m_Label As Token

    Private m_LabelBuilder As Nullable(Of Label)

    Public Overrides Function ResolveTypeReferences() As Boolean
        Return True
    End Function

    Sub New(ByVal Parent As ParsedObject, ByVal Label As Token)
        MyBase.New(Parent)
        m_Label = Label
    End Sub

    Shared Function CanBeMe(ByVal tm As tm) As Boolean
        Return tm.PeekToken = KS.Colon AndAlso (tm.CurrentToken.IsIntegerLiteral OrElse tm.CurrentToken.IsIdentifier)
    End Function

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Emitter.MarkLabel(Info, GetLabel(Info))

        Return result
    End Function

    Public Overrides Function ResolveStatement(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        Compiler.Helper.AddCheck("Check if the label name is unique.")

        Return result
    End Function

    Function GetLabel(ByVal Info As EmitInfo) As Label
        If m_LabelBuilder.HasValue = False Then
            m_LabelBuilder = Emitter.DefineLabel(Info)
        End If
        Return m_LabelBuilder.Value
    End Function

    ReadOnly Property Label() As Token
        Get
            Return m_Label
        End Get
    End Property
End Class
