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
''' InterfaceDeclaration  ::=
'''	[  Attributes  ]  [  TypeModifier+  ]  "Interface" Identifier  [  TypeParameters  ]  StatementTerminator
'''	[  InterfaceBase+  ]
'''	[  InterfaceMemberDeclaration+  ]
'''	"End" "Interface" StatementTerminator
''' </summary>
''' <remarks></remarks>
Public Class InterfaceDeclaration
    Inherits GenericTypeDeclaration

    Private m_InterfaceBases As InterfaceBases

    Sub New(ByVal Parent As ParsedObject, ByVal [Namespace] As String)
        MyBase.New(Parent, [Namespace])
    End Sub

    Shadows Sub Init(ByVal CustomAttributes As Attributes, ByVal Modifiers As Modifiers, ByVal Members As MemberDeclarations, ByVal Name As Identifier, ByVal TypeParameters As TypeParameters, ByVal InterfaceBases As InterfaceBases)
        MyBase.Init(CustomAttributes, Modifiers, Members, Name, TypeParameters)
        m_InterfaceBases = InterfaceBases
    End Sub

    Public Overrides ReadOnly Property TypeAttributes() As System.Reflection.TypeAttributes
        Get
            Return Helper.getTypeAttributeScopeFromScope(Modifiers, IsNestedType) Or _
            Reflection.TypeAttributes.Interface Or Reflection.TypeAttributes.Abstract
        End Get
    End Property

    Public Overrides ReadOnly Property IsShared() As Boolean
        Get
            Return False
        End Get
    End Property

    ReadOnly Property InterfaceBases() As InterfaceBases
        Get
            Return m_InterfaceBases
        End Get
    End Property

    Public Overrides Function ResolveType() As Boolean
        Dim result As Boolean = True

        If m_InterfaceBases IsNot Nothing Then
            result = m_InterfaceBases.ResolveTypeReferences AndAlso result
            MyBase.ImplementedTypes = m_InterfaceBases.AsTypes
        End If

        Return result
    End Function

    Shared Function IsMe(ByVal tm As tm) As Boolean
        Dim i As Integer
        While tm.PeekToken(i).Equals(ModifierMasks.TypeModifiers)
            i += 1
        End While
        Return tm.PeekToken(i).Equals(KS.Interface)
    End Function

End Class