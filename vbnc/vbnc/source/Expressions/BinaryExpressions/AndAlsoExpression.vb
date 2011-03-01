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

Public Class AndAlsoExpression
    Inherits BinaryExpression

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True
        Dim opType As TypeCode = MyBase.OperandTypeCode

        ValidateBeforeGenerateCode(Info)

        Select Case opType
            Case TypeCode.Boolean, TypeCode.Object
                If opType = TypeCode.Object Then
                    Helper.Assert(Helper.CompareType(OperandType, Compiler.TypeCache.System_Object))
                End If

                Dim loadfalse, loadtrue, endexp As Label
                loadfalse = Emitter.DefineLabel(Info)
                loadtrue = Emitter.DefineLabel(Info)
                endexp = Emitter.DefineLabel(Info)

                result = m_LeftExpression.GenerateCode(Info) AndAlso result
                If opType = TypeCode.Object Then
                    Emitter.EmitCall(Info, Compiler.TypeCache.MS_VB_CS_Conversions__ToBoolean_Object)
                End If
                Emitter.EmitBranchIfFalse(Info, loadfalse)

                result = m_RightExpression.GenerateCode(Info) AndAlso result
                If opType = TypeCode.Object Then
                    Emitter.EmitCall(Info, Compiler.TypeCache.MS_VB_CS_Conversions__ToBoolean_Object)
                End If
                Emitter.EmitBranchIfTrue(Info, loadtrue)

                Emitter.MarkLabel(Info, loadfalse) '
                Emitter.EmitLoadValue(Info, False) 'Load false value
                Emitter.EmitBranch(Info, endexp)

                Emitter.MarkLabel(Info, loadtrue)
                Emitter.EmitLoadValue(Info, True) 'Load true value

                Emitter.MarkLabel(Info, endexp) 'The end of the expression
               
                If opType = TypeCode.Object Then
                    Emitter.EmitBox(Info, Compiler.TypeCache.System_Boolean)
                    Emitter.EmitCall(Info, Compiler.TypeCache.System_Runtime_CompilerServices_RuntimeHelpers__GetObjectValue_Object)
                End If
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
            Return KS.AndAlso
        End Get
    End Property

    Public Overrides Function GetConstant(ByRef result As Object, ByVal lvalue As Object, ByVal rvalue As Object) As Boolean
        If TypeOf lvalue Is Boolean = False Then Return False
        If TypeOf rvalue Is Boolean = False Then Return False
        result = CBool(lvalue) AndAlso CBool(rvalue)
        Return True
    End Function
End Class
