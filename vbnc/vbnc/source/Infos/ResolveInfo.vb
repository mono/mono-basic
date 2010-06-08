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

Public Class ResolveInfo
    Private m_Bits As New Collections.BitArray(32)
    Private Const c_SkipFunctionReturnVariable As Integer = 0
    Private Const c_CanFail As Integer = 1
    Private Const c_CanBeLateCall As Integer = 2
    Private Const c_CanBeImplicitSimpleName As Integer = 3
    Private Const c_EventResolution As Integer = 4

    Public Compiler As Compiler

    Private Shared DefaultInfo As ResolveInfo

    Shared Function [Default](ByVal Compiler As Compiler) As ResolveInfo
        If DefaultInfo Is Nothing OrElse Compiler Is DefaultInfo.Compiler = False Then
            DefaultInfo = New ResolveInfo(Compiler)
        End If

        Return DefaultInfo
    End Function

    Sub New(ByVal Compiler As Compiler, Optional ByVal SkipFunctionReturnVariable As Boolean = False, Optional ByVal CanFail As Boolean = False, Optional ByVal CanBeImplicitSimpleName As Boolean = True)
        Me.SkipFunctionReturnVariable = SkipFunctionReturnVariable
        Me.CanFail = CanFail
        Me.CanBeImplicitSimpleName = CanBeImplicitSimpleName
        Me.Compiler = Compiler
    End Sub

    Public Property CanBeImplicitSimpleName() As Boolean
        Get
            Return m_Bits(c_CanBeImplicitSimpleName)
        End Get
        Set(ByVal value As Boolean)
            m_Bits(c_CanBeImplicitSimpleName) = value
        End Set
    End Property

    Public Property CanBeLateCall() As Boolean
        Get
            Return m_Bits(c_CanBeLateCall)
        End Get
        Set(ByVal value As Boolean)
            m_Bits(c_CanBeLateCall) = value
        End Set
    End Property

    Public Property SkipFunctionReturnVariable() As Boolean
        Get
            Return m_Bits(c_SkipFunctionReturnVariable)
        End Get
        Set(ByVal value As Boolean)
            m_Bits(c_SkipFunctionReturnVariable) = value
        End Set
    End Property

    Public Property CanFail() As Boolean
        Get
            Return m_Bits(c_CanFail)
        End Get
        Set(ByVal value As Boolean)
            m_Bits(c_CanFail) = value
        End Set
    End Property

    Public Property IsEventResolution() As Boolean
        Get
            Return m_Bits(c_EventResolution)
        End Get
        Set(ByVal value As Boolean)
            m_Bits(c_EventResolution) = value
        End Set
    End Property
End Class
