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
''' PropertySignature  ::=  SubSignature  [  "As"  [ New ] [  Attributes  ]  TypeName  [  (  ArgumentList  )  ] ]
''' </summary>
''' <remarks></remarks>
Public Class PropertySignature
    Inherits FunctionSignature

    Private m_AsNew As Boolean
    Private m_AsNewLocation As Span
    Private m_ArgumentList As ArgumentList

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Name As String, ByVal Parameters As ParameterList, ByVal ReturnType As Mono.Cecil.TypeReference, ByVal Location As Span)
        MyBase.New(Parent, Name, Parameters, ReturnType, Location)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Name As String, ByVal Parameters As ParameterList, ByVal ReturnType As TypeName, ByVal Location As Span)
        MyBase.New(Parent, Name, Parameters, ReturnType, Location)
    End Sub

    Shadows Sub Init(ByVal Identifier As Identifier, ByVal TypeParameters As TypeParameters, ByVal ParameterList As ParameterList, ByVal ReturnTypeAttributes As Attributes, ByVal TypeName As TypeName, ByVal Location As Span, ByVal AsNew As Boolean, ByVal AsNewLocation As Span, ByVal ArgumentList As ArgumentList)
        MyBase.Init(Identifier, TypeParameters, ParameterList, ReturnTypeAttributes, TypeName, Location)
        m_AsNew = AsNew
        m_AsNewLocation = AsNewLocation
        m_ArgumentList = ArgumentList
    End Sub

    Public Overrides Function ResolveCode(Info As ResolveInfo) As Boolean

        Dim result As Boolean = MyBase.ResolveCode(Info)

        If m_ArgumentList IsNot Nothing Then
            result = m_ArgumentList.ResolveCode(Info) AndAlso result
        End If

        Return result

    End Function

    ReadOnly Property AsNew As Boolean
        Get
            Return m_AsNew
        End Get
    End Property

    ReadOnly Property AsNewLocation As Span
        Get
            Return m_AsNewLocation
        End Get
    End Property

    ReadOnly Property ArgumentList As ArgumentList
        Get
            Return m_ArgumentList
        End Get
    End Property

End Class
