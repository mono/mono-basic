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
''' TypeName ::= ArrayTypeName | NonArrayTypeName
''' </summary>
''' <remarks></remarks>
Public Class TypeName
    Inherits ParsedObject

    ''' <summary>
    ''' The name of this type descriptor
    ''' </summary>
    ''' <remarks></remarks>
    Private m_TypeName As ParsedObject

    Private m_ResolvedType As Mono.Cecil.TypeReference

    Sub New(ByVal Parent As ParsedObject, Optional ByVal NonArrayTypeName As NonArrayTypeName = Nothing, Optional ByVal ArrayTypeName As ArrayTypeName = Nothing)
        MyBase.New(Parent)
        If NonArrayTypeName IsNot Nothing AndAlso ArrayTypeName IsNot Nothing Then
            Throw New ArgumentException("Both NonArrayTypeName and ArrayTypeName cannot be specified.")
        ElseIf NonArrayTypeName IsNot Nothing Then
            Init(NonArrayTypeName)
        ElseIf ArrayTypeName IsNot Nothing Then
            Init(ArrayTypeName)
        End If
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Type As Mono.Cecil.TypeReference)
        MyBase.New(Parent)
        m_ResolvedType = Type
    End Sub

    Sub Init(ByVal NonArrayTypeName As NonArrayTypeName)
        m_TypeName = NonArrayTypeName
    End Sub

    Sub Init(ByVal ArrayTypeName As ArrayTypeName)
        m_TypeName = ArrayTypeName
    End Sub

    Sub Init(ByVal Type As Mono.Cecil.TypeReference)
        m_ResolvedType = Type
    End Sub

    ReadOnly Property AsString() As String
        Get
            If TypeOf m_TypeName Is NonArrayTypeName Then Return AsNonArrayTypeName.Name
            If TypeOf m_TypeName Is ArrayTypeName Then Return AsArrayTypeName.Name
            Return DirectCast(m_TypeName, INameable).Name
        End Get
    End Property

    ReadOnly Property IsNonArrayTypeName() As Boolean
        Get
            Return TypeOf m_TypeName Is NonArrayTypeName
        End Get
    End Property

    ReadOnly Property IsArrayTypeName() As Boolean
        Get
            Return TypeOf m_TypeName Is ArrayTypeName
        End Get
    End Property

    ReadOnly Property AsNonArrayTypeName() As NonArrayTypeName
        Get
            Helper.Assert(IsNotArray)
            Return DirectCast(m_TypeName, NonArrayTypeName)
        End Get
    End Property

    ReadOnly Property AsArrayTypeName() As ArrayTypeName
        Get
            Helper.Assert(IsArray)
            Return DirectCast(m_TypeName, ArrayTypeName)
        End Get
    End Property

    ReadOnly Property IsNotArray() As Boolean
        Get
            Return TypeOf m_TypeName Is NonArrayTypeName
        End Get
    End Property

    ''' <summary>
    ''' Returns true if this type is an array.
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    ReadOnly Property IsArray() As Boolean
        Get
            Return TypeOf m_TypeName Is ArrayTypeName
        End Get
    End Property

    ReadOnly Property TypeName() As ParsedObject
        Get
            Return m_TypeName
        End Get
    End Property

    ''' <summary>
    ''' The resolved type of the TypeName.
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public ReadOnly Property ResolvedType() As Mono.Cecil.TypeReference
        Get
            Return m_ResolvedType
        End Get
    End Property

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True
        Dim atn As ArrayTypeName
        Dim natn As NonArrayTypeName

        atn = TryCast(m_TypeName, ArrayTypeName)
        If atn IsNot Nothing Then
            result = atn.ResolveCode(Info) AndAlso result
        Else
            natn = TryCast(m_TypeName, NonArrayTypeName)
            If natn IsNot Nothing Then
                result = natn.ResolveCode(Info) AndAlso result
            End If
        End If

        Return result
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        If Me.IsArrayTypeName Then
            result = Me.AsArrayTypeName.ResolveTypeReferences AndAlso result
            m_ResolvedType = Me.AsArrayTypeName.ResolvedType
        ElseIf Me.IsNonArrayTypeName Then
            result = Me.AsNonArrayTypeName.ResolveTypeReferences AndAlso result
            m_ResolvedType = Me.AsNonArrayTypeName.ResolvedType
        ElseIf m_ResolvedType Is Nothing Then
            Throw New InternalException(Me)
        End If

        If result = False Then Return result

        Helper.Assert(m_ResolvedType IsNot Nothing)

        Return result
    End Function
End Class