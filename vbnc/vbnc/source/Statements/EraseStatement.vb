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
''' EraseStatement  ::= "Erase" EraseExpressions  StatementTerminator
''' EraseExpressions  ::=
'''	  Expression  |
'''	  EraseExpressions  ,  Expression
''' </summary>
''' <remarks></remarks>
Public Class EraseStatement
    Inherits Statement

    Private m_Targets As ExpressionList

    ReadOnly Property Targets() As ExpressionList
        Get
            Return m_targets
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal Targets As ExpressionList)
        m_Targets = Targets
    End Sub

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Dim nullExp As New NothingConstantExpression(Me)
        result = nullExp.ResolveExpression(ResolveInfo.Default(Info.Compiler)) AndAlso result
        For i As Integer = 0 To m_Targets.Count - 1
            Dim exp As Expression = DirectCast(m_Targets.Item(i), Expression)
            result = exp.GenerateCode(Info.Clone(Me, nullExp)) AndAlso result
        Next

        Return result
    End Function

    Public Overrides Function ResolveStatement(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True
        result = m_Targets.ResolveCode(info) AndAlso result
        Return result
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        result = m_Targets.ResolveTypeReferences() AndAlso result

        Return result
    End Function
End Class
