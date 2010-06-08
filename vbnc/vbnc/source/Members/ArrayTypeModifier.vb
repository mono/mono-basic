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
''' ArrayTypeModifier  ::=  "("  [  RankList  ]  ")"
''' RankList  ::= ","  | RankList  ","
''' </summary>
''' <remarks></remarks>
Public Class ArrayTypeModifier
    Inherits ParsedObject

    Private m_Ranks As Integer

    Public Overrides Function ResolveTypeReferences() As Boolean
        Return True
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Ranks As Integer)
        MyBase.New(Parent)
        m_Ranks = Ranks
    End Sub

    Sub Init(ByVal Ranks As Integer)
        m_Ranks = Ranks
    End Sub

    Function Clone(Optional ByVal NewParent As ParsedObject = Nothing) As ArrayTypeModifier
        If NewParent Is Nothing Then NewParent = Me.Parent
        Return New ArrayTypeModifier(NewParent, m_Ranks)
    End Function

    ReadOnly Property Ranks() As Integer
        Get
            Return m_Ranks
        End Get
    End Property

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Return True 'Nothing to resolve.
    End Function

    Overrides Function ToString() As String
        Return "(" & New String(","c, m_Ranks - 1) & ")"
    End Function

    Shared Function CanBeMe(ByVal tm As tm) As Boolean
        Return tm.CurrentToken = KS.LParenthesis AndAlso (tm.PeekToken.Equals(KS.Comma, KS.RParenthesis))
    End Function

End Class
