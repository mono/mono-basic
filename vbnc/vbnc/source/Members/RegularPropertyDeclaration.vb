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
''' RegularPropertyMemberDeclaration  ::=
'''	[  Attributes  ]  [  PropertyModifier+  ] "Property" FunctionSignature  [  ImplementsClause  ]
'''		LineTerminator
'''	PropertyAccessorDeclaration+
'''	"End" "Property" StatementTerminator
'''
''' </summary>
''' <remarks></remarks>
Public Class RegularPropertyDeclaration
    Inherits PropertyDeclaration

    Sub New(ByVal Parent As TypeDeclaration)
        MyBase.New(Parent)
    End Sub

    Shared Function IsMe(ByVal tm As tm) As Boolean
        Dim i As Integer
        While tm.PeekToken(i).Equals(ModifierMasks.PropertyModifiers)
            i += 1
        End While
        Return tm.PeekToken(i).Equals(KS.Property)
    End Function

End Class
