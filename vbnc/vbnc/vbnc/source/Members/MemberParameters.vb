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

'<Obsolete()> Public Class MemberParameters
'    Inherits NamedObjects

'    Sub New(ByVal Compiler As Compiler, ByVal Index As Index)
'        MyBase.New(Compiler, Index)
'    End Sub

'    Public Shadows Function Clone(Optional ByVal NewParent As MethodMember = Nothing) As MemberParameters
'        Dim result As New MemberParameters(Compiler, Nothing)

'        For Each m As MemberParameter In Me
'            result.Add(m.Clone(NewParent))
'        Next

'        Return result
'    End Function

'    Overloads Sub Add(ByVal Item As MemberParameter)
'        MyBase.Add(Item)
'    End Sub

'    Default Shadows ReadOnly Property Item(ByVal Index As Integer) As MemberParameter
'        Get
'            Return DirectCast(MyBase.Item(Index), MemberParameter)
'        End Get
'    End Property

'    ReadOnly Property Length() As Integer
'        Get
'            Return Count
'        End Get
'    End Property

'    Public Shadows Function ToArray() As MemberParameter()
'        Dim result As MemberParameter()
'        ReDim result(Count - 1)
'        Array.Copy(MyBase.ToArray, result, Count)
'        Return result
'    End Function

'    Overridable Function DefineParameters() As Boolean
'        Dim result As Boolean = True

'        For Each p As MemberParameter In Me
'            result = p.DefineParameter AndAlso result
'        Next

'        Return result
'    End Function
'End Class
