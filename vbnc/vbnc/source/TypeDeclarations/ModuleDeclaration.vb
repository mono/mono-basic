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
''' ModuleDeclaration  ::=
'''	[  Attributes  ]  [  TypeModifier+  ]  "Module"  Identifier  StatementTerminator
'''	[  ModuleMemberDeclaration+  ]
'''	"End" "Module" StatementTerminator
'''
''' </summary>
''' <remarks></remarks>
Public Class ModuleDeclaration
    Inherits TypeDeclaration

    Public Overrides Function CreateDefinition() As Boolean
        Dim result As Boolean = True

        result = MyBase.CreateDefinition() AndAlso result
        result = AddAttribute() AndAlso result

        TypeAttributes = TypeAttributes Or Mono.Cecil.TypeAttributes.Sealed

        Return result
    End Function

    Private Function AddAttribute() As Boolean
        Dim result As Boolean = True
        Dim newAttrib As Attribute

        If Compiler.TypeCache.MS_VB_CS_StandardModuleAttribute Is Nothing Then Return True

        newAttrib = New Attribute(Me, Compiler.TypeCache.MS_VB_CS_StandardModuleAttribute)
        result = newAttrib.ResolveCode(ResolveInfo.Default(Compiler)) AndAlso result

        If MyBase.CustomAttributes Is Nothing Then MyBase.CustomAttributes = New Attributes(Me)
        MyBase.CustomAttributes.Add(newAttrib)

        Return result
    End Function

    Sub New(ByVal Parent As ParsedObject, ByVal [Namespace] As String, ByVal Name As Identifier)
        MyBase.New(Parent, [Namespace], Name)
    End Sub

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        MyBase.BaseType = Compiler.TypeCache.System_Object
        result = MyBase.ResolveTypeReferences AndAlso result

        Return result
    End Function

    Shared Function IsMe(ByVal tm As tm) As Boolean
        Dim i As Integer
        While tm.PeekToken(i).Equals(ModifierMasks.TypeModifiers)
            i += 1
        End While
        Return tm.PeekToken(i).Equals(KS.Module)
    End Function

    Public Overrides ReadOnly Property IsShared() As Boolean
        Get
            Return True
        End Get
    End Property

    Protected Overrides ReadOnly Property NeedsSharedConstructor As Boolean
        Get
            Return Me.HasSharedConstantFields OrElse Me.HasSharedFieldsWithInitializers
        End Get
    End Property
End Class