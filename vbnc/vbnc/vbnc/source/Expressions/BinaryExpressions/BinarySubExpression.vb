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

Public Class BinarySubExpression
    Inherits BinaryExpression


    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        ValidateBeforeGenerateCode(Info)

        Dim expInfo As EmitInfo = Info.Clone(Me, True, False, OperandType)

        result = m_LeftExpression.GenerateCode(expInfo) AndAlso result
        result = m_RightExpression.GenerateCode(expInfo) AndAlso result

        Select Case OperandTypeCode
            Case TypeCode.Int16, TypeCode.Int32, TypeCode.Int64, TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64, TypeCode.Byte, TypeCode.SByte, TypeCode.Boolean
                Emitter.EmitSubOrSubOvfOrSubOvfUn(Info, OperandType)
            Case TypeCode.Double, TypeCode.Single
                Emitter.EmitSub(Info, OperandType)
            Case TypeCode.Decimal
                Emitter.EmitCall(Info, Compiler.TypeCache.System_Decimal__Subtract_Decimal_Decimal)
            Case TypeCode.Object
                Helper.Assert(Helper.CompareType(OperandType, Compiler.TypeCache.System_Object))
                Emitter.EmitCall(Info, Compiler.TypeCache.MS_VB_CS_Operators__SubtractObject_Object_Object)
            Case Else
                Throw New InternalException(Me)
        End Select
        Return result
    End Function

    Overrides ReadOnly Property ExpressionType() As Mono.Cecil.TypeReference
        Get
            Return Compiler.TypeResolution.TypeCodeToType(TypeConverter.GetBinarySubResultType(Helper.GetTypeCode(Compiler, m_LeftExpression.ExpressionType), Helper.GetTypeCode(Compiler, m_RightExpression.ExpressionType)))
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject, ByVal LExp As Expression, ByVal RExp As Expression)
        MyBase.New(Parent, LExp, RExp)
    End Sub

    Public Overrides ReadOnly Property Keyword() As KS
        Get
            Return KS.Minus
        End Get
    End Property

    Public Overrides ReadOnly Property IsConstant() As Boolean
        Get
            Return MyBase.IsConstant 'CHECK: is this true?
        End Get
    End Property

    Public Overrides ReadOnly Property ConstantValue() As Object
        Get
            Dim rvalue, lvalue As Object
            lvalue = m_LeftExpression.ConstantValue
            rvalue = m_RightExpression.ConstantValue
            If lvalue Is Nothing Or rvalue Is Nothing Then
                Return Nothing
            Else

                Dim tlvalue, trvalue As Mono.Cecil.TypeReference
                Dim clvalue, crvalue As TypeCode
                tlvalue = CecilHelper.GetType(Compiler, lvalue)
                clvalue = Helper.GetTypeCode(Compiler, tlvalue)
                trvalue = CecilHelper.GetType(Compiler, rvalue)
                crvalue = Helper.GetTypeCode(Compiler, trvalue)

                Dim csmallest As TypeCode
                csmallest = vbnc.TypeConverter.GetBinaryOperandType(Compiler, Me.Keyword, tlvalue, trvalue)

                Select Case csmallest
                    Case TypeCode.Byte
                        If CByte(lvalue) < CByte(rvalue) Then
                            Return CSByte(lvalue) - CSByte(rvalue)
                        Else
                            Return CByte(lvalue) - CByte(rvalue)
                        End If
                    Case TypeCode.SByte
                        Dim tmp As Short = CShort(lvalue) - CShort(rvalue)
                        If tmp < SByte.MinValue OrElse tmp > SByte.MaxValue Then
                            Return tmp
                        Else
                            Return CSByte(tmp)
                        End If
                    Case TypeCode.Int16
                        Dim tmp As Integer = CInt(lvalue) - CInt(rvalue)
                        If tmp > Short.MaxValue OrElse tmp < Short.MinValue Then
                            Return tmp
                        Else
                            Return CShort(tmp)
                        End If
                    Case TypeCode.UInt16
                        If CUShort(lvalue) < CUShort(rvalue) Then
                            Return CShort(lvalue) - CShort(rvalue)
                        Else
                            Return CUShort(lvalue) - CUShort(rvalue)
                        End If
                    Case TypeCode.Int32
                        Dim tmp As Long = CLng(lvalue) - CLng(rvalue)
                        If tmp > Integer.MaxValue OrElse tmp < Integer.MinValue Then
                            Return tmp
                        Else
                            Return CInt(tmp)
                        End If
                    Case TypeCode.UInt32
                        If CUInt(lvalue) < CUInt(rvalue) Then
                            Return CInt(lvalue) - CInt(rvalue)
                        Else
                            Return CUInt(lvalue) - CUInt(rvalue)
                        End If
                    Case TypeCode.Int64
                        Dim tmp As Double = CLng(lvalue) - CLng(rvalue)
                        If tmp < Long.MinValue OrElse tmp > Long.MaxValue Then
                            Return tmp
                        Else
                            Return CLng(tmp)
                        End If
                    Case TypeCode.UInt64
                        If CULng(lvalue) < CULng(rvalue) Then
                            Return CLng(lvalue) - CLng(rvalue)
                        Else
                            Return CULng(lvalue) - CULng(rvalue)
                        End If
                    Case TypeCode.Double
                        Return CDbl(lvalue) - CDbl(rvalue) 'No overflow possible
                    Case TypeCode.Single
                        Return CSng(lvalue) - CSng(rvalue) 'No overflow possible
                    Case TypeCode.Decimal
                        Return CDec(lvalue) - CDec(rvalue)
                    Case Else
                        Helper.Stop()
                        Throw New InternalException(Me)
                End Select
            End If
        End Get
    End Property
End Class
