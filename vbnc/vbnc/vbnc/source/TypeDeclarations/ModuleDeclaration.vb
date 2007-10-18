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
''' ModuleDeclaration  ::=
'''	[  Attributes  ]  [  TypeModifier+  ]  "Module"  Identifier  StatementTerminator
'''	[  ModuleMemberDeclaration+  ]
'''	"End" "Module" StatementTerminator
'''
''' </summary>
''' <remarks></remarks>
Public Class ModuleDeclaration
    Inherits TypeDeclaration
    Implements IHasImplicitMembers

    Private Function AddAttribute() As Boolean
        Dim result As Boolean = True
        Dim newAttrib As Attribute

        newAttrib = New Attribute(Me, Compiler.TypeCache.MS_VB_CS_StandardModuleAttribute)
        result = newAttrib.ResolveCode(ResolveInfo.Default(Compiler)) AndAlso result

        MyBase.CustomAttributes.Add(newAttrib)

        Return result
    End Function

    Public Overrides Function DefineType() As Boolean
        Dim result As Boolean = True

        result = AddAttribute() AndAlso result
        result = MyBase.DefineType() AndAlso result

        Return result
    End Function

    Sub New(ByVal Parent As ParsedObject, ByVal [Namespace] As String)
        MyBase.New(Parent, [Namespace])
    End Sub

    Public Overrides Function ResolveType() As Boolean
        Dim result As Boolean = True

#If ENABLECECIL Then
        MyBase.CecilBaseType = Compiler.CecilTypeCache.System_Object
#End If

        MyBase.BaseType = Compiler.TypeCache.System_Object
        result = MyBase.ResolveType AndAlso result

        Me.FindDefaultConstructors()

        Return result
    End Function

    Shared Function IsMe(ByVal tm As tm) As Boolean
        Dim i As Integer
        While tm.PeekToken(i).Equals(ModifierMasks.TypeModifiers)
            i += 1
        End While
        Return tm.PeekToken(i).Equals(KS.Module)
    End Function

    Public Overrides ReadOnly Property TypeAttributes() As System.Reflection.TypeAttributes
        Get
            Return Helper.getTypeAttributeScopeFromScope(Modifiers, IsNestedType) Or Reflection.TypeAttributes.Sealed
        End Get
    End Property

    Public Overrides ReadOnly Property IsShared() As Boolean
        Get
            Return True
        End Get
    End Property


    Private Function CreateImplicitMembers() As Boolean Implements IHasImplicitMembers.CreateImplicitMembers
        Dim result As Boolean = True

        If DefaultSharedConstructor Is Nothing AndAlso (Me.HasSharedConstantFields OrElse Me.HasSharedFieldsWithInitializers) Then
            DefaultSharedConstructor = New ConstructorDeclaration(Me)
            DefaultSharedConstructor.Init(Nothing, New Modifiers(ModifierMasks.Shared), New SubSignature(DefaultSharedConstructor, ConstructorDeclaration.SharedConstructorName, New ParameterList(DefaultSharedConstructor)), New CodeBlock(DefaultSharedConstructor))
            result = DefaultSharedConstructor.ResolveTypeReferences AndAlso result
            Members.Add(DefaultSharedConstructor)
            BeforeFieldInit = True
        End If

        Return result
    End Function
End Class
