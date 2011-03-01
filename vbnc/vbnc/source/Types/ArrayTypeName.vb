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
''' ArrayTypeName          ::=  NonArrayTypeName  ArrayTypeModifiers
''' ArrayTypeModifiers     ::=  ArrayTypeModifier+
''' ArrayTypeModifier      ::=  "("  [  RankList  ]  ")"
''' RankList               ::=  ","  | RankList
''' </summary>
''' <remarks></remarks>
Public Class ArrayTypeName
    Inherits ParsedObject

    Private m_TypeName As NonArrayTypeName
    Private m_ArrayTypeModifiers As ArrayTypeModifiers

    Private m_ResolvedType As Mono.Cecil.TypeReference

    ReadOnly Property TypeName() As NonArrayTypeName
        Get
            Return m_Typename
        End Get
    End Property

    ReadOnly Property ArrayTypeModifiers() As ArrayTypeModifiers
        Get
            Return m_ArrayTypeModifiers
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject)
        MyBase.new(Parent)
    End Sub

    Sub Init(ByVal TypeName As NonArrayTypeName, ByVal ArrayTypeModifiers As ArrayTypeModifiers)
        m_TypeName = TypeName
        m_ArrayTypeModifiers = ArrayTypeModifiers
    End Sub

    ReadOnly Property ResolvedType() As Mono.Cecil.TypeReference
        Get
            Return m_ResolvedType
        End Get
    End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        result = m_TypeName.ResolveTypeReferences AndAlso result
        'Not necessary.'result = m_ArrayTypeModifiers.ResolveCode AndAlso result

        Dim tp As Mono.Cecil.TypeReference = m_TypeName.ResolvedType
        tp = m_ArrayTypeModifiers.CreateArrayType(tp)
        m_ResolvedType = tp

        Return result
    End Function

    ReadOnly Property Name() As String
        Get
            Return m_TypeName.Name
        End Get
    End Property

    Shared Function CanBeArrayTypeModifier(ByVal tm As tm) As Boolean
        Return tm.CurrentToken = KS.LParenthesis AndAlso (tm.PeekToken.Equals(KS.Comma, KS.RParenthesis))
    End Function

    Overrides Function ToString() As String
        Return m_TypeName.ToString & m_ArrayTypeModifiers.ToString
    End Function

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = m_TypeName.ResolveCode(Info) AndAlso result

        Return result
    End Function
End Class
