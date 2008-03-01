'' 
'' Visual Basic.Net Compiler
'' Copyright (C) 2004 - 2007 Rolf Bjarne Kvinge, RKvinge@novell.com
'' 
'' This library is free software; you can redistribute it and/or
'' modify it under the terms of the GNU Lesser General Public
'' License as published by the Free Software Foundation; either
'' version 2.1 of the License, or (at your option) any later version.
'' 
'' This library is distributed in the hope that it will be useful,
'' but WITHOUT ANY WARRANTY; without even the implied warranty of
'' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
'' Lesser General Public License for more details.
'' 
'' You should have received a copy of the GNU Lesser General Public
'' License along with this library; if not, write to the Free Software
'' Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
'' 
'#If DEBUG Then
'#Const DEBUGFIELDACCESS = 0
'#End If

'Public Class GenericFieldDescriptor
'    Inherits FieldDescriptor

'    Private m_OpenField As Mono.Cecil.FieldReference
'    Private m_ClosedType As Mono.Cecil.TypeReference
'    Private m_TypeParameters As Mono.Cecil.TypeReference()
'    Private m_TypeArguments As Mono.Cecil.TypeReference()

'    Private m_ClosedField As FieldInfo

'#If ENABLECECIL Then
'    Private m_ClosedFieldCecil As Mono.Cecil.FieldReference
'    Private m_OpenFieldCecil As Mono.Cecil.FieldReference
'    Private m_ClosedTypeCecil As Mono.Cecil.TypeReference
'    Private m_TypeArgumentsOriginal As Mono.Cecil.TypeReference()
'    Private m_TypeArgumentsCecil As Mono.Cecil.TypeReference()
'#End If
'    Sub New(ByVal Parent As ParsedObject, ByVal OpenField As Mono.Cecil.FieldReference, ByVal TypeParameters() As Mono.Cecil.TypeReference, ByVal TypeArguments() As Mono.Cecil.TypeReference, ByVal ClosedType As Mono.Cecil.TypeReference)
'        MyBase.New(Parent)

'        m_OpenField = OpenField
'        m_ClosedType = ClosedType
'        m_TypeParameters = TypeParameters
'        m_TypeArguments = TypeArguments
'#If ENABLECECIL Then
'        m_TypeArgumentsOriginal = m_TypeArguments
'#End If
'    End Sub

'    Public Overrides ReadOnly Property Attributes() As System.Reflection.FieldAttributes
'        Get
'            Throw New NotImplementedException
'            'Dim result As FieldAttributes
'            'result = m_OpenField.Attributes
'            'DumpMethodInfo(result)
'            'Return result
'        End Get
'    End Property

'    Public Overrides ReadOnly Property Name() As String
'        Get
'            Dim result As String
'            result = m_OpenField.Name
'            DumpMethodInfo(result)
'            Return result
'        End Get
'    End Property

'    Public Overrides ReadOnly Property FieldType() As System.Type
'        Get
'            Throw New NotImplementedException
'            'Dim result As Type
'            'result = m_OpenField.FieldType
'            'result = Helper.ApplyTypeArguments(Parent, result, m_TypeParameters, m_TypeArguments)
'            'DumpMethodInfo(result)
'            'Return result
'        End Get
'    End Property

'    Public Overrides ReadOnly Property DeclaringType() As System.Type
'        Get
'            Throw New NotImplementedException
'            'Dim result As Type
'            'result = m_OpenField.DeclaringType
'            'DumpMethodInfo(result)
'            'Helper.Assert(result IsNot Nothing)
'            'Return result
'        End Get
'    End Property

'#If ENABLECECIL Then
'    Overrides ReadOnly Property FieldInCecil() As Mono.Cecil.FieldReference
'        Get
'            If m_ClosedFieldCecil Is Nothing Then
'                m_ClosedTypeCecil = m_ClosedType
'                m_OpenFieldCecil = m_OpenField
'                m_ClosedFieldCecil = New Mono.Cecil.FieldReference(m_OpenField.Name, m_ClosedTypeCecil, m_OpenFieldCecil.FieldType)
'            End If
'            Return m_ClosedFieldCecil
'        End Get
'    End Property
'#End If

'    Public Overrides ReadOnly Property FieldInReflection() As System.Reflection.FieldInfo
'        Get
'            Throw New NotImplementedException
'            'If m_ClosedField Is Nothing Then
'            '    m_ClosedType = Helper.GetTypeOrTypeBuilder(m_ClosedType)
'            '    m_OpenField = Helper.GetFieldOrFieldBuilder(m_OpenField)
'            '    If m_ClosedType.GetType.FullName = "System.RuntimeType" Then
'            '        m_ClosedField = m_ClosedType.GetField(m_OpenField.Name, BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance)
'            '    Else
'            '        m_ClosedField = TypeBuilder.GetField(m_ClosedType, m_OpenField)
'            '    End If
'            '    Compiler.TypeManager.RegisterReflectionMember(m_ClosedField, Me)
'            'End If

'            'Return m_ClosedField
'        End Get
'    End Property

'End Class
