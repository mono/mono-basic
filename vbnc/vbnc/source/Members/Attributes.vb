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
'''  Attributes ::=	AttributeBlock  |	Attributes  AttributeBlock
''' </summary>
''' <remarks></remarks>
Public Class Attributes
    Inherits BaseObjects(Of Attribute)

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub SetParent(ByVal Parent As ParsedObject)
        For Each item As IBaseObject In Me
            item.Parent = Parent
        Next
    End Sub

    Function Clone(Optional ByVal NewParent As ParsedObject = Nothing) As Attributes
        If NewParent Is Nothing Then NewParent = DirectCast(Me.Parent, ParsedObject)
        Dim result As New Attributes(NewParent)
        For Each item As Attribute In Me
            result.Add(item.Clone(NewParent))
        Next
        Return result
    End Function

    Function IsDefined(ByVal AttributeType As Mono.Cecil.TypeReference) As Boolean
        Dim result As Boolean = False

        For Each att As Attribute In Me
            If AttributeType Is Nothing Then Return False
            If Helper.CompareType(att.AttributeType, AttributeType) Then
                result = True
                Exit For
            End If
        Next

        Return result
    End Function

    ''' <summary>
    ''' Might return nothing
    ''' </summary>
    ''' <param name="AttributeType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function FindAttributes(ByVal AttributeType As Mono.Cecil.TypeReference) As Generic.List(Of Attribute)
        Dim result As Generic.List(Of Attribute) = Nothing

        If AttributeType Is Nothing Then Return Nothing

        For Each att As Attribute In Me
            If Helper.CompareType(att.AttributeType, AttributeType) Then
                If result Is Nothing Then result = New Generic.List(Of Attribute)
                result.Add(att)
            End If
        Next
        Return result
    End Function

    Shared Function IsMe(ByVal tm As tm) As Boolean
        Return tm.CurrentToken = KS.LT
    End Function
End Class
