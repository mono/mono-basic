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
    Private m_ValueField As Mono.Cecil.FieldDefinition

    Public Const EnumTypeMemberName As String = "value__"

    Sub New(ByVal Parent As ParsedObject, ByVal [Namespace] As String, ByVal Name As Identifier, ByVal EnumType As KS)
        MyBase.New(Parent, [Namespace], Name)
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

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveTypeReferences() AndAlso result
        UpdateDefinition()

        Return result
    End Function

    ReadOnly Property EnumConstantTypeKeyword() As KS
        Get
            Return m_QualifiedName
        End Get
    End Property

    ReadOnly Property EnumConstantType() As Mono.Cecil.TypeReference
        Get
            Return Compiler.TypeResolution.TypeCodeToType(TypeResolution.KeywordToTypeCode(m_QualifiedName))
        End Get
    End Property

    Overrides Function DefineType() As Boolean
        Dim result As Boolean = True

        UpdateDefinition()

        Return result
    End Function

    Public Overrides Function DefineTypeHierarchy() As Boolean
        Dim result As Boolean = True

        result = MyBase.DefineTypeHierarchy AndAlso result

        UpdateDefinition()

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

    Public Overrides Sub UpdateDefinition()
        MyBase.UpdateDefinition()

        TypeAttributes = Helper.getTypeAttributeScopeFromScope(Modifiers, IsNestedType) Or Mono.Cecil.TypeAttributes.Sealed
        BaseType = Compiler.TypeCache.System_Enum

        If m_ValueField Is Nothing AndAlso m_QualifiedName <> KS.None Then
            m_ValueField = New Mono.Cecil.FieldDefinition(EnumTypeMemberName, Mono.Cecil.FieldAttributes.Public Or Mono.Cecil.FieldAttributes.SpecialName Or Mono.Cecil.FieldAttributes.RTSpecialName, Helper.GetTypeOrTypeReference(Compiler, EnumConstantType))
            CecilType.Fields.Add(m_ValueField)
        End If

        If m_ValueField IsNot Nothing Then
            m_ValueField.FieldType = Helper.GetTypeOrTypeReference(Compiler, EnumConstantType)
        End If
    End Sub

    Public Overrides ReadOnly Property IsShared() As Boolean
        Get
            Return True
        End Get
    End Property
End Class