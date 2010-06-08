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

Public Class NamedArgument
    Inherits Argument

    Private m_Name As String

    Overrides ReadOnly Property IsNamedArgument() As Boolean
        Get
            Return True
        End Get
    End Property

    Overrides ReadOnly Property AsString() As String
        Get
            Return m_Name & " := " & MyBase.AsString
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Shadows Sub Init(ByVal Name As String, ByVal Expression As Expression)
        MyBase.Init(Expression)
        m_Name = Name
    End Sub

    Public ReadOnly Property Name() As String
        Get
            Return m_Name
        End Get
    End Property

    Shared Function CanBeMe(ByVal tm As tm) As Boolean
        Return tm.CurrentToken.IsIdentifierOrKeyword AndAlso tm.PeekToken = KS.Colon AndAlso tm.PeekToken(2) = KS.Equals
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Return MyBase.ResolveTypeReferences()
    End Function
End Class
