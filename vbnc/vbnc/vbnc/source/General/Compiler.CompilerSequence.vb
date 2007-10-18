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

Partial Public Class Compiler
    ''' <summary>
    ''' The sequence of the compiler:
    ''' </summary>
    ''' <remarks></remarks>
    Enum CompilerSequence
        Start
        ''' <summary>
        ''' Scanned ... ConditionalCompiled
        ''' </summary>
        ''' <remarks></remarks>
        Scanned
        ''' <summary>
        ''' - Tokens are scanned and created.
        ''' Scanned ... ConditionalCompiled ... Parsed
        ''' </summary>
        ''' <remarks></remarks>
        ConditionalCompiled
        ''' <summary>
        ''' - This is where the tokens are parsed and the parse tree created.
        ''' ConditionalCompiled ... Parsed ... Resolved
        ''' </summary>
        ''' <remarks></remarks>
        Parsed
        ''' <summary>
        ''' - This is where the Compiler generated elements are added.
        ''' Parsed ... Resolved ... DefinedTypes
        ''' </summary>
        ''' <remarks></remarks>
        Resolved
        ''' <summary>
        ''' - This is where the types are created with Reflection.Emit.
        '''   No expressions has been resolved yet.
        ''' Resolved ... DefinedTypes ... DefinedInheritsAndImplements ... DefinedMembers
        ''' </summary>
        ''' <remarks></remarks>
        DefinedTypes
        ''' <summary>
        ''' - This is where the types's implements and inherits clauses are loaded.
        ''' </summary>
        ''' <remarks></remarks>
        DefinedInheritsAndImplements
        ''' <summary>
        ''' - This is where the type members as created with Reflection.Emit.
        '''   Constant expressions are solved in this step.
        '''   Type descriptors are also resolved in this step.
        ''' DefinedTypes ... DefinedInheritsAndImplements ...  DefinedMembers ... CodeResolved ... GeneratedCode
        ''' </summary>
        ''' <remarks></remarks>
        DefinedMembers
        ''' <summary>
        ''' - All the rest of the code is resolved in this step.
        ''' </summary>
        ''' <remarks></remarks>
        CodeResolved
        ''' <summary>
        ''' DefinedMembers ... CodeResolved ... GeneratedCode 
        ''' </summary>
        ''' <remarks></remarks>
        GeneratedCode
        [End]
    End Enum
End Class
