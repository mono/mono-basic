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
''' EnumMemberDeclaration  ::=  [  Attributes  ]  Identifier  [  "="  ConstantExpression  ]  StatementTerminator
''' </summary>
''' <remarks></remarks>
Public Class EnumMemberDeclaration
    Inherits MemberDeclaration
    Implements IFieldMember

    ''' <summary>
    ''' The index of the constant in the enum.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_EnumIndex As Integer
    Private m_Identifier As Identifier
    Private m_ConstantExpression As Expression

    Private m_FieldBuilderCecil As Mono.Cecil.FieldDefinition

    Sub New(ByVal Parent As ParsedObject)
        MyBase.new(Parent)
    End Sub

    Shadows Sub Init(ByVal EnumIndex As Integer, ByVal Identifier As Identifier, ByVal ConstantExpression As Expression)
        MyBase.Init(Nothing, Identifier.Identifier)
        m_EnumIndex = EnumIndex
        m_Identifier = Identifier
        m_ConstantExpression = ConstantExpression
    End Sub

    Public Property ConstantValue() As Object
        Get
            Return m_FieldBuilderCecil.Constant
        End Get
        Set(ByVal value As Object)
            m_FieldBuilderCecil.Constant = value
        End Set
    End Property

    Public Overrides Function CreateDefinition() As Boolean
        Dim result As Boolean = True

        result = MyBase.CreateDefinition AndAlso result

        Helper.Assert(m_FieldBuilderCecil Is Nothing)
        m_FieldBuilderCecil = New Mono.Cecil.FieldDefinition(Name, Mono.Cecil.FieldAttributes.Public Or Mono.Cecil.FieldAttributes.Static Or Mono.Cecil.FieldAttributes.Literal Or Mono.Cecil.FieldAttributes.HasDefault, Parent.CecilType)
        m_FieldBuilderCecil.Annotations.Add(Compiler, Me)
        Parent.CecilType.Fields.Add(m_FieldBuilderCecil)
        m_FieldBuilderCecil.Name = Name
        m_FieldBuilderCecil.IsLiteral = True

        Return result
    End Function

    ReadOnly Property EnumIndex() As Integer
        Get
            Return m_EnumIndex
        End Get
    End Property

    Public ReadOnly Property FieldBuilder() As Mono.Cecil.FieldDefinition Implements IFieldMember.FieldBuilder
        Get
            Return m_FieldBuilderCecil
        End Get
    End Property

    Public ReadOnly Property FieldType() As Mono.Cecil.TypeReference Implements IFieldMember.FieldType
        Get
            Return Me.FindFirstParent(Of EnumDeclaration).EnumConstantType
        End Get
    End Property

    Public Overrides ReadOnly Property MemberDescriptor() As Mono.Cecil.MemberReference
        Get
            Return m_FieldBuilderCecil
        End Get
    End Property

    Public Overrides ReadOnly Property IsShared() As Boolean
        Get
            Return True
        End Get
    End Property

    ReadOnly Property Identifier() As Identifier
        Get
            Return m_Identifier
        End Get
    End Property

    ReadOnly Property ConstantExpression() As Expression
        Get
            Return m_ConstantExpression
        End Get
    End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveTypeReferences AndAlso result
        result = Helper.ResolveTypeReferences(m_ConstantExpression) AndAlso result

        Return result
    End Function

    Function GetConstantValue(ByRef result As Object) As Boolean
        Dim obj As Object = Nothing

        If m_FieldBuilderCecil.Constant IsNot Nothing Then
            result = m_FieldBuilderCecil.Constant
            Return True
        End If

        If m_ConstantExpression IsNot Nothing Then
            If Not m_ConstantExpression.GetConstant(obj, True) Then Return False
        Else
            If m_EnumIndex = 0 Then
                obj = 0
            ElseIf Not Parent.Constants(m_EnumIndex - 1).GetConstantValue(obj) Then
                Return False
            Else
                obj = CDec(obj) + 1
            End If
        End If

        Select Case Parent.EnumConstantTypeKeyword
            Case KS.Byte
                obj = CByte(obj)
            Case KS.SByte
                obj = CSByte(obj)
            Case KS.Short
                obj = CShort(obj)
            Case KS.UShort
                obj = CUShort(obj)
            Case KS.Integer
                obj = CInt(obj)
            Case KS.UInteger
                obj = CUInt(obj)
            Case KS.Long
                obj = CLng(obj)
            Case KS.ULong
                obj = CULng(obj)
        End Select

        result = obj
        Return True
    End Function

    Function ResolveMember(ByVal Info As ResolveInfo) As Boolean Implements INonTypeMember.ResolveMember
        Dim result As Boolean = True
        
        If m_ConstantExpression IsNot Nothing Then result = m_ConstantExpression.ResolveExpression(Info) AndAlso result

        Return result
    End Function

    Function DefineConstant() As Boolean
        Dim result As Boolean = True
        Dim obj As Object = Nothing

        result = GetConstantValue(obj) AndAlso result
        ConstantValue = obj

        Return result
    End Function

    Shadows ReadOnly Property Parent() As EnumDeclaration
        Get
            Return Me.FindFirstParent(Of EnumDeclaration)()
        End Get
    End Property
End Class

