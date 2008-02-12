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

Imports System.Reflection.Emit

''' <summary>
''' EnumDeclaration  ::=
'''	[  Attributes  ]  [  TypeModifier+  ]  "Enum"  Identifier  [  "As"  IntegralTypeName  ]  StatementTerminator
'''	   EnumMemberDeclaration+
'''	"End" "Enum"  StatementTerminator
''' 
''' LAMESPEC: IntegralTypeName is QualifiedName in the spec. (QualifiedName doesn't exist...)
''' </summary>
''' <remarks></remarks>
Public Class EnumDeclaration
    Inherits TypeDeclaration

    Private m_QualifiedName As KS = KS.Integer
    Private m_Constants As Generic.List(Of EnumMemberDeclaration)

    Public Const EnumTypeMemberName As String = "value__"

    Sub New(ByVal Parent As ParsedObject, ByVal [Namespace] As String)
        MyBase.New(Parent, [Namespace])
    End Sub

    Shadows Sub Init(ByVal CustomAttributes As Attributes, ByVal Modifiers As Modifiers, ByVal Members As MemberDeclarations, ByVal Name As Identifier, ByVal EnumType As KS)
        MyBase.Init(CustomAttributes, Modifiers, Members, Name, 0)
        m_QualifiedName = EnumType
    End Sub

    ReadOnly Property Constants() As Generic.List(Of EnumMemberDeclaration)
        Get
            If m_Constants Is Nothing Then
                m_Constants = Members.GetSpecificMembers(Of EnumMemberDeclaration)()
                Helper.Assert(m_Constants.Count = Members.Count)
            End If
            Return m_Constants
        End Get
    End Property

    Overrides Function ResolveType() As Boolean
        Dim result As Boolean = True

#If ENABLECECIL Then
        CecilBaseType = Compiler.CecilTypeCache.System_Enum
#End If
        BaseType = Compiler.TypeCache.System_Enum

        result = MyBase.ResolveType AndAlso result

        Return result
    End Function

    ReadOnly Property EnumConstantTypeKeyword() As KS
        Get
            Return m_QualifiedName
        End Get
    End Property

    ReadOnly Property EnumConstantType() As Type 'Descriptor
        Get
            Return Compiler.TypeResolution.TypeCodeToType(TypeResolution.KeywordToTypeCode(m_QualifiedName))
        End Get
    End Property

    Overrides Function DefineType() As Boolean
        Dim result As Boolean = True

        Dim enumBuilder As EnumBuilder = Nothing

        Helper.Assert(BaseType IsNot Nothing OrElse Me.TypeDescriptor.IsInterface)
        Helper.Assert(Name IsNot Nothing)

        'Create the type builder.
        Dim Attr As TypeAttributes
        Attr = Me.TypeAttributes
        If Helper.IsOnMono OrElse IsNestedType Then
            result = MyBase.DefineType() AndAlso result
        Else
            'This is necessary on MS, since they won't allow the use of the enum fields in attribute constructors otherwise.
            enumBuilder = Compiler.ModuleBuilder.DefineEnum(FullName, Attr And Reflection.TypeAttributes.VisibilityMask, EnumConstantType)
            Compiler.TypeManager.RegisterReflectionType(enumBuilder, Me.TypeDescriptor)
            MyBase.EnumBuilder = enumBuilder
        End If

        Helper.Assert(TypeBuilder IsNot Nothing OrElse enumBuilder IsNot Nothing)

        Return result
    End Function

    Public Overrides Function DefineTypeHierarchy() As Boolean
        Dim result As Boolean = True

        result = MyBase.DefineTypeHierarchy AndAlso result

        If TypeBuilder IsNot Nothing Then
            Dim valueField As FieldInfo
            valueField = TypeBuilder.DefineField(EnumTypeMemberName, EnumConstantType, Reflection.FieldAttributes.Public Or Reflection.FieldAttributes.SpecialName Or FieldAttributes.RTSpecialName)
        End If

        Return result
    End Function

    Shared Function IsMe(ByVal tm As tm) As Boolean
        Dim i As Integer
        While tm.PeekToken(i).Equals(ModifierMasks.TypeModifiers)
            i += 1
        End While
        Return tm.PeekToken(i).Equals(KS.Enum)
    End Function

    ReadOnly Property QualifiedName() As KS
        Get
            Return m_QualifiedName
        End Get
    End Property

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

End Class