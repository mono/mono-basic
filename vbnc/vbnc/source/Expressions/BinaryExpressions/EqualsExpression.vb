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

Public Class EqualsExpression
    Inherits BinaryExpression

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        ValidateBeforeGenerateCode(Info)

        Dim expInfo As EmitInfo = Info.Clone(Me, True, False, OperandType)

        result = m_LeftExpression.GenerateCode(expInfo) AndAlso result
        result = m_RightExpression.GenerateCode(expInfo) AndAlso result

        Select Case OperandTypeCode
            Case TypeCode.Byte, TypeCode.SByte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64, TypeCode.Single, TypeCode.Double, TypeCode.Boolean, TypeCode.Char
                Emitter.EmitEquals(Info, OperandType)
            Case TypeCode.DateTime
                Helper.Assert(Compiler.TypeCache.System_DateTime__Compare_DateTime_DateTime IsNot Nothing, "Date_Compare__Date_Date Is Nothing")
                Emitter.EmitCall(Info, Compiler.TypeCache.System_DateTime__Compare_DateTime_DateTime)
                Emitter.EmitLoadI4Value(Info, 0)
                Emitter.EmitEquals(Info, Compiler.TypeCache.System_Int32)
            Case TypeCode.Decimal
                Helper.Assert(Compiler.TypeCache.System_Decimal__Compare_Decimal_Decimal IsNot Nothing, "Decimal_Compare__Decimal_Decimal Is Nothing")
                Emitter.EmitCall(Info, Compiler.TypeCache.System_Decimal__Compare_Decimal_Decimal)
                Emitter.EmitLoadI4Value(Info, 0)
                Emitter.EmitEquals(Info, Compiler.TypeCache.System_Int32)
            Case TypeCode.Object
                Helper.Assert(Compiler.TypeCache.MS_VB_CS_Operators__ConditionalCompareObjectEqual_Object_Object_Boolean IsNot Nothing, "MS_VB_CS_Operators_ConditionalCompareObjectEqual__Object_Object_Bool Is Nothing")
                Helper.Assert(Helper.CompareType(OperandType, Compiler.TypeCache.System_Object))
                Emitter.EmitLoadI4Value(Info, Info.IsOptionCompareText)
                'Compiler.Report.WriteLine("MS_VB_CS_Operators_ConditionalCompareObjectEqual__Object_Object_Bool: " & Me.Location.ToString)

                If Helper.CompareType(ExpressionType, Compiler.TypeCache.System_Object) Then
                    Emitter.EmitCall(Info, Compiler.TypeCache.MS_VB_CS_Operators__CompareObjectEqual_Object_Object_Boolean)
                Else
                    Emitter.EmitCall(Info, Compiler.TypeCache.MS_VB_CS_Operators__ConditionalCompareObjectEqual_Object_Object_Boolean)
                End If
            Case TypeCode.String
                Helper.Assert(Compiler.TypeCache.MS_VB_CS_Operators__CompareString_String_String_Boolean IsNot Nothing, "MS_VB_CS_Operators_CompareString__String_String_Bool Is Nothing")
                Emitter.EmitLoadI4Value(Info, Info.IsOptionCompareText)
                'Compiler.Report.WriteLine("MS_VB_CS_Operators_CompareString__String_String_Bool: " & Me.Location.ToString)
                Emitter.EmitCall(Info, Compiler.TypeCache.MS_VB_CS_Operators__CompareString_String_String_Boolean)
                Emitter.EmitLoadI4Value(Info, 0)
                Emitter.EmitEquals(Info, Compiler.TypeCache.System_Int32)
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
            Return KS.Equals
        End Get
    End Property

    Public Overrides Function GetConstant(ByRef result As Object, ByVal lvalue As Object, ByVal rvalue As Object) As Boolean
        Dim tlvalue, trvalue As Mono.Cecil.TypeReference
        Dim clvalue, crvalue As TypeCode

        tlvalue = CecilHelper.GetType(Compiler, lvalue)
        clvalue = Helper.GetTypeCode(Compiler, tlvalue)
        trvalue = CecilHelper.GetType(Compiler, rvalue)
        crvalue = Helper.GetTypeCode(Compiler, trvalue)

        If clvalue = TypeCode.Boolean AndAlso crvalue = TypeCode.Boolean Then
            result = CBool(lvalue) = CBool(rvalue)
            Return True
        ElseIf clvalue = TypeCode.DateTime AndAlso crvalue = TypeCode.DateTime Then
            result = CDate(lvalue) = CDate(rvalue)
            Return True
        ElseIf clvalue = TypeCode.Char AndAlso crvalue = TypeCode.Char Then
            result = CChar(lvalue) = CChar(rvalue)
            Return True
        ElseIf clvalue = TypeCode.String AndAlso crvalue = TypeCode.String Then
            result = CStr(lvalue) = CStr(rvalue)
            Return True
        ElseIf clvalue = TypeCode.String AndAlso crvalue = TypeCode.Char OrElse _
         clvalue = TypeCode.Char AndAlso crvalue = TypeCode.String Then
            result = CStr(lvalue) = CStr(rvalue)
            Return True
        End If

        Dim smallest As Mono.Cecil.TypeReference
        Dim csmallest As TypeCode
        smallest = Compiler.TypeResolution.GetSmallestIntegralType(tlvalue, trvalue)
        Helper.Assert(smallest IsNot Nothing)
        csmallest = Helper.GetTypeCode(Compiler, smallest)

        Select Case csmallest
            Case TypeCode.Byte
                result = CByte(lvalue) = CByte(rvalue)
            Case TypeCode.SByte
                result = CSByte(lvalue) = CSByte(rvalue)
            Case TypeCode.Int16
                result = CShort(lvalue) = CShort(rvalue)
            Case TypeCode.UInt16
                result = CUShort(lvalue) = CUShort(rvalue)
            Case TypeCode.Int32
                result = CInt(lvalue) = CInt(rvalue)
            Case TypeCode.UInt32
                result = CUInt(lvalue) = CUInt(rvalue)
            Case TypeCode.Int64
                result = CLng(lvalue) = CLng(rvalue)
            Case TypeCode.UInt64
                result = CULng(lvalue) = CULng(rvalue)
            Case TypeCode.Double
                result = CDbl(lvalue) = CDbl(rvalue)
            Case TypeCode.Single
                result = CSng(lvalue) = CSng(rvalue)
            Case TypeCode.Decimal
                result = CDec(lvalue) = CDec(rvalue)
            Case Else
                Return False
        End Select

        Return True
    End Function
End Class

