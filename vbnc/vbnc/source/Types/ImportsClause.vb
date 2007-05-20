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
''' ImportsClause  ::=  ImportsAliasClause  |  ImportsNamespaceClause
''' </summary>
''' <remarks></remarks>
Public Class ImportsClause
    Inherits ParsedObject
    Implements INameable

    Private m_Clause As BaseObject

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal Clause As ImportsAliasClause)
        m_Clause = Clause
    End Sub

    Sub Init(ByVal Clause As ImportsNamespaceClause)
        m_Clause = Clause
    End Sub

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Return m_Clause.ResolveCode(Info)
    End Function

    ReadOnly Property IsAliasClause() As Boolean
        Get
            Return TypeOf m_Clause Is ImportsAliasClause
        End Get
    End Property

    ReadOnly Property IsNamespaceClause() As Boolean
        Get
            Return TypeOf m_Clause Is ImportsNamespaceClause
        End Get
    End Property

    ReadOnly Property AsAliasClause() As ImportsAliasClause
        Get
            Return DirectCast(m_Clause, ImportsAliasClause)
        End Get
    End Property

    ReadOnly Property AsNamespaceClause() As ImportsNamespaceClause
        Get
            Return DirectCast(m_Clause, ImportsNamespaceClause)
        End Get
    End Property

    Public ReadOnly Property Name() As String Implements INameable.Name
        Get
            If Me.IsAliasClause Then
                Return Me.AsAliasClause.Name
            Else
                Return ""
            End If
        End Get
    End Property
End Class
