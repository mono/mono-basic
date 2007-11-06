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

#If DEBUG Then
    Public Overrides Sub Dump(ByVal Dumper As IndentedTextWriter)
        Dumper.Write("-")
        Expression.Dump(Dumper)
    End Sub
#End If

    Public Overrides ReadOnly Property ConstantValue() As Object
        Get
            Helper.Assert(IsConstant)
            Dim value As Object = Expression.ConstantValue
            Helper.Assert(value IsNot Nothing)
            Select Case Helper.GetTypeCode(Compiler, value.GetType)
                Case TypeCode.SByte
                    Return -CSByte(value)
                Case TypeCode.Int16
                    Return -CShort(value)
                Case TypeCode.Int32
                    Return -CInt(value)
                Case TypeCode.Int64
                    Return -CLng(value)
                Case TypeCode.Byte
                Case TypeCode.UInt16
                Case TypeCode.UInt32
                Case TypeCode.UInt64
                    Return -CULng(value)
                Case TypeCode.Decimal
                    Return -CDec(value)
                Case TypeCode.Double
                    Return -CDbl(value)
                Case TypeCode.Single
                    Return -CSng(value)
                Case Else
                    Helper.Stop()
            End Select
            Helper.Stop()
            Return Nothing
        End Get
    End Property

    Public Overrides ReadOnly Property Keyword() As KS
        Get
            Return KS.Minus
        End Get
    End Property
End Class