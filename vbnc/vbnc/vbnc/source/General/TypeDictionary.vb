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

Public Class TypeDictionary
    Inherits Generic.Dictionary(Of String, Mono.Cecil.TypeReference)

    Public Shared ReadOnly EmptyDictionary As New TypeDictionary()

    Sub New()
        MyBase.new(Helper.StringComparer)
    End Sub

    Shadows Sub Add(ByVal Type As Mono.Cecil.TypeReference)
        Dim name As String = Type.Name
        If MyBase.ContainsKey(name) Then
            'System.Console.WriteLine("Already added type: " & Type.FullName)
        Else
            MyBase.Add(name, Type)
        End If
    End Sub

    Shadows Sub AddRange(ByVal Types As Generic.IEnumerable(Of Mono.Cecil.TypeReference))
        For Each Type As Mono.Cecil.TypeReference In Types
            Add(Type)
        Next
    End Sub

    Function TypesAsArray() As Mono.Cecil.TypeReference()
        Dim result() As Mono.Cecil.TypeReference
        ReDim result(Me.Count - 1)
        MyBase.Values.CopyTo(result, 0)
        Return result
    End Function

    Function ToTypeList() As TypeList
        Dim result As New TypeList()
        result.AddRange(Me.Values)
        Return result
    End Function

    ''' <summary>
    ''' Returns nothing if the specified key is not found.
    ''' </summary>
    ''' <param name="key"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Default Shadows ReadOnly Property Item(ByVal key As String) As Mono.Cecil.TypeReference
        Get
            If MyBase.ContainsKey(key) Then
                Return MyBase.Item(key)
            Else
                Return Nothing
            End If
        End Get
    End Property
End Class

