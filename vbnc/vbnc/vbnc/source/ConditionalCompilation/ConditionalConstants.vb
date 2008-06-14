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
    Inherits Generic.Dictionary(Of String, ConditionalConstant)

    Public Sub New()
        MyBase.New(Helper.StringComparer)
    End Sub

    Public Sub New(ByVal CopyFrom As ConditionalConstants)
        Me.New()
        For Each constant As ConditionalConstant In CopyFrom.Values
            Add(constant)
        Next
    End Sub

    Public Function Clone() As ConditionalConstants
        Return New ConditionalConstants(Me)
    End Function

#If DEBUG Then
    ReadOnly Property AsString() As String
        Get
            Dim result As String = String.Empty
            For Each item As Generic.KeyValuePair(Of String, ConditionalConstant) In Me
                If result <> String.Empty Then result &= ";"
                result &= item.Key & "=" & CStr(item.Value.Value)
            Next
            Return result
        End Get
    End Property
#End If

    ''' <summary>
    ''' Adds the constant to the collection. 
    ''' If the constant already exists, it is replaced.
    ''' </summary>
    ''' <param name="Constant"></param>
    ''' <remarks></remarks>
    Public Shadows Sub Add(ByVal Constant As ConditionalConstant)
        If MyBase.ContainsKey(Constant.Name) Then
            MyBase.Remove(Constant.Name)
        End If
        MyBase.Add(Constant.Name, Constant)
    End Sub

    Default Public Shadows ReadOnly Property Item(ByVal key As String) As ConditionalConstant
        Get
            Return DirectCast(MyBase.Item(key), ConditionalConstant)
        End Get
    End Property

End Class