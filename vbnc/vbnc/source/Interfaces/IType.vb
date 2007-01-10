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
''' Every BaseObject that represents a type must implement this interface.
''' Structure, Enum, Class, Interface, Delegate
''' 
''' TypeParameter also implements this interface.
''' </summary>
''' <remarks></remarks>
Public Interface IType
    Inherits INameable, IModifiable, IDefinableType, IMember, ICreatableType

    ReadOnly Property TypeAttributes() As TypeAttributes
    ReadOnly Property TypeBuilder() As TypeBuilder
    ReadOnly Property Members() As MemberDeclarations
    ReadOnly Property BaseType() As Type
    ReadOnly Property TypeDescriptor() As TypeDescriptor
    ReadOnly Property IsNestedType() As Boolean
    ReadOnly Property [Namespace]() As String
    Function ResolveType() As Boolean 'No info is needed to resolve a type.
End Interface
