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

Public Class LEExpression
    Inherits BinaryExpression

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        ValidateBeforeGenerateCode(Info)

        Dim eqInfo As EmitInfo = Info.Clone(Me, True, False, OperandType)

        result = m_LeftExpression.GenerateCode(eqInfo) AndAlso result
        result = m_RightExpression.GenerateCode(eqInfo) AndAlso result

        Select Case OperandTypeCode
            Case TypeCode.Boolean
                Emitter.EmitGE(Info, OperandType) 'LAMESPEC
            Case TypeCode.SByte, TypeCode.Int16, TypeCode.Int32, TypeCode.Int64, TypeCode.Single, TypeCode.Double, TypeCode.Char
                Emitter.EmitLE(Info, OperandType)
            Case TypeCode.Byte, TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64
                Emitter.EmitLE_Un(Info, OperandType)
            Case TypeCode.DateTime
                Emitter.EmitCall(Info, Compiler.TypeCache.System_DateTime__Compare_DateTime_DateTime)
                Emitter.EmitLoadI4Value(Info, 0)
                Emitter.EmitLE(Info, Compiler.TypeCache.System_Int32)
            Case TypeCode.Decimal
                Emitter.EmitCall(Info, Compiler.TypeCache.System_Decimal__Compare_Decimal_Decimal)
                Emitter.EmitLoadI4Value(Info, 0)
                Emitter.EmitLE(Info, Compiler.TypeCache.System_Int32)
            Case TypeCode.Object
                Helper.Assert(Helper.CompareType(OperandType, Compiler.TypeCache.System_Object))
                Emitter.EmitLoadI4Value(Info, Info.IsOptionCompareText)
                If Helper.CompareType(ExpressionType, Compiler.TypeCache.System_Object) Then
                    Emitter.EmitCall(Info, Compiler.TypeCache.MS_VB_CS_Operators__CompareObjectLessEqual_Object_Object_Boolean)
                Else
                    Emitter.EmitCall(Info, Compiler.TypeCache.MS_VB_CS_Operators__ConditionalCompareObjectLessEqual_Object_Object_Boolean)
                End If
            Case TypeCode.String
                Emitter.EmitLoadI4Value(Info, Info.IsOptionCompareText)
                Emitter.EmitCall(Info, Compiler.TypeCache.MS_VB_CS_Operators__CompareString_String_String_Boolean)
                Emitter.EmitLoadI4Value(Info, 0)
                Emitter.EmitLE(Info, Compiler.TypeCache.System_Int32)
            Case Else
                Throw New InternalException(Me)
        End Select

        Return result
    End Function

    Overrides ReadOnly Property ExpressionType() As Mono.Cecil.TypeReference
        Get
            Return Compiler.TypeResolution.TypeCodeToType(TypeConverter.GetLEResultType(Helper.GetTypeCode(Compiler, m_LeftExpression.ExpressionType), Helper.GetTypeCode(Compiler, m_RightExpression.ExpressionType)))
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject, ByVal LExp As Expression, ByVal RExp As Expression)
        MyBase.New(Parent, LExp, RExp)
    End Sub

    Public Overrides ReadOnly Property Keyword() As KS
        Get
            Return KS.LE
        End Get
    End Property

    Public Overrides Function GetConstant(ByRef m_ConstantValue As Object, ByVal lvalue As Object, ByVal rvalue As Object) As Boolean
        Dim tlvalue, trvalue As Mono.Cecil.TypeReference
        Dim clvalue, crvalue As TypeCode

        tlvalue = CecilHelper.GetType(Compiler, lvalue)
        clvalue = Helper.GetTypeCode(Compiler, tlvalue)
        trvalue = CecilHelper.GetType(Compiler, rvalue)
        crvalue = Helper.GetTypeCode(Compiler, trvalue)

        If clvalue = TypeCode.Boolean AndAlso crvalue = TypeCode.Boolean Then
            m_ConstantValue = CBool(lvalue) <= CBool(rvalue)
            Return True
        ElseIf clvalue = TypeCode.DateTime AndAlso crvalue = TypeCode.DateTime Then
            m_ConstantValue = CDate(lvalue) <= CDate(rvalue)
            Return True
        ElseIf clvalue = TypeCode.Char AndAlso crvalue = TypeCode.Char Then
            m_ConstantValue = CChar(lvalue) <= CChar(rvalue)
            Return True
        ElseIf clvalue = TypeCode.String AndAlso crvalue = TypeCode.String Then
            m_ConstantValue = CStr(lvalue) <= CStr(rvalue)
            Return True
        ElseIf clvalue = TypeCode.String AndAlso crvalue = TypeCode.Char OrElse _
         clvalue = TypeCode.Char AndAlso crvalue = TypeCode.String Then
            m_ConstantValue = CStr(lvalue) <= CStr(rvalue)
            Return True
        End If

        Dim smallest As Mono.Cecil.TypeReference
        Dim csmallest As TypeCode
        smallest = Compiler.TypeResolution.GetSmallestIntegralType(tlvalue, trvalue)
        Helper.Assert(smallest IsNot Nothing)
        csmallest = Helper.GetTypeCode(Compiler, smallest)

        Select Case csmallest
            Case TypeCode.Byte
                m_ConstantValue = CByte(lvalue) <= CByte(rvalue)
            Case TypeCode.SByte
                m_ConstantValue = CSByte(lvalue) <= CSByte(rvalue)
            Case TypeCode.Int16
                m_ConstantValue = CShort(lvalue) <= CShort(rvalue)
            Case TypeCode.UInt16
                m_ConstantValue = CUShort(lvalue) <= CUShort(rvalue)
            Case TypeCode.Int32
                m_ConstantValue = CInt(lvalue) <= CInt(rvalue)
            Case TypeCode.UInt32
                m_ConstantValue = CUInt(lvalue) <= CUInt(rvalue)
            Case TypeCode.Int64
                m_ConstantValue = CLng(lvalue) <= CLng(rvalue)
            Case TypeCode.UInt64
                m_ConstantValue = CULng(lvalue) <= CULng(rvalue)
            Case TypeCode.Double
                m_ConstantValue = CDbl(lvalue) <= CDbl(rvalue)
            Case TypeCode.Single
                m_ConstantValue = CSng(lvalue) <= CSng(rvalue)
            Case TypeCode.Decimal
                m_ConstantValue = CDec(lvalue) <= CDec(rvalue)
            Case Else
                Return False
        End Select

        Return True
    End Function
End Class

