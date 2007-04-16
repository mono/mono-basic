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

    Private m_ResolvedType As Type

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

    Sub New(ByVal Parent As ParsedObject, ByVal Type As Type)
        MyBase.New(Parent)
        m_ResolvedType = Type
    End Sub

    Sub Init(ByVal NonArrayTypeName As NonArrayTypeName)
        m_TypeName = NonArrayTypeName
    End Sub

    Sub Init(ByVal ArrayTypeName As ArrayTypeName)
        m_TypeName = ArrayTypeName
    End Sub

    Sub Init(ByVal Type As Type)
        m_ResolvedType = Type
    End Sub

    Function Clone(Optional ByVal NewParent As ParsedObject = Nothing) As TypeName
        If NewParent Is Nothing Then NewParent = Me.Parent
        Dim result As New TypeName(NewParent)
        If Me.IsNonArrayTypeName Then
            result.Init(Me.AsNonArrayTypeName.clone)
        ElseIf Me.IsArrayTypeName Then
            result.Init(Me.AsArrayTypeName.clone)
        Else
            Throw New InternalException(Me)
        End If
        Return result
    End Function

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

    ''' <summary>
    ''' The name of this type.
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    ReadOnly Property Name() As String
        Get
            Return DirectCast(m_TypeName, INameable).Name
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
    Public ReadOnly Property ResolvedType() As Type
        Get
            Helper.Assert(m_ResolvedType IsNot Nothing)
            Return m_ResolvedType
        End Get
    End Property

    <Obsolete("No code to resolve here.")> Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Helper.Assert(m_ResolvedType IsNot Nothing)
        Return True
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

    ''' <summary>
    ''' Converts this type descriptor into a readable string representation (it's name, basically, with any ranks appended.)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Overrides Function ToString() As String
        Return Name.ToString
    End Function

End Class