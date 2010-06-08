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
''' An expression with this classification can only appear as the left side
''' of a member access. In any other context an expression classified as
''' a type causes a compile-time error.
''' </summary>
''' <remarks></remarks>
Public Class TypeClassification
    Inherits ExpressionClassification

    Private m_Type As Mono.Cecil.TypeDefinition 'Descriptor
    Private m_TypeParameter As TypeParameter
    Private m_Group As MyGroupData

    ReadOnly Property MyGroup() As MyGroupData
        Get
            If m_Group Is Nothing Then
                If Not CanBeExpression() Then Return Nothing
            End If
            Return m_Group
        End Get
    End Property

    ReadOnly Property Expression() As Expression
        Get
            If m_Group Is Nothing Then
                If Not CanBeExpression() Then Return Nothing
                If m_Group Is Nothing Then Return Nothing
            End If
            Return m_Group.DefaultInstanceAlias
        End Get
    End Property

    Function CreateAliasExpression(ByVal SharedExpression As Expression, ByRef result As Expression) As Boolean
        Dim sne As SimpleNameExpression = TryCast(SharedExpression, SimpleNameExpression)
        Dim mae As MemberAccessExpression
        Dim maeIE As MemberAccessExpression

        If TypeOf SharedExpression.Parent Is Is_IsNotExpression Then
            Dim fieldLoad As New LoadFieldExpression(SharedExpression, DirectCast(m_Type.Annotations(Compiler), TypeDeclaration).MyGroupField.FieldBuilder, m_Group.DefaultInstanceAlias)
            result = fieldLoad
        Else
            If sne IsNot Nothing Then
                maeIE = New MemberAccessExpression(SharedExpression.Parent)
                maeIE.Init(Expression, New IdentifierOrKeyword(SharedExpression.Parent, Token.CreateIdentifierToken(sne.Location, sne.Identifier.Identifier)))
            Else
                mae = TryCast(SharedExpression, MemberAccessExpression)
                maeIE = New MemberAccessExpression(SharedExpression.Parent)
                maeIE.Init(Expression, mae.SecondExpression)
            End If
            result = maeIE
        End If
        Return result.ResolveExpression(ResolveInfo.Default(SharedExpression.Compiler))
    End Function

    ReadOnly Property CanBeExpression() As Boolean
        Get
            Dim result As Boolean = False

            If m_Group IsNot Nothing Then Return m_Group.DefaultInstanceAlias IsNot Nothing

            If Compiler.Assembly.GroupedClasses Is Nothing Then Return False

            If m_Type Is Nothing Then Return False

            For Each data As MyGroupData In Compiler.Assembly.GroupedClasses
                If data.DefaultInstanceAlias Is Nothing Then Continue For
                If data.TypeToCollect Is Nothing Then Continue For
                If Helper.IsSubclassOf(data.TypeToCollect, m_Type) = False Then Continue For

                m_Group = data

                Return True
            Next

            Return False
        End Get
    End Property

    ReadOnly Property IsTypeParameter() As Boolean
        Get
            Return m_TypeParameter IsNot Nothing
        End Get
    End Property

    Property Type() As Mono.Cecil.TypeDefinition 'Descriptor
        Get
            Return m_Type
        End Get
        Set(ByVal value As Mono.Cecil.TypeDefinition) 'Descriptor)
            m_Type = value
        End Set
    End Property

    Sub New(ByVal Parent As ParsedObject, ByVal Type As TypeDeclaration)
        MyBase.new(Classifications.Type, Parent)
        m_Type = Type.CecilType
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal TypeParameter As TypeParameter)
        MyBase.New(Classifications.Type, Parent)
        m_TypeParameter = TypeParameter
    End Sub

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Classifications.Type, Parent)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Type As Object)
        Me.new(Parent)
        If TypeOf Type Is Mono.Cecil.TypeDefinition Then
            m_Type = DirectCast(Type, Mono.Cecil.TypeDefinition)
            'ElseIf TypeOf Type Is Type Then
            '    m_Type = DirectCast(Type, Type) ' New TypeDescriptor(DirectCast(Type, Type))
        ElseIf TypeOf Type Is TypeDeclaration Then
            m_Type = DirectCast(Type, TypeDeclaration).CecilType
        ElseIf TypeOf Type Is TypeParameter Then
            m_TypeParameter = DirectCast(Type, TypeParameter)
        Else
            Throw New InternalException(Me)
        End If
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Type As Mono.Cecil.TypeDefinition)
        MyBase.New(Classifications.Type, Parent)
        m_Type = Type
    End Sub
End Class
