' 
' Visual Basic.Net Compiler
' Copyright (C) 2004 - 2008 Rolf Bjarne Kvinge, RKvinge@novell.com
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


    ''' <summary>
    ''' Create a new collection with the specified index.
    ''' </summary>
    ''' <param name="Index"></param>
    ''' <remarks></remarks>
    Sub New(ByVal Parent As ParsedObject, Optional ByVal Index As Index = Nothing)
        MyBase.New(Parent)
        If Index Is Nothing Then
            m_Index = New Index(Parent)
        Else
            m_Index = Index
        End If
    End Sub

    ''' <summary>
    ''' Returns the index for this collection.
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    ReadOnly Property Index() As Index
        Get
            Return m_Index
        End Get
    End Property

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
    Shadows Function ContainsName(ByVal Name As String) As Boolean
        Return m_Index.ContainsName(Name)
    End Function

    ''' <summary>
    ''' Adds a new object to this collection.
    ''' </summary>
    ''' <param name="Base"></param>
    ''' <remarks></remarks>
    Shadows Sub Add(ByVal Base As T)
#If DEBUG Then
        If Base.Name = "" Then Throw New InternalException(Base)
#End If
        MyBase.Add(Base)
        m_Index.Add(Base)
    End Sub
End Class
