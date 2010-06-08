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

Public Class ParseTypeInfo
    Inherits ParseAttributableInfo

    Private m_Namespace As String
    Private m_BaseType As IType

    ReadOnly Property [Namespace]() As String
        Get
            Return m_Namespace
        End Get
    End Property

    ReadOnly Property BaseType() As IType
        Get
            Return m_BaseType
        End Get
    End Property

    Sub New(ByVal Compiler As Compiler, ByVal Attributes As Attributes, ByVal [Namespace] As String, ByVal BaseType As IType)
        MyBase.New(Compiler, Attributes)
        m_Namespace = [Namespace]
        m_BaseType = BaseType
    End Sub

End Class
