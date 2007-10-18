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
''' BoundList::= Expression | "0" "To" Expression  | UpperBoundList ,  Expression
''' UpperBoundList::= Expression | UpperBoundList , Expression
''' </summary>
''' <remarks></remarks>
Public Class BoundList
    Inherits ParsedObject

    Private m_Expressions() As Expression

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal Expressions() As Expression)
        m_Expressions = Expressions
    End Sub

    Function Clone(Optional ByVal NewParent As ParsedObject = Nothing) As BoundList
        If NewParent Is Nothing Then NewParent = Me.Parent
        Dim result As New BoundList(NewParent)
        result.Init(Helper.CloneExpressionArray(m_Expressions, result))
        Return result
    End Function

    ReadOnly Property Expressions() As Expression()
        Get
            Return m_Expressions
        End Get
    End Property

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Return Helper.ResolveCodeCollection(m_Expressions, info)
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        result = Helper.ResolveTypeReferences(m_Expressions) AndAlso result

        Return result
    End Function

End Class
