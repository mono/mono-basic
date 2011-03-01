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

Public Class UnaryMinusExpression
    Inherits UnaryExpression

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Shadows Sub Init(ByVal Expression As Expression)
        MyBase.Init(Expression)
    End Sub

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Dim expInfo As EmitInfo = Info.Clone(Me, True, False, OperandType)

        Select Case OperandTypeCode
            Case TypeCode.Decimal
                result = Expression.GenerateCode(expInfo) AndAlso result
                Emitter.EmitCall(Info, Compiler.TypeCache.System_Decimal__Negate_Decimal)
            Case TypeCode.Single, TypeCode.Double
                result = Expression.GenerateCode(expInfo) AndAlso result
                Emitter.EmitNeg(Info)
            Case TypeCode.SByte, TypeCode.Int16
                result = Expression.GenerateCode(expInfo) AndAlso result
                Emitter.EmitNeg(Info)
            Case TypeCode.Object
                Helper.Assert(Helper.CompareType(OperandType, Compiler.TypeCache.System_Object))
                result = Me.Expression.GenerateCode(expInfo) AndAlso result
                Emitter.EmitCall(Info, Compiler.TypeCache.MS_VB_CS_Operators__NegateObject_Object)
            Case TypeCode.Int32
                Emitter.EmitLoadI4Value(Info, 0)
                result = Expression.GenerateCode(expInfo) AndAlso result
                Emitter.EmitSubOvf(Info, OperandType)
            Case TypeCode.Int64
                Emitter.EmitLoadI8Value(Info, 0)
                result = Expression.GenerateCode(expInfo) AndAlso result
                Emitter.EmitSubOvf(Info, OperandType)
            Case Else
                Throw New InternalException(Me)
        End Select

        Return result
    End Function

    Public Overrides Function GetConstant(ByRef result As Object, ByVal ShowError As Boolean) As Boolean
        If Not Expression.GetConstant(result, ShowError) Then Return False

        Select Case Helper.GetTypeCode(Compiler, CecilHelper.GetType(Compiler, result))
            Case TypeCode.SByte
                result = -CSByte(result)
            Case TypeCode.Int16
                result = -CShort(result)
            Case TypeCode.Int32
                result = -CInt(result)
            Case TypeCode.Int64
                result = -CLng(result)
            Case TypeCode.Byte
            Case TypeCode.UInt16
            Case TypeCode.UInt32
            Case TypeCode.UInt64
                result = -CULng(result)
            Case TypeCode.Decimal
                result = -CDec(result)
            Case TypeCode.Double
                result = -CDbl(result)
            Case TypeCode.Single
                result = -CSng(result)
            Case Else
                If ShowError Then Show30059()
                Return False
        End Select

        Return True
    End Function

    Public Overrides ReadOnly Property Keyword() As KS
        Get
            Return KS.Minus
        End Get
    End Property
End Class