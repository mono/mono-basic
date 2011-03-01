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


Public Class LikeExpression
    Inherits BinaryExpression

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        ValidateBeforeGenerateCode(Info)

        Dim expInfo As EmitInfo = Info.Clone(Me, True, False, OperandType)

        result = m_LeftExpression.GenerateCode(expInfo) AndAlso result
        result = m_RightExpression.GenerateCode(expInfo) AndAlso result

        Dim cmInfo As EmitInfo = Info.Clone(Me, True, False, Compiler.TypeCache.MS_VB_CompareMethod)
        If Info.IsOptionCompareText Then
            Emitter.EmitLoadValue(cminfo, Microsoft.VisualBasic.CompareMethod.Text)
        Else
            Emitter.EmitLoadValue(cmInfo, Microsoft.VisualBasic.CompareMethod.Binary)
        End If

        Select Case MyBase.OperandTypeCode
            Case TypeCode.String
                Emitter.EmitCallOrCallVirt(Info, Compiler.TypeCache.MS_VB_CS_LikeOperator__LikeString_String_String_CompareMethod)
            Case TypeCode.Object
                'Helper.Assert(Helper.CompareType(ExpressionType, Compiler.TypeCache.Object))
                Emitter.EmitCallOrCallVirt(Info, Compiler.TypeCache.MS_VB_CS_LikeOperator__LikeObject_Object_Object_CompareMethod)
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
            Return KS.Like
        End Get
    End Property
End Class
