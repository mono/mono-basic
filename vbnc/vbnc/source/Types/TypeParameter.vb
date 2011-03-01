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
''' TypeParameter  ::= 	Identifier  [  TypeParameterConstraints  ]
''' </summary>
''' <remarks></remarks>
Public Class TypeParameter
    Inherits ParsedObject
    Implements INameable

    Private m_Identifier As Identifier
    Private m_TypeParameterConstraints As TypeParameterConstraints
    Private m_Defined As Boolean
    Private m_GenericParameterPosition As Integer
    Private m_CecilBuilder As Mono.Cecil.GenericParameter

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Public Overrides Function CreateDefinition() As Boolean
        Dim result As Boolean = True

        result = MyBase.CreateDefinition() AndAlso result

        If m_CecilBuilder IsNot Nothing Then Return result

        Dim p As BaseObject = Me.Parent
        Dim owner As Mono.Cecil.IGenericParameterProvider = Nothing
        While p IsNot Nothing AndAlso owner Is Nothing
            Dim tD As TypeDeclaration = TryCast(p, TypeDeclaration)
            Dim mD As MethodBaseDeclaration = TryCast(p, MethodBaseDeclaration)

            If tD IsNot Nothing Then
                owner = tD.CecilType
                Exit While
            ElseIf mD IsNot Nothing Then
                owner = mD.CecilBuilder
                Exit While
            Else
                p = p.Parent
            End If
        End While

        Helper.Assert(owner IsNot Nothing)

        m_CecilBuilder = New Mono.Cecil.GenericParameter(m_Identifier.Identifier, owner)
        m_CecilBuilder.Annotations.Add(Compiler, Me)
        owner.GenericParameters.Add(m_CecilBuilder)

        Return result
    End Function

    ReadOnly Property CecilBuilder() As Mono.Cecil.GenericParameter
        Get
            Return m_CecilBuilder
        End Get
    End Property

    Shared Function Clone(ByVal Builder As Mono.Cecil.GenericParameter, ByVal Owner As Mono.Cecil.IGenericParameterProvider, ByVal Position As Integer) As Mono.Cecil.GenericParameter
        Dim result As New Mono.Cecil.GenericParameter(Builder.Name, Owner)

        For i As Integer = 0 To Builder.Constraints.Count - 1
            result.Constraints.Add(Builder.Constraints(i))
        Next

        result.HasDefaultConstructorConstraint = Builder.HasDefaultConstructorConstraint
        result.HasNotNullableValueTypeConstraint = Builder.HasNotNullableValueTypeConstraint
        result.HasReferenceTypeConstraint = Builder.HasReferenceTypeConstraint
        result.IsContravariant = Builder.IsContravariant
        result.IsCovariant = Builder.IsCovariant
        result.IsNonVariant = Builder.IsNonVariant

        Return result
    End Function

    Property Identifier() As Identifier
        Get
            Return m_Identifier
        End Get
        Set(ByVal value As Identifier)
            Helper.Assert(value IsNot Nothing)
            Helper.Assert(m_Identifier Is Nothing)
            m_Identifier = value
        End Set
    End Property

    ReadOnly Property TypeParameterConstraintsNullable() As TypeParameterConstraints
        Get
            Return m_TypeParameterConstraints
        End Get
    End Property

    Property TypeParameterConstraints() As TypeParameterConstraints
        Get
            If m_TypeParameterConstraints Is Nothing Then
                m_TypeParameterConstraints = New TypeParameterConstraints(Me)
            End If
            Return m_TypeParameterConstraints
        End Get
        Set(ByVal value As TypeParameterConstraints)
            Helper.Assert(value IsNot Nothing)
            Helper.Assert(m_TypeParameterConstraints Is Nothing)
            m_TypeParameterConstraints = value
        End Set
    End Property

    Property GenericParameterPosition() As Integer
        Get
            Return m_GenericParameterPosition
        End Get
        Set(ByVal value As Integer)
            m_GenericParameterPosition = value
        End Set
    End Property

    Public Property Name() As String Implements INameable.Name
        Get
            Return m_Identifier.Name
        End Get
        Set(ByVal value As String)
            m_Identifier.Name = value
        End Set
    End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        If m_TypeParameterConstraints IsNot Nothing Then
            result = m_TypeParameterConstraints.ResolveTypeReferences AndAlso result
            result = DefineParameterConstraints() AndAlso result
        End If
        Return result
    End Function

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        Return result
    End Function

    ReadOnly Property GenericParameterAttributes() As Mono.Cecil.GenericParameterAttributes
        Get
            Dim result As Mono.Cecil.GenericParameterAttributes

            If m_TypeParameterConstraints IsNot Nothing Then
                For i As Integer = 0 To m_TypeParameterConstraints.Constraints.Count - 1
                    result = result Or m_TypeParameterConstraints.Constraints(i).SpecialConstraintAttribute
                Next
            End If

            Return result
        End Get
    End Property

    Function DefineParameterConstraints() As Boolean
        Dim result As Boolean = True
        Dim attributes As Mono.Cecil.GenericParameterAttributes

        If m_Defined Then Return True
        m_Defined = True

        attributes = GenericParameterAttributes

        If m_TypeParameterConstraints IsNot Nothing Then
            Dim interfaces As New Generic.List(Of Mono.Cecil.TypeReference)
            Dim basetype As Mono.Cecil.TypeReference = Nothing
            For Each constraint As Constraint In m_TypeParameterConstraints.Constraints
                If constraint.TypeName IsNot Nothing Then
                    If Helper.IsInterface(Compiler, constraint.TypeName.ResolvedType) Then
                        interfaces.Add(constraint.TypeName.ResolvedType)
                    Else
                        If basetype IsNot Nothing Then
                            result = Helper.AddError(Me) AndAlso result
                            result = False
                        Else
                            basetype = constraint.TypeName.ResolvedType
                        End If
                    End If
                End If
            Next
            If basetype IsNot Nothing Then
                basetype = Helper.GetTypeOrTypeBuilder(Compiler, basetype)
                m_CecilBuilder.Constraints.Add(Helper.GetTypeOrTypeReference(Compiler, basetype))
            End If
            If interfaces.Count > 0 Then
                For i As Integer = 0 To interfaces.Count - 1
                    m_CecilBuilder.Constraints.Add(Helper.GetTypeOrTypeReference(Compiler, interfaces(i)))
                Next
            End If
        End If

        m_CecilBuilder.Attributes = CType(attributes, Mono.Cecil.GenericParameterAttributes)
        Return result
    End Function
End Class
