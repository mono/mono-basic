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
''' AttributeBlock  ::=  "&lt;"  AttributeList  "&gt;"
''' </summary>
''' <remarks></remarks>
Public Class AttributeBlock
    Inherits BaseObject

    Private m_List As AttributeList

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Return m_List.GenerateCode(Info)
    End Function

    ReadOnly Property List() As AttributeList
        Get
            Return m_List
        End Get
    End Property

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Return m_List.ResolveCode(info)
    End Function

    Sub New(ByVal Parent As BaseObject, ByVal ParamArray Attributes() As Attribute)
        MyBase.New(Parent)
        m_List = New AttributeList(Me)
        m_List.List.AddRange(Attributes)
    End Sub

    Shared Function IsMe(ByVal tm As tm) As Boolean
        Return tm.CurrentToken = KS.LT
    End Function

End Class
