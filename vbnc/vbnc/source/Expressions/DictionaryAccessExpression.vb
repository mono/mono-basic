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
''' DictionaryAccessExpression ::= [Expression] "!" IdentifierOrKeyword
''' </summary>
''' <remarks></remarks>
Public Class DictionaryAccessExpression
    Inherits Expression

    ''' <summary>
    ''' The first part may be nothing in a with block.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_FirstPart As Expression
    Private m_SecondPart As IdentifierOrKeyword

    Private m_DefaultProperty As PropertyInfo
    Private m_WithStatement As WithStatement

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal FirstPart As Expression, ByVal SecondPart As IdentifierOrKeyword)
        m_FirstPart = FirstPart
        m_SecondPart = SecondPart
    End Sub
    
    Overrides ReadOnly Property ExpressionType() As Type
        Get
            Return m_DefaultProperty.PropertyType
        End Get
    End Property

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        If m_FirstPart IsNot Nothing Then
            result = m_FirstPart.GenerateCode(Info) AndAlso result
        Else
            Emitter.EmitLoadVariable(Info, m_WithStatement.WithVariable)
        End If
        Emitter.EmitLoadValue(Info, m_SecondPart.Identifier)
        If Info.IsRHS Then
            Emitter.EmitCallOrCallVirt(Info, m_DefaultProperty.GetGetMethod)
        ElseIf Info.IsLHS Then
            Helper.NotImplemented()
        Else
            Throw New InternalException(Me)
        End If

        Return result
    End Function

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        Dim firsttp As Type
        If m_FirstPart IsNot Nothing Then
            result = m_FirstPart.ResolveExpression(Info) AndAlso result
            firsttp = m_FirstPart.ExpressionType
        Else
            m_WithStatement = Me.FindFirstParent(Of WithStatement)()
            firsttp = m_WithStatement.WithExpression.ExpressionType
        End If


        Dim attr As Object() = firsttp.GetCustomAttributes(Compiler.TypeCache.DefaultMemberAttribute, True)
        If attr.Length = 1 Then
            Dim att As DefaultMemberAttribute = TryCast(attr(0), DefaultMemberAttribute)
            Helper.Assert(att IsNot Nothing)
            m_DefaultProperty = firsttp.GetProperty(att.MemberName)
            If m_DefaultProperty IsNot Nothing Then
                Classification = New ValueClassification(Me, m_DefaultProperty.PropertyType)
            Else
                Helper.AddError()
            End If
        ElseIf attr.Length > 1 Then
            Helper.NotImplemented()
        Else
            Helper.NotImplemented()
        End If

        Return result
    End Function

    Shared Function IsBinaryMe(ByVal tm As tm) As Boolean
        Return tm.CurrentToken.Equals(KS.Exclamation)
    End Function

    Shared Function IsUnaryMe(ByVal tm As tm) As Boolean
        Return tm.CurrentToken.Equals(KS.Exclamation)
    End Function

    Shared Function CreateAndParseTo(ByRef result As Expression) As Boolean
        Helper.NotImplemented()
    End Function


#If DEBUG Then
    Public Overrides Sub Dump(ByVal Dumper As IndentedTextWriter)
        If m_FirstPart IsNot Nothing Then m_FirstPart.Dump(Dumper)
        Dumper.Write("!")
        Compiler.Dumper.Dump(m_SecondPart)
    End Sub
#End If

End Class
