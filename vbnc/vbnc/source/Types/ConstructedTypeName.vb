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
''' ConstructedTypeName ::=	QualifiedIdentifier  "("  "Of"  TypeArgumentList  ")"
''' </summary>
''' <remarks></remarks>
Public Class ConstructedTypeName
    Inherits ParsedObject

    Private m_QualifiedIdentifier As QualifiedIdentifier
    Private m_TypeArgumentList As TypeArgumentList

    Private m_ResolvedType As Type
    Private m_OpenResolvedType As Type
    Private m_ClosedResolvedType As Type

    Sub New(ByVal Parent As ParsedObject)
        MyBase.new(Parent)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal QualifiedIdentifier As QualifiedIdentifier, ByVal TypeArgumentList As TypeArgumentList)
        MyBase.new(Parent)
        m_QualifiedIdentifier = QualifiedIdentifier
        m_TypeArgumentList = TypeArgumentList
    End Sub

    Sub Init(ByVal QualifiedIdentifier As QualifiedIdentifier, ByVal TypeArgumentList As TypeArgumentList)
        m_QualifiedIdentifier = QualifiedIdentifier
        m_TypeArgumentList = TypeArgumentList
    End Sub

    Function Clone(Optional ByVal NewParent As ParsedObject = Nothing) As ConstructedTypeName
        If NewParent Is Nothing Then NewParent = Me.Parent
        Dim result As New ConstructedTypeName(NewParent)
        result.Init(m_QualifiedIdentifier.Clone(result), m_TypeArgumentList.clone(result))
        Return result
    End Function

    ReadOnly Property OpenResolvedType() As Type
        Get
            Return m_OpenResolvedType
        End Get
    End Property

    ReadOnly Property ClosedResolvedType() As Type
        Get
            Return m_ClosedResolvedType
        End Get
    End Property

    ReadOnly Property TypeArgumentList() As TypeArgumentList
        Get
            Return m_TypeArgumentList
        End Get
    End Property

    ReadOnly Property QualifiedIdentifier() As QualifiedIdentifier
        Get
            Return m_QualifiedIdentifier
        End Get
    End Property

    ReadOnly Property ResolvedType() As Type
        Get
            Return m_ResolvedType
        End Get
    End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        result = m_TypeArgumentList.ResolveTypeReferences AndAlso result

        Dim nri As New TypeNameResolutionInfo(Me, Me, m_TypeArgumentList.Count)
        result = nri.Resolve AndAlso result

        If result = False Then Return result

        If nri.FoundOnlyOneObject Then
            If nri.FoundIs(Of TypeDescriptor)() Then
                m_OpenResolvedType = nri.FoundAs(Of TypeDescriptor)()
            ElseIf nri.FoundIs(Of IType)() Then
                m_OpenResolvedType = nri.FoundAs(Of IType).TypeDescriptor
            ElseIf nri.FoundIs(Of Type)() Then
                m_OpenResolvedType = nri.FoundAsType
            Else
                Helper.AddError(Me)
            End If
            Dim GenericArguments() As Type
            GenericArguments = m_TypeArgumentList.AsTypeArray
            m_ClosedResolvedType = Compiler.TypeManager.MakeGenericType(Me, m_OpenResolvedType, GenericArguments)
            m_ResolvedType = m_ClosedResolvedType
        Else
            Helper.AddError(Me)
        End If

        Return result
    End Function

    ReadOnly Property Name() As String
        Get
            Return m_QualifiedIdentifier.Name
        End Get
    End Property

End Class
