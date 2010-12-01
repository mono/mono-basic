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
''' A list of define objects.
''' </summary>
Public Class Defines
    Inherits ArrayList

    ''' <summary>
    ''' Returns the Define at the specified index.
    ''' </summary>
    Default Shadows ReadOnly Property Item(ByVal Index As Integer) As Define
        Get
            Return DirectCast(MyBase.Item(Index), Define)
        End Get
    End Property

    ''' <summary>
    ''' Adds a new Define to the list.
    ''' </summary>
    Shadows Function Add(ByVal Define As Define) As Integer
        Return MyBase.Add(Define)
    End Function

    Function IsDefined(ByVal str As String) As Boolean
        Dim def As Define
        def = Item(str)
        If def Is Nothing Then Return False

        If def.Value <> "" Then
            Dim b As Boolean
            If Boolean.TryParse(def.Value, b) Then Return b
            If VB.IsNumeric(def.Value) Then Return CBool(CDbl(def.Value))
            Return False
        End If

        Return False
    End Function

    Default Shadows ReadOnly Property Item(ByVal Name As String) As Define
        Get
            For Each def As Define In Me
                If Helper.CompareName(def.Symbol, Name) Then
                    Return def
                End If
            Next
            Return Nothing
        End Get
    End Property
End Class