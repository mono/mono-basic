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

Public Class Expressions
    Inherits ArrayList

    Shadows Sub Add(ByVal value As Expression)
        MyBase.Add(value)
    End Sub

    Default Shadows Property Item(ByVal Index As Integer) As Expression
        Get
            Return DirectCast(MyBase.Item(Index), Expression)
        End Get
        Set(ByVal value As Expression)
            MyBase.Item(Index) = value
        End Set
    End Property

    Shadows Function ToArray() As Expression()
        Dim result As Expression()
        ReDim result(Count - 1)
        Array.Copy(MyBase.ToArray, result, Count)
        Return result
    End Function

    ReadOnly Property Length() As Integer
        Get
            Return Count
        End Get
    End Property
End Class
