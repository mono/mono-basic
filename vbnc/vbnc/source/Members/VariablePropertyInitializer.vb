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
''' VariablePropertyInitializer  :: IdentifierOrKeyword  ":="  AttributeArgumentExpression
''' </summary>
''' <remarks></remarks>
Public Class VariablePropertyInitializer
    Inherits ParsedObject

    Private m_Identifier As String
    Private m_IdentifierOrKeyword As IdentifierOrKeyword
    Private m_AttributeArgumentExpression As AttributeArgumentExpression

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = m_AttributeArgumentExpression.ResolveCode(Info) AndAlso result

        Return result
    End Function

    ReadOnly Property Identifier() As String
        Get
            If m_Identifier IsNot Nothing Then
                Return m_Identifier
            End If
            Return m_IdentifierOrKeyword.Identifier
        End Get
    End Property

    ReadOnly Property AttributeArgumentExpression() As AttributeArgumentExpression
        Get
            Return m_AttributeArgumentExpression
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal Identifier As String, ByVal AttributeArgumentExpression As AttributeArgumentExpression)
        m_Identifier = Identifier
        m_AttributeArgumentExpression = AttributeArgumentExpression
    End Sub

    Sub Init(ByVal IdentifierOrKeyword As IdentifierOrKeyword, ByVal AttributeArgumentExpression As AttributeArgumentExpression)
        m_IdentifierOrKeyword = IdentifierOrKeyword
        m_AttributeArgumentExpression = AttributeArgumentExpression
    End Sub

    Shared Function IsMe(ByVal tm As tm) As Boolean
        Return tm.CurrentToken.IsIdentifierOrKeyword AndAlso tm.PeekToken.Equals(KS.Colon) AndAlso tm.PeekToken(2).Equals(KS.Equals)
    End Function
End Class
