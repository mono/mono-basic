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

Public Class ArrayTypeDescriptor
    Inherits TypeDescriptor

    Private m_Ranks As Integer
    Private m_ElementTypeDescriptor As TypeDescriptor
    Private m_ElementType As Type
    Private m_ArrayType As Type
    Private m_FullName As String

    Private m_AllMembers As Generic.List(Of MemberInfo)
    Private m_AllDeclaredMembers As Generic.List(Of MemberInfo)

    Sub New(ByVal ElementType As TypeDescriptor, ByVal Ranks As Integer)
        Me.New(ElementType.Parent, ElementType, Ranks)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal ElementType As Type, ByVal Ranks As Integer)
        MyBase.New(Parent)
        Helper.Assert(ElementType IsNot Nothing)
        m_ElementType = ElementType
        m_ElementTypeDescriptor = TryCast(m_ElementType, TypeDescriptor)
        m_Ranks = Ranks
    End Sub

    Sub New(ByVal ElementType As TypeParameterDescriptor, ByVal Ranks As Integer)
        MyBase.New(ElementType.TypeParameter)
        Helper.Assert(ElementType.TypeParameter IsNot Nothing)
        m_ElementType = ElementType
        m_Ranks = Ranks
    End Sub

    Public Overrides ReadOnly Property [Namespace]() As String
        Get
            Return m_ElementType.Namespace
        End Get
    End Property

    Public Overrides ReadOnly Property ContainsGenericParameters() As Boolean
        Get
            Dim result As Boolean

            result = m_ElementType.ContainsGenericParameters OrElse m_ElementType.IsGenericParameter

            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property IsGenericParameter() As Boolean
        Get
            Dim result As Boolean

            result = False

            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Protected Overrides ReadOnly Property AllDeclaredMembers() As System.Collections.Generic.List(Of System.Reflection.MemberInfo)
        Get
            If m_AllDeclaredMembers Is Nothing Then
                m_AllDeclaredMembers = New Generic.List(Of MemberInfo)
            End If
            Return m_AllDeclaredMembers
        End Get
    End Property

    Protected Overrides ReadOnly Property AllMembers() As System.Collections.Generic.List(Of System.Reflection.MemberInfo)
        Get
            If m_AllMembers Is Nothing Then
                m_AllMembers = New Generic.List(Of MemberInfo)
                Helper.AddMembers(Compiler, Me, m_AllMembers, Helper.GetBaseMembers(Compiler, Me))
            End If

            Return m_AllMembers
        End Get
    End Property

    Protected Overrides Function GetAttributeFlagsImpl() As System.Reflection.TypeAttributes
        Dim result As TypeAttributes

        result = TypeAttributes.AutoLayout Or TypeAttributes.AnsiClass Or TypeAttributes.Class Or TypeAttributes.Sealed
        If m_ElementType.IsGenericParameter = False Then
            result = result Or m_ElementType.Attributes
        Else
            result = result Or TypeAttributes.Public
        End If

        DumpMethodInfo(result)
        Return result
    End Function

    Public Overrides ReadOnly Property IsGenericTypeDefinition() As Boolean
        Get
            Dim result As Boolean
            result = False
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides Function GetInterfaces() As System.Type()
        Dim result As New Generic.List(Of Type)

        result.AddRange(Compiler.TypeCache.System_Array.GetInterfaces)

        Dim typeArgs As Type() = New Type() {m_ElementType}
        result.Add(Compiler.TypeManager.MakeGenericType(Me.Parent, Compiler.TypeCache.System_Collections_Generic_IList1, typeArgs))
        result.Add(Compiler.TypeManager.MakeGenericType(Me.Parent, Compiler.TypeCache.System_Collections_Generic_ICollection1, typeArgs))
        result.Add(Compiler.TypeManager.MakeGenericType(Me.Parent, Compiler.TypeCache.System_Collections_Generic_IEnumerable1, typeArgs))

        Return result.ToArray
    End Function

    Public Overrides Function GetArrayRank() As Integer
        Dim result As Integer
        result = m_Ranks
        MyBase.DumpMethodInfo(result)
        Return result
    End Function

    Public Overrides Function GetElementType() As System.Type
        Dim result As Type
        result = m_ElementType
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
        result = True
        MyBase.DumpMethodInfo(result)
        Return result
    End Function

    Protected Overrides Function IsByRefImpl() As Boolean
        Dim result As Boolean
        result = False
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

    Public Overrides ReadOnly Property BaseType() As System.Type
        Get
            Dim result As Type
            result = Compiler.TypeCache.System_Array
            MyBase.DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property FullName() As String
        Get
            Dim result As String

            If m_FullName Is Nothing Then
                If TypeOf m_ElementType Is TypeParameterDescriptor Then
                    result = Nothing ' result = m_ElementType.Name & "[" & New String(","c, m_Ranks - 1) & "]"
                Else
                    result = String.Concat(m_ElementType.FullName, "[", New String(","c, m_Ranks - 1), "]")
                End If
                m_FullName = result
            Else
                result = m_FullName
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
            result = m_ElementType.Name & "[" & New String(","c, m_Ranks - 1) & "]"
            MyBase.DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property UnderlyingSystemType() As System.Type
        Get
            Dim result As Type
            If m_ArrayType Is Nothing Then CreateType()
            result = m_ArrayType
            MyBase.DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property IsGenericType() As Boolean
        Get
            Dim result As Boolean
            result = False
            MyBase.DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Private Sub CreateType()
        Helper.Assert(m_ArrayType Is Nothing)
        Helper.Assert(m_ElementType IsNot Nothing)
        Dim tmp As Type = Helper.GetTypeOrTypeBuilder(m_ElementType)
        Helper.Assert(tmp IsNot Nothing)
        If m_Ranks = 1 Then
            m_ArrayType = tmp.MakeArrayType()
        Else
            m_ArrayType = tmp.MakeArrayType(m_Ranks)
        End If
        Compiler.TypeManager.RegisterReflectionType(m_ArrayType, Me)
        'Helper.Assert(Helper.CompareName(Me.FullName, m_ArrayType.FullName))
    End Sub

    Public Overrides ReadOnly Property TypeInReflection() As System.Type
        Get
            Return UnderlyingSystemType
        End Get
    End Property

    Public Overrides Function IsAssignableFrom(ByVal c As System.Type) As Boolean
        Dim result As Boolean

        If c.IsArray = False Then
            result = False
        ElseIf Helper.CompareType(Me.GetElementType, c.GetElementType) Then
            result = True
        Else
            Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Declaration.Location)
            result = False
        End If

        Return result
    End Function
End Class
