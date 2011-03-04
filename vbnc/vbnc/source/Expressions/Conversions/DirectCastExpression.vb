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
Public Class DirectCastExpression
    Inherits CTypeExpression

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent, True)
    End Sub

    Shadows Sub Init(ByVal Expression As Expression, ByVal DestinationType As TypeName)
        MyBase.Init(Expression, DestinationType)
    End Sub

    Shadows Sub Init(ByVal Expression As Expression, ByVal DestinationType As Mono.Cecil.TypeReference)
        MyBase.Init(Expression, DestinationType)
    End Sub

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        If CecilHelper.IsGenericParameter(ExpressionType) Then
            result = Expression.GenerateCode(Info.Clone(Me, True, False, Expression.ExpressionType)) AndAlso result
            If Helper.CompareType(Expression.ExpressionType, ExpressionType) = False Then
                Emitter.EmitUnbox_Any(Info, ExpressionType)
            End If
        Else
            If CecilHelper.IsValueType(Expression.ExpressionType) Then
                If Helper.CompareType(ExpressionType, Expression.ExpressionType) Then
                    result = Expression.GenerateCode(Info.Clone(Me, True, False, Expression.ExpressionType)) AndAlso result
                ElseIf Helper.IsEnum(Compiler, ExpressionType) Then
                    result = Expression.GenerateCode(Info.Clone(Me, True, False, Expression.ExpressionType)) AndAlso result
                    Emitter.EmitConversion(Expression.ExpressionType, Helper.GetEnumType(Compiler, ExpressionType), Info)
                Else
                    Return Compiler.Report.ShowMessage(Messages.VBNC99999, Me.Location, ExpressionType.FullName & " - " & Expression.ExpressionType.FullName)
                End If
            ElseIf CecilHelper.IsGenericParameter(ExpressionType) = False AndAlso CecilHelper.IsClass(Expression.ExpressionType) AndAlso CecilHelper.IsValueType(ExpressionType) Then
                result = Expression.GenerateCode(Info.Clone(Me, True, False, Expression.ExpressionType)) AndAlso result
                Emitter.EmitUnbox(Info, ExpressionType)
                Emitter.EmitLoadObject(Info, ExpressionType)
            ElseIf CecilHelper.IsGenericParameter(Expression.ExpressionType) Then
                result = Expression.GenerateCode(Info.Clone(Me, True, False, Expression.ExpressionType)) AndAlso result
                If Helper.CompareType(Expression.ExpressionType, ExpressionType) = False Then
                    Emitter.EmitBox(Info, Expression.ExpressionType)
                    Emitter.EmitCastClass(Info, ExpressionType)
                End If
            Else
                result = Expression.GenerateCode(Info.Clone(Me, True, False, Expression.ExpressionType)) AndAlso result
                If Helper.CompareType(Expression.ExpressionType, ExpressionType) = False Then
                    Emitter.EmitCastClass(Info, ExpressionType)
                End If
            End If
        End If
        Return result
    End Function

    Protected Overrides ReadOnly Property GetKeyword() As KS
        Get
            Return KS.DirectCast
        End Get
    End Property
End Class
