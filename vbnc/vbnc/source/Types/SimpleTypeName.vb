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
''' SimpleTypeName ::= QualifiedIdentifier | BuiltInTypeName
''' </summary>
''' <remarks></remarks>
Public Class SimpleTypeName
    Inherits ParsedObject

    Private m_TypeName As ParsedObject

    Private m_ResolvedType As Mono.Cecil.TypeReference
    Private m_TypeParameter As TypeParameter

    Sub New(ByVal Parent As ParsedObject)
        MyBase.new(Parent)
    End Sub

    Sub Init(ByVal QualifiedIdentifier As QualifiedIdentifier)
        Helper.Assert(QualifiedIdentifier IsNot Nothing)
        m_TypeName = QualifiedIdentifier
    End Sub

    Sub Init(ByVal BuiltInTypeName As BuiltInTypeName)
        Helper.Assert(BuiltInTypeName IsNot Nothing)
        m_TypeName = BuiltInTypeName
    End Sub

    Friend Sub ChangeQualifiedIdentifier(ByVal qi As QualifiedIdentifier)
        Helper.Assert(IsQualifiedIdentifier)
        'Helper.Assert(AsQualifiedIdentifier.Second IsNot Nothing)
        Helper.Assert(AsQualifiedIdentifier.First Is qi)
        m_TypeName = qi
    End Sub

    ReadOnly Property TypeParameter() As TypeParameter
        Get
            Return m_TypeParameter
        End Get
    End Property

    ReadOnly Property ResolvedType() As Mono.Cecil.TypeReference 'Descriptor
        Get
            Return m_ResolvedType
        End Get
    End Property

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        If m_TypeName IsNot Nothing Then result = m_TypeName.ResolveCode(Info) AndAlso result
        If m_TypeParameter IsNot Nothing Then result = m_TypeParameter.ResolveCode(Info) AndAlso result

        Return result
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Return ResolveTypeReferences(False)
    End Function

    Overloads Function ResolveTypeReferences(ByVal AsAttributeTypeName As Boolean) As Boolean
        Dim result As Boolean = True
        If IsBuiltInTypeName Then
            'Not necessary.'result = AsBuiltInTypeName.ResolveTypeReferences AndAlso result
            m_ResolvedType = AsBuiltInTypeName.ResolvedType
        ElseIf IsQualifiedIdentifier Then
            Dim tpParam As Mono.Cecil.GenericParameter
            result = AsQualifiedIdentifier.ResolveAsTypeName(AsAttributeTypeName) AndAlso result
            If result = False Then Return result
            m_ResolvedType = AsQualifiedIdentifier.ResolvedType
            tpParam = TryCast(m_ResolvedType, Mono.Cecil.GenericParameter)
            If tpParam IsNot Nothing Then
                m_TypeParameter = DirectCast(tpParam.Annotations(Compiler), TypeParameter)
            End If
            If result = False Then Return result
            If Not TypeOf m_ResolvedType Is Mono.Cecil.GenericInstanceType AndAlso m_ResolvedType.GenericParameters.Count > 0 Then
                Dim tmp As New Mono.Cecil.GenericInstanceType(m_ResolvedType)
                For i As Integer = 0 To m_ResolvedType.GenericParameters.Count - 1
                    tmp.GenericArguments.Add(m_ResolvedType.GenericParameters(i))
                Next
                m_ResolvedType = tmp
            End If
        Else
            Throw New InternalException(Me)
        End If
        Return result

    End Function

    ReadOnly Property TypeName() As ParsedObject
        Get
            Return m_TypeName
        End Get
    End Property

    ReadOnly Property Name() As String
        Get
            If IsBuiltInTypeName Then
                Return AsBuiltInTypeName.Name
            ElseIf IsQualifiedIdentifier Then
                Return AsQualifiedIdentifier.Name
            Else
                Throw New InternalException(Me)
            End If
        End Get
    End Property

    ReadOnly Property IsBuiltInTypeName() As Boolean
        Get
            Return TypeOf m_TypeName Is BuiltInTypeName
        End Get
    End Property

    ReadOnly Property AsBuiltInTypeName() As BuiltInTypeName
        Get
            Return DirectCast(m_TypeName, BuiltInTypeName)
        End Get
    End Property

    ReadOnly Property IsQualifiedIdentifier() As Boolean
        Get
            Return TypeOf m_TypeName Is QualifiedIdentifier
        End Get
    End Property

    ReadOnly Property AsQualifiedIdentifier() As QualifiedIdentifier
        Get
            Return DirectCast(m_TypeName, QualifiedIdentifier)
        End Get
    End Property

End Class
