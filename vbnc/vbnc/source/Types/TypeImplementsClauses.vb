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
''' TypeImplementsClause  ::=  "Implements" Implements StatementTerminator
''' Implements  ::=	NonArrayTypeName  |	Implements  ","  NonArrayTypeName
''' </summary>
''' <remarks></remarks>
Public Class TypeImplementsClauses
    Inherits ParsedObject
    
    Private m_Clauses As New Generic.List(Of NonArrayTypeName)

    Sub New(ByVal Parent As ParsedObject)
        MyBase.new(Parent)
    End Sub

    Sub Init(ByVal Clauses As Generic.List(Of NonArrayTypeName))
        m_Clauses = Clauses
    End Sub

    Public Overrides Function ResolveTypeReferences() As Boolean
        Return Helper.ResolveTypeReferencesCollection(m_Clauses)
    End Function

    ReadOnly Property Clauses() As Generic.List(Of NonArrayTypeName)
        Get
            Return m_Clauses
        End Get
    End Property

    Shared Function IsMe(ByVal tm As tm) As Boolean
        Return tm.CurrentToken = KS.Implements
    End Function

    Function GetTypes() As Type()
        Dim result(m_Clauses.Count - 1) As Type
        For i As Integer = 0 To result.GetUpperBound(0)
            result(i) = m_Clauses(i).ResolvedType
        Next
        Return result
    End Function
End Class
