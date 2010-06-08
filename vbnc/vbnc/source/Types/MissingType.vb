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

Public Class MissingType
    Inherits Mono.Cecil.TypeReference

    Private m_Compiler As Compiler

    Sub New(ByVal Compiler As Compiler)
        MyBase.New("Missing", "")
        m_Compiler = Compiler
    End Sub

    Shared Operator =(ByVal t As Mono.Cecil.TypeReference, ByVal m As MissingType) As Boolean
        Return Helper.CompareType(m, t)
    End Operator

    Shared Operator <>(ByVal t As Mono.Cecil.TypeReference, ByVal m As MissingType) As Boolean
        Return Not t = m
    End Operator

    Shared Operator =(ByVal m As MissingType, ByVal t As Mono.Cecil.TypeReference) As Boolean
        Return Helper.CompareType(m, t)
    End Operator

    Shared Operator <>(ByVal m As MissingType, ByVal t As Mono.Cecil.TypeReference) As Boolean
        Return Not m = t
    End Operator
End Class
