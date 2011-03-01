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

Public Interface IMethod
    Inherits INonTypeMember
    ReadOnly Property Signature() As SubSignature
    ReadOnly Property HasReturnValue() As Boolean
    ReadOnly Property GetParameters() As Mono.Cecil.ParameterDefinition()
    ReadOnly Property DefaultReturnVariable() As Mono.Cecil.Cil.VariableDefinition

    ReadOnly Property HandlesOrImplements() As HandlesOrImplements
    Property MethodImplementationFlags() As Mono.Cecil.MethodImplAttributes
    ReadOnly Property CecilBuilder() As Mono.Cecil.MethodDefinition
End Interface
