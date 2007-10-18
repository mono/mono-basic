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
#Const DEBUGFIELDACCESS = 0
#End If

Public Class GenericFieldDescriptor
    Inherits FieldDescriptor

    Private m_OpenFieldDescriptor As FieldDescriptor
    Private m_OpenField As FieldInfo
    Private m_ClosedType As Type
    Private m_TypeParameters As Type()
    Private m_TypeArguments As Type()

    Private m_ClosedField As FieldInfo

    Sub New(ByVal Parent As ParsedObject, ByVal OpenField As FieldInfo, ByVal TypeParameters() As Type, ByVal TypeArguments() As Type, ByVal ClosedType As Type)
        MyBase.New(Parent)

        m_OpenField = OpenField
        m_OpenFieldDescriptor = TryCast(m_OpenField, FieldDescriptor)
        m_ClosedType = ClosedType
        m_TypeParameters = TypeParameters
        m_TypeArguments = TypeArguments

    End Sub

    Public Overrides ReadOnly Property Attributes() As System.Reflection.FieldAttributes
        Get
            Dim result As FieldAttributes
            result = m_OpenField.Attributes
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property Name() As String
        Get
            Dim result As String
            result = m_OpenField.Name
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property FieldType() As System.Type
        Get
            Dim result As Type
            result = m_OpenField.FieldType
            result = Helper.ApplyTypeArguments(Parent, result, m_TypeParameters, m_TypeArguments)
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property DeclaringType() As System.Type
        Get
            Dim result As Type
            result = m_OpenField.DeclaringType
            DumpMethodInfo(result)
            Helper.Assert(result IsNot Nothing)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property FieldInReflection() As System.Reflection.FieldInfo
        Get
            If m_ClosedField Is Nothing Then
                m_ClosedType = Helper.GetTypeOrTypeBuilder(m_ClosedType)
                m_OpenField = Helper.GetFieldOrFieldBuilder(m_OpenField)
                If m_ClosedType.GetType.FullName = "System.RuntimeType" Then
                    m_ClosedField = m_ClosedType.GetField(m_OpenField.Name, BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance)
                Else
                    m_ClosedField = TypeBuilder.GetField(m_ClosedType, m_OpenField)
                End If
                Compiler.TypeManager.RegisterReflectionMember(m_ClosedField, Me)
            End If

            Return m_ClosedField
        End Get
    End Property

End Class
