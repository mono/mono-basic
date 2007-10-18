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

Public Class MissingType
    Inherits Type

    Private m_Compiler As Compiler

    Sub New(ByVal Compiler As Compiler)
        m_Compiler = Compiler
    End Sub

    Shared Operator =(ByVal t As Type, ByVal m As MissingType) As Boolean
        Return Helper.CompareType(m, t)
    End Operator

    Shared Operator <>(ByVal t As Type, ByVal m As MissingType) As Boolean
        Return Not t = m
    End Operator

    Shared Operator =(ByVal m As MissingType, ByVal t As Type) As Boolean
        Return Helper.CompareType(m, t)
    End Operator

    Shared Operator <>(ByVal m As MissingType, ByVal t As Type) As Boolean
        Return Not m = t
    End Operator

    Public Overrides Function Equals(ByVal o As Object) As Boolean
        Return TypeOf o Is MissingType
    End Function


    Public Overrides Function ToString() As String
        Return "<Missing>"
    End Function



    Public Overrides ReadOnly Property Assembly() As System.Reflection.Assembly
        Get
            Throw New NotImplementedException
        End Get
    End Property

    Public Overrides ReadOnly Property AssemblyQualifiedName() As String
        Get
            Throw New NotImplementedException
        End Get
    End Property

    Public Overrides ReadOnly Property BaseType() As System.Type
        Get
            Throw New NotImplementedException
        End Get
    End Property

    Public Overrides ReadOnly Property FullName() As String
        Get
            Throw New NotImplementedException
        End Get
    End Property

    Protected Overrides Function GetAttributeFlagsImpl() As System.Reflection.TypeAttributes
        Throw New NotImplementedException
    End Function

    Protected Overrides Function GetConstructorImpl(ByVal bindingAttr As System.Reflection.BindingFlags, ByVal binder As System.Reflection.Binder, ByVal callConvention As System.Reflection.CallingConventions, ByVal types() As System.Type, ByVal modifiers() As System.Reflection.ParameterModifier) As System.Reflection.ConstructorInfo
        Throw New NotImplementedException
    End Function

    Public Overloads Overrides Function GetConstructors(ByVal bindingAttr As System.Reflection.BindingFlags) As System.Reflection.ConstructorInfo()
        Throw New NotImplementedException
    End Function

    Public Overloads Overrides Function GetCustomAttributes(ByVal inherit As Boolean) As Object()
        Throw New NotImplementedException
    End Function

    Public Overloads Overrides Function GetCustomAttributes(ByVal attributeType As System.Type, ByVal inherit As Boolean) As Object()
        Throw New NotImplementedException
    End Function

    Public Overrides Function GetElementType() As System.Type
        Throw New NotImplementedException
    End Function

    Public Overloads Overrides Function GetEvent(ByVal name As String, ByVal bindingAttr As System.Reflection.BindingFlags) As System.Reflection.EventInfo
        Throw New NotImplementedException
    End Function

    Public Overloads Overrides Function GetEvents(ByVal bindingAttr As System.Reflection.BindingFlags) As System.Reflection.EventInfo()
        Throw New NotImplementedException
    End Function

    Public Overloads Overrides Function GetField(ByVal name As String, ByVal bindingAttr As System.Reflection.BindingFlags) As System.Reflection.FieldInfo
        Throw New NotImplementedException
    End Function

    Public Overloads Overrides Function GetFields(ByVal bindingAttr As System.Reflection.BindingFlags) As System.Reflection.FieldInfo()
        Throw New NotImplementedException
    End Function

    Public Overloads Overrides Function GetInterface(ByVal name As String, ByVal ignoreCase As Boolean) As System.Type
        Throw New NotImplementedException
    End Function

    Public Overrides Function GetInterfaces() As System.Type()
        Throw New NotImplementedException
    End Function

    Public Overloads Overrides Function GetMembers(ByVal bindingAttr As System.Reflection.BindingFlags) As System.Reflection.MemberInfo()
        Throw New NotImplementedException
    End Function

    Protected Overrides Function GetMethodImpl(ByVal name As String, ByVal bindingAttr As System.Reflection.BindingFlags, ByVal binder As System.Reflection.Binder, ByVal callConvention As System.Reflection.CallingConventions, ByVal types() As System.Type, ByVal modifiers() As System.Reflection.ParameterModifier) As System.Reflection.MethodInfo
        Throw New NotImplementedException
    End Function

    Public Overloads Overrides Function GetMethods(ByVal bindingAttr As System.Reflection.BindingFlags) As System.Reflection.MethodInfo()
        Throw New NotImplementedException
    End Function

    Public Overloads Overrides Function GetNestedType(ByVal name As String, ByVal bindingAttr As System.Reflection.BindingFlags) As System.Type
        Throw New NotImplementedException
    End Function

    Public Overloads Overrides Function GetNestedTypes(ByVal bindingAttr As System.Reflection.BindingFlags) As System.Type()
        Throw New NotImplementedException
    End Function

    Public Overloads Overrides Function GetProperties(ByVal bindingAttr As System.Reflection.BindingFlags) As System.Reflection.PropertyInfo()
        Throw New NotImplementedException
    End Function

    Protected Overrides Function GetPropertyImpl(ByVal name As String, ByVal bindingAttr As System.Reflection.BindingFlags, ByVal binder As System.Reflection.Binder, ByVal returnType As System.Type, ByVal types() As System.Type, ByVal modifiers() As System.Reflection.ParameterModifier) As System.Reflection.PropertyInfo
        Throw New NotImplementedException
    End Function

    Public Overrides ReadOnly Property GUID() As System.Guid
        Get
            Throw New NotImplementedException
        End Get
    End Property

    Protected Overrides Function HasElementTypeImpl() As Boolean
        Throw New NotImplementedException
    End Function

    Public Overloads Overrides Function InvokeMember(ByVal name As String, ByVal invokeAttr As System.Reflection.BindingFlags, ByVal binder As System.Reflection.Binder, ByVal target As Object, ByVal args() As Object, ByVal modifiers() As System.Reflection.ParameterModifier, ByVal culture As System.Globalization.CultureInfo, ByVal namedParameters() As String) As Object
        Throw New NotImplementedException
    End Function

    Protected Overrides Function IsArrayImpl() As Boolean
        Throw New NotImplementedException
    End Function

    Protected Overrides Function IsByRefImpl() As Boolean
        Throw New NotImplementedException
    End Function

    Protected Overrides Function IsCOMObjectImpl() As Boolean
        Throw New NotImplementedException
    End Function

    Public Overrides Function IsDefined(ByVal attributeType As System.Type, ByVal inherit As Boolean) As Boolean
        Throw New NotImplementedException
    End Function

    Protected Overrides Function IsPointerImpl() As Boolean
        Throw New NotImplementedException
    End Function

    Protected Overrides Function IsPrimitiveImpl() As Boolean
        Throw New NotImplementedException
    End Function

    Public Overrides ReadOnly Property [Module]() As System.Reflection.Module
        Get
            Throw New NotImplementedException
        End Get
    End Property

    Public Overrides ReadOnly Property Name() As String
        Get
            Throw New NotImplementedException
        End Get
    End Property

    Public Overrides ReadOnly Property [Namespace]() As String
        Get
            Throw New NotImplementedException
        End Get
    End Property

    Public Overrides ReadOnly Property UnderlyingSystemType() As System.Type
        Get
            Throw New NotImplementedException
        End Get
    End Property

End Class
