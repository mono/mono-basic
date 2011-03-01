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

Public Class CBoolExpression
    Inherits ConversionExpression

    Sub New(ByVal Parent As ParsedObject, ByVal IsExplicit As Boolean)
        MyBase.New(Parent, IsExplicit)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Expression As Expression)
        MyBase.New(Parent, Expression)
    End Sub

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Return GenerateCode(Me, Info)
    End Function

    Overloads Shared Function GenerateCode(ByVal Conversion As ConversionExpression, ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True
        Dim expType As Mono.Cecil.TypeReference = Nothing
        Dim expTypeCode As TypeCode
        Dim Expression As Expression = Conversion.Expression

        result = GenerateCodeForExpression(Conversion, Info, expTypeCode, expType) AndAlso result

        Select Case expTypeCode
            Case TypeCode.Boolean
                'Nothing to do
            Case TypeCode.Char, TypeCode.DateTime
                Info.Compiler.Report.ShowMessage(Messages.VBNC30311, Expression.Location, Helper.ToString(Expression, expType), Helper.ToString(Expression, expType))
                result = False
            Case TypeCode.Byte, TypeCode.UInt16, TypeCode.UInt32, TypeCode.SByte, TypeCode.Int16, TypeCode.Int32
                Emitter.EmitLoadI4Value(Info, 0I, expType)
                Emitter.EmitGT_Un(Info, expType)
            Case TypeCode.Int64
                Emitter.EmitLoadI8Value(Info, 0L, expType)
                Emitter.EmitGT_Un(Info, expType)
            Case TypeCode.UInt64
                Emitter.EmitLoadI8Value(Info, 0UL, expType)
                Emitter.EmitGT_Un(Info, expType)
            Case TypeCode.Double
                Emitter.EmitLoadR8Value(Info, 0.0, expType)
                Emitter.EmitEquals(Info, expType)
                Emitter.EmitLoadI4Value(Info, 0I, Info.Compiler.TypeCache.System_Boolean)
                Emitter.EmitEquals(Info, Info.Compiler.TypeCache.System_Boolean)
            Case TypeCode.Single
                Emitter.EmitLoadR4Value(Info, 0.0!, expType)
                Emitter.EmitEquals(Info, expType)
                Emitter.EmitLoadI4Value(Info, 0I, Info.Compiler.TypeCache.System_Boolean)
                Emitter.EmitEquals(Info, Info.Compiler.TypeCache.System_Boolean)
            Case TypeCode.Object
                Helper.Assert(Info.Compiler.TypeCache.MS_VB_CS_Conversions__ToBoolean_Object IsNot Nothing, "MS_VB_CS_Conversions_ToBoolean__Object Is Nothing")
                If Helper.CompareType(expType, Info.Compiler.TypeCache.System_Object) Then
                    Emitter.EmitCall(Info, Info.Compiler.TypeCache.MS_VB_CS_Conversions__ToBoolean_Object)
                ElseIf Helper.CompareType(expType, Info.Compiler.TypeCache.Nothing) Then
                    Emitter.EmitCall(Info, Info.Compiler.TypeCache.MS_VB_CS_Conversions__ToBoolean_Object)
                Else
                    Return Info.Compiler.Report.ShowMessage(Messages.VBNC99997, Expression.Location)
                End If
            Case TypeCode.String
                Helper.Assert(Info.Compiler.TypeCache.MS_VB_CS_Conversions__ToBoolean_String IsNot Nothing, "MS_VB_CS_Conversions_ToBoolean__String Is Nothing")
                Emitter.EmitCall(Info, Info.Compiler.TypeCache.MS_VB_CS_Conversions__ToBoolean_String)
            Case TypeCode.Decimal
                Helper.Assert(Info.Compiler.TypeCache.System_Convert__ToBoolean_Decimal IsNot Nothing, "System_Convert_ToBoolean__Decimal Is Nothing")
                Emitter.EmitCall(Info, Info.Compiler.TypeCache.System_Convert__ToBoolean_Decimal)
            Case Else
                Return Info.Compiler.Report.ShowMessage(Messages.VBNC99997, Expression.Location)
        End Select

        Return result
    End Function

    Public Overrides Function GetConstant(ByRef result As Object, ByVal ShowError As Boolean) As Boolean
        If Not Expression.GetConstant(result, ShowError) Then Return False
        Return ConvertToBoolean(result, ShowError)
    End Function

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveExpressionInternal(Info) AndAlso result

        If result = False Then Return result

        result = Validate(Info, Me) AndAlso result

        Return result
    End Function

    Shared Function Validate(ByVal Info As ResolveInfo, ByVal Conversion As ConversionExpression) As Boolean
        Dim result As Boolean = True

        Dim expType As Mono.Cecil.TypeReference = Nothing
        Dim expTypeCode As TypeCode
        Dim Expression As Expression = Conversion.Expression
        Dim ExpressionType As TypeReference = Conversion.ExpressionType
        
        result = ValidateForNullable(Info, Conversion, expTypeCode, expType) AndAlso result

        If Conversion.GetConstant(Nothing, False) Then Return result

        Select Case expTypeCode
            Case TypeCode.Char, TypeCode.DateTime
                Info.Compiler.Report.ShowMessage(Messages.VBNC30311, Expression.Location, Helper.ToString(Expression, expType), Helper.ToString(Expression, Info.Compiler.TypeCache.System_Boolean))
                result = False
            Case TypeCode.Object
                If Helper.CompareType(expType, Info.Compiler.TypeCache.System_Object) Then
                    'OK
                ElseIf Helper.CompareType(expType, Info.Compiler.TypeCache.Nothing) Then
                    'OK
                Else
                    result = Conversion.FindUserDefinedConversionOperator() AndAlso result
                End If
            Case TypeCode.Boolean
                'Implicitly convertible
            Case Else
                If Conversion.IsExplicit = False AndAlso Conversion.Location.File(Conversion.Compiler).IsOptionStrictOn Then
                    result = Conversion.Compiler.Report.ShowMessage(Messages.VBNC30512, Conversion.Location, Helper.ToString(Conversion, expType), Helper.ToString(Conversion, ExpressionType)) AndAlso result
                End If
        End Select

        Return result
    End Function

    Overrides ReadOnly Property ExpressionType() As Mono.Cecil.TypeReference
        Get
            Return Compiler.TypeCache.System_Boolean
        End Get
    End Property
End Class

