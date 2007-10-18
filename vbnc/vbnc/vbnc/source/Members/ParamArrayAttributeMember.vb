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

'Public Class ParamArrayAttributeMember
'    Inherits AttributeMember
'    Public Overrides Function Resolve() As Boolean
'        Return True 'Already resolved!
'    End Function

'    Public Overrides ReadOnly Property Type() As System.Type
'        Get
'            Return Compiler.TypeCache.ParamArrayAttribute
'        End Get
'    End Property

'    Sub New(ByVal Parent As MemberParameter)
'        MyBase.New(Parent, Parent.Compiler)
'        m_Type = New NonArrayTypeName(Me)
'        m_Arguments = New InvocationOrIndexExpression(Me)
'    End Sub

'    Public Overrides ReadOnly Property Constructor() As System.Reflection.ConstructorInfo
'        Get
'            Return Compiler.TypeCache.ParamArrayAttributeConstructor
'        End Get
'    End Property
'End Class
