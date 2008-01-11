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

Public Class CStrExpression
    Inherits ConversionExpression

    Sub New(ByVal Parent As ParsedObject, ByVal Expression As Expression)
        MyBase.New(Parent, Expression)
    End Sub

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Return GenerateCode(Me.Expression, Info)
    End Function

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveExpressionInternal(Info) AndAlso result
        result = Validate(Info, Expression.ExpressionType) AndAlso result

        Return result
    End Function

    Shared Function Validate(ByVal Info As ResolveInfo, ByVal SourceType As Type) As Boolean
        Dim result As Boolean = True

        'Dim expType As Type = SourceType
        'Dim expTypeCode As TypeCode = Helper.GetTypeCode(Compiler, expType)
        'Dim ExpressionType As Type = Info.Compiler.TypeCache.String

        Return result
    End Function

    Overloads Shared Function GenerateCode(ByVal Expression As Expression, ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Dim expType As Type = Expression.ExpressionType
        Dim expTypeCode As TypeCode = Helper.GetTypeCode(Info.Compiler, expType)

        result = Expression.Classification.GenerateCode(Info.Clone(Expression, expType)) AndAlso result

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
                Info.Stack.SwitchHead(expType, Info.Compiler.TypeCache.System_Int32)
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
                    Info.Stack.SwitchHead(Info.Compiler.TypeCache.Nothing, Info.Compiler.TypeCache.System_String)
                ElseIf Helper.CompareType(expType, Info.Compiler.TypeCache.System_Char_Array) Then
                    Emitter.EmitNew(Info, Info.Compiler.TypeCache.System_String__ctor_Array)
                Else
                    result = CTypeExpression.GenerateUserDefinedConversionCode(Info, Expression, Info.Compiler.TypeCache.System_String) AndAlso result
                End If
            Case TypeCode.Decimal
                Emitter.EmitCall(Info, Info.Compiler.TypeCache.MS_VB_CS_Conversions__ToString_Decimal)
            Case Else
                Return Info.Compiler.Report.ShowMessage(Messages.VBNC99997, Expression.Location)
        End Select

        Return result
    End Function

    Public Overrides ReadOnly Property IsConstant() As Boolean
        Get
            Return Expression.IsConstant AndAlso (Helper.CompareType(Expression.ExpressionType, Compiler.TypeCache.System_String) OrElse Helper.CompareType(Expression.ExpressionType, Compiler.TypeCache.System_Char))
        End Get
    End Property

    Public Overrides ReadOnly Property ConstantValue() As Object
        Get
            Dim tpCode As TypeCode
            Dim originalValue As Object
            originalValue = Expression.ConstantValue
            tpCode = Helper.GetTypeCode(Compiler, originalValue.GetType)
            Select Case tpCode
                Case TypeCode.Char, TypeCode.String
                    Return CStr(originalValue)
                Case Else
                    Compiler.Report.ShowMessage(Messages.VBNC30060, originalValue.ToString, ExpressionType.ToString)
                    Return False
            End Select
        End Get
    End Property

    Overrides ReadOnly Property ExpressionType() As Type
        Get
            Return Compiler.TypeCache.System_String '_Descriptor
        End Get
    End Property
End Class
