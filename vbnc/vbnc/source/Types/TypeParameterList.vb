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
''' TypeParameterList  ::= 	TypeParameter  | TypeParameterList  ","  TypeParameter
''' CHANGED: Switched name of TypeParameters and TypeParameterList
''' </summary>
''' <remarks></remarks>
Public Class TypeParameterList
    Inherits NamedBaseList(Of TypeParameter)

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Shadows Sub Add(ByVal Item As TypeParameter)
        If Me.List.Contains(Item) Then
            Throw New InternalException
        End If
    End Sub

    Function AsTypeArray() As Mono.Cecil.TypeReference()
        Dim result(Me.Count - 1) As Mono.Cecil.TypeReference

        For i As Integer = 0 To Me.Count - 1
            result(i) = Item(i).CecilBuilder
        Next

        Return result
    End Function
End Class
