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

Public Class CDecExpression
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

    Overloads Shared Function GenerateCode(ByVal Expression As Expression, ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Dim expType As Type = Expression.ExpressionType
        Dim expTypeCode As TypeCode = Helper.GetTypeCode(expType)

        result = Expression.Classification.GenerateCode(Info.Clone(expType)) AndAlso result

        Select Case expTypeCode
            Case TypeCode.Boolean
                Emitter.EmitCall(Info, Info.Compiler.TypeCache.MS_VB_CS_Conversions_ToDecimal__Boolean)
            Case TypeCode.Decimal
                'Nothing to do
            Case TypeCode.DateTime, TypeCode.Char
                Info.Compiler.Report.ShowMessage(Messages.VBNC30311, expType.Name, expType.Name)
                result = False
            Case TypeCode.SByte, TypeCode.Int16
                Emitter.EmitConv_I4_Overflow(Info, expType)
                Emitter.EmitNew(Info, Info.Compiler.TypeCache.DecimalConstructor_Int32)
            Case TypeCode.Int32
                Emitter.EmitNew(Info, Info.Compiler.TypeCache.DecimalConstructor_Int32)
            Case TypeCode.Int64
                Emitter.EmitNew(Info, Info.Compiler.TypeCache.DecimalConstructor_Int64)
            Case TypeCode.Byte, TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64
                Emitter.EmitConv_U8_Overflow_Underflow(Info, expType)
                Emitter.EmitNew(Info, Info.Compiler.TypeCache.DecimalConstructor_UInt64)
            Case TypeCode.Double
                Emitter.EmitNew(Info, Info.Compiler.TypeCache.DecimalConstructor_Double)
            Case TypeCode.Single
                Emitter.EmitNew(Info, info.Compiler.TypeCache.DecimalConstructor_Single)
            Case TypeCode.Object
                If Helper.CompareType(expType, Info.Compiler.TypeCache.Object) Then
                    Emitter.EmitCall(Info, Info.Compiler.TypeCache.MS_VB_CS_Conversions_ToDecimal__Object)
                Else
                    Helper.NotImplemented()
                End If
            Case TypeCode.String
                Emitter.EmitCall(Info, Info.Compiler.TypeCache.MS_VB_CS_Conversions_ToDecimal__String)
            Case Else
                Helper.NotImplemented()
        End Select

        Return result
    End Function

    Public Overrides ReadOnly Property ConstantValue() As Object
        Get
            Dim tpCode As TypeCode
            Dim originalValue As Object
            originalValue = Expression.ConstantValue
            tpCode = Helper.GetTypeCode(originalValue.GetType)
            Select Case tpCode
                Case TypeCode.Boolean, TypeCode.SByte, TypeCode.Byte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt32, TypeCode.UInt64, TypeCode.Int64, TypeCode.Decimal
                    Return CDec(originalValue) 'No range checking needed.
                Case TypeCode.Single, TypeCode.Double
                    Dim resultvalue As Object = 0
                    If Compiler.TypeResolution.CheckNumericRange(originalValue, resultvalue, ExpressionType) Then
                        Return resultvalue
                    Else
                        Compiler.Report.ShowMessage(Messages.VBNC30439, ExpressionType.ToString)
                        Return New Decimal
                    End If
                Case Else
                    Compiler.Report.ShowMessage(Messages.VBNC30060, originalValue.ToString, ExpressionType.ToString)
                    Return New Decimal
            End Select
        End Get
    End Property

    Overrides ReadOnly Property ExpressionType() As Type
        Get
            Return Compiler.TypeCache.Decimal '_Descriptor
        End Get
    End Property
End Class
