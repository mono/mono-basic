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

Imports System.Reflection
Imports System.Reflection.Emit

''' <summary>
''' NewExpression ::= ObjectCreationExpression | ArrayCreationExpression | DelegateCreationExpression
''' </summary>
''' <remarks></remarks>
Public Class NewExpression
    Inherits Expression

    Private m_Expression As Expression

    Public Overrides Function ResolveTypeReferences() As Boolean
        Return m_Expression.ResolveTypeReferences
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal Expression As Expression)
        m_Expression = Expression
    End Sub

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        result = m_Expression.GenerateCode(Info) AndAlso result 'Helper.NotImplemented()

        Return result
    End Function

    Shared Function IsMe(ByVal tm As tm) As Boolean
        Return tm.CurrentToken.Equals(KS.[New])
    End Function

    Shared Function CreateAndParseTo(ByRef result As Expression) As Boolean
        Return result.Compiler.Report.ShowMessage(Messages.VBNC99997, result.Location)
    End Function

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = m_Expression.ResolveExpression(info) AndAlso result
        Classification = m_Expression.Classification

        Return result
    End Function

    ReadOnly Property IsArrayCreationExpression() As Boolean
        Get
            Return TypeOf m_Expression Is ArrayCreationExpression
        End Get
    End Property

    Overrides ReadOnly Property ExpressionType() As Type
        Get
            Return m_Expression.ExpressionType
        End Get
    End Property


#If DEBUG Then
    Public Overrides Sub Dump(ByVal Dumper As IndentedTextWriter)
        m_Expression.Dump(Dumper)
    End Sub
#End If
End Class
