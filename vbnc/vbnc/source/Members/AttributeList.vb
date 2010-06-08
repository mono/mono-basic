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
''' AttributeList  ::=	Attribute  | AttributeList  ,  Attribute
''' </summary>
''' <remarks></remarks>
Public Class AttributeList
    Inherits BaseObject
    Private m_List As New Generic.List(Of Attribute)

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Return Helper.GenerateCodeCollection(m_List, Info)
    End Function

    ReadOnly Property List() As Generic.List(Of Attribute)
        Get
            Return m_list
        End Get
    End Property

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True
        For Each attrib As Attribute In m_List
            result = attrib.ResolveCode(info) AndAlso result
        Next
        Return result
    End Function

    Sub New(ByVal Parent As BaseObject)
        MyBase.New(Parent)
    End Sub

End Class
