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
''' ThrowStatement  ::= "Throw" [  Expression  ]  StatementTerminator
''' </summary>
''' <remarks></remarks>
Public Class ThrowStatement
    Inherits Statement

    Private m_Exception As Expression

    Public Overrides Function ResolveTypeReferences() As Boolean
        Return m_Exception Is Nothing OrElse m_Exception.ResolveTypeReferences
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal Exception As Expression)
        m_Exception = Exception
    End Sub

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        If m_Exception Is Nothing Then
            Info.ILGen.Emit(Mono.Cecil.Cil.OpCodes.Rethrow)
        Else
            result = m_Exception.GenerateCode(Info.Clone(Me, True, False, m_Exception.ExpressionType)) AndAlso result
            Emitter.EmitThrow(Info)
        End If

        Return result
    End Function

    Public Overrides Function ResolveStatement(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        If m_Exception IsNot Nothing Then
            result = m_Exception.ResolveExpression(Info) AndAlso result
            If result = False Then Return result
            result = Helper.VerifyValueClassification(m_Exception, Info) AndAlso result
        End If

        Return result
    End Function
End Class
