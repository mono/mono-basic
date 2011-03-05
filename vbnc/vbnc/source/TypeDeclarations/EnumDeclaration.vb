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

    Private m_Constants As Generic.List(Of EnumMemberDeclaration)
    Private m_ValueField As Mono.Cecil.FieldDefinition
    Public EnumConstantType As TypeReference
    Public EnumType As NonArrayTypeName
    Public EnumConstantTypeKeyword As KS

    Public Const EnumTypeMemberName As String = "value__"

    Sub New(ByVal Parent As ParsedObject, ByVal [Namespace] As String, ByVal Name As Identifier)
        MyBase.New(Parent, [Namespace], Name)
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

    Shared Function IsMe(ByVal tm As tm) As Boolean
        Dim i As Integer
        While tm.PeekToken(i).Equals(ModifierMasks.TypeModifiers)
            i += 1
        End While
        Return tm.PeekToken(i).Equals(KS.Enum)
    End Function

    Public Overrides Function CreateDefinition() As Boolean
        Dim result As Boolean = True

        Helper.Assert(m_ValueField Is Nothing)

        result = MyBase.CreateDefinition() AndAlso result

        TypeAttributes = TypeAttributes Or Mono.Cecil.TypeAttributes.Sealed
        BaseType = Compiler.TypeCache.System_Enum

        m_ValueField = New Mono.Cecil.FieldDefinition(EnumTypeMemberName, Mono.Cecil.FieldAttributes.Public Or Mono.Cecil.FieldAttributes.SpecialName Or Mono.Cecil.FieldAttributes.RTSpecialName, Nothing)
        CecilType.Fields.Add(m_ValueField)

        Return result
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveTypeReferences AndAlso result
        If EnumConstantType Is Nothing Then
            If EnumType IsNot Nothing Then
                result = EnumType.ResolveTypeReferences AndAlso result
                If result = False Then Return False
                EnumConstantType = EnumType.ResolvedType
            Else
                EnumConstantType = Compiler.TypeCache.System_Int32
            End If
        End If
        If Helper.CompareType(Compiler.TypeCache.System_Byte, EnumConstantType) Then
            EnumConstantTypeKeyword = KS.Byte
        ElseIf Helper.CompareType(EnumConstantType, Compiler.TypeCache.System_SByte) Then
            EnumConstantTypeKeyword = KS.SByte
        ElseIf Helper.CompareType(EnumConstantType, Compiler.TypeCache.System_UInt16) Then
            EnumConstantTypeKeyword = KS.UShort
        ElseIf Helper.CompareType(EnumConstantType, Compiler.TypeCache.System_Int16) Then
            EnumConstantTypeKeyword = KS.Short
        ElseIf Helper.CompareType(EnumConstantType, Compiler.TypeCache.System_UInt32) Then
            EnumConstantTypeKeyword = KS.UInteger
        ElseIf Helper.CompareType(EnumConstantType, Compiler.TypeCache.System_Int32) Then
            EnumConstantTypeKeyword = KS.Integer
        ElseIf Helper.CompareType(EnumConstantType, Compiler.TypeCache.System_UInt64) Then
            EnumConstantTypeKeyword = KS.ULong
        ElseIf Helper.CompareType(EnumConstantType, Compiler.TypeCache.System_Int64) Then
            EnumConstantTypeKeyword = KS.Long
        Else
            result = Compiler.Report.ShowMessage(Messages.VBNC30650, Me.Location)
        End If
        m_ValueField.FieldType = Helper.GetTypeOrTypeReference(Compiler, EnumConstantType)

        Return result
    End Function

    Public Overrides ReadOnly Property IsShared() As Boolean
        Get
            Return True
        End Get
    End Property
End Class

