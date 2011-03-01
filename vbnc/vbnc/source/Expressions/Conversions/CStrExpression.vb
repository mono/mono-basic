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

Public Class CStrExpression
    Inherits ConversionExpression

    Sub New(ByVal Parent As ParsedObject, ByVal Expression As Expression)
        MyBase.New(Parent, Expression)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal IsExplicit As Boolean)
        MyBase.New(Parent, IsExplicit)
    End Sub

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Return GenerateCode(Me, Info)
    End Function

    Public Overrides Function GetConstant(ByRef result As Object, ByVal ShowError As Boolean) As Boolean
        If Not Expression.GetConstant(result, ShowError) Then Return False
        Return ConvertToString(result, ShowError)
    End Function

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveExpressionInternal(Info) AndAlso result
        result = Validate(Info, Me) AndAlso result

        Return result
    End Function

    Shared Function Validate(ByVal Info As ResolveInfo, ByVal Conversion As ConversionExpression) As Boolean
        Dim result As Boolean = True

        Dim Expression As Expression = Conversion.Expression
        Dim ExpressionType As TypeReference = Conversion.ExpressionType
        Dim expType As Mono.Cecil.TypeReference = Expression.ExpressionType
        Dim expTypeCode As TypeCode = Helper.GetTypeCode(Info.Compiler, expType)

        Select Case expTypeCode
            Case TypeCode.Object
                If Helper.CompareType(expType, Info.Compiler.TypeCache.System_Char_Array) Then
                    'OK
                ElseIf Helper.CompareType(expType, Info.Compiler.TypeCache.System_Object) Then
                    'OK
                ElseIf Helper.CompareType(expType, Info.Compiler.TypeCache.Nothing) Then
                    'OK
                ElseIf Helper.CompareType(expType, Info.Compiler.TypeCache.System_Char_Array) Then
                    'OK
                Else
                    result = Conversion.FindUserDefinedConversionOperator() AndAlso result
                End If
        End Select

        Return result
    End Function

    Overloads Shared Function GenerateCode(ByVal Conversion As ConversionExpression, ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True
        Dim expType As Mono.Cecil.TypeReference = Nothing
        Dim expTypeCode As TypeCode
        Dim Expression As Expression = Conversion.Expression

        result = GenerateCodeForExpression(Conversion, Info, expTypeCode, expType) AndAlso result

        Select Case expTypeCode
            Case TypeCode.Boolean
                Emitter.EmitCall(Info, Info.Compiler.TypeCache.MS_VB_CS_Conversions__ToString_Boolean)
            Case TypeCode.String
                'Nothing to do
            Case TypeCode.Char
                Emitter.EmitCall(Info, Info.Compiler.TypeCache.MS_VB_CS_Conversions__ToString_Char)
            Case TypeCode.DateTime
                Emitter.EmitCall(Info, Info.Compiler.TypeCache.MS_VB_CS_Conversions__ToString_DateTime)
            Case TypeCode.SByte, TypeCode.Int16
                Emitter.EmitCall(Info, Info.Compiler.TypeCache.MS_VB_CS_Conversions__ToString_Int32)
            Case TypeCode.Int32
                Emitter.EmitCall(Info, Info.Compiler.TypeCache.MS_VB_CS_Conversions__ToString_Int32)
            Case TypeCode.Int64
                Emitter.EmitCall(Info, Info.Compiler.TypeCache.MS_VB_CS_Conversions__ToString_Int64)
            Case TypeCode.Byte
                Emitter.EmitCall(Info, Info.Compiler.TypeCache.MS_VB_CS_Conversions__ToString_Byte)
            Case TypeCode.UInt16, TypeCode.UInt32
                Emitter.EmitCall(Info, Info.Compiler.TypeCache.MS_VB_CS_Conversions__ToString_UInt32)
            Case TypeCode.UInt64
                Emitter.EmitCall(Info, Info.Compiler.TypeCache.MS_VB_CS_Conversions__ToString_UInt64)
            Case TypeCode.Double
                Emitter.EmitCall(Info, Info.Compiler.TypeCache.MS_VB_CS_Conversions__ToString_Double)
            Case TypeCode.Single
                Emitter.EmitCall(Info, Info.Compiler.TypeCache.MS_VB_CS_Conversions__ToString_Single)
            Case TypeCode.Object
                If Helper.CompareType(expType, Info.Compiler.TypeCache.System_Char_Array) Then
                    Emitter.EmitNew(Info, Info.Compiler.TypeCache.System_String__ctor_Array)
                ElseIf Helper.CompareType(expType, Info.Compiler.TypeCache.System_Object) Then
                    Emitter.EmitCall(Info, Info.Compiler.TypeCache.MS_VB_CS_Conversions__ToString_Object)
                ElseIf Helper.CompareType(expType, Info.Compiler.TypeCache.Nothing) Then
                    'No conversion necessary
                ElseIf Helper.CompareType(expType, Info.Compiler.TypeCache.System_Char_Array) Then
                    Emitter.EmitNew(Info, Info.Compiler.TypeCache.System_String__ctor_Array)
                Else
                    Return Info.Compiler.Report.ShowMessage(Messages.VBNC99999, Expression.Location, "Can't convert")
                End If
            Case TypeCode.Decimal
                Emitter.EmitCall(Info, Info.Compiler.TypeCache.MS_VB_CS_Conversions__ToString_Decimal)
            Case Else
                Return Info.Compiler.Report.ShowMessage(Messages.VBNC99997, Expression.Location)
        End Select

        Return result
    End Function

    Overrides ReadOnly Property ExpressionType() As Mono.Cecil.TypeReference
        Get
            Return Compiler.TypeCache.System_String
        End Get
    End Property
End Class
