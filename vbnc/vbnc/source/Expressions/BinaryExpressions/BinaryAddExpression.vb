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

Public Class BinaryAddExpression
    Inherits BinaryExpression

    Protected Overrides Function ResolveExpressions(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveExpressions(Info) AndAlso result

        If result = False Then Return result

        Dim l, lS, r, rS As Boolean
        l = Helper.CompareType(m_LeftExpression.ExpressionType, Compiler.TypeCache.System_DBNull)
        r = Helper.CompareType(m_RightExpression.ExpressionType, Compiler.TypeCache.System_DBNull)
        If l = False Then lS = Helper.CompareType(m_LeftExpression.ExpressionType, Compiler.TypeCache.System_String)
        If r = False Then rS = Helper.CompareType(m_RightExpression.ExpressionType, Compiler.TypeCache.System_String)

        If (l AndAlso rS) Then 'DBNull + String
            m_LeftExpression = New NothingConstantExpression(Me)
            result = m_LeftExpression.ResolveExpression(Info) AndAlso result
        ElseIf (lS AndAlso r) Then 'String + DBNull
            m_RightExpression = New NothingConstantExpression(Me)
            result = m_RightExpression.ResolveExpression(Info) AndAlso result
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
            Case TypeCode.Int16, TypeCode.Int32, TypeCode.Int64, TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64, TypeCode.Byte, TypeCode.SByte, TypeCode.Boolean
                Emitter.EmitAddOrAddOvf(Info, OperandType)
            Case TypeCode.Double, TypeCode.Single
                Emitter.EmitAdd(Info, OperandType)
            Case TypeCode.Decimal
                Emitter.EmitCall(Info, Compiler.TypeCache.System_Decimal__Add_Decimal_Decimal)
            Case TypeCode.Object
                Helper.Assert(Helper.CompareType(OperandType, Compiler.TypeCache.System_Object))
                Emitter.EmitCall(Info, Compiler.TypeCache.MS_VB_CS_Operators__AddObject_Object_Object)
            Case TypeCode.String
                Emitter.EmitCall(Info, Compiler.TypeCache.System_String__Concat_String_String)
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
            Return KS.Add
        End Get
    End Property

    Public Overrides Function GetConstant(ByRef m_ConstantValue As Object, ByVal lvalue As Object, ByVal rvalue As Object) As Boolean
        Dim tlvalue, trvalue As Mono.Cecil.TypeReference
        Dim clvalue, crvalue As TypeCode

        tlvalue = CecilHelper.GetType(Compiler, lvalue)
        clvalue = Helper.GetTypeCode(Compiler, tlvalue)
        trvalue = CecilHelper.GetType(Compiler, rvalue)
        crvalue = Helper.GetTypeCode(Compiler, trvalue)

        If clvalue = TypeCode.String AndAlso crvalue = TypeCode.String Then
            m_ConstantValue = CStr(lvalue) & CStr(rvalue)
            Return True
        End If

        Dim csmallest As TypeCode
        csmallest = vbnc.TypeConverter.GetBinaryAddResultType(clvalue, crvalue)

        Select Case csmallest
            Case TypeCode.Byte
                Dim tmp As UShort = CUShort(lvalue) + CUShort(rvalue)
                If tmp < Byte.MinValue OrElse tmp > Byte.MaxValue Then
                    m_ConstantValue = tmp
                Else
                    m_ConstantValue = CByte(tmp)
                End If
            Case TypeCode.SByte
                Dim tmp As Short = CShort(lvalue) + CShort(rvalue)
                If tmp < SByte.MinValue OrElse tmp > SByte.MaxValue Then
                    m_ConstantValue = tmp
                Else
                    m_ConstantValue = CSByte(tmp)
                End If
            Case TypeCode.Int16
                Dim tmp As Integer = CInt(lvalue) + CInt(rvalue)
                If tmp > Short.MaxValue OrElse tmp < Short.MinValue Then
                    m_ConstantValue = tmp
                Else
                    m_ConstantValue = CShort(tmp)
                End If
            Case TypeCode.UInt16
                Dim tmp As UInteger = CUInt(lvalue) + CUInt(rvalue)
                If tmp > UShort.MaxValue Then
                    m_ConstantValue = tmp
                Else
                    m_ConstantValue = CUShort(tmp)
                End If
            Case TypeCode.Int32
                Dim tmp As Long = CLng(lvalue) + CLng(rvalue)
                If tmp > Integer.MaxValue OrElse tmp < Integer.MinValue Then
                    m_ConstantValue = tmp
                Else
                    m_ConstantValue = CInt(tmp)
                End If
            Case TypeCode.UInt32
                Dim tmp As ULong = CULng(lvalue) + CULng(rvalue)
                If tmp > UInteger.MaxValue Then
                    m_ConstantValue = tmp
                Else
                    m_ConstantValue = CUInt(tmp)
                End If
            Case TypeCode.Int64
                Dim tmp As Double = CLng(lvalue) + CLng(rvalue)
                If tmp < Long.MinValue OrElse tmp > Long.MaxValue Then
                    m_ConstantValue = tmp
                Else
                    m_ConstantValue = CLng(tmp)
                End If
            Case TypeCode.UInt64
                Dim tmp As Double = CULng(lvalue) + CULng(rvalue)
                If tmp < ULong.MinValue OrElse tmp > ULong.MaxValue Then
                    m_ConstantValue = tmp
                Else
                    m_ConstantValue = CULng(tmp)
                End If
            Case TypeCode.Double
                m_ConstantValue = CDbl(lvalue) + CDbl(rvalue) 'No overflow possible
            Case TypeCode.Single
                m_ConstantValue = CSng(lvalue) + CSng(rvalue) 'No overflow possible
            Case TypeCode.Decimal
                m_ConstantValue = CDec(lvalue) + CDec(rvalue)
            Case Else
                Return False
        End Select

        Return True
    End Function
End Class
