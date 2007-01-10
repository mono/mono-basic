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

Option Compare Text

Public Class ConditionalConstants
    Inherits Hashtable
    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(ByVal CopyFrom As ConditionalConstants)
        For Each constant As ConditionalConstant In CopyFrom.Values
            Add(constant)
        Next
    End Sub
    ''' <summary>
    ''' Adds the constant to the collection. 
    ''' If the constant already exists, it is replaced.
    ''' </summary>
    ''' <param name="Constant"></param>
    ''' <remarks></remarks>
    Public Shadows Sub Add(ByVal Constant As ConditionalConstant)
        If MyBase.Contains(Constant.Name) Then
            MyBase.Remove(Constant.Name)
        End If
        MyBase.Add(Constant.Name, Constant)
    End Sub

    Default Public Shadows ReadOnly Property Item(ByVal key As String) As ConditionalConstant
        Get
            Return DirectCast(MyBase.Item(key), ConditionalConstant)
        End Get
    End Property
    Public Shadows Function Contains(ByVal key As String) As Boolean
        Return MyBase.Contains(key)
    End Function
    Public Sub Dump(ByVal Dumper As IndentedTextWriter)
        Dim lstConstants As New ArrayList(Me.Values)
        lstConstants.Sort()
        For Each constant As ConditionalConstant In lstConstants
            constant.Dump(Dumper)
        Next
    End Sub
End Class