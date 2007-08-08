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

Public Class NamespaceDictionary
    Inherits Generic.Dictionary(Of String, TypeDictionary)

    Sub New()
        MyBase.new(Helper.StringComparer)
    End Sub

    Sub AddType(ByVal Type As Type)
        Dim [namespace] As String = Type.Namespace

        If [namespace] Is Nothing Then [namespace] = String.Empty
        GetTypes([namespace]).Add(Type)
    End Sub

    Function GetTypes(ByVal [Namespace] As String) As TypeDictionary
        If Me.ContainsKey([Namespace]) = False Then
            Return Me.AddNamespace([Namespace])
        End If
        Return MyBase.Item([Namespace])
    End Function

    Function AddNamespace(ByVal [Namespace] As String) As TypeDictionary
        Dim result As New TypeDictionary()
        Helper.Assert(MyBase.ContainsKey([Namespace]) = False)
        MyBase.Add([Namespace], result)
        Return result
    End Function

    ''' <summary>
    ''' Never returns nothing.
    ''' </summary>
    ''' <param name="Namespace"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Default Shadows ReadOnly Property Item(ByVal [Namespace] As String) As TypeDictionary
        Get
            If MyBase.ContainsKey([Namespace]) Then
                Return MyBase.Item([Namespace])
            Else
                Return TypeDictionary.EmptyDictionary
            End If
        End Get
    End Property

    Function GetAllDictionaries() As Generic.List(Of TypeDictionary)
        Dim result As New Generic.List(Of TypeDictionary)
        result.AddRange(Values)
        Return result
    End Function
End Class

#If ENABLECECIL Then
Public Class CecilNamespaceDictionary
    Inherits Generic.Dictionary(Of String, CecilTypeDictionary)

    Sub New()
        MyBase.new(Helper.StringComparer)
    End Sub

    Sub AddType(ByVal Type As Mono.Cecil.TypeDefinition)
        Dim [namespace] As String = Type.Namespace

        If [namespace] Is Nothing Then [namespace] = ""
        GetTypes([namespace]).Add(Type)
    End Sub

    Function GetTypes(ByVal [Namespace] As String) As CecilTypeDictionary
        If Me.ContainsKey([Namespace]) = False Then
            Return Me.AddNamespace([Namespace])
        End If
        Return MyBase.Item([Namespace])
    End Function

    Function AddNamespace(ByVal [Namespace] As String) As CecilTypeDictionary
        Dim result As New CecilTypeDictionary()
        Helper.Assert(MyBase.ContainsKey([Namespace]) = False)
        MyBase.Add([Namespace], result)
        Return result
    End Function

    ''' <summary>
    ''' Never returns nothing.
    ''' </summary>
    ''' <param name="Namespace"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Default Shadows ReadOnly Property Item(ByVal [Namespace] As String) As CecilTypeDictionary
        Get
            If MyBase.ContainsKey([Namespace]) Then
                Return MyBase.Item([Namespace])
            Else
                Return CecilTypeDictionary.EmptyDictionary
            End If
        End Get
    End Property

    Function GetAllDictionaries() As Generic.List(Of CecilTypeDictionary)
        Dim result As New Generic.List(Of CecilTypeDictionary)
        result.AddRange(Values)
        Return result
    End Function
End Class

#End If