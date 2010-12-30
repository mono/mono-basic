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

Public Class ConditionalConstant
    ''' <summary>
    ''' The name of the constant. The names are not case sensitive.
    ''' </summary>
    ''' <remarks></remarks>
    Public Name As String = ""
    ''' <summary>
    ''' The value of the constant. If a string value, it is case sensitive
    ''' Possible types: Boolean, Double (no integer values, nor Decimal), Date, String
    ''' </summary>
    ''' <remarks></remarks>
    Public Value As Object

    ReadOnly Property IsDefined() As Boolean
        Get
            Return CBool(Value)
        End Get
    End Property

    Sub New(ByVal Name As String, ByVal Value As Object)
        Me.Name = Name
        Me.Value = Value
    End Sub

    ''' <summary>
    ''' Returns the name of the constant.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Overrides Function ToString() As String
        Return Name
    End Function
End Class
