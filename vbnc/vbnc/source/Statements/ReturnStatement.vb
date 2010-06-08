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

Public Class ReturnStatement
    Inherits Statement

    Private m_Expression As Expression

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal Expression As Expression)
        m_Expression = Expression
    End Sub

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True
        Dim isSub As Boolean

        isSub = Info.Method.Signature.ReturnType Is Nothing OrElse Helper.CompareType(Info.Method.Signature.ReturnType, Compiler.TypeCache.System_Void)
        If isSub Then
            Helper.Assert(m_Expression Is Nothing)
        Else
            Helper.Assert(m_Expression IsNot Nothing)
            result = m_Expression.GenerateCode(Info.Clone(Me, True, , Info.Method.Signature.ReturnType)) AndAlso result
        End If

        Emitter.EmitRetOrLeave(Info, Me, Not isSub)

        Return result
    End Function


    Public Overrides Function ResolveStatement(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        If m_Expression IsNot Nothing Then
            result = m_Expression.ResolveExpression(info) AndAlso result
            If result AndAlso m_Expression.Classification.IsValueClassification = False Then
                m_Expression = m_Expression.ReclassifyToValueExpression
                result = m_Expression.ResolveExpression(Info) AndAlso result
            End If
        End If

        If result = False Then Return result

        If m_Expression IsNot Nothing Then
            Dim method As IMethod
            method = Me.FindFirstParent(Of IMethod)()
            m_Expression = Helper.CreateTypeConversion(Me, m_Expression, method.Signature.ReturnType, result)
        End If

        Return result
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Return m_Expression Is Nothing OrElse m_Expression.ResolveTypeReferences()
    End Function
End Class
