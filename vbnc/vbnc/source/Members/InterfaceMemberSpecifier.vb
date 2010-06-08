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
''' InterfaceMemberSpecifier  ::=  NonArrayTypeName  "."  IdentifierOrKeyword
''' </summary>
''' <remarks></remarks>
Public Class InterfaceMemberSpecifier
    Inherits ParsedObject

    Private m_1 As NonArrayTypeName
    Private m_2 As IdentifierOrKeyword

    ''' <summary>
    ''' Resolved in ResolveTypeReferences.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_ResolvedType As Mono.Cecil.TypeReference
    ''' <summary>
    ''' Resolved in ResolveCode.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_ResolvedMember As Mono.Cecil.MemberReference

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal First As NonArrayTypeName, ByVal Second As IdentifierOrKeyword)
        m_1 = First
        m_2 = Second
    End Sub

    ReadOnly Property ResolvedMethod() As Mono.Cecil.MemberReference
        Get
            Return m_ResolvedMember
        End Get
    End Property

    ReadOnly Property ResolvedEventInfo() As Mono.Cecil.EventReference
        Get
            Return TryCast(m_ResolvedMember, Mono.Cecil.EventReference)
        End Get
    End Property

    ReadOnly Property ResolvedMethodInfo() As Mono.Cecil.MethodReference
        Get
            Return TryCast(m_ResolvedMember, Mono.Cecil.MethodReference)
        End Get
    End Property

    ReadOnly Property ResolvedPropertyInfo() As Mono.Cecil.PropertyReference
        Get
            Return TryCast(m_ResolvedMember, Mono.Cecil.PropertyReference)
        End Get
    End Property

    ReadOnly Property ResolvedType() As Mono.Cecil.TypeReference
        Get
            Return m_ResolvedType
        End Get
    End Property

    ReadOnly Property First() As NonArrayTypeName
        Get
            Return m_1
        End Get
    End Property

    ReadOnly Property Second() As IdentifierOrKeyword
        Get
            Return m_2
        End Get
    End Property

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        Dim lst As Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference)

        lst = Compiler.TypeManager.GetCache(m_ResolvedType).LookupFlattenedMembers(m_2.Name)
        'If lst.Count = 0 AndAlso m_ResolvedType.IsInterface Then
        '    lst.AddRange(Compiler.TypeManager.GetCache(m_ResolvedType).LookupMembersFlattened(m_2.Name))
        'End If
        m_ResolvedMember = MethodGroupClassification.ResolveInterfaceGroup(lst, Me.FindFirstParent(Of IMember))
        If m_ResolvedMember Is Nothing Then
            Helper.AddError(Me, "Implemented method has not the same signature as the interface method")
        End If

        Return result
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        result = m_1.ResolveTypeReferences AndAlso result

        m_ResolvedType = m_1.ResolvedType

        Return result
    End Function


    Public Function ResolveEarly() As Boolean
        Dim result As Boolean = True

        result = ResolveTypeReferences() AndAlso result
        result = ResolveCode(ResolveInfo.Default(Compiler)) AndAlso result

        Return result
    End Function
End Class
