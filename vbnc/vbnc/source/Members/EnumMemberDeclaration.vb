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
''' EnumMemberDeclaration  ::=  [  Attributes  ]  Identifier  [  "="  ConstantExpression  ]  StatementTerminator
''' </summary>
''' <remarks></remarks>
Public Class EnumMemberDeclaration
    Inherits MemberDeclaration
    Implements IFieldMember

    Private m_Descriptor As New FieldDescriptor(Me)

    ''' <summary>
    ''' The index of the constant in the enum.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_EnumIndex As Integer
    Private m_Identifier As Identifier
    Private m_ConstantExpression As Expression

    Private m_FieldBuilder As FieldBuilder

    Private m_ConstantValue As Object
    Private m_ResolvedMember As Boolean

    Sub New(ByVal Parent As ParsedObject)
        MyBase.new(Parent)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal EnumIndex As Integer)
        MyBase.New(Parent)
        m_EnumIndex = EnumIndex
    End Sub

    Shadows Sub Init(ByVal EnumIndex As Integer, ByVal Attributes As Attributes, ByVal Identifier As Identifier, ByVal ConstantExpression As Expression)
        MyBase.Init(Attributes, New Modifiers(), Identifier.Identifier)
        m_EnumIndex = EnumIndex
        m_Identifier = Identifier
        m_ConstantExpression = ConstantExpression
    End Sub

    ReadOnly Property ConstantValue() As Object
        Get
            If m_ResolvedMember = False Then ResolveMember(ResolveInfo.Default(Compiler))
            Return m_ConstantValue
        End Get
    End Property


    ReadOnly Property EnumIndex() As Integer
        Get
            Return m_EnumIndex
        End Get
    End Property

    Public ReadOnly Property FieldBuilder() As System.Reflection.Emit.FieldBuilder Implements IFieldMember.FieldBuilder
        Get
            Return m_FieldBuilder
        End Get
    End Property

    Public ReadOnly Property FieldType() As System.Type Implements IFieldMember.FieldType
        Get
            Return Me.FindFirstParent(Of EnumDeclaration).EnumConstantType
        End Get
    End Property

    Public Overrides ReadOnly Property MemberDescriptor() As System.Reflection.MemberInfo
        Get
            Return m_Descriptor
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

    Function ResolveMember(ByVal Info As ResolveInfo) As Boolean Implements INonTypeMember.ResolveMember
        If m_ResolvedMember Then Return True
        Dim result As Boolean = True
        Dim parent As EnumDeclaration = Me.FindFirstParent(Of EnumDeclaration)()

        Dim obj As Object
        If m_ConstantExpression IsNot Nothing Then
            result = m_ConstantExpression.ResolveExpression(Info) AndAlso result
            obj = m_ConstantExpression.ConstantValue
        Else
            If m_EnumIndex = 0 Then
                obj = 0
            Else
                obj = CDec(parent.Constants(m_EnumIndex - 1).ConstantValue) + 1
            End If
        End If

        Select Case parent.EnumConstantTypeKeyword
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
            Case Else
                Throw New InternalException(Me)
        End Select
        m_ConstantValue = obj
        result = m_ConstantValue IsNot Nothing AndAlso result
        m_ResolvedMember = True

        Return result
    End Function

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveCode(Info) AndAlso result
        If m_ConstantExpression IsNot Nothing Then result = m_ConstantExpression.ResolveExpression(Info) AndAlso result

        Return result
    End Function

    ''' <summary>
    ''' Define the enum constant.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DefineMember() As Boolean Implements IDefinableMember.DefineMember
        Dim result As Boolean = True

        Helper.Assert(m_ConstantValue IsNot Nothing)

        Dim parent As EnumDeclaration = Me.FindFirstParent(Of EnumDeclaration)()

        If Helper.IsOnMono Then
            Dim newValue As Object = m_ConstantValue
            If parent.TypeBuilder IsNot Nothing Then
                'This is a work around for a Mono bug (otherwise the enum constant will be defined of its base integral type in metadata).
                'MS doesn't allow this (parameter to Enum.ToObject can't be a TypeBuilder), so only execute on Mono.
                newValue = [Enum].ToObject(parent.TypeBuilder, m_ConstantValue)
            ElseIf parent.EnumBuilder IsNot Nothing Then
                'This will crash the compiler on Mono, so only do this when we have a typebuilder.
                'the EnumBuilder is anyways only used on MS to work around an MS bug, otherwise we only use TypeBuilder.
                'newValue = [Enum].ToObject(parent.EnumBuilder, m_ConstantValue)
            End If
#If EXTENDEDDEBUG Then
            Console.WriteLine("Changed Enum field type from '" & m_ConstantValue.GetType.Name & "' to '" & newValue.GetType.Name & "'")
#End If
            m_ConstantValue = newValue
        End If

        If parent.TypeBuilder IsNot Nothing Then
            m_FieldBuilder = parent.TypeBuilder.DefineField(Name, parent.TypeBuilder, Reflection.FieldAttributes.Public Or Reflection.FieldAttributes.Static Or Reflection.FieldAttributes.Literal)
            m_FieldBuilder.SetConstant(m_ConstantValue)
        ElseIf parent.EnumBuilder IsNot Nothing Then
            m_FieldBuilder = parent.EnumBuilder.DefineLiteral(Name, m_ConstantValue)
        Else
            Throw New InternalException(Me)
        End If

        Compiler.TypeManager.RegisterReflectionMember(m_FieldBuilder, Me.MemberDescriptor)

        Return result
    End Function

    Public ReadOnly Property FieldDescriptor() As FieldDescriptor Implements IFieldMember.FieldDescriptor
        Get
            Return m_Descriptor
        End Get
    End Property
End Class