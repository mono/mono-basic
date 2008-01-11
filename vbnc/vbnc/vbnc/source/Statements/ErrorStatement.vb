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
''' ErrorStatement  ::=  "Error" Expression  StatementTerminator
''' </summary>
''' <remarks></remarks>
Public Class ErrorStatement
    Inherits Statement

    Private m_ErrNumber As Expression

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal ErrNumber As Expression)
        m_ErrNumber = ErrNumber
    End Sub

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        result = m_ErrNumber.GenerateCode(Info.Clone(Me, True, False, Compiler.TypeCache.System_Int32)) AndAlso result
        Emitter.EmitConversion(m_ErrNumber.ExpressionType, Compiler.TypeCache.System_Int32, Info)
        Emitter.EmitCallOrCallVirt(Info, Compiler.TypeCache.MS_VB_CS_ProjectData__CreateProjectError_Int32)
        Emitter.EmitThrow(Info)

        Return result
    End Function

    Public Overrides Function ResolveStatement(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = m_ErrNumber.ResolveExpression(info) AndAlso result

        Compiler.Helper.AddCheck("The expression must be classified as a value and its type must be implicitly convertible to Integer.")

        Return result
    End Function


#If DEBUG Then
    Public Sub Dump(ByVal Dumper As IndentedTextWriter)
        Dumper.Write("Error ")
        m_ErrNumber.Dump(Dumper)
        Dumper.WriteLine("")
    End Sub
#End If
End Class
