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
''' CustomEventMemberDeclaration  ::=
'''	[  Attributes  ]  [  EventModifiers+  ]  "Custom" "Event" Identifier "As" TypeName  [  ImplementsClause  ]
'''		StatementTerminator
'''		EventAccessorDeclaration+
'''	"End" "Event" StatementTerminator
''' </summary>
''' <remarks></remarks>
Public Class CustomEventDeclaration
    Inherits EventDeclaration

    Private m_Type As NonArrayTypeName

    Sub New(ByVal Parent As TypeDeclaration)
        MyBase.New(Parent)
    End Sub

    Shadows Sub Init(ByVal Attributes As Attributes, ByVal Modifiers As Modifiers, ByVal Identifier As Identifier, ByVal TypeName As NonArrayTypeName, ByVal ImplementsClause As MemberImplementsClause)
        MyBase.Init(Attributes, Modifiers, Identifier, ImplementsClause)
        m_Type = TypeName
    End Sub

    Shared Function IsMe(ByVal tm As tm) As Boolean
        Dim i As Integer
        While tm.PeekToken(i).Equals(ModifierMasks.EventModifiers)
            i += 1
        End While
        Return tm.PeekToken(i).Equals("Custom") AndAlso tm.PeekToken(i + 1).Equals(KS.Event)
    End Function

    ReadOnly Property Type() As NonArrayTypeName
        Get
            Return m_Type
        End Get
    End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        If m_Type IsNot Nothing Then
            Helper.Assert(EventType Is Nothing)
            result = m_Type.ResolveTypeReferences AndAlso result
            EventType = m_Type.ResolvedType
        End If

        result = MyBase.ResolveTypeReferences AndAlso result

        Return result
    End Function
End Class
