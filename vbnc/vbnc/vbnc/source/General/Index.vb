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

Imports System.Reflection.Emit
Imports System.Reflection

Public Class Index
    Private m_Parent As ParsedObject
    Private m_lstCollections As New Generic.Dictionary(Of String, IndexList)(Helper.StringComparer)

    Public Sub New(ByVal Parent As ParsedObject)
        m_Parent = Parent
    End Sub

    ReadOnly Property Compiler() As Compiler
        Get
            Return m_Parent.Compiler
        End Get
    End Property

    Public Function GetAllNames() As String()
        Dim result() As String
        ReDim result(m_lstCollections.Count - 1)
        m_lstCollections.Keys.CopyTo(result, 0)
        Array.Sort(result)
        Return result
    End Function

    Shadows Sub Add(ByVal Base As INameable)
        Dim idxList As IndexList
        'Does name exist already?
        Dim name As String = Base.Name

        If name Is Nothing Then Throw New InternalException("Got nothing for base type = " & CObj(Base).GetType().FullName)

        If m_lstCollections.ContainsKey(name) Then
            idxList = DirectCast(m_lstCollections.Item(name), IndexList)
        Else 'If not, create a new indexlist
            idxList = New IndexList()
            idxList.Name = name
            m_lstCollections.Add(idxList.Name, idxList)
        End If
        'Add the value
        idxList.Values.Add(Base)
    End Sub

    ''' <summary>
    ''' Looks an list of all TypeBase objects which has the specified Name. 
    '''	If no TypeBase found, returns nothing
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Shadows ReadOnly Property Item(ByVal Name As String) As Generic.List(Of INameable)
        Get
            'Name = Name.ToLower
            If m_lstCollections.ContainsKey(Name) Then
                Return m_lstCollections.Item(Name).Values
            Else
                Return Nothing
            End If
        End Get
    End Property

    ''' <summary>
    ''' Returns true if the specified Name is found in this index
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shadows Function ContainsName(ByVal Name As String) As Boolean
        Return m_lstCollections.ContainsKey(Name)
    End Function

    ReadOnly Property GetAllTypeBases() As ArrayList
        Get
            Dim result As New ArrayList
            For Each i As IndexList In m_lstCollections.Values
                result.AddRange(i.Values)
            Next
            Return result
        End Get
    End Property
End Class
