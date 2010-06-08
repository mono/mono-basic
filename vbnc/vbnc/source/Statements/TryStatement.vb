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
'''TryStatement  ::=
'''	"Try" StatementTerminator
'''	   [  Block  ]
'''	[  CatchStatement+  ]
'''	[  FinallyStatement  ]
'''	"End" "Try" StatementTerminator
''' </summary>
''' <remarks></remarks>
Public Class TryStatement
    Inherits BlockStatement

    Private m_Catches As BaseObjects(Of CatchStatement)

    Private m_FinallyBlock As CodeBlock

    ReadOnly Property FinallyBlock() As CodeBlock
        Get
            Return m_finallyblock
        End Get
    End Property

    ReadOnly Property Catches() As BaseObjects(Of CatchStatement)
        Get
            Return m_Catches
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Shadows Sub Init(ByVal Catches As BaseObjects(Of CatchStatement), ByVal TryBlock As CodeBlock, ByVal FinallyBlock As CodeBlock)
        MyBase.Init(TryBlock)
        m_Catches = Catches
        m_FinallyBlock = FinallyBlock
    End Sub

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        EndLabel = Emitter.EmitBeginExceptionBlock(Info)

        result = CodeBlock.GenerateCode(Info) AndAlso result

        For i As Integer = 0 To m_Catches.Count - 1
            Dim catchstmt As CatchStatement = m_Catches(i)
            result = catchstmt.GenerateCode(Info) AndAlso result
        Next

        If m_FinallyBlock IsNot Nothing Then
            Info.ILGen.BeginFinallyBlock()
            result = m_FinallyBlock.GenerateCode(Info) AndAlso result
        End If

        Info.ILGen.EndExceptionBlock()

        Return result
    End Function

    Public Overrides Function ResolveStatement(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = m_Catches.ResolveCode(Info) AndAlso result
        If m_FinallyBlock IsNot Nothing Then result = m_FinallyBlock.ResolveCode(Info) AndAlso result
        result = CodeBlock.ResolveCode(Info) AndAlso result

        Return result
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        If m_Catches IsNot Nothing Then result = m_Catches.ResolveTypeReferences() AndAlso result
        If m_FinallyBlock IsNot Nothing Then result = m_FinallyBlock.ResolveTypeReferences AndAlso result

        result = MyBase.ResolveTypeReferences AndAlso result

        Return result
    End Function
End Class
