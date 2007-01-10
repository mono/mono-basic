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

''' <summary>
'''  Attributes ::=	AttributeBlock  |	Attributes  AttributeBlock
''' </summary>
''' <remarks></remarks>
Public Class Attributes
    Inherits BaseObjects(Of Attribute)

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Function Clone(Optional ByVal NewParent As ParsedObject = Nothing) As Attributes
        If NewParent Is Nothing Then NewParent = DirectCast(Me.Parent, ParsedObject)
        Dim result As New Attributes(NewParent)
        For Each item As Attribute In Me
            result.Add(item.Clone(NewParent))
        Next
        Return result
    End Function

    Function IsDefined(ByVal AttributeType As Type) As Boolean
        Dim result As Boolean = False

        For Each att As Attribute In Me
            If Helper.CompareType(att.AttributeType, attributeType) Then
                result = True
                Exit For
            End If
        Next

        Return result
    End Function

    Shared Function IsMe(ByVal tm As tm) As Boolean
        Return tm.CurrentToken = KS.LT
    End Function

End Class
