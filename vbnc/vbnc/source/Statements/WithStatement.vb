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
''' WithStatement  ::=
'''	   "With" Expression  StatementTerminator
'''	        [  Block  ]
'''	   "End" "With" StatementTerminator
''' </summary>
''' <remarks></remarks>
Public Class WithStatement
    Inherits BlockStatement

    Private m_WithExpression As Expression

    Private m_WithVariable As LocalBuilder

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        result = Helper.ResolveTypeReferences(m_WithExpression) AndAlso result
        result = MyBase.ResolveTypeReferences() AndAlso result

        Return result
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Shadows Sub Init(ByVal Code As CodeBlock, ByVal WithExpression As Expression)
        MyBase.Init(Code)
        m_WithExpression = WithExpression
    End Sub

    ReadOnly Property WithVariable() As LocalBuilder
        Get
            Return m_WithVariable
        End Get
    End Property

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        m_WithVariable = Info.ILGen.DeclareLocal(Helper.GetTypeOrTypeBuilder(m_WithExpression.ExpressionType))
        result = m_WithExpression.GenerateCode(Info.Clone(True, False, m_WithVariable.LocalType)) AndAlso result
        Emitter.EmitStoreVariable(Info, m_WithVariable)

        result = CodeBlock.GenerateCode(Info) AndAlso result

        Return result
    End Function

    ReadOnly Property WithExpression() As Expression
        Get
            Return m_WithExpression
        End Get
    End Property

    Public Overrides Function ResolveStatement(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = m_WithExpression.ResolveExpression(Info) AndAlso result
        result = CodeBlock.ResolveCode(Info) AndAlso result

        Return result
    End Function
End Class
