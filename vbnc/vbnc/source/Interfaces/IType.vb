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
''' Every BaseObject that represents a type must implement this interface.
''' Structure, Enum, Class, Interface, Delegate
''' 
''' TypeParameter also implements this interface.
''' </summary>
''' <remarks></remarks>
Public Interface IType
    Inherits INameable, IModifiable, IMember

    Property TypeAttributes() As Mono.Cecil.TypeAttributes
    ReadOnly Property CecilType() As Mono.Cecil.TypeDefinition
    ReadOnly Property Members() As MemberDeclarations
    Property BaseType() As Mono.Cecil.TypeReference
    ReadOnly Property IsNestedType() As Boolean
    ReadOnly Property [Namespace]() As String
End Interface
