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

Public Class ConcatExpression
    Inherits BinaryExpression

    Protected Overrides Function ResolveExpressions(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveExpressions(Info) AndAlso result

        If result = False Then Return result

        Dim l, r, other As Boolean
        l = Helper.CompareType(m_LeftExpression.ExpressionType, Compiler.TypeCache.System_DBNull)
        r = Helper.CompareType(m_RightExpression.ExpressionType, Compiler.TypeCache.System_DBNull)
        If l AndAlso r = False Then 'DBNull & whatever
            m_LeftExpression = New NothingConstantExpression(Me)
            result = m_LeftExpression.ResolveExpression(Info) AndAlso result
        ElseIf l = False AndAlso r Then 'whatever & DBNull
            m_RightExpression = New NothingConstantExpression(Me)
            result = m_RightExpression.ResolveExpression(Info) AndAlso result
        Else
            other = True
        End If

        If l = False Then
            If Helper.CompareType(m_LeftExpression.ExpressionType, Compiler.TypeCache.System_Char_Array) Then
                m_LeftExpression = New CStrExpression(Me, m_LeftExpression)
                result = m_LeftExpression.ResolveExpression(Info) AndAlso result
            End If
        End If

        If r = False Then
            If Helper.CompareType(m_RightExpression.ExpressionType, Compiler.TypeCache.System_Char_Array) Then
                m_RightExpression = New CStrExpression(Me, m_RightExpression)
                result = m_RightExpression.ResolveExpression(Info) AndAlso result
            End If
        End If

        Return result
    End Function
    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        ValidateBeforeGenerateCode(Info)

        Dim expInfo As EmitInfo = Info.Clone(Me, True, False, OperandType)

        result = m_LeftExpression.GenerateCode(expInfo) AndAlso result
        result = m_RightExpression.GenerateCode(expInfo) AndAlso result

        Select Case OperandTypeCode
            Case TypeCode.String
                Emitter.EmitCall(Info, Compiler.TypeCache.System_String__Concat_String_String)
            Case TypeCode.Object
                Helper.Assert(Helper.CompareType(OperandType, Compiler.TypeCache.System_Object))
                Emitter.EmitCall(Info, Compiler.TypeCache.MS_VB_CS_Operators__ConcatenateObject_Object_Object)
            Case Else
                Throw New InternalException(Me)
        End Select
        Return result
    End Function

    Sub New(ByVal Parent As ParsedObject, ByVal LExp As Expression, ByVal RExp As Expression)
        MyBase.New(Parent, LExp, RExp)
    End Sub

    Public Overrides ReadOnly Property Keyword() As KS
        Get
            Return KS.Concat
        End Get
    End Property

    Public Overrides ReadOnly Property IsConstant() As Boolean
        Get
            Return m_LeftExpression.IsConstant AndAlso (Helper.CompareType(m_LeftExpression.ExpressionType, Compiler.TypeCache.System_String) OrElse Helper.CompareType(m_LeftExpression.ExpressionType, Compiler.TypeCache.System_Char)) _
              AndAlso m_RightExpression.IsConstant AndAlso (Helper.CompareType(m_RightExpression.ExpressionType, Compiler.TypeCache.System_String) OrElse Helper.CompareType(m_RightExpression.ExpressionType, Compiler.TypeCache.System_Char))
        End Get
    End Property

    Public Overrides ReadOnly Property ConstantValue() As Object
        Get
            If IsConstant = False Then Throw New InternalException(Me)

            Dim rvalue, lvalue As String

            lvalue = CStr(m_LeftExpression.ConstantValue)
            rvalue = CStr(m_RightExpression.ConstantValue)

            Return lvalue & rvalue
        End Get
    End Property
End Class
