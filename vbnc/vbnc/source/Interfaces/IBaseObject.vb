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

Public Interface IBaseObject
    ReadOnly Property Assembly() As AssemblyDeclaration
    Property Location() As Span
    Property Parent() As BaseObject
    ReadOnly Property Compiler() As Compiler
    ReadOnly Property FullName() As String
    Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
    Function Define() As Boolean
    Function GenerateCode(ByVal Info As EmitInfo) As Boolean
    ReadOnly Property ObjectID() As Integer
End Interface
