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
''' ParameterList ::= Parameter | ParameterList "," Parameter
''' </summary>
''' <remarks></remarks>
Public Class ParameterList
    Inherits NamedBaseList(Of Parameter)

    Private m_ParameterInfos() As ParameterInfo

    Sub New(ByVal Parent As ParsedObject)
        MyBase.new(Parent)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal ParameterTypes() As Type)
        MyBase.new(Parent)
        If ParameterTypes IsNot Nothing Then
            For Each t As Type In ParameterTypes
                Add("", t)
            Next
        End If
    End Sub

    Function Clone(Optional ByVal NewParent As ParsedObject = Nothing) As ParameterList
        If NewParent Is Nothing Then NewParent = Me.Parent
        Dim result As New ParameterList(NewParent)
        For i As Integer = 0 To Me.Count - 1
            result.Add(Me.Item(i).Clone(result))
        Next
        Return result
    End Function

    Sub DefineParameters(ByVal MethodBuilder As MethodBuilder)
        For i As Integer = 0 To Count - 1
            MethodBuilder.DefineParameter(i + 1, ParameterAttributes.None, Item(i).Name)
        Next
    End Sub

    Overloads Function Add(ByVal Name As String, ByVal Type As Type) As Parameter
        Return MyBase.Add(New Parameter(Me, Name, Type))
    End Function

    ReadOnly Property AsParameterInfo() As ParameterInfo()
        Get
            If m_ParameterInfos Is Nothing Then
                ReDim m_ParameterInfos(Me.Count - 1)
                For i As Integer = 0 To Count - 1
                    m_ParameterInfos(i) = New ParameterDescriptor(Me(i))
                Next
            End If
            Return m_ParameterInfos
        End Get
    End Property

    Function ToTypeArray() As Type()
        Dim result(Me.Count - 1) As Type
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
