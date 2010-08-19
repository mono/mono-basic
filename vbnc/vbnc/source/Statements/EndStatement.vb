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
''' EndStatement  ::= "End" StatementTerminator
''' </summary>
''' <remarks></remarks>
Public Class EndStatement
    Inherits Statement

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Public Overrides Function ResolveTypeReferences() As Boolean
        Return True
    End Function

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Emitter.EmitCall(Info, Compiler.TypeCache.MS_VB_CS_ProjectData__EndApp)

        Return result
    End Function

    Public Overrides Function ResolveStatement(ByVal Info As ResolveInfo) As Boolean
        Dim m As Object
        Dim method As IMethod

        Compiler.Helper.AddCheck("End statements may not be used in programs that are not executable (for example, DLLs). ")

        m = FindMethod()
        method = TryCast(m, IMethod)
        If method IsNot Nothing Then
            method.CecilBuilder.ImplAttributes = Mono.Cecil.MethodImplAttributes.NoInlining Or Mono.Cecil.MethodImplAttributes.NoOptimization
        End If

        Return True
    End Function
End Class
