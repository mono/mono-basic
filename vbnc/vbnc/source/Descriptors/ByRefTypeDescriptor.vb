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

#If DEBUG Then
#Const DEBUGTYPEACCESS = 0
#End If

Public Class ByRefTypeDescriptor
    Inherits TypeDescriptor

    Private m_ElementTypeDescriptor As TypeDescriptor
    Private m_ElementType As Type

    Private m_ByRefType As Type

    Sub New(ByVal ElementType As TypeDescriptor)
        MyBase.New(ElementType.Declaration)
        Helper.Assert(ElementType.Declaration IsNot Nothing)
        m_ElementTypeDescriptor = ElementType
        m_ElementType = ElementType
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal ElementType As Type)
        MyBase.New(Parent)
        Helper.Assert(ElementType IsNot Nothing)
        m_ElementTypeDescriptor = TryCast(ElementType, TypeDescriptor)
        m_ElementType = ElementType
    End Sub

    Public Overrides Function GetElementType() As System.Type
        Helper.Assert(m_ElementType IsNot Nothing)
        MyBase.DumpMethodInfo(m_ElementType)
        Return m_ElementType
    End Function

    Protected Overrides Function HasElementTypeImpl() As Boolean
        Dim result As Boolean
        result = True
        MyBase.DumpMethodInfo(result)
        Return result
    End Function

    Protected Overrides Function IsArrayImpl() As Boolean
        Dim result As Boolean
        result = False
        MyBase.DumpMethodInfo(result)
        Return result
    End Function

    Protected Overrides Function IsByRefImpl() As Boolean
        Dim result As Boolean
        result = True
        MyBase.DumpMethodInfo(result)
        Return result
    End Function

    Protected Overrides Function IsCOMObjectImpl() As Boolean
        Dim result As Boolean
        result = False
        MyBase.DumpMethodInfo(result)
        Return result
    End Function

    Protected Overrides Function IsPointerImpl() As Boolean
        Dim result As Boolean
        result = False
        MyBase.DumpMethodInfo(result)
        Return result
    End Function

    Protected Overrides Function IsPrimitiveImpl() As Boolean
        Dim result As Boolean
        result = False
        MyBase.DumpMethodInfo(result)
        Return result
    End Function

    Protected Overrides Function IsValueTypeImpl() As Boolean
        Dim result As Boolean
        result = False
        MyBase.DumpMethodInfo(result)
        Return result
    End Function

    Public Overrides Function GetInterfaces() As System.Type()
        Dim result As type()
        result = Type.EmptyTypes
        DumpMethodInfo(result)
        Return result
    End Function

    Protected Overrides Function GetAttributeFlagsImpl() As System.Reflection.TypeAttributes
        Dim result As TypeAttributes
        result = TypeAttributes.Class ' = 0
        DumpMethodInfo(result)
        Return result
    End Function

    Public Overrides ReadOnly Property IsGenericType() As Boolean
        Get
            Dim result As Boolean
            result = False
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property IsGenericTypeDefinition() As Boolean
        Get
            Dim result As Boolean
            result = False
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Protected Overrides ReadOnly Property AllDeclaredMembers() As System.Collections.Generic.List(Of System.Reflection.MemberInfo)
        Get
            Return New Generic.List(Of MemberInfo)
        End Get
    End Property

    Protected Overrides ReadOnly Property AllMembers() As System.Collections.Generic.List(Of System.Reflection.MemberInfo)
        Get
            Return New Generic.List(Of MemberInfo)
        End Get
    End Property

    Public Overrides ReadOnly Property BaseType() As System.Type
        Get
            Dim result As Type
            result = Nothing
            MyBase.DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property FullName() As String
        Get
            Dim result As String
            If m_ElementTypeDescriptor IsNot Nothing Then
                result = m_ElementTypeDescriptor.FullName & "&"
            ElseIf m_ElementType IsNot Nothing Then
                result = m_ElementType.FullName & "&"
            Else
                Compiler.Report.ShowMessage(Messages.VBNC30747, Me.m_ElementTypeDescriptor.Declaration.Location)
                result = "&"
            End If
            MyBase.DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property MetadataToken() As Integer
        Get
            Throw New NotSupportedException
        End Get
    End Property

    Public Overrides ReadOnly Property Name() As String
        Get
            Dim result As String
            If m_ElementTypeDescriptor IsNot Nothing Then
                result = m_ElementTypeDescriptor.Name & "&"
            ElseIf m_ElementType IsNot Nothing Then
                result = m_ElementType.Name & "&"
            Else
                Compiler.Report.ShowMessage(Messages.VBNC30747, Me.m_ElementTypeDescriptor.Declaration.Location)
                result = "&"
            End If
            MyBase.DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property UnderlyingSystemType() As System.Type
        Get
            Dim result As Type
            If m_ByRefType Is Nothing Then CreateType()
            result = m_ByRefType
            MyBase.DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Private Sub CreateType()
        Helper.Assert(m_ByRefType Is Nothing)
        Helper.Assert(m_ElementTypeDescriptor IsNot Nothing OrElse m_elementtype IsNot Nothing)

        Dim tmp As Type = Nothing
        If m_ElementTypeDescriptor IsNot Nothing Then
            tmp = Helper.GetTypeOrTypeBuilder(m_ElementTypeDescriptor)
        ElseIf m_ElementType IsNot Nothing Then
            tmp = Helper.GetTypeOrTypeBuilder(m_ElementType)
        End If
        Helper.Assert(tmp IsNot Nothing)
        m_ByRefType = tmp.MakeByRefType()

        Compiler.TypeManager.RegisterReflectionType(m_ByRefType, Me)
    End Sub

    Public Overrides ReadOnly Property TypeInReflection() As System.Type
        Get
            Return UnderlyingSystemType
        End Get
    End Property
End Class
