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
''' InterfaceBase   ::= Inherits  InterfaceBases  StatementTerminator
''' InterfaceBases  ::= NonArrayTypeName  | InterfaceBases  ","  NonArrayTypeName
'''
''' </summary>
''' <remarks></remarks>
Public Class InterfaceBases
    Inherits ParsedObject

    Private m_Bases() As NonArrayTypeName

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal Bases As NonArrayTypeName())
        m_Bases = Bases
    End Sub

    ReadOnly Property Bases() As NonArrayTypeName()
        Get
            Return m_Bases
        End Get
    End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        Helper.Assert(m_Bases IsNot Nothing)
        result = Helper.ResolveTypeReferencesCollection(m_Bases)

        Return result
    End Function

    Shared Function IsMe(ByVal tm As tm) As Boolean
        Return tm.CurrentToken = KS.Inherits
    End Function

    ''' <summary>
    ''' Returns the interface bases as an array of types.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property AsTypes() As Type()
        Get
            Dim result(m_Bases.GetUpperBound(0)) As Type
            For i As Integer = 0 To m_Bases.GetUpperBound(0)
                result(i) = m_Bases(i).ResolvedType
            Next
            Return result
        End Get
    End Property

End Class
