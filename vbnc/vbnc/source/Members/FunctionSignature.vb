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
''' FunctionSignature  ::=  SubSignature  [  "As"  [  Attributes  ]  TypeName  ]
''' </summary>
''' <remarks></remarks>
Public Class FunctionSignature
    Inherits SubSignature
    Implements INameable

    Private m_ReturnTypeAttributes As Attributes
    Private m_TypeName As TypeName

    Private m_ReturnType As Type

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Name As String, ByVal Parameters As ParameterList, ByVal ReturnType As Type, ByVal Location As Span)
        MyBase.New(Parent, Name, Parameters)
        m_ReturnType = ReturnType
        MyBase.Location = Location
    End Sub

    Shadows Sub Init(ByVal Identifier As String, ByVal TypeParameters As TypeParameters, ByVal ParameterList As ParameterList, ByVal ReturnTypeAttributes As Attributes, ByVal TypeName As Type, ByVal Location As Span)
        MyBase.Init(Identifier, TypeParameters, ParameterList)

        m_ReturnTypeAttributes = ReturnTypeAttributes
        m_ReturnType = TypeName
        MyBase.Location = Location
    End Sub

    Shadows Sub Init(ByVal Identifier As String, ByVal TypeParameters As TypeParameters, ByVal ParameterList As ParameterList, ByVal ReturnTypeAttributes As Attributes, ByVal TypeName As TypeName, ByVal Location As Span)
        MyBase.Init(Identifier, TypeParameters, ParameterList)

        m_ReturnTypeAttributes = ReturnTypeAttributes
        m_TypeName = TypeName
        MyBase.Location = Location
    End Sub

    Shadows Sub Init(ByVal Identifier As Identifier, ByVal TypeParameters As TypeParameters, ByVal ParameterList As ParameterList, ByVal ReturnTypeAttributes As Attributes, ByVal TypeName As TypeName, ByVal Location As Span)
        MyBase.Init(Identifier, TypeParameters, ParameterList)

        m_ReturnTypeAttributes = ReturnTypeAttributes
        m_TypeName = TypeName
        MyBase.Location = Location
    End Sub

    ''' <summary>
    ''' The returned object will always be a function signature.
    ''' </summary>
    ''' <param name="NewParent"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Overrides Function Clone(Optional ByVal NewParent As ParsedObject = Nothing) As SubSignature
        If NewParent Is Nothing Then NewParent = Me.Parent
        Dim result As New FunctionSignature(NewParent)
        MyBase.CloneTo(result)
        If m_ReturnTypeAttributes IsNot Nothing Then result.m_ReturnTypeAttributes = m_ReturnTypeAttributes.clone(result)
        result.m_ReturnType = m_ReturnType

        Return result
    End Function

    ReadOnly Property ReturnTypeAttributes() As Attributes
        Get
            Return m_ReturnTypeAttributes
        End Get
    End Property

    ReadOnly Property TypeName() As TypeName
        Get
            Return m_TypeName
        End Get
    End Property

    Overrides ReadOnly Property ReturnParameter() As ParameterInfo
        Get
            If m_ReturnParameter Is Nothing Then m_ReturnParameter = New ParameterDescriptor(ReturnType, 1, Me)
            Return m_ReturnParameter
        End Get
    End Property

    Public Overrides ReadOnly Property ReturnType() As Type
        Get
            'Helper.Assert(m_ReturnType IsNot Nothing)
            Return m_ReturnType
        End Get
    End Property

    Public Overrides Function ResolveTypeReferences(ByVal ResolveTypeParameters As Boolean) As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveTypeReferences(ResolveTypeParameters) AndAlso result
        If m_ReturnTypeAttributes IsNot Nothing Then result = m_ReturnTypeAttributes.ResolveTypeReferences AndAlso result

        If m_ReturnType Is Nothing Then
            If m_TypeName IsNot Nothing Then
                result = m_TypeName.ResolveTypeReferences AndAlso result
                If result = False Then Return result
                m_ReturnType = m_TypeName.ResolvedType
            ElseIf Identifier.HasTypeCharacter Then
                m_ReturnType = TypeCharacters.TypeCharacterToType(Compiler, Identifier.TypeCharacter)
            Else
                If Me.Location.File(Compiler).IsOptionStrictOn Then
                    result = Compiler.Report.ShowMessage(Messages.VBNC30210, Me.Location) AndAlso result
                Else
                    result = Compiler.Report.ShowMessage(Messages.VBNC42024, Me.Location) AndAlso result
                End If
                m_ReturnType = Compiler.TypeCache.System_Object
            End If
        End If

        Return result
    End Function

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveCode(info) AndAlso result

        Helper.Assert(m_ReturnType IsNot Nothing OrElse Compiler.Report.Errors > 0)

        Return result
    End Function

End Class
