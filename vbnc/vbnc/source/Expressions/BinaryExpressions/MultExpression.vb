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

Public Class MultExpression
    Inherits BinaryExpression

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        ValidateBeforeGenerateCode(Info)

        Dim expInfo As EmitInfo = Info.Clone(Me, True, False, OperandType)

        result = m_LeftExpression.GenerateCode(expInfo) AndAlso result
        result = m_RightExpression.GenerateCode(expInfo) AndAlso result

        Select Case OperandTypeCode
            Case TypeCode.SByte, TypeCode.Byte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64
                Emitter.EmitMultOrMultOvf(Info, OperandType)
            Case TypeCode.Double, TypeCode.Single
                Emitter.EmitMult(Info, OperandType)
            Case TypeCode.Decimal
                Emitter.EmitCall(Info, Compiler.TypeCache.System_Decimal__Multiply_Decimal_Decimal)
            Case TypeCode.Object
                Helper.Assert(Helper.CompareType(OperandType, Compiler.TypeCache.System_Object))
                Emitter.EmitCall(Info, Compiler.TypeCache.MS_VB_CS_Operators__MultiplyObject_Object_Object)
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
            Return KS.Mult
        End Get
    End Property

    Public Overrides Function GetConstant(ByRef m_ConstantValue As Object, ByVal lvalue As Object, ByVal rvalue As Object) As Boolean
        Dim tlvalue, trvalue As Mono.Cecil.TypeReference
        Dim clvalue, crvalue As TypeCode

        tlvalue = CecilHelper.GetType(Compiler, lvalue)
        clvalue = Helper.GetTypeCode(Compiler, tlvalue)
        trvalue = CecilHelper.GetType(Compiler, rvalue)
        crvalue = Helper.GetTypeCode(Compiler, trvalue)

        Dim csmallest As TypeCode
        csmallest = TypeConverter.GetBinaryOperandType(Compiler, Me.Keyword, tlvalue, trvalue)

        Select Case csmallest
            Case TypeCode.Byte
                Dim tmp As UShort = CUShort(lvalue) * CUShort(rvalue)
                If tmp < Byte.MinValue OrElse tmp > Byte.MaxValue Then
                    m_ConstantValue = tmp
                Else
                    m_ConstantValue = CByte(lvalue) * CByte(rvalue)
                End If
            Case TypeCode.SByte
                Dim tmp As Short = CShort(lvalue) * CShort(rvalue)
                If tmp < SByte.MinValue OrElse tmp > SByte.MaxValue Then
                    m_ConstantValue = tmp
                Else
                    m_ConstantValue = CSByte(lvalue) * CSByte(rvalue)
                End If
            Case TypeCode.Int16
                Dim tmp As Integer = CInt(lvalue) * CInt(rvalue)
                If tmp > Short.MaxValue OrElse tmp < Short.MinValue Then
                    m_ConstantValue = tmp
                Else
                    m_ConstantValue = CShort(lvalue) * CShort(rvalue)
                End If
            Case TypeCode.UInt16
                Dim tmp As UInteger = CUInt(lvalue) * CUInt(rvalue)
                If tmp > UShort.MaxValue Then
                    m_ConstantValue = tmp
                Else
                    m_ConstantValue = CUShort(lvalue) * CUShort(rvalue)
                End If
            Case TypeCode.Int32
                Dim tmp As Long = CLng(lvalue) * CLng(rvalue)
                If tmp > Integer.MaxValue OrElse tmp < Integer.MinValue Then
                    m_ConstantValue = tmp
                Else
                    m_ConstantValue = CInt(lvalue) * CInt(rvalue)
                End If
            Case TypeCode.UInt32
                Dim tmp As ULong = CULng(lvalue) * CULng(rvalue)
                If tmp > UInteger.MaxValue Then
                    m_ConstantValue = tmp
                Else
                    m_ConstantValue = CUInt(lvalue) * CUInt(rvalue)
                End If
            Case TypeCode.Int64
                Dim tmp As Double
                If CLng(rvalue) < 0 Then
                    tmp = Long.MaxValue / -CLng(rvalue)
                Else
                    tmp = Long.MaxValue / CLng(rvalue)
                End If
                If CLng(lvalue) < 0 AndAlso -CLng(lvalue) > tmp OrElse CLng(lvalue) > tmp Then
                    m_ConstantValue = CDec(lvalue) * CDec(rvalue)
                Else
                    m_ConstantValue = CLng(lvalue) * CLng(rvalue)
                End If
            Case TypeCode.UInt64
                If CULng(lvalue) > ULong.MaxValue / CULng(rvalue) Then
                    m_ConstantValue = CDec(lvalue) * CDec(rvalue)
                Else
                    m_ConstantValue = CULng(lvalue) * CULng(rvalue)
                End If
            Case TypeCode.Double
                m_ConstantValue = CDbl(lvalue) * CDbl(rvalue) 'No overflow possible
            Case TypeCode.Single
                m_ConstantValue = CSng(lvalue) * CSng(rvalue) 'No overflow possible
            Case TypeCode.Decimal
                m_ConstantValue = CDec(lvalue) * CDec(rvalue)
            Case Else
                Return False
        End Select

        Return True
    End Function
End Class
