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
Public Class DirectCastExpression
    Inherits CTypeExpression

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Shadows Sub Init(ByVal Expression As Expression, ByVal DestinationType As TypeName)
        MyBase.Init(Expression, DestinationType)
    End Sub

    Shadows Sub Init(ByVal Expression As Expression, ByVal DestinationType As Type)
        MyBase.Init(Expression, DestinationType)
    End Sub

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        If ExpressionType.IsGenericParameter Then
            result = Expression.GenerateCode(Info.Clone(Me, True, False, Expression.ExpressionType)) AndAlso result
            If Helper.CompareType(Expression.ExpressionType, ExpressionType) = False Then
                Emitter.EmitUnbox_Any(Info, ExpressionType)
            End If
        Else
            If Expression.ExpressionType.IsValueType Then
                If Helper.CompareType(ExpressionType, Expression.ExpressionType) Then
                    result = Expression.GenerateCode(Info.Clone(Me, True, False, Expression.ExpressionType)) AndAlso result
                Else
                    Return Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
                End If
            ElseIf ExpressionType.IsGenericParameter = False AndAlso Expression.ExpressionType.IsClass AndAlso ExpressionType.IsValueType Then
                result = Expression.GenerateCode(Info.Clone(Me, True, False, Expression.ExpressionType)) AndAlso result
                Emitter.EmitUnbox(Info, ExpressionType)
                Emitter.EmitLoadObject(Info, ExpressionType)
            Else
                result = Expression.GenerateCode(Info.Clone(Me, True, False, Expression.ExpressionType)) AndAlso result
                If Helper.CompareType(Expression.ExpressionType, ExpressionType) = False Then
                    Emitter.EmitCastClass(Info, Expression.ExpressionType, ExpressionType)
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
