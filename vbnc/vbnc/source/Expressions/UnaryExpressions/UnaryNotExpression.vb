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

Public Class UnaryNotExpression
    Inherits UnaryExpression

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Shadows Sub Init(ByVal Expression As Expression)
        MyBase.Init(Expression)
    End Sub

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        ValidateBeforeGenerateCode(Info)

        Dim expInfo As EmitInfo = Info.Clone(Me, True, False, OperandType)
        result = Expression.GenerateCode(expInfo) AndAlso result

        Select Case Me.OperandTypeCode

            Case TypeCode.Boolean
                Emitter.EmitLoadI4Value(Info, 0I, Compiler.TypeCache.System_Boolean)
                Emitter.EmitEquals(Info, Compiler.TypeCache.System_Boolean)
            Case TypeCode.Byte, TypeCode.SByte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64
                Emitter.EmitNot(Info, OperandType)
            Case TypeCode.Object
                Emitter.EmitCall(Info, Compiler.TypeCache.MS_VB_CS_Operators__NotObject_Object)
            Case Else
                Throw New InternalException(Me)
        End Select

        Return result
    End Function

#If DEBUG Then
    Public Overrides Sub Dump(ByVal Dumper As IndentedTextWriter)
        Dumper.Write("Not ")
        Expression.Dump(Dumper)
    End Sub
#End If

    Public Overrides ReadOnly Property IsConstant() As Boolean
        Get
            Return Expression.IsConstant AndAlso Compiler.TypeResolution.IsIntegralType(Expression.ExpressionType)
        End Get
    End Property

    Public Overrides ReadOnly Property ConstantValue() As Object
        Get
            Helper.Assert(IsConstant)
            Dim value As Object = Expression.ConstantValue
            Helper.Assert(value IsNot Nothing)
            Select Case Helper.GetTypeCode(Compiler, value.GetType)
                Case TypeCode.SByte
                    Return Not CSByte(value)
                Case TypeCode.Byte
                    Return Not CByte(value)
                Case TypeCode.Int16
                    Return Not CShort(value)
                Case TypeCode.Int32
                    Return Not CInt(value)
                Case TypeCode.Int64
                    Return Not CLng(value)
                Case TypeCode.UInt16
                    Return Not CUShort(value)
                Case TypeCode.UInt32
                    Return Not CUInt(value)
                Case TypeCode.UInt64
                    Return Not CULng(value)
                Case TypeCode.Decimal, TypeCode.Double, TypeCode.Single
                    Throw New InternalException(Me)
                Case Else
                    Throw New InternalException(Me)
            End Select
            Helper.Stop()
            Return Nothing
        End Get
    End Property

    Public Overrides ReadOnly Property Keyword() As KS
        Get
            Return KS.Not
        End Get
    End Property
End Class