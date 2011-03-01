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

Public MustInherit Class NamedBaseList(Of T As ParsedObject)
    Inherits BaseList(Of T)

    Shadows Function Add(ByVal Item As T) As T
        MyBase.Add(Item)
        Return Item
    End Function

    Default Shadows ReadOnly Property Item(ByVal index As Integer) As T
        Get
            Return DirectCast(MyBase.Item(index), T)
        End Get
    End Property


    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    ''' <summary>
    ''' Finds the list item with the specified name.
    ''' Returns nothing if nothing found.
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Default Shadows ReadOnly Property Item(ByVal Name As String) As T
        Get
            For Each tp As T In Me
                Dim t As INameable = CType(CObj(tp), INameable)
                If Helper.CompareName(t.Name, Name) Then
                    Return tp
                End If
            Next
            Return Nothing
        End Get
    End Property
End Class
