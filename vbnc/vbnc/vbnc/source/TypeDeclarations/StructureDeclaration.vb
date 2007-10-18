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
''' StructureDeclaration  ::=
'''	[  Attributes  ]  [  StructureModifier+  ]  "Structure" Identifier  [  TypeParameters  ]	StatementTerminator
'''	[  TypeImplementsClause+  ]
'''	[  StructMemberDeclaration+  ]
'''	"End" "Structure"  StatementTerminator
''' </summary>
''' <remarks></remarks>
Public Class StructureDeclaration
    Inherits PartialTypeDeclaration

    Sub New(ByVal Parent As ParsedObject, ByVal [Namespace] As String)
        MyBase.New(Parent, [Namespace])
    End Sub

    Public Overrides Function ResolveType() As Boolean
        Dim result As Boolean = True

        MyBase.BaseType = Compiler.TypeCache.System_ValueType

#If ENABLECECIL Then
        MyBase.CecilBaseType = Compiler.CecilTypeCache.System_ValueType
#End If
        result = MyBase.ResolveType AndAlso result

        Return result
    End Function

    Shared Function IsMe(ByVal tm As tm) As Boolean
        Dim i As Integer
        While tm.PeekToken(i).Equals(ModifierMasks.StructureModifiers)
            i += 1
        End While
        Return tm.PeekToken(i).Equals(KS.Structure)
    End Function

    Public Overrides ReadOnly Property TypeAttributes() As System.Reflection.TypeAttributes
        Get
            Dim result As TypeAttributes = MyBase.TypeAttributes

            result = result Or Reflection.TypeAttributes.SequentialLayout Or Reflection.TypeAttributes.Sealed

            Return result
        End Get
    End Property
End Class
