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

Public Class TryCastExpression
    Inherits CTypeExpression

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent, True)
    End Sub

    Shadows Sub Init(ByVal Expression As Expression, ByVal DestinationType As TypeName)
        MyBase.Init(Expression, DestinationType)
    End Sub

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        result = Expression.GenerateCode(Info.Clone(Me, ExpressionType)) AndAlso result

        If CecilHelper.IsGenericParameter(Expression.ExpressionType) Then
            Emitter.EmitBox(Info, Expression.ExpressionType)
        End If

        Emitter.EmitIsInst(Info, Expression.ExpressionType, ExpressionType)

        Return result
    End Function

    Protected Overrides ReadOnly Property GetKeyword() As KS
        Get
            Return KS.TryCast
        End Get
    End Property
End Class
