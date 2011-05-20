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

Public Class XOrExpression
    Inherits BinaryExpression

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        ValidateBeforeGenerateCode(Info)

        Dim expInfo As EmitInfo = Info.Clone(Me, True, False, OperandType)

        result = m_LeftExpression.GenerateCode(expInfo) AndAlso result
        result = m_RightExpression.GenerateCode(expInfo) AndAlso result

        Select Case OperandTypeCode
            Case TypeCode.Byte, TypeCode.SByte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64, TypeCode.Boolean
                Emitter.EmitXOr(Info, OperandType)
            Case TypeCode.Object
                Helper.Assert(Helper.CompareType(OperandType, Compiler.TypeCache.System_Object))
                Emitter.EmitCall(Info, Compiler.TypeCache.MS_VB_CS_Operators__XorObject_Object_Object)
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
            Return KS.Xor
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
            m_ConstantValue = CBool(lvalue) Xor CBool(rvalue)
            Return True
        End If

        Dim smallest As Mono.Cecil.TypeReference
        Dim csmallest As TypeCode
        smallest = Compiler.TypeResolution.GetSmallestIntegralType(tlvalue, trvalue)
        Helper.Assert(smallest IsNot Nothing)
        csmallest = Helper.GetTypeCode(Compiler, smallest)

        Select Case csmallest
            Case TypeCode.Byte
                m_ConstantValue = CByte(lvalue) Xor CByte(rvalue)
            Case TypeCode.SByte
                m_ConstantValue = CSByte(lvalue) Xor CSByte(rvalue)
            Case TypeCode.Int16
                m_ConstantValue = CShort(lvalue) Xor CShort(rvalue)
            Case TypeCode.UInt16
                m_ConstantValue = CUShort(lvalue) Xor CUShort(rvalue)
            Case TypeCode.Int32
                m_ConstantValue = CInt(lvalue) Xor CInt(rvalue)
            Case TypeCode.UInt32
                m_ConstantValue = CUInt(lvalue) Xor CUInt(rvalue)
            Case TypeCode.Int64
                m_ConstantValue = CLng(lvalue) Xor CLng(rvalue)
            Case TypeCode.UInt64
                m_ConstantValue = CULng(lvalue) Xor CULng(rvalue)
            Case Else
                Return False
        End Select

        Return True
    End Function
End Class
