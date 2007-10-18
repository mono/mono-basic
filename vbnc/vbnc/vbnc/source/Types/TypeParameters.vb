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
''' TypeParameters  ::= "("  "Of"  TypeParameterList  ")"
''' CHANGED: Switched name of TypeParameters and TypeParameterList
''' </summary>
''' <remarks></remarks>
Public Class TypeParameters
    Inherits ParsedObject

    Private m_TypeParameters As TypeParameterList

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal TypeParameters As TypeParameterList)
        m_TypeParameters = TypeParameters
    End Sub

    Function Clone(Optional ByVal NewParent As ParsedObject = Nothing) As TypeParameters
        If NewParent Is Nothing Then NewParent = Me.Parent
        Dim result As New TypeParameters(NewParent)
        result.Init(m_TypeParameters.clone(result))
        Return result
    End Function

    ReadOnly Property Parameters() As TypeParameterList
        Get
            Return m_TypeParameters
        End Get
    End Property

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Return m_TypeParameters.ResolveCode(info)
    End Function

    Shared Function IsMe(ByVal tm As tm) As Boolean
        Return tm.CurrentToken = KS.LParenthesis AndAlso tm.PeekToken = KS.Of
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        result = m_TypeParameters.ResolveTypeReferences AndAlso result

        Return result
    End Function
End Class
