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

Public MustInherit Class ParsedObject
    Inherits BaseObject

    Private m_HasErrors As Boolean

    Protected Sub New(ByVal Parent As ParsedObject)
        MyBase.new(Parent)
    End Sub

    Protected Sub New(ByVal Parent As ParsedObject, ByVal Location As Span)
        MyBase.new(Parent, Location)
    End Sub

    Protected Sub New(ByVal Parent As Compiler)
        MyBase.new(Parent)
    End Sub

    Protected Sub New()
        MyBase.New()
    End Sub

    Property HasErrors() As Boolean
        Get
            Return m_HasErrors
        End Get
        Set(ByVal value As Boolean)
            m_HasErrors = value
        End Set
    End Property

    Shadows Property Parent() As ParsedObject
        Get
            Return DirectCast(MyBase.Parent, ParsedObject)
        End Get
        Set(ByVal value As ParsedObject)
            MyBase.Parent = value
        End Set
    End Property

    Overridable Function ResolveTypeReferences() As Boolean
        Return True
    End Function

    Overridable Function ResolveBaseType() As Boolean
        Return True
    End Function

    Overridable Function CreateDefinition() As Boolean
        Return True
    End Function

    Public Function Show30059() As Boolean
        Return Compiler.Report.ShowMessage(Messages.VBNC30059, Me.Location)
    End Function
End Class
