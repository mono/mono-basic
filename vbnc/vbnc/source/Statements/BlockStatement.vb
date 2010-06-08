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

Public MustInherit Class BlockStatement
    Inherits Statement

    Private m_Code As CodeBlock
    Private m_EndLabel As Label

    Property EndLabel() As Label
        Get
            Return m_EndLabel
        End Get
        Protected Set(ByVal value As Label)
            m_EndLabel = value
        End Set
    End Property

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Protected Sub Init(ByVal Block As CodeBlock)
        m_Code = Block
    End Sub

    ReadOnly Property CodeBlock() As CodeBlock
        Get
            Return m_Code
        End Get
    End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        '#If DEBUG Then
        '        Dim m1, m2 As MethodInfo
        '        m1 = Me.GetType.GetMethod("ResolveTypeReferences")
        '        m2 = GetType(BlockStatement).GetMethod("ResolveTypeReferences")
        '        Helper.Assert(m1 IsNot m2)
        '#End If

        result = m_Code.ResolveTypeReferences AndAlso result

        Return result
    End Function

End Class
