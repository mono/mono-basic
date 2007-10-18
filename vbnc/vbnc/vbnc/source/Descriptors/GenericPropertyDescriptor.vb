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
#Const DEBUGMETHODACCESS = 0
#End If

Public Class GenericPropertyDescriptor
    Inherits PropertyDescriptor

    Private m_ClosedType As Type
    Private m_OpenPropertyDescriptor As PropertyDescriptor
    Private m_OpenProperty As PropertyInfo
    Private m_ClosedPropertyDescriptor As PropertyDescriptor
    Private m_ClosedProperty As PropertyInfo

    Private m_TypeParameters As Type()
    Private m_TypeArguments As Type()

    Sub New(ByVal Parent As ParsedObject, ByVal OpenProperty As PropertyInfo, ByVal TypeParameters() As Type, ByVal TypeArguments() As Type, ByVal ClosedType As Type)
        MyBase.New(Parent)

        m_OpenProperty = OpenProperty
        m_OpenPropertyDescriptor = TryCast(m_OpenProperty, PropertyDescriptor)
        m_ClosedType = ClosedType
        m_TypeParameters = TypeParameters
        m_TypeArguments = TypeArguments

    End Sub

    Public Overrides ReadOnly Property IsDefault() As Boolean
        Get
            If m_OpenPropertyDescriptor IsNot Nothing Then
                Return m_OpenPropertyDescriptor.IsDefault
            Else
                Dim memberAttrib As DefaultMemberAttribute
                memberAttrib = Helper.GetDefaultMemberAttribute(m_OpenProperty.DeclaringType)
                Return memberAttrib IsNot Nothing AndAlso Helper.CompareName(memberAttrib.MemberName, Me.Name)
            End If
        End Get
    End Property

    Public Overrides ReadOnly Property IsShared() As Boolean
        Get
            If m_OpenPropertyDescriptor IsNot Nothing Then
                Return m_OpenPropertyDescriptor.IsShared
            Else
                Return CBool(Helper.GetPropertyAttributes(m_OpenProperty) And MethodAttributes.Static)
            End If
        End Get
    End Property

    Public Overrides Function GetGetMethod(ByVal nonPublic As Boolean) As System.Reflection.MethodInfo
        Static result As MethodInfo = Nothing

        If result Is Nothing Then
            Dim tmp As MethodInfo = m_OpenProperty.GetGetMethod(nonPublic)
            If tmp IsNot Nothing Then
                result = Parent.Compiler.TypeManager.MakeGenericMethod(Me.Parent, tmp, m_TypeParameters, m_TypeArguments, m_ClosedType)
            End If
        End If

        DumpMethodInfo(result)
        Return result
    End Function

    Public Overrides Function GetSetMethod(ByVal nonPublic As Boolean) As System.Reflection.MethodInfo
        Static result As MethodInfo = Nothing

        If result Is Nothing Then
            Dim tmp As MethodInfo = m_OpenProperty.GetSetMethod(nonPublic)
            If tmp IsNot Nothing Then
                result = Parent.Compiler.TypeManager.MakeGenericMethod(Me.Parent, tmp, m_TypeParameters, m_TypeArguments, m_ClosedType)
            End If
        End If

        DumpMethodInfo(result)
        Return result
    End Function

    Public Overrides ReadOnly Property PropertyType() As System.Type
        Get
            Static result As Type
            If result Is Nothing Then
                result = m_OpenProperty.PropertyType
                result = Helper.ApplyTypeArguments(Me.Parent, result, m_TypeParameters, m_TypeArguments)
            End If
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides Function GetIndexParameters() As System.Reflection.ParameterInfo()
        Static result As ParameterInfo()

        If result Is Nothing Then
            result = m_OpenProperty.GetIndexParameters
            result = Helper.ApplyTypeArguments(Me.Parent, result, m_TypeParameters, m_TypeArguments)
        End If

        DumpMethodInfo(result)
        Return result
    End Function

    Public Overrides ReadOnly Property Attributes() As System.Reflection.PropertyAttributes
        Get
            Dim result As PropertyAttributes
            result = m_OpenProperty.Attributes
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property Name() As String
        Get
            Dim result As String
            result = m_OpenProperty.Name
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property DeclaringType() As System.Type
        Get
            Dim result As Type
            result = m_ClosedType
            DumpMethodInfo(result)
            Helper.Assert(result IsNot Nothing)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property PropertyInReflection() As System.Reflection.PropertyInfo
        Get
            If m_ClosedProperty Is Nothing Then
                Try
                    m_ClosedType = Helper.GetTypeOrTypeBuilder(m_ClosedType)
                    m_OpenProperty = Helper.GetPropertyOrPropertyBuilder(m_OpenProperty)
                    If m_ClosedType.GetType.FullName = "System.RuntimeType" Then
                        m_ClosedProperty = m_ClosedType.GetProperty(m_OpenProperty.Name, BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance)
                    Else
                        'There is no such thing as a generic property in the reflection api
                        'the reflection objects are created with the get and set accessors
                        m_ClosedProperty = Me
                    End If
                    Compiler.TypeManager.RegisterReflectionMember(m_ClosedProperty, Me)
                Catch ex As Exception
                    Parent.Compiler.ShowExceptionInfo(ex)
                    Helper.StopIfDebugging()
                    Throw
                End Try
            End If

            Return m_ClosedProperty
        End Get
    End Property

End Class
