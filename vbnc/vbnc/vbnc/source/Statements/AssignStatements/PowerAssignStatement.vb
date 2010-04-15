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

Public Class PowerAssignStatement
    Inherits CompoundAssignmentStatement

    Sub New(ByVal Parent As ParsedObject)
        MyBase.new(Parent)
    End Sub

#If DEBUG Then
    Public Overrides ReadOnly Property AssignmentType() As KS
        Get
            Return KS.PowerAssign
        End Get
    End Property
#End If

    Protected Overloads Overrides Function ResolveStatement(ByVal LSide As Expression, ByVal RSide As Expression) As Expression
        Dim result As Expression = New ExponentExpression(Me, LSide, RSide)
        result = ConversionExpression.GetTypeConversion(Me, result, LSide.ExpressionType)
        Return result
    End Function
End Class
