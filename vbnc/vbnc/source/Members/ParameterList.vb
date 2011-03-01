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
''' ParameterList ::= Parameter | ParameterList "," Parameter
''' </summary>
''' <remarks></remarks>
Public Class ParameterList
    Inherits NamedBaseList(Of Parameter)

    Private m_ParameterInfos() As Mono.Cecil.ParameterDefinition

    Sub New(ByVal Parent As ParsedObject)
        MyBase.new(Parent)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal ParameterTypes() As Mono.Cecil.TypeReference)
        MyBase.new(Parent)
        If ParameterTypes IsNot Nothing Then
            For Each t As Mono.Cecil.TypeReference In ParameterTypes
                Add("", t)
            Next
        End If
    End Sub

    Sub AddCloned(ByVal List As ParameterList)
        For i As Integer = 0 To List.Count - 1
            Add(List(i).Clone(Me))
        Next
    End Sub

    Function Clone(Optional ByVal NewParent As ParsedObject = Nothing) As ParameterList
        If NewParent Is Nothing Then NewParent = Me.Parent
        Dim result As New ParameterList(NewParent)
        For i As Integer = 0 To Me.Count - 1
            result.Add(Me.Item(i).Clone(result))
        Next
        Return result
    End Function

    Function DefineOptionalParameters() As Boolean
        Dim result As Boolean = True

        For i As Integer = 0 To Me.Count - 1
            result = Item(i).DefineOptionalParameters AndAlso result
        Next

        Return result
    End Function

    Overloads Function Add(ByVal Name As String, ByVal Type As Mono.Cecil.TypeReference) As Parameter
        Return MyBase.Add(New Parameter(Me, Name, Type))
    End Function

    ReadOnly Property AsParameterInfo() As Mono.Cecil.ParameterDefinition()
        Get
            If m_ParameterInfos Is Nothing Then
                ReDim m_ParameterInfos(Me.Count - 1)
                For i As Integer = 0 To Count - 1
                    m_ParameterInfos(i) = Me(i).CecilBuilder
                Next
            End If
            Return m_ParameterInfos
        End Get
    End Property

    Function ToTypeArray() As Mono.Cecil.TypeReference()
        Dim result(Me.Count - 1) As Mono.Cecil.TypeReference
        For i As Integer = 0 To Me.Count - 1
            result(i) = Me.Item(i).ParameterType
        Next
        Return result
    End Function

    ''' <summary>
    ''' Resolves either all optional parameters or all non-optional parameters.
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <param name="OptionalParameters"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ResolveParameters(ByVal Info As ResolveInfo, ByVal OptionalParameters As Boolean) As Boolean
        Dim result As Boolean = True

        For i As Integer = 0 To Count - 1
            If Me(i).Modifiers.Is(ModifierMasks.Optional) = OptionalParameters Then
                result = Me(i).ResolveCode(Info) AndAlso result
            End If
        Next

        Return result
    End Function

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Return ResolveParameters(Info, False)
    End Function
End Class
