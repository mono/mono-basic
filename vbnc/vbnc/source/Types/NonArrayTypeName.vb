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
''' NonArrayTypeName  ::= SimpleTypeName  |	ConstructedTypeName
''' SimpleTypeName    ::= QualifiedIdentifier  |	*BuiltInTypeName*
''' BuiltInTypeName   ::= "Object"  |  *PrimitiveTypeName*
''' PrimitiveTypeName      ::=  *NumericTypeName*  |  "Boolean" |  "Date"  |  "Char"  |  "String"
''' NumericTypeName        ::=  *IntegralTypeName*  |  *FloatingPointTypeName*  |  "Decimal"
''' IntegralTypeName       ::=  "Byte"  |  "SByte"  |  "UShort"  |  "Short"  |  "UInteger"  |  "Integer"  |  "ULong"  |  "Long"
''' FloatingPointTypeName  ::=  "Single"  |  "Double"
''' ConstructedTypeName    ::=  QualifiedIdentifier  "("  "Of"  TypeArgumentList  ")"
''' </summary>
''' <remarks></remarks>
Public Class NonArrayTypeName
    Inherits ParsedObject

    Private m_TypeName As ParsedObject

    Private m_ResolvedType As Mono.Cecil.TypeReference
    Private m_IsNullable As Boolean

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal TypeName As SimpleTypeName)
        m_TypeName = TypeName
    End Sub

    Sub Init(ByVal TypeName As ConstructedTypeName)
        m_TypeName = TypeName
    End Sub

    Property IsNullable As Boolean
        Get
            Return m_IsNullable
        End Get
        Set(ByVal value As Boolean)
            m_IsNullable = value
        End Set
    End Property

    ReadOnly Property AsString() As String
        Get
            Return ToString()
        End Get
    End Property

    ReadOnly Property IsResolved() As Boolean
        Get
            Return m_ResolvedType IsNot Nothing
        End Get
    End Property

    ReadOnly Property ResolvedType() As Mono.Cecil.TypeReference 'Descriptor
        Get
            Return m_ResolvedType
        End Get
    End Property

    ReadOnly Property ResolvedCecilType() As Mono.Cecil.TypeReference
        Get
            Return m_ResolvedType
        End Get
    End Property

    ReadOnly Property Name() As String
        Get
            If IsConstructedTypeName Then
                Return AsConstructedTypeName.Name
            ElseIf Me.IsSimpleTypeName Then
                Return AsSimpleTypeName.Name
            Else
                Throw New InternalException(Me)
            End If
        End Get
    End Property

    ReadOnly Property IsConstructedTypeName() As Boolean
        Get
            Return TypeOf m_TypeName Is ConstructedTypeName
        End Get
    End Property

    ReadOnly Property AsConstructedTypeName() As ConstructedTypeName
        Get
            Return DirectCast(m_TypeName, ConstructedTypeName)
        End Get
    End Property

    ReadOnly Property IsSimpleTypeName() As Boolean
        Get
            Return TypeOf m_TypeName Is SimpleTypeName
        End Get
    End Property

    ReadOnly Property AsSimpleTypeName() As SimpleTypeName
        Get
            Return DirectCast(m_TypeName, SimpleTypeName)
        End Get
    End Property

    ReadOnly Property TypeName() As ParsedObject
        Get
            Return m_TypeName
        End Get
    End Property

    Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        Dim stn As SimpleTypeName = TryCast(m_TypeName, SimpleTypeName)
        Dim ctn As ConstructedTypeName = TryCast(m_TypeName, ConstructedTypeName)

        If stn IsNot Nothing Then
            result = stn.ResolveTypeReferences AndAlso result
            m_ResolvedType = stn.ResolvedType
        ElseIf ctn IsNot Nothing Then
            result = ctn.ResolveTypeReferences AndAlso result
            m_ResolvedType = ctn.ResolvedType
        Else
            Throw New InternalException(Me)
        End If

        If m_IsNullable Then
            result = CecilHelper.CreateNullableType(Me, m_ResolvedType, m_ResolvedType) AndAlso result
        End If

        Helper.Assert(m_ResolvedType IsNot Nothing OrElse result = False)

        Return result
    End Function

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True
        Dim stn As SimpleTypeName
        Dim ctn As ConstructedTypeName

        stn = TryCast(m_TypeName, SimpleTypeName)
        If stn IsNot Nothing Then
            result = stn.ResolveCode(Info) AndAlso result
        Else
            ctn = TryCast(m_TypeName, ConstructedTypeName)
            If ctn IsNot Nothing Then
                result = ctn.ResolveCode(Info) AndAlso result
            End If
        End If

        Return result
    End Function

    Overrides Function ToString() As String
        If IsConstructedTypeName Then
            Return AsConstructedTypeName.Name
        ElseIf IsSimpleTypeName Then
            Return AsSimpleTypeName.Name
        Else
            Throw New InternalException(Me)
        End If
    End Function

End Class
