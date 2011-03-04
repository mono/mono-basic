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

Public Class UnaryNotExpression
    Inherits UnaryExpression

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Shadows Sub Init(ByVal Expression As Expression)
        MyBase.Init(Expression)
    End Sub

    Overrides ReadOnly Property ExpressionType() As Mono.Cecil.TypeReference
        Get
            Dim result As Mono.Cecil.TypeReference
            Dim lType As Mono.Cecil.TypeReference

            lType = Me.Expression.ExpressionType

            If Helper.IsEnum(Compiler, lType) Then
                result = lType
            Else
                result = MyBase.ExpressionType()
            End If

            Return result
        End Get
    End Property

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

    Public Overrides Function GetConstant(ByRef result As Object, ByVal ShowError As Boolean) As Boolean
        If Not Expression.GetConstant(result, ShowError) Then Return False

        Select Case Helper.GetTypeCode(Compiler, CecilHelper.GetType(Compiler, result))
            Case TypeCode.SByte
                result = Not CSByte(result)
            Case TypeCode.Byte
                result = Not CByte(result)
            Case TypeCode.Int16
                result = Not CShort(result)
            Case TypeCode.Int32
                result = Not CInt(result)
            Case TypeCode.Int64
                result = Not CLng(result)
            Case TypeCode.UInt16
                result = Not CUShort(result)
            Case TypeCode.UInt32
                result = Not CUInt(result)
            Case TypeCode.UInt64
                result = Not CULng(result)
            Case Else
                If ShowError Then Show30059()
                Return False
        End Select

        Return True
    End Function

    Public Overrides ReadOnly Property Keyword() As KS
        Get
            Return KS.Not
        End Get
    End Property
End Class
