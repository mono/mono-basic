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

    Sub New(ByVal Parent As ParsedObject, ByVal [Namespace] As String, ByVal Name As Identifier, ByVal TypeParameters As TypeParameters)
        MyBase.New(Parent, [Namespace], Name, TypeParameters)
    End Sub


    Public Overrides Function CreateDefinition() As Boolean
        Dim result As Boolean = True

        result = MyBase.CreateDefinition AndAlso result
        TypeAttributes = Helper.getTypeAttributeScopeFromScope(Modifiers, IsNestedType) Or Mono.Cecil.TypeAttributes.Interface Or Mono.Cecil.TypeAttributes.Abstract

        Return result
    End Function

    Public Overrides ReadOnly Property IsShared() As Boolean
        Get
            Return False
        End Get
    End Property

    Property InterfaceBases() As InterfaceBases
        Get
            Return m_InterfaceBases
        End Get
        Set(ByVal value As InterfaceBases)
            m_InterfaceBases = value
        End Set
    End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        If m_InterfaceBases IsNot Nothing Then
            result = m_InterfaceBases.ResolveTypeReferences AndAlso result
            For i As Integer = 0 To m_InterfaceBases.Bases.Length - 1
                AddInterface(m_InterfaceBases.Bases(i).ResolvedType)
            Next
        End If

        result = MyBase.ResolveTypeReferences AndAlso result

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