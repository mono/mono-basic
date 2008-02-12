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
''' SubSignature  ::=  Identifier  [  TypeParameterList  ]  [  "("  [  ParameterList  ]  ")"  ]
''' </summary>
''' <remarks></remarks>
Public Class SubSignature
    Inherits ParsedObject
    Implements INameable

    ''' <summary>
    ''' The name of the signature.
    ''' Should never be nothing once initialized.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_Identifier As Identifier
    ''' <summary>
    ''' The type parameters of the signature.
    ''' Might be nothing.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_TypeParameters As TypeParameters
    ''' <summary>
    ''' The parameters of the signature.
    ''' Should never be nothing once initialized.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_ParameterList As ParameterList

    Protected m_ReturnParameter As ParameterInfo

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Name As String, ByVal Parameters As ParameterInfo())
        MyBase.New(Parent)
        m_Identifier = New Identifier(Me, Name, Nothing, TypeCharacters.Characters.None)
        m_ParameterList = New ParameterList(Me)
        For i As Integer = 0 To Parameters.GetUpperBound(0)
            m_ParameterList.Add(Parameters(i).Name, Parameters(i).ParameterType)
        Next
        'Helper.Assert(m_Identifier IsNot Nothing)
        Helper.Assert(m_ParameterList IsNot Nothing)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Name As String, ByVal Parameters As ParameterList)
        MyBase.New(Parent)
        m_Identifier = New Identifier(Me, Name, Nothing, TypeCharacters.Characters.None)
        m_ParameterList = Parameters
        'Helper.Assert(m_Identifier IsNot Nothing)
        Helper.Assert(m_ParameterList IsNot Nothing)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Name As String, ByVal Parameters As Type())
        MyBase.New(Parent)
        m_Identifier = New Identifier(Me, Name, Nothing, TypeCharacters.Characters.None)
        m_ParameterList = New ParameterList(Me, Parameters)
        'Helper.Assert(m_Identifier IsNot Nothing)
        Helper.Assert(m_ParameterList IsNot Nothing)
    End Sub

    Sub Init(ByVal Identifier As Identifier, ByVal TypeParameters As TypeParameters, ByVal ParameterList As ParameterList)
        m_Identifier = Identifier
        m_TypeParameters = TypeParameters
        m_ParameterList = ParameterList
        'Helper.Assert(m_Identifier IsNot Nothing)
        Helper.Assert(m_ParameterList IsNot Nothing)
    End Sub

    Sub Init(ByVal Identifier As String, ByVal TypeParameters As TypeParameters, ByVal ParameterList As ParameterList)
        Me.Init(New Identifier(Me, Identifier, Nothing, TypeCharacters.Characters.None), TypeParameters, ParameterList)
    End Sub

    Overridable Function Clone(Optional ByVal NewParent As ParsedObject = Nothing) As SubSignature
        If NewParent Is Nothing Then NewParent = Me.Parent
        Dim result As New SubSignature(NewParent)
        CloneTo(result)
        Return result
    End Function

    Sub CloneTo(ByVal ClonedSignature As SubSignature)
        ClonedSignature.m_Identifier = m_Identifier
        If m_TypeParameters IsNot Nothing Then ClonedSignature.m_TypeParameters = m_TypeParameters.Clone(ClonedSignature)
        If m_ParameterList IsNot Nothing Then ClonedSignature.m_ParameterList = m_ParameterList.Clone(ClonedSignature)
    End Sub

    ReadOnly Property Identifier() As Identifier
        Get
            Return m_Identifier
        End Get
    End Property

    Overridable ReadOnly Property ReturnParameter() As ParameterInfo
        Get
            If m_ReturnParameter Is Nothing Then m_ReturnParameter = New ParameterDescriptor(Compiler.TypeCache.System_Void, 1, Me)
            Return m_ReturnParameter
        End Get
    End Property

    Overridable ReadOnly Property ReturnType() As Type
        Get
            Return Nothing
        End Get
    End Property

    ReadOnly Property Parameters() As ParameterList
        Get
            Helper.Assert(m_ParameterList IsNot Nothing)
            Return m_ParameterList
        End Get
    End Property

    ReadOnly Property TypeParameters() As TypeParameters
        Get
            Return m_TypeParameters
        End Get
    End Property

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        Me.CheckCodeNotResolved()

        Helper.Assert(m_ParameterList IsNot Nothing)

        result = m_ParameterList.ResolveCode(info) AndAlso result
        If m_TypeParameters IsNot Nothing Then result = m_TypeParameters.ResolveCode(Info) AndAlso result

        Return result
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Return ResolveTypeReferences(True)
    End Function

    Overridable Overloads Function ResolveTypeReferences(ByVal ResolveTypeParameters As Boolean) As Boolean
        Dim result As Boolean = True

        Helper.Assert(m_ParameterList IsNot Nothing)

        result = m_ParameterList.ResolveTypeReferences AndAlso result
        If ResolveTypeParameters = True AndAlso m_TypeParameters IsNot Nothing Then result = m_TypeParameters.ResolveTypeReferences AndAlso result

        Return result
    End Function

    Public ReadOnly Property Name() As String Implements INameable.Name
        Get
            Return m_Identifier.Name
        End Get
    End Property

End Class
