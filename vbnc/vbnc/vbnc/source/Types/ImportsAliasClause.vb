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
''' ImportsAliasClause  ::=
'''	Identifier  =  QualifiedIdentifier  |
'''	Identifier  =  ConstructedTypeName
''' 
''' ConstructedTypeName  ::=
'''	QualifiedIdentifier  "("  "Of"  TypeArgumentList  ")"
''' </summary>
''' <remarks></remarks>
Public Class ImportsAliasClause
    Inherits ParsedObject

    Private m_Identifier As Identifier
    Private m_Second As ImportsNamespaceClause

    ReadOnly Property Identifier() As Identifier
        Get
            Return m_Identifier
        End Get
    End Property

    ReadOnly Property Second() As ImportsNamespaceClause
        Get
            Return m_second
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal Identifier As Identifier, ByVal Second As ImportsNamespaceClause)
        m_Identifier = Identifier
        m_Second = Second
    End Sub

    ''' <summary>
    ''' The imported namespace or type name.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property NamespaceClause() As ImportsNamespaceClause
        Get
            Return m_Second
        End Get
    End Property

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Helper.Assert(m_Second IsNot Nothing)
        Return m_Second.ResolveCode(Info)
    End Function

    ''' <summary>
    ''' The alias
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Name() As String
        Get
            Return m_Identifier.Name
        End Get
    End Property

    Shared Function IsMe(ByVal tm As tm) As Boolean
        Return tm.CurrentToken.IsIdentifier AndAlso tm.PeekToken.Equals(KS.Equals)
    End Function

    Shared Function IsMe(ByVal str As String) As Boolean
        Return str.Contains("=")
    End Function


End Class
