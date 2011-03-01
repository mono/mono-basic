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
''' Base class for lists of type List ::= Item | List "," Item
''' </summary>
''' <remarks></remarks>
Public Class BaseList(Of T As ParsedObject)
    Inherits ParsedObject
    Implements Generic.IEnumerable(Of T)

    Private m_List As New Generic.List(Of T)

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True
        For i As Integer = 0 To m_List.Count - 1
            result = m_List(i).ResolveCode(Info) AndAlso result
        Next
        Return result
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Return Helper.ResolveTypeReferencesCollection(m_List)
    End Function

    Public Overrides Function CreateDefinition() As Boolean
        Dim result As Boolean = True

        For i As Integer = 0 To m_List.Count - 1
            result = m_List(i).CreateDefinition() AndAlso result
        Next

        Return result
    End Function

    Sub Insert(ByVal index As Integer, ByVal Item As T)
        m_List.Insert(index, Item)
    End Sub

    Sub RemoveAt(ByVal index As Integer)
        m_List.RemoveAt(index)
    End Sub

    Sub Clear()
        m_List.Clear()
    End Sub

    Function Add(ByVal Item As T) As T
        m_List.Add(Item)
        Return Item
    End Function

    Sub AddRange(ByVal List As Generic.IEnumerable(Of T))
        For Each item As T In List
            m_List.Add(item)
        Next
    End Sub

    Overridable Function NewObject() As T
        Throw New InternalException(Me)
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.new(Parent)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal ParamArray Objects() As T)
        MyBase.new(Parent)
        m_List.AddRange(Objects)
    End Sub

    Default ReadOnly Property Item(ByVal Index As Integer) As T
        Get
            Return DirectCast(m_List.Item(Index), T)
        End Get
    End Property

    ReadOnly Property Count() As Integer
        Get
            Return m_List.Count
        End Get
    End Property

    ReadOnly Property Length() As Integer
        Get
            Return m_List.Count
        End Get
    End Property

    Public ReadOnly Property List() As Generic.List(Of T)
        Get
            Return m_List
        End Get
    End Property

    Private Function GetEnumerator2() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        Return m_List.GetEnumerator
    End Function

    Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of T) Implements System.Collections.Generic.IEnumerable(Of T).GetEnumerator
        Return m_List.GetEnumerator
    End Function
End Class
