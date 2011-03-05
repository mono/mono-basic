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
Public MustInherit Class Argument
    Inherits ParsedObject

    ''' <summary>
    ''' Expression might very well be nothing.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_Expression As Expression

    Overridable ReadOnly Property IsNamedArgument() As Boolean
        Get
            Return False
        End Get
    End Property

    Overridable ReadOnly Property AsString() As String
        Get
            If m_Expression Is Nothing Then
                Return "Nothing"
            Else
                Return m_Expression.AsString
            End If
        End Get
    End Property

    Overridable ReadOnly Property AsTypeString() As String
        Get
            If m_Expression Is Nothing Then
                Return "Nothing"
            Else
                If m_Expression.ExpressionType Is Nothing Then
                    Return "(Nothing)"
                Else
                    If m_Expression.ExpressionType.FullName = "" Then
                        Return m_Expression.ExpressionType.Name
                    Else
                        Return m_Expression.ExpressionType.FullName
                    End If
                End If
            End If
        End Get
    End Property

    Protected Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal Expression As Expression)
        m_Expression = Expression
    End Sub

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Return m_Expression.GenerateCode(Info)
    End Function

    Overloads Function GenerateCode(ByVal Info As EmitInfo, ByVal Destination As Mono.Cecil.ParameterDefinition) As Boolean
        Dim result As Boolean = True

        If m_Expression IsNot Nothing Then
            result = m_Expression.GenerateCode(Info) AndAlso result
        Else
            Helper.Assert(Destination IsNot Nothing)
            Helper.Assert(Destination.IsOptional)
            Emitter.EmitLoadValue(Info, Destination.Constant)
        End If

        Return result
    End Function

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        If m_Expression IsNot Nothing Then
            result = m_Expression.ResolveExpression(Info) AndAlso result

            If result = False Then Return False

            If m_Expression.Classification Is Nothing Then
                Return Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
            End If

            If result AndAlso m_Expression.Classification.IsMethodGroupClassification Then
                m_Expression = m_Expression.ReclassifyToValueExpression
                result = m_Expression.ResolveExpression(Info) AndAlso result
            ElseIf result AndAlso m_Expression.Classification.IsPropertyGroupClassification Then
                m_Expression = m_Expression.ReclassifyToPropertyAccessExpression
                result = m_Expression.ResolveExpression(Info) AndAlso result
            End If
        End If

        Return result
    End Function

    Property Expression() As Expression
        Get
            Return m_Expression
        End Get
        Set(ByVal value As Expression)
            m_Expression = value
        End Set
    End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Return m_Expression Is Nothing OrElse m_Expression.ResolveTypeReferences
    End Function

End Class

