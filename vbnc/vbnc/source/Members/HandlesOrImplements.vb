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
''' HandlesOrImplements  ::=  HandlesClause  |  ImplementsClause
''' </summary>
''' <remarks></remarks>
Public Class HandlesOrImplements
    Inherits ParsedObject

    Private m_Clause As BaseObject


    ReadOnly Property Clause() As BaseObject
        Get
            Return m_clause
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject)
        MyBase.new(Parent)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal ImplementsClause As MemberImplementsClause)
        MyBase.new(Parent)
        m_Clause = ImplementsClause
        Helper.Assert(m_Clause IsNot Nothing)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal HandlesClause As HandlesClause)
        MyBase.new(Parent)
        m_Clause = HandlesClause
        Helper.Assert(m_Clause IsNot Nothing)
    End Sub

    Sub Init(ByVal ImplementsClause As MemberImplementsClause)
        m_Clause = ImplementsClause
        Helper.Assert(m_Clause IsNot Nothing)
    End Sub

    Sub Init(ByVal HandlesClause As HandlesClause)
        m_Clause = HandlesClause
        Helper.Assert(m_Clause IsNot Nothing)
    End Sub

    ''' <summary>
    ''' Returns nothing if it is not a handles clause.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property HandlesClause() As HandlesClause
        Get
            Helper.Assert(m_Clause IsNot Nothing)
            Return TryCast(m_Clause, HandlesClause)
        End Get
    End Property

    ''' <summary>
    ''' Returns nothing if it is not an implements clause.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property ImplementsClause() As MemberImplementsClause
        Get
            helper.Assert(m_clause IsNot Nothing)
            Return TryCast(m_Clause, MemberImplementsClause)
        End Get
    End Property

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Helper.Assert(m_Clause IsNot Nothing)
        Return m_Clause.ResolveCode(info)
    End Function

    Shared Function IsMe(ByVal tm As tm) As Boolean
        Return HandlesClause.IsMe(tm) OrElse vbnc.MemberImplementsClause.IsMe(tm)
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        If HandlesClause IsNot Nothing Then
            result = HandlesClause.ResolveTypeReferences AndAlso result
        ElseIf ImplementsClause IsNot Nothing Then
            result = ImplementsClause.ResolveTypeReferences AndAlso result
        Else
            Helper.Stop()
            Throw New InternalException(Me)
        End If

        Return result
    End Function
End Class
