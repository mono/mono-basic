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

Public Class Nameables(Of T As INameable)
    Inherits BaseObjects(Of T)

    ''' <summary>
    ''' The index of the types this collections contains.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_Index As Index

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
        m_Index = New Index(Parent)
    End Sub


    ''' <summary>
    ''' Looks up the type of the specified index. 
    ''' </summary>
    ''' <param name="Index"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    ''' <exception cref="IndexOutOfRangeException">If the Index is invalid.</exception>
    Default Shadows ReadOnly Property Item(ByVal Index As Integer) As T
        Get
            Return DirectCast(MyBase.Item(Index), T)
        End Get
    End Property

    ''' <summary>
    ''' Returns true if contains (a) type(s) with this name.
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Obsolete()> _
    Shadows Function ContainsName(ByVal Name As String) As Boolean
        Return m_Index.ContainsName(Name)
    End Function

    ''' <summary>
    ''' Adds a new object to this collection.
    ''' </summary>
    ''' <param name="Base"></param>
    ''' <remarks></remarks>
    Shadows Sub Add(ByVal Base As T)
        'This is a workaround for #463303
        Dim B As INameable = DirectCast(Base, INameable)

#If DEBUG Then
        If B.Name = "" Then Throw New InternalException(Base)
#End If
        MyBase.Add(Base)
        m_Index.Add(Base)
    End Sub

    Shadows Sub AddRange(ByVal Base As Generic.IEnumerable(Of T))
        For Each obj As T In Base
            Add(obj)
        Next
    End Sub

    ReadOnly Property Index() As Index
        Get
            Return m_Index
        End Get
    End Property
End Class
