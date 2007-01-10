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
''' RedimStatement  ::= "ReDim" [ "Preserve" ]  RedimClauses  StatementTerminator
''' RedimClauses  ::=
'''	   RedimClause  |
'''	   RedimClauses  ","  RedimClause
''' RedimClause  ::=  Expression  ArraySizeInitializationModifier
''' </summary>
''' <remarks></remarks>
Public Class ReDimStatement
    Inherits Statement

    Private m_IsPreserve As Boolean
    Private m_Clauses As RedimClauses

    ReadOnly Property Clauses() As RedimClauses
        Get
            Return m_Clauses
        End Get
    End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Return m_Clauses Is Nothing OrElse m_Clauses.ResolveTypeReferences
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal IsPreserve As Boolean, ByVal Clauses As RedimClauses)
        m_IsPreserve = IsPreserve
        m_Clauses = Clauses
    End Sub
    
    ReadOnly Property IsPreserve() As Boolean
        Get
            Return m_IsPreserve
        End Get
    End Property

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        result = Helper.GenerateCodeCollection(m_Clauses, Info) AndAlso result

        Return result
    End Function

    Public Overrides Function ResolveStatement(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = m_Clauses.ResolveCode(info) AndAlso result

        Compiler.Helper.AddCheck("Each clause in the statement must be classified as a variable or a property access whose type is an array type or Object, and be followed by a list of array bounds. ")
        Compiler.Helper.AddCheck("The number of the bounds must be consistent with the type of the variable; any number of bounds is allowed for Object.")
        Compiler.Helper.AddCheck("If the Preserve keyword is specified, then the expressions must also be classifiable as a value, and the new size for each dimension except for the rightmost one must be the same as the size of the existing array. ")

        Return result
    End Function

End Class
