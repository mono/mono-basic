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
''' TypeParameter  ::= 	Identifier  [  TypeParameterConstraints  ]
''' </summary>
''' <remarks></remarks>
Public Class TypeParameter
    Inherits ParsedObject
    Implements INameable

    Private m_Identifier As Identifier
    Private m_TypeParameterConstraints As TypeParameterConstraints
    Private m_GenericParameterPosition As Integer
    Private m_GenericParameterConstraints() As Type
    Private m_Builder As GenericTypeParameterBuilder
    Private m_Descriptor As New TypeParameterDescriptor(Me)

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal Identifier As Identifier, ByVal TypeParameterConstraints As TypeParameterConstraints, ByVal GenericParameterPosition As Integer)
        m_Identifier = Identifier
        m_TypeParameterConstraints = TypeParameterConstraints
        m_GenericParameterPosition = GenericParameterPosition
    End Sub

    Function Clone(Optional ByVal NewParent As ParsedObject = Nothing) As TypeParameter
        If NewParent Is Nothing Then NewParent = Me.Parent
        Dim result As New TypeParameter(NewParent)
        result.m_Identifier = m_Identifier
        If m_TypeParameterConstraints IsNot Nothing Then result.m_TypeParameterConstraints = m_TypeParameterConstraints.clone(result)
        Return result
    End Function

    ReadOnly Property TypeDescriptor() As TypeParameterDescriptor
        Get
            Return m_Descriptor
        End Get
    End Property

    ReadOnly Property GenericParameterPosition() As Integer
        Get
            Return m_GenericParameterPosition
        End Get
    End Property

    ReadOnly Property TypeParameterBuilder() As GenericTypeParameterBuilder
        Get
            Return m_Builder
        End Get
    End Property

    ReadOnly Property Identifier() As Identifier
        Get
            Return m_Identifier
        End Get
    End Property

    ReadOnly Property TypeParameterConstraints() As TypeParameterConstraints
        Get
            Return m_TypeParameterConstraints
        End Get
    End Property

    Public ReadOnly Property Name() As String Implements INameable.Name
        Get
            Return m_Identifier.Name
        End Get
    End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        Me.CheckTypeReferencesNotResolved()
        If m_TypeParameterConstraints IsNot Nothing Then
            result = m_TypeParameterConstraints.ResolveTypeReferences AndAlso result
        End If

        Return result
    End Function

    <Obsolete("No code to resolve here.")> Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        'If m_TypeParameterConstraints IsNot Nothing Then result = m_TypeParameterConstraints.ResolveCode AndAlso result

        Return result
    End Function

    ReadOnly Property GenericParameterAttributes() As GenericParameterAttributes
        Get
            Dim result As GenericParameterAttributes

            If m_TypeParameterConstraints IsNot Nothing Then
                For Each constraint As Constraint In m_TypeParameterConstraints.Constraints
                    result = result Or constraint.SpecialConstraintAttribute
                Next
            End If

            Return result
        End Get
    End Property

    Function DefineParameterConstraints(ByVal TypeParameterBuilder As GenericTypeParameterBuilder) As Boolean
        Dim result As Boolean = True

        m_Builder = TypeParameterBuilder

        Dim attributes As GenericParameterAttributes

        attributes = GenericParameterAttributes

        If m_TypeParameterConstraints IsNot Nothing Then
            Dim interfaces As New Generic.List(Of Type)
            Dim basetype As Type = Nothing
            For Each constraint As Constraint In m_TypeParameterConstraints.Constraints
                If constraint.TypeName IsNot Nothing Then
                    If constraint.TypeName.ResolvedType.IsInterface Then
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
                basetype = Helper.GetTypeOrTypeBuilder(basetype)
                m_Builder.SetBaseTypeConstraint(basetype)
#If DEBUGREFLECTION Then
                Helper.DebugReflection_AppendLine("{0}.SetBaseTypeConstraint({1})", m_Builder, basetype)
#End If
            End If
            If interfaces.Count > 0 Then
                Dim types As Type() = Helper.GetTypeOrTypeBuilders(interfaces.ToArray)
                m_Builder.SetInterfaceConstraints(types)
#If DEBUGREFLECTION Then
                types = Helper.DebugReflection_BuildArray(Of Type)(types)
                Helper.DebugReflection_AppendLine("{0}.SetInterfaceConstraints({1})", m_Builder, types)
#End If
            End If

            If basetype IsNot Nothing Then interfaces.Add(basetype)
            m_GenericParameterConstraints = interfaces.ToArray
        End If

        m_Builder.SetGenericParameterAttributes(attributes)
#If DEBUGREFLECTION Then
        Helper.DebugReflection_AppendLine("{0}.SetGenericParameterAttributes(System.Reflection.GenericParameterAttributes.{1})", m_Builder, attributes.ToString)
#End If
        Return result
    End Function

    Function GetGenericParameterConstraints() As Type()
        Return m_GenericParameterConstraints
    End Function


End Class
